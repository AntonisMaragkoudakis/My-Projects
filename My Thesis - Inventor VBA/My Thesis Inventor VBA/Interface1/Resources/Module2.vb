Imports System.Math
Imports Inventor
Module Module2






    Sub Model_21_3D_Construction(ByVal myApplication As Inventor.Application, ByVal d1 As Double, ByVal Ypsos1 As Double, ByVal d2 As Double, ByVal Ypsos2 As Double, ByVal YpsosTomhs As Double, ByRef partDoc As PartDocument, ByRef partComp As SheetMetalComponentDefinition, ByRef Rip_precision As Double)


        Dim unitsOM As UnitsOfMeasure
        unitsOM = partDoc.UnitsOfMeasure
        unitsOM.LengthUnits = Inventor.UnitsTypeEnum.kMillimeterLengthUnits
        unitsOM.AngleUnits = Inventor.UnitsTypeEnum.kDegreeAngleUnits
        'unitsOM.LengthDisplayPrecision = 8
        'unitsOM.AngleDisplayPrecision = 8



        ''Surface 1 extrude
        Dim sk1 As PlanarSketch = partComp.Sketches.Add(partComp.WorkPlanes.Item(2))
        sk1.SketchCircles.AddByCenterRadius(myApplication.TransientGeometry.CreatePoint2d(0, 0), d1 / 20)
        Dim oProfile1 As Profile
        oProfile1 = sk1.Profiles.AddForSurface

        Dim oExtrudeDef1 As ExtrudeDefinition
        oExtrudeDef1 = partComp.Features.ExtrudeFeatures.CreateExtrudeDefinition(oProfile1, PartFeatureOperationEnum.kSurfaceOperation)
        oExtrudeDef1.SetDistanceExtent((Ypsos1 - YpsosTomhs) / 10, PartFeatureExtentDirectionEnum.kPositiveExtentDirection)
        oExtrudeDef1.SetDistanceExtentTwo(YpsosTomhs / 10)
        Dim oExtrude1 As ExtrudeFeature
        oExtrude1 = partComp.Features.ExtrudeFeatures.Add(oExtrudeDef1)

        ''''''''Teliko Plane Gwnias Default stis 90
        Dim oPlane As WorkPlane
        oPlane = partComp.WorkPlanes.AddByLinePlaneAndAngle(partComp.WorkAxes(3), partComp.WorkPlanes(2), 90 * Math.PI / 180) 'alliws tha einai (90 - txtBoxGwnias.text) * Math.PI / 180


        Dim upExtr2 As Double = Ypsos2 / 10
        'Surface 2 extrude
        Dim sk2 As PlanarSketch = partComp.Sketches.Add(oPlane)
        sk2.SketchCircles.AddByCenterRadius(myApplication.TransientGeometry.CreatePoint2d(0, 0), d2 / 20)
        Dim oProfile4 As Profile
        oProfile4 = sk2.Profiles.AddForSurface

        Dim oExtrudeDef2 As ExtrudeDefinition
        oExtrudeDef2 = partComp.Features.ExtrudeFeatures.CreateExtrudeDefinition(oProfile4, PartFeatureOperationEnum.kSurfaceOperation)
        oExtrudeDef2.SetDistanceExtent(upExtr2, PartFeatureExtentDirectionEnum.kPositiveExtentDirection)
        Dim oExtrude2 As ExtrudeFeature
        oExtrude2 = partComp.Features.ExtrudeFeatures.Add(oExtrudeDef2)



        '''''''''''''''''''''
        ' Sheet MEatal Defaults ( Thickness kai k_factor_value )
        partComp.SheetMetalStyles.Item(2).Activate()
        Dim default_k_factor As String
        default_k_factor = partComp.UnfoldMethod.kFactor
        partComp.UnfoldMethod.kFactor = "1 ul"
        'Alagh Thicknes isws xreiastei
        partComp.UseSheetMetalStyleThickness = False
        Dim oThicknessParam As Parameter
        oThicknessParam = partComp.Thickness
        'Change the value of the parameter.
        oThicknessParam.Value *= 0.1
        'MessageBox.Show(oThicknessParam.Value)




        'Split
        Dim oSplit1 As SplitFeature
        oSplit1 = partDoc.ComponentDefinition.Features.SplitFeatures.SplitFaces(partComp.WorkSurfaces.Item(2), True, partComp.WorkSurfaces.Item(1))

        Dim oSplit2 As SplitFeature
        oSplit2 = partDoc.ComponentDefinition.Features.SplitFeatures.SplitFaces(partComp.WorkSurfaces.Item(1), True, partComp.WorkSurfaces.Item(2))

        Dim oFaceColl1 As FaceCollection
        oFaceColl1 = myApplication.TransientObjects.CreateFaceCollection
        oFaceColl1.Add(partComp.WorkSurfaces.Item(1)._SurfaceBody.Faces.Item(1))

        Dim oThicken1 As ThickenFeature
        oThicken1 = partComp.Features.ThickenFeatures.Add(oFaceColl1, "0.05 mm", PartFeatureExtentDirectionEnum.kPositiveExtentDirection, PartFeatureOperationEnum.kNewBodyOperation)



        ' Rip precicion ( Ousiastika epilegw oso to dynaton mikrotero rip symfwna me thn Diametro ) 
        If d1 < 10 Then
            Rip_precision = d1 * 0.00001
        ElseIf d1 < 100 Then
            Rip_precision = d1 * 0.000001
        ElseIf d1 < 10000 Then
            Rip_precision = d1 * 0.0000001
        ElseIf d1 < 100000 Then
            Rip_precision = d1 * 0.00000005
        Else
            Rip_precision = d1 * 0.00000001
        End If
        'San sketch h feta pou epitrepei ston swlina na ksediplwsei
        Dim sk3 As PlanarSketch = partComp.Sketches.Add(partComp.WorkPlanes.Item(1))
        sk3.SketchLines.AddAsTwoPointCenteredRectangle(myApplication.TransientGeometry.CreatePoint2d((Ypsos1 / 2 - YpsosTomhs) / 10, 0), myApplication.TransientGeometry.CreatePoint2d((Ypsos1 - YpsosTomhs) / 10 + 0.1, Rip_precision))
        Dim oProfile3 As Profile
        oProfile3 = sk3.Profiles.AddForSolid
        'Kanw cut thn feta ayth
        Dim oExtrudeDef3 As ExtrudeDefinition
        oExtrudeDef3 = partComp.Features.ExtrudeFeatures.CreateExtrudeDefinition(oProfile3, PartFeatureOperationEnum.kCutOperation)
        oExtrudeDef3.SetDistanceExtent(d1 / 20, PartFeatureExtentDirectionEnum.kPositiveExtentDirection)
        Dim oExtrude3 As ExtrudeFeature
        oExtrude3 = partComp.Features.ExtrudeFeatures.Add(oExtrudeDef3)


        'Visibilities off
        partDoc.ObjectVisibility.UserWorkPlanes = False
        partDoc.ObjectVisibility.ConstructionSurfaces = False


    End Sub






    Public Sub Rotate_Fp21(ByVal partComp As SheetMetalComponentDefinition, ByVal Rip_precision As Double, ByVal Diam As Double, ByVal YpsosStoY As Double, ByVal fp As FlatPattern, ByRef peristrafike As Integer, ByRef oLength As Double, ByRef oWidth As Double)


        ' Ypologismoi gia na vrw telika poso einai h katw plevra tou anaptygmatos exwntas aferesei to Rip
        Dim katheth_mm As Double
        Dim theoritiko As Double
        Dim pososto_gwnias As Double
        katheth_mm = Rip_precision * 40
        pososto_gwnias = (2 * Math.PI - Math.Atan(katheth_mm / Diam)) / (2 * Math.PI)
        theoritiko = pososto_gwnias * Diam * Math.PI


        ' Epidi to inventor sto flat pattern vazei to megalytero megethos ston x aksona kanw peristofh -90 otan  ( (YpsosTomhs > Diam * Math.PI) 
        ' etsi wste me to rotete ayto na mpei sto x axis h diastash  Diametros * 3.1415 

        If (YpsosStoY > theoritiko) Then

            fp.FlatPatternOrientations.ActiveFlatPatternOrientation.AlignmentType = AlignmentTypeEnum.kVerticalAlignment
            fp.FlatPatternOrientations.ActiveFlatPatternOrientation.FlipAlignmentAxis = True
            fp.FlatPatternOrientations.ActiveFlatPatternOrientation.FlipBaseFace = True
            peristrafike = 1
        Else

            fp.FlatPatternOrientations.ActiveFlatPatternOrientation.FlipBaseFace = True
            peristrafike = 2
        End If

        ' oLength einai to max sto x axis tou flatt pattern--> na einai Diametros *pi  .....  oWidth einai to max sto y axis tou flat pattern --> na einai max ypsos
        oLength = partComp.FlatPattern.Length
        oWidth = partComp.FlatPattern.Width

    End Sub






    Public Sub Ypologismos_Edge21(ByVal myEdge1 As Inventor.Edge, ByVal myEdge2 As Inventor.Edge, IsesDiametroi As Boolean, ByVal NumOfPoints As Integer, ByVal peristrafike As Integer, ByVal YpsosTomhs As Double, ByVal Ypsos1 As Double, ByRef xPoints() As Double, ByRef yPoints() As Double)


        Dim oCurveEval As CurveEvaluator
        oCurveEval = myEdge1.Evaluator
        ' Get the parametric range of the curve.
        Dim dMinParam As Double
        Dim dMaxParam As Double
        Call oCurveEval.GetParamExtents(dMinParam, dMaxParam)
        '''''Gia na vrw to length gia debugin kurios
        'Dim curveLength As Double
        'Call oCurveEval.GetLengthAtParam(dMinParam, dMaxParam, curveLength)
        'MessageBox.Show(curveLength)



        If Not IsesDiametroi Then '' oxi ises diametroi



            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' kalo gia isa length
            For i = 0 To NumOfPoints

                ' Calculate the current parameter to evaluate.
                Dim currentParam As Double                                              '''' mallon akyro to parakato symperasma
                currentParam = dMinParam + ((dMaxParam - dMinParam) / NumOfPoints) * i  '''' otan einai kykliko to edge oti einai h loupa p.x to (NumOfPoints - 1) to kanw syn 1

                ' Assign the value to an array since the GetPointAtParam
                ' takes an array as input.
                Dim adParam(0) As Double
                adParam(0) = currentParam

                ' Get the coordinates of the parameter point in model space.
                Dim adPoints(2) As Double
                Call oCurveEval.GetPointAtParam(adParam, adPoints)


                'MsgBox(dMinParam & "  ,  " & dMaxParam & "  ,  " & adPoints(0) & "  ,  " & adPoints(1) & "  ,  " & adPoints(2))
                If i <> NumOfPoints Then
                    If peristrafike = 2 Then

                        xPoints(i) = adPoints(0) * 10
                        yPoints(i) = 10 * adPoints(1) + Ypsos1
                    Else

                        xPoints(i) = (adPoints(0) - YpsosTomhs / 20) * 10 - (Ypsos1 - YpsosTomhs) / 2
                        yPoints(i) = 10 * (adPoints(1) + YpsosTomhs / 20) + (Ypsos1 - YpsosTomhs) / 2
                    End If
                End If

            Next

            ''''''' antigrafw se temp
            Dim XtempArray(NumOfPoints - 1) As Double
            Dim YtempArray(NumOfPoints - 1) As Double
            For i = 0 To NumOfPoints - 1
                XtempArray(i) = xPoints(i)
                YtempArray(i) = yPoints(i)
            Next
            '''''' edw vazw se swsth seira
            For i = 0 To NumOfPoints / 2 - 1
                xPoints(i) = XtempArray(i + NumOfPoints / 2)
                yPoints(i) = YtempArray(i + NumOfPoints / 2)
            Next
            For i = NumOfPoints / 2 To NumOfPoints - 1
                xPoints(i) = XtempArray(i - NumOfPoints / 2)
                yPoints(i) = YtempArray(i - NumOfPoints / 2)
            Next



        Else


            ''''''''''''''''' Ypologismos Edge 1 '''''''''''''''''''''
            ' Iterate into steps over the curve length and print the
            ' parameter values and corresponding model points.
            For i = 0 To NumOfPoints / 2

                ' Calculate the current parameter to evaluate.
                Dim currentParam As Double
                currentParam = dMinParam + ((dMaxParam - dMinParam) / (NumOfPoints / 2)) * i  '' An exei diaforetikh arxh kai telos to afeinw idio

                ' Assign the value to an array since the GetPointAtParam
                ' takes an array as input.
                Dim adParam(0) As Double
                adParam(0) = currentParam

                ' Get the coordinates of the parameter point in model space.
                Dim adPoints(2) As Double
                Call oCurveEval.GetPointAtParam(adParam, adPoints)


                'MsgBox(dMinParam & "  ,  " & dMaxParam & "  ,  " & adPoints(0) & "  ,  " & adPoints(1) & "  ,  " & adPoints(2))

                If peristrafike = 2 Then

                    xPoints(i) = adPoints(0) * 10
                    yPoints(i) = 10 * adPoints(1) + Ypsos1
                Else

                    xPoints(i) = (adPoints(0) - YpsosTomhs / 20) * 10 - (Ypsos1 - YpsosTomhs) / 2
                    yPoints(i) = 10 * (adPoints(1) + YpsosTomhs / 20) + (Ypsos1 - YpsosTomhs) / 2
                End If

            Next



            ''''''''''''''''' Ypologismos Edge 2 '''''''''''''''''''''
            ' Pairnw to Evaluator gia to myEdge2
            oCurveEval = myEdge2.Evaluator
            ' Get the parametric range of the curve.
            Call oCurveEval.GetParamExtents(dMinParam, dMaxParam)
            '''''Gia na vrw to length gia debugin kurios
            'Dim curveLength As Double
            'Call oCurveEval.GetLengthAtParam(dMinParam, dMaxParam, curveLength)
            'MessageBox.Show(curveLength)
            Dim xPoints1(NumOfPoints - 1) As Double
            Dim yPoints1(NumOfPoints - 1) As Double



            ' Iterate into steps over the curve length and print the
            ' parameter values and corresponding model points.
            For i = 0 To NumOfPoints / 2

                ' Calculate the current parameter to evaluate.
                Dim currentParam As Double
                currentParam = dMinParam + ((dMaxParam - dMinParam) / (NumOfPoints / 2)) * i

                ' Assign the value to an array since the GetPointAtParam
                ' takes an array as input.
                Dim adParam(0) As Double
                adParam(0) = currentParam

                ' Get the coordinates of the parameter point in model space.
                Dim adPoints(2) As Double
                Call oCurveEval.GetPointAtParam(adParam, adPoints)


                'MsgBox(dMinParam & "  ,  " & dMaxParam & "  ,  " & adPoints(0) & "  ,  " & adPoints(1) & "  ,  " & adPoints(2))
                If i <> NumOfPoints / 2 Then

                    If peristrafike = 2 Then

                        xPoints(i + NumOfPoints / 2) = adPoints(0) * 10
                        yPoints(i + NumOfPoints / 2) = 10 * adPoints(1) + Ypsos1
                    Else
                        'MsgBox(i + NumOfPoints / 2)
                        xPoints(i + NumOfPoints / 2) = (adPoints(0) - YpsosTomhs / 20) * 10 - (Ypsos1 - YpsosTomhs) / 2
                        yPoints(i + NumOfPoints / 2) = 10 * (adPoints(1) + YpsosTomhs / 20) + (Ypsos1 - YpsosTomhs) / 2
                    End If

                End If

            Next

            ''''' an thelw na metraei antitheta apo tin fora tou rologiou to vgazw apo sxolia
            ''''' antistofh array gia swsth seira na metraei antitheta apo tin fora tou rologiou
            'Dim XtempArray(NumOfPoints - 1) As Double
            'Dim YtempArray(NumOfPoints - 1) As Double
            'For i = 0 To NumOfPoints - 1
            '    XtempArray(i) = xPoints(i)
            '    YtempArray(i) = yPoints(i)
            'Next
            ''''''' edw kanw thn antistrofh panw ftiaxnw ta temp
            'For i = 1 To NumOfPoints - 1
            '    xPoints(i) = XtempArray(NumOfPoints - i)
            '    yPoints(i) = YtempArray(NumOfPoints - i)
            'Next


        End If


    End Sub









    Sub Model_22_3D_Construction(ByVal myApplication As Inventor.Application, ByVal d1 As Double, ByVal Ypsos1 As Double, ByVal d2 As Double, ByVal Ypsos2 As Double, ByVal YpsosTomhs As Double, ByRef partDoc As PartDocument, ByRef partComp As SheetMetalComponentDefinition, ByRef Rip_precision As Double)


        Dim unitsOM As UnitsOfMeasure
        unitsOM = partDoc.UnitsOfMeasure
        unitsOM.LengthUnits = Inventor.UnitsTypeEnum.kMillimeterLengthUnits
        unitsOM.AngleUnits = Inventor.UnitsTypeEnum.kDegreeAngleUnits
        'unitsOM.LengthDisplayPrecision = 8
        'unitsOM.AngleDisplayPrecision = 8



        ''Surface 1 extrude
        Dim sk1 As PlanarSketch = partComp.Sketches.Add(partComp.WorkPlanes.Item(2))
        sk1.SketchCircles.AddByCenterRadius(myApplication.TransientGeometry.CreatePoint2d(0, 0), d1 / 20)
        Dim oProfile1 As Profile
        oProfile1 = sk1.Profiles.AddForSurface

        ' empros pisw extrude apo to (0,0)
        Dim oExtrudeDef1 As ExtrudeDefinition
        oExtrudeDef1 = partComp.Features.ExtrudeFeatures.CreateExtrudeDefinition(oProfile1, PartFeatureOperationEnum.kSurfaceOperation)
        oExtrudeDef1.SetDistanceExtent((Ypsos1 - YpsosTomhs) / 10, PartFeatureExtentDirectionEnum.kPositiveExtentDirection)
        oExtrudeDef1.SetDistanceExtentTwo(YpsosTomhs / 10)
        Dim oExtrude1 As ExtrudeFeature
        oExtrude1 = partComp.Features.ExtrudeFeatures.Add(oExtrudeDef1)


        ''''''''Teliko Plane Gwnias Default stis 90
        Dim oPlane As WorkPlane
        oPlane = partComp.WorkPlanes.AddByLinePlaneAndAngle(partComp.WorkAxes(3), partComp.WorkPlanes(2), 90 * Math.PI / 180) 'alliws tha einai (90 - txtBoxGwnias.text) * Math.PI / 180


        ' orizontiou sketch sto oPlane
        Dim sk2 As PlanarSketch = partComp.Sketches.Add(oPlane)
        sk2.SketchCircles.AddByCenterRadius(myApplication.TransientGeometry.CreatePoint2d(0, 0), d2 / 20)
        Dim oProfile2 As Profile
        oProfile2 = sk2.Profiles.AddForSurface

        ' aplo extrude 2
        Dim oExtrudeDef2 As ExtrudeDefinition
        oExtrudeDef2 = partComp.Features.ExtrudeFeatures.CreateExtrudeDefinition(oProfile2, PartFeatureOperationEnum.kSurfaceOperation)
        oExtrudeDef2.SetDistanceExtent(Ypsos2 / 10, PartFeatureExtentDirectionEnum.kPositiveExtentDirection)
        Dim oExtrude2 As ExtrudeFeature
        oExtrude2 = partComp.Features.ExtrudeFeatures.Add(oExtrudeDef2)



        '''''''''''''''''''''
        ' Sheet MEatal Defaults ( Thickness kai k_factor_value )
        partComp.SheetMetalStyles.Item(2).Activate()
        Dim default_k_factor As String
        default_k_factor = partComp.UnfoldMethod.kFactor
        partComp.UnfoldMethod.kFactor = "1 ul"
        'Alagh Thicknes isws xreiastei
        partComp.UseSheetMetalStyleThickness = False
        Dim oThicknessParam As Parameter
        oThicknessParam = partComp.Thickness
        'Change the value of the parameter.
        oThicknessParam.Value *= 0.1
        'MessageBox.Show(oThicknessParam.Value)




        'Split   ''' diafora me to 3D_21 mono sto oSplit1 allazw mono to prwto tou orisma se WorkSurfaces.Item(2)
        Dim oSplit1 As SplitFeature
        oSplit1 = partDoc.ComponentDefinition.Features.SplitFeatures.SplitFaces(partComp.WorkSurfaces.Item(2), True, partComp.WorkSurfaces.Item(1))

        Dim oSplit2 As SplitFeature
        oSplit2 = partDoc.ComponentDefinition.Features.SplitFeatures.SplitFaces(partComp.WorkSurfaces.Item(1), True, partComp.WorkSurfaces.Item(2))


        ' xrisimo gia debuging tou orismatos tou oFaceColl
        'MsgBox(partComp.WorkSurfaces.Item(2)._SurfaceBody.Faces.Count)


        ''''' diafora me to 3D_21 sto oti parousiazwntai 3 surface faces sthn periptwsh tou d1=d2 opou kai prepei na piasw to 3o surface face (_SurfaceBody.Faces.Item(3))
        Dim oFaceColl1 As FaceCollection
        oFaceColl1 = myApplication.TransientObjects.CreateFaceCollection
        If d1 = d2 Then
            oFaceColl1.Add(partComp.WorkSurfaces.Item(2)._SurfaceBody.Faces.Item(3))
        Else
            oFaceColl1.Add(partComp.WorkSurfaces.Item(2)._SurfaceBody.Faces.Item(1))
        End If
        ' thicken to swsto face epifanias
        Dim oThicken1 As ThickenFeature
        oThicken1 = partComp.Features.ThickenFeatures.Add(oFaceColl1, "0.05 mm", PartFeatureExtentDirectionEnum.kPositiveExtentDirection, PartFeatureOperationEnum.kNewBodyOperation)



        ' Rip precicion ( Ousiastika epilegw oso to dynaton mikrotero rip symfwna me thn Diametro ) 
        If d1 < 10 Then
            Rip_precision = d1 * 0.00001
        ElseIf d1 < 100 Then
            Rip_precision = d1 * 0.000001
        ElseIf d1 < 10000 Then
            Rip_precision = d1 * 0.0000001
        ElseIf d1 < 100000 Then
            Rip_precision = d1 * 0.00000005
        Else
            Rip_precision = d1 * 0.00000001
        End If
        'San sketch h feta pou epitrepei ston swlina na ksediplwsei
        Dim sk3 As PlanarSketch = partComp.Sketches.Add(partComp.WorkPlanes.Item(2))
        sk3.SketchLines.AddAsTwoPointCenteredRectangle(myApplication.TransientGeometry.CreatePoint2d((Ypsos2 / 2) / 10, 0), myApplication.TransientGeometry.CreatePoint2d((Ypsos2) / 10 + 0.1, Rip_precision))
        Dim oProfile3 As Profile
        oProfile3 = sk3.Profiles.AddForSolid
        'Kanw cut thn feta ayth
        Dim oExtrudeDef3 As ExtrudeDefinition
        oExtrudeDef3 = partComp.Features.ExtrudeFeatures.CreateExtrudeDefinition(oProfile3, PartFeatureOperationEnum.kCutOperation)
        oExtrudeDef3.SetDistanceExtent(d1 / 20, PartFeatureExtentDirectionEnum.kPositiveExtentDirection)
        Dim oExtrude3 As ExtrudeFeature
        oExtrude3 = partComp.Features.ExtrudeFeatures.Add(oExtrudeDef3)


        'Visibilities off
        partDoc.ObjectVisibility.UserWorkPlanes = False
        partDoc.ObjectVisibility.ConstructionSurfaces = False


    End Sub




    Public Sub UnfoldTheFaceOrizontiou(ByVal myApplication As Inventor.Application, ByVal partComp As SheetMetalComponentDefinition, ByRef TheFaceCounter As Integer)


        Dim myface As Face
        Dim counterr As Integer
        counterr = 0
        Dim Params(0 To 1) As Double
        Dim Normals(0 To 2) As Double
        For Each myface In partComp.SurfaceBodies(1).Faces
            counterr += 1
            Call myface.Evaluator.GetNormal(Params, Normals)
            Dim oUnitNormal As UnitVector
            oUnitNormal = myApplication.TransientGeometry.CreateUnitVector(Normals(0), Normals(1), Normals(2))
            'MsgBox("Planar Face[" & counterr & "] Normal: [" & oUnitNormal.X & ", " & oUnitNormal.Y & ", " & oUnitNormal.Z & "]")
            If oUnitNormal.X = 0 And oUnitNormal.Y = 0 And oUnitNormal.Z = 1 Then
                TheFaceCounter = counterr
            End If
        Next
        ''''''''''''' 7 einai to max '''''''''' Apodekta to 4 kai to 6 anapodo sto pc mou
        myface = partComp.SurfaceBodies(1).Faces.Item(TheFaceCounter)
        partComp.ASideDefinitions.Add(myface)

        'Flatt Pattern
        partComp.Unfold()

        '' Epistrofh sto folded part
        'partComp.FlatPattern.ExitEdit()

    End Sub


    Public Sub SelectRotateCase22(ByVal Rip_precision As Double, ByVal Diam As Double, ByVal D1 As Double, ByRef YpsosStoY As Double, ByRef peristrafike2 As Integer) ''ByVal fp As FlatPattern, 


        ' Ypologismoi gia na vrw telika poso einai h katw plevra tou anaptygmatos exwntas aferesei to Rip
        Dim katheth_mm As Double
        Dim theoritiko As Double
        Dim pososto_gwnias As Double
        katheth_mm = Rip_precision * 40
        pososto_gwnias = (2 * Math.PI - Math.Atan(katheth_mm / Diam)) / (2 * Math.PI)
        theoritiko = pososto_gwnias * Diam * Math.PI


        ' Epidi to inventor sto flat pattern vazei to megalytero megethos ston x aksona kanw peristofh -90 otan  ( (YpsosTomhs > Diam * Math.PI) 
        ' etsi wste me to rotete ayto na mpei sto x axis h diastash  Diametros * 3.1415 
        peristrafike2 = 0
        If (YpsosStoY >= theoritiko) And Diam = D1 Then         'Kai flip ta axis (toumpa ta axis) kai vertical

            'fp.FlatPatternOrientations.ActiveFlatPatternOrientation.AlignmentType = AlignmentTypeEnum.kVerticalAlignment
            'fp.FlatPatternOrientations.ActiveFlatPatternOrientation.FlipAlignmentAxis = True
            peristrafike2 = 1

        ElseIf (theoritiko >= YpsosStoY) And Diam = D1 Then    'Mono base flip (toumpa apo thn vash)

            'fp.FlatPatternOrientations.ActiveFlatPatternOrientation.FlipBaseFace = True
            peristrafike2 = 2

        ElseIf (YpsosStoY > theoritiko) And Diam <> D1 Then    'Mono vertical

            'fp.FlatPatternOrientations.ActiveFlatPatternOrientation.AlignmentType = AlignmentTypeEnum.kVerticalAlignment
            peristrafike2 = 3

        End If

    End Sub






    Public Sub Ypologismos_Edge22(ByVal oEdges22 As Edges, ByVal NumOfPoints As Integer, ByVal peristrafike2 As Integer, ByVal diam As Double, ByVal YpsosStoY As Double, ByRef xPoints22() As Double, ByRef yPoints22() As Double)



        Dim oCurveEval As CurveEvaluator
        Dim dMinParam As Double
        Dim dMaxParam As Double
        Dim myedge22 As Edge



        If peristrafike2 = 1 Then    ' exw 3 edges na analysw

            'dialegw edge pou tha analysw
            myedge22 = oEdges22.Item(10)   ' mporei na einai  10, 12, 13, 16

        ElseIf peristrafike2 = 2 Then

            'dialegw edge pou tha analysw
            myedge22 = oEdges22.Item(12)   ' mporei na einai  10, 12, 13, 16

        ElseIf peristrafike2 = 3 Then ' d1 <> d2

            'dialegw edge pou tha analysw
            myedge22 = oEdges22.Item(12)   ' mporei na einai  12, 13

        Else ' d1 <> d2

            'dialegw edge pou tha analysw
            myedge22 = oEdges22.Item(12)   ' mporei na einai 12, 13

        End If


        'pairnw to evaluator
        oCurveEval = myedge22.Evaluator
        ' Get the parametric range of the curve.
        Call oCurveEval.GetParamExtents(dMinParam, dMaxParam)
        '''''Gia na vrw to length gia debugin kurios
        'Dim curveLength As Double
        'Call oCurveEval.GetLengthAtParam(dMinParam, dMaxParam, curveLength)
        'MessageBox.Show(curveLength)




        ' Iterate into steps over the curve length and print the
        ' parameter values and corresponding model points.
        For i = 0 To (NumOfPoints / 4)


            ' Calculate the current parameter to evaluate.
            Dim currentParam As Double
            currentParam = dMinParam + ((dMaxParam - dMinParam) / (NumOfPoints / 4)) * i  '''' otan den einai kykliko to edge oti einai h loupa to afeinw idio p.x to (NumOfPoints / 2)

            ' Assign the value to an array since the GetPointAtParam
            ' takes an array as input.
            Dim adParam(0) As Double
            adParam(0) = currentParam

            ' Get the coordinates of the parameter point in model space.
            Dim adPoints(2) As Double
            Call oCurveEval.GetPointAtParam(adParam, adPoints)


            If peristrafike2 = 1 Then

                xPoints22(i) = Abs(10 * adPoints(1) - Math.PI * diam)
                yPoints22(i) = 10 * adPoints(0)

            ElseIf peristrafike2 = 2 Then

                xPoints22(i) = 10 * adPoints(0)
                yPoints22(i) = Abs(10 * adPoints(1))

            ElseIf peristrafike2 = 3 Then

                xPoints22(i) = 10 * adPoints(1)
                yPoints22(i) = YpsosStoY + Abs(10 * adPoints(0))

            Else

                xPoints22(i) = 10 * adPoints(0)
                yPoints22(i) = 10 * adPoints(1) + YpsosStoY

            End If

        Next



        '''' kanw antistrofh tou mikrou edge se oles tis periptwseis ektos tou peristrafike2 = 1 pou den htan antistrofo
        If peristrafike2 = 1 Or peristrafike2 = 3 Or peristrafike2 = 0 Then

            ''''''ftiaxnw ta temp
            Dim XtempArray(NumOfPoints / 4) As Double
            Dim YtempArray(NumOfPoints / 4) As Double
            For i = 0 To NumOfPoints / 4
                XtempArray(i) = xPoints22(i)
                YtempArray(i) = yPoints22(i)
            Next

            '''' kai kanw antistrofh
            For i = 0 To NumOfPoints / 4
                xPoints22(i) = XtempArray(NumOfPoints / 4 - i)
                yPoints22(i) = YtempArray(NumOfPoints / 4 - i)
            Next

        End If



        '''' kanw miroring to 1/4 tou synolou sto 1/2
        Dim j As Integer = 2
        For i = NumOfPoints / 4 + 1 To NumOfPoints / 2

            yPoints22(i) = yPoints22(i - j)
            xPoints22(i) = 2 * xPoints22(NumOfPoints / 4) - xPoints22(i - j)

            j += 2
        Next


        '''' kanw miroring to 1/2 tou synolou sto synolo
        j = 2
        For i = NumOfPoints / 2 + 1 To NumOfPoints

            yPoints22(i) = yPoints22(i - j)
            xPoints22(i) = 2 * xPoints22(NumOfPoints / 2) - xPoints22(i - j)

            j += 2
        Next




    End Sub



    Public Sub efarmoghRotate22(ByVal partComp As SheetMetalComponentDefinition, ByVal fp As FlatPattern, ByVal peristrafike2 As Integer, ByRef oLength2 As Double, ByRef oWidth2 As Double)


        ' Epidi to inventor sto flat pattern vazei to megalytero megethos ston x aksona kanw peristofh -90 otan  ( (YpsosTomhs > Diam * Math.PI) 
        ' etsi wste me to rotete ayto na mpei sto x axis h diastash  Diametros * 3.1415 
        If peristrafike2 = 1 Then         'Kai flip ta axis (toumpa ta axis) kai vertical

            fp.FlatPatternOrientations.ActiveFlatPatternOrientation.AlignmentType = AlignmentTypeEnum.kVerticalAlignment
            fp.FlatPatternOrientations.ActiveFlatPatternOrientation.FlipAlignmentAxis = True

        ElseIf peristrafike2 = 2 Then    'Mono base flip (toumpa apo thn vash)

            fp.FlatPatternOrientations.ActiveFlatPatternOrientation.FlipBaseFace = True

        ElseIf peristrafike2 = 3 Then    'Mono vertical

            fp.FlatPatternOrientations.ActiveFlatPatternOrientation.AlignmentType = AlignmentTypeEnum.kVerticalAlignment

        End If

        ' oLength einai to max sto x axis tou flatt pattern--> na einai Diametros *pi  .....  oWidth einai to max sto y axis tou flat pattern --> na einai max ypsos
        oLength2 = partComp.FlatPattern.Length
        oWidth2 = partComp.FlatPattern.Width

    End Sub



    Public Sub Fill_List2(NumOfPoints As Double, ByVal xPointsValues As Double(), ByVal yPointsValues As Double(), ByVal xPointsValues22 As Double(), ByVal yPointsValues22 As Double(), ByRef ListView1 As Windows.Forms.ListView)

        Dim oListViewItem As ListViewItem
        For i = 0 To NumOfPoints - 1
            oListViewItem = ListView1.Items.Add(i + 1)
            oListViewItem.SubItems.Add(xPointsValues(i))
            oListViewItem.SubItems.Add(yPointsValues(i))
            oListViewItem.SubItems.Add(xPointsValues22(i))
            oListViewItem.SubItems.Add(yPointsValues22(i))
        Next

        oListViewItem = ListView1.Items.Add(NumOfPoints + 1)
        oListViewItem.SubItems.Add("-")
        oListViewItem.SubItems.Add("-")
        oListViewItem.SubItems.Add(xPointsValues22(NumOfPoints))
        oListViewItem.SubItems.Add(yPointsValues22(NumOfPoints))

    End Sub




    Public Sub FindMaxHoleDimensions(xPointsValues() As Double, yPointsValues() As Double, ByRef min_X As Double, ByRef max_X As Double, ByRef min_Y As Double, ByRef max_Y As Double, ByRef holeMikos As Double, ByRef holeYpsos As Double)


        max_X = xPointsValues(0)
        min_X = xPointsValues(0)
        For i = 0 To xPointsValues.Length - 1

            If xPointsValues(i) < min_X Then
                min_X = xPointsValues(i)
            ElseIf xPointsValues(i) > max_X Then
                max_X = xPointsValues(i)
            End If

        Next
        holeMikos = max_X - min_X


        max_Y = yPointsValues(0)
        min_Y = yPointsValues(0)
        For i = 0 To yPointsValues.Length - 1

            If yPointsValues(i) < min_Y Then
                min_Y = yPointsValues(i)
            ElseIf yPointsValues(i) > max_Y Then
                max_Y = yPointsValues(i)
            End If

        Next
        holeYpsos = max_Y - min_Y

    End Sub



    Public Sub ParametroiV1(xPointsValues() As Double, yPointsValues() As Double, D1 As Double, ByRef V_Ypsos1 As Double, ByRef V_YpsosTomhs As Double, ByRef Gwnia_Toksou As Double, ByRef ViewScale1 As Double)

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' ypologismoi gia nea Ypsos1 , YpsosTomhs , gwniaToksou
        ''''''' findMaxHoleDimensions gia na ro kanonikopoihsh stin klimaka 
        Dim holeMikos, holeYpsos, min_X, max_X, min_Y, max_Y As Double
        FindMaxHoleDimensions(xPointsValues, yPointsValues, min_X, max_X, min_Y, max_Y, holeMikos, holeYpsos)
        'MsgBox(holeMikos & "  " & holeYpsos)
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        ' Define the view scales that we need
        'Dim ViewScale1 As Double
        ViewScale1 = 70 / Max(holeMikos, holeYpsos)

        ''''''''' Gia ta V_Ypsos1, V_YpsosTomhs
        Dim holeMikosStoSheet As Double = holeMikos / 10 * ViewScale1
        Dim holeYpsosStoSheet As Double = holeYpsos / 10 * ViewScale1
        Dim X_StoSheet As Double = (10 - holeYpsosStoSheet) / 2
        Dim X_Sto3D As Double = X_StoSheet * 10 / ViewScale1
        V_Ypsos1 = holeYpsos + 2 * X_Sto3D
        V_YpsosTomhs = V_Ypsos1 / 2


        ''''''''''''''' Gia thn gwnia tou toksou
        Dim X_Mikous_StoSheet As Double = 5 - holeMikosStoSheet
        Dim X_Mikous_Sto3D As Double = X_Mikous_StoSheet * 10 / ViewScale1
        Dim ToksoSto3D As Double = holeMikos + X_Mikous_Sto3D
        'MsgBox(ToksoSto3D)
        Gwnia_Toksou = 360 * ToksoSto3D / (Math.PI * D1)
        'MsgBox(Gwnia_Toksou)


    End Sub






    Public Sub KovwSeGrammes21_IsaDiam(oView As DrawingView, myApplication As Inventor.Application, ViewScale1 As Double, NumOfPoints As Integer)
        Dim myCurve1 As DrawingCurve
        Dim myCurve2 As DrawingCurve


        myCurve1 = oView.DrawingCurves.Item(5)
        myCurve2 = oView.DrawingCurves.Item(6)


        Dim oCurveEval1 As Curve2dEvaluator
        Dim dMinParam1 As Double
        Dim dMaxParam1 As Double
        Dim oCurveEval2 As Curve2dEvaluator
        Dim dMinParam2 As Double
        Dim dMaxParam2 As Double
        'pairnw to evaluator
        oCurveEval1 = myCurve1.Evaluator2D
        ' Get the parametric range of the curve.
        Call oCurveEval1.GetParamExtents(dMinParam1, dMaxParam1)
        'pairnw to evaluator
        oCurveEval2 = myCurve2.Evaluator2D
        ' Get the parametric range of the curve.
        Call oCurveEval2.GetParamExtents(dMinParam2, dMaxParam2)


        Dim oTG As TransientGeometry
        oTG = myApplication.TransientGeometry

        Dim currentParam1 As Double
        Dim adParam1(0) As Double
        Dim adPoints1(2) As Double

        Dim currentParam2 As Double
        Dim adParam2(0) As Double
        Dim adPoints2(2) As Double

        Dim oViewSketch As DrawingSketch = oView.Sketches.Add  '''''''''''''''''''''' Edw anoigw to sketch ''''''''''''''''''''''
        oViewSketch.Edit()

        '' prwth grammh kai monadikh orizontia''''''''''''''''''''''''''
        adParam1(0) = dMinParam1
        Call oCurveEval1.GetPointAtParam(adParam1, adPoints1)
        Dim x11 As Double = adPoints1(0) / ViewScale1
        Dim y11 As Double = adPoints1(1) / ViewScale1
        adParam2(0) = dMinParam2
        Call oCurveEval2.GetPointAtParam(adParam2, adPoints2)
        Dim x22 As Double = adPoints2(0) / ViewScale1
        Dim y22 As Double = adPoints2(1) / ViewScale1
        Try
            oViewSketch.SketchLines.AddByTwoPoints(oTG.CreatePoint2d(x11, y11), oTG.CreatePoint2d(x22, y22))
        Catch
            'MsgBox("skata")
        End Try  '''''''''''''''''''''''''''''''''''''''''''''''''''''''


        For i = 0 To (NumOfPoints / 2)


            ' Calculate the current parameter to evaluate.
            currentParam1 = dMinParam1 + ((dMaxParam1 - dMinParam1) / (NumOfPoints / 2)) * i
            ' Assign the value to an array since the GetPointAtParam
            ' takes an array as input.
            adParam1(0) = currentParam1
            ' Get the coordinates of the parameter point in model space.
            Call oCurveEval1.GetPointAtParam(adParam1, adPoints1)


            ' Calculate the current parameter to evaluate.
            currentParam2 = dMaxParam2 - ((dMaxParam2 - dMinParam2) / (NumOfPoints / 2)) * i
            ' Assign the value to an array since the GetPointAtParam
            ' takes an array as input.
            adParam2(0) = currentParam2
            ' Get the coordinates of the parameter point in model space.
            Call oCurveEval2.GetPointAtParam(adParam2, adPoints2)


            Dim x1 As Double = adPoints1(0) / ViewScale1
            Dim y1 As Double = adPoints1(1) / ViewScale1
            Dim x2 As Double = adPoints2(0) / ViewScale1
            Dim y2 As Double = adPoints2(1) / ViewScale1

            'MsgBox(x1 & "  " & y1 & "  " & x2 & "  " & y2 & "  ")

            If i <> 0 And i <> NumOfPoints / 2 Then
                Try
                    oViewSketch.SketchLines.AddByTwoPoints(oTG.CreatePoint2d(x1, y1), oTG.CreatePoint2d(x2, y2))
                Catch
                    'MsgBox("skata")

                End Try
            End If

        Next

        oViewSketch.ExitEdit()      '''''''''''''''''''' Edw kleinw to sketch ''''''''''''''''''''''''
        'MsgBox(oViewSketch.SketchLines.Count)



    End Sub







    Public Sub KovwSeGrammes21_DiaforetikaDiam(oView As DrawingView, myApplication As Inventor.Application, ViewScale1 As Double, NumOfPoints As Integer)

        ' pianw to curve ths kampilhs
        Dim myCurve1 As DrawingCurve
        myCurve1 = oView.DrawingCurves.Item(5)


        Dim oCurveEval1 As Curve2dEvaluator
        Dim dMinParam1 As Double
        Dim dMaxParam1 As Double

        'pairnw to evaluator ths kampilhs
        oCurveEval1 = myCurve1.Evaluator2D
        ' Get the parametric range of the curve.
        Call oCurveEval1.GetParamExtents(dMinParam1, dMaxParam1)



        Dim oTG As TransientGeometry
        oTG = myApplication.TransientGeometry

        Dim currentParam1 As Double
        Dim adParam1(0) As Double
        Dim adPoints1(2) As Double

        Dim currentParam2 As Double
        Dim adParam2(0) As Double
        Dim adPoints2(2) As Double

        Dim oViewSketch As DrawingSketch = oView.Sketches.Add  '''''''''''''''''''''' Edw anoigw to sketch ''''''''''''''''''''''
        oViewSketch.Edit()

        '' prwth grammh kai monadikh orizontia''''''''''''''''''''''''''
        'parametros prwtou simeioy
        adParam1(0) = dMinParam1
        Call oCurveEval1.GetPointAtParam(adParam1, adPoints1)
        Dim x11 As Double = adPoints1(0) / ViewScale1
        Dim y11 As Double = adPoints1(1) / ViewScale1
        'parametros mesou simeiou
        adParam2(0) = dMinParam1 + (dMaxParam1 - dMinParam1) / 2
        Call oCurveEval1.GetPointAtParam(adParam2, adPoints2)
        Dim x22 As Double = adPoints2(0) / ViewScale1
        Dim y22 As Double = adPoints2(1) / ViewScale1
        Try
            oViewSketch.SketchLines.AddByTwoPoints(oTG.CreatePoint2d(x11, y11), oTG.CreatePoint2d(x22, y22))
        Catch
            'MsgBox("skata")
        End Try  '''''''''''''''''''''''''''''''''''''''''''''''''''''''

         
        For i = 0 To NumOfPoints


            ' Calculate the current parameter to evaluate.
            currentParam1 = dMinParam1 + ((dMaxParam1 - dMinParam1) / NumOfPoints) * i
            ' Assign the value to an array since the GetPointAtParam
            ' takes an array as input.
            adParam1(0) = currentParam1
            ' Get the coordinates of the parameter point in model space.
            Call oCurveEval1.GetPointAtParam(adParam1, adPoints1)


            ' Calculate the current parameter to evaluate.
            currentParam2 = dMaxParam1 - ((dMaxParam1 - dMinParam1) / NumOfPoints) * i
            ' Assign the value to an array since the GetPointAtParam
            ' takes an array as input.
            adParam2(0) = currentParam2
            ' Get the coordinates of the parameter point in model space.
            Call oCurveEval1.GetPointAtParam(adParam2, adPoints2)


            Dim x1 As Double = adPoints1(0) / ViewScale1
            Dim y1 As Double = adPoints1(1) / ViewScale1
            Dim x2 As Double = adPoints2(0) / ViewScale1
            Dim y2 As Double = adPoints2(1) / ViewScale1

            'MsgBox(x1 & "  " & y1 & "  " & x2 & "  " & y2 & "  ")

            If i <> 0 And i <> NumOfPoints Then
                Try
                    oViewSketch.SketchLines.AddByTwoPoints(oTG.CreatePoint2d(x1, y1), oTG.CreatePoint2d(x2, y2))
                Catch
                    'MsgBox("skata")

                End Try
            End If

        Next

        oViewSketch.ExitEdit()      '''''''''''''''''''' Edw kleinw to sketch ''''''''''''''''''''''''
        'MsgBox(oViewSketch.SketchLines.Count)



    End Sub






    Public Sub Kouti_kai_invisibility(myApplication As Application, xP As Double, yP As Double, D1 As Double, D2 As Double, Ypsos1 As Double, oView As DrawingView, oSheet As Sheet)

        Dim oTG As TransientGeometry
        oTG = myApplication.TransientGeometry


        Dim oViewSketch As DrawingSketch = oSheet.Sketches.Add
        oViewSketch.Edit()

        Dim x1 As Double = xP - 5
        Dim y1 As Double = yP + 5
        Dim x2 As Double = xP + 5
        Dim y2 As Double = yP + 5
        Dim x3 As Double = xP + 5
        Dim y3 As Double = yP - 5
        Dim x4 As Double = xP - 5
        Dim y4 As Double = yP - 5

        Try

            oViewSketch.SketchLines.AddByTwoPoints(oTG.CreatePoint2d(x1, y1), oTG.CreatePoint2d(x2, y2))
            oViewSketch.SketchLines.AddByTwoPoints(oTG.CreatePoint2d(x2, y2), oTG.CreatePoint2d(x3, y3))
            oViewSketch.SketchLines.AddByTwoPoints(oTG.CreatePoint2d(x3, y3), oTG.CreatePoint2d(x4, y4))
            oViewSketch.SketchLines.AddByTwoPoints(oTG.CreatePoint2d(x4, y4), oTG.CreatePoint2d(x1, y1))

            oViewSketch.SketchLines(1).LineWeight = 0.05
            oViewSketch.SketchLines(2).LineWeight = 0.05
            oViewSketch.SketchLines(3).LineWeight = 0.05
            oViewSketch.SketchLines(4).LineWeight = 0.05

        Catch
            'MsgBox("skata")
        End Try

        oViewSketch.ExitEdit()




        For i = 1 To oView.DrawingCurves.Count

            If D1 = D2 And i <> 5 And i <> 6 Then

                For j = 1 To oView.DrawingCurves.Item(i).Segments.Count

                    oView.DrawingCurves.Item(i).Segments.Item(j).Visible = False

                Next

            ElseIf D1 <> D2 And i <> 5 Then

                For j = 1 To oView.DrawingCurves.Item(i).Segments.Count

                    oView.DrawingCurves.Item(i).Segments.Item(j).Visible = False

                Next

            End If

        Next


        Dim sText As String
        sText = Format(D1 * Math.PI, "0.00") & " mm  x  " & Format(Ypsos1, "0.00") & " mm"

        Dim oTitlePoint1 As Point2d
        Dim oGeneralNoteTitles As GeneralNote
        oTitlePoint1 = myApplication.TransientGeometry.CreatePoint2d(xP + 4.4 - 0.16 * sText.Length, yP + 4.75)
        oGeneralNoteTitles = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTitlePoint1, sText)
        oGeneralNoteTitles.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & sText & "</StyleOverride>"

    End Sub







    Public Sub EpiloghArithmwn_21(myApplication As Application, ByVal NumOfPoints As Integer, ByVal ViewScale1 As Double, xP As Double, yP As Double, ByVal xPoints() As Double, ByVal yPoints() As Double, ByRef oSheet As Sheet, oLength As Double, oWidth As Double)


        Dim oTG As TransientGeometry
        oTG = myApplication.TransientGeometry


        Dim sText As String
        Dim oGeneralNote As GeneralNote
        Dim xDiff As Double = 0
        Dim yDiff As Double = 0



        Dim Center As Double
        Center = NumOfPoints / 4



        For i = 0 To NumOfPoints / 2


            sText = i + 1

            If i = 0 Then
                xDiff = -0.22
                yDiff = 0.14
            ElseIf i = NumOfPoints / 2 Then
                xDiff = 0.05
                yDiff = 0.14
            ElseIf i = Center Then
                xDiff = -sText.Length * 0.11
                yDiff = 0.35
            ElseIf i < Center Then
                xDiff = -sText.Length * 0.16
                yDiff = 0.35
            ElseIf i > Center Then
                xDiff = -sText.Length * 0.04
                yDiff = 0.35
            End If


            If (NumOfPoints < 32) Or ((NumOfPoints = 32 Or NumOfPoints = 40 Or NumOfPoints = 48) And i Mod 2 = 0) Or (NumOfPoints = 36 And i Mod 3 = 0) Or (NumOfPoints = 44 And (i = 0 Or i = 4 Or i = 7 Or i = 11 Or i = 15 Or i = 18 Or i = 22)) Then 'isws thelei kai i=2,9,13,20

                oGeneralNote = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTG.CreatePoint2d(((xPoints(i) / 10) - (oLength / 2)) * ViewScale1 + xP + xDiff, ((yPoints(i) / 10) - (oWidth / 2)) * ViewScale1 + yP + yDiff), sText)

                If i <> 0 And i <> NumOfPoints / 2 Then

                    sText = NumOfPoints - i + 1

                    If i < Center Then
                        xDiff = -sText.Length * 0.16
                    ElseIf i = Center Then
                        xDiff = -sText.Length * 0.11
                    End If

                    oGeneralNote = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTG.CreatePoint2d(((xPoints(NumOfPoints - i) / 10) - (oLength / 2)) * ViewScale1 + xP + xDiff, ((yPoints(NumOfPoints - i) / 10) - (oWidth / 2)) * ViewScale1 + yP - 0.08), sText)

                End If

            End If

        Next




    End Sub









    Public Sub EpiloghArithmwn_22(myApplication As Application, ByVal NumOfPoints As Integer, ByVal ViewScale1 As Double, xP As Double, yP As Double, ByVal xPoints() As Double, ByVal yPoints() As Double, ByRef oSheet As Sheet, oLength As Double, oWidth As Double)


        Dim oTG As TransientGeometry
        oTG = myApplication.TransientGeometry


        Dim sText As String
        Dim oGeneralNote As GeneralNote
        Dim xDiff As Double = 0
        Dim yDiff As Double = 0



        Dim Center As Double
        Center = NumOfPoints / 4



        For i = 0 To NumOfPoints / 2


            sText = i + 1

            If i = 0 Then
                xDiff = -0.2
                yDiff = 0.3
            ElseIf i = NumOfPoints / 2 Then
                xDiff = -sText.Length * 0.09
                yDiff = 0.35
            ElseIf i = Center Then
                xDiff = -sText.Length * 0.11
                yDiff = 0.35
            ElseIf i < Center Then
                xDiff = -sText.Length * 0.15
                yDiff = 0.35
            ElseIf i > Center Then
                xDiff = -0.06
                yDiff = 0.35
            End If




            Dim b20, b24, b28, b32, b36, b40, b44, b48 As Boolean
            b20 = NumOfPoints = 20 And (i = 0 Or i = 3 Or i = 5 Or i = 7 Or i = 10)
            b24 = NumOfPoints = 24 And (i = 0 Or i = 2 Or i = 4 Or i = 6 Or i = 8 Or i = 10 Or i = 12)
            b28 = NumOfPoints = 28 And (i = 0 Or i = 4 Or i = 7 Or i = 10 Or i = 14)
            b32 = NumOfPoints = 32 And (i = 0 Or i = 3 Or i = 5 Or i = 8 Or i = 11 Or i = 13 Or i = 16)
            b36 = NumOfPoints = 36 And (i = 0 Or i = 3 Or i = 6 Or i = 9 Or i = 12 Or i = 15 Or i = 18)
            b40 = NumOfPoints = 40 And (i = 0 Or i = 4 Or i = 7 Or i = 10 Or i = 13 Or i = 16 Or i = 20)
            b44 = NumOfPoints = 44 And (i = 0 Or i = 4 Or i = 7 Or i = 11 Or i = 15 Or i = 18 Or i = 22)
            b48 = NumOfPoints = 48 And (i = 0 Or i = 4 Or i = 8 Or i = 12 Or i = 16 Or i = 20 Or i = 24)

            If (NumOfPoints < 20 Or b20 Or b24 Or b28 Or b32 Or b36 Or b40 Or b44 Or b48) Then

                oGeneralNote = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTG.CreatePoint2d(((xPoints(i) / 10) - (oLength / 2)) * ViewScale1 + xP + xDiff, ((yPoints(i) / 10) - (oWidth / 2)) * ViewScale1 + yP + yDiff), sText)

                If i <> 0 Then

                    sText = NumOfPoints / 2 + i + 1

                    If i = Center Then
                        xDiff = -sText.Length * 0.11
                        yDiff = 0.35
                    ElseIf i < Center Then
                        xDiff = -sText.Length * 0.15
                        yDiff = 0.35
                    ElseIf i > Center Then
                        xDiff = -0.06
                        yDiff = 0.35
                    ElseIf i = NumOfPoints / 2 Then
                        xDiff += 0.14
                        yDiff = 0.3
                    End If


                    oGeneralNote = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTG.CreatePoint2d(((xPoints(NumOfPoints / 2 + i) / 10) - (oLength / 2)) * ViewScale1 + xP + xDiff, ((yPoints(NumOfPoints / 2 + i) / 10) - (oWidth / 2)) * ViewScale1 + yP + yDiff), sText)

                End If

            End If

        Next




    End Sub














    Public Sub Ypomnima2(ByVal xPos As Double, ByVal ViewScale1 As Double, ByVal ViewScale2 As Double, ByVal Default_Micro_Path As String, ByVal oTg As TransientGeometry, ByRef oSketchYpomnima As DrawingSketch)

        ' Use the functionality of the sketch to add title block graphics.
        oSketchYpomnima.SketchLines.AddAsTwoPointRectangle(oTg.CreatePoint2d(xPos + 0, 0), oTg.CreatePoint2d(xPos + 18, 3.6))
        oSketchYpomnima.SketchLines.AddByTwoPoints(oTg.CreatePoint2d(xPos + 7.6, 0), oTg.CreatePoint2d(xPos + 7.6, 3.6))
        oSketchYpomnima.SketchLines.AddByTwoPoints(oTg.CreatePoint2d(xPos + 7.6, 1.8), oTg.CreatePoint2d(xPos + 18, 1.8))
        oSketchYpomnima.SketchLines.AddByTwoPoints(oTg.CreatePoint2d(xPos + 15.5, 1.8), oTg.CreatePoint2d(xPos + 15.5, 3.6))
        oSketchYpomnima.SketchLines.AddByTwoPoints(oTg.CreatePoint2d(xPos + 13.88, 0), oTg.CreatePoint2d(xPos + 13.88, 1.8))
        oSketchYpomnima.SketchLines.AddByTwoPoints(oTg.CreatePoint2d(xPos + 13.88, 0.9), oTg.CreatePoint2d(xPos + 18, 0.9))

        '' Override the line weight of the second line.
        oSketchYpomnima.SketchLines(1).LineWeight = 0.05
        oSketchYpomnima.SketchLines(2).LineWeight = 0.05
        oSketchYpomnima.SketchLines(3).LineWeight = 0.05
        oSketchYpomnima.SketchLines(4).LineWeight = 0.05
        oSketchYpomnima.SketchLines(5).LineWeight = 0.025
        oSketchYpomnima.SketchLines(6).LineWeight = 0.025
        oSketchYpomnima.SketchLines(7).LineWeight = 0.025
        oSketchYpomnima.SketchLines(8).LineWeight = 0.025
        oSketchYpomnima.SketchLines(9).LineWeight = 0.025


        'Dim LogoFilename As String = "C:\Users\antma\Desktop\InterfaceAplo\Interface1\micro.png"
        'add embedded image
        Dim oSketchImage As SketchImage
        oSketchImage = oSketchYpomnima.SketchImages.Add(Default_Micro_Path, oTg.CreatePoint2d(xPos + 0.1, 3.5), False)
        'set image size in cm
        oSketchImage.Height = 3.4
        oSketchImage.Width = 7.4

        Dim sText As String
        sText = "Τίτλος:"
        Dim oTextBox As TextBox
        oTextBox = oSketchYpomnima.TextBoxes.AddFitted(oTg.CreatePoint2d(xPos + 7.73, 3.2), sText)
        oTextBox.VerticalJustification = VerticalTextAlignmentEnum.kAlignTextUpper
        oTextBox.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextLeft
        oTextBox.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & sText & "</StyleOverride>"
        sText = "Τομή Κυλίνδρου Με Κάθετο Κύλινδρο"
        oTextBox = oSketchYpomnima.TextBoxes.AddFitted(oTg.CreatePoint2d(xPos + 7.73, 2.6), sText)
        oTextBox.VerticalJustification = VerticalTextAlignmentEnum.kAlignTextUpper
        oTextBox.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextLeft
        oTextBox.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & sText & "</StyleOverride>"


        sText = "Προγραμματισμός - Σχεδίαση:"
        oTextBox = oSketchYpomnima.TextBoxes.AddFitted(oTg.CreatePoint2d(xPos + 7.73, 1.4), sText)
        oTextBox.VerticalJustification = VerticalTextAlignmentEnum.kAlignTextUpper
        oTextBox.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextLeft
        oTextBox.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & sText & "</StyleOverride>"
        sText = "Μαραγκουδάκης Αντώνιος - Εμμανουήλ"
        oTextBox = oSketchYpomnima.TextBoxes.AddFitted(oTg.CreatePoint2d(xPos + 7.73, 0.8), sText)
        oTextBox.VerticalJustification = VerticalTextAlignmentEnum.kAlignTextUpper
        oTextBox.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextLeft
        oTextBox.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & sText & "</StyleOverride>"


        Dim oDate As Date = Date.Now
        sText = "Ημερομηνία:"
        oTextBox = oSketchYpomnima.TextBoxes.AddFitted(oTg.CreatePoint2d(xPos + 15.7, 3.2), sText)
        oTextBox.VerticalJustification = VerticalTextAlignmentEnum.kAlignTextUpper
        oTextBox.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextLeft
        oTextBox.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & sText & "</StyleOverride>"
        sText = oDate.ToString("dd MMM yyyy")
        oTextBox = oSketchYpomnima.TextBoxes.AddFitted(oTg.CreatePoint2d(xPos + 15.7, 2.575), sText)
        oTextBox.VerticalJustification = VerticalTextAlignmentEnum.kAlignTextUpper
        oTextBox.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextLeft
        oTextBox.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & sText & "</StyleOverride>"


        'View scale 
        sText = "Κλίμακα 1:  " & "  1 : " & Format(10 / ViewScale1, "0.00")
        oTextBox = oSketchYpomnima.TextBoxes.AddFitted(oTg.CreatePoint2d(xPos + 14.06, 1.515), sText)
        oTextBox.VerticalJustification = VerticalTextAlignmentEnum.kAlignTextUpper
        oTextBox.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextLeft
        oTextBox.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & sText & "</StyleOverride>"


        sText = "Κλίμακα 2:  " & "  1 : " & Format(10 / ViewScale2, "0.00")
        oTextBox = oSketchYpomnima.TextBoxes.AddFitted(oTg.CreatePoint2d(xPos + 14.06, 0.615), sText)
        oTextBox.VerticalJustification = VerticalTextAlignmentEnum.kAlignTextUpper
        oTextBox.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextLeft
        oTextBox.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & sText & "</StyleOverride>"

    End Sub





    Public Sub DWG_21(oSavePath1 As String, myApplication As Application, oSheet As Sheet, D1 As Double, D2 As Double, Ypsos1 As Double, NumOfPoints As Integer, ViewScale1 As Double, xPointsValues As Double(), yPointsValues As Double(), oLength As Double, oWidth As Double, maxSeires As Integer, xPoints As Double(), yPoints As Double())

        Dim oSaveName1 As String
        Dim sPartPath As String
        oSaveName1 = "test_V1" & ".ipt"
        sPartPath = oSavePath1 & oSaveName1

        Dim oPartDoc As PartDocument = myApplication.Documents.Open(sPartPath, True)
        'kanw kai unfold
        'oPartDoc = myApplication.ActiveDocument
        oPartDoc.SubType = "{9C464203-9BAE-11D3-8BAD-0060B0CE6BB4}"
        Dim partComp As ComponentDefinition
        partComp = oPartDoc.ComponentDefinition
        Dim TheFaceCounter As Integer
        UnfoldTheFace(myApplication, partComp, TheFaceCounter)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''' Flat Pattern View Anaptygmatos ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Create a new NameValueMap object
        Dim oBaseViewOptions As NameValueMap
        oBaseViewOptions = myApplication.TransientObjects.CreateNameValueMap
        ' Set the options to use when creating the base view.
        oBaseViewOptions.Add("SheetMetalFoldedModel", False)

        ' Now we define the placement points for
        ' the drawing views we shall be adding to the sheet   (SOS prepei panta na einai akairea kai prin ton ypologismo sto (0,0) )
        Dim oTitlePoint1 As Point2d
        Dim xP As Integer = 0
        Dim yP As Integer = 0

        Dim oPlacementPoint1 As Point2d
        oPlacementPoint1 = myApplication.TransientGeometry.CreatePoint2d(xP, yP)



        ''''''' findMaxHoleDimensions gia na ro kanonikopoihsh stin klimaka ''''''''''''''''''''''''''''''''''''''''''''''
        'Dim holeMikos, holeYpsos, min_X, max_X, min_Y, max_Y As Double
        'FindMaxHoleDimensions(xPointsValues, yPointsValues, min_X, max_X, min_Y, max_Y, holeMikos, holeYpsos)
        'MsgBox(holeMikos & "  " & holeYpsos)
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Define the view scales that we need
        'Dim ViewScale1 As Double
        'ViewScale1 = 70 / Max(holeMikos, holeYpsos)


        ' define the view orientation for each view
        Dim ViewOrientation1 As ViewOrientationTypeEnum = ViewOrientationTypeEnum.kDefaultViewOrientation

        ' define the view style for each view
        Dim ViewStyle1 As DrawingViewStyleEnum = DrawingViewStyleEnum.kHiddenLineRemovedDrawingViewStyle  'kShadedHiddenLineDrawingViewStyle
        ' now create our two views                                                                          
        Dim oView1 As DrawingView
        oView1 = oSheet.DrawingViews.AddBaseView(oPartDoc, oPlacementPoint1, ViewScale1, ViewOrientation1, ViewStyle1, , , oBaseViewOptions)



        Dim oGeneralNoteTitles As GeneralNote
        oTitlePoint1 = myApplication.TransientGeometry.CreatePoint2d(7 - 3.44, 21 + 6.1)
        oGeneralNoteTitles = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTitlePoint1, "ΑΝΑΠΤΥΓΜΑ ΚΑΤΑΚΟΡΥΦΟΥ ΚΥΛΙΝΔΡΟΥ (1)")
        oGeneralNoteTitles.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & "ΑΝΑΠΤΥΓΜΑ ΚΑΤΑΚΟΡΥΦΟΥ ΚΥΛΙΝΔΡΟΥ (1)" & "</StyleOverride>"







        xP = 7
        yP = 21
        Kouti_kai_invisibility(myApplication, xP, yP, D1, D2, Ypsos1, oView1, oSheet)


        If D1 = D2 Then


            ''''''''''''''' kanw unvisible to endiameso  
            ''epidi mporei an exei ginei cut h gia allo logo na exw polla segments. Tote xriazete For Each oCurveSeg In oDrawingCurve.Segments
            oView1.DrawingCurves.Item(7).Segments.Item(1).Visible = False


            '''''''''''''''' Kovw se Grammes prwta apo to break gia na mhn xasw th swsth thesh twn drawingCurves
            KovwSeGrammes21_IsaDiam(oView1, myApplication, ViewScale1, NumOfPoints)


        Else

            ''''''''''''''' kanw unvisible to endiameso  
            oView1.DrawingCurves.Item(6).Segments.Item(1).Visible = False


            '''''''''''''''' Kovw se Grammes prwta apo to break gia na mhn xasw th swsth thesh twn drawingCurves
            KovwSeGrammes21_DiaforetikaDiam(oView1, myApplication, ViewScale1, NumOfPoints)

        End If




        ''''''''''''''' Katskeyh Systima Aksonwn 
        Dim oTG As TransientGeometry
        oTG = myApplication.TransientGeometry

        Dim xAxis, yAxis As Double
        xAxis = 0
        yAxis = 0

        AxisSystem(myApplication, oView1, oTG, xAxis, yAxis, ViewScale1)


        '''''''''''''''' PROSDIORISMOS NEOU CENTER kai epilogh arithmwn
        xP = 7
        yP = 21
        Dim oViewPos As Point2d = myApplication.TransientGeometry.CreatePoint2d(xP, yP)
        oView1.Position = oViewPos

        EpiloghArithmwn_21(myApplication, NumOfPoints, ViewScale1, xP, yP, xPointsValues, yPointsValues, oSheet, oLength, oWidth)



        ''''''''''''''''''''''''''''''''' Table 

        Dim xTablePosition As Double
        xTablePosition = xP - 5
        Dim yTablePosition As Double
        yTablePosition = yP - 6.5 - 1
        Dim xTableSpace As Double = 5.2    'se cm einai 4.8cm o prwtos kai o deyteros tha mpei praktika sto +0.2 cm 

        Table_Construction(myApplication, oSheet, maxSeires, NumOfPoints, xPoints, yPoints, xTablePosition, yTablePosition, xTableSpace)


        oPartDoc.Close(True)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



        '''''''''''''''''''''''''''''''''''' 3D Model View 1
        oSaveName1 = "test_V1_3D" & ".ipt"
        sPartPath = oSavePath1 & oSaveName1
        Dim oPartDoc_3D1 As PartDocument = myApplication.Documents.Open(sPartPath, True)


        ' Create a new NameValueMap object
        Dim oBaseViewOptions2 As NameValueMap
        oBaseViewOptions2 = myApplication.TransientObjects.CreateNameValueMap
        ' Set the options to use when creating the base view.
        oBaseViewOptions2.Add("IncludeSurfaceBodies", False)

        ' Now we define the placement points for
        'the two drawing views we shall be adding to the sheet
        Dim xP_3D As Integer = 16
        Dim yP_3D As Integer = 12
        Dim oPlacementPoint2 As Point2d
        oPlacementPoint2 = myApplication.TransientGeometry.CreatePoint2d(xP_3D, yP_3D)


        Dim ViewScale3D_1 As Double
        ViewScale3D_1 = 32 / D1

        ' define the view orientation for each view
        Dim ViewOrientation2 As ViewOrientationTypeEnum = ViewOrientationTypeEnum.kIsoTopLeftViewOrientation

        ' define the view style for each view
        Dim ViewStyle2 As DrawingViewStyleEnum = DrawingViewStyleEnum.kShadedDrawingViewStyle   ''kShadedHiddenLineDrawingViewStyle

        ' now create our two views
        Dim oView2 As DrawingView
        oView2 = oSheet.DrawingViews.AddBaseView(oPartDoc_3D1, oPlacementPoint2, ViewScale3D_1, ViewOrientation2, ViewStyle2, , , oBaseViewOptions2)



        Dim oTitlePoint2 As Point2d
        oTitlePoint2 = myApplication.TransientGeometry.CreatePoint2d(xP_3D - 2.27, yP_3D + 5.58)
        oGeneralNoteTitles = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTitlePoint2, "3D ΜΟΝΤΕΛΟ ΚΑΤΑΚΟΡΥΦΟΥ")
        oGeneralNoteTitles.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextCenter
        oGeneralNoteTitles.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & "3D ΜΟΝΤΕΛΟ ΚΑΤΑΚΟΡΥΦΟΥ" & "</StyleOverride>"

        '''''''''''''''''''''''''''' 3d model Ypsos Dimension
        Dim oGeneralDims As GeneralDimensions = oSheet.DrawingDimensions.GeneralDimensions


        Dim kath As Integer = 14
        Dim oriz As Integer = 9
        If oView2.DrawingCurves.Count <> 14 Then
            kath = 12
        End If


        ''''Dimension gia to katheto
        Dim finalCurve3 As DrawingCurve
        finalCurve3 = oView2.DrawingCurves.Item(kath)


        Dim oGeomIntent3 As GeometryIntent
        oGeomIntent3 = oSheet.CreateGeometryIntent(finalCurve3)


        Dim textPoint3 As Inventor.Point2d = myApplication.TransientGeometry.CreatePoint2d(oView2.DrawingCurves.Item(kath).MidPoint.X + 0.8, oView2.DrawingCurves.Item(kath).MidPoint.Y + Math.Tan(Math.PI / 6) * 0.8)
        Dim myDim3DKatheto As GeneralDimension
        myDim3DKatheto = oGeneralDims.AddLinear(textPoint3, oGeomIntent3)

        'kryvw to lathos value kai dinw to swsto
        myDim3DKatheto.HideValue = True
        myDim3DKatheto.Text.FormattedText = Format(Ypsos1, "0.00") & " mm"




        ''''''Dimension gia thn diametro tou 3D object
        Dim finalCurve4 As DrawingCurve
        finalCurve4 = oView2.DrawingCurves.Item(oriz)  ' gia thn diametro 

        Dim oGeomIntent4 As GeometryIntent
        oGeomIntent4 = oSheet.CreateGeometryIntent(finalCurve4)

        '' To curve  oView2.DrawingCurves.Item(2)  einai to xYpsosTomhs san curve object
        Dim textPoint4 As Point2d = myApplication.TransientGeometry.CreatePoint2d(xP_3D, oView2.DrawingCurves.Item(kath).StartPoint.Y - 1.8)    ''''  ,yP2 - 5)
        Dim myDim3DOrizontio As GeneralDimension
        myDim3DOrizontio = oGeneralDims.AddDiameter(textPoint4, oGeomIntent4)
        myDim3DOrizontio.Text.FormattedText = "<DimensionValue/>" & " mm"

        oPartDoc_3D1.Close(True)

    End Sub












    Public Sub DWG_22(oSavePath1 As String, myApplication As Application, V2_Konto As Boolean, V2_Psilo As Boolean, oSheet As Sheet, oLength2 As Double, oWidth2 As Double, NumOfPoints As Integer, D1 As Double, D2 As Double, xPointsValues22 As Double(), yPointsValues22 As Double(), maxSeires As Integer, xPoints22 As Double(), yPoints22 As Double())



        Dim oSaveName1 As String
        Dim sPartPath As String
        If Not (V2_Konto Or V2_Psilo) Then
            oSaveName1 = "test_R2" & ".ipt"
            sPartPath = oSavePath1 & oSaveName1
        Else
            oSaveName1 = "test_V2" & ".ipt"
            sPartPath = oSavePath1 & oSaveName1
        End If
        Dim oPartDoc As PartDocument = myApplication.Documents.Open(sPartPath, True)

        Dim partComp As ComponentDefinition
        partComp = oPartDoc.ComponentDefinition
        partComp.Unfold()
        partComp.FlatPattern.Edit()





        'Create a New NameValueMap object
        Dim oBaseViewOptions As NameValueMap
        oBaseViewOptions = myApplication.TransientObjects.CreateNameValueMap
        'Set the options to use when creating the base view.
        oBaseViewOptions.Add("SheetMetalFoldedModel", False)

        'Now we define the placement points for
        'the Drawing views we shall be adding To the sheet   (SOS prepei panta na einai akairea kai prin ton ypologismo sto (0, 0) )
        Dim oTitlePoint1 As Point2d
        Dim xP As Integer = 28
        Dim yP As Integer = 22

        Dim oPlacementPoint1 As Point2d
        oPlacementPoint1 = myApplication.TransientGeometry.CreatePoint2d(xP, yP)


        'Define the view scales that we need
        Dim ViewScale2 As Double
        ViewScale2 = 10 / oLength2


        'define the view orientation for each view
        Dim ViewOrientation1 As ViewOrientationTypeEnum = ViewOrientationTypeEnum.kDefaultViewOrientation

        'define the view style for each view
        Dim ViewStyle1 As DrawingViewStyleEnum = DrawingViewStyleEnum.kShadedHiddenLineDrawingViewStyle  'kHiddenLineRemovedDrawingViewStyle
        'Now create our two views                                                                          
        Dim oView1 As DrawingView
        oView1 = oSheet.DrawingViews.AddBaseView(oPartDoc, oPlacementPoint1, ViewScale2, ViewOrientation1, ViewStyle1, , , oBaseViewOptions)



        Dim oGeneralNoteTitles As GeneralNote
        oTitlePoint1 = myApplication.TransientGeometry.CreatePoint2d(xP - 3.22, yP + oWidth2 * ViewScale2 / 2 + 1.1)
        oGeneralNoteTitles = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTitlePoint1, "ΑΝΑΠΤΥΓΜΑ ΟΡΙΖΟΝΤΙΟΥ ΚΥΛΙΝΔΡΟΥ (2)")
        oGeneralNoteTitles.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & "ΑΝΑΠΤΥΓΜΑ ΟΡΙΖΟΝΤΙΟΥ ΚΥΛΙΝΔΡΟΥ (2)" & "</StyleOverride>"



        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' Katskeyh Systima Aksonwn 
        Dim oTG As TransientGeometry
        oTG = myApplication.TransientGeometry

        Dim xAxis, yAxis As Double
        xAxis = -(oLength2 / 2)
        yAxis = -(oWidth2 / 2)

        AxisSystem(myApplication, oView1, oTG, xAxis, yAxis, ViewScale2)



        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' orizontio katheto dimension 
        Dim oriz, kath As Integer

        If D1 = D2 Then
            oriz = 3
            kath = 2
        Else
            oriz = 5
            kath = 2
        End If

        Dim finalCurve1, finalCurve2 As DrawingCurve
        finalCurve1 = oView1.DrawingCurves.Item(kath)      ' gia to katheto
        finalCurve2 = oView1.DrawingCurves.Item(oriz)      ' gia to orizontio

        Dim oGeomIntent1 As GeometryIntent
        Dim oGeomIntent2 As GeometryIntent

        oGeomIntent1 = oSheet.CreateGeometryIntent(finalCurve1)   ' gia to katheto
        oGeomIntent2 = oSheet.CreateGeometryIntent(finalCurve2)   ' gia to orizontio


        Dim oGeneralDims As GeneralDimensions = oSheet.DrawingDimensions.GeneralDimensions
        'Xtizw to Dimension tou orizontiou
        Dim textPoint2 As Inventor.Point2d = myApplication.TransientGeometry.CreatePoint2d(finalCurve2.MidPoint.X, finalCurve2.MidPoint.Y - 0.55)
        Dim myDimOrizontio As GeneralDimension
        myDimOrizontio = oGeneralDims.AddLinear(textPoint2, oGeomIntent2)
        myDimOrizontio.Text.FormattedText = "<DimensionValue/>" & " mm"

        'Xtizw to Dimension tou kathetou
        Dim textPoint1 As Inventor.Point2d = myApplication.TransientGeometry.CreatePoint2d(finalCurve1.MidPoint.X + 0.7, finalCurve1.MidPoint.Y) 'oView.Center.X + 5.5
        Dim myDimKatheto As GeneralDimension
        myDimKatheto = oGeneralDims.AddLinear(textPoint1, oGeomIntent1)

        'An den einai poiotiko prosthetw apla to mm
        If Not (V2_Konto Or V2_Psilo) Then
            myDimKatheto.Text.FormattedText = "<DimensionValue/>" & " mm"
        Else
            'alliws kryvw to lathos value kai dinw to swsto
            myDimKatheto.HideValue = True
            myDimKatheto.Text.FormattedText = Format(yPoints22(NumOfPoints), "0.00") & " mm"
        End If
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' break operation
        Dim maxYpsosCm As Double = yPoints22(NumOfPoints / 4) / 10
        Dim cutGap As Double
        cutGap = 0.3

        If V2_Konto Or V2_Psilo Then

            Dim myCurve1 As DrawingCurve
            myCurve1 = oView1.DrawingCurves.Item(1)
            Dim mesoYstoSheet As Double = myCurve1.MidPoint.Y

            Dim diaforaStoViwe As Double = maxYpsosCm - oLength2
            Dim diaforaStoSheet As Double = diaforaStoViwe * ViewScale2
            Dim araKovoume As Double = diaforaStoSheet + cutGap

            ''''' Cut
            'kai pairnw kata cutGap giro apo to UpCutMesoStoSheet
            Dim oStartPoint As Point2d
            oStartPoint = myApplication.TransientGeometry.CreatePoint2d(0, mesoYstoSheet - cutGap / 2)

            'Define the end point of the break
            Dim oEndPoint As Point2d
            oEndPoint = myApplication.TransientGeometry.CreatePoint2d(0, mesoYstoSheet + cutGap / 2)

            Dim oBreakOperation As BreakOperation
            oBreakOperation = oView1.BreakOperations.Add(BreakOrientationEnum.kVerticalBreakOrientation, oStartPoint, oEndPoint, BreakStyleEnum.kStructuralBreakStyle, 8, (cutGap - 0.0001), 1, False)


        End If



        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' Kovw se grammes to View tou Flat Pattern 
        'Dim oTG As TransientGeometry
        'oTG = myApplication.TransientGeometry

        Dim oViewSketch As DrawingSketch = oView1.Sketches.Add
        oViewSketch.Edit()

        For i = 0 To NumOfPoints

            If Not (V2_Konto Or V2_Psilo) Then  '' and not konto

                Try
                    oViewSketch.SketchLines.AddByTwoPoints(oTG.CreatePoint2d((xPointsValues22(i) / 10) - (oLength2 / 2), -(oWidth2 / 2)), oTG.CreatePoint2d((xPointsValues22(i) / 10) - (oLength2 / 2), (yPointsValues22(i) / 10) - (oWidth2 / 2)))
                Catch

                End Try

            Else
                Dim xYpsosTomhsCurve1 As DrawingCurve
                xYpsosTomhsCurve1 = oView1.DrawingCurves.Item(1)

                Dim katwCut As Double 'to kanw dia ViewScale1 gia na to ferw stis diastaseis tou oView apo tou oSheet
                katwCut = (Abs(xYpsosTomhsCurve1.StartPoint.Y - xYpsosTomhsCurve1.EndPoint.Y) / 2 - cutGap / 2) / ViewScale2
                'MsgBox(katwCut)

                Try
                    oViewSketch.SketchLines.AddByTwoPoints(oTG.CreatePoint2d((xPointsValues22(i) / 10) - (oLength2 / 2), -(oWidth2 / 2)), oTG.CreatePoint2d((xPointsValues22(i) / 10) - (oLength2 / 2), (-(oWidth2 / 2) + katwCut)))
                    oViewSketch.SketchLines.AddByTwoPoints(oTG.CreatePoint2d((xPointsValues22(i) / 10) - (oLength2 / 2), (-(oWidth2 / 2) + katwCut + cutGap / ViewScale2)), oTG.CreatePoint2d((xPointsValues22(i) / 10) - (oLength2 / 2), (yPointsValues22(i) / 10) - (oWidth2 / 2)))
                Catch

                End Try

            End If

        Next

        oViewSketch.ExitEdit()
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        '''''''' epilogh Arithmwn meta pou kovw se grammes
        EpiloghArithmwn_22(myApplication, NumOfPoints, ViewScale2, xP, yP, xPointsValues22, yPointsValues22, oSheet, oLength2, oWidth2)


        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' Table 
        Dim mposiko As Double
        Dim TotalHeight As Double = yPoints22(NumOfPoints / 4)
        mposiko = (10 - TotalHeight * ViewScale2 / 10) / 4
        If V2_Konto Or V2_Psilo Then
            mposiko = 0
        End If
        Dim xTablePosition As Double
        xTablePosition = xP - 5
        Dim yTablePosition As Double
        yTablePosition = yP - 6.5 + mposiko
        Dim xTableSpace As Double = 5.2    'se cm einai 4.8cm o prwtos kai o deyteros tha mpei praktika sto +0.2 cm 

        Table_Construction(myApplication, oSheet, maxSeires, NumOfPoints + 1, xPoints22, yPoints22, xTablePosition, yTablePosition, xTableSpace)
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''





        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' 3D Model View ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Create a new NameValueMap object
        Dim oBaseViewOptions2 As NameValueMap
        oBaseViewOptions2 = myApplication.TransientObjects.CreateNameValueMap
        ' Set the options to use when creating the base view.
        oBaseViewOptions2.Add("IncludeSurfaceBodies", False)

        ' Now we define the placement points for
        ' the two drawing views we shall be adding to the sheet
        Dim xP2 As Integer = 38
        Dim yP2 As Integer = 13
        Dim oPlacementPoint2 As Point2d
        oPlacementPoint2 = myApplication.TransientGeometry.CreatePoint2d(xP2, yP2)

        ' define the view orientation for each view
        Dim ViewOrientation2 As ViewOrientationTypeEnum = ViewOrientationTypeEnum.kIsoTopRightViewOrientation

        ' define the view style for each view
        Dim ViewStyle2 As DrawingViewStyleEnum = DrawingViewStyleEnum.kShadedHiddenLineDrawingViewStyle

        ' now create our two views
        Dim oView2 As DrawingView
        oView2 = oSheet.DrawingViews.AddBaseView(oPartDoc, oPlacementPoint2, ViewScale2, ViewOrientation2, ViewStyle2, , , oBaseViewOptions2)

        oView2.RotateByAngle(-120 * Math.PI / 180)


        Dim oTitlePoint2 As Point2d
        oTitlePoint2 = myApplication.TransientGeometry.CreatePoint2d(xP2 - 2.12, oView2.Top + 0.8)
        oGeneralNoteTitles = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTitlePoint2, "3D ΜΟΝΤΕΛΟ ΟΡΙΖΟΝΤΙΟΥ")
        oGeneralNoteTitles.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextCenter
        oGeneralNoteTitles.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & "3D ΜΟΝΤΕΛΟ ΟΡΙΖΟΝΤΙΟΥ" & "</StyleOverride>"



        '''''''''''''''''''''''''''' 3d model Ypsos Dimension

        Dim kath3D, diam3D As Integer
        If D1 = D2 Then
            kath3D = 2
            diam3D = 11
        Else
            kath3D = 1
            diam3D = 14
        End If

        ''''Dimension gia to katheto
        Dim finalCurve3 As DrawingCurve
        finalCurve3 = oView2.DrawingCurves.Item(kath3D)

        Dim oGeomIntent3 As GeometryIntent
        oGeomIntent3 = oSheet.CreateGeometryIntent(finalCurve3)


        Dim textPoint3 As Inventor.Point2d = myApplication.TransientGeometry.CreatePoint2d(oView2.DrawingCurves.Item(kath3D).MidPoint.X - 1.1, oView2.DrawingCurves.Item(kath3D).MidPoint.Y - Math.Tan(Math.PI / 6) * 1.1)
        Dim myDim3DKatheto As GeneralDimension
        myDim3DKatheto = oGeneralDims.AddLinear(textPoint3, oGeomIntent3)

        'An einai poiotiko kryvw to lathos value kai vazw to swsto alliws prosthetw apla to (mm)
        If Not (V2_Konto Or V2_Psilo) Then
            myDim3DKatheto.Text.FormattedText = "<DimensionValue/>" & " mm"
        Else
            'kryvw to lathos value kai dinw to swsto
            myDim3DKatheto.HideValue = True
            myDim3DKatheto.Text.FormattedText = Format(yPoints22(NumOfPoints), "0.00") & " mm"
        End If



        ''''''Dimension gia thn diametro tou 3D object
        Dim finalCurve4 As DrawingCurve
        finalCurve4 = oView2.DrawingCurves.Item(diam3D)  ' gia thn diametro  (synolika exei 19)

        Dim oGeomIntent4 As GeometryIntent
        oGeomIntent4 = oSheet.CreateGeometryIntent(finalCurve4)


        Dim yDim As Double
        If D1 = D2 Then
            yDim = oView2.DrawingCurves.Item(kath3D).StartPoint.Y - 0.87
        Else
            yDim = oView2.DrawingCurves.Item(kath3D).EndPoint.Y - 0.87
        End If

        ' To curve  oView2.DrawingCurves.Item(2)  einai to xYpsosTomhs san curve object
        Dim textPoint4 As Inventor.Point2d = myApplication.TransientGeometry.CreatePoint2d(xP2, yDim)    ''''  ,yP2 - 5)
        Dim myDim3DOrizontio As GeneralDimension
        myDim3DOrizontio = oGeneralDims.AddDiameter(textPoint4, oGeomIntent4)
        myDim3DOrizontio.Text.FormattedText = "<DimensionValue/>" & " mm"



        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' 3D BREAK OPERATION
        If V2_Konto Or V2_Psilo Then

            Dim cutGap3D As Double = 0.25

            'vrisko to antistixo curve tou xYpsosTomhs sto 3D model
            Dim xYpsosTomhsCurve3D As DrawingCurve
            xYpsosTomhsCurve3D = oView2.DrawingCurves.Item(kath3D)

            'kai pairnw kata cutGap giro apo to .midPoint.y 
            ' Define the start point of the break
            Dim oStartPoint2 As Point2d
            oStartPoint2 = myApplication.TransientGeometry.CreatePoint2d(xYpsosTomhsCurve3D.MidPoint.X, xYpsosTomhsCurve3D.MidPoint.Y - cutGap3D)

            ' Define the end point of the break
            Dim oEndPoint2 As Point2d
            oEndPoint2 = myApplication.TransientGeometry.CreatePoint2d(xYpsosTomhsCurve3D.MidPoint.X, xYpsosTomhsCurve3D.MidPoint.Y + cutGap3D)


            Dim oBreakOperation2 As BreakOperation
            oBreakOperation2 = oView2.BreakOperations.Add(BreakOrientationEnum.kVerticalBreakOrientation, oStartPoint2, oEndPoint2, BreakStyleEnum.kRectangularBreakStyle, 10, (cutGap3D - 0.0001), 1, False)

        End If

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' TELOS 3D ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        ''''''''''''''''''''''''kleinw kai to part 
        oPartDoc.Close()



    End Sub










End Module
