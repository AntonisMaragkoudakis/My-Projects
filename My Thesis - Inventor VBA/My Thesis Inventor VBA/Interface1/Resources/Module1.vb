Imports Inventor
Imports System.Math
Imports System.Text
Module Module1




    Sub Model_1_3D_Construction(ByVal myApplication As Inventor.Application, ByVal Diam As Double, ByVal Ypsos As Double, ByVal YpsosTomhs As Double, ByVal Gwnia As Double, ByRef partDoc As PartDocument, ByRef partComp As SheetMetalComponentDefinition, ByRef Rip_precision As Double)


        Dim unitsOM As UnitsOfMeasure
        unitsOM = partDoc.UnitsOfMeasure
        unitsOM.LengthUnits = Inventor.UnitsTypeEnum.kMillimeterLengthUnits
        unitsOM.AngleUnits = Inventor.UnitsTypeEnum.kDegreeAngleUnits
        'unitsOM.LengthDisplayPrecision = 8
        'unitsOM.AngleDisplayPrecision = 8

        '''''''''''''''''''Ypsos Tomhs = 0
        If YpsosTomhs = 0 Then
            YpsosTomhs = 0.0001
        End If

        '''' Surface Solina
        Dim sk1 As PlanarSketch = partComp.Sketches.Add(partComp.WorkPlanes.Item(2))
        sk1.SketchCircles.AddByCenterRadius(myApplication.TransientGeometry.CreatePoint2d(0, 0), Diam / 20)
        Dim oProfile1 As Profile
        oProfile1 = sk1.Profiles.AddForSurface

        Dim oExtrudeDef1 As ExtrudeDefinition
        oExtrudeDef1 = partComp.Features.ExtrudeFeatures.CreateExtrudeDefinition(oProfile1, PartFeatureOperationEnum.kSurfaceOperation)
        oExtrudeDef1.SetDistanceExtent(Ypsos / 10, PartFeatureExtentDirectionEnum.kPositiveExtentDirection)
        Dim oExtrude1 As ExtrudeFeature
        oExtrude1 = partComp.Features.ExtrudeFeatures.Add(oExtrudeDef1)

        'Boithitiko Plane
        Dim oPlane1 As WorkPlane
        oPlane1 = partComp.WorkPlanes.AddByPlaneAndOffset(partComp.WorkPlanes.Item(2), YpsosTomhs / 10)
        'Boithitikh gramh
        Dim sk2 As PlanarSketch = partComp.Sketches.Add(oPlane1)
        sk2.SketchLines.AddByTwoPoints(myApplication.TransientGeometry.CreatePoint2d(Diam / 20, 0), myApplication.TransientGeometry.CreatePoint2d(Diam / 20, Diam / 20))
        Dim oLine As SketchLine
        oLine = partComp.Sketches.Item(2).SketchLines.Item(1)
        sk2.Visible = False

        ''''''''Teliko Plane Gwnias
        Dim oPlane2 As WorkPlane
        oPlane2 = partComp.WorkPlanes.AddByLinePlaneAndAngle(oLine, oPlane1, (Gwnia * Math.PI / 180))



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
        oThicknessParam.Value = oThicknessParam.Value * 0.1
        'MessageBox.Show(oThicknessParam.Value)


        '''''''' Split kai Thicken
        Dim oSplit1 As SplitFeature
        If (Gwnia = "90") Then
            oSplit1 = partDoc.ComponentDefinition.Features.SplitFeatures.SplitFaces(oPlane1, True, partComp.WorkSurfaces.Item(1))
        Else
            oSplit1 = partDoc.ComponentDefinition.Features.SplitFeatures.SplitFaces(oPlane2, True, partComp.WorkSurfaces.Item(1))
        End If

        Dim oFaceColl1 As FaceCollection
        oFaceColl1 = myApplication.TransientObjects.CreateFaceCollection
        oFaceColl1.Add(partComp.WorkSurfaces.Item(1)._SurfaceBody.Faces.Item(2))

        Dim oThicken1 As ThickenFeature
        oThicken1 = partComp.Features.ThickenFeatures.Add(oFaceColl1, "0.05", PartFeatureExtentDirectionEnum.kPositiveExtentDirection, PartFeatureOperationEnum.kNewBodyOperation)
        '''''''''''''''''''''


        ' Rip precicion ( Ousiastika epilegw oso to dynaton mikrotero rip symfwna me thn Diametro ) 
        If Diam < 10 Then
            Rip_precision = Diam * 0.00001
        ElseIf Diam < 100 Then
            Rip_precision = Diam * 0.000001
        ElseIf Diam < 10000 Then
            Rip_precision = Diam * 0.0000001
        ElseIf Diam < 100000 Then
            Rip_precision = Diam * 0.00000005
        Else
            Rip_precision = Diam * 0.00000001
        End If
        '' (san sketch h feta pou epitrepei ston swlina na ksediplwsei)
        Dim sk3 As PlanarSketch = partComp.Sketches.Add(partComp.WorkPlanes.Item(1))
        sk3.SketchLines.AddAsTwoPointCenteredRectangle(myApplication.TransientGeometry.CreatePoint2d(Ypsos / 20, 0), myApplication.TransientGeometry.CreatePoint2d(0, Rip_precision))
        Dim oProfile3 As Profile
        oProfile3 = sk3.Profiles.AddForSolid
        'Kanw cut thn feta ayth
        Dim oExtrudeDef3 As ExtrudeDefinition
        oExtrudeDef3 = partComp.Features.ExtrudeFeatures.CreateExtrudeDefinition(oProfile3, PartFeatureOperationEnum.kCutOperation)
        oExtrudeDef3.SetDistanceExtent(Diam / 20, PartFeatureExtentDirectionEnum.kNegativeExtentDirection)
        Dim oExtrude3 As ExtrudeFeature
        oExtrude3 = partComp.Features.ExtrudeFeatures.Add(oExtrudeDef3)


        'Visibilities off
        partDoc.ObjectVisibility.UserWorkPlanes = False
        partDoc.ObjectVisibility.ConstructionSurfaces = False

    End Sub







    Public Sub Eikona_3D(ByVal myApplication As Inventor.Application, ByVal oSavePath As String, ByVal folder As String, ByRef PictureBox3 As Windows.Forms.PictureBox, ByRef tries As Integer)

        If Dir(oSavePath) = "" Then
            System.IO.Directory.CreateDirectory(oSavePath)
        End If


        If Dir(folder) = "" Then
            System.IO.Directory.CreateDirectory(folder)
        End If

        Dim m_Camera As Inventor.Camera
        m_Camera = myApplication.ActiveView.Camera
        'm_Camera.SceneObject = fp
        myApplication.DisplayOptions.Show3DIndicator = False

        'partDoc.DisplayName = "Test"
        'Dim oFileName As String = Trim(Mid(partDoc.FullFileName, InStrRev(partDoc.FullFileName, "\", -1) + 1, Len(partDoc.FullFileName) - InStrRev(partDoc.FullFileName, "\", -1) - 4))
        Dim oIptFileName As String = "Test_ipt"

        m_Camera.ViewOrientationType = 10760
        m_Camera.Fit()
        m_Camera.ApplyWithoutTransition()

        tries = 0
        Dim camError As Boolean = False
        Try
            'FileSystem.Kill("C:\Inventor Saves\Inventor VBA\*.*")
            m_Camera.SaveAsBitmap(folder & oIptFileName & ".bmp", 2970, 2100, myApplication.TransientObjects.CreateColor(255, 255, 255), myApplication.TransientObjects.CreateColor(255, 255, 255))
        Catch
            camError = True
        End Try


        While camError
            tries += 1
            oIptFileName = "Test_ipt" & tries
            Try
                m_Camera.SaveAsBitmap(folder & oIptFileName & ".bmp", 2970, 2100, myApplication.TransientObjects.CreateColor(255, 255, 255), myApplication.TransientObjects.CreateColor(255, 255, 255))
                camError = False
            Catch
            End Try
        End While

        If tries = 0 Then
            Try
                FileSystem.Kill("C:\Inventor Saves\Inventor VBA\*.*")
                m_Camera.SaveAsBitmap(folder & oIptFileName & ".bmp", 2970, 2100, myApplication.TransientObjects.CreateColor(255, 255, 255), myApplication.TransientObjects.CreateColor(255, 255, 255))
            Catch
            End Try
        End If

        myApplication.DisplayOptions.Show3DIndicator = True
        PictureBox3.Image = Image.FromFile("C:\Inventor Saves\Inventor VBA\" & oIptFileName & ".bmp")
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' TELOS EIKONA 3D MONTELOU '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    End Sub






    Public Sub UnfoldTheFace(ByVal myApplication As Inventor.Application, ByVal partComp As SheetMetalComponentDefinition, ByRef TheFaceCounter As Integer)


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
            If oUnitNormal.X = 1 And oUnitNormal.Y = 0 And oUnitNormal.Z = 0 Then
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





    Public Sub Rotate_Fp(ByVal partComp As SheetMetalComponentDefinition, ByVal Rip_precision As Double, ByVal Diam As Double, ByVal YpsosTomhs As Double, ByVal fp As FlatPattern, ByRef peristrafike As Boolean, ByRef oLength As Double, ByRef oWidth As Double)


        ' Ypologismoi gia na vrw telika poso einai h katw plevra tou anaptygmatos exwntas aferesei to Rip
        Dim katheth_mm As Double
        Dim theoritiko As Double
        Dim pososto_gwnias As Double
        katheth_mm = Rip_precision * 40
        pososto_gwnias = (2 * Math.PI - Math.Atan(katheth_mm / Diam)) / (2 * Math.PI)
        theoritiko = pososto_gwnias * Diam * Math.PI

        ' oLength einai to max sto x axis tou flatt pattern--> na einai Diametros *pi  .....  oWidth einai to max sto y axis tou flat pattern --> na einai max ypsos
        oLength = partComp.FlatPattern.Length
        oWidth = partComp.FlatPattern.Width
        'MessageBox.Show(oLength * 10 & ", " & Diam * Math.PI & ", " & oWidth * 10 & ", " & theoritiko)


        ' Epidi to inventor sto flat pattern vazei to megalytero megethos ston x aksona kanw peristofh -90 otan  ( (YpsosTomhs > Diam * Math.PI) 
        ' etsi wste me to rotete ayto na mpei sto x axis h diastash  Diametros * 3.1415 
        Dim tempLength As Double = oLength
        peristrafike = False
        If (YpsosTomhs > theoritiko) Then
            fp.FlatPatternOrientations.ActiveFlatPatternOrientation.AlignmentType = AlignmentTypeEnum.kVerticalAlignment
            peristrafike = True
            oLength = oWidth
            oWidth = tempLength
        End If

    End Sub




    Public Sub Epilogh_Edge(ByVal oEdges As Edges, ByRef TheEdgeCounter As Integer)


        Dim TestEdge As Edge
        'Dim curveLength As Double
        Dim TestCurveEval As CurveEvaluator
        Dim TestMinParam As Double
        Dim TestMaxParam As Double
        Dim countt As Integer
        countt = 0
        For Each TestEdge In oEdges
            countt += 1

            TestCurveEval = TestEdge.Evaluator
            Call TestCurveEval.GetParamExtents(TestMinParam, TestMaxParam)
            'Call TestCurveEval.GetLengthAtParam(TestMinParam, TestMaxParam, curveLength)
            'MessageBox.Show(countt & " " & TestMinParam & " " & TestMaxParam & " " & curveLength)

            If TestMinParam = -Math.PI Then
                TheEdgeCounter = countt
            End If

        Next
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim myEdge As Edge
        myEdge = oEdges.Item(TheEdgeCounter)

    End Sub






    Public Sub Ypologismos_Edge(ByVal myEdge As Inventor.Edge, ByVal NumOfPoints As Integer, ByVal peristrafike As Boolean, ByVal YpsosTomhs As Double, ByRef xPoints() As Double, ByRef yPoints() As Double)

        ''pernw ton CurveEvaluator tou myEdge
        Dim oCurveEval As CurveEvaluator
        oCurveEval = myEdge.Evaluator
        '' Get the parametric range of the curve.
        Dim dMinParam As Double
        Dim dMaxParam As Double
        Call oCurveEval.GetParamExtents(dMinParam, dMaxParam)
        'Dim curveLength As Double
        'Call oCurveEval.GetLengthAtParam(dMinParam, dMaxParam, curveLength)
        'MessageBox.Show(curveLength)


        Dim i As Integer
        For i = 0 To ((NumOfPoints - 1) / 2)     '(NumOfPoints - 1) / 2    einai to meso simeio

            ' Calculate the current parameter to evaluate.
            Dim currentParam As Double
            currentParam = dMinParam + ((dMaxParam - dMinParam) / ((NumOfPoints - 1) / 2)) * i

            ' Assign the value to an array since the GetPointAtParam
            ' takes an array as input.
            Dim adParam(0) As Double
            adParam(0) = currentParam

            ' Get the coordinates of the parameter point in model space.
            Dim adPoints(2) As Double
            Call oCurveEval.GetPointAtParam(adParam, adPoints)


            'MsgBox(dMinParam & "  ,  " & dMaxParam & "  ,  " & adPoints(0) & "  ,  " & adPoints(1) & "  ,  " & adPoints(2))

            If Not peristrafike Then
                If i = 0 Then
                    xPoints((NumOfPoints - 1) / 2) = adPoints(0) * 10
                Else
                    xPoints(((NumOfPoints - 1) / 2) - i) = adPoints(0) * 10
                    xPoints(((NumOfPoints - 1) / 2) + i) = 2 * xPoints((NumOfPoints - 1) / 2) - adPoints(0) * 10
                End If

                yPoints(((NumOfPoints - 1) / 2) - i) = 10 * adPoints(1) + YpsosTomhs
                yPoints(((NumOfPoints - 1) / 2) + i) = 10 * adPoints(1) + YpsosTomhs
            Else

                If i = 0 Then
                    xPoints((NumOfPoints - 1) / 2) = (adPoints(0) - YpsosTomhs / 20) * 10
                Else
                    xPoints(((NumOfPoints - 1) / 2) - i) = (adPoints(0) - YpsosTomhs / 20) * 10
                    xPoints(((NumOfPoints - 1) / 2) + i) = 2 * xPoints((NumOfPoints - 1) / 2) - (adPoints(0) - YpsosTomhs / 20) * 10
                End If

                yPoints(((NumOfPoints - 1) / 2) - i) = 10 * (adPoints(1) + YpsosTomhs / 20)
                yPoints(((NumOfPoints - 1) / 2) + i) = 10 * (adPoints(1) + YpsosTomhs / 20)

            End If

        Next

        '''' Provlima tou 2*YpsosTomhs
        If yPoints(0) > 1.5 * YpsosTomhs Then
            'MsgBox("MPIKE GAMW")
            For k = 0 To NumOfPoints - 1

                yPoints(k) -= YpsosTomhs
            Next

        End If

    End Sub





    Public Sub Fill_Pattern(ByVal fp As FlatPattern, ByVal NumOfPoints As Integer, ByVal myApplication As Application, ByRef xPoints() As Double, ByRef yPoints() As Double)

        '''''''' Vazw ta ypologismena simeia sto Flat Pattern
        Dim pointsSketch As PlanarSketch = fp.Sketches.Add(fp.TopFace, False)
        For i = 0 To NumOfPoints - 1
            pointsSketch.SketchPoints.Add(myApplication.TransientGeometry.CreatePoint2d(xPoints(i) / 10, yPoints(i) / 10))
            'MsgBox(xPoints(i) & "  ,  " & yPoints(i))
        Next
        ''Orizw to myRedColor
        Dim myRedColor As Color
        myRedColor = myApplication.TransientObjects.CreateColor(255, 0, 0)
        pointsSketch.Color = myRedColor

    End Sub




    Public Sub Eikona_Fp(ByVal fp As FlatPattern, ByVal myApplication As Application, ByVal partDoc As PartDocument, ByVal tries As Integer, ByVal folder As String, ByRef PictureBox2 As Windows.Forms.PictureBox)

        fp.Edit()
        partDoc.SelectSet.Select(fp.TopFace)
        myApplication.CommandManager.ControlDefinitions.Item("AppLookAtCmd").Execute()
        partDoc.SelectSet.Clear()
        Dim m_Camera As Inventor.Camera
        m_Camera = myApplication.ActiveView.Camera
        'm_Camera.SceneObject = fp
        myApplication.DisplayOptions.Show3DIndicator = False

        'partDoc.DisplayName = "Test"
        'Dim oFileName As String = Trim(Mid(partDoc.FullFileName, InStrRev(partDoc.FullFileName, "\", -1) + 1, Len(partDoc.FullFileName) - InStrRev(partDoc.FullFileName, "\", -1) - 4))
        Dim oFileName As String = "Test_flat"
        m_Camera.Fit()
        m_Camera.ApplyWithoutTransition()

        'Dim tries As Integer = 0
        'Dim camError As Boolean = False
        If tries > 0 Then
            oFileName = "Test_flat" & tries
        End If

        Try
            m_Camera.SaveAsBitmap(folder & oFileName & ".bmp", 2970, 2100, myApplication.TransientObjects.CreateColor(255, 255, 255), myApplication.TransientObjects.CreateColor(255, 255, 255))
        Catch
        End Try

        myApplication.DisplayOptions.Show3DIndicator = True
        PictureBox2.Image = Image.FromFile("C:\Inventor Saves\Inventor VBA\" & oFileName & ".bmp")

    End Sub




    Public Sub Fill_List(NumOfPoints As Double, ByVal xPointsValues As Double(), ByVal yPointsValues As Double(), ByRef ListView1 As Windows.Forms.ListView)

        Dim oListViewItem As ListViewItem
        For i = 0 To NumOfPoints - 1
            oListViewItem = ListView1.Items.Add(i + 1)
            oListViewItem.SubItems.Add(xPointsValues(i))
            oListViewItem.SubItems.Add(yPointsValues(i))
        Next

    End Sub



    Public Sub Restart(partComp As SheetMetalComponentDefinition, partDoc As PartDocument, myApplication As Application, oSavePath1 As String, oSaveName1 As String)

        'Kleinw to flatt pattern gia na mporw na apothikeysw to part
        If partComp.HasFlatPattern = True Then
            partComp.FlatPattern.ExitEdit()
        End If


        'Dim oSavePath1 As String = "C:\Inventor Saves\Inventor Saves Hidden\"

        'Apothikevw to part wste na mporw meta na anoiksw to drawing
        If Dir(oSavePath1) = "" Then
            System.IO.Directory.CreateDirectory(oSavePath1)
        End If

        Try
            FileSystem.Kill("C:\Inventor Saves Hidden\*.*")
        Catch
        End Try
        Dim subpath As String
        For Each subpath In IO.Directory.GetDirectories(oSavePath1)
            IO.Directory.Delete(subpath, True)
        Next

        Try
            partDoc.SaveAs(oSavePath1 & oSaveName1, True)
            'RichTextBox2.AppendText(vbNewLine & "Επιτυχής Αποθήκευση.!")
        Catch
        End Try

        ' Afou exw idh apothikeymeno to path twra to kleinw
        Call partDoc.Close(True)

    End Sub


    Public Sub AxisSystem(ByVal myApplication As Application, ByVal oView As DrawingView, ByVal oTG As TransientGeometry, ByVal xAxis As Double, ByVal yAxis As Double, ByVal ViewScale1 As Double)

        Dim BlackColor As Color
        BlackColor = myApplication.TransientObjects.CreateColor(0, 0, 0)

        Dim AxisSketch As DrawingSketch = oView.Sketches.Add
        AxisSketch.Edit()

        'katakorifh grammh
        Dim line1 As SketchLine
        line1 = AxisSketch.SketchLines.AddByTwoPoints(oTG.CreatePoint2d(xAxis, yAxis), oTG.CreatePoint2d(xAxis, yAxis + 0.25 / ViewScale1))
        line1.LineWeight = 0.05
        'line1.OverrideColor = myApplication.TransientObjects.CreateColor(0, 255, 0)

        'orizontia grammh
        Dim line2 As SketchLine
        line2 = AxisSketch.SketchLines.AddByTwoPoints(oTG.CreatePoint2d(xAxis, yAxis), oTG.CreatePoint2d(xAxis + 0.25 / ViewScale1, yAxis))
        line2.LineWeight = 0.05
        'line2.OverrideColor = myApplication.TransientObjects.CreateColor(255, 0, 0)

        'Deksi Velos
        Dim oTriangle1 As SketchEntitiesEnumerator
        oTriangle1 = AxisSketch.SketchLines.AddAsPolygon(3, oTG.CreatePoint2d(xAxis + 0.3 / ViewScale1, yAxis), oTG.CreatePoint2d(xAxis + 0.4 / ViewScale1, yAxis), True)
        Dim oProfileTriangle1 As Profile
        oProfileTriangle1 = AxisSketch.Profiles.AddForSolid
        Dim RedColor As Color
        RedColor = myApplication.TransientObjects.CreateColor(255, 0, 0)
        Dim fillRed As SketchFillRegion
        fillRed = AxisSketch.SketchFillRegions.Add(oProfileTriangle1)      ', BlackColor)     '''' RedColor


        ' To x se TEXT
        Dim xTextBox As TextBox
        xTextBox = AxisSketch.TextBoxes.AddFitted(oTG.CreatePoint2d(xAxis + 0.57 / ViewScale1, yAxis - 0.035 / ViewScale1), "X")
        xTextBox.VerticalJustification = VerticalTextAlignmentEnum.kAlignTextUpper
        xTextBox.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextRight
        xTextBox.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & "X" & "</StyleOverride>"
        xTextBox.Color = RedColor

        'AxisSketch.ExitEdit()



        'Panw Velos
        'Dim AxisSketch2 As DrawingSketch = oView.Sketches.Add
        'AxisSketch2.Edit()

        Dim oTriangle2 As SketchEntitiesEnumerator
        oTriangle2 = AxisSketch.SketchLines.AddAsPolygon(3, oTG.CreatePoint2d(xAxis, yAxis + 0.3 / ViewScale1), oTG.CreatePoint2d(xAxis, yAxis + 0.4 / ViewScale1), True)
        Dim oProfileTriangle2 As Profile
        oProfileTriangle2 = AxisSketch.Profiles.AddForSolid
        Dim GreenColor As Color
        GreenColor = myApplication.TransientObjects.CreateColor(0, 255, 0)
        Dim fillGreen As SketchFillRegion
        fillGreen = AxisSketch.SketchFillRegions.Add(oProfileTriangle2)           ', BlackColor)    ''''GreenColor


        ' To y se TEXT
        Dim yTextBox As TextBox
        yTextBox = AxisSketch.TextBoxes.AddFitted(oTG.CreatePoint2d(xAxis - 0.05 / ViewScale1, yAxis + 0.42 / ViewScale1), "Y")
        yTextBox.VerticalJustification = VerticalTextAlignmentEnum.kAlignTextUpper
        yTextBox.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextRight
        yTextBox.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & "Y" & "</StyleOverride>"
        yTextBox.Color = GreenColor

        'AxisSketch2.ExitEdit()



        'Mayros Kyklos
        'Dim AxisSketch3 As DrawingSketch = oView.Sketches.Add
        'AxisSketch3.Edit()

        AxisSketch.SketchCircles.AddByCenterRadius(myApplication.TransientGeometry.CreatePoint2d(xAxis, yAxis), 0.04 / ViewScale1)
        Dim oProfileCircle As Profile
        oProfileCircle = AxisSketch.Profiles.AddForSolid
        'Dim BlackColor As Color
        'BlackColor = myApplication.TransientObjects.CreateColor(0, 0, 0)
        Dim fillBlack As SketchFillRegion
        fillBlack = AxisSketch.SketchFillRegions.Add(oProfileCircle, BlackColor)


        'fillRed.Color = RedColor
        'fillGreen.Color = GreenColor
        'fillBlack.Color = BlackColor
        AxisSketch.ExitEdit()
        'AxisSketch3.ExitEdit()


    End Sub




    Public Sub Table_Construction(ByVal myApplication As Application, ByVal oSheet As Sheet, ByVal maxSeires As Double, ByVal NumOfPoints As Double, ByVal xPointsValues As Double(), ByVal yPointsValues As Double(), ByVal xTablePosition As Double, ByVal yTablePosition As Double, ByVal xTableSpace As Double)

        Dim oTitles(0 To 2) As String
        oTitles(0) = "Point"
        oTitles(1) = "X (mm)"
        oTitles(2) = "Y (mm)"


        Dim oColumnWidths(0 To 2) As Double
        oColumnWidths(0) = 0.8
        oColumnWidths(1) = 2
        oColumnWidths(2) = 2

        'maxSeires = 49 
        Dim oContents(0 To 3 * (maxSeires + 1) - 1) As String

        Dim oRowHeights(0 To maxSeires) As Double
        For i = 0 To maxSeires
            oRowHeights(i) = 0.35
        Next

        For i = 0 To maxSeires

            If i <= NumOfPoints - 1 Then

                oContents(3 * i) = i + 1
                oContents(3 * i + 1) = xPointsValues(i)
                oContents(3 * i + 2) = yPointsValues(i)

            Else

                oContents(3 * i) = i + 1
                oContents(3 * i + 1) = ""
                oContents(3 * i + 2) = ""

            End If

        Next


        Dim oTablePlacement As Point2d
        oTablePlacement = myApplication.TransientGeometry.CreatePoint2d(xTablePosition, yTablePosition)
        Dim oCustomTable As CustomTable
        oCustomTable = oSheet.CustomTables.Add("Σημεία", oTablePlacement, 3, maxSeires + 1, oTitles, oContents, oColumnWidths, oRowHeights)

        oCustomTable.Style.DataTextStyle.FontSize = "0.25"
        oCustomTable.Style.ColumnHeaderTextStyle.FontSize = "0.25"
        oCustomTable.Style.TitleTextStyle.FontSize = "0.25"

        oCustomTable.Style.InsideLineWeight = "0.025"
        oCustomTable.Style.OutsideLineWeight = "0.025"
        ' epiptepei mikrotero row height
        oCustomTable.Style.RowGap = "0.015"
        oCustomTable.Delete()



        '''''''''''''''''''''''''''''''''''''''''''''''' Kataskeyh twn 2 Pinakwn
        Dim oContents1(0 To 3 * (maxSeires + 1) / 2 - 1) As String
        Dim oContents2(0 To 3 * (maxSeires + 1) / 2 - 1) As String

        For i = 0 To 3 * (maxSeires + 1) / 2 - 1
            oContents1(i) = oContents(i)
        Next
        For i = 0 To 3 * (maxSeires + 1) / 2 - 1
            oContents2(i) = oContents(i + (3 * (maxSeires + 1) / 2))
        Next

        maxSeires = (maxSeires + 1) / 2
        Dim oRowHeights1(0 To maxSeires - 1) As Double
        For i = 0 To maxSeires - 1
            oRowHeights1(i) = 0.35
        Next
        oTablePlacement = myApplication.TransientGeometry.CreatePoint2d(xTablePosition, yTablePosition)
        oCustomTable = oSheet.CustomTables.Add("Σημεία", oTablePlacement, 3, maxSeires, oTitles, oContents1, oColumnWidths, oRowHeights1)


        Dim oRowHeights2(0 To maxSeires - 1) As Double
        For i = 0 To maxSeires - 1
            oRowHeights2(i) = 0.35
        Next
        oTablePlacement = myApplication.TransientGeometry.CreatePoint2d(xTablePosition + xTableSpace, yTablePosition)
        oCustomTable = oSheet.CustomTables.Add("Σημεία", oTablePlacement, 3, maxSeires, oTitles, oContents2, oColumnWidths, oRowHeights2)

    End Sub




    Public Sub EpiloghArithmwn(ByVal Gwnia As Double, ByVal NumOfPoints As Integer, ByVal oTG As TransientGeometry, ByVal ViewScale1 As Double, ByVal oPlacementPoint1 As Point2d, ByVal axisProblem As Boolean, ByVal YpsosTomhsLengthSheet As Double, ByVal xPoints() As Double, ByVal yPoints() As Double, ByVal oLength As Double, ByVal oWidth As Double, ByRef oSheet As Sheet)


        Dim sText As String
        Dim oGeneralNote As GeneralNote
        Dim xDiff As Double = 0
        Dim yDiff As Double
        Dim angleFactor As Double = 0
        If Gwnia < 35 Then
            angleFactor = 0.048
        End If

        Dim Center As Double
        Center = (NumOfPoints + 1) / 2


        If NumOfPoints < 20 Then

            For i = 0 To NumOfPoints - 1


                If Gwnia > 45 Then
                    yDiff = 0.385
                Else
                    yDiff = 0.32
                End If


                sText = i + 1

                If i = 0 Or i = NumOfPoints - 1 Then
                    xDiff = -0.07 - (sText.Length - 1) * 0.095
                ElseIf i = NumOfPoints - 2 And i > 13 Then
                    xDiff = -0.1
                ElseIf i = Center - 1 Then
                    xDiff = -sText.Length * 0.1
                ElseIf i < Center - 1 Then
                    If Gwnia > 50 Then
                        xDiff = -sText.Length * 0.18
                        yDiff = 0.32
                    Else
                        xDiff = -sText.Length * 0.16 + sText.Length * angleFactor
                    End If

                ElseIf i > Center - 1 Then
                    If Gwnia > 50 Then
                        xDiff = 0
                    Else
                        xDiff = -sText.Length * 0.04
                    End If
                End If


                If axisProblem And i = 0 Then
                    xDiff += 0.15
                    If YpsosTomhsLengthSheet > 0.12 Then
                        xDiff -= YpsosTomhsLengthSheet / 2.5 - 0.048
                    End If
                End If

                oGeneralNote = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTG.CreatePoint2d(((xPoints(i) / 10) - (oLength / 2)) * ViewScale1 + oPlacementPoint1.X + xDiff, ((yPoints(i) / 10 - (oWidth / 2))) * ViewScale1 + oPlacementPoint1.Y + yDiff), sText)

            Next

        Else

            ''''''''''''''''''''''''''''''''''''''''''''''Ksekinaw apo tin mesh kai vazw deksia aristera me vima ( NumOfPoints DIV 10 )
            Dim i As Integer
            For i = Center To NumOfPoints Step (NumOfPoints \ 10)

                If Gwnia > 45 Then
                    yDiff = 0.385
                Else
                    yDiff = 0.335
                End If


                Dim aristero As Integer
                Dim deksio As Integer
                Dim aristeroStr As String
                Dim deksioStr As String

                aristero = 2 * Center - i
                deksio = i
                aristeroStr = aristero
                deksioStr = deksio



                Dim DeksioXDiff As Double
                Dim AristeroXDiff As Double
                If Gwnia > 50 Then
                    DeksioXDiff = 0
                    AristeroXDiff = -aristeroStr.Length * 0.18
                    yDiff = 0.34
                Else
                    DeksioXDiff = -deksioStr.Length * 0.04
                    AristeroXDiff = -aristeroStr.Length * 0.16 + aristeroStr.Length * angleFactor
                End If


                If i = NumOfPoints Then
                    DeksioXDiff = -0.17
                    AristeroXDiff = -0.07
                    If axisProblem Then
                        AristeroXDiff += 0.15
                        If YpsosTomhsLengthSheet > 0.12 Then
                            xDiff -= YpsosTomhsLengthSheet / 2.5 - 0.048
                        End If
                    End If
                ElseIf i = Center Then
                    DeksioXDiff = -deksioStr.Length * 0.1
                ElseIf i + (NumOfPoints \ 10) > NumOfPoints Then
                    DeksioXDiff = -0.16
                    AristeroXDiff = -0.1
                End If



                If i > Center Then

                    oGeneralNote = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTG.CreatePoint2d(((xPoints(aristero - 1) / 10) - (oLength / 2)) * ViewScale1 + oPlacementPoint1.X + AristeroXDiff, ((yPoints(aristero - 1) / 10 - (oWidth / 2))) * ViewScale1 + oPlacementPoint1.Y + yDiff), aristeroStr)
                    If Gwnia > 45 Then
                        yDiff = 0.385
                    Else
                        yDiff = 0.335
                    End If
                    oGeneralNote = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTG.CreatePoint2d(((xPoints(deksio - 1) / 10) - (oLength / 2)) * ViewScale1 + oPlacementPoint1.X + DeksioXDiff, ((yPoints(deksio - 1) / 10 - (oWidth / 2))) * ViewScale1 + oPlacementPoint1.Y + yDiff), deksioStr)

                Else

                    oGeneralNote = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTG.CreatePoint2d(((xPoints(deksio - 1) / 10) - (oLength / 2)) * ViewScale1 + oPlacementPoint1.X + DeksioXDiff, ((yPoints(deksio - 1) / 10 - (oWidth / 2))) * ViewScale1 + oPlacementPoint1.Y + yDiff), deksioStr)
                End If

            Next

            '''''''''''''''''''''an ksekinisw apo to kentro kai den ypervw kata pol/sio to NumOfPoints tote tha prepei na valw to Prwto kai Telftaio.  ( Tha mpainoun an to vgalw apo ta sxoleia )
            'If i <> NumOfPoints + (NumOfPoints \ 10) Then 'And NumOfPoints <> 43 And NumOfPoints <> 39 And NumOfPoints <> 33 And NumOfPoints <> 27 And NumOfPoints <> 21 Then
            '    xDiff = -0.07
            '    sText = 1
            '    oGeneralNote = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTG.CreatePoint2d(((xPoints(0) / 10) - (oLength / 2)) * ViewScale1 + oPlacementPoint1.X + xDiff, ((yPoints(0) / 10 - (oWidth / 2))) * ViewScale1 + oPlacementPoint1.Y + yDiff), sText)
            '    sText = NumOfPoints
            '    oGeneralNote = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTG.CreatePoint2d(((xPoints(NumOfPoints - 1) / 10) - (oLength / 2)) * ViewScale1 + oPlacementPoint1.X + xDiff, ((yPoints(NumOfPoints - 1) / 10 - (oWidth / 2))) * ViewScale1 + oPlacementPoint1.Y + yDiff), sText)
            'End If

        End If


    End Sub







    Public Sub Ypomnima(ByVal xPos As Double, ByVal ViewScale1 As Double, ByVal Poiotiko As Boolean, ByVal KlimakaKampili As Boolean, ByVal Default_Micro_Path As String, ByVal oTg As TransientGeometry, ByRef oSketchYpomnima As DrawingSketch)

        ' Use the functionality of the sketch to add title block graphics.
        oSketchYpomnima.SketchLines.AddAsTwoPointRectangle(oTg.CreatePoint2d(xPos + 0, 0), oTg.CreatePoint2d(xPos + 18, 3.6))
        oSketchYpomnima.SketchLines.AddByTwoPoints(oTg.CreatePoint2d(xPos + 7.6, 0), oTg.CreatePoint2d(xPos + 7.6, 3.6))
        oSketchYpomnima.SketchLines.AddByTwoPoints(oTg.CreatePoint2d(xPos + 15.6, 0), oTg.CreatePoint2d(xPos + 15.6, 3.6))
        oSketchYpomnima.SketchLines.AddByTwoPoints(oTg.CreatePoint2d(xPos + 7.6, 1.8), oTg.CreatePoint2d(xPos + 18, 1.8))

        ' Override the line weight of the second line.
        oSketchYpomnima.SketchLines(1).LineWeight = 0.05
        oSketchYpomnima.SketchLines(2).LineWeight = 0.05
        oSketchYpomnima.SketchLines(3).LineWeight = 0.05
        oSketchYpomnima.SketchLines(4).LineWeight = 0.05
        oSketchYpomnima.SketchLines(5).LineWeight = 0.025
        oSketchYpomnima.SketchLines(6).LineWeight = 0.025
        oSketchYpomnima.SketchLines(7).LineWeight = 0.025



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
        sText = "Τομή Κυλίνδρου Με Πλάγιο Επίπεδο"
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
        sText = "Κλίμακα:"
        oTextBox = oSketchYpomnima.TextBoxes.AddFitted(oTg.CreatePoint2d(xPos + 15.7, 1.4), sText)
        oTextBox.VerticalJustification = VerticalTextAlignmentEnum.kAlignTextUpper
        oTextBox.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextLeft
        oTextBox.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & sText & "</StyleOverride>"
        sText = "1 : " & Format(10 / ViewScale1, "0.00")
        If Poiotiko Then
            sText = "Μη Αναλογική"
        ElseIf KlimakaKampili Then
            sText = sText & " Κ"
        End If
        oTextBox = oSketchYpomnima.TextBoxes.AddFitted(oTg.CreatePoint2d(xPos + 15.7, 0.8), sText)
        oTextBox.VerticalJustification = VerticalTextAlignmentEnum.kAlignTextUpper
        oTextBox.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextLeft
        oTextBox.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & sText & "</StyleOverride>"

    End Sub




End Module
