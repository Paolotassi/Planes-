Imports System.IO


Public Class CreateMessage


    '---------- CREAZIONE E INVIO DEL MESSAGGIO ----------'

    'Gestisce il bottone "Send"
    Private Sub ButtonSend_Click(sender As Object, e As EventArgs) Handles ButtonSend.Click
        LabelSure.Visible = True
        ButtonNo.Visible = True
        ButtonYes.Visible = True

    End Sub

    'Gestisce il bottone "No"
    Private Sub ButtonNo_Click(sender As Object, e As EventArgs) Handles ButtonNo.Click
        LabelSure.Visible = False
        ButtonNo.Visible = False
        ButtonYes.Visible = False

        DoSend = False
    End Sub

    'Gestisce il bottone "Yes"
    Private Sub ButtonYes_Click(sender As Object, e As EventArgs) Handles ButtonYes.Click
        NewFirstString()
        NewMessage()
        DoSend = True
    End Sub



    'Crea il nuovo messaggio
    Private Sub NewMessage()
        Dim LocalMessage As String
        LocalMessage = ""

        If CheckBox_PP.Checked Then
            LocalMessage += "pp"
            LocalMessage += TextBox_PP.Text
            LocalMessage += "$"
        End If
        If CheckBox_PI.Checked Then
            LocalMessage += "pi"
            LocalMessage += TextBox_PI.Text
            LocalMessage += "$"
        End If
        If CheckBox_PD.Checked Then
            LocalMessage += "pd"
            LocalMessage += TextBox_PD.Text
            LocalMessage += "$"
        End If
        If CheckBox_RP.Checked Then
            LocalMessage += "rp"
            LocalMessage += TextBox_RP.Text
            LocalMessage += "$"
        End If
        If CheckBox_RI.Checked Then
            LocalMessage += "ri"
            LocalMessage += TextBox_RI.Text
            LocalMessage += "$"
        End If
        If CheckBox_RD.Checked Then
            LocalMessage += "rd"
            LocalMessage += TextBox_RD.Text
            LocalMessage += "$"
        End If
        If CheckBox_YP.Checked Then
            LocalMessage += "yp"
            LocalMessage += TextBox_YP.Text
            LocalMessage += "$"
        End If
        If CheckBox_YI.Checked Then
            LocalMessage += "yi"
            LocalMessage += TextBox_YI.Text
            LocalMessage += "$"
        End If
        If CheckBox_YD.Checked Then
            LocalMessage += "yd"
            LocalMessage += TextBox_YD.Text
            LocalMessage += "$"
        End If
        If CheckBox_F.Checked Then
            LocalMessage += "fq"
            LocalMessage += TextBox_F.Text
            LocalMessage += "$"
        End If
        LocalMessage += "_"

        Message = LocalMessage
    End Sub

    Private Sub CreateMessage_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub



    '---------- CHECKBOXES ----------'

    Private Sub CheckBox_PP_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_PP.CheckedChanged
        If CheckBox_PP.Checked Then
            TextBox_PP.Enabled = True
        Else
            TextBox_PP.Enabled = False
        End If
    End Sub

    Private Sub CheckBox_PI_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_PI.CheckedChanged
        If CheckBox_PI.Checked Then
            TextBox_PI.Enabled = True
        Else
            TextBox_PI.Enabled = False
        End If
    End Sub

    Private Sub CheckBox_PD_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_PD.CheckedChanged
        If CheckBox_PD.Checked Then
            TextBox_PD.Enabled = True
        Else
            TextBox_PD.Enabled = False
        End If
    End Sub

    Private Sub CheckBox_RP_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_RP.CheckedChanged
        If CheckBox_RP.Checked Then
            TextBox_RP.Enabled = True
        Else
            TextBox_RP.Enabled = False
        End If
    End Sub

    Private Sub CheckBox_RI_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_RI.CheckedChanged
        If CheckBox_RI.Checked Then
            TextBox_RI.Enabled = True
        Else
            TextBox_RI.Enabled = False
        End If
    End Sub

    Private Sub CheckBox_RD_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_RD.CheckedChanged
        If CheckBox_RD.Checked Then
            TextBox_RD.Enabled = True
        Else
            TextBox_RD.Enabled = False
        End If
    End Sub

    Private Sub CheckBox_YP_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_YP.CheckedChanged
        If CheckBox_YP.Checked Then
            TextBox_YP.Enabled = True
        Else
            TextBox_YP.Enabled = False
        End If
    End Sub

    Private Sub CheckBox_YI_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_YI.CheckedChanged
        If CheckBox_YI.Checked Then
            TextBox_YI.Enabled = True
        Else
            TextBox_YI.Enabled = False
        End If
    End Sub

    Private Sub CheckBox_YD_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_YD.CheckedChanged
        If CheckBox_YD.Checked Then
            TextBox_YD.Enabled = True
        Else
            TextBox_YD.Enabled = False
        End If
    End Sub

    Private Sub CheckBox_F_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_F.CheckedChanged
        If CheckBox_F.Checked Then
            TextBox_F.Enabled = True
        Else
            TextBox_F.Enabled = False
        End If
    End Sub



    '---------- NEW FLIGHT PLAN ----------'
    'Queste 3 controllano i checkbox
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_Insert.CheckedChanged
        If CheckBox_Insert.Checked Then
            TextBox_Ins_After.Enabled = True
            CheckBox_Replace.Checked = False
            CheckBox_Delete.Checked = False
        Else
            TextBox_Ins_After.Enabled = False
        End If
    End Sub
    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_Replace.CheckedChanged
        If CheckBox_Replace.Checked Then
            TextBox_Replace_From.Enabled = True
            TextBox_Replace_To.Enabled = True
            Label_R_From.Enabled = True
            Label_R_To.Enabled = True
            CheckBox_Insert.Checked = False
            CheckBox_Delete.Checked = False
        Else
            TextBox_Replace_From.Enabled = False
            TextBox_Replace_To.Enabled = False
            Label_R_From.Enabled = False
            Label_R_To.Enabled = False
        End If
    End Sub
    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_Delete.CheckedChanged
        If CheckBox_Delete.Checked Then
            TextBox_Delete_From.Enabled = True
            TextBox_Delete_To.Enabled = True
            Label_D_From.Enabled = True
            Label_D_To.Enabled = True
            CheckBox_Insert.Checked = False
            CheckBox_Replace.Checked = False
        Else
            TextBox_Delete_From.Enabled = False
            TextBox_Delete_To.Enabled = False
            Label_D_From.Enabled = False
            Label_D_To.Enabled = False
        End If
    End Sub

    'apre Flight Plan Editor
    Private Sub Button_Open_FPE_Click(sender As Object, e As EventArgs) Handles Button_Open_FPE.Click
        'Apri la finestra
        Dim fpe As New FlightPlanEditor
        fpe.TopMost = True
        fpe.Show()
        New_Flight_Plan = New String(FP_Dimension) {}
        'Inizializza gli array di stringhe
        For i = 0 To FP_Dimension Step 1
            New_Flight_Plan(i) = ""
        Next
    End Sub

    'Gestisce il bottone "Load Flight Plan"
    Private Sub Button_Load_FP_Click(sender As Object, e As EventArgs) Handles Button_FP_Load.Click

        Dim Local_Flight_Plan As String()
        Local_Flight_Plan = New String(FP_Dimension) {}
        Dim IsFileValid As Boolean
        IsFileValid = True
        'Apre il file e lo trasforma nelle stringhe di Local_Flight_Plan
        Dim OpenFileDialog1 As New OpenFileDialog
        Dim FileS As FileStream

        Dim j As Integer

        OpenFileDialog1.ShowDialog()
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then

            ListView_WP.Items.Clear()
            ListView_WP.Items.Add("Flight plan Not found")

            Dim fileName As String
            fileName = OpenFileDialog1.FileName

            FileS = New FileStream(fileName, FileMode.Open, FileAccess.Read)
            Dim sR As New StreamReader(FileS)
            sR.BaseStream.Seek(0, SeekOrigin.Begin)


            Dim StopLoop As Boolean
            j = 0
            StopLoop = False
            While j <= FP_Dimension And StopLoop = False

                Local_Flight_Plan(j) = sR.ReadLine()
                ' New_Flight_Plan(j + 1) = Local_Flight_Plan(j)

                If Local_Flight_Plan(j).Equals("End") Then
                    StopLoop = True
                ElseIf Not Local_Flight_Plan(j).Contains("$") OrElse Not Local_Flight_Plan(j).ElementAt(Local_Flight_Plan(j).Length - 1) = "$" Then
                    StopLoop = True
                    IsFileValid = False

                Else
                    j += 1

                End If

                ' j += 1

            End While
            sR.Close()

        End If

        If IsFileValid = True Then


            Dim z As ListViewItem

            ListView_WP.Items.Clear()


            For Each s As String In Local_Flight_Plan
                z = New ListViewItem(s)
                ListView_WP.Items.Add(z)

                If s.Equals("End") Then
                    Exit For
                End If

            Next



            Dim i As Integer
            i = 0

            ' New_Flight_Plan(i) = ""  'La prima stringa viene creata quando si preme "Send"

            Do While Not Local_Flight_Plan(i).Equals("End")
                New_Flight_Plan(i + 1) = Local_Flight_Plan(i)
                i += 1
            Loop
            New_Flight_Plan(i + 1) = Local_Flight_Plan(i)   'passa anche "End"

            Label_IF.Text = "Done"
            Label_IF.Visible = True



        Else
            Label_IF.Text = "Error: Invalid file"
            Label_IF.Visible = True
        End If






    End Sub

    'Inserisce la prima stringa di "New Flight Plan"
    Private Sub NewFirstString()
        Dim LocalString As String
        LocalString = ""

        If CheckBox_Insert.Checked Then
            LocalString += "in"
            LocalString += TextBox_Ins_After.Text
            LocalString += "$"
        ElseIf CheckBox_Replace.Checked Then
            LocalString += "re"
            LocalString += "fr"
            LocalString += TextBox_Replace_From.Text
            LocalString += "to"
            LocalString += TextBox_Replace_To.Text
            LocalString += "$"
        ElseIf CheckBox_Delete.Checked Then
            LocalString += "de"
            LocalString += "fr"
            LocalString += TextBox_Delete_From.Text
            LocalString += "to"
            LocalString += TextBox_Delete_To.Text
            LocalString += "$"
        Else
            LocalString = "Not"
        End If

        New_Flight_Plan(0) = LocalString

    End Sub


End Class