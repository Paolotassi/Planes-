Imports System.IO

Public Class FlightPlanEditor

    Const FP_Dimension = 1000  'Dimensione del piano di volo

    Dim Current_Instruction As String       'contiene l'istruzione corrente e serve a passare alcune condizioni al pulsante "yes"
    Dim Overview_String As String           'contiene l'elemento corrente che verrà rappresentato in Overview 
    Dim Overview_Backup As String()         'contiene la versione "facile" su cui lavorare dell'overview
    Dim Flight_Plan As String()             'contiene il piano di volo completo
    Dim Indice As Integer                   'contiene il numero di istruzioni presenti nel piano di volo

    'Azzera tutte le caselle di testo delle istruzioni e i pulsanti
    'N.B.: non resetta "Indice"
    Public Sub Reset_Input_Values()

        TextBoxInstruction_1.Visible = False
        TextBoxInstruction_2.Visible = False
        TextBoxInstruction_3.Visible = False
        TextBoxInstruction_4.Visible = False
        TextBoxInstruction_5.Visible = False
        TextBoxInstruction_6.Visible = False
        LabelInstruction_1.Visible = True
        LabelInstruction_2.Visible = False
        LabelInstruction_3.Visible = False
        LabelInstruction_4.Visible = False
        LabelInstruction_5.Visible = False
        LabelInstruction_6.Visible = False
        ButtonInstruction_Add.Enabled = False
        ButtonInstruction_Replace.Enabled = False
        LabelSure.Visible = False
        ButtonNo.Visible = False
        ButtonYes.Visible = False

        LabelInstruction_1.Text = "Create a valid instruction first"

        ComboBoxInstruction_Type_1.Items.Clear()
        ComboBoxInstruction_Type_2.Items.Clear()

        ComboBoxInstruction_Type_1.Items.Add("Reach waypoint")
        ComboBoxInstruction_Type_1.Items.Add("Change engine power")
        ComboBoxInstruction_Type_1.Items.Add("Reach angle")
        ComboBoxInstruction_Type_1.Items.Add("Keep going until condition")
        ComboBoxInstruction_Type_1.Items.Add("Set new PID constants")
        ComboBoxInstruction_Type_1.Items.Add("Set new PID frequency")

        TextBoxInstruction_1.Text = "0"
        TextBoxInstruction_2.Text = "0"
        TextBoxInstruction_3.Text = "0"
        TextBoxInstruction_4.Text = "0"
        TextBoxInstruction_5.Text = "0"
        TextBoxInstruction_6.Text = "0"

    End Sub

    'Crea l'istruzione come definita
    Public Sub Create_Instruction()

        Select Case (ComboBoxInstruction_Type_1.SelectedItem)
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
            Case "Set new PID constants"
                Current_Instruction = "pd"
                Overview_String = "Set new PID "
            Case "Set new PID frequency"
                Current_Instruction = "pd"
                Overview_String = "Set new PID "

        End Select

        Select Case (ComboBoxInstruction_Type_2.SelectedItem)
            Case "Latitude, Longitude, Altitude"

                Current_Instruction += "la"     'aggiunge la latitudine all'istruzione
                Current_Instruction += TextBoxInstruction_1.Text
                Current_Instruction += "lo"     'aggiunge la longitudine
                Current_Instruction += TextBoxInstruction_2.Text
                Current_Instruction += "al"     'aggiunge l'altitudine
                Current_Instruction += TextBoxInstruction_3.Text

                Overview_String += "Lat(N): "
                Overview_String += TextBoxInstruction_1.Text
                Overview_String += " Long(E): "
                Overview_String += TextBoxInstruction_2.Text
                Overview_String += " Alt: "
                Overview_String += TextBoxInstruction_3.Text

            Case "Power %"
                Current_Instruction += "po"
                Current_Instruction += TextBoxInstruction_1.Text 'aggiunge l'altitudine

                Overview_String += "Engine power goes to "
                Overview_String += TextBoxInstruction_1.Text
                Overview_String += " %"

            Case "Pitch"

                Current_Instruction += "pi"
                Current_Instruction += TextBoxInstruction_1.Text 'aggiunge il beccheggio

                Overview_String += "pitch angle of "
                Overview_String += TextBoxInstruction_1.Text
                Overview_String += "°"

            Case "Roll"

                Current_Instruction += "ro"
                Current_Instruction += TextBoxInstruction_1.Text 'aggiunge il rollio

                Overview_String += "roll angle of "
                Overview_String += TextBoxInstruction_1.Text
                Overview_String += "°"

            Case "Yaw (deviation from current angle)"

                Current_Instruction += "ya"
                Current_Instruction += TextBoxInstruction_1.Text 'aggiunge l'imbardata

                Overview_String += "yaw angle of "
                Overview_String += TextBoxInstruction_1.Text
                Overview_String += "°"

            Case "Reached pitch angle"

                Current_Instruction += "pi"
                Current_Instruction += TextBoxInstruction_1.Text 'aggiunge il beccheggio

                Overview_String += "a pitch angle of "
                Overview_String += TextBoxInstruction_1.Text
                Overview_String += "° is reached"

            Case "Reached roll angle"

                Current_Instruction += "ro"
                Current_Instruction += TextBoxInstruction_1.Text 'aggiunge il rollio

                Overview_String += "a roll angle of "
                Overview_String += TextBoxInstruction_1.Text
                Overview_String += "° is reached"

            Case "Reached yaw angle"

                Current_Instruction += "ya"
                Current_Instruction += TextBoxInstruction_1.Text 'aggiunge l'imbardata

                Overview_String += "a yaw angle of "
                Overview_String += TextBoxInstruction_1.Text
                Overview_String += "° is reached"

            Case "Reached Latitude"

                Current_Instruction += "la"
                Current_Instruction += TextBoxInstruction_1.Text 'aggiunge la latitudine

                Overview_String += "a latitude of "
                Overview_String += TextBoxInstruction_1.Text
                Overview_String += "°N is reached"

            Case "Reached Longitude"

                Current_Instruction += "lo"
                Current_Instruction += TextBoxInstruction_1.Text 'aggiunge la longitudine

                Overview_String += "a longitude of "
                Overview_String += TextBoxInstruction_1.Text
                Overview_String += "°E is reached"

            Case "Altitude"

                Current_Instruction += "al"
                Current_Instruction += TextBoxInstruction_1.Text 'aggiunge l'altitudine

                Overview_String += "an altitude of "
                Overview_String += TextBoxInstruction_1.Text
                Overview_String += "m is reached"

            Case "Time since last instruction"

                Current_Instruction += "ti"
                Current_Instruction += TextBoxInstruction_1.Text 'aggiunge il tempo

                Overview_String += "a time of "
                Overview_String += TextBoxInstruction_1.Text
                Overview_String += "s is passed"


            Case "Pitch controller"

                Current_Instruction += "pp"     'aggiunge P
                Current_Instruction += TextBoxInstruction_1.Text
                Current_Instruction += "pi"     'aggiunge I
                Current_Instruction += TextBoxInstruction_2.Text
                Current_Instruction += "pd"     'aggiunge D
                Current_Instruction += TextBoxInstruction_3.Text

                Overview_String += "values for pitch control: Proportional: "
                Overview_String += TextBoxInstruction_1.Text
                Overview_String += " Integral: "
                Overview_String += TextBoxInstruction_2.Text
                Overview_String += " Derivative: "
                Overview_String += TextBoxInstruction_3.Text

            Case "Roll controller"

                Current_Instruction += "rp"     'aggiunge P
                Current_Instruction += TextBoxInstruction_1.Text
                Current_Instruction += "ri"     'aggiunge I
                Current_Instruction += TextBoxInstruction_2.Text
                Current_Instruction += "rd"     'aggiunge D
                Current_Instruction += TextBoxInstruction_3.Text

                Overview_String += "values for roll control: Proportional: "
                Overview_String += TextBoxInstruction_1.Text
                Overview_String += " Integral: "
                Overview_String += TextBoxInstruction_2.Text
                Overview_String += " Derivative: "
                Overview_String += TextBoxInstruction_3.Text

            Case "Yaw controller"

                Current_Instruction += "yp"     'aggiunge P
                Current_Instruction += TextBoxInstruction_1.Text
                Current_Instruction += "yi"     'aggiunge I
                Current_Instruction += TextBoxInstruction_2.Text
                Current_Instruction += "yd"     'aggiunge D
                Current_Instruction += TextBoxInstruction_3.Text

                Overview_String += "values for yaw control: Proportional: "
                Overview_String += TextBoxInstruction_1.Text
                Overview_String += " Integral: "
                Overview_String += TextBoxInstruction_2.Text
                Overview_String += " Derivative: "
                Overview_String += TextBoxInstruction_3.Text

            Case "Frequency"

                Current_Instruction += "fq"     'aggiunge frequenza PID
                Current_Instruction += TextBoxInstruction_1.Text


                Overview_String += "frequency to : "
                Overview_String += TextBoxInstruction_1.Text
                Overview_String += " Hz"

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
        x(0) = NumericUpDownInstruction_Number.Text
        x(1) = Overview_String
        y = New ListViewItem(x)
        ListViewOverview.Items.Add(y)
        Overview_Backup(Indice) = Overview_String



        'aggiorna l'indice dell'istruzione seguente
        Indice += 1
        NumericUpDownInstruction_Number.Value = Indice


        TextBoxInstruction_1.Text = "0"
        TextBoxInstruction_2.Text = "0"
        TextBoxInstruction_3.Text = "0"
        TextBoxInstruction_4.Text = "0"
        TextBoxInstruction_5.Text = "0"
        TextBoxInstruction_6.Text = "0"

        'Reset_Input_Values()

    End Sub
    'Setup della finesta
    'Crea la prima parte della lista di istruzioni possibili
    Private Sub Create_Flight_Plan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Reset_Input_Values()
        ListViewOverview.View = View.Details
        ListViewOverview.Columns.Add("Number", 60)
        ListViewOverview.Columns.Add("Instruction", 600)
        Indice = 0
        NumericUpDownInstruction_Number.Value = 0
        Flight_Plan = New String(FP_Dimension) {}
        Overview_Backup = New String(FP_Dimension) {}

        'Inizializza gli array di stringhe
        For i = 0 To FP_Dimension Step 1
            Flight_Plan(i) = ""
            Overview_Backup(i) = ""
        Next
    End Sub

    'Crea la seconda parte della lista di istruzioni possibili
    Private Sub ComboBox_Instruction_Type_1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxInstruction_Type_1.SelectedIndexChanged

        ComboBoxInstruction_Type_2.Items.Clear()

        Select Case (ComboBoxInstruction_Type_1.SelectedItem)
            Case "Reach waypoint"

                ComboBoxInstruction_Type_2.Items.Add("Latitude, Longitude, Altitude")
                ComboBoxInstruction_Type_2.SelectedItem = ("Latitude, Longitude, Altitude")

            Case "Change engine power"

                ComboBoxInstruction_Type_2.Items.Add("Power %")
                ComboBoxInstruction_Type_2.SelectedItem = ("Power %")

            Case "Reach angle"

                ComboBoxInstruction_Type_2.Items.Add("Pitch")
                ComboBoxInstruction_Type_2.Items.Add("Roll")
                ComboBoxInstruction_Type_2.Items.Add("Yaw (deviation from current angle)")
                ComboBoxInstruction_Type_2.SelectedItem = ("Pitch")

            Case "Keep going until condition"

                ComboBoxInstruction_Type_2.Items.Add("Reached pitch angle")
                ComboBoxInstruction_Type_2.Items.Add("Reached roll angle")
                ComboBoxInstruction_Type_2.Items.Add("Reached yaw angle")
                ComboBoxInstruction_Type_2.Items.Add("Reached Latitude")
                ComboBoxInstruction_Type_2.Items.Add("Reached Longitude")
                ComboBoxInstruction_Type_2.Items.Add("Reached Altitude")
                ComboBoxInstruction_Type_2.Items.Add("Time since last instruction")
                ComboBoxInstruction_Type_2.SelectedItem = ("Time since last instruction")

            Case "Set new PID constants"

                ComboBoxInstruction_Type_2.Items.Add("Pitch controller")
                ComboBoxInstruction_Type_2.Items.Add("Roll controller")
                ComboBoxInstruction_Type_2.Items.Add("Yaw controller")
                ComboBoxInstruction_Type_2.SelectedItem = ("Pitch controller")

            Case "Set new PID frequency"

                ComboBoxInstruction_Type_2.Items.Add("Frequency")
                ComboBoxInstruction_Type_2.SelectedItem = ("Frequency")

        End Select


    End Sub

    'Crea la casella dove inserire i dati per completare l'istruzione
    'Attiva i pulsanti "Add" e "Replace"
    Private Sub ComboBox_Instruction_Type_2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxInstruction_Type_1.SelectedIndexChanged

        'Azzera tutte le caselle di testo delle istruzioni e i pulsanti
        TextBoxInstruction_1.Visible = False
        TextBoxInstruction_2.Visible = False
        TextBoxInstruction_3.Visible = False
        TextBoxInstruction_4.Visible = False
        TextBoxInstruction_5.Visible = False
        TextBoxInstruction_6.Visible = False
        LabelInstruction_1.Visible = False
        LabelInstruction_2.Visible = False
        LabelInstruction_3.Visible = False
        LabelInstruction_4.Visible = False
        LabelInstruction_5.Visible = False
        LabelInstruction_6.Visible = False
        ButtonInstruction_Add.Enabled = True
        ButtonInstruction_Replace.Enabled = True




        Select Case (ComboBoxInstruction_Type_1.SelectedItem)
            Case "Reach waypoint"

                TextBoxInstruction_1.Visible = True
                TextBoxInstruction_2.Visible = True
                TextBoxInstruction_3.Visible = True
                LabelInstruction_1.Visible = True
                LabelInstruction_2.Visible = True
                LabelInstruction_3.Visible = True

                LabelInstruction_1.Text = "Latitude  :"
                LabelInstruction_2.Text = "Longitude :"
                LabelInstruction_3.Text = "Altitude  :"

            Case "Change engine power"

                TextBoxInstruction_1.Visible = True
                LabelInstruction_1.Visible = True

                LabelInstruction_1.Text = "Power % :"

            Case "Reach angle"

                TextBoxInstruction_1.Visible = True
                LabelInstruction_1.Visible = True

                LabelInstruction_1.Text = "Angle :"

            Case "Keep going until condition"

                TextBoxInstruction_1.Visible = True
                LabelInstruction_1.Visible = True

                LabelInstruction_1.Text = "Condition value :"

            Case "Set new PID constants"

                TextBoxInstruction_1.Visible = True
                TextBoxInstruction_2.Visible = True
                TextBoxInstruction_3.Visible = True
                LabelInstruction_1.Visible = True
                LabelInstruction_2.Visible = True
                LabelInstruction_3.Visible = True

                LabelInstruction_1.Text = "Proportional  :"
                LabelInstruction_2.Text = "Integral :"
                LabelInstruction_3.Text = "Derivative  :"


            Case "Set new PID frequency"

                TextBoxInstruction_1.Visible = True
                LabelInstruction_1.Visible = True

                LabelInstruction_1.Text = "Frequency (Hz) :"

        End Select


    End Sub

    'Gestisce la funzione "Add"
    Private Sub Button_Instruction_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonInstruction_Add.Click

        Create_Instruction()
        Print_Instruction()
        TextBox1.Text = Flight_Plan(Indice - 1)
        ButtonLoad.Enabled = False
        'Reset_Input_Values()

    End Sub

    'Gestisce la funzione "Replace"
    Private Sub Button_Instruction_Replace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonInstruction_Replace.Click
        Current_Instruction = "Replace"

        LabelSure.Visible = True
        ButtonNo.Visible = True
        ButtonYes.Visible = True

        'Reset_Input_Values()

    End Sub

    'Gestisce la funzione "Delete"
    Private Sub Button_Instruction_Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonInstruction_Delete.Click
        Current_Instruction = "Delete"

        LabelSure.Visible = True
        ButtonNo.Visible = True
        ButtonYes.Visible = True
    End Sub

    'Gestisce la funzione "Yes"
    Private Sub Button_Yes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonYes.Click

        Dim i As Int32
        Dim x As String() = New String(2) {}
        Dim y As ListViewItem

        If Current_Instruction = "Delete" AndAlso (NumericUpDownInstruction_Number.Value <= Indice) Then

            'Sistema Overview_Backup
            For i = (NumericUpDownInstruction_Number.Value) To (Indice - 1) Step 1
                Overview_Backup(i) = Overview_Backup(i + 1)
                Flight_Plan(i) = Flight_Plan(i + 1)
            Next
            Overview_Backup(Indice) = ""
            Flight_Plan(Indice) = ""


            Reset_Input_Values()
            Indice -= 1

            'Riscrivi Overview 
            ListViewOverview.Items.Clear()
            For i = 0 To (Indice - 1) Step 1
                x(0) = i.ToString
                x(1) = Overview_Backup(i)
                y = New ListViewItem(x)
                ListViewOverview.Items.Add(y)
            Next


        ElseIf Current_Instruction = "Replace" Then

            Create_Instruction()

            'Sistema Overview_Backup
            Overview_Backup(NumericUpDownInstruction_Number.Value) = Overview_String
            Flight_Plan(NumericUpDownInstruction_Number.Value) = Current_Instruction

            TextBox1.Text = Flight_Plan(NumericUpDownInstruction_Number.Value)

            'Riscrivi Overview 
            ListViewOverview.Items.Clear()
            For i = 0 To (Indice - 1) Step 1
                x(0) = i.ToString
                x(1) = Overview_Backup(i)
                y = New ListViewItem(x)
                ListViewOverview.Items.Add(y)

            Next



            Reset_Input_Values()

        End If


    End Sub

    'Gestisce la funzione "No"
    Private Sub Button_No_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonNo.Click

        Reset_Input_Values()

    End Sub

    'Gestisce la funzione "Save"
    Private Sub Button_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSave.Click

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

        'Aggiungi la stringa "End"
        Dim ind As Integer
        ind = 0
        While Flight_Plan(ind).Contains("$")
            ind += 1
        End While
        Flight_Plan(ind) = "End"





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
    Private Sub Button_Load_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonLoad.Click

        'Apre il file e lo trasforma nelle stringhe di Flight_Plan
        Dim OpenFileDialog1 As New OpenFileDialog

        Dim FileS As FileStream

        Dim Valid As Boolean
        Valid = True

        OpenFileDialog1.ShowDialog()
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim fileName As String
            fileName = OpenFileDialog1.FileName

            FileS = New FileStream(fileName, FileMode.Open, FileAccess.Read)
            Dim sR As New StreamReader(FileS)
            'Using i As New StreamReader(FileS)
            sR.BaseStream.Seek(0, SeekOrigin.Begin)

            Dim j As Integer
            Dim StopLoop As Boolean
            j = 0
            StopLoop = False
            While j <= FP_Dimension And StopLoop = False
                'While i.Peek() > -1
                Flight_Plan(j) = sR.ReadLine()
                TextBox1.Text += Flight_Plan(j)
                If Flight_Plan(j) = "End" Then
                    StopLoop = True
                ElseIf Not Flight_Plan(j).Contains("$") OrElse Not Flight_Plan(j).ElementAt(Flight_Plan(j).Length - 1) = "$" Then
                    StopLoop = True
                    Valid = False
                Else
                    j += 1
                End If

            End While
            sR.Close()

            If Valid = False Then
                Label_Invalid_File1.Text = "Error: Invalid File"
                Label_Invalid_File1.Visible = True
            Else
                Label_Invalid_File1.Visible = False
            End If




            Flight_Plan(j) = ""
            Indice = j
            NumericUpDownInstruction_Number.Value = Indice
        End If

        'Passa da Flight_Plan a Overview_Backup 
        Dim Part As String
        Dim i, x, y As Integer
        i = 0
        If Valid = True Then


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





                    Case "pd"
                        Overview_String = "Set new PID "
                        Part = Flight_Plan(i)(2) + Flight_Plan(i)(3)

                        Select Case (Part)
                            Case "pp"
                                Overview_String += "values for pitch control: Proportional: "

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

                                Overview_String += " Integral: "

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

                                Overview_String += " Derivative: "

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



                            Case "rp"
                                Overview_String += "values for roll control: Proportional: "

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

                                Overview_String += " Integral: "

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

                                Overview_String += " Derivative: "

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



                            Case "yp"
                                Overview_String += "values for yaw control: Proportional: "

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

                                Overview_String += " Integral: "

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

                                Overview_String += " Derivative: "

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




                            Case "fq"

                                Overview_String += "frequency: "

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

                                Overview_String += " Hz"






                        End Select

                End Select


                Overview_Backup(i) = Overview_String
                i += 1

            Loop

            Indice = i
            ButtonLoad.Enabled = False

            'Passa da Overview_Backup a Overview
            'Riscrivi Overview 
            Dim z As ListViewItem
            Dim xx As String() = New String(2) {}
            ListViewOverview.Items.Clear()
            For i = 0 To (Indice - 1) Step 1
                xx(0) = i.ToString
                xx(1) = Overview_Backup(i)
                z = New ListViewItem(xx)
                ListViewOverview.Items.Add(z)
            Next
        End If
    End Sub

    'Gestisce la funzione "New"
    Private Sub ButtonNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonNew.Click

        Reset_Input_Values()
        Indice = 0
        NumericUpDownInstruction_Number.Value = 0
        Flight_Plan = New String(FP_Dimension) {}
        Overview_Backup = New String(FP_Dimension) {}
        ListViewOverview.Items.Clear()
        ButtonLoad.Enabled = True

        'Inizializza gli array di stringhe
        For i = 0 To FP_Dimension Step 1
            Flight_Plan(i) = ""
            Overview_Backup(i) = ""
        Next



    End Sub

End Class