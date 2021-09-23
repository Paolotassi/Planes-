
Imports System.IO
Public Class Create_Flight_Plan

    Const FP_Dimension = 1000  'Dimensione del piano di volo

    Dim Current_Instruction As String       'contiene l'istruzione corrente e serve a passare alcune condizioni al pulsante "yes"
    Dim Overview_String As String           'contiene l'elemento corrente che verrà rappresentato in Overview 
    Dim Overview_Backup As String()         'contiene la versione "facile" su cui lavorare dell'overview
    Dim Flight_Plan As String()             'contiene il piano di volo completo
    Dim Indice As Integer                    'contiene il numero di istruzioni presenti nel piano di volo

    'Azzera tutte le caselle di testo delle istruzioni e i pulsanti
    'N.B.: non resetta "Indice"
    Public Sub Reset_Input_Values()

        TextBox_Instruction_1.Visible = False
        TextBox_Instruction_2.Visible = False
        TextBox_Instruction_3.Visible = False
        TextBox_Instruction_4.Visible = False
        TextBox_Instruction_5.Visible = False
        TextBox_Instruction_6.Visible = False
        Label_Instruction_1.Visible = True
        Label_Instruction_2.Visible = False
        Label_Instruction_3.Visible = False
        Label_Instruction_4.Visible = False
        Label_Instruction_5.Visible = False
        Label_Instruction_6.Visible = False
        Button_Instruction_Add.Enabled = False
        Button_Instruction_Replace.Enabled = False
        Label_Sure.Visible = False
        Button_No.Visible = False
        Button_Yes.Visible = False

        Label_Instruction_1.Text = "Create a valid instruction first"

        ComboBox_Instruction_Type_1.Items.Clear()
        ComboBox_Instruction_Type_2.Items.Clear()

        ComboBox_Instruction_Type_1.Items.Add("Reach waypoint")
        ComboBox_Instruction_Type_1.Items.Add("Change engine power")
        ComboBox_Instruction_Type_1.Items.Add("Reach angle")
        ComboBox_Instruction_Type_1.Items.Add("Keep going until condition")

        TextBox_Instruction_1.Text = "0"
        TextBox_Instruction_2.Text = "0"
        TextBox_Instruction_3.Text = "0"
        TextBox_Instruction_4.Text = "0"
        TextBox_Instruction_5.Text = "0"
        TextBox_Instruction_6.Text = "0"

    End Sub

    'Crea l'istruzione come definita
    Public Sub Create_Instruction()

        Select Case (ComboBox_Instruction_Type_1.SelectedItem)
            Case "Reach waypoint"
                Current_Instruction = "wa"
                Overview_String = "Reach these coordinates: "
            Case "Change engine power"
                Current_Instruction = "po"
                Overview_String = "Engine power goes to "
            Case "Reach angle"
                Current_Instruction = "an"
                Overview_String = "Reach a "
            Case "Keep going until condition"
                Current_Instruction = "co"
                Overview_String = "Keep going until "
        End Select

        Select Case (ComboBox_Instruction_Type_2.SelectedItem)
            Case "Latitude, Longitude, Altitude"

                Current_Instruction += "la"     'aggiunge la latitudine all'istruzione
                Current_Instruction += TextBox_Instruction_1.Text
                Current_Instruction += "lo"     'aggiunge la longitudine
                Current_Instruction += TextBox_Instruction_2.Text
                Current_Instruction += "al"     'aggiunge l'altitudine
                Current_Instruction += TextBox_Instruction_3.Text

                Overview_String += "Lat(N): "
                Overview_String += TextBox_Instruction_1.Text
                Overview_String += " Long(E): "
                Overview_String += TextBox_Instruction_2.Text
                Overview_String += " Alt: "
                Overview_String += TextBox_Instruction_3.Text

            Case "Power %"
                Current_Instruction += "po"
                Current_Instruction += TextBox_Instruction_1.Text 'aggiunge l'altitudine

                Overview_String += "Engine power goes to "
                Overview_String += TextBox_Instruction_1.Text
                Overview_String += " %"

            Case "Pitch"

                Current_Instruction += "pi"
                Current_Instruction += TextBox_Instruction_1.Text 'aggiunge il beccheggio

                Overview_String += "pitch angle of "
                Overview_String += TextBox_Instruction_1.Text
                Overview_String += "°"

            Case "Roll"

                Current_Instruction += "ro"
                Current_Instruction += TextBox_Instruction_1.Text 'aggiunge il rollio

                Overview_String += "roll angle of "
                Overview_String += TextBox_Instruction_1.Text
                Overview_String += "°"

            Case "Yaw (deviation from current angle)"

                Current_Instruction += "ya"
                Current_Instruction += TextBox_Instruction_1.Text 'aggiunge l'imbardata

                Overview_String += "yaw angle of "
                Overview_String += TextBox_Instruction_1.Text
                Overview_String += "°"

            Case "Reached pitch angle"

                Current_Instruction += "pi"
                Current_Instruction += TextBox_Instruction_1.Text 'aggiunge il beccheggio

                Overview_String += "a pitch angle of "
                Overview_String += TextBox_Instruction_1.Text
                Overview_String += "° is reached"

            Case "Reached roll angle"

                Current_Instruction += "ro"
                Current_Instruction += TextBox_Instruction_1.Text 'aggiunge il rollio

                Overview_String += "a roll angle of "
                Overview_String += TextBox_Instruction_1.Text
                Overview_String += "° is reached"

            Case "Reached yaw angle"

                Current_Instruction += "ya"
                Current_Instruction += TextBox_Instruction_1.Text 'aggiunge l'imbardata

                Overview_String += "a yaw angle of "
                Overview_String += TextBox_Instruction_1.Text
                Overview_String += "° is reached"

            Case "Reached Latitude"

                Current_Instruction += "la"
                Current_Instruction += TextBox_Instruction_1.Text 'aggiunge la latitudine

                Overview_String += "a latitude of "
                Overview_String += TextBox_Instruction_1.Text
                Overview_String += "°N is reached"

            Case "Reached Longitude"

                Current_Instruction += "lo"
                Current_Instruction += TextBox_Instruction_1.Text 'aggiunge la longitudine

                Overview_String += "a longitude of "
                Overview_String += TextBox_Instruction_1.Text
                Overview_String += "°E is reached"

            Case "Altitude"

                Current_Instruction += "al"
                Current_Instruction += TextBox_Instruction_1.Text 'aggiunge l'altitudine

                Overview_String += "an altitude of "
                Overview_String += TextBox_Instruction_1.Text
                Overview_String += "m is reached"

            Case "Time since last instruction"

                Current_Instruction += "ti"
                Current_Instruction += TextBox_Instruction_1.Text 'aggiunge il tempo

                Overview_String += "a time of "
                Overview_String += TextBox_Instruction_1.Text
                Overview_String += "s is passed"

        End Select

        Current_Instruction += "$"

    End Sub

    'Stampa i valori dell'istruzione
    Public Sub Print_Instruction()

        'aggiunge la stringa al piano di volo
        Flight_Plan(Indice) = Current_Instruction

        'aggiunge la stringa all'overview
        Dim x As String() = New String(2) {}
        Dim y As ListViewItem
        x(0) = NumericUpDown_Instruction_Number.Text
        x(1) = Overview_String
        y = New ListViewItem(x)
        ListView_Overview.Items.Add(y)
        Overview_Backup(Indice) = Overview_String



        'aggiorna l'indice dell'istruzione seguente
        Indice += 1
        NumericUpDown_Instruction_Number.Value = Indice



        Reset_Input_Values()

    End Sub
    'Setup della finesta
    'Crea la prima parte della lista di istruzioni possibili
    Private Sub Create_Flight_Plan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Reset_Input_Values()
        ListView_Overview.View = View.Details
        ListView_Overview.Columns.Add("Number", 60)
        ListView_Overview.Columns.Add("Instruction", 600)
        Indice = 0
        NumericUpDown_Instruction_Number.Value = 0
        Flight_Plan = New String(FP_Dimension) {}
        Overview_Backup = New String(FP_Dimension) {}

        'Inizializza gli array di stringhe
        For i = 0 To FP_Dimension Step 1
            Flight_Plan(i) = ""
            Overview_Backup(i) = ""
        Next
    End Sub

    'Crea la seconda parte della lista di istruzioni possibili
    Private Sub ComboBox_Instruction_Type_1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox_Instruction_Type_1.SelectedIndexChanged

        ComboBox_Instruction_Type_2.Items.Clear()

        If ComboBox_Instruction_Type_1.SelectedItem = "Reach waypoint" Then

            ComboBox_Instruction_Type_2.Items.Add("Latitude, Longitude, Altitude")
            ComboBox_Instruction_Type_2.SelectedItem = ("Latitude, Longitude, Altitude")

        ElseIf ComboBox_Instruction_Type_1.SelectedItem = "Change engine power" Then

            ComboBox_Instruction_Type_2.Items.Add("Power %")
            ComboBox_Instruction_Type_2.SelectedItem = ("Power %")

        ElseIf ComboBox_Instruction_Type_1.SelectedItem = "Reach angle" Then

            ComboBox_Instruction_Type_2.Items.Add("Pitch")
            ComboBox_Instruction_Type_2.Items.Add("Roll")
            ComboBox_Instruction_Type_2.Items.Add("Yaw (deviation from current angle)")
            ComboBox_Instruction_Type_2.SelectedItem = ("Pitch")

        ElseIf ComboBox_Instruction_Type_1.SelectedItem = "Keep going until condition" Then

            ComboBox_Instruction_Type_2.Items.Add("Reached pitch angle")
            ComboBox_Instruction_Type_2.Items.Add("Reached roll angle")
            ComboBox_Instruction_Type_2.Items.Add("Reached yaw angle")
            ComboBox_Instruction_Type_2.Items.Add("Reached Latitude")
            ComboBox_Instruction_Type_2.Items.Add("Reached Longitude")
            ComboBox_Instruction_Type_2.Items.Add("Reached Altitude")
            ComboBox_Instruction_Type_2.Items.Add("Time since last instruction")
            ComboBox_Instruction_Type_2.SelectedItem = ("Time since last instruction")

        End If


    End Sub

    'Crea la casella dove inserire i dati per completare l'istruzione
    'Attiva i pulsanti "Add" e "Replace"
    Private Sub ComboBox_Instruction_Type_2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox_Instruction_Type_1.SelectedIndexChanged

        'Azzera tutte le caselle di testo delle istruzioni e i pulsanti
        TextBox_Instruction_1.Visible = False
        TextBox_Instruction_2.Visible = False
        TextBox_Instruction_3.Visible = False
        TextBox_Instruction_4.Visible = False
        TextBox_Instruction_5.Visible = False
        TextBox_Instruction_6.Visible = False
        Label_Instruction_1.Visible = False
        Label_Instruction_2.Visible = False
        Label_Instruction_3.Visible = False
        Label_Instruction_4.Visible = False
        Label_Instruction_5.Visible = False
        Label_Instruction_6.Visible = False
        Button_Instruction_Add.Enabled = True
        Button_Instruction_Replace.Enabled = True

        If ComboBox_Instruction_Type_1.SelectedItem = "Reach waypoint" Then

            TextBox_Instruction_1.Visible = True
            TextBox_Instruction_2.Visible = True
            TextBox_Instruction_3.Visible = True
            Label_Instruction_1.Visible = True
            Label_Instruction_2.Visible = True
            Label_Instruction_3.Visible = True

            Label_Instruction_1.Text = "Latitude  :"
            Label_Instruction_2.Text = "Longitude :"
            Label_Instruction_3.Text = "Altitude  :"

        ElseIf ComboBox_Instruction_Type_1.SelectedItem = "Change engine power" Then

            TextBox_Instruction_1.Visible = True
            Label_Instruction_1.Visible = True

            Label_Instruction_1.Text = "Power % :"

        ElseIf ComboBox_Instruction_Type_1.SelectedItem = "Reach angle" Then

            TextBox_Instruction_1.Visible = True
            Label_Instruction_1.Visible = True

            Label_Instruction_1.Text = "Angle :"


        ElseIf ComboBox_Instruction_Type_1.SelectedItem = "Keep going until condition" Then

            TextBox_Instruction_1.Visible = True
            Label_Instruction_1.Visible = True

            Label_Instruction_1.Text = "Condition value :"

        End If



    End Sub

    'Gestisce la funzione "Add"
    Private Sub Button_Instruction_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Instruction_Add.Click

        Create_Instruction()
        Print_Instruction()
        TextBox1.Text = Flight_Plan(Indice - 1)
        Button_Load.Enabled = False

    End Sub

    'Gestisce la funzione "Replace"
    Private Sub Button_Instruction_Replace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Instruction_Replace.Click
        Current_Instruction = "Replace"

        Label_Sure.Visible = True
        Button_No.Visible = True
        Button_Yes.Visible = True
    End Sub

    'Gestisce la funzione "Delete"
    Private Sub Button_Instruction_Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Instruction_Delete.Click
        Current_Instruction = "Delete"

        Label_Sure.Visible = True
        Button_No.Visible = True
        Button_Yes.Visible = True
    End Sub

    'Gestisce la funzione "Yes"
    Private Sub Button_Yes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Yes.Click

        Dim i As Int32
        Dim x As String() = New String(2) {}
        Dim y As ListViewItem

        If Current_Instruction = "Delete" AndAlso (NumericUpDown_Instruction_Number.Value <= Indice) Then

            'Sistema Overview_Backup
            For i = (NumericUpDown_Instruction_Number.Value) To (Indice - 1) Step 1
                Overview_Backup(i) = Overview_Backup(i + 1)
                Flight_Plan(i) = Flight_Plan(i + 1)
            Next
            Overview_Backup(Indice) = ""
            Flight_Plan(Indice) = ""


            Reset_Input_Values()
            Indice -= 1

            'Riscrivi Overview 
            ListView_Overview.Items.Clear()
            For i = 0 To (Indice - 1) Step 1
                x(0) = i.ToString
                x(1) = Overview_Backup(i)
                y = New ListViewItem(x)
                ListView_Overview.Items.Add(y)
            Next


        ElseIf Current_Instruction = "Replace" Then

            Create_Instruction()

            'Sistema Overview_Backup
            Overview_Backup(NumericUpDown_Instruction_Number.Value) = Overview_String
            Flight_Plan(NumericUpDown_Instruction_Number.Value) = Current_Instruction

            TextBox1.Text = Flight_Plan(NumericUpDown_Instruction_Number.Value)

            'Riscrivi Overview 
            ListView_Overview.Items.Clear()
            For i = 0 To (Indice - 1) Step 1
                x(0) = i.ToString
                x(1) = Overview_Backup(i)
                y = New ListViewItem(x)
                ListView_Overview.Items.Add(y)

            Next



            Reset_Input_Values()

        End If


    End Sub

    'Gestisce la funzione "No"
    Private Sub Button_No_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_No.Click

        Reset_Input_Values()

    End Sub

    'Gestisce la funzione "Save"
    Private Sub Button_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Save.Click

        Dim myStream As New MemoryStream()
        Dim fileStream As FileStream
        Dim writer As StreamWriter
        Dim saveFileDialog1 As New SaveFileDialog()


        ' writer = New MemoryStream(Encoding.UTF8.GetBytes(Overview_Backup))

        saveFileDialog1.FileName = "Flight_Plan"
        saveFileDialog1.DefaultExt = "txt"
        saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        saveFileDialog1.FilterIndex = 2
        saveFileDialog1.RestoreDirectory = True

        writer = New StreamWriter(myStream)

        For Each s As String In Flight_Plan

            'If s IsNot "" Then

            Current_Instruction = s
            writer.WriteLine(Current_Instruction)

            'End If

        Next

        'Dim i As Integer
        'i = 0
        'Do While i < Indice

        '    Current_Instruction = Flight_Plan(i)
        '    'Current_Instruction += "\n"
        '    writer.WriteLine(Current_Instruction)

        '    i += 1

        'Loop





        If saveFileDialog1.ShowDialog() = DialogResult.OK Then
            fileStream = saveFileDialog1.OpenFile()

            TextBox1.Text = myStream.ToString()

            myStream.Position = 0
            myStream.WriteTo(fileStream)

            fileStream.Close()

        End If



    End Sub

    'Gestisce la funzione "Load"
    Private Sub Button_Load_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Load.Click

        'Apre il file e lo trasforma nelle stringhe di Flight_Plan
        Dim OpenFileDialog1 As New OpenFileDialog

        Dim FileS As FileStream


        OpenFileDialog1.ShowDialog()
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim fileName As String
            fileName = OpenFileDialog1.FileName

            FileS = New FileStream(fileName, FileMode.Open, FileAccess.Read)
            Dim sR As New StreamReader(FileS)
            'Using i As New StreamReader(FileS)
            sR.BaseStream.Seek(0, SeekOrigin.Begin)

            Dim j As Integer
            j = 0
            While j <= FP_Dimension
                'While i.Peek() > -1
                Flight_Plan(j) = sR.ReadLine()
                TextBox1.Text += Flight_Plan(j)

                j += 1


            End While
            sR.Close()



        End If

        'Passa da Flight_Plan a Overview_Backup 
        Dim Part As String
        Dim i, x, y As Integer
        i = 0
        Do While Flight_Plan(i) IsNot "" AndAlso i <= FP_Dimension

            Part = Flight_Plan(i)(0) + Flight_Plan(i)(1)
            Select Case (Part)
                Case "wa"
                    Overview_String = "Reach these coordinates: "
                    Overview_String += "Lat(N): "

                    x = 2
                    'Trova una cifra
                    Do While Asc(Flight_Plan(i)(x)) < 47 Or Asc(Flight_Plan(i)(x)) > 58
                        x += 1
                    Loop

                    y = x
                    'Passa le cifre a Part
                    Part = ""
                    Do While Asc(Flight_Plan(i)(y)) > 47 And Asc(Flight_Plan(i)(y)) < 58
                        Part += Flight_Plan(i)(y)
                        y += 1
                    Loop

                    Overview_String += Part

                    Overview_String += " Long(E): "

                    x = y
                    'Trova una cifra
                    Do While Asc(Flight_Plan(i)(x)) < 47 Or Asc(Flight_Plan(i)(x)) > 58
                        x += 1
                    Loop

                    y = x
                    'Passa le cifre a Part
                    Part = ""
                    Do While Asc(Flight_Plan(i)(y)) > 47 And Asc(Flight_Plan(i)(y)) < 58
                        Part += Flight_Plan(i)(y)
                        y += 1
                    Loop

                    Overview_String += Part

                    Overview_String += " Alt: "

                    x = y
                    'Trova una cifra
                    Do While Asc(Flight_Plan(i)(x)) < 47 Or Asc(Flight_Plan(i)(x)) > 58
                        x += 1
                    Loop

                    y = x
                    'Passa le cifre a Part
                    Part = ""
                    Do While Asc(Flight_Plan(i)(y)) > 47 And Asc(Flight_Plan(i)(y)) < 58
                        Part += Flight_Plan(i)(y)
                        y += 1
                    Loop

                    Overview_String += Part

                Case "po"
                    Overview_String = "Engine power goes to "

                    x = 2
                    'Trova una cifra
                    Do While Asc(Flight_Plan(i)(x)) < 47 Or Asc(Flight_Plan(i)(x)) > 58
                        x += 1
                    Loop

                    y = x
                    'Passa le cifre a Part
                    Part = ""
                    Do While Asc(Flight_Plan(i)(y)) > 47 And Asc(Flight_Plan(i)(y)) < 58
                        Part += Flight_Plan(i)(y)
                        y += 1
                    Loop

                    Overview_String += Part

                    Overview_String += " %"

                Case "an"
                    Overview_String = "Reach a "

                    Part = Flight_Plan(i)(2) + Flight_Plan(i)(3)

                    Select Case (Part)
                        Case "pi"
                            Overview_String += "pitch angle of "

                            x = 2
                            'Trova una cifra
                            Do While Asc(Flight_Plan(i)(x)) < 47 Or Asc(Flight_Plan(i)(x)) > 58
                                x += 1
                            Loop

                            y = x
                            'Passa le cifre a Part
                            Part = ""
                            Do While Asc(Flight_Plan(i)(y)) > 47 And Asc(Flight_Plan(i)(y)) < 58
                                Part += Flight_Plan(i)(y)
                                y += 1
                            Loop

                            Overview_String += Part

                            Overview_String += "°"

                        Case "ro"
                            Overview_String += "roll angle of "

                            x = 2
                            'Trova una cifra
                            Do While Asc(Flight_Plan(i)(x)) < 47 Or Asc(Flight_Plan(i)(x)) > 58
                                x += 1
                            Loop

                            y = x
                            'Passa le cifre a Part
                            Part = ""
                            Do While Asc(Flight_Plan(i)(y)) > 47 And Asc(Flight_Plan(i)(y)) < 58
                                Part += Flight_Plan(i)(y)
                                y += 1
                            Loop

                            Overview_String += Part

                            Overview_String += "°"

                        Case "ya"
                            Overview_String += "yaw angle of "

                            x = 2
                            'Trova una cifra
                            Do While Asc(Flight_Plan(i)(x)) < 47 Or Asc(Flight_Plan(i)(x)) > 58
                                x += 1
                            Loop

                            y = x
                            'Passa le cifre a Part
                            Part = ""
                            Do While Asc(Flight_Plan(i)(y)) > 47 And Asc(Flight_Plan(i)(y)) < 58
                                Part += Flight_Plan(i)(y)
                                y += 1
                            Loop

                            Overview_String += Part

                            Overview_String += "°"
                    End Select



                Case "co"
                    Overview_String = "Keep going until "

                    Part = Flight_Plan(i)(2) + Flight_Plan(i)(3)

                    Select Case (Part)
                        Case "pi"
                            Overview_String += "a pitch angle of "

                            x = 2
                            'Trova una cifra
                            Do While Asc(Flight_Plan(i)(x)) < 47 Or Asc(Flight_Plan(i)(x)) > 58
                                x += 1
                            Loop

                            y = x
                            'Passa le cifre a Part
                            Part = ""
                            Do While Asc(Flight_Plan(i)(y)) > 47 And Asc(Flight_Plan(i)(y)) < 58
                                Part += Flight_Plan(i)(y)
                                y += 1
                            Loop

                            Overview_String += Part

                            Overview_String += "° is reached"

                        Case "ro"
                            Overview_String += "a roll angle of "

                            x = 2
                            'Trova una cifra
                            Do While Asc(Flight_Plan(i)(x)) < 47 Or Asc(Flight_Plan(i)(x)) > 58
                                x += 1
                            Loop

                            y = x
                            'Passa le cifre a Part
                            Part = ""
                            Do While Asc(Flight_Plan(i)(y)) > 47 And Asc(Flight_Plan(i)(y)) < 58
                                Part += Flight_Plan(i)(y)
                                y += 1
                            Loop

                            Overview_String += Part

                            Overview_String += "° is reached"

                        Case "ya"
                            Overview_String += "a yaw angle of "

                            x = 2
                            'Trova una cifra
                            Do While Asc(Flight_Plan(i)(x)) < 47 Or Asc(Flight_Plan(i)(x)) > 58
                                x += 1
                            Loop

                            y = x
                            'Passa le cifre a Part
                            Part = ""
                            Do While Asc(Flight_Plan(i)(y)) > 47 And Asc(Flight_Plan(i)(y)) < 58
                                Part += Flight_Plan(i)(y)
                                y += 1
                            Loop

                            Overview_String += Part

                            Overview_String += "° is reached"

                        Case "la"

                            Overview_String += "a latitude of "

                            x = 2
                            'Trova una cifra
                            Do While Asc(Flight_Plan(i)(x)) < 47 Or Asc(Flight_Plan(i)(x)) > 58
                                x += 1
                            Loop

                            y = x
                            'Passa le cifre a Part
                            Part = ""
                            Do While Asc(Flight_Plan(i)(y)) > 47 And Asc(Flight_Plan(i)(y)) < 58
                                Part += Flight_Plan(i)(y)
                                y += 1
                            Loop

                            Overview_String += Part

                            Overview_String += "°N is reached"

                        Case "lo"

                            Overview_String += "a longitude of "

                            x = 2
                            'Trova una cifra
                            Do While Asc(Flight_Plan(i)(x)) < 47 Or Asc(Flight_Plan(i)(x)) > 58
                                x += 1
                            Loop

                            y = x
                            'Passa le cifre a Part
                            Part = ""
                            Do While Asc(Flight_Plan(i)(y)) > 47 And Asc(Flight_Plan(i)(y)) < 58
                                Part += Flight_Plan(i)(y)
                                y += 1
                            Loop

                            Overview_String += Part

                            Overview_String += "°E is reached"

                        Case "al"

                            Overview_String += "an altitude of "

                            x = 2
                            'Trova una cifra
                            Do While Asc(Flight_Plan(i)(x)) < 47 Or Asc(Flight_Plan(i)(x)) > 58
                                x += 1
                            Loop

                            y = x
                            'Passa le cifre a Part
                            Part = ""
                            Do While Asc(Flight_Plan(i)(y)) > 47 And Asc(Flight_Plan(i)(y)) < 58
                                Part += Flight_Plan(i)(y)
                                y += 1
                            Loop

                            Overview_String += Part

                            Overview_String += "m is reached"

                        Case "ti"

                            Overview_String += "a time of "

                            x = 2
                            'Trova una cifra
                            Do While Asc(Flight_Plan(i)(x)) < 47 Or Asc(Flight_Plan(i)(x)) > 58
                                x += 1
                            Loop

                            y = x
                            'Passa le cifre a Part
                            Part = ""
                            Do While Asc(Flight_Plan(i)(y)) > 47 And Asc(Flight_Plan(i)(y)) < 58
                                Part += Flight_Plan(i)(y)
                                y += 1
                            Loop

                            Overview_String += Part

                            Overview_String += "s is passed"

                    End Select

            End Select


            Overview_Backup(i) = Overview_String
            i += 1

        Loop

        Indice = i
        Button_Load.Enabled = False

        'Passa da Overview_Backup a Overview
        'Riscrivi Overview 
        Dim z As ListViewItem
        Dim xx As String() = New String(2) {}
        ListView_Overview.Items.Clear()
        For i = 0 To (Indice - 1) Step 1
            xx(0) = i.ToString
            xx(1) = Overview_Backup(i)
            z = New ListViewItem(xx)
            ListView_Overview.Items.Add(z)
        Next

    End Sub

    'Gestisce la funzione "New"
    Private Sub Button_New_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_New.Click

        Reset_Input_Values()
        Indice = 0
        NumericUpDown_Instruction_Number.Value = 0
        Flight_Plan = New String(FP_Dimension) {}
        Overview_Backup = New String(FP_Dimension) {}
        ListView_Overview.Items.Clear()
        Button_Load.Enabled = True

        'Inizializza gli array di stringhe
        For i = 0 To FP_Dimension Step 1
            Flight_Plan(i) = ""
            Overview_Backup(i) = ""
        Next



    End Sub

End Class