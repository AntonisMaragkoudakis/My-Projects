Imports System.Math
Imports Inventor

Module Module5




    Sub Model_51_3D_Construction(ByVal myApplication As Inventor.Application, ByVal d1 As Double, ByVal Ypsos1 As Double, ByVal d2 As Double, ByVal Ypsos2 As Double, ByVal YpsosTomhs As Double, ByVal Dx As Double, ByVal gwnia As Double, ByRef partDoc As PartDocument, ByRef partComp As SheetMetalComponentDefinition, ByRef Rip_precision As Double, DiaforaGwniasRad As Double)


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
        oPlane = partComp.WorkPlanes.AddByLinePlaneAndAngle(partComp.WorkAxes(3), partComp.WorkPlanes(2), Abs(gwnia - 90) * Math.PI / 180)


        Dim upExtr2 As Double = Ypsos2 / 10
        'Surface 2 extrude
        Dim sk2 As PlanarSketch = partComp.Sketches.Add(oPlane)
        sk2.SketchCircles.AddByCenterRadius(myApplication.TransientGeometry.CreatePoint2d(Dx / 10, 0), d2 / 20)
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

        Dim Angle As Double
        Angle = Math.Asin(Dx / (d1 / 2))
        'MsgBox(Angle)


        Dim kathetoPlane As WorkPlane
        kathetoPlane = partComp.WorkPlanes.AddByLinePlaneAndAngle(partComp.WorkAxes(3), partComp.WorkPlanes(2), 90 * Math.PI / 180)

        'ftiaxnw to neo ypo gwnia plane gia thn feta
        Dim SplitPlane As WorkPlane
        SplitPlane = partComp.WorkPlanes.AddByLinePlaneAndAngle(partComp.WorkAxes(2), kathetoPlane, Angle + DiaforaGwniasRad)
        'San sketch h feta pou epitrepei ston swlina na ksediplwsei
        Dim sk3 As PlanarSketch = partComp.Sketches.Add(SplitPlane)
        sk3.SketchLines.AddAsTwoPointCenteredRectangle(myApplication.TransientGeometry.CreatePoint2d((Ypsos1 / 2 - YpsosTomhs) / 10, 0), myApplication.TransientGeometry.CreatePoint2d((Ypsos1 - YpsosTomhs) / 10 + 0.1, Rip_precision))
        Dim oProfile3 As Profile
        oProfile3 = sk3.Profiles.AddForSolid
        'Kanw cut thn feta ayth
        Dim oExtrudeDef3 As ExtrudeDefinition
        oExtrudeDef3 = partComp.Features.ExtrudeFeatures.CreateExtrudeDefinition(oProfile3, PartFeatureOperationEnum.kCutOperation)
        oExtrudeDef3.SetDistanceExtent(d1 / 20, PartFeatureExtentDirectionEnum.kNegativeExtentDirection)
        Dim oExtrude3 As ExtrudeFeature
        oExtrude3 = partComp.Features.ExtrudeFeatures.Add(oExtrudeDef3)


        'Visibilities off
        partDoc.ObjectVisibility.UserWorkPlanes = False
        partDoc.ObjectVisibility.ConstructionSurfaces = False


    End Sub







    Sub Ypologismos_Min_Max_51(ByVal myEdge1 As Edge, ByVal oLength As Double, ByVal NumOfPoints As Double, ByVal peristrafike As Integer, ByVal Ypsos1 As Double, ByVal YpsosTomhs As Double, ByRef minX As Double, ByRef maxX As Double, ByRef minY As Double, ByRef maxY As Double)


        '''' arxikopoihsh twn max sto miden
        maxX = -oLength * 10
        maxY = -Ypsos1
        '''' arxikopoihsh minX, minY se megaliteres apo tis pragmatika min times tous
        minX = oLength * 10
        minY = Ypsos1

        ''''''' analiw me while to myEdge1 kai briskw ta min-max shmeia

        Dim oCurveEval As CurveEvaluator
        oCurveEval = myEdge1.Evaluator
        ' Get the parametric range of the curve.
        Dim dMinParam As Double
        Dim dMaxParam As Double
        Call oCurveEval.GetParamExtents(dMinParam, dMaxParam)

        Dim tempX, tempY As Double

        For i = 0 To 1000

            ' Calculate the current parameter to evaluate.
            Dim currentParam As Double                                              '''' mallon akyro to parakato symperasma
            currentParam = dMinParam + ((dMaxParam - dMinParam) / 1000) * i  '''' otan einai kykliko to edge oti einai h loupa p.x to (NumOfPoints - 1) to kanw syn 1

            ' Assign the value to an array since the GetPointAtParam
            ' takes an array as input.
            Dim adParam(0) As Double
            adParam(0) = currentParam

            ' Get the coordinates of the parameter point in model space.
            Dim adPoints(2) As Double
            Call oCurveEval.GetPointAtParam(adParam, adPoints)

            'xPoints(i) += oLength * 10
            'yPoints(i) -= Ypsos1
            'MsgBox(dMinParam & "  ,  " & dMaxParam & "  ,  " & adPoints(0) & "  ,  " & adPoints(1) & "  ,  " & adPoints(2))
            If i <> NumOfPoints Then
                If peristrafike = 2 Then

                    tempX = adPoints(0) * 10                             '+ oLength * 10
                    tempY = 10 * adPoints(1) + Ypsos1 - 2 * Ypsos1       '- Ypsos1
                Else

                    tempX = (adPoints(0) - YpsosTomhs / 20) * 10 - (Ypsos1 - YpsosTomhs) / 2 + oLength * 10
                    tempY = 10 * (adPoints(1) + YpsosTomhs / 20) + (Ypsos1 - YpsosTomhs) / 2 - Ypsos1
                End If
            End If

            If tempX > maxX Then
                maxX = tempX
            End If

            If tempY > maxY Then
                maxY = tempY
            End If

            If tempX < minX Then
                minX = tempX
            End If

            If tempY < minY Then
                minY = tempY
            End If

        Next

        minY = Ypsos1 - Abs(minY)
        maxY = Ypsos1 - Abs(maxY)

        'MsgBox("p1=( " & minX & ", " & mesoY & " )")
        'MsgBox("p2=( " & mesoX & ", " & maxY & " )")
        'MsgBox("p3=( " & maxX & ", " & mesoY & " )")
        'MsgBox("p4=( " & mesoX & ", " & minY & " )")

    End Sub








    Sub Model_52_3D_Construction(ByVal myApplication As Inventor.Application, ByVal d1 As Double, ByVal Ypsos1 As Double, ByVal d2 As Double, ByVal Ypsos2 As Double, ByVal YpsosTomhs As Double, ByVal Dx As Double, ByVal gwnia As Double, ByRef partDoc As PartDocument, ByRef partComp As SheetMetalComponentDefinition, ByRef Rip_precision As Double)


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
        oPlane = partComp.WorkPlanes.AddByLinePlaneAndAngle(partComp.WorkAxes(3), partComp.WorkPlanes(2), Abs(gwnia - 90) * Math.PI / 180)


        ' orizontiou sketch sto oPlane
        Dim sk2 As PlanarSketch = partComp.Sketches.Add(oPlane)
        sk2.SketchCircles.AddByCenterRadius(myApplication.TransientGeometry.CreatePoint2d(Dx / 10, 0), d2 / 20)
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



        Dim sk3 As PlanarSketch = partComp.Sketches.Add(oPlane)
        Dim myLine As SketchLine
        myLine = sk3.SketchLines.AddByTwoPoints(myApplication.TransientGeometry.CreatePoint2d(0, 0), myApplication.TransientGeometry.CreatePoint2d(-d2 / 100, 0))

        Dim oPlane2 As WorkPlane
        oPlane2 = partComp.WorkPlanes.AddByLinePlaneAndAngle(myLine, oPlane, 90 * Math.PI / 180)

        'San sketch h feta pou epitrepei ston swlina na ksediplwsei
        Dim sk4 As PlanarSketch = partComp.Sketches.Add(oPlane2)
        sk4.SketchLines.AddAsTwoPointCenteredRectangle(myApplication.TransientGeometry.CreatePoint2d(-Dx / 10, (Ypsos2 / 2) / 10), myApplication.TransientGeometry.CreatePoint2d(-Dx / 10 + Rip_precision, (Ypsos2) / 10 + 0.1))
        Dim oProfile3 As Profile
        oProfile3 = sk4.Profiles.AddForSolid
        'Kanw cut thn feta ayth
        Dim oExtrudeDef3 As ExtrudeDefinition
        oExtrudeDef3 = partComp.Features.ExtrudeFeatures.CreateExtrudeDefinition(oProfile3, PartFeatureOperationEnum.kCutOperation)
        oExtrudeDef3.SetDistanceExtent(d1 / 20, PartFeatureExtentDirectionEnum.kPositiveExtentDirection)
        Dim oExtrude3 As ExtrudeFeature
        oExtrude3 = partComp.Features.ExtrudeFeatures.Add(oExtrudeDef3)


        'Visibilities off
        partDoc.ObjectVisibility.UserWorkPlanes = False
        partDoc.ObjectVisibility.ConstructionSurfaces = False
        sk3.Visible = False
        sk4.Visible = False



    End Sub










    Public Sub Ypologismos_Edge_52(ByVal myEdge1 As Inventor.Edge, ByVal myEdge2 As Inventor.Edge, ByVal NumOfPoints As Integer, ByVal peristrafike As Boolean, ByVal YpsosTomhs As Double, ByRef xPoints() As Double, ByRef yPoints() As Double, ByRef provlima As Boolean)

        ''pernw ton CurveEvaluator tou myEdge
        Dim oCurveEval As CurveEvaluator
        oCurveEval = myEdge1.Evaluator
        '' Get the parametric range of the curve.
        Dim dMinParam As Double
        Dim dMaxParam As Double
        Call oCurveEval.GetParamExtents(dMinParam, dMaxParam)
        'Dim curveLength As Double
        'Call oCurveEval.GetLengthAtParam(dMinParam, dMaxParam, curveLength)
        'MessageBox.Show(curveLength)


        Dim i As Integer
        For i = 0 To ((NumOfPoints - 1) / 4)     '(NumOfPoints - 1) / 2    einai to meso simeio

            ' Calculate the current parameter to evaluate.
            Dim currentParam As Double
            currentParam = dMinParam + ((dMaxParam - dMinParam) / ((NumOfPoints - 1) / 4)) * i

            ' Assign the value to an array since the GetPointAtParam
            ' takes an array as input.
            Dim adParam(0) As Double
            adParam(0) = currentParam

            ' Get the coordinates of the parameter point in model space.
            Dim adPoints(2) As Double
            Call oCurveEval.GetPointAtParam(adParam, adPoints)


            If Not peristrafike Then
                If i = 0 Then
                    xPoints((NumOfPoints - 1) / 4) = adPoints(0) * 10
                Else
                    xPoints(((NumOfPoints - 1) / 4) - i) = adPoints(0) * 10
                End If
                yPoints(((NumOfPoints - 1) / 4) - i) = 10 * adPoints(1) + YpsosTomhs

            Else

                If i = 0 Then
                    xPoints((NumOfPoints - 1) / 4) = (adPoints(0) - YpsosTomhs / 20) * 10
                Else
                    xPoints(((NumOfPoints - 1) / 4) - i) = (adPoints(0) - YpsosTomhs / 20) * 10
                End If
                yPoints(((NumOfPoints - 1) / 4) - i) = 10 * (adPoints(1) + YpsosTomhs / 20)

            End If

        Next



        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' pame pali gia to allo edge 
        ''pernw ton CurveEvaluator tou myEdge
        'Dim oCurveEval As CurveEvaluator
        oCurveEval = myEdge2.Evaluator
        '' Get the parametric range of the curve.
        Call oCurveEval.GetParamExtents(dMinParam, dMaxParam)

        For i = 0 To ((NumOfPoints - 1) * 3 / 4)     '(NumOfPoints - 1) / 2    einai to meso simeio

            ' Calculate the current parameter to evaluate.
            Dim currentParam As Double
            currentParam = dMinParam + ((dMaxParam - dMinParam) / ((NumOfPoints - 1) * 3 / 4)) * i

            ' Assign the value to an array since the GetPointAtParam
            ' takes an array as input.
            Dim adParam(0) As Double
            adParam(0) = currentParam

            ' Get the coordinates of the parameter point in model space.
            Dim adPoints(2) As Double
            Call oCurveEval.GetPointAtParam(adParam, adPoints)


            If Not peristrafike Then
                If i = 0 Then
                    xPoints((NumOfPoints - 1) * 3 / 4 + NumOfPoints / 4) = adPoints(0) * 10
                Else
                    xPoints(((NumOfPoints - 1) * 3 / 4) - i + NumOfPoints / 4) = adPoints(0) * 10
                End If
                yPoints(((NumOfPoints - 1) * 3 / 4) - i + NumOfPoints / 4) = 10 * adPoints(1) + YpsosTomhs

            Else

                If i = 0 Then
                    xPoints((NumOfPoints - 1) * 3 / 4 + NumOfPoints / 4) = (adPoints(0) - YpsosTomhs / 20) * 10
                Else
                    xPoints(((NumOfPoints - 1) * 3 / 4) - i + NumOfPoints / 4) = (adPoints(0) - YpsosTomhs / 20) * 10
                End If
                yPoints(((NumOfPoints - 1) * 3 / 4) - i + NumOfPoints / 4) = 10 * (adPoints(1) + YpsosTomhs / 20)

            End If

        Next


        '''' Provlima tou 2*YpsosTomhs
        If yPoints(0) > 1.5 * YpsosTomhs Then
            provlima = True
            For k = 0 To NumOfPoints - 1

                yPoints(k) -= YpsosTomhs
            Next

        End If

    End Sub





    Public Sub Ypolgogismos_52_Symetrikos(ByVal oEdges22 As Edges, ByVal NumOfPoints As Integer, ByVal YpsosStoY As Double, ByVal peristrafike2 As Integer, ByRef xPoints22 As Double(), ByRef yPoints22 As Double(), ByVal provlima As Boolean)

        Dim myedge As Edge

        'dialegw to  proto edge pou tha analysw
        myedge = oEdges22.Item(13)

        Dim x1(NumOfPoints / 4) As Double
        Dim y1(NumOfPoints / 4) As Double
        'Ypologismos_Genikos(myedge, NumOfPoints / 4, x1, y1)
        Ypologismos_Peiragmenos_Edge52(myedge, NumOfPoints / 4, peristrafike2, YpsosStoY, xPoints22(0), xPoints22(NumOfPoints / 4), x1, y1)
        '''' kai kanw antistrofh
        For i = 1 To NumOfPoints / 4 - 1
            xPoints22(i) = x1(NumOfPoints / 4 - i)
            yPoints22(i) = y1(NumOfPoints / 4 - i)
        Next



        'dialegw to  deytero edge pou tha analysw
        myedge = oEdges22.Item(9)  ' mporei na einai kai 11

        Dim x2(NumOfPoints * 3 / 4) As Double
        Dim y2(NumOfPoints * 3 / 4) As Double
        'Ypologismos_Genikos(myedge, NumOfPoints * 3 / 4, x2, y2)
        Ypologismos_Peiragmenos_Edge52(myedge, NumOfPoints * 3 / 4, peristrafike2, YpsosStoY, xPoints22(NumOfPoints / 4), xPoints22(NumOfPoints), x2, y2)
        '''' kai kanw antistrofh
        Dim j As Integer = NumOfPoints * 3 / 4 - 1
        For i = NumOfPoints / 4 + 1 To NumOfPoints - 1
            xPoints22(i) = x2(j)
            yPoints22(i) = y2(j)
            j = j - 1
        Next



        '''' Provlima tou 2 * YpsosStoY
        If provlima Then
            'MsgBox("MPIKE GAMW")
            For k = 1 To NumOfPoints - 1
                If k <> NumOfPoints / 4 Then
                    yPoints22(k) -= YpsosStoY
                End If
            Next

        End If

    End Sub




    Public Sub Ypologismos_Peiragmenos_Edge52(ByVal myEdge1 As Inventor.Edge, ByVal NumOfPoints As Integer, ByVal peristrafike2 As Integer, ByVal YpsosStoY As Double, ByVal minX As Double, ByVal maxX As Double, ByRef xPoints() As Double, ByRef yPoints() As Double)


        Dim xLength As Double = maxX - minX
        Dim xStep As Double = xLength / NumOfPoints
        Dim nextX, vima, currentParam, tempX, tempY As Double
        Dim megalytero As Boolean

        Dim oCurveEval As CurveEvaluator
        oCurveEval = myEdge1.Evaluator
        ' Get the parametric range of the curve.
        Dim dMinParam As Double
        Dim dMaxParam As Double
        Call oCurveEval.GetParamExtents(dMinParam, dMaxParam)

        For i = 1 To NumOfPoints - 1



            nextX = maxX - i * xStep

            'arxikopoiw to vhma tou nextX pou paw na vrw
            vima = ((dMaxParam - dMinParam) / NumOfPoints) * i


            '''''ypologizw ta prwta tempX,tempY
            currentParam = dMinParam + vima
            ' Assign the value to an array since the GetPointAtParam
            ' takes an array as input.
            Dim adParam(0) As Double
            adParam(0) = currentParam
            ' Get the coordinates of the parameter point in model space.
            Dim adPoints(2) As Double
            Call oCurveEval.GetPointAtParam(adParam, adPoints)
            'MsgBox(dMinParam & "  ,  " & dMaxParam & "  ,  " & adPoints(0) & "  ,  " & adPoints(1) & "  ,  " & adPoints(2))



            If Not peristrafike2 Then
                tempX = adPoints(0) * 10
                tempY = 10 * adPoints(1) + YpsosStoY
            Else
                tempX = (adPoints(0) - YpsosStoY / 20) * 10
                tempY = 10 * (adPoints(1) + YpsosStoY / 20)
            End If



            'If peristrafike2 = 3 Then
            '    tempX = 10 * adPoints(1)
            '    tempY = YpsosStoY + Abs(10 * adPoints(0))
            'Else
            '    tempX = 10 * adPoints(0)
            '    tempY = 10 * adPoints(1) + YpsosStoY
            'End If

            If tempX > nextX Then
                megalytero = True
            Else
                megalytero = False
            End If



            While Format(tempX, "0.000") <> Format(nextX, "0.000")

                ''MsgBox(tempX & " , " & tempY & " , " & vima & " , " & nextX & " , " & dMinParam & " , " & dMaxParam & " , " & currentParam)


                If tempX > nextX And megalytero Then
                    If currentParam + vima > dMaxParam Then
                        currentParam = dMaxParam
                    Else
                        currentParam = currentParam + vima
                    End If
                ElseIf tempX > nextX And Not (megalytero) Then
                    megalytero = True
                    vima = vima / 2
                    currentParam = currentParam + vima
                ElseIf tempX < nextX And Not (megalytero) Then
                    currentParam = currentParam - vima
                ElseIf tempX < nextX And megalytero Then
                    megalytero = False
                    vima = vima / 2
                    currentParam = currentParam - vima
                End If


                adParam(0) = currentParam
                ' Get the coordinates of the parameter point in model space.
                Call oCurveEval.GetPointAtParam(adParam, adPoints)
                'MsgBox(dMinParam & "  ,  " & dMaxParam & "  ,  " & adPoints(0) & "  ,  " & adPoints(1) & "  ,  " & adPoints(2))
                If Not peristrafike2 Then
                    tempX = adPoints(0) * 10
                    tempY = 10 * adPoints(1) + YpsosStoY
                Else
                    tempX = (adPoints(0) - YpsosStoY / 20) * 10
                    tempY = 10 * (adPoints(1) + YpsosStoY / 20)
                End If

            End While

            xPoints(i) = tempX
            yPoints(i) = tempY


        Next



    End Sub




    Sub MaxY_2_ypologismos_52(ByVal oEdges22 As Edges, ByVal peristrafike2 As Integer, ByVal YpsosStoY As Double, ByRef maxY_2 As Double, ByRef minY_2 As Double, ByVal provlima As Boolean)

        Dim tempY As Double = 0
        Dim proigoymeno_Y As Double = 0
        Dim paraProigoymeno_Y As Double

        Dim myedge As Edge
        'dialegw to  deytero edge pou tha analysw
        myedge = oEdges22.Item(9)   ' mporei na einai  11

        Dim oCurveEval As CurveEvaluator
        oCurveEval = myedge.Evaluator
        ' Get the parametric range of the curve.
        Dim dMinParam As Double
        Dim dMaxParam As Double
        Call oCurveEval.GetParamExtents(dMinParam, dMaxParam)



        '''''''''''''''''''''' edw kanw forloop gia to maxY_2
        For i = 0 To 50000

            ' Calculate the current parameter to evaluate.
            Dim currentParam As Double
            currentParam = dMaxParam - ((dMaxParam - dMinParam) / 50000) * i

            ' Assign the value to an array since the GetPointAtParam
            ' takes an array as input.
            Dim adParam(0) As Double
            adParam(0) = currentParam

            ' Get the coordinates of the parameter point in model space.
            Dim adPoints(2) As Double
            Call oCurveEval.GetPointAtParam(adParam, adPoints)

            paraProigoymeno_Y = proigoymeno_Y
            proigoymeno_Y = tempY


            If Not peristrafike2 Then
                tempY = 10 * adPoints(1) + YpsosStoY
            Else
                tempY = 10 * (adPoints(1) + YpsosStoY / 20)
            End If

            '' edw pleon ta exw piasei kai ta 3 me to temoY ousiastika na einai to twrinoY
            'MsgBox(paraProigoymeno_Y & " , " & proigoymeno_Y & " , " & tempY)


            If proigoymeno_Y > tempY And proigoymeno_Y > paraProigoymeno_Y Then
                maxY_2 = proigoymeno_Y
                'MsgBox("vgike , " & i)
                Exit For
            End If


        Next





        '''''''''''''''''''' omoiws gia to minY_2 gia pio grigorh ektelesh anapoda apo dMinParam + vhma
        tempY = 0
        proigoymeno_Y = 0
        For i = 0 To 50000

            ' Calculate the current parameter to evaluate.
            Dim currentParam As Double
            currentParam = dMinParam + ((dMaxParam - dMinParam) / 50000) * i

            ' Assign the value to an array since the GetPointAtParam
            ' takes an array as input.
            Dim adParam(0) As Double
            adParam(0) = currentParam

            ' Get the coordinates of the parameter point in model space.
            Dim adPoints(2) As Double
            Call oCurveEval.GetPointAtParam(adParam, adPoints)

            paraProigoymeno_Y = proigoymeno_Y
            proigoymeno_Y = tempY


            If Not peristrafike2 Then
                tempY = 10 * adPoints(1) + YpsosStoY
            Else
                tempY = 10 * (adPoints(1) + YpsosStoY / 20)
            End If

            '' edw pleon ta exw piasei kai ta 3 me to temoY ousiastika na einai to twrinoY
            'MsgBox(paraProigoymeno_Y & " , " & proigoymeno_Y & " , " & tempY)

            If proigoymeno_Y < tempY And proigoymeno_Y < paraProigoymeno_Y Then
                minY_2 = proigoymeno_Y
                'MsgBox("vgike , " & i)
                Exit For
            End If

        Next


        If provlima Then
            maxY_2 -= YpsosStoY
            minY_2 -= YpsosStoY
        End If
        'MsgBox("telos , " & maxY_2 & " , " & minY_2)

    End Sub













    Public Sub DWG_51(oSavePath1 As String, myApplication As Application, oSheet As Sheet, D1 As Double, D2 As Double, Ypsos1 As Double, NumOfPoints As Integer, ViewScale1 As Double, Dx As Double, xPointsValues As Double(), yPointsValues As Double(), oLength As Double, oWidth As Double, maxSeires As Integer, xPoints As Double(), yPoints As Double(), Rip_precision_sto_voithitiko1 As Double, adynato_3D1 As Boolean, poiotiko1 As Boolean, axis_X As Double, axis_Y As Double, V_Ypsos1 As Double)

        Dim oSaveName1 As String
        Dim sPartPath As String
        oSaveName1 = "test_V1" & ".ipt"
        sPartPath = oSavePath1 & oSaveName1

        Dim oPartDoc As PartDocument = myApplication.Documents.Open(sPartPath, True)

        oPartDoc.SubType = "{9C464203-9BAE-11D3-8BAD-0060B0CE6BB4}"

        Dim partComp As ComponentDefinition
        partComp = oPartDoc.ComponentDefinition
        Dim TheFaceCounter As Integer
        If partComp.HasFlatPattern = False Then
            UnfoldTheFace(myApplication, partComp, TheFaceCounter)
            Dim fp As FlatPattern = myApplication.ActiveEditObject
            Dim peristrafike As Integer
            Rotate_Fp51(partComp, Rip_precision_sto_voithitiko1, D1, V_Ypsos1, fp, peristrafike, oLength, oWidth)
        End If

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


        ' define the view orientation for each view
        Dim ViewOrientation1 As ViewOrientationTypeEnum = ViewOrientationTypeEnum.kDefaultViewOrientation

        ' define the view style for each view
        Dim ViewStyle1 As DrawingViewStyleEnum = DrawingViewStyleEnum.kHiddenLineRemovedDrawingViewStyle  'kShadedHiddenLineDrawingViewStyle
        ' now create our two views                                                                          
        Dim oView1 As DrawingView
        oView1 = oSheet.DrawingViews.AddBaseView(oPartDoc, oPlacementPoint1, ViewScale1, ViewOrientation1, ViewStyle1, , , oBaseViewOptions)



        Dim oGeneralNoteTitles As GeneralNote
        oTitlePoint1 = myApplication.TransientGeometry.CreatePoint2d(7 - 3.4, 21 + 6.1)
        oGeneralNoteTitles = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTitlePoint1, "ΑΝΑΠΤΥΓΜΑ ΚΑΤΑΚΟΡΥΦΟΥ ΚΥΛΙΝΔΡΟΥ (1)")
        oGeneralNoteTitles.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & "ΑΝΑΠΤΥΓΜΑ ΚΑΤΑΚΟΡΥΦΟΥ ΚΥΛΙΝΔΡΟΥ (1)" & "</StyleOverride>"



        If poiotiko1 Then
            Dim oTitlePoint2 As Point2d
            oTitlePoint2 = myApplication.TransientGeometry.CreatePoint2d(7 - 3.44 + 2.27, 21 + 6.7)
            oGeneralNoteTitles = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTitlePoint2, "Π Ο Ι Ο Τ Ι Κ Ο")
            oGeneralNoteTitles.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & "Π Ο Ι Ο Τ Ι Κ Ο" & "</StyleOverride>"
        End If




        xP = 7
        yP = 21
        Dim dikths_kampilhs As Integer
        Kouti_kai_invisibility_51(myApplication, xP, yP, dikths_kampilhs, D1, Ypsos1, oView1, oSheet)










        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' Katskeyh Systima Aksonwn 
        Dim oTG As TransientGeometry
        oTG = myApplication.TransientGeometry

        Dim oCurveEval As Curve2dEvaluator
        Dim dMinParam As Double
        Dim dMaxParam As Double
        Dim adParam(0) As Double
        Dim adPoints(2) As Double



        ''''''' thelw na vrw to x tou view pou tha einai to proto tis kampilis
        'pianw thn kampilh
        Dim panwCurve As DrawingCurve = oView1.DrawingCurves.Item(dikths_kampilhs)
        'pairnw to evaluator
        oCurveEval = panwCurve.Evaluator2D
        ' Get the parametric range of the curve sto 1/4 ths tha einai to meso x
        Call oCurveEval.GetParamExtents(dMinParam, dMaxParam)
        Dim mesoParam As Double
        mesoParam = dMinParam '+ ((dMaxParam - dMinParam) / 4)
        ' Assign the value to an array since the GetPointAtParam
        ' takes an array as input.
        adParam(0) = mesoParam
        ' Get the coordinates of the parameter point in model space.
        Call oCurveEval.GetPointAtParam(adParam, adPoints)
        Dim axis_X_StoSheet As Double = adPoints(0)
        Dim axis_X_StoView As Double = axis_X_StoSheet / ViewScale1
        Dim axis_Y_StoSheet As Double = adPoints(1)
        Dim axis_Y_StoView As Double = axis_Y_StoSheet / ViewScale1


        Dim xAxis, yAxis As Double
        xAxis = axis_X_StoView 'to thelw se cm opote den to kanw /10
        yAxis = axis_Y_StoView

        AxisSystem(myApplication, oView1, oTG, xAxis, yAxis, ViewScale1)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''




        '''''''''''''''' PROSDIORISMOS NEOU CENTER kai epilogh arithmwn
        xP = 7
        yP = 21
        Dim oViewPos As Point2d = myApplication.TransientGeometry.CreatePoint2d(xP, yP)
        oView1.Position = oViewPos



        '''''''''''''''''''''''' Epilogh Arithmwn
        EpiloghArithmwn_51(myApplication, NumOfPoints, ViewScale1, xP, yP, xPointsValues, yPointsValues, oSheet, oLength, oWidth)



        ''''''''''''''''''''''''''''''''' Table 
        Dim xTablePosition As Double
        xTablePosition = xP - 5
        Dim yTablePosition As Double
        yTablePosition = yP - 6.5 - 1
        Dim xTableSpace As Double = 5.2    'se cm einai 4.8cm o prwtos kai o deyteros tha mpei praktika sto +0.2 cm 

        Table_Construction(myApplication, oSheet, maxSeires, NumOfPoints, xPoints, yPoints, xTablePosition, yTablePosition, xTableSpace)


        '''''''''''''''Emfanish tou axis_x kai axis_y sto xarti
        Dim xP_ax As Double = 13.4 '' idia me  xP_3D - 2.6 
        Dim yP_ax As Double = yP + 0.75
        Emfanisi_axis_syntetagmenes(myApplication, oSheet, oGeneralNoteTitles, xP_ax, yP_ax, axis_X, axis_Y)



        '''''''''' Ptin trleiwsw na diksw to axis.x kai axis.y sto xarti ????????????

        oPartDoc.Close(True)

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



        '''''''''''''''''''''''''''''''''''' 3D Model View 1
        If Not adynato_3D1 Then

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
            Dim xP_3D As Double = 16
            Dim yP_3D As Double = 10
            Dim oPlacementPoint2 As Point2d
            oPlacementPoint2 = myApplication.TransientGeometry.CreatePoint2d(xP_3D, yP_3D)


            Dim ViewScale3D_1 As Double
            ViewScale3D_1 = 32 / D1

            ' define the view orientation for each view
            Dim ViewOrientation2 As ViewOrientationTypeEnum = ViewOrientationTypeEnum.kIsoTopLeftViewOrientation

            ' define the view style for each view
            Dim ViewStyle2 As DrawingViewStyleEnum = DrawingViewStyleEnum.kShadedDrawingViewStyle   'kShadedHiddenLineDrawingViewStyle

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

            'MsgBox(oView2.DrawingCurves.Count)
            Dim kath As Integer = 13
            Dim oriz As Integer = 9
            If oView2.DrawingCurves.Count <> 13 Then
                kath = oView2.DrawingCurves.Count
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

        Else

            Dim xP_3D As Double = 17
            Dim yP_3D As Double = 6


            Dim oTitlePoint3 As Point2d
            oTitlePoint3 = myApplication.TransientGeometry.CreatePoint2d(xP_3D - 2.25 - 0.77, yP_3D + 3.8)
            oGeneralNoteTitles = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTitlePoint3, "Η ΤΟΜΗ ΠΟΥ ΠΡΟΚΕΙΠΤΕΙ ΑΠΟ")
            oGeneralNoteTitles.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextCenter
            oGeneralNoteTitles.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & "Η ΤΟΜΗ ΠΟΥ ΠΡΟΚΕΙΠΤΕΙ ΑΠΟ" & "</StyleOverride>"


            Dim oTitlePoint4 As Point2d
            oTitlePoint4 = myApplication.TransientGeometry.CreatePoint2d(xP_3D - 2.27 - 0.175, yP_3D + 3.4)
            oGeneralNoteTitles = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTitlePoint4, "ΤΗΝ ΤΟΜΗ ΕΙΝΑΙ ΑΡΚΕΤΑ")
            oGeneralNoteTitles.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextCenter
            oGeneralNoteTitles.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & "ΤΗΝ ΤΟΜΗ ΕΙΝΑΙ ΑΡΚΕΤΑ" & "</StyleOverride>"


            Dim oTitlePoint5 As Point2d
            oTitlePoint5 = myApplication.TransientGeometry.CreatePoint2d(xP_3D - 2.27 - 0.347, yP_3D + 3)
            oGeneralNoteTitles = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTitlePoint5, "ΣΤΕΝΟΜΑΚΡΗ ΚΑΙ ΚΑΘΙΣΤΑ")
            oGeneralNoteTitles.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextCenter
            oGeneralNoteTitles.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & "ΣΤΕΝΟΜΑΚΡΗ ΚΑΙ ΚΑΘΙΣΤΑ" & "</StyleOverride>"

            Dim oTitlePoint6 As Point2d
            oTitlePoint6 = myApplication.TransientGeometry.CreatePoint2d(xP_3D - 2.27 - 0.44, yP_3D + 2.6)
            oGeneralNoteTitles = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTitlePoint6, "ΜΗ ΠΡΟΣΙΤΗ ΤΗΝ ΠΡΟΒΟΛΗ")
            oGeneralNoteTitles.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextCenter
            oGeneralNoteTitles.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & "ΜΗ ΠΡΟΣΙΤΗ ΤΗΝ ΠΡΟΒΟΛΗ" & "</StyleOverride>"

            Dim oTitlePoint7 As Point2d
            oTitlePoint7 = myApplication.TransientGeometry.CreatePoint2d(xP_3D - 2.27 + 0.407, yP_3D + 2.2)
            oGeneralNoteTitles = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTitlePoint7, "ΤΟΥ 3D ΜΟΝΤΕΛΟΥ")
            oGeneralNoteTitles.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextCenter
            oGeneralNoteTitles.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & "ΤΟΥ 3D ΜΟΝΤΕΛΟΥ" & "</StyleOverride>"

        End If

    End Sub





    Public Sub Kouti_kai_invisibility_51(myApplication As Application, xP As Double, yP As Double, ByRef dikths_kampilhs As Integer, D1 As Double, Ypsos1 As Double, oView As DrawingView, oSheet As Sheet)


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
            'MsgBox("error 404")
        End Try

        oViewSketch.ExitEdit()




        For i = 1 To oView.DrawingCurves.Count
            If oView.DrawingCurves.Item(i).CurveType <> "5128" Then


                For j = 1 To oView.DrawingCurves.Item(i).Segments.Count

                    oView.DrawingCurves.Item(i).Segments.Item(j).Visible = False

                Next

            Else
                dikths_kampilhs = i

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





    Public Sub EpiloghArithmwn_51(myApplication As Application, ByVal NumOfPoints As Integer, ByVal ViewScale1 As Double, xP As Double, yP As Double, ByVal xPoints() As Double, ByVal yPoints() As Double, ByRef oSheet As Sheet, oLength As Double, oWidth As Double)




        Dim oTG As TransientGeometry
        oTG = myApplication.TransientGeometry


        Dim sText As String
        Dim oGeneralNote As GeneralNote
        Dim xDiff As Double
        Dim yDiff As Double = 0

        For i = 0 To NumOfPoints - 1

            Dim b20, b24, b28, b32, b36, b40, b44, b48 As Boolean
            b20 = NumOfPoints = 20 And (i = 0 Or i = 3 Or i = 5 Or i = 7 Or i = 10 Or i = 13 Or i = 15 Or i = 17)
            b24 = NumOfPoints = 24 And (i Mod 2 = 0)
            b28 = NumOfPoints = 28 And (i = 0 Or i = 4 Or i = 7 Or i = 10 Or i = 14 Or i = 18 Or i = 21 Or i = 24)
            b32 = NumOfPoints = 32 And (i = 0 Or i = 3 Or i = 5 Or i = 8 Or i = 11 Or i = 13 Or i = 16 Or i = 19 Or i = 21 Or i = 24 Or i = 27 Or i = 29)
            b36 = NumOfPoints = 36 And (i Mod 3 = 0)
            b40 = NumOfPoints = 40 And (i = 0 Or i = 4 Or i = 7 Or i = 10 Or i = 13 Or i = 16 Or i = 20 Or i = 24 Or i = 27 Or i = 30 Or i = 33 Or i = 36)
            b44 = NumOfPoints = 44 And (i = 0 Or i = 4 Or i = 7 Or i = 11 Or i = 15 Or i = 18 Or i = 22 Or i = 26 Or i = 29 Or i = 33 Or i = 37 Or i = 40)
            b48 = NumOfPoints = 48 And (i Mod 4 = 0)


            sText = i + 1
            xDiff = -sText.Length * 0.1

            '''' vazw ta volika simeia
            If (NumOfPoints < 20 Or b20 Or b24 Or b28 Or b32 Or b36 Or b40 Or b44 Or b48) Then




                Dim metatopish As Double = 0
                ''''''''''''''' vriksw tin metatopisi tou x symfwna me ton rythmo metavolhs ( mono sta mh simeia kampis)
                If Not (i = 0) Then

                    If xPoints(i) > xPoints(i - 1) Then '''' panw miso
                        yDiff = 0.38
                    Else
                        yDiff = -0.13      '''' katw miso
                    End If


                    Dim gwnia As Double
                    Dim dx, dy As Double


                    If xPoints(i) > xPoints(i - 1) Then '''' panw miso


                        '''''' an ayksanei me noiazei ws pros to epomeno dy
                        If yPoints(i) < yPoints(i + 1) Then

                            dx = xPoints(i + 1) - xPoints(i)
                            dy = yPoints(i + 1) - yPoints(i)
                            gwnia = Math.Atan(dy / dx) * 180 / PI
                            metatopish = -((gwnia / 90) / 10) * sText.Length - 0.015
                            If gwnia > 40 Then
                                metatopish = metatopish - 0.015
                            End If

                        Else ''''' meiwnei me noiazei ws pros to proigoymeno dy

                            dx = Abs(xPoints(i - 1) - xPoints(i))
                            dy = Abs(yPoints(i - 1) - yPoints(i))
                            gwnia = Math.Atan(dy / dx) * 180 / PI
                            metatopish = ((gwnia / 90) / 10) * sText.Length + 0.015
                            If gwnia > 40 Then
                                metatopish = metatopish + 0.015
                            End If

                        End If

                    Else    '''' katw miso


                        If i = NumOfPoints - 1 Then

                            dx = Abs(xPoints(0) - xPoints(i))
                            dy = Abs(yPoints(0) - yPoints(i))
                            gwnia = Math.Atan(dy / dx) * 180 / PI
                            metatopish = -((gwnia / 90) / 10) * sText.Length - 0.015
                            If gwnia > 40 Then
                                metatopish = metatopish - 0.015
                            End If

                        ElseIf yPoints(i) > yPoints(i + 1) Then

                            dx = Abs(xPoints(i + 1) - xPoints(i))
                            dy = Abs(yPoints(i + 1) - yPoints(i))
                            gwnia = Math.Atan(dy / dx) * 180 / PI
                            metatopish = ((gwnia / 90) / 10) * sText.Length + 0.015
                            If gwnia > 40 Then
                                metatopish = metatopish + 0.015
                            End If

                        Else

                            dx = Abs(xPoints(i - 1) - xPoints(i))
                            dy = Abs(yPoints(i - 1) - yPoints(i))
                            gwnia = Math.Atan(dy / dx) * 180 / PI
                            metatopish = -((gwnia / 90) / 10) * sText.Length - 0.015
                            If gwnia > 40 Then
                                metatopish = metatopish - 0.015
                            End If

                        End If


                    End If

                Else

                    If i = 0 Then
                        xDiff = -0.22
                        yDiff = 0.14
                    ElseIf i = NumOfPoints / 2 Then
                        xDiff = 0.08
                        yDiff = 0.14
                    End If


                End If

                If i <> 0 And i <> NumOfPoints - 1 Then
                    If (yPoints(i) < yPoints(i + 1) And yPoints(i) < yPoints(i - 1)) Or (yPoints(i) > yPoints(i + 1) And yPoints(i) > yPoints(i - 1)) Then
                        metatopish = 0
                    ElseIf xPoints(i) > xPoints(i + 1) And xPoints(i) > xPoints(i - 1) Then
                        metatopish = sText.Length * 0.2
                        yDiff = 0.14
                    End If

                End If

                ' topotheto to simeio simfwna me olous tous parametrous
                oGeneralNote = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTG.CreatePoint2d(((xPoints(i) / 10) - (oLength / 2)) * ViewScale1 + xP + xDiff + metatopish, ((yPoints(i) / 10) - (oWidth / 2)) * ViewScale1 + yP + yDiff), sText)

            End If



            If i <> 0 Then
                Telitsa(myApplication, oSheet, oTG, ((xPoints(i) / 10) - (oLength / 2)) * ViewScale1 + xP, ((yPoints(i) / 10) - (oWidth / 2)) * ViewScale1 + yP)
            End If


        Next


    End Sub






    Public Sub DWG_52(oSavePath1 As String, myApplication As Application, V2_Konto As Boolean, V2_Psilo As Boolean, Poiotiko42 As Boolean, Adynato3D2 As Boolean, DWG_maxY_2 As Double, oSheet As Sheet, oLength2 As Double, oWidth2 As Double, NumOfPoints As Integer, D1 As Double, D2 As Double, gwnia As Double, xPointsValues22 As Double(), yPointsValues22 As Double(), maxSeires As Integer, xPoints22 As Double(), yPoints22 As Double())



        Dim oSaveName1 As String
        Dim sPartPath As String
        If Not (V2_Konto Or V2_Psilo Or Poiotiko42) Then
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
        Dim xP As Double = 28
        Dim yP As Double = 21.7

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
        If Not (Poiotiko42 Or V2_Konto Or V2_Psilo) Then
            oTitlePoint1 = myApplication.TransientGeometry.CreatePoint2d(xP - 3.05, yP + oWidth2 * ViewScale2 / 2 + 1.1)
        Else
            oTitlePoint1 = myApplication.TransientGeometry.CreatePoint2d(xP - 3.05, yP + oWidth2 * ViewScale2 / 2 + 1.05)
        End If
        oGeneralNoteTitles = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTitlePoint1, "ΑΝΑΠΤΥΓΜΑ ΥΠΟ ΓΩΝΙΑ ΚΥΛΙΝΔΡΟΥ (2)")
        oGeneralNoteTitles.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & "ΑΝΑΠΤΥΓΜΑ ΥΠΟ ΓΩΝΙΑ ΚΥΛΙΝΔΡΟΥ (2)" & "</StyleOverride>"



        If Poiotiko42 Then
            Dim oTitlePoint2 As Point2d
            oTitlePoint2 = myApplication.TransientGeometry.CreatePoint2d(28 - 3.44 + 2.27, yP + oWidth2 * ViewScale2 / 2 + 1.6)
            oGeneralNoteTitles = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTitlePoint2, "Π Ο Ι Ο Τ Ι Κ Ο")
            oGeneralNoteTitles.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & "Π Ο Ι Ο Τ Ι Κ Ο" & "</StyleOverride>"
        End If




        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' Katskeyh Systima Aksonwn 
        Dim oTG As TransientGeometry
        oTG = myApplication.TransientGeometry

        Dim xAxis, yAxis As Double
        xAxis = -(oLength2 / 2)
        yAxis = -(oWidth2 / 2)

        AxisSystem(myApplication, oView1, oTG, xAxis, yAxis, ViewScale2)




        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' orizontio katheto dimension 

        Dim oriz, kath As Integer
        oriz = oView1.DrawingCurves.Count - 10
        kath = 2



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
        Dim maxYpsosCm As Double = DWG_maxY_2
        Dim cutGap As Double
        cutGap = 0.3

        If (V2_Konto Or V2_Psilo) And Not (Poiotiko42) Then

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

            If Not (V2_Konto Or V2_Psilo) Or Poiotiko42 Then  '' and not konto

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

        EpiloghArithmwn_52_Diaforetika_Diam(myApplication, NumOfPoints, ViewScale2, xP, yP, xPointsValues22, yPointsValues22, oSheet, oLength2, oWidth2)



        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' Table 
        Dim mposiko As Double
        Dim TotalHeight As Double = DWG_maxY_2 'yPoints22(NumOfPoints / 4)
        mposiko = (10 - TotalHeight * ViewScale2 / 10) / 4
        If V2_Konto Or V2_Psilo Or Poiotiko42 Then
            mposiko = 0
        End If
        Dim xTablePosition As Double
        xTablePosition = xP - 5
        Dim yTablePosition As Double
        yTablePosition = yP - 6.5 + mposiko
        Dim xTableSpace As Double = 5.2    'se cm einai 4.8cm o prwtos kai o deyteros tha mpei praktika sto +0.2 cm 

        Table_Construction(myApplication, oSheet, maxSeires, NumOfPoints + 1, xPoints22, yPoints22, xTablePosition, yTablePosition, xTableSpace)
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''





        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' 3D Model View ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        'kleinw to proigoumeno doc tou flat pattern
        oPartDoc.Close()

        'kai an den einai poiotiko proxwraw
        If Not Adynato3D2 Then

            oSaveName1 = "test_V2_3D" & ".ipt"
            sPartPath = oSavePath1 & oSaveName1
            Dim oPartDoc_3D1 As PartDocument = myApplication.Documents.Open(sPartPath, True)

            ' Create a new NameValueMap object
            Dim oBaseViewOptions2 As NameValueMap
            oBaseViewOptions2 = myApplication.TransientObjects.CreateNameValueMap
            ' Set the options to use when creating the base view.
            oBaseViewOptions2.Add("IncludeSurfaceBodies", False)

            ' Now we define the placement points for
            ' the two drawing views we shall be adding to the sheet
            Dim xP2 As Double = 38.1
            Dim yP2 As Double = 11.5
            Dim oPlacementPoint2 As Point2d
            oPlacementPoint2 = myApplication.TransientGeometry.CreatePoint2d(xP2, yP2)

            ' define the view orientation for each view
            Dim ViewOrientation2 As ViewOrientationTypeEnum = ViewOrientationTypeEnum.kIsoTopRightViewOrientation

            ' define the view style for each view
            Dim ViewStyle2 As DrawingViewStyleEnum = DrawingViewStyleEnum.kShadedDrawingViewStyle 'kShadedHiddenLineDrawingViewStyle

            ' now create our two views
            Dim oView2 As DrawingView
            oView2 = oSheet.DrawingViews.AddBaseView(oPartDoc_3D1, oPlacementPoint2, ViewScale2, ViewOrientation2, ViewStyle2, , , oBaseViewOptions2)



            Dim kath3D, diam3D As Integer
            If D1 = D2 Then
                kath3D = 2
                diam3D = oView2.DrawingCurves.Count - 5
            Else
                kath3D = 1
                diam3D = oView2.DrawingCurves.Count - 5
            End If

            ''''''' ypologizw tin gwnia pou prepei na exei to 3d
            Dim x1, x2, y1, y2, dx, dy, dgwnias As Double
            x1 = oView2.DrawingCurves.Item(kath3D).StartPoint.X
            y1 = oView2.DrawingCurves.Item(kath3D).StartPoint.Y
            x2 = oView2.DrawingCurves.Item(kath3D).EndPoint.X
            y2 = oView2.DrawingCurves.Item(kath3D).EndPoint.Y
            dx = Abs(x1 - x2)
            dy = Abs(y1 - y2)
            dgwnias = Math.Atan(dx / dy) * 180 / Math.PI

            oView2.RotateByAngle((180 + dgwnias) * Math.PI / 180)




            ''''''''''''''''''''''''''''''''''   vriskw ta  maxY_Sto_3D,  maxX_Sto_3D,  minX_Sto_3D
            Dim maxY_Sto_3D As Double = 0
            Dim maxX_Sto_3D As Double = 0
            Dim minX_Sto_3D As Double = 1000
            Dim yStart, yEnd, xStart, xEnd As Double
            For i = 1 To oView2.DrawingCurves.Count

                If oView2.DrawingCurves.Item(i).CurveType = CurveTypeEnum.kLineCurve Or oView2.DrawingCurves.Item(i).CurveType = CurveTypeEnum.kLineSegmentCurve Then
                    yStart = oView2.DrawingCurves.Item(i).StartPoint.Y
                    yEnd = oView2.DrawingCurves.Item(i).EndPoint.Y
                    xStart = oView2.DrawingCurves.Item(i).StartPoint.X
                    xEnd = oView2.DrawingCurves.Item(i).EndPoint.X
                End If


                If Max(yStart, yEnd) > maxY_Sto_3D Then
                    maxY_Sto_3D = Max(yStart, yEnd)
                End If

                If Max(xStart, xEnd) > maxX_Sto_3D Then
                    maxX_Sto_3D = Max(xStart, xEnd)
                End If

                If Min(xStart, xEnd) < minX_Sto_3D And Min(xStart, xEnd) > 30 Then
                    minX_Sto_3D = Min(xStart, xEnd)
                End If

            Next


            Dim X_Title_Center As Double = minX_Sto_3D + (maxX_Sto_3D - minX_Sto_3D) / 2
            Dim oTitlePoint3 As Point2d
            oTitlePoint3 = myApplication.TransientGeometry.CreatePoint2d(X_Title_Center - 2.15, oView2.Top + 0.8)
            oGeneralNoteTitles = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTitlePoint3, "3D ΜΟΝΤΕΛΟ ΟΡΙΖΟΝΤΙΟΥ")
            oGeneralNoteTitles.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextCenter
            oGeneralNoteTitles.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & "3D ΜΟΝΤΕΛΟ ΟΡΙΖΟΝΤΙΟΥ" & "</StyleOverride>"



            ''''
            Dim mikroYpsos As Double = Abs(oView2.DrawingCurves.Item(kath3D).StartPoint.Y - oView2.DrawingCurves.Item(kath3D).EndPoint.Y)
            Dim minY As Double = Min(oView2.DrawingCurves.Item(kath3D).StartPoint.Y, oView2.DrawingCurves.Item(kath3D).EndPoint.Y)

            ''''Dimension gia to katheto
            Dim finalCurve3 As DrawingCurve
            finalCurve3 = oView2.DrawingCurves.Item(kath3D)

            Dim oGeomIntent3 As GeometryIntent
            oGeomIntent3 = oSheet.CreateGeometryIntent(finalCurve3)

            Dim gwniaDimension As Double = Abs(gwnia - 45) / 45 * 30 * Math.PI / 180
            Dim textPoint3 As Inventor.Point2d
            If mikroYpsos > 4 Then
                If gwnia < 45 Then
                    textPoint3 = myApplication.TransientGeometry.CreatePoint2d(oView2.DrawingCurves.Item(kath3D).MidPoint.X - 1.3, oView2.DrawingCurves.Item(kath3D).MidPoint.Y - Math.Tan(gwniaDimension) * 1.3)
                Else
                    textPoint3 = myApplication.TransientGeometry.CreatePoint2d(oView2.DrawingCurves.Item(kath3D).MidPoint.X - 1.3, oView2.DrawingCurves.Item(kath3D).MidPoint.Y + Math.Tan(gwniaDimension) * 1.3)
                End If
            Else
                textPoint3 = myApplication.TransientGeometry.CreatePoint2d(oView2.DrawingCurves.Item(kath3D).MidPoint.X - 1.3, minY - 1.5)
            End If

            Dim myDim3DKatheto As GeneralDimension
            myDim3DKatheto = oGeneralDims.AddLinear(textPoint3, oGeomIntent3)

            'An einai poiotiko kryvw to lathos value kai vazw to swsto alliws prosthetw apla to (mm)
            'If Not (V2_Konto Or V2_Psilo) Then
            'myDim3DKatheto.Text.FormattedText = "<DimensionValue/>" & " mm"
            'Else
            'kryvw to lathos value kai dinw to swsto
            myDim3DKatheto.HideValue = True
            myDim3DKatheto.Text.FormattedText = Format(yPoints22(0), "0.00") & " mm"
            'End If





            '''''''Dimension gia thn diametro tou 3D object      den kserw an tha valw sigoura
            'Dim finalCurve4 As DrawingCurve
            'finalCurve4 = oView2.DrawingCurves.Item(diam3D)  ' gia thn diametro  (synolika exei 19)

            'Dim oGeomIntent4 As GeometryIntent
            'oGeomIntent4 = oSheet.CreateGeometryIntent(finalCurve4)


            'Dim yDim As Double
            'If D1 = D2 Then
            '    yDim = oView2.DrawingCurves.Item(kath3D).StartPoint.Y - 0.87
            'Else
            '    yDim = oView2.DrawingCurves.Item(kath3D).EndPoint.Y - 0.87
            'End If

            '' To curve  oView2.DrawingCurves.Item(2)  einai to xYpsosTomhs san curve object
            'Dim textPoint4 As Inventor.Point2d = myApplication.TransientGeometry.CreatePoint2d(xP2, yDim)    ''''  ,yP2 - 5)
            'Dim myDim3DOrizontio As GeneralDimension
            'myDim3DOrizontio = oGeneralDims.AddDiameter(textPoint4, oGeomIntent4)
            'myDim3DOrizontio.Text.FormattedText = "<DimensionValue/>" & " mm"



            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' TELOS 3D ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            ''''''''''''''''''''''''kleinw kai to part 
            oPartDoc_3D1.Close()



        Else

            Dim xP_3D As Double = 37.85
            Dim yP_3D As Double = 8.5


            Dim oTitlePoint3 As Point2d
            oTitlePoint3 = myApplication.TransientGeometry.CreatePoint2d(xP_3D - 2.25 - 0.77, yP_3D + 3.8)
            oGeneralNoteTitles = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTitlePoint3, "Η ΤΟΜΗ ΠΟΥ ΠΡΟΚΕΙΠΤΕΙ ΑΠΟ")
            oGeneralNoteTitles.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextCenter
            oGeneralNoteTitles.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & "Η ΤΟΜΗ ΠΟΥ ΠΡΟΚΕΙΠΤΕΙ ΑΠΟ" & "</StyleOverride>"


            Dim oTitlePoint4 As Point2d
            oTitlePoint4 = myApplication.TransientGeometry.CreatePoint2d(xP_3D - 2.27 - 0.175, yP_3D + 3.4)
            oGeneralNoteTitles = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTitlePoint4, "ΤΗΝ ΤΟΜΗ ΕΙΝΑΙ ΑΡΚΕΤΑ")
            oGeneralNoteTitles.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextCenter
            oGeneralNoteTitles.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & "ΤΗΝ ΤΟΜΗ ΕΙΝΑΙ ΑΡΚΕΤΑ" & "</StyleOverride>"


            Dim oTitlePoint5 As Point2d
            oTitlePoint5 = myApplication.TransientGeometry.CreatePoint2d(xP_3D - 2.27 - 0.347, yP_3D + 3)
            oGeneralNoteTitles = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTitlePoint5, "ΣΤΕΝΟΜΑΚΡΗ ΚΑΙ ΚΑΘΙΣΤΑ")
            oGeneralNoteTitles.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextCenter
            oGeneralNoteTitles.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & "ΣΤΕΝΟΜΑΚΡΗ ΚΑΙ ΚΑΘΙΣΤΑ" & "</StyleOverride>"

            Dim oTitlePoint6 As Point2d
            oTitlePoint6 = myApplication.TransientGeometry.CreatePoint2d(xP_3D - 2.27 - 0.44, yP_3D + 2.6)
            oGeneralNoteTitles = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTitlePoint6, "ΜΗ ΠΡΟΣΙΤΗ ΤΗΝ ΠΡΟΒΟΛΗ")
            oGeneralNoteTitles.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextCenter
            oGeneralNoteTitles.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & "ΜΗ ΠΡΟΣΙΤΗ ΤΗΝ ΠΡΟΒΟΛΗ" & "</StyleOverride>"

            Dim oTitlePoint7 As Point2d
            oTitlePoint7 = myApplication.TransientGeometry.CreatePoint2d(xP_3D - 2.27 + 0.407, yP_3D + 2.2)
            oGeneralNoteTitles = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTitlePoint7, "ΤΟΥ 3D ΜΟΝΤΕΛΟΥ")
            oGeneralNoteTitles.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextCenter
            oGeneralNoteTitles.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & "ΤΟΥ 3D ΜΟΝΤΕΛΟΥ" & "</StyleOverride>"

        End If

    End Sub



    Public Sub EpiloghArithmwn_52_Diaforetika_Diam(myApplication As Application, ByVal NumOfPoints As Integer, ByVal ViewScale1 As Double, xP As Double, yP As Double, ByVal xPoints() As Double, ByVal yPoints() As Double, ByRef oSheet As Sheet, oLength As Double, oWidth As Double)


        Dim oTG As TransientGeometry
        oTG = myApplication.TransientGeometry


        Dim sText As String
        Dim oGeneralNote As GeneralNote
        Dim xDiff As Double = 0
        Dim yDiff As Double = 0.35

        For i = 0 To NumOfPoints

            Dim b20, b24, b28, b32, b36, b40, b44, b48 As Boolean
            b20 = NumOfPoints = 20 And (i = 0 Or i = 3 Or i = 5 Or i = 7 Or i = 10 Or i = 13 Or i = 15 Or i = 17 Or i = 20)
            b24 = NumOfPoints = 24 And (i Mod 2 = 0)
            b28 = NumOfPoints = 28 And (i = 0 Or i = 4 Or i = 7 Or i = 10 Or i = 14 Or i = 18 Or i = 21 Or i = 24 Or i = 28)
            b32 = NumOfPoints = 32 And (i = 0 Or i = 3 Or i = 5 Or i = 8 Or i = 11 Or i = 13 Or i = 16 Or i = 19 Or i = 21 Or i = 24 Or i = 26 Or i = 29 Or i = 32)
            b36 = NumOfPoints = 36 And (i Mod 3 = 0)
            b40 = NumOfPoints = 40 And (i = 0 Or i = 4 Or i = 7 Or i = 10 Or i = 13 Or i = 16 Or i = 20 Or i = 24 Or i = 27 Or i = 30 Or i = 33 Or i = 36 Or i = 40)
            b44 = NumOfPoints = 44 And (i = 0 Or i = 4 Or i = 7 Or i = 11 Or i = 15 Or i = 18 Or i = 22 Or i = 26 Or i = 29 Or i = 33 Or i = 37 Or i = 40 Or i = 44)
            b48 = NumOfPoints = 48 And (i Mod 4 = 0)


            sText = i + 1
            xDiff = -sText.Length * 0.1

            '''' vazw ta volika simeia
            If (NumOfPoints < 20 Or b20 Or b24 Or b28 Or b32 Or b36 Or b40 Or b44 Or b48) Then



                Dim metatopish As Double = 0
                ''''''''''''''' vriksw tin metatopisi tou x symfwna me ton rythmo metavolhs ( mono sta mh simeia kampis)
                If Not (i = 0 Or i = NumOfPoints) Then

                    Dim gwnia As Double
                    Dim dx, dy As Double

                    ''''''''''''' ypologizw thn metatopish
                    If i = NumOfPoints / 4 Then ' ayksanei

                        dx = xPoints(i + 1) - xPoints(i - 1)
                        dy = yPoints(i + 1) - yPoints(i - 1)
                        gwnia = Math.Atan(dy / dx) * 180 / PI
                        metatopish = -((gwnia / 90) / 10) * sText.Length - 0.015
                        If gwnia > 40 Then
                            metatopish = metatopish - 0.015
                        End If

                    ElseIf i = NumOfPoints * 3 / 4 Then ' meiwnei

                        dx = Abs(xPoints(i - 1) - xPoints(i + 1))
                        dy = Abs(yPoints(i - 1) - yPoints(i + 1))
                        gwnia = Math.Atan(dy / dx) * 180 / PI
                        metatopish = ((gwnia / 90) / 10) * sText.Length + 0.015
                        If gwnia > 40 Then
                            metatopish = metatopish + 0.015
                        End If

                    ElseIf yPoints(i) < yPoints(i + 1) Then  '''''' an ayksanei me noiazei ws pros to epomeno dy

                        dx = xPoints(i + 1) - xPoints(i)
                        dy = yPoints(i + 1) - yPoints(i)
                        gwnia = Math.Atan(dy / dx) * 180 / PI
                        metatopish = -((gwnia / 90) / 10) * sText.Length - 0.015
                        If gwnia > 40 Then
                            metatopish = metatopish - 0.015
                        End If

                    Else ''''' meiwnei me noiazei ws pros to proigoymeno dy

                        dx = Abs(xPoints(i - 1) - xPoints(i))
                        dy = Abs(yPoints(i - 1) - yPoints(i))
                        gwnia = Math.Atan(dy / dx) * 180 / PI
                        metatopish = ((gwnia / 90) / 10) * sText.Length + 0.015
                        If gwnia > 40 Then
                            metatopish = metatopish + 0.015
                        End If

                    End If

                End If


                ' topotheto to simeio simfwna me olous tous parametrous
                oGeneralNote = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTG.CreatePoint2d(((xPoints(i) / 10) - (oLength / 2)) * ViewScale1 + xP + xDiff + metatopish, ((yPoints(i) / 10) - (oWidth / 2)) * ViewScale1 + yP + yDiff), sText)

            End If


        Next


    End Sub



    Public Sub Ypomnima5(ByVal xPos As Double, ByVal ViewScale1 As Double, ByVal poiotiko1 As Boolean, ByVal Poiotiko52 As Boolean, ByVal ViewScale2 As Double, ByVal Default_Micro_Path As String, ByVal oTg As TransientGeometry, ByRef oSketchYpomnima As DrawingSketch)

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
        sText = "Υπό Γωνία Κύλινδροι Με Απόσταση Στους Άξονες"
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
        If poiotiko1 Then
            sText = "Κλίμακα 1: " & "Μη Αναλογική"
        Else
            sText = "Κλίμακα 1:  " & "  1 : " & Format(10 / ViewScale1, "0.00")
        End If

        oTextBox = oSketchYpomnima.TextBoxes.AddFitted(oTg.CreatePoint2d(xPos + 14.06, 1.515), sText)
        oTextBox.VerticalJustification = VerticalTextAlignmentEnum.kAlignTextUpper
        oTextBox.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextLeft
        oTextBox.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & sText & "</StyleOverride>"


        'View scale 
        If Poiotiko52 Then
            sText = "Κλίμακα 2: " & "Μη Αναλογική"
        Else
            sText = "Κλίμακα 2:  " & "  1 : " & Format(10 / ViewScale2, "0.00")
        End If

        oTextBox = oSketchYpomnima.TextBoxes.AddFitted(oTg.CreatePoint2d(xPos + 14.06, 0.615), sText)
        oTextBox.VerticalJustification = VerticalTextAlignmentEnum.kAlignTextUpper
        oTextBox.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextLeft
        oTextBox.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & sText & "</StyleOverride>"

    End Sub





End Module
