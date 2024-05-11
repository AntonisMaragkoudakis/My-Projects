Imports Inventor
Imports System.Math
Imports System.Text



Public Class Form1




    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try


            '''' Dimarw to default path gia opou xriastw Pre-saves
            Dim defPath As String
            defPath = My.Application.Info.DirectoryPath
            '''''''''''''''''''''' Ksekinontas neo ypologismo Adiazw oti provalei to application
            ListView1.Items.Clear()
            RichTextBox1.Clear()
            RichTextBox2.Clear()
            PictureBox2.Image = Nothing
            PictureBox3.Image = Nothing


            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' Error Code '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim height As Double
            Dim oErr As Boolean = False
            Dim oErrStr As String
            Dim k As Integer = 0

            Dim Diam As Double, Ypsos As Double, Gwnia As Double, YpsosTomhs As Double, NumOfPoints As Double
            Try
                Diam = Double.Parse(TextBox1.Text)
                Ypsos = Double.Parse(TextBox2.Text)
                Gwnia = Double.Parse(TextBox3.Text)
                YpsosTomhs = Double.Parse(TextBox4.Text)
                NumOfPoints = Double.Parse(TextBox5.Text)
            Catch ex As Exception
                oErr = True
                oErrStr = "Λανθασμένη Υποβολή Δεδομένων. Κάποια από τα δεδομένα σας δεν έχουν εισαχθεί ως αριθμοί." & vbNewLine & vbNewLine
                RichTextBox1.AppendText(oErrStr)
            End Try

            'Tria dekadika psifia ypopsin
            Diam = Double.Parse(Format(Val(Diam), "0.00"))
            Ypsos = Double.Parse(Format(Val(Ypsos), "0.00"))
            Gwnia = Double.Parse(Format(Val(Gwnia), "0.00"))
            YpsosTomhs = Double.Parse(Format(Val(YpsosTomhs), "0.00"))
            'height
            height = Diam * Math.Tan(Gwnia * Math.PI / 180)

            If Not oErr Then

                If Gwnia >= 90 Or Gwnia <= 0 Then
                    k += 1
                    oErr = True
                    oErrStr = "ΕRROR " & k & ": Λανθασμένη Υποβολή Δεδομένων. Η Γωνία πρέπει να έχει τιμή στο διάστημα (0 , 90)." & vbNewLine & vbNewLine
                    RichTextBox1.AppendText(oErrStr)
                End If

                If YpsosTomhs / Ypsos >= 1 Then
                    k += 1
                    oErr = True
                    oErrStr = "ΕRROR " & k & ": Λανθασμένη Υποβολή Δεδομένων. Το ύψος της κυλινδρικής επιφάνιας πρέπει να είναι μεγαλύτερο του ύψους τομής." & vbNewLine & vbNewLine
                    RichTextBox1.AppendText(oErrStr)
                End If

                If NumOfPoints Mod 2 <> 1 Or NumOfPoints < 3 Then
                    k += 1
                    oErr = True
                    oErrStr = "ΕRROR " & k & ": Λανθασμένη Υποβολή Δεδομένων. O αριθμός δειγμάτων ' n '  πρέπει να είναι περιτός και μεγαλύτερος του 3." & vbNewLine & vbNewLine
                    RichTextBox1.AppendText(oErrStr)
                End If


                'height = Diam * Math.Tan(Gwnia * Math.PI / 180)
                If height + YpsosTomhs > Ypsos And k = 0 Then
                    k += 1
                    oErr = True
                    Dim oMaxAngle As Double
                    oMaxAngle = (Ypsos - YpsosTomhs) / Diam
                    oMaxAngle = Math.Atan(oMaxAngle) * 180 / Math.PI
                    oErrStr = "ΕRROR " & k & ": Λανθασμένη Υποβολή Δεδομένων. Σήμφωνα με τα δεδομένα σας, το επίπεδο υπό γωνία " & Format((Gwnia), "0.000") & "° δεν τέμνει διαγώνια την κυλινδρική επιφάνια εφ όλης της περιμέτρου της. Θα πρέπει ή το ύψος να είναι μεγαλύτερο των " & Format(height + YpsosTomhs, "0.000") & "(mm) ή η γωνία να είναι μικρότερη των " & Format(oMaxAngle, "0.000") & "°." & vbNewLine & vbNewLine
                    RichTextBox1.AppendText(oErrStr)
                End If


                If Diam < 1 Or Ypsos < 1 Or YpsosTomhs < 1 Then
                    k += 1
                    oErr = True
                    oErrStr = "ΕRROR " & k & ": Λανθασμένη Υποβολή Δεδομένων. Δεν έχει νόημα η παραγωγή μηψανολογικού σχεδίου για Διάμετρο, Ύψος ή Ύψος Τομής μικρότερα του ενός χιλιοστού. Δώστε τιμές μεγαλύτερες ή ίσες τις μονάδας." & vbNewLine & vbNewLine
                    RichTextBox1.AppendText(oErrStr)
                End If

                If (Diam >= 10000 Or Ypsos >= 10000) Then
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

                If Diam <> TextBox1.Text Or Ypsos <> TextBox2.Text Or Gwnia <> TextBox3.Text Or YpsosTomhs <> TextBox4.Text Then
                    k += 1
                    oErr = True

                    oErrStr = "ΕRROR " & k & ": Λανθασμένη Υποβολή Δεδομένων. Παρακαλώ δώστε μέχρι και 2 δεκαδικά ψηφία στα δεδομένα." & vbNewLine & vbNewLine
                    RichTextBox1.AppendText(oErrStr)
                End If


            End If

            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' TELOS Error Code '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' Warning Code '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim Perimetros As Double
            Dim TotalHeight As Double
            'Dim MissingYpsosTommhs As Double
            Dim PoiotikoGwnia As Boolean = False
            Dim PoiotikoKonto As Boolean = False
            Dim PoiotikoPsilo As Boolean = False
            Dim MonoYpologismos As Boolean = False
            Dim xYpsosTomhs As Double
            Dim V_Ypsos As Double
            Dim maxSeires As Integer = 49

            Perimetros = Math.PI * Diam
            TotalHeight = height + YpsosTomhs



            If Not oErr Then

                Dim msgBoxWarning As Integer


                If NumOfPoints > maxSeires Then

                    msgBoxWarning = MsgBox("WARNING : Για περισσότερα από 49 σημεία θα γίνει μόνο ο υπολογισμός και δεν θα κατασκευαστεί μηχανολογικό σχέδιο. Θέλετε να συνεχίσετε?", vbQuestion + vbYesNo + vbDefaultButton2, "WARNING")

                    If msgBoxWarning = vbNo Then
                        oErr = True
                    Else
                        oErrStr = "Warning " & ": Για περισσότερα από 49 σημεία θα γίνει μόνο ο υπολογισμός και δεν θα κατασκευαστεί μηχανολογικό σχέδιο." & vbNewLine & vbNewLine
                        RichTextBox1.AppendText(oErrStr)
                    End If

                    MonoYpologismos = True

                Else


                    If Gwnia > 70 Then

                        msgBoxWarning = MsgBox("WARNING : Για γωνία μεγαλύτερη των 70° το ανάπτυγμα θα είναι αρκετά μακρόστενο λόγο μεγάλου ύψους. Το μηχανολογικό σχέδιο προκειμένου να είναι ευανάγνωστο και να οριοθετιθεί όμορφα ώς προς το ύψος του θα είναι ποιoτικό, δίχως πραγματικές αναλογίες και κλίμακα. Θέλετε να συνεχίσετε?", vbQuestion + vbYesNo + vbDefaultButton2, "WARNING")

                        If msgBoxWarning = vbNo Then
                            oErr = True
                        Else
                            oErrStr = "Warning " & ": Για γωνία μεγαλύτερη των 70° το ανάπτυγμα θα είναι αρκετά μακρόστενο λόγο μεγάλου ύψους. Το μηχανολογικό σχέδιο προκειμένου να είναι ευανάγνωστο και να οριοθετιθεί όμορφα ώς προς το ύψος του θα είναι ποιoτικό, δίχως πραγματικές αναλογίες και κλίμακα." & vbNewLine & vbNewLine
                            RichTextBox1.AppendText(oErrStr)
                        End If

                        PoiotikoGwnia = True
                        height = Diam * Math.Tan(70 * Math.PI / 180)
                        xYpsosTomhs = Perimetros - height
                        V_Ypsos = xYpsosTomhs + height + 1
                        'MissingYpsosTommhs = YpsosTomhs - xYpsosTomhs

                    ElseIf TotalHeight < 0.5 * Perimetros Then

                        msgBoxWarning = MsgBox("WARNING : Για τα δεδομένα αυτά το ανάπτυγμα θα είναι στενόμακρο με μικρό ύψος. Το μηχανολογικό σχέδιο θα εμφανίζει το ανάπτυγμα με οριζόντια τομή προκειμένου να είναι ευανάγνωστο και να οριοθετιθεί όμορφα ώς προς το ύψος του. Θέλετε να συνεχίσετε?", vbQuestion + vbYesNo + vbDefaultButton2, "WARNING")

                        If msgBoxWarning = vbNo Then
                            oErr = True
                        Else
                            oErrStr = "Warning " & ": Για τα δεδομένα αυτά το ανάπτυγμα θα είναι στενόμακρο με μικρό ύψος. Το μηχανολογικό σχέδιο θα εμφανίζει το ανάπτυγμα με οριζόντια τομή προκειμένου να είναι ευανάγνωστο και να οριοθετιθεί όμορφα ώς προς το ύψος του." & vbNewLine & vbNewLine
                            RichTextBox1.AppendText(oErrStr)
                        End If

                        PoiotikoKonto = True
                        xYpsosTomhs = Perimetros - height
                        V_Ypsos = xYpsosTomhs + height + 1
                        'xYpsosTomhs = Double.Parse(Format(Val(xYpsosTomhs), "0.0000"))
                        'MissingYpsosTommhs = YpsosTomhs - xYpsosTomhs
                        'MsgBox(MissingYpsosTommhs & "  " & YpsosTomhs & "  " & xYpsosTomhs & "  " & height)


                    ElseIf (TotalHeight > Perimetros) Then

                        msgBoxWarning = MsgBox("WARNING : Για τα δεδομένα αυτά το ανάπτυγμα θα είναι στενόμακρο με μεγάλο ύψος. Το μηχανολογικό σχέδιο θα εμφανίζει το ανάπτυγμα με οριζόντια τομή προκειμένου να είναι ευανάγνωστο και να οριοθετιθεί όμορφα ώς προς το ύψος του. Θέλετε να συνεχίσετε?", vbQuestion + vbYesNo + vbDefaultButton2, "WARNING")

                        If msgBoxWarning = vbNo Then
                            oErr = True
                        Else
                            oErrStr = "Warning " & ": Για τα δεδομένα αυτά το ανάπτυγμα θα είναι στενόμακρο με μεγάλο ύψος. Το μηχανολογικό σχέδιο θα εμφανίζει το ανάπτυγμα με οριζόντια τομή προκειμένου να είναι ευανάγνωστο και να οριοθετιθεί όμορφα ώς προς το ύψος του." & vbNewLine & vbNewLine
                            RichTextBox1.AppendText(oErrStr)
                        End If

                        PoiotikoPsilo = True
                        xYpsosTomhs = Perimetros - height
                        V_Ypsos = xYpsosTomhs + height + 1


                    End If

                End If

            End If
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' Telos Warning Code ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' Kateaskevh 3D MONTELOY AN DEN EXW ERROR '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
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



                '3D kataskeyh
                Model_1_3D_Construction(myApplication, Diam, Ypsos, YpsosTomhs, Gwnia, partDoc, partComp, Rip_precision)


                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' EIKONA 3D MONTELOU ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                Dim oSavePath As String = "C:\Inventor Saves\"
                Dim folder As String = "C:\Inventor Saves\Inventor VBA\"
                Dim tries As Integer

                'Kalw synartish
                Eikona_3D(myApplication, oSavePath, folder, PictureBox3, tries)

                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' FLAT PATTERN CODE''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                ' A Side Definition '''' To swsto face exei mallon ( oUnitNormal.X = 1 and oUnitNormal.Y = 0 and oUnitNormal.Z = 0 )
                Dim TheFaceCounter As Integer

                'Kalw synartish gia unfold sto katalilo face
                UnfoldTheFace(myApplication, partComp, TheFaceCounter)

                'Exwntas kanei Unfold Pianw to antikimeno toy FlatPattern
                Dim fp As FlatPattern = myApplication.ActiveEditObject

                '''''Kanw Rotate to Fp an xriazete kai epistrefw ta oLength, oWidth kai to an telika peristrafike
                Dim oLength As Double
                Dim oWidth As Double
                Dim peristrafike As Boolean

                'Kanw Rotate To Fp an xriazete kai epistrefw ta oLength, oWidth kai to an telika peristrafike
                Rotate_Fp(partComp, Rip_precision, Diam, YpsosTomhs, fp, peristrafike, oLength, oWidth)

                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' YPOLOGISMOS EDGES '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                'Pianw ta Edges tou face kai epilegw 
                Dim fpBody As SurfaceBody = fp.SurfaceBodies(1)
                Dim oEdges As Edges = fpBody.Faces(TheFaceCounter).Edges
                Dim TheEdgeCounter As Integer
                Epilogh_Edge(oEdges, TheEdgeCounter)

                'Ypologizw to Face mou
                Dim myEdge As Edge
                myEdge = oEdges.Item(TheEdgeCounter)
                Dim xPoints(NumOfPoints - 1) As Double
                Dim yPoints(NumOfPoints - 1) As Double


                Ypologismos_Edge(myEdge, NumOfPoints, peristrafike, YpsosTomhs, xPoints, yPoints)
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

                For i = 0 To NumOfPoints - 1
                    xPointsValues(i) = xPoints(i)
                    yPointsValues(i) = yPoints(i)
                    xPoints(i) = Format(xPoints(i), "0.00")
                    yPoints(i) = Format(yPoints(i), "0.00")
                    'MsgBox(xPoints(i) & "  ,  " & yPoints(i))
                Next
                'Kai ta vazw strogylopoihmena sthn lista tis maskas
                Fill_List(NumOfPoints, xPoints, yPoints, ListView1)
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


                ''''''''''''''''''''''''''''''''''''''''' Kleinw fp, apothikeyw to part ksanaanoigw pleon gia na mporw na ksekinisw thn kataskeyh toy Vohthitikou ''''''''''''''''''''''''''''''''''''''

                Dim oSavePath1 As String = "C:\Inventor Saves\Inventor Saves Hidden\"
                Dim oSaveName1 As String = "test_R" & ".ipt"
                Dim sPartPath As String = oSavePath1 & oSaveName1


                ' Apothikeyw to part poy doylevw me onoma 'test_R.ipt' sto Inventor Saves Hidden kai to kleinw gia na mporw na ksekinisw thn kataskeyh toy Vohthitikou
                Restart(partComp, partDoc, myApplication, oSavePath1, oSaveName1)
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''






                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' Vohthitiko ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                ' Ksekinaw twra tin kataskeyh tou Vohthitikou gia ta xValus kai yValues pou tha exw sto .dwg (an exw ews 49 seimeia kai an den exw Real kateytheian)
                If (NumOfPoints <= maxSeires) And (PoiotikoGwnia Or PoiotikoPsilo Or PoiotikoKonto) Then
                    myApplication = GetObject(, "Inventor.Application")
                    myApplication.Documents.CloseAll()
                    myApplication.Documents.Add(Inventor.DocumentTypeEnum.kPartDocumentObject, myApplication.FileManager.GetTemplateFile(Inventor.DocumentTypeEnum.kPartDocumentObject), True)

                    partDoc = myApplication.ActiveDocument
                    partDoc.SubType = "{9C464203-9BAE-11D3-8BAD-0060B0CE6BB4}"
                    partComp = partDoc.ComponentDefinition


                    '' analoga me tin periptwsh kataskeyazw to analogo Vohthitiko
                    If PoiotikoKonto Or PoiotikoPsilo Then

                        Model_1_3D_Construction(myApplication, Diam, V_Ypsos, xYpsosTomhs, Gwnia, partDoc, partComp, Rip_precision)

                    ElseIf PoiotikoGwnia Then

                        Model_1_3D_Construction(myApplication, Diam, V_Ypsos, xYpsosTomhs, 70, partDoc, partComp, Rip_precision)
                    End If


                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' FLAT PATTERN CODE ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    ' A Side Definition '''' To swsto face exei mallon ( oUnitNormal.X = 1 and oUnitNormal.Y = 0 and oUnitNormal.Z = 0 )

                    'Kalw synartish gia unfold sto katalilo face
                    UnfoldTheFace(myApplication, partComp, TheFaceCounter)

                    'Exwntas kanei Unfold Pianw to antikimeno toy FlatPattern
                    fp = myApplication.ActiveEditObject


                    '''''Kanw Rotate to Fp an xriazete kai epistrefw ta oLength, oWidth kai to an telika peristrafike

                    Rotate_Fp(partComp, Rip_precision, Diam, xYpsosTomhs, fp, peristrafike, oLength, oWidth)

                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' YPOLOGISMOS EDGES '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    'Pianw ta Edges tou face kai epilegw
                    fpBody = fp.SurfaceBodies(1)
                    oEdges = fpBody.Faces(TheFaceCounter).Edges
                    Epilogh_Edge(oEdges, TheEdgeCounter)

                    'Ypologizw to Face mou
                    myEdge = oEdges.Item(TheEdgeCounter)
                    'Ta exw kanei dim panw se kathe periptwsh edw tha paroyn times epeita apo thn synartish
                    'Dim xPointsValues(NumOfPoints - 1) As Double
                    'Dim yPointsValues(NumOfPoints - 1) As Double

                    Ypologismos_Edge(myEdge, NumOfPoints, peristrafike, xYpsosTomhs, xPointsValues, yPointsValues)

                    'Telos YPOLOGISMOY exw parei ta xPointsValues ,yPointsValues pou tha xristw gia to Drawing

                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



                    ''''''''''''''''''''''''''''''''''''''''' Kleinw fp, apothikeyw to part kai to kleinw gia na mporw na ksekinisw kai na ksekinisw to IDW '''''''''''''''''''''''''''''''''''''''''''''''
                    oSaveName1 = "test_V" & ".ipt"
                    sPartPath = oSavePath1 & oSaveName1


                    ' Apothikeyw to part poy doylevw me onoma 'test.ipt' sto oSavePath1 kai to kleinw
                    Restart(partComp, partDoc, myApplication, oSavePath1, oSaveName1)
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                End If







                'maxSeires = 49
                If NumOfPoints <= maxSeires Then

                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' DRAWING CODE '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    ' Anoigw to swsto Voithitiko h to Pragmatiko .ipt. Pleon gia to drawing tha to exw me onoma oPartDoc
                    If PoiotikoGwnia Or PoiotikoPsilo Or PoiotikoKonto Then
                        oSaveName1 = "test_V" & ".ipt"
                        sPartPath = oSavePath1 & oSaveName1
                    Else
                        oSaveName1 = "test_R" & ".ipt"
                        sPartPath = oSavePath1 & oSaveName1
                    End If

                    Dim oPartDoc As PartDocument = myApplication.Documents.Open(sPartPath, True)
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


                    '''''''''''''''''''''''''''''''''''''''''' Drawing definition
                    ' Define drawing template
                    Dim sTemplateFile As String
                    sTemplateFile = myApplication.FileManager.GetTemplateFile(DocumentTypeEnum.kDrawingDocumentObject)

                    Dim oDrawDoc As DrawingDocument
                    oDrawDoc = myApplication.Documents.Add(DocumentTypeEnum.kDrawingDocumentObject, sTemplateFile)


                    ' The drawing is created with a single sheet,
                    ' so we'll add our views to it.
                    Dim oSheet As Sheet = oDrawDoc.Sheets.Item(1)

                    oSheet.Size = DrawingSheetSizeEnum.kA4DrawingSheetSize
                    '''''''''''''' Epilegw Portrait Orientation
                    oSheet.Orientation = PageOrientationTypeEnum.kPortraitPageOrientation



                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''' Flat Pattern View Anaptygmatos ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    ' Create a new NameValueMap object
                    Dim oBaseViewOptions As NameValueMap
                    oBaseViewOptions = myApplication.TransientObjects.CreateNameValueMap
                    ' Set the options to use when creating the base view.
                    oBaseViewOptions.Add("SheetMetalFoldedModel", False)

                    ' Now we define the placement points for
                    ' the drawing views we shall be adding to the sheet
                    Dim oTitlePoint1 As Point2d
                    Dim xP As Integer = 6.6
                    Dim yP As Integer = 22.5

                    Dim oPlacementPoint1 As Point2d
                    oPlacementPoint1 = myApplication.TransientGeometry.CreatePoint2d(xP, yP)

                    ' Define the view scales that we need
                    Dim ViewScale1 As Double
                    ViewScale1 = 10 / oLength

                    ' define the view orientation for each view
                    Dim ViewOrientation1 As ViewOrientationTypeEnum = ViewOrientationTypeEnum.kDefaultViewOrientation

                    ' define the view style for each view
                    Dim ViewStyle1 As DrawingViewStyleEnum = DrawingViewStyleEnum.kShadedHiddenLineDrawingViewStyle     ' kHiddenLineRemovedDrawingViewStyle

                    ' now create our two views
                    Dim oView As DrawingView
                    oView = oSheet.DrawingViews.AddBaseView(oPartDoc, oPlacementPoint1, ViewScale1, ViewOrientation1, ViewStyle1, , , oBaseViewOptions)



                    Dim oGeneralNoteTitles As GeneralNote
                    oTitlePoint1 = myApplication.TransientGeometry.CreatePoint2d(xP - 3.1, yP + oWidth * ViewScale1 / 2 + 1)
                    oGeneralNoteTitles = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTitlePoint1, "ΑΝΑΠΤΥΓΜΑ ΚΥΛΙΝΔΡΙΚΗΣ ΕΠΙΦΑΝΕΙΑΣ")
                    oGeneralNoteTitles.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & "ΑΝΑΠΤΥΓΜΑ ΚΥΛΙΝΔΡΙΚΗΣ ΕΠΙΦΑΝΕΙΑΣ" & "</StyleOverride>"


                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' Dimensions Anaptygmatos '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    Dim kath, oriz As Integer
                    kath = 2
                    oriz = 3
                    If oView.DrawingCurves.Count = 10 Then
                        kath = 4
                        oriz = 7
                    End If


                    'MsgBox(oView.DrawingCurves.Count)   vgazei 6 curves  (poly kala)
                    Dim finalCurve1, finalCurve2 As DrawingCurve
                    finalCurve1 = oView.DrawingCurves.Item(kath)      ' gia to katheto
                    finalCurve2 = oView.DrawingCurves.Item(oriz)      ' gia to orizontio

                    Dim oGeomIntent1 As GeometryIntent
                    Dim oGeomIntent2 As GeometryIntent

                    oGeomIntent1 = oSheet.CreateGeometryIntent(finalCurve1)   ' gia to katheto
                    oGeomIntent2 = oSheet.CreateGeometryIntent(finalCurve2)   ' gia to orizontio


                    Dim oGeneralDims As GeneralDimensions = oSheet.DrawingDimensions.GeneralDimensions
                    'Xtizw to Dimension tou orizontiou
                    Dim textPoint2 As Inventor.Point2d = myApplication.TransientGeometry.CreatePoint2d(finalCurve2.MidPoint.X, finalCurve2.MidPoint.Y - 0.6)
                    Dim myDimOrizontio As GeneralDimension
                    myDimOrizontio = oGeneralDims.AddLinear(textPoint2, oGeomIntent2)
                    myDimOrizontio.Text.FormattedText = "<DimensionValue/>" & " mm"

                    'Xtizw to Dimension tou kathetou
                    Dim textPoint1 As Inventor.Point2d = myApplication.TransientGeometry.CreatePoint2d(finalCurve1.MidPoint.X + 0.6, finalCurve1.MidPoint.Y) 'oView.Center.X + 5.5
                    Dim myDimKatheto As GeneralDimension
                    myDimKatheto = oGeneralDims.AddLinear(textPoint1, oGeomIntent1)

                    'An einai poiotiko kryvw to lathos value kai vazw to swsto alliws prosthetw apla to mm
                    If Not (PoiotikoGwnia Or PoiotikoKonto Or PoiotikoPsilo) Then
                        myDimKatheto.Text.FormattedText = "<DimensionValue/>" & " mm"
                    Else
                        'kryvw to lathos value kai dinw to swsto
                        myDimKatheto.HideValue = True
                        myDimKatheto.Text.FormattedText = Format(YpsosTomhs, "0.00") & " mm"
                    End If


                    '''''''''''''''''''''''''''''''''''''''''''''''' An to thelw gia kathe curve to Dimension (xwris na peirasei txt apla na ta vasei)
                    'Dim finalCurve As DrawingCurve
                    'Dim oGeneralDims As GeneralDimensions = oSheet.DrawingDimensions.GeneralDimensions
                    'Dim oGeomIntent1 As GeometryIntent
                    'For i = 1 To oView.DrawingCurves.Count
                    '    Try
                    '        finalCurve = oView.DrawingCurves.Item(i)

                    '        oGeomIntent1 = oSheet.CreateGeometryIntent(finalCurve)

                    '        Dim textPoint As Inventor.Point2d = myApplication.TransientGeometry.CreatePoint2d(oView.Center.X + 6, oView.Top - 5)
                    '        'Error on this line below
                    '        oGeneralDims.AddLinear(textPoint, oGeomIntent1)
                    '        MsgBox("skata")
                    '    Catch
                    '        'MsgBox("skata")
                    '    End Try
                    'Next
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' Katskeyh Systima Aksonwn '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    Dim oTG As TransientGeometry
                    oTG = myApplication.TransientGeometry

                    Dim xAxis, yAxis As Double
                    xAxis = -(oLength / 2)
                    yAxis = -(oWidth / 2)

                    AxisSystem(myApplication, oView, oTG, xAxis, yAxis, ViewScale1)

                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' 3D Model View ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    ' Create a new NameValueMap object
                    Dim oBaseViewOptions2 As NameValueMap
                    oBaseViewOptions2 = myApplication.TransientObjects.CreateNameValueMap
                    ' Set the options to use when creating the base view.
                    oBaseViewOptions2.Add("IncludeSurfaceBodies", False)

                    ' Now we define the placement points for
                    ' the two drawing views we shall be adding to the sheet
                    Dim xP2 As Integer = 17
                    Dim yP2 As Integer = 14.5
                    Dim oPlacementPoint2 As Point2d
                    oPlacementPoint2 = myApplication.TransientGeometry.CreatePoint2d(xP2, yP2)

                    ' define the view orientation for each view
                    Dim ViewOrientation2 As ViewOrientationTypeEnum = ViewOrientationTypeEnum.kIsoTopLeftViewOrientation

                    ' define the view style for each view
                    Dim ViewStyle2 As DrawingViewStyleEnum = DrawingViewStyleEnum.kShadedHiddenLineDrawingViewStyle

                    ' now create our two views
                    Dim oView2 As DrawingView
                    oView2 = oSheet.DrawingViews.AddBaseView(oPartDoc, oPlacementPoint2, ViewScale1, ViewOrientation2, ViewStyle2, , , oBaseViewOptions2)


                    Dim Factor3D As Double = 0
                    If oLength * 0.7 > oWidth Then
                        Factor3D = 0.35
                    End If

                    Dim oTitlePoint2 As Point2d
                    oTitlePoint2 = myApplication.TransientGeometry.CreatePoint2d(xP2 - 2.45, yP2 + oWidth * ViewScale1 / 2 + 0.55 + Factor3D)
                    oGeneralNoteTitles = oSheet.DrawingNotes.GeneralNotes.AddFitted(oTitlePoint2, "3D ΜΟΝΤΕΛΟ ΚΥΛΙΝΔΡΟΥ")
                    oGeneralNoteTitles.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextCenter
                    oGeneralNoteTitles.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & "3D ΜΟΝΤΕΛΟ ΚΥΛΙΝΔΡΟΥ" & "</StyleOverride>"

                    ''''''''''''''''''''''''''' 3d model Ypsos Dimension

                    ''''Dimension gia to katheto
                    Dim finalCurve3 As DrawingCurve
                    finalCurve3 = oView2.DrawingCurves.Item(2)

                    Dim oGeomIntent3 As GeometryIntent
                    oGeomIntent3 = oSheet.CreateGeometryIntent(finalCurve3)


                    Dim textPoint3 As Inventor.Point2d = myApplication.TransientGeometry.CreatePoint2d(oView2.DrawingCurves.Item(2).MidPoint.X - 1.3, oView2.DrawingCurves.Item(2).MidPoint.Y - Math.Tan(Math.PI / 6) * 1.3)
                    Dim myDim3DKatheto As GeneralDimension
                    myDim3DKatheto = oGeneralDims.AddLinear(textPoint3, oGeomIntent3)

                    'An einai poiotiko kryvw to lathos value kai vazw to swsto alliws prosthetw apla to (mm)
                    If Not (PoiotikoGwnia Or PoiotikoKonto Or PoiotikoPsilo) Then
                        myDim3DKatheto.Text.FormattedText = "<DimensionValue/>" & " mm"
                    Else
                        'kryvw to lathos value kai dinw to swsto
                        myDim3DKatheto.HideValue = True
                        myDim3DKatheto.Text.FormattedText = Format(YpsosTomhs, "0.00") & " mm"
                    End If



                    ''''''Dimension gia thn diametro tou 3D object
                    Dim finalCurve4 As DrawingCurve
                    finalCurve4 = oView2.DrawingCurves.Item(9)  ' gia thn diametro  (synolika exei 19)

                    Dim oGeomIntent4 As GeometryIntent
                    oGeomIntent4 = oSheet.CreateGeometryIntent(finalCurve4)

                    ' To curve  oView2.DrawingCurves.Item(2)  einai to xYpsosTomhs san curve object
                    Dim textPoint4 As Inventor.Point2d = myApplication.TransientGeometry.CreatePoint2d(xP2, oView2.DrawingCurves.Item(2).StartPoint.Y - 0.87)    ''''  ,yP2 - 5)
                    Dim myDim3DOrizontio As GeneralDimension
                    myDim3DOrizontio = oGeneralDims.AddDiameter(textPoint4, oGeomIntent4)
                    myDim3DOrizontio.Text.FormattedText = "<DimensionValue/>" & " mm"



                    '''''''''''''''''''''''prospatheia gia kathe DrawingCurves gia debuging
                    'Dim oGeneralDims As GeneralDimensions = oSheet.DrawingDimensions.GeneralDimensions
                    'For i = 1 To 19

                    '    Try
                    '        Dim finalCurve4 As DrawingCurve
                    '        finalCurve4 = oView2.DrawingCurves.Item(i)  ' gia thn diametro  (synolika exei 19)

                    '        Dim oGeomIntent4 As GeometryIntent
                    '        oGeomIntent4 = oSheet.CreateGeometryIntent(finalCurve4)

                    '        Dim textPoint4 As Inventor.Point2d = myApplication.TransientGeometry.CreatePoint2d(xP2, yP2 - 5)
                    '        Dim myDim3DOrizontio As GeneralDimension
                    '       myDim3DOrizontio = oGeneralDims.AddDiameter(textPoint4, oGeomIntent4)
                    '        myDim3DOrizontio.Text.FormattedText = "<DimensionValue/>" & " mm"

                    '        MsgBox(i)

                    '    Catch ex As Exception

                    '    End Try

                    'Next
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' Break Operation '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    Dim cutGap As Double
                    cutGap = 0.3

                    If PoiotikoGwnia Or PoiotikoKonto Or PoiotikoPsilo Then


                        ''''''''''''''''''''''''''''''''''''''''''''''''''  CUT sto Anaptygma
                        ''''vrisko to antistixo curve tou xYpsosTomhs
                        Dim xYpsosTomhsCurve As DrawingCurve
                        xYpsosTomhsCurve = oView.DrawingCurves.Item(kath)

                        'kai pairnw kata cutGap giro apo to .midPoint.y 
                        Dim oStartPoint As Point2d
                        oStartPoint = myApplication.TransientGeometry.CreatePoint2d(xYpsosTomhsCurve.MidPoint.X, xYpsosTomhsCurve.MidPoint.Y - cutGap / 2)

                        ' Define the end point of the break
                        Dim oEndPoint As Point2d
                        oEndPoint = myApplication.TransientGeometry.CreatePoint2d(xYpsosTomhsCurve.MidPoint.X, xYpsosTomhsCurve.MidPoint.Y + cutGap / 2)

                        Dim oBreakOperation As BreakOperation
                        oBreakOperation = oView.BreakOperations.Add(BreakOrientationEnum.kVerticalBreakOrientation, oStartPoint, oEndPoint, BreakStyleEnum.kStructuralBreakStyle, 8, (cutGap - 0.0001), 1, False)



                        ''''''''''''''''''''''''''''''''''''''''''''''''' 3DModel CUT
                        Dim provlima3Dgwnia As Double
                        provlima3Dgwnia = 0
                        Dim cutGap3D As Double
                        cutGap3D = cutGap
                        If PoiotikoGwnia Then
                            cutGap3D = 0.1
                            provlima3Dgwnia = 0.29
                        End If


                        'vrisko to antistixo curve tou xYpsosTomhs sto 3D model
                        Dim xYpsosTomhsCurve3D As DrawingCurve
                        xYpsosTomhsCurve3D = oView2.DrawingCurves.Item(2)

                        'kai pairnw kata cutGap giro apo to .midPoint.y 
                        ' Define the start point of the break
                        Dim oStartPoint2 As Point2d
                        oStartPoint2 = myApplication.TransientGeometry.CreatePoint2d(xYpsosTomhsCurve3D.MidPoint.X, xYpsosTomhsCurve3D.MidPoint.Y + provlima3Dgwnia - cutGap3D)

                        ' Define the end point of the break
                        Dim oEndPoint2 As Point2d
                        oEndPoint2 = myApplication.TransientGeometry.CreatePoint2d(xYpsosTomhsCurve3D.MidPoint.X, xYpsosTomhsCurve3D.MidPoint.Y + provlima3Dgwnia + cutGap3D)


                        Dim oBreakOperation2 As BreakOperation
                        oBreakOperation2 = oView2.BreakOperations.Add(BreakOrientationEnum.kVerticalBreakOrientation, oStartPoint2, oEndPoint2, BreakStyleEnum.kStructuralBreakStyle, 10, (cutGap3D - 0.0001), 1, False)



                    End If
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''




                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' Table ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    Dim mposiko As Double
                    mposiko = (10 - TotalHeight * ViewScale1 / 10) / 4
                    If PoiotikoGwnia Or PoiotikoKonto Or PoiotikoPsilo Then
                        mposiko = 0
                    End If

                    Dim xTablePosition As Double
                    xTablePosition = xP - 5
                    Dim yTablePosition As Double
                    yTablePosition = yP - 6.5 + mposiko
                    Dim xTableSpace As Double = 5.2    'se cm einai 4.8cm o prwtos kai o deyteros tha mpei praktika sto +0.2 cm 

                    Table_Construction(myApplication, oSheet, maxSeires, NumOfPoints, xPoints, yPoints, xTablePosition, yTablePosition, xTableSpace)

                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' Kovw se grammes to View tou Flat Pattern ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    'Dim oTG As TransientGeometry
                    oTG = myApplication.TransientGeometry

                    Dim oViewSketch As DrawingSketch = oView.Sketches.Add
                    oViewSketch.Edit()

                    For i = 0 To NumOfPoints - 1

                        If Not (PoiotikoGwnia Or PoiotikoKonto Or PoiotikoPsilo) Then

                            Try
                                oViewSketch.SketchLines.AddByTwoPoints(oTG.CreatePoint2d((xPointsValues(i) / 10) - (oLength / 2), -(oWidth / 2)), oTG.CreatePoint2d((xPointsValues(i) / 10) - (oLength / 2), (yPointsValues(i) / 10) - (oWidth / 2)))
                            Catch

                            End Try

                        Else
                            Dim xYpsosTomhsCurve1 As DrawingCurve
                            xYpsosTomhsCurve1 = oView.DrawingCurves.Item(kath)

                            Dim katwCut As Double 'to kanw dia ViewScale1 gia na to ferw stis diastaseis tou oView apo tou oSheet
                            katwCut = (Abs(xYpsosTomhsCurve1.StartPoint.Y - xYpsosTomhsCurve1.EndPoint.Y) / 2 - cutGap / 2) / ViewScale1

                            Try
                                oViewSketch.SketchLines.AddByTwoPoints(oTG.CreatePoint2d((xPointsValues(i) / 10) - (oLength / 2), -(oWidth / 2)), oTG.CreatePoint2d((xPointsValues(i) / 10) - (oLength / 2), (-(oWidth / 2) + katwCut)))
                                oViewSketch.SketchLines.AddByTwoPoints(oTG.CreatePoint2d((xPointsValues(i) / 10) - (oLength / 2), (-(oWidth / 2) + katwCut + cutGap / ViewScale1)), oTG.CreatePoint2d((xPointsValues(i) / 10) - (oLength / 2), (yPointsValues(i) / 10) - (oWidth / 2)))
                            Catch

                            End Try

                        End If

                    Next

                    oViewSketch.ExitEdit()
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''





                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' Apari8mw tis grammes ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    ' Gia to provlima pou to 1 mplekei me to systima akswnwn
                    Dim YpsosTomhsLengthSheet
                    YpsosTomhsLengthSheet = Abs(oView.DrawingCurves.Item(1).StartPoint.Y - oView.DrawingCurves.Item(1).EndPoint.Y)

                    Dim axisProblem As Boolean = False
                    If YpsosTomhsLengthSheet < 0.27 Then
                        axisProblem = True
                    End If

                    EpiloghArithmwn(Gwnia, NumOfPoints, oTG, ViewScale1, oPlacementPoint1, axisProblem, YpsosTomhsLengthSheet, xPointsValues, yPointsValues, oLength, oWidth, oSheet)
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''




                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' ΥΠΟΜΝΙΜΑ '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    Dim oTitleBlockDef As TitleBlockDefinition
                    oTitleBlockDef = oDrawDoc.TitleBlockDefinitions.Add("TUC MICRO Title Block")

                    Dim oSketchYpomnima As DrawingSketch
                    oTitleBlockDef.Edit(oSketchYpomnima)

                    Dim xPos As Double
                    xPos = 1

                    Dim KlimakaKampili As Boolean
                    KlimakaKampili = PoiotikoKonto Or PoiotikoPsilo

                    Dim Default_Micro_Path As String = defPath & "\micro.png"
                    Ypomnima(xPos, ViewScale1, PoiotikoGwnia, KlimakaKampili, Default_Micro_Path, oTG, oSketchYpomnima)
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''




                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' Prospatheia eikonas''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    Dim Defaul_Pic1_Path As String = defPath & "\Capture1.png"
                    'Dim LogoFilename1 As String = "C:\Users\antma\Desktop\Case 1\Interface1\picture1.png"
                    Dim oSketchImage1 As SketchImage
                    oSketchImage1 = oSketchYpomnima.SketchImages.Add(Defaul_Pic1_Path, oTG.CreatePoint2d(19 - 6 - 0.15, 27.7 - 0.15), False) 'to 6.5 = oSketchImage1.Width 
                    'set image size in cm
                    oSketchImage1.Height = 4
                    oSketchImage1.Width = 6
                    Dim xText As String = 19 - 0.2
                    Dim yText As String = 27.7 - 0.15 - oSketchImage1.Height - 0.15

                    '''''''''''''''''''''''''''''''''''''''''' Dedomena dipla apo foto
                    Dim oTextBox As TextBox
                    Dim sTextDedomena As String
                    sTextDedomena = "d = " & Format(Diam, "0.00") & " mm"
                    oTextBox = oSketchYpomnima.TextBoxes.AddFitted(oTG.CreatePoint2d(xText, yText), sTextDedomena)
                    oTextBox.VerticalJustification = VerticalTextAlignmentEnum.kAlignTextUpper
                    oTextBox.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextRight
                    oTextBox.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & sTextDedomena & "</StyleOverride>"

                    sTextDedomena = "h = " & Format(Ypsos, "0.00") & " mm"
                    oTextBox = oSketchYpomnima.TextBoxes.AddFitted(oTG.CreatePoint2d(xText, yText - 0.4), sTextDedomena)
                    oTextBox.VerticalJustification = VerticalTextAlignmentEnum.kAlignTextUpper
                    oTextBox.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextRight
                    oTextBox.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & sTextDedomena & "</StyleOverride>"

                    sTextDedomena = "l = " & Format(YpsosTomhs, "0.00") & " mm"
                    oTextBox = oSketchYpomnima.TextBoxes.AddFitted(oTG.CreatePoint2d(xText, yText - 0.8), sTextDedomena)
                    oTextBox.VerticalJustification = VerticalTextAlignmentEnum.kAlignTextUpper
                    oTextBox.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextRight
                    oTextBox.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & sTextDedomena & "</StyleOverride>"

                    sTextDedomena = "φ = " & Format(Gwnia, "0.00") & "°"
                    oTextBox = oSketchYpomnima.TextBoxes.AddFitted(oTG.CreatePoint2d(xText, yText - 1.2), sTextDedomena)
                    oTextBox.VerticalJustification = VerticalTextAlignmentEnum.kAlignTextUpper
                    oTextBox.HorizontalJustification = HorizontalTextAlignmentEnum.kAlignTextRight
                    oTextBox.FormattedText = "<StyleOverride FontSize = '" & 0.25 & "'>" & sTextDedomena & "</StyleOverride>"


                    oSheet.TitleBlock.Delete()
                    ' Add an instance of the title block definition to the sheet.
                    Dim oTitleBlock As TitleBlock
                    oTitleBlock = oSheet.AddTitleBlock(oTitleBlockDef, , )

                    oTitleBlockDef.ExitEdit()
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' Telos Ypomnimatos '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



                    '''''''''' Temp Apothikevseis twn .dwg kai .pdf
                    oDrawDoc.SaveAsInventorDWG(oSavePath1 & "test.dwg", True)
                    oDrawDoc.SaveAsInventorDWG(oSavePath1 & "test.pdf", True)
                    '''''''''' Kleinw ORISTIKA to oDrawDoc kai to oPartDoc
                    'oDrawDoc.Close(True)
                    'oPartDoc.Close(True)


                End If
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' Telos DWG CODE ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''




                '''''''''''''''''''''''''''''''''''''''''''''''''''' Enarksh Temp Apothikevsewn gia to .txt panta (oxi mono otan kanw dwg gia mexri 49 simeia) '''''''''''''''''''''''''''''''''''''''''
                '''''''''' Save txt file
                Dim path As String = oSavePath1 & "test.txt"
                If Not System.IO.File.Exists(path) Then
                    System.IO.File.Create(path).Dispose()
                End If

                System.IO.File.WriteAllText(path, "")
                Dim file As System.IO.StreamWriter
                file = My.Computer.FileSystem.OpenTextFileWriter(path, True)
                For i = 0 To NumOfPoints - 1
                    file.WriteLine("Point[" & i + 1 & "] = ( " & Format(xPoints(i), "0.00") & " ," & Format(yPoints(i), "0.00") & " )")
                Next
                file.Close()


                ''''''''''''''''''''''''' Hide the saves folder
                Dim objFSO As Object
                Dim objFolder As Object
                objFSO = CreateObject("Scripting.FileSystemObject")
                objFolder = objFSO.GetFolder("C:\Inventor Saves\Inventor Saves Hidden")
                Dim d As New System.IO.DirectoryInfo("C:\Inventor Saves\Inventor Saves Hidden")
                If Not (d.Attributes And IO.FileAttributes.Hidden) = IO.FileAttributes.Hidden Then
                    objFolder.Attributes = objFolder.Attributes Xor 2
                End If
                objFolder = Nothing
                objFSO = Nothing
                ''''''''''''''''''''''''' Hide the patterns picture folder
                objFSO = CreateObject("Scripting.FileSystemObject")
                objFolder = objFSO.GetFolder(folder)
                Dim d1 As New System.IO.DirectoryInfo(folder)
                If Not (d1.Attributes And IO.FileAttributes.Hidden) = IO.FileAttributes.Hidden Then
                    objFolder.Attributes = objFolder.Attributes Xor 2
                End If
                objFolder = Nothing
                objFSO = Nothing
                '''''''''''''''''''''''''' Telos Temp Apothikevsewn kai Hide fakelwn


            End If
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' TELOS IF NOT ERR '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


        Catch ex As Exception

            MsgBox("Κάτι πήγε στραβά. Προσπαθήστε ξανά.")
        End Try

    End Sub







    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        'Try_Save_Project(RichTextBox2, TextBox6, TextBox5, ListView1)
        Try

            Dim oBoolean As Boolean = True
            RichTextBox2.Clear()

            Dim oSaveName As String
            oSaveName = TextBox6.Text
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
                        If TextBox5.Text > 49 Then
                            My.Computer.FileSystem.CopyFile("C:\Inventor Saves\Inventor Saves Hidden\test_R.ipt", oSavePath & oSaveName & "\" & oSaveName & ".ipt")
                            My.Computer.FileSystem.CopyFile("C:\Inventor Saves\Inventor Saves Hidden\test.txt", oSavePath & oSaveName & "\" & oSaveName & ".txt")
                            RichTextBox2.AppendText("Επιτυχής αποθήκευση του project στον φάκελο C:\Inventor Saves\" & oSaveName)
                        Else
                            My.Computer.FileSystem.CopyFile("C:\Inventor Saves\Inventor Saves Hidden\test_R.ipt", oSavePath & oSaveName & "\" & oSaveName & ".ipt")
                            My.Computer.FileSystem.CopyFile("C:\Inventor Saves\Inventor Saves Hidden\test.dwg", oSavePath & oSaveName & "\" & oSaveName & ".dwg")
                            My.Computer.FileSystem.CopyFile("C:\Inventor Saves\Inventor Saves Hidden\test.pdf", oSavePath & oSaveName & "\" & oSaveName & ".pdf")
                            My.Computer.FileSystem.CopyFile("C:\Inventor Saves\Inventor Saves Hidden\test.txt", oSavePath & oSaveName & "\" & oSaveName & ".txt")
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





    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ''''' Default Path
        'Dim defPath As String
        'defPath = My.Application.Info.DirectoryPath


        ''''' Default Picture 1 in Default Path
        'Dim Defaul_Pic1_Path As String = defPath & "\picture1.png"
        'Dim pic1_Exists
        'pic1_Exists = Dir(Defaul_Pic1_Path)

        'If pic1_Exists = "" Then
        '    'MsgBox("The selected file doesn't exist")
        '    My.Resources.picture1.Save(Defaul_Pic1_Path)
        '    'Else
        '    '    MsgBox("The selected file exists")
        'End If



        ''''' MICRO PICTURE in Default Path
        'Dim Default_Micro_Path As String = defPath & "\micro.png"
        'Dim Micro_exists
        'Micro_exists = Dir(Default_Micro_Path)

        ''an den yparxei save ekei
        'If Micro_exists = "" Then

        '    My.Resources.micro.Save(Default_Micro_Path)
        'End If



    End Sub


    '' Isws xriastei prin klisei to gui (an to trexw san add-in) na epanaferw tha sheet metal defauls gia na min minoun etsi gia panta kathe pou anoigei to inventor
    'partComp.UnfoldMethod.kFactor = default_k_factor
    'partComp.SheetMetalStyles.Item(1).Activate()


End Class

