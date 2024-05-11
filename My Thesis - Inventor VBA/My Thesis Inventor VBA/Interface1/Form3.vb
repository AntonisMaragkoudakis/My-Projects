﻿Imports Inventor
Imports System.Math
Imports System.Text

Public Class Form3


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        '''' Dimarw to default path gia opou xriastw Pre-saves
        Dim defPath As String
        defPath = My.Application.Info.DirectoryPath
        '''''''''''''''''''''' Ksekinontas neo ypologismo Adiazw oti provalei to application
        ListView1.Items.Clear()
        RichTextBox1.Clear()
        RichTextBox2.Clear()
        PictureBox2.Image = Nothing
        PictureBox3.Image = Nothing
        PictureBox4.Image = Nothing
        PictureBox5.Image = Nothing


        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' Error Code '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Dim apenanti As Double
        Dim oErr As Boolean = False
        Dim oErrStr As String
        Dim k As Integer = 0

        Dim D1 As Double, Ypsos1 As Double, D2 As Double, Ypsos2 As Double, YpsosTomhs As Double, Dx As Double, NumOfPoints As Integer
        Try
            D1 = Double.Parse(TextBox1.Text)
            Ypsos1 = Double.Parse(TextBox2.Text)
            D2 = Double.Parse(TextBox3.Text)
            Ypsos2 = Double.Parse(TextBox4.Text)
            YpsosTomhs = Double.Parse(TextBox5.Text)
            Dx = Double.Parse(TextBox6.Text)
            NumOfPoints = Double.Parse(TextBox7.Text)
        Catch ex As Exception
            oErr = True
            oErrStr = "Λανθασμένη Υποβολή Δεδομένων. Κάποια από τα δεδομένα σας δεν έχουν εισαχθεί ως αριθμοί." & vbNewLine & vbNewLine
            RichTextBox1.AppendText(oErrStr)
        End Try


        If Not oErr Then

            'Isxioun ola pou eixame kai prin me mono epipleon ton elexgo ths orizontias metatopisis
            If Dx >= (D1 - D2) / 2 Then
                k += 1
                oErr = True
                oErrStr = "ΕRROR " & k & ": Λανθασμένη Υποβολή Δεδομένων. Η οριζόντια μετατόπιση έιναι εκτώς ορίων. Δώστε νέα τιμή στην οριζόντια μετατόπιση ώστε να ανοίκει στο διάστημα ( 0 , (D1-D2)/2 )." & vbNewLine & vbNewLine
                RichTextBox1.AppendText(oErrStr)
            End If

            ' Elegxos gia error otan D2 < D1/10
            If D2 < D1 / 10 Then
                k += 1
                oErr = True
                oErrStr = "ΕRROR " & k & ": Λανθασμένη Υποβολή Δεδομένων. Δεν υπάρχει νόημα για υπολογισμό D2 δέκα φορές μικρότερο του D1. Δώστε νέες διαμέτρους ώστε D2 >= D1/10." & vbNewLine & vbNewLine
                RichTextBox1.AppendText(oErrStr)
            End If

            If (D2 >= Ypsos1) Then
                k += 1
                oErr = True
                oErrStr = "ΕRROR " & k & ": Λανθασμένη Υποβολή Δεδομένων. H διάμετρος της οριζόντιας κυλινδρικής επιφάνιας πρέπει να είναι μικρότερη από το ύψος της κατακόρυφης κυλινδρικής επιφάνιας (d2 < h1)" & vbNewLine & vbNewLine
                RichTextBox1.AppendText(oErrStr)
            End If

            If (YpsosTomhs <= D2 / 2 Or YpsosTomhs >= Ypsos1 - D2 / 2) And (D2 < Ypsos1) Then
                k += 1
                oErr = True
                oErrStr = "ΕRROR " & k & ": Λανθασμένη Υποβολή Δεδομένων. Το ύψος τομής πρέπει να είναι μεγαλύτερο από d2/2 και μικρότερο από  h1 - d2/2. Παρακαλώ δώστε νέο ύψος τομής που να ανοίκει στο ( " & D2 / 2 & " , " & Ypsos1 - D2 / 2 & " )." & vbNewLine & vbNewLine
                RichTextBox1.AppendText(oErrStr)
            End If

            If D2 >= D1 Then
                k += 1
                oErr = True
                oErrStr = "ΕRROR " & k & ": Λανθασμένη Υποβολή Δεδομένων. H διάμετρος της κατακόρυφης κυλινδρικής επιφάνιας πρέπει να είναι μεγαλύτερη της διαμέτρου της οριζόντιας κυλινδρικής επιφάνιας (d1 > d2)." & vbNewLine & vbNewLine
                RichTextBox1.AppendText(oErrStr)
            End If

            If Ypsos2 <= D1 / 2 Then
                k += 1
                oErr = True
                oErrStr = "ΕRROR " & k & ": Λανθασμένη Υποβολή Δεδομένων. Το ύψος της οριζόντιας κυλινδρικής επιφάνιας πρέπει να είναι μεγαλύτερο του μισού της διαμέτρου της κατακόρυφης κυλινδρικής επιφάνιας (h2 > d2/2)." & vbNewLine & vbNewLine
                RichTextBox1.AppendText(oErrStr)
            End If

            If NumOfPoints Mod 4 <> 0 Or NumOfPoints < 4 Then
                k += 1
                oErr = True
                oErrStr = "ΕRROR " & k & ": Λανθασμένη Υποβολή Δεδομένων. O αριθμός δειγμάτων ' n '  πρέπει να είναι θετικό πολλαπλάσιο του 4." & vbNewLine & vbNewLine
                RichTextBox1.AppendText(oErrStr)
            End If



            If D1 < 1 Or Ypsos1 < 1 Or D2 < 1 Or Ypsos2 < 1 Or YpsosTomhs < 1 Then
                k += 1
                oErr = True
                oErrStr = "ΕRROR " & k & ": Λανθασμένη Υποβολή Δεδομένων. Δεν έχει νόημα η παραγωγή μηψανολογικού σχεδίου για Διάμετρο, Ύψος ή Ύψος Τομής μικρότερα του ενός χιλιοστού. Δώστε τιμές μεγαλύτερες ή ίσες τις μονάδας." & vbNewLine & vbNewLine
                RichTextBox1.AppendText(oErrStr)
            End If

            If (D1 >= 10000 Or Ypsos1 >= 10000 Or D2 >= 10000 Or Ypsos2 >= 10000) Then
                k += 1
                oErr = True
                oErrStr = "ΕRROR " & k & ": Λανθασμένη Υποβολή Δεδομένων. Δεν έχει νόημα η παραγωγή μηψανολογικού σχεδίου για τόσο μεγάλες τιμές σε Διάμετρο ή Ύψος. Δώστε έως τετραψήφιες τιμές." & vbNewLine & vbNewLine
                RichTextBox1.AppendText(oErrStr)
            End If

            If NumOfPoints > 10000 Then
                k += 1
                oErr = True
                oErrStr = "ΕRROR " & k & ": Λανθασμένη Υποβολή Δεδομένων. Για υπολογισμό πάνω από 9999 σημείων ο χρόνος εκτέλεσης του προγράμματος γίνεται υπερβολικά μεγάλος. Δώστε μικτότερο αριθμό 'n'." & vbNewLine & vbNewLine
                RichTextBox1.AppendText(oErrStr)
            End If



            'Dim msgBoxWarning As Integer
            'If YpsosTomhs + apenanti > 4 * D1 * Math.PI Or 10 * (YpsosTomhs + apenanti) < D1 * Math.PI Then

            '    msgBoxWarning = MsgBox("WARNING : Για τα δεδομένα αυτά το μηχανολογικό σχέδιο θα είναι τόσο στενόμακρο που πιθανώς δεν θα να είναι ευανάγνωστο. Θέλετε να συνεχίσετε?", vbQuestion + vbYesNo + vbDefaultButton2, "WARNING")

            '    If msgBoxWarning = vbNo Then
            '        oErr = True
            '    End If

            'End If

        End If

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' TELOS Error Code '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''




        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' Warning Code '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Dim maxSeires As Integer = 49
        'Dim PoiotikoKonto As Boolean = False
        'Dim PoiotikoPsilo As Boolean = False
        'klp gia warning code

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' AN DEN EXW ERROR PROXORAW STO INVENTOR'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' Kateaskevh 3D MONTELOY 31 ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        If Not oErr Then


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
            Model_31_3D_Construction(myApplication, D1, Ypsos1, D2, Ypsos2, YpsosTomhs, Dx, partDoc, partComp, Rip_precision, DiaforaGwniasRad)


            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' TELOS 3D MONTELOU 31 ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''




            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' EIKONA 3D MONTELOU 31 '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim oSavePath As String = "C:\Inventor Saves\"
            Dim folder As String = "C:\Inventor Saves\Inventor VBA\"
            Dim tries As Integer

            'Kalw synartish
            Eikona_3D(myApplication, oSavePath, folder, PictureBox3, tries)

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''




            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' FLAT PATTERN CODE AND ROTATE 31 ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

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
            Rotate_Fp21(partComp, Rip_precision, D1, Ypsos1, fp, peristrafike, oLength, oWidth)
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''




            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' YPOLOGISMOS EDGES 31 ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            'Pianw ta Edges tou face. Tha mporousa na ta epilegw kai apo tin Epilogh_edges(oEdges,....)
            Dim fpBody As SurfaceBody = fp.SurfaceBodies(1)
            Dim oEdges As Edges = fpBody.Edges


            '''''''''''''''''''''''''''''''''''''''''''''''''''''' gia debuging gia na vriskw to length kathe edge
            'MsgBox(oEdges.Count)
            ''''' Get the parametric range of the curve.
            'Dim dMinParam As Double
            'Dim dMaxParam As Double
            'myEdge2.Evaluator.GetParamExtents(dMinParam, dMaxParam)
            'Dim curveLength As Double
            'myEdge2.Evaluator.GetLengthAtParam(dMinParam, dMaxParam, curveLength)
            'MsgBox(curveLength)
            'For i = 1 To oEdges.Count
            '    myEdge2 = oEdges.Item(i)

            '    myEdge2.Evaluator.GetParamExtents(dMinParam, dMaxParam)
            '    myEdge2.Evaluator.GetLengthAtParam(dMinParam, dMaxParam, curveLength)
            '    MsgBox(i & "  " & curveLength)
            'Next
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''


            ' Gia D1=D2 exei 18 edges anti gia 15 pou exei se kathe allh periptwsh
            Dim IsesDiametroi As Boolean = False
            If D1 = D2 Then
                IsesDiametroi = True
            End If

            '''' gia d1 != d2 ta edges 12 kai 14 kanoyn
            Dim myEdge1 As Edge
            myEdge1 = oEdges.Item(12)
            ''''' gia d1 = d2 xriazomai dio edges. gia to edge1 kanei pali to 12 kai kanoun kai ta 13 15 16
            Dim myEdge2 As Edge
            myEdge2 = oEdges.Item(13)


            Dim xPoints(NumOfPoints - 1) As Double
            Dim yPoints(NumOfPoints - 1) As Double

            Ypologismos_Edge21(myEdge1, myEdge2, IsesDiametroi, NumOfPoints, peristrafike, YpsosTomhs, Ypsos1, xPoints, yPoints)
            'Telos YPOLOGISMOY exw parei ta xPoints , yPoints

            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' Vazw ta ypologismena simeia sto Flat Pattern '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Fill_Pattern(fp, NumOfPoints, myApplication, xPoints, yPoints)
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''' Apothikeyv eikona Flatt Pattern kai thn Provalw sthn Maska '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Eikona_Fp(fp, myApplication, partDoc, tries, folder, PictureBox2)
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



            ''''''''''''''''''''''''''''''''''''''''''''''''''''' Pleon exw ta swsta values ta kanw strogilopoihsh '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            'Dilwnw kai ftiaxnw ta x,y poy tha doylevw sto dgw an den xriastei voithitiko
            Dim xPointsValues(NumOfPoints - 1) As Double
            Dim yPointsValues(NumOfPoints - 1) As Double

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' SOS edw ypologizw thn DiaforaGwniasRad gia na einai kentrarismeno sto DRAWING
            Dim Tokso As Double = xPoints(NumOfPoints / 2) - xPoints(0)
            Dim Lpsomi As Double = xPoints(0)
            Dim Rpsomi As Double = Math.PI * D1 - xPoints(NumOfPoints / 2)
            Dim DiaforaToksou As Double = (Lpsomi - Rpsomi) / 2
            DiaforaGwniasRad = DiaforaToksou / (D1 / 2)
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            '''''''''''''''''''''' SOOOOOOOOOOOS New center tha exw pleon thn mesh tou toksou dld to (xPoints(0) + Tokso / 2)
            'Dim DWG_Axis_x_mmFromNewCenter As Double = (xPoints(0) + Tokso / 2) - xPoints(NumOfPoints / 4)




            ''''''''''metakinish center kai kata -Dx gia vlepw plewn ws kentro to x =  x(num/4) kai oxi tin katw gwnia olou tou anaptygmatos
            Dim DxCenter_mm As Double
            DxCenter_mm = Abs(xPoints(NumOfPoints / 4) - Math.PI * D1 / 2)

            For i = 0 To NumOfPoints - 1
                xPointsValues(i) = xPoints(i)
                yPointsValues(i) = yPoints(i)
                ''''' ta metafero kai sto (0,0) to kentro ths trypas
                xPoints(i) = Format(xPoints(i) - Math.PI * D1 / 2 - DxCenter_mm, "0.00")
                yPoints(i) = Format(yPoints(i) - YpsosTomhs, "0.00")
            Next

            'Kai DEN ta vazw strogylopoihmena sthn lista tis maskas giati meta tha ta valw ola mazi
            'Fill_List(NumOfPoints, xPoints, yPoints, ListView1)
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''







            ''''''''''''''''''''''''''''''''''''''''' Kleinw fp, apothikeyw to part ksanaanoigw pleon gia na mporw na ksekinisw thn kataskeyh toy Vohthitikou ''''''''''''''''''''''''''''''''''''''

            Dim oSavePath1 As String = "C:\Inventor Saves\Inventor Saves Hidden\"
            Dim oSaveName1 As String = "test_R1" & ".ipt"
            Dim sPartPath As String = oSavePath1 & oSaveName1

            '''''' Apothikeyw to part poy doylevw me onoma 'test_R.ipt' sto Inventor Saves Hidden kai to kleinw gia na mporw na ksekinisw thn kataskeyh toy Vohthitikou
            Restart(partComp, partDoc, myApplication, oSavePath1, oSaveName1)
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' TELOS 3D 31 ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''








            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''  ARXH 32  '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            '''''''''''''''''''''''''''''  ARXH 3D 22
            myApplication = GetObject(, "Inventor.Application")
            myApplication.Documents.CloseAll()
            myApplication.Documents.Add(Inventor.DocumentTypeEnum.kPartDocumentObject, myApplication.FileManager.GetTemplateFile(Inventor.DocumentTypeEnum.kPartDocumentObject), True)

            partDoc = myApplication.ActiveDocument
            partDoc.SubType = "{9C464203-9BAE-11D3-8BAD-0060B0CE6BB4}"
            partComp = partDoc.ComponentDefinition


            '''''' 3D Model
            Model_32_3D_Construction(myApplication, D1, Ypsos1, D2, Ypsos2, YpsosTomhs, Dx, partDoc, partComp, Rip_precision)
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' EIKONA 3D MONTELOU 32 '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Eikona_3D(myApplication, oSavePath, folder, PictureBox5, tries)
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' FLAT PATTERN CODE AND ROTATE 32 ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            '' A Side Definition '''' To swsto face exei mallon ( oUnitNormal.X = 1 and oUnitNormal.Y = 0 and oUnitNormal.Z = 0 )

            ''Kalw synartish gia unfold sto katalilo face
            UnfoldTheFaceOrizontiou(myApplication, partComp, TheFaceCounter)

            ''Exwntas kanei Unfold Pianw to antikimeno toy FlatPattern
            fp = myApplication.ActiveEditObject



            '''''''''''''''''''''''''''''''''''' vriskw to Ypsos tis katheths grammhs sto curveLength
            fpBody = fp.SurfaceBodies(1)
            oEdges = fpBody.Edges
            '''''' Get the parametric range of the curve.
            Dim dMinParam As Double
            Dim dMaxParam As Double
            Dim curveLength As Double

            myEdge2 = oEdges.Item(1)
            myEdge2.Evaluator.GetParamExtents(dMinParam, dMaxParam)
            myEdge2.Evaluator.GetLengthAtParam(dMinParam, dMaxParam, curveLength)
            curveLength = curveLength * 10


            Dim peristrafike2 As Boolean = False
            Dim oLength2 As Double
            Dim oWidth2 As Double
            '''''Kanw Rotate To Fp an xriazete kai epistrefw ta oLength, oWidth kai to an telika peristrafike
            RotateCase32(partComp, Rip_precision, D2, D1, curveLength, peristrafike2, fp, oLength2, oWidth2)
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''





            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' YPOLOGISMOS EDGES 32 '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            'Dim fpBody As SurfaceBody
            'Dim oEdges As Edges 
            fpBody = fp.SurfaceBodies(1)
            oEdges = fpBody.Edges

            '''''''''''''''''''''''''''''''''''''''''''''''''''''' gia debuging gia na vriskw to length kathe edge
            'fpBody = fp.SurfaceBodies(1)
            'oEdges = fpBody.Edges
            'MsgBox(oEdges.Count)
            '''''' Get the parametric range of the curve.
            'Dim dMinParam As Double
            'Dim dMaxParam As Double
            'Dim curveLength As Double
            'For i = 1 To oEdges.Count
            '    myEdge2 = oEdges.Item(i)

            '    myEdge2.Evaluator.GetParamExtents(dMinParam, dMaxParam)
            '    myEdge2.Evaluator.GetLengthAtParam(dMinParam, dMaxParam, curveLength)
            '    MsgBox(i & "  " & curveLength)
            'Next
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''



            ''''' Edw h listes den einai num - 1 giati exoume kai to teleytaio
            Dim xPoints22(NumOfPoints) As Double
            Dim yPoints22(NumOfPoints) As Double
            Dim myEdge As Edge = oEdges.Item(13)

            ' o ypologismos linei aytomata kai to provlima tou 2 * ypsosTomhs
            Ypologismos_Edge(myEdge, NumOfPoints + 1, peristrafike2, curveLength, xPoints22, yPoints22)

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' Vazw ta ypologismena simeia sto Flat Pattern 32 ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ''meta to rotate pernaw to simeia panw sto anaptigma
            Fill_Pattern(fp, NumOfPoints + 1, myApplication, xPoints22, yPoints22)
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' Apothikeyv eikona Flatt Pattern kai thn Provalw sthn Maska 32 ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Eikona_Fp(fp, myApplication, partDoc, tries, folder, PictureBox4)
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            ''''''''''''''''''''''''''''''''''''''''''''''''''''' Pleon exw ta swsta values ta kanw strogilopoihsh  32 '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            'Dilwnw kai ftiaxnw ta x,y poy tha doylevw sto dgw an den xriastei voithitiko  ( den einai num - 1 giati exoume kai to teleytaio )
            Dim xPointsValues22(NumOfPoints) As Double
            Dim yPointsValues22(NumOfPoints) As Double

            ''''' Edw exw ena parapanw ara mexri numOfPoints kai oxi num - 1
            For i = 0 To NumOfPoints

                xPointsValues22(i) = xPoints22(i)
                yPointsValues22(i) = yPoints22(i)
                xPoints22(i) = Format(xPoints22(i), "0.00")
                yPoints22(i) = Format(yPoints22(i), "0.00")
                'MsgBox(xPoints(i) & "  ,  " & yPoints(i))
            Next

            '''''Kai ta vazw OLA MAZI strogylopoihmena sthn lista tis maskas
            Fill_List2(NumOfPoints, xPoints, yPoints, xPoints22, yPoints22, ListView1)
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



            '''''''''''''''''''''''''''''''' Kleinw to fp, apothikeyw to part ksanaanoigw pleon gia na mporw na ksekinisw thn kataskeyh toy Vohthitikou ''''''''''''''''''''''''''''''''''''''

            'Dim oSavePath1 As String = "C:\Inventor Saves\Inventor Saves Hidden\"
            oSaveName1 = "test_R2" & ".ipt"
            sPartPath = oSavePath1 & oSaveName1

            '''' Apothikeyw to part poy doylevw me onoma 'test_R.ipt' sto Inventor Saves Hidden kai to kleinw gia na mporw na ksekinisw thn kataskeyh toy Vohthitikou
            partComp.FlatPattern.ExitEdit()
            Restart(partComp, partDoc, myApplication, oSavePath1, oSaveName1)
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' TELOS 3D 32 ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''







            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' ARXH VOITHITIKON 31 '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            myApplication = GetObject(, "Inventor.Application")
            myApplication.Documents.CloseAll()
            myApplication.Documents.Add(Inventor.DocumentTypeEnum.kPartDocumentObject, myApplication.FileManager.GetTemplateFile(Inventor.DocumentTypeEnum.kPartDocumentObject), True)

            partDoc = myApplication.ActiveDocument
            partDoc.SubType = "{9C464203-9BAE-11D3-8BAD-0060B0CE6BB4}"
            partComp = partDoc.ComponentDefinition




            '''''' Vriskw tis kenouries parametrous
            Dim V_Ypsos1, V_YpsosTomhs, Gwnia_Toksou, ViewScale1 As Double
            ParametroiV1(xPointsValues, yPointsValues, D1, V_Ypsos1, V_YpsosTomhs, Gwnia_Toksou, ViewScale1)

            '''''''3D kataskeyh me tis kainouries parametrous
            Model_31_3D_Construction(myApplication, D1, V_Ypsos1, D2, Ypsos2, V_YpsosTomhs, Dx, partDoc, partComp, Rip_precision, DiaforaGwniasRad)

            '''''''FLAT PATTERN CODE AND ROTATE 21

            ' A Side Definition '''' To swsto face exei mallon ( oUnitNormal.X = 1 and oUnitNormal.Y = 0 and oUnitNormal.Z = 0 )
            'Kalw synartish gia unfold sto katalilo face
            UnfoldTheFace(myApplication, partComp, TheFaceCounter)

            'Exwntas kanei Unfold Pianw to antikimeno toy FlatPattern
            fp = myApplication.ActiveEditObject

            '''''Kanw Rotate to Fp an xriazete kai epistrefw ta oLength, oWidth kai to an telika peristrafike
            'Kanw Rotate To Fp an xriazete kai epistrefw ta oLength, oWidth kai to an telika peristrafike
            ''''''  me tis kainouries parametrous
            Rotate_Fp21(partComp, Rip_precision, D1, V_Ypsos1, fp, peristrafike, oLength, oWidth)


            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' YPOLOGISMOS EDGES 31 
            'Pianw ta Edges tou face. Tha mporousa na ta epilegw kai apo tin Epilogh_edges(oEdges,....)
            fpBody = fp.SurfaceBodies(1)
            oEdges = fpBody.Edges



            ' Gia D1=D2 exei 18 edges anti gia 15 pou exei se kathe allh periptwsh
            IsesDiametroi = False
            If D1 = D2 Then
                IsesDiametroi = True
            End If

            '''' gia d1 != d2 ta edges 12 kai 14 kanoyn
            myEdge1 = oEdges.Item(12)
            ''''' gia d1 = d2 xriazomai dio edges. gia to edge1 kanei pali to 12 kai kanoun kai ta 13 15 16
            myEdge2 = oEdges.Item(13)


            '''' kanw ton ypologismo
            Ypologismos_Edge21(myEdge1, myEdge2, IsesDiametroi, NumOfPoints, peristrafike, V_YpsosTomhs, V_Ypsos1, xPointsValues, yPointsValues)
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' Telos YPOLOGISMOY exw parei ta xPointsValues, yPointsValues gia to DWG

            '''''''''''neo metakinish center
            'DxCenter = xPointsValues(NumOfPoints / 4)
            'For i = 0 To NumOfPoints - 1
            '    xPointsValues(i) = xPointsValues(i) + Abs(DxCenter / 10)
            'Next


            '''''''' Kleinw fp, apothikeyw to part ksanaanoigw pleon gia na mporw na ksekinisw thn kataskeyh toy Vohthitikou 

            oSavePath1 = "C:\Inventor Saves\Inventor Saves Hidden\"
            oSaveName1 = "test_V1" & ".ipt"
            sPartPath = oSavePath1 & oSaveName1

            ''''' Apothikeyw to part poy doylevw me onoma 'test_R.ipt' sto Inventor Saves Hidden kai to kleinw gia na mporw na ksekinisw thn kataskeyh toy Vohthitikou
            Restart(partComp, partDoc, myApplication, oSavePath1, oSaveName1)


            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' TELOS VOITHITIKOU 31''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' VOITHITIKO 3D 1 ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim partDoc3D1 As PartDocument
            Dim partComp3D1 As SheetMetalComponentDefinition
            Dim Rip_precision3D1 As Double


            myApplication = GetObject(, "Inventor.Application")
            myApplication.Documents.Add(Inventor.DocumentTypeEnum.kPartDocumentObject, myApplication.FileManager.GetTemplateFile(Inventor.DocumentTypeEnum.kPartDocumentObject), True)

            partDoc3D1 = myApplication.ActiveDocument
            partDoc3D1.SubType = "{9C464203-9BAE-11D3-8BAD-0060B0CE6BB4}"
            partComp3D1 = partDoc3D1.ComponentDefinition


            Dim Ypsos3D1 = D1 * 2.85
            Dim ypsosTomhs3D1 = Ypsos3D1 / 2

            '3D kataskeyh
            Model_31_3D_Construction(myApplication, D1, Ypsos3D1, D2, Ypsos2, ypsosTomhs3D1, Dx, partDoc3D1, partComp3D1, Rip_precision3D1, DiaforaGwniasRad)


            '''''''' Kleinw fp, apothikeyw to part ksanaanoigw pleon gia na mporw na ksekinisw thn kataskeyh toy Vohthitikou 

            oSavePath1 = "C:\Inventor Saves\Inventor Saves Hidden\"
            oSaveName1 = "test_V1_3D" & ".ipt"
            sPartPath = oSavePath1 & oSaveName1


            ''''' Apothikeyw to part poy doylevw me onoma 'test_R.ipt' sto Inventor Saves Hidden kai to kleinw gia na mporw na ksekinisw thn kataskeyh toy Vohthitikou
            Restart(partComp3D1, partDoc3D1, myApplication, oSavePath1, oSaveName1)

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''






            '''''''''''''''''''''''''''''''''''''''''''''''''''''' ARXH VOITHITIKOY_32 OTAN XRIAZETE '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim V2_Konto As Boolean = False
            Dim V2_Psilo As Boolean = False
            Dim maxYpsos2 As Double = yPoints22(NumOfPoints / 2) / 10
            Dim diaforaSeXiliosta As Double = Abs(maxYpsos2 - oLength2) * 10
            Dim V_Ypsos2 As Double
            If maxYpsos2 > oLength2 Then
                V2_Psilo = True
                V_Ypsos2 = Ypsos2 - diaforaSeXiliosta
                curveLength = curveLength - diaforaSeXiliosta
            ElseIf maxYpsos2 < 0.5 * oLength2 Then
                V2_Konto = True
                V_Ypsos2 = Ypsos2 + diaforaSeXiliosta
                curveLength = curveLength + diaforaSeXiliosta
            End If

            If V2_Psilo Or V2_Konto Then

                ''''''''''' ARXH VOITHITIKON 21 

                myApplication = GetObject(, "Inventor.Application")
                myApplication.Documents.CloseAll()
                myApplication.Documents.Add(Inventor.DocumentTypeEnum.kPartDocumentObject, myApplication.FileManager.GetTemplateFile(Inventor.DocumentTypeEnum.kPartDocumentObject), True)

                partDoc = myApplication.ActiveDocument
                partDoc.SubType = "{9C464203-9BAE-11D3-8BAD-0060B0CE6BB4}"
                partComp = partDoc.ComponentDefinition


                '''''''3D kataskeyh me tis kainouries parametrous
                Model_32_3D_Construction(myApplication, D1, Ypsos1, D2, V_Ypsos2, YpsosTomhs, Dx, partDoc, partComp, Rip_precision)

                'Kalw synartish gia unfold sto katalilo face
                UnfoldTheFaceOrizontiou(myApplication, partComp, TheFaceCounter)

                'Exwntas kanei Unfold Pianw to antikimeno toy FlatPattern
                fp = myApplication.ActiveEditObject

                ''''Kanw Rotate To Fp an xriazete kai epistrefw ta oLength, oWidth kai to an telika peristrafike
                RotateCase32(partComp, Rip_precision, D2, D1, curveLength, peristrafike2, fp, oLength2, oWidth2)
                'Exwntas kanei Unfold Pianw to antikimeno toy FlatPattern


                '''''''''''''''''''''''''''''' YPOLOGISMOS EDGES 32 
                'Dim fpBody As SurfaceBody
                'Dim oEdges As Edges 
                fpBody = fp.SurfaceBodies(1)
                oEdges = fpBody.Edges


                ' o ypologismos linei aytomata kai to provlima tou 2 * ypsosTomhs
                myEdge = oEdges.Item(13)
                Ypologismos_Edge(myEdge, NumOfPoints + 1, peristrafike2, curveLength, xPointsValues22, yPointsValues22)
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' Telos YPOLOGISMOY exw parei ta xPointsValues, yPointsValues gia to DWG


                'Dim oSavePath1 As String = "C:\Inventor Saves\Inventor Saves Hidden\"
                oSaveName1 = "test_V2" & ".ipt"
                sPartPath = oSavePath1 & oSaveName1


                ''''' Apothikeyw to part poy doylevw me onoma 'test_R.ipt' sto Inventor Saves Hidden kai to kleinw gia na mporw na ksekinisw thn kataskeyh toy Vohthitikou
                partComp.FlatPattern.ExitEdit()
                Restart(partComp, partDoc, myApplication, oSavePath1, oSaveName1)

            End If

            '''''''''''''' TELOS VOITHITIKOU 22

            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' TELOS VOITHITIKON ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''








            '    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' DRAWING CODE 31 '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' DRAWING CODE 31 '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' DRAWING CODE 31 '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' DRAWING CODE 31 ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            maxSeires = 49
            If NumOfPoints <= maxSeires Then



                '''''''''''''''''''''''''''''''''''''''''' Drawing definition
                ' Define drawing template
                Dim sTemplateFile As String
                sTemplateFile = myApplication.FileManager.GetTemplateFile(DocumentTypeEnum.kDrawingDocumentObject)

                Dim oDrawDoc As DrawingDocument
                oDrawDoc = myApplication.Documents.Add(DocumentTypeEnum.kDrawingDocumentObject, sTemplateFile)


                ' The drawing is created with a single sheet,
                ' so we'll add our views to it.
                Dim oSheet As Sheet = oDrawDoc.Sheets.Item(1)

                oSheet.Size = DrawingSheetSizeEnum.kA3DrawingSheetSize
                '''''''''''''' Epilegw Landscape sta polla
                oSheet.Orientation = PageOrientationTypeEnum.kLandscapePageOrientation

                '''''''''''''''''''''''''''''''''''''''



                '''''''''''''''''''''''''''''''''''''''''' ta panta gia ton katakoryfo
                DWG_31(oSavePath1, myApplication, oSheet, D1, D2, Ypsos1, NumOfPoints, ViewScale1, Dx, xPointsValues, yPointsValues, oLength, oWidth, maxSeires, xPoints, yPoints)



                '''''''''''''''''''''''''''''''''''''''''' ta panta gia ton orizontio
                DWG_32(oSavePath1, myApplication, V2_Konto, V2_Psilo, oSheet, oLength2, oWidth2, NumOfPoints, D1, D2, xPointsValues22, yPointsValues22, maxSeires, xPoints22, yPoints22, Dx)




                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' ΥΠΟΜΝΙΜΑ '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                Dim oTitleBlockDef As TitleBlockDefinition
                oTitleBlockDef = oDrawDoc.TitleBlockDefinitions.Add("TUC MICRO Title Block")

                Dim oSketchYpomnima As DrawingSketch
                oTitleBlockDef.Edit(oSketchYpomnima)

                Dim xPos As Double
                xPos = 1

                Dim oTG As TransientGeometry = myApplication.TransientGeometry
                Dim ViewScale2 As Double
                ViewScale2 = 10 / oLength2

                Dim Default_Micro_Path As String = defPath & "\micro.png"
                Ypomnima3(xPos, ViewScale1, ViewScale2, Default_Micro_Path, oTG, oSketchYpomnima)
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''




                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' Prospatheia eikonas''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                Dim Defaul_Pic1_Path As String = defPath & "\Capture3.png"
                '''''Dim LogoFilename1 As String = "C:\Users\antma\Desktop\Case 1\Interface1\picture1.png"
                Dim oSketchImage1 As SketchImage
                oSketchImage1 = oSketchYpomnima.SketchImages.Add(Defaul_Pic1_Path, oTG.CreatePoint2d(19 - 6.5 - 0.15, 27.7 - 0.15), False) 'to 6.5 = oSketchImage1.Width 
                '''''set image size in cm
                oSketchImage1.Height = 4
                oSketchImage1.Width = 6.5
                Dim xText As String = 19 - 0.2
                Dim yText As String = 27.7 - 0.15 - oSketchImage1.Height - 0.15

                '''''''''''''''''''''''''''''''''''''''' Dedomena dipla apo foto
                Dim oTextBox As TextBox
                Dim sTextDedomena As String
                sTextDedomena = "d1 = " & Format(D1, "0.00") & " mm"
                oTextBox = oSketchYpomnima.TextBoxes.AddFitted(oTG.CreatePoint2d(xText, yText), sTextDedomena)
                oTextBox.VerticalJustification = VerticalTextAlignmentEnum.kAlignTextUpper
                oTextBox.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextRight
                oTextBox.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & sTextDedomena & "</StyleOverride>"

                sTextDedomena = "h1 = " & Format(Ypsos1, "0.00") & " mm"
                oTextBox = oSketchYpomnima.TextBoxes.AddFitted(oTG.CreatePoint2d(xText, yText - 0.4), sTextDedomena)
                oTextBox.VerticalJustification = VerticalTextAlignmentEnum.kAlignTextUpper
                oTextBox.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextRight
                oTextBox.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & sTextDedomena & "</StyleOverride>"

                sTextDedomena = "d2 = " & Format(D2, "0.00") & " mm"
                oTextBox = oSketchYpomnima.TextBoxes.AddFitted(oTG.CreatePoint2d(xText, yText - 0.8), sTextDedomena)
                oTextBox.VerticalJustification = VerticalTextAlignmentEnum.kAlignTextUpper
                oTextBox.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextRight
                oTextBox.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & sTextDedomena & "</StyleOverride>"

                sTextDedomena = "h2= " & Format(Ypsos2, "0.00") & " mm"
                oTextBox = oSketchYpomnima.TextBoxes.AddFitted(oTG.CreatePoint2d(xText, yText - 1.2), sTextDedomena)
                oTextBox.VerticalJustification = VerticalTextAlignmentEnum.kAlignTextUpper
                oTextBox.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextRight
                oTextBox.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & sTextDedomena & "</StyleOverride>"

                sTextDedomena = "I= " & Format(YpsosTomhs, "0.00") & " mm"
                oTextBox = oSketchYpomnima.TextBoxes.AddFitted(oTG.CreatePoint2d(xText, yText - 1.6), sTextDedomena)
                oTextBox.VerticalJustification = VerticalTextAlignmentEnum.kAlignTextUpper
                oTextBox.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextRight
                oTextBox.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & sTextDedomena & "</StyleOverride>"

                sTextDedomena = "x= " & Format(Dx, "0.00") & " mm"
                oTextBox = oSketchYpomnima.TextBoxes.AddFitted(oTG.CreatePoint2d(xText, yText - 2.0), sTextDedomena)
                oTextBox.VerticalJustification = VerticalTextAlignmentEnum.kAlignTextUpper
                oTextBox.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextRight
                oTextBox.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & sTextDedomena & "</StyleOverride>"

                oSheet.TitleBlock.Delete()
                '''' Add an instance of the title block definition to the sheet.
                Dim oTitleBlock As TitleBlock
                oTitleBlock = oSheet.AddTitleBlock(oTitleBlockDef, , )

                oTitleBlockDef.ExitEdit()
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' Telos Ypomnimatos '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



                '''''''''' Temp Apothikevseis twn .dwg kai .pdf
                oDrawDoc.SaveAsInventorDWG(oSavePath1 & "test.dwg", True)
                oDrawDoc.SaveAsInventorDWG(oSavePath1 & "test.pdf", True)
                '''''''''' Kleinw ORISTIKA to oDrawDoc 
                'oDrawDoc.Close(True)



            End If
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' Telos DWG CODE ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''




            '    ''''''''''''''' APOTHIKEYSH gia to .txt  (panta oxi mono otan kanw dwg gia mexri 49 simeia) 
            '    '''''''''' Save txt file (1)
            '    Dim path As String = oSavePath1 & "test (1).txt"
            '    If Not System.IO.File.Exists(path) Then
            '        System.IO.File.Create(path).Dispose()
            '    End If

            '    System.IO.File.WriteAllText(path, "")
            '    Dim file As System.IO.StreamWriter
            '    file = My.Computer.FileSystem.OpenTextFileWriter(path, True)
            '    For i = 0 To NumOfPoints - 1
            '        file.WriteLine("Point[" & i + 1 & "] = ( " & Format(xPoints(i), "0.00") & " ," & Format(yPoints(i), "0.00") & " )")
            '    Next
            '    file.Close()


            '    '''''''''' Save txt file (2)
            '    path = oSavePath1 & "test (2).txt"
            '    If Not System.IO.File.Exists(path) Then
            '        System.IO.File.Create(path).Dispose()
            '    End If

            '    System.IO.File.WriteAllText(path, "")
            '    file = My.Computer.FileSystem.OpenTextFileWriter(path, True)
            '    For i = 0 To NumOfPoints - 1
            '        file.WriteLine("Point[" & i + 1 & "] = ( " & Format(xPoints22(i), "0.00") & " ," & Format(yPoints22(i), "0.00") & " )")
            '    Next
            '    file.Close()





            '    ''''''''''''''''''''''''' Hide the saves folder
            '    Dim objFSO As Object
            '    Dim objFolder As Object
            '    objFSO = CreateObject("Scripting.FileSystemObject")
            '    objFolder = objFSO.GetFolder("C:\Inventor Saves\Inventor Saves Hidden")
            '    Dim d As New System.IO.DirectoryInfo("C:\Inventor Saves\Inventor Saves Hidden")
            '    If Not (d.Attributes And IO.FileAttributes.Hidden) = IO.FileAttributes.Hidden Then
            '        objFolder.Attributes = objFolder.Attributes Xor 2
            '    End If
            '    objFolder = Nothing
            '    objFSO = Nothing
            '    ''''''''''''''''''''''''' Hide the patterns picture folder
            '    objFSO = CreateObject("Scripting.FileSystemObject")
            '    objFolder = objFSO.GetFolder(folder)
            '    Dim dir1 As New System.IO.DirectoryInfo(folder)
            '    If Not (dir1.Attributes And IO.FileAttributes.Hidden) = IO.FileAttributes.Hidden Then
            '        objFolder.Attributes = objFolder.Attributes Xor 2
            '    End If
            '    objFolder = Nothing
            '    objFSO = Nothing
            '    ''''''''''''''''''''''''' Telos Temp Apothikevsewn kai Hide fakelwn


        End If




    End Sub








    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Try

            Dim oBoolean As Boolean = True
            RichTextBox2.Clear()

            Dim oSaveName As String
            oSaveName = TextBox8.Text
            Dim oSavePath As String = "C:\Inventor Saves\"
            If Dir(oSavePath) = "" Then
                System.IO.Directory.CreateDirectory(oSavePath)
            End If



            Dim StrError As String = "Unkown Error"
            ' Determines if there are bad characters in the name.
            For Each badChar As Char In oSaveName
                If badChar = "/" Or badChar = "\" Or badChar = ":" Or badChar = "*" Or badChar = "?" Or badChar = "<" Or badChar = ">" Or badChar = "|" Or badChar = Chr(34) Then
                    oBoolean = False
                    StrError = "To Όνομα περιέχεi μη αποδεκτούς χαρακτήρες όπως ( / \ : * ? < > | " & Chr(34) & " ). Προσπαθήστε ξανά με νέο όνομα."
                End If
            Next
            If oSaveName = "" Then
                oBoolean = False
                StrError = "To Όνομα είναι κενό. Παρακαλώ εισάγετε ένα όνομα."
            End If
            If ListView1.Items.Count = 0 Then
                oBoolean = False
                StrError = "Δεν υπάρχουν αρχεία προς αποθήκεθση. Παρακαλώ υποβάλετε δεδομένα για την παραγωγή αρχείων."
            End If

            If oBoolean Then
                If Dir(oSavePath & oSaveName) = "" Then
                    System.IO.Directory.CreateDirectory(oSavePath & oSaveName)
                    Try
                        If TextBox7.Text > 49 Then
                            My.Computer.FileSystem.CopyFile("C:\Inventor Saves\Inventor Saves Hidden\test_R1.ipt", oSavePath & oSaveName & "\" & oSaveName & " (1)" & ".ipt")
                            My.Computer.FileSystem.CopyFile("C:\Inventor Saves\Inventor Saves Hidden\test_R2.ipt", oSavePath & oSaveName & "\" & oSaveName & " (2)" & ".ipt")
                            My.Computer.FileSystem.CopyFile("C:\Inventor Saves\Inventor Saves Hidden\test (1).txt", oSavePath & oSaveName & "\" & oSaveName & " (1).txt")
                            My.Computer.FileSystem.CopyFile("C:\Inventor Saves\Inventor Saves Hidden\test (2).txt", oSavePath & oSaveName & "\" & oSaveName & " (2).txt")
                            RichTextBox2.AppendText("Επιτυχής αποθήκευση του project στον φάκελο C:\Inventor Saves\" & oSaveName)
                        Else
                            My.Computer.FileSystem.CopyFile("C:\Inventor Saves\Inventor Saves Hidden\test_R1.ipt", oSavePath & oSaveName & "\" & oSaveName & " (1)" & ".ipt")
                            My.Computer.FileSystem.CopyFile("C:\Inventor Saves\Inventor Saves Hidden\test_R2.ipt", oSavePath & oSaveName & "\" & oSaveName & " (2)" & ".ipt")
                            My.Computer.FileSystem.CopyFile("C:\Inventor Saves\Inventor Saves Hidden\test.dwg", oSavePath & oSaveName & "\" & oSaveName & ".dwg")
                            My.Computer.FileSystem.CopyFile("C:\Inventor Saves\Inventor Saves Hidden\test.pdf", oSavePath & oSaveName & "\" & oSaveName & ".pdf")
                            My.Computer.FileSystem.CopyFile("C:\Inventor Saves\Inventor Saves Hidden\test (1).txt", oSavePath & oSaveName & "\" & oSaveName & " (1).txt")
                            My.Computer.FileSystem.CopyFile("C:\Inventor Saves\Inventor Saves Hidden\test (2).txt", oSavePath & oSaveName & "\" & oSaveName & " (2).txt")
                            RichTextBox2.AppendText("Επιτυχής αποθήκευση του project στον φάκελο C:\Inventor Saves\" & oSaveName)
                        End If
                    Catch
                        RichTextBox2.AppendText("To Όνομα του Project υπάρχει ήδη. Παρακαλώ δώστε ένα νέο όνομα και προσπαθήστε ξανά.")
                    End Try
                Else
                    RichTextBox2.AppendText("To Όνομα του Project υπάρχει ήδη. Παρακαλώ δώστε ένα νέο όνομα και προσπαθήστε ξανά.")
                End If
            Else
                RichTextBox2.AppendText(StrError)
            End If


        Catch ex As Exception

            MsgBox("Κάτι πήγε στραβά. Προσπαθήστε να αποθηκεύσετε ξανά.")
        End Try

    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    '' Isws xriastei prin klisei to gui (an to trexw san add-in) na epanaferw tha sheet metal defauls gia na min minoun etsi gia panta kathe pou anoigei to inventor
    'partComp.UnfoldMethod.kFactor = default_k_factor
    'partComp.SheetMetalStyles.Item(1).Activate()

End Class