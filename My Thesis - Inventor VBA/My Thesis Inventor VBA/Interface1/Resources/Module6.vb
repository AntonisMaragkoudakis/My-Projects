Imports System.Math
Imports Inventor

Module Module6







    Public Sub Vres_Min_Ypsos1(D1 As Double, D2 As Double, safe_Ypsos1 As Double, safe_Ypsos2 As Double, gwnia As Double, YpsosTomhs As Double, Dx As Double, NumOfPoints As Double, ByRef min_Ypsos1 As Double)

        Dim partDoc As PartDocument
        Dim partComp As SheetMetalComponentDefinition
        Dim Rip_precision As Double

        Dim myApplication As Inventor.Application
        myApplication = GetObject(, "Inventor.Application")
        myApplication.Documents.CloseAll()
        myApplication.Documents.Add(Inventor.DocumentTypeEnum.kPartDocumentObject, myApplication.FileManager.GetTemplateFile(Inventor.DocumentTypeEnum.kPartDocumentObject), True)

        partDoc = myApplication.ActiveDocument
        partDoc.SubType = "{9C464203-9BAE-11D3-8BAD-0060B0CE6BB4}"
        partComp = partDoc.ComponentDefinition


        Dim DiaforaGwniasRad As Double = 0
        '3D kataskeyh
        Model_51_3D_Construction(myApplication, D1, safe_Ypsos1, D2, safe_Ypsos2, YpsosTomhs, Dx, gwnia, partDoc, partComp, Rip_precision, DiaforaGwniasRad)


        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' TELOS 3D MONTELOU 31 ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' FLAT PATTERN CODE AND ROTATE 31 ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        ' A Side Definition '''' To swsto face exei mallon ( oUnitNormal.X = 1 and oUnitNormal.Y = 0 and oUnitNormal.Z = 0 )
        Dim TheFaceCounter As Integer

        'Kalw synartish gia unfold sto katalilo face
        UnfoldTheFace(myApplication, partComp, TheFaceCounter)

        'Exwntas kanei Unfold Pianw to antikimeno toy FlatPattern
        Dim fp As FlatPattern = myApplication.ActiveEditObject

        '''''Kanw Rotate to Fp an xriazete kai epistrefw ta oLength, oWidth kai to an telika peristrafike
        Dim oLength As Double
        Dim oWidth As Double
        Dim peristrafike As Integer

        'Kanw Rotate To Fp an xriazete kai epistrefw ta oLength, oWidth kai to an telika peristrafike
        Rotate_Fp51(partComp, Rip_precision, D1, safe_Ypsos1, fp, peristrafike, oLength, oWidth)
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' YPOLOGISMOS EDGES 31 ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        'Pianw ta Edges tou face. Tha mporousa na ta epilegw kai apo tin Epilogh_edges(oEdges,....)
        Dim fpBody As SurfaceBody = fp.SurfaceBodies(1)
        Dim oEdges As Edges = fpBody.Edges

        '''' gia d1 != d2 ta edges 12 kai 14 kanoyn
        Dim myEdge1 As Edge
        myEdge1 = oEdges.Item(12)


        Dim minX, maxX, minY, maxY As Double
        Ypologismos_Min_Max_51(myEdge1, oLength, NumOfPoints, peristrafike, safe_Ypsos1, YpsosTomhs, minX, maxX, minY, maxY)


        min_Ypsos1 = maxY


        Dim oSavePath1 As String = "C:\Inventor Saves\Inventor Saves Hidden\"
        Dim oSaveName1 As String = "skoupidi" & ".ipt"
        Dim sPartPath As String = oSavePath1 & oSaveName1
        Restart(partComp, partDoc, myApplication, oSavePath1, oSaveName1)

    End Sub



    Public Sub Vres_Min_Ypsos2(D1 As Double, D2 As Double, safe_Ypsos1 As Double, safe_Ypsos2 As Double, gwnia As Double, YpsosTomhs As Double, Dx As Double, NumOfPoints As Double, ByRef min_Ypsos2 As Double, ByVal Ypsos2 As Double)

        Dim partDoc As PartDocument
        Dim partComp As SheetMetalComponentDefinition
        Dim Rip_precision As Double

        Dim myApplication As Inventor.Application
        myApplication = GetObject(, "Inventor.Application")
        myApplication.Documents.CloseAll()
        myApplication.Documents.Add(Inventor.DocumentTypeEnum.kPartDocumentObject, myApplication.FileManager.GetTemplateFile(Inventor.DocumentTypeEnum.kPartDocumentObject), True)

        partDoc = myApplication.ActiveDocument
        partDoc.SubType = "{9C464203-9BAE-11D3-8BAD-0060B0CE6BB4}"
        partComp = partDoc.ComponentDefinition




        '''''' 3D Model
        Model_52_3D_Construction(myApplication, D1, safe_Ypsos1, D2, safe_Ypsos2, YpsosTomhs, Dx, gwnia, partDoc, partComp, Rip_precision)
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' FLAT PATTERN CODE AND ROTATE 32 ''''''''''''''''''''''''''''''''''''''''''''''''''
        ''''Kalw synartish gia unfold sto katalilo face
        Dim TheFaceCounter As Integer
        UnfoldTheFaceOrizontiou(myApplication, partComp, TheFaceCounter)

        ''''Exwntas kanei Unfold Pianw to antikimeno toy FlatPattern
        Dim fp As FlatPattern
        fp = myApplication.ActiveEditObject



        '''''''''''''''''''''''''''''''''''' vriskw to Ypsos tis katheths grammhs sto curveLength
        Dim fpBody As SurfaceBody
        Dim oEdges As Edges
        fpBody = fp.SurfaceBodies(1)
        oEdges = fpBody.Edges
        '''''' Get the parametric range of the curve.
        Dim dMinParam As Double
        Dim dMaxParam As Double
        Dim curveLength As Double

        Dim myEdge2 As Edge
        myEdge2 = oEdges.Item(1)
        myEdge2.Evaluator.GetParamExtents(dMinParam, dMaxParam)
        myEdge2.Evaluator.GetLengthAtParam(dMinParam, dMaxParam, curveLength)
        curveLength = curveLength * 10


        Dim peristrafike2 As Boolean = False
        Dim oLength2 As Double
        Dim oWidth2 As Double
        '''''Kanw Rotate To Fp an xriazete kai epistrefw ta oLength, oWidth kai to an telika peristrafike
        RotateCase32(partComp, Rip_precision, D2, D1, curveLength, peristrafike2, fp, oLength2, oWidth2)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' YPOLOGISMOS EDGES 32 ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ''''' Edw h listes den einai num - 1 giati exoume kai to teleytaio
        Dim xPoints22(NumOfPoints) As Double
        Dim yPoints22(NumOfPoints) As Double
        Dim Edge1 As Edge = oEdges.Item(13)
        Dim Edge2 As Edge = oEdges.Item(9)

        ' o ypologismos linei aytomata kai to provlima tou 2 * ypsosTomhs
        Dim provlima As Boolean = False
        Ypologismos_Edge_52(Edge1, Edge2, NumOfPoints + 1, peristrafike2, curveLength, xPoints22, yPoints22, provlima)

        'edw kanw me stathero vima sto x ton ypologismo (malon den xriazete edw)
        'Ypolgogismos_52_Symetrikos(oEdges, NumOfPoints, curveLength, peristrafike2, xPoints22, yPoints22, provlima)

        '''''' Ypologizw kai to max ypsos tou deyterou anaptygmatos
        Dim maxY_2, minY_2 As Double
        MaxY_2_ypologismos_52(oEdges, peristrafike2, curveLength, maxY_2, minY_2, provlima)


        min_Ypsos2 = safe_Ypsos2 - minY_2


        Dim oSavePath1 As String = "C:\Inventor Saves\Inventor Saves Hidden\"
        Dim oSaveName1 As String = "skoupidi2" & ".ipt"
        Dim sPartPath As String = oSavePath1 & oSaveName1
        Restart(partComp, partDoc, myApplication, oSavePath1, oSaveName1)

    End Sub








    Public Sub Rotate_Fp51(ByVal partComp As SheetMetalComponentDefinition, ByVal Rip_precision As Double, ByVal Diam As Double, ByVal YpsosStoY As Double, ByVal fp As FlatPattern, ByRef peristrafike As Integer, ByRef oLength As Double, ByRef oWidth As Double)


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
            'fp.FlatPatternOrientations.ActiveFlatPatternOrientation.FlipBaseFace = True
            peristrafike = 1
        Else

            'fp.FlatPatternOrientations.ActiveFlatPatternOrientation.FlipBaseFace = True
            fp.FlatPatternOrientations.ActiveFlatPatternOrientation.FlipAlignmentAxis = False
            peristrafike = 2
        End If



        ' oLength einai to max sto x axis tou flatt pattern--> na einai Diametros *pi  .....  oWidth einai to max sto y axis tou flat pattern --> na einai max ypsos
        oLength = partComp.FlatPattern.Length
        oWidth = partComp.FlatPattern.Width

    End Sub



    Public Sub DenEkanaFlipAxis(NumOfPoints As Integer, oLength As Double, Ypsos1 As Double, ByRef xPoints() As Double, ByRef yPoints() As Double, ByVal peristrafike As Integer)

        If peristrafike = 1 Then
            For i = 0 To NumOfPoints - 1
                xPoints(i) += oLength * 10
                yPoints(i) -= Ypsos1
            Next
            '''''''ftiaxnw thn seira
            Dim Temp_xPoints(NumOfPoints - 1) As Double
            Dim Temp_yPoints(NumOfPoints - 1) As Double
            For i = 0 To NumOfPoints - 1
                Temp_xPoints(i) = xPoints(i)
                Temp_yPoints(i) = yPoints(i)
            Next
            For i = 0 To NumOfPoints - 1
                If i <= NumOfPoints / 2 Then
                    xPoints(i) = Temp_xPoints(Abs(i - NumOfPoints / 2))
                    yPoints(i) = Temp_yPoints(Abs(i - NumOfPoints / 2))
                Else
                    xPoints(i) = Temp_xPoints(NumOfPoints - Abs(i - NumOfPoints / 2))
                    yPoints(i) = Temp_yPoints(NumOfPoints - Abs(i - NumOfPoints / 2))
                End If
            Next
        Else

            For i = 0 To NumOfPoints - 1
                yPoints(i) -= 2 * Ypsos1
            Next

        End If





    End Sub







    Sub Model_51_3D_Construction_2(ByVal myApplication As Inventor.Application, ByVal d1 As Double, ByVal Ypsos1 As Double, ByVal d2 As Double, ByVal Ypsos2 As Double, ByVal YpsosTomhs As Double, ByVal Dx As Double, ByVal gwnia As Double, ByRef partDoc As PartDocument, ByRef partComp As SheetMetalComponentDefinition, ByRef Rip_precision As Double, DiaforaGwniasRad As Double)


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
        oExtrudeDef1.SetDistanceExtent(Ypsos1 / 10, PartFeatureExtentDirectionEnum.kPositiveExtentDirection)
        Dim oExtrude1 As ExtrudeFeature
        oExtrude1 = partComp.Features.ExtrudeFeatures.Add(oExtrudeDef1)

        '' Voithitiko plane gia arnitiko Ypsos tomhs
        Dim oPlaneYpsousTomhs As WorkPlane
        oPlaneYpsousTomhs = partComp.WorkPlanes.AddByPlaneAndOffset(partComp.WorkPlanes(2), YpsosTomhs / 10)
        Dim skYpsousTomhs As PlanarSketch = partComp.Sketches.Add(oPlaneYpsousTomhs)
        Dim myLine As SketchLine
        myLine = skYpsousTomhs.SketchLines.AddByTwoPoints(myApplication.TransientGeometry.CreatePoint2d(0, 0), myApplication.TransientGeometry.CreatePoint2d(0, d2 / 100))



        ''''''''Teliko Plane Gwnias Default stis 90
        Dim oPlane As WorkPlane
        oPlane = partComp.WorkPlanes.AddByLinePlaneAndAngle(myLine, oPlaneYpsousTomhs, Abs(gwnia - 90) * Math.PI / 180)


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
        sk3.SketchLines.AddAsTwoPointCenteredRectangle(myApplication.TransientGeometry.CreatePoint2d((Ypsos1 / 2) / 10, 0), myApplication.TransientGeometry.CreatePoint2d((Ypsos1) / 10 + 0.1, Rip_precision))
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








    Public Sub Emfanisi_axis_syntetagmenes(myApplication As Application, oSheet As Sheet, oGeneralNoteTitles As GeneralNote, xP As Double, yP As Double, axis_X As Double, axis_Y As Double)

        Dim oTitlePoint_axis_1 As Point2d
        oTitlePoint_axis_1 = myApplication.TransientGeometry.CreatePoint2d(xP, yP - 0.4)
        oGeneralNoteTitles = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTitlePoint_axis_1, "TΟ ΚΕΝΤΡΟ ΤΟΥ ΑΞΟΝΑ ΣΕ ΣΧΕΣΗ")
        oGeneralNoteTitles.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextCenter
        oGeneralNoteTitles.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & "TΟ ΚΕΝΤΡΟ ΤΟΥ ΑΞΟΝΑ ΣΕ ΣΧΕΣΗ" & "</StyleOverride>"

        Dim oTitlePoint_axis_2 As Point2d
        oTitlePoint_axis_2 = myApplication.TransientGeometry.CreatePoint2d(xP + 0.01, yP - 0.8)
        oGeneralNoteTitles = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTitlePoint_axis_2, "ΜΕ ΤΟ ΚΑΤΩ ΑΡΙΣΤΕΡΟ ΑΚΡΟ ΤΟΥ")
        oGeneralNoteTitles.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextCenter
        oGeneralNoteTitles.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & "ΜΕ ΤΟ ΚΑΤΩ ΑΡΙΣΤΕΡΟ ΑΚΡΟ ΤΟΥ" & "</StyleOverride>"

        Dim oTitlePoint_axis_3 As Point2d
        oTitlePoint_axis_3 = myApplication.TransientGeometry.CreatePoint2d(xP + 0.09, yP - 1.2)
        oGeneralNoteTitles = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTitlePoint_axis_3, "ΑΝΑΠΤΥΓΜΑΤΟΣ, ΒΡΙΣΚΕΤΑΙ ΣΤΟ")
        oGeneralNoteTitles.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextCenter
        oGeneralNoteTitles.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & "ΑΝΑΠΤΥΓΜΑΤΟΣ, ΒΡΙΣΚΕΤΑΙ ΣΤΟ" & "</StyleOverride>"



        Dim txt1, txt2 As String
        txt1 = Format(axis_X, "0.00")
        txt2 = Format(axis_Y, "0.00")

        Dim size1, size2, total As Integer
        size1 = txt1.Length - 1
        size2 = txt2.Length - 1
        total = size1 + size2


        Dim mikos_mm As Double = 11.5 + total * 2
        Dim diafora_mm As Double = 26.7 - mikos_mm / 2

        Dim oTitlePoint_axis_4 As Point2d
        oTitlePoint_axis_4 = myApplication.TransientGeometry.CreatePoint2d(xP + diafora_mm / 10, yP - 1.6)
        oGeneralNoteTitles = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTitlePoint_axis_4, "(" & txt1 & "," & txt2 & ") mm")
        oGeneralNoteTitles.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextCenter
        oGeneralNoteTitles.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & "(" & txt1 & "," & txt2 & ") mm" & "</StyleOverride>"



    End Sub




    Public Sub Telitsa(ByVal myApplication As Application, ByVal oSheet As Sheet, ByVal oTG As TransientGeometry, ByVal xp As Double, ByVal yp As Double)

        Dim BlackColor As Color
        BlackColor = myApplication.TransientObjects.CreateColor(0, 0, 0)

        Dim AxisSketch As DrawingSketch = oSheet.Sketches.Add
        AxisSketch.Edit()

        AxisSketch.SketchCircles.AddByCenterRadius(myApplication.TransientGeometry.CreatePoint2d(xp, yp), 0.05)
        Dim oProfileCircle As Profile
        oProfileCircle = AxisSketch.Profiles.AddForSolid
        Dim fillBlack As SketchFillRegion
        fillBlack = AxisSketch.SketchFillRegions.Add(oProfileCircle, BlackColor)

        AxisSketch.ExitEdit()

    End Sub




End Module
