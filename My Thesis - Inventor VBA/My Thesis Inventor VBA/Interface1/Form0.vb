Public Class Form0


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim myForm As New Form1
        myForm.ShowDialog()

    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim myForm2 As New Form2
        myForm2.ShowDialog()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Dim myForm3 As New Form3
        myForm3.ShowDialog()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Dim myForm4 As New Form4
        myForm4.ShowDialog()
    End Sub


    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        Dim myForm5 As New Form5
        myForm5.ShowDialog()
    End Sub


    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        '''' Default Path
        Dim defPath As String
        defPath = My.Application.Info.DirectoryPath


        '''' Default Picture 1 in Default Path
        Dim Defaul_Pic1_Path As String = defPath & "\picture1.png"
        Dim pic1_Exists
        pic1_Exists = Dir(Defaul_Pic1_Path)

        If pic1_Exists = "" Then
            'MsgBox("The selected file doesn't exist")
            My.Resources.picture1.Save(Defaul_Pic1_Path)
            'Else
            '    MsgBox("The selected file exists")
        End If


        '''' Default Picture 1 in Default Path
        Dim Defaul_Pic2_Path As String = defPath & "\picture2.png"
        Dim pic2_Exists
        pic2_Exists = Dir(Defaul_Pic2_Path)

        If pic2_Exists = "" Then
            'MsgBox("The selected file doesn't exist")
            My.Resources.picture2.Save(Defaul_Pic2_Path)
            'Else
            '    MsgBox("The selected file exists")
        End If


        '''' MICRO PICTURE in Default Path
        Dim Default_Micro_Path As String = defPath & "\micro.png"
        Dim Micro_exists
        Micro_exists = Dir(Default_Micro_Path)

        'an den yparxei save ekei
        If Micro_exists = "" Then

            My.Resources.micro.Save(Default_Micro_Path)
        End If


        '''' Default Picture 1 in Default Path
        Dim Defaul_Pic11_Path As String = defPath & "\Capture1.png"
        Dim pic11_Exists
        pic11_Exists = Dir(Defaul_Pic11_Path)

        If pic11_Exists = "" Then
            'MsgBox("The selected file doesn't exist")
            My.Resources.Capture1.Save(Defaul_Pic11_Path)
            'Else
            '    MsgBox("The selected file exists")
        End If

        '''' Default Picture 2 in Default Path
        Dim Defaul_Pic22_Path As String = defPath & "\Capture2.png"
        Dim pic22_Exists
        pic22_Exists = Dir(Defaul_Pic22_Path)

        If pic22_Exists = "" Then
            'MsgBox("The selected file doesn't exist")
            My.Resources.Capture2.Save(Defaul_Pic22_Path)
            'Else
            '    MsgBox("The selected file exists")
        End If


        '''' Default Picture 3 in Default Path
        Dim Defaul_Pic33_Path As String = defPath & "\Capture3.png"
        Dim pic33_Exists
        pic33_Exists = Dir(Defaul_Pic33_Path)

        If pic33_Exists = "" Then
            'MsgBox("The selected file doesn't exist")
            My.Resources.Capture3.Save(Defaul_Pic33_Path)
            'Else
            '    MsgBox("The selected file exists")
        End If


        '''' Default Picture 4 in Default Path
        Dim Defaul_Pic44_Path As String = defPath & "\Capture4.png"
        Dim pic44_Exists
        pic44_Exists = Dir(Defaul_Pic44_Path)

        If pic44_Exists = "" Then
            'MsgBox("The selected file doesn't exist")
            My.Resources.Capture4.Save(Defaul_Pic44_Path)
            'Else
            '    MsgBox("The selected file exists")
        End If


        '''' Default Picture 5 in Default Path
        Dim Defaul_Pic55_Path As String = defPath & "\Capture5.png"
        Dim pic55_Exists
        pic55_Exists = Dir(Defaul_Pic55_Path)

        If pic55_Exists = "" Then
            'MsgBox("The selected file doesn't exist")
            My.Resources.Capture5.Save(Defaul_Pic55_Path)
            'Else
            '    MsgBox("The selected file exists")
        End If


    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        Dim webAddress As String = "https://www.m3.tuc.gr/"
        Process.Start(webAddress)

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

        Dim webAddress As String = "https://www.tuc.gr/index.php?id=4992"
        Process.Start(webAddress)

    End Sub
End Class