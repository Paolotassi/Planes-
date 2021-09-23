
Imports System
Imports System.IO
Imports System.IO.Ports
Imports System.Threading
Imports System.Diagnostics

'NOTE:
'----------'
'Ora il thread della comunicazione durante il volo parte appena viene scelta la porta seriale.
'Bisogna farlo partire alla fine dell'upload setup e/o dalla voce "read telemetry" del menu
'----------'
'Se viene interrotta la comunicazione USB durante lo svolgimento del programma, questo crasha.
'Non trovo soluzioni per ora, ma sarebbe consigliabile (ovviamente) trovare un modo per segnalarlo
'----------'
'Sarebbe da implementare un sistema per cui tenendo premuto i bottoni di zoom e altezza della mappa
'i valori aumentano. Ora bisogna cliccare per ogni incremento
'----------'
'Vedi se si riesce a implementare maps sulla mappa.
'Sempre sulla mappa, mancano dei cursori per modificare il centro (e un textbox a fianco che ne riporti le coordinate)
'Manca la conversione delle unità di misura se sono selezionati i metri
'----------'
'Vedi come implementare gli errori (scrivere Error_Handler)

Public Class NewFlight


    'comunicazione USB
    Dim USB_Port As String
    Public com1 As IO.Ports.SerialPort = Nothing
    Const BaudRate As UInt64 = 9600
    'vairabili di grafica
    Public InstrBit As New Bitmap(2000, 1000)
    Public ibg As Graphics = Graphics.FromImage(InstrBit)
    Dim DefBrush As New SolidBrush(Me.BackColor)
    'finestra "Create Message"
    Dim cm As New CreateMessage
    'impostazioni mappa
    Dim divZoom As Boolean
    Dim MA_cfr As Byte
    Dim MA_exp As Byte


    '---------- THREAD DI LETTURA/DISPLAY DELLA TELEMETRIA E GESTIONE DELLA COMUNICAZIONE SERIALE  ----------'

    Dim FlightDataCaller As Thread
    Dim FDCStopThread As Boolean



    'Legge la telemetria in entrata
    Public Sub Get_Raw_Flight_Data()
        Dim LocalRawData As String


        Do While True
            If DoSend Then    'invia un messaggio, se richiesto
                SendMessage()
                If Not New_Flight_Plan(0) = "Not" Then 'invia una modifica al piano di volo, se richiesto
                    SendNewFP()
                End If
                DoSend = False
            End If



            LocalRawData = com1.ReadLine()
            If LocalRawData.Contains("E") AndAlso LocalRawData.Contains("$") Then   'verifica se è un messaggio completo di errore
                Error_Handler(LocalRawData)

            ElseIf LocalRawData.Contains("T") AndAlso LocalRawData.Contains("_") Then 'verifica se è un messaggio completo di telemetria
                Raw_Data = LocalRawData
                Data_Preparation(Raw_Data)

                Background()
                Gimbal_Indicator()
                Surfaces_Indicator()
                Map()
                PictureBoxInstruments.Image = InstrBit
                DataNumber += 1



            End If

            If FDCStopThread Then
                Exit Do
            End If


        Loop

    End Sub

    'Prende le stringhe ricevute dalla seriale e le trasforma in "dati rielaborati"
    Public Sub Data_Preparation(ByVal Raw_Data As String)
        'La stringa di dati è strutturata con coppie di lettere seguite da una stringa di numeri che terminano con "$" (es. la0451325$). 
        'La stringa complessiva inizia con "T" e termina con "_"
        'Di seguito la legenda
        'ti = time (ms)
        'la = latitudine    lo = longitudine        al = altitudine
        'pi = pitch         pw = pitch wanted       pa = pitch action
        'ro = roll          rw = roll wanted        ra = roll action
        'ya = yaw           yw = yaw wanted         ya = yaw action
        'sp = speed         po = power %            ns = numero satelliti

        Dim i, max As Integer
        Dim Part As String

        i = 1   'così si salta la "T", cioè il primo carattere
        max = Raw_Data.IndexOf("_")

        RD_Array = Raw_Data.ToCharArray



        Do While i < max - 1
            'mancano i casi della posizione e del motore

            'Part = ToString(RD_Array(i) + RD_Array(i + 1))
            Part = RD_Array(i) + RD_Array(i + 1)
            Select Case (Part)
                'Tutti i pitch
                Case "ti"
                    Part = ""
                    'Salva cifre finchè non trovi "$"
                    i += 2
                    Do While Not (Asc(RD_Array(i)) = 36)
                        Part += RD_Array(i)
                        i += 1
                    Loop
                    Flight_Time = Convert.ToUInt64(Part)
                'Tutti i pitch
                Case "pi"
                    Part = ""
                    'Salva cifre finchè non trovi "$"
                    i += 2
                    Do While Not (Asc(RD_Array(i)) = 36)
                        Part += RD_Array(i)
                        i += 1
                    Loop
                    Pitch = Convert.ToInt32(Part)
                Case "pw"
                    Part = ""
                    'Salva cifre finchè non trovi "$"
                    i += 2
                    Do While Not (Asc(RD_Array(i)) = 36)
                        Part += RD_Array(i)
                        i += 1
                    Loop
                    Pitch_Wanted = Convert.ToInt32(Part)
                Case "pa"
                    Part = ""
                    'Salva cifre finchè non trovi "$"
                    i += 2
                    Do While Not (Asc(RD_Array(i)) = 36)
                        Part += RD_Array(i)
                        i += 1
                    Loop
                    Pitch_Action = Convert.ToInt32(Part)

                'Tutti i roll
                Case "ro"
                    Part = ""
                    'Salva cifre finchè non trovi "$"
                    i += 2
                    Do While Not (Asc(RD_Array(i)) = 36)
                        Part += RD_Array(i)
                        i += 1
                    Loop
                    Roll = Convert.ToInt32(Part)
                Case "rw"
                    Part = ""
                    'Salva cifre finchè non trovi "$"
                    i += 2
                    Do While Not (Asc(RD_Array(i)) = 36)
                        Part += RD_Array(i)
                        i += 1
                    Loop
                    Roll_Wanted = Convert.ToInt32(Part)
                Case "ra"
                    Part = ""
                    'Salva cifre finchè non trovi "$"
                    i += 2
                    Do While Not (Asc(RD_Array(i)) = 36)
                        Part += RD_Array(i)
                        i += 1
                    Loop
                    Roll_Action = Convert.ToInt32(Part)

                'Tutti gli yaw
                Case "ya"
                    Part = ""
                    'Salva cifre finchè non trovi "$"
                    i += 2
                    Do While Not (Asc(RD_Array(i)) = 36)
                        Part += RD_Array(i)
                        i += 1
                    Loop
                    Yaw = Convert.ToInt32(Part)
                Case "yw"
                    Part = ""
                    'Salva cifre finchè non trovi "$"
                    i += 2
                    Do While Not (Asc(RD_Array(i)) = 36)
                        Part += RD_Array(i)
                        i += 1
                    Loop
                    Yaw_Wanted = Convert.ToInt32(Part)
                Case "ya"
                    Part = ""
                    'Salva cifre finchè non trovi "$"
                    i += 2
                    Do While Not (Asc(RD_Array(i)) = 36)
                        Part += RD_Array(i)
                        i += 1
                    Loop
                    Yaw_Action = Convert.ToInt32(Part)

                Case "la"
                    Part = ""
                    'Salva cifre finchè non trovi "$"
                    i += 2
                    Do While Not (Asc(RD_Array(i)) = 36)
                        Part += RD_Array(i)
                        i += 1
                    Loop
                    Latitude(DataNumber) = Convert.ToDouble(Part)

                Case "lo"
                    Part = ""
                    'Salva cifre finchè non trovi "$"
                    i += 2
                    Do While Not (Asc(RD_Array(i)) = 36)
                        Part += RD_Array(i)
                        i += 1
                    Loop
                    Longitude(DataNumber) = Convert.ToDouble(Part)

                Case "al"
                    Part = ""
                    'Salva cifre finchè non trovi "$"
                    i += 2
                    Do While Not (Asc(RD_Array(i)) = 36)
                        Part += RD_Array(i)
                        i += 1
                    Loop
                    Altitude(DataNumber) = Convert.ToInt32(Part)

                Case "ns"
                    Part = ""
                    'Salva cifre finchè non trovi "$"
                    i += 2
                    Do While Not (Asc(RD_Array(i)) = 36)
                        Part += RD_Array(i)
                        i += 1
                    Loop
                    nSat = Convert.ToUInt16(Part)

                Case Else
                    i = max

            End Select

            i += 1

        Loop


    End Sub

    'invia messaggi al radiocomando
    Public Sub SendMessage()
        com1.WriteLine(Message)
    End Sub

    'invia modifiche al piano di volo al radiocomando
    Public Sub SendNewFP()
        For Each s As String In New_Flight_Plan
            com1.WriteLine(s)
        Next
    End Sub

    '---------- SUB DI DISEGNO ----------'

    Public Sub Background() 'ByVal e As System.Windows.Forms.PaintEventArgs)


        Dim Legend_Borders As New Rectangle(New Point(GIB_X + GIB_D + GIB_D \ 4, GIB_Y + GIB_Height + GIB_D \ 4), New Size(GIB_Lenght \ 2 - GIB_X + GIB_D + GIB_D \ 4, GIB_D))
        Dim Legend As New StringFormat(StringFormatFlags.NoClip)


        ibg.FillRectangle(DefBrush, 0, 0, 2000, 1000)

        'Gimbal Indicator Background
        ibg.FillRectangle(GIB_Brush1, GIB_X, GIB_Y + (GIB_D \ 2), GIB_Lenght, GIB_Height - GIB_D)
        ibg.FillRectangle(GIB_Brush1, GIB_X + (GIB_D \ 2), GIB_Y, GIB_Lenght - GIB_D, GIB_Height)
        ibg.FillEllipse(GIB_Brush1, GIB_X, GIB_Y, GIB_D, GIB_D)
        ibg.FillEllipse(GIB_Brush1, GIB_X + GIB_Lenght - GIB_D, GIB_Y, GIB_D, GIB_D)
        ibg.FillEllipse(GIB_Brush1, GIB_X, GIB_Y + GIB_Height - GIB_D, GIB_D, GIB_D)
        ibg.FillEllipse(GIB_Brush1, GIB_X + GIB_Lenght - GIB_D, GIB_Y + GIB_Height - GIB_D, GIB_D, GIB_D)
        ibg.FillRectangle(GIB_Brush4, GIB_X + GIB_D, GIB_Y + GIB_D, GIB_Lenght - (GIB_D * 2), ((GIB_Height - (GIB_D * 2)) \ 2))    'cielo
        ibg.FillRectangle(GIB_Brush3, GIB_X + GIB_D, GIB_Y + (GIB_Height \ 2), GIB_Lenght - (GIB_D * 2), ((GIB_Height - (GIB_D * 2)) \ 2))    'terreno

        'Gimbal indicator legend
        'New Point(40, 40), New Size(80, 80)

        ibg.FillRectangle(GI_Brush1, GIB_X, GIB_Y + GIB_Height + GIB_D \ 4, GIB_D, GIB_D)
        ibg.FillRectangle(GI_Brush2, GIB_X + GIB_Lenght \ 2, GIB_Y + GIB_Height + GIB_D \ 4, GIB_D, GIB_D)
        Legend.LineAlignment = StringAlignment.Center
        Legend.Alignment = StringAlignment.Near

        'ibg.FillRectangle(Brushes.Red, Legend_Borders)

        ibg.DrawString("Plane's attitude", Me.Font, GIB_Brush1, RectangleF.op_Implicit(Legend_Borders), Legend)
        Legend_Borders.Location = New Point(GIB_X + GIB_Lenght \ 2 + GIB_D + GIB_D \ 4, GIB_Y + GIB_Height + GIB_D \ 4)
        'ibg.FillRectangle(Brushes.Red, Legend_Borders)
        ibg.DrawString("Plane's wanted attitude", Me.Font, Brushes.Black, RectangleF.op_Implicit(Legend_Borders), Legend)

        'Gimbal Indicator linee di Background Pitch, dall'alto al basso
        ibg.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) - GIB_D, GIB_Y + (GIB_Lenght \ 2) - ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 6) \ 6), GIB_X + (GIB_Lenght \ 2) + GIB_D, GIB_Y + (GIB_Lenght \ 2) - ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 6) \ 6))
        ibg.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) - (GIB_D \ 2), GIB_Y + (GIB_Lenght \ 2) - ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 5) \ 6), GIB_X + (GIB_Lenght \ 2) + (GIB_D \ 2), GIB_Y + (GIB_Lenght \ 2) - ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 5) \ 6))
        ibg.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) - (GIB_D \ 2), GIB_Y + (GIB_Lenght \ 2) - ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 4) \ 6), GIB_X + (GIB_Lenght \ 2) + (GIB_D \ 2), GIB_Y + (GIB_Lenght \ 2) - ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 4) \ 6))
        ibg.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) - GIB_D, GIB_Y + (GIB_Lenght \ 2) - ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 3) \ 6), GIB_X + (GIB_Lenght \ 2) + GIB_D, GIB_Y + (GIB_Lenght \ 2) - ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 3) \ 6))
        ibg.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) - (GIB_D \ 2), GIB_Y + (GIB_Lenght \ 2) - ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 2) \ 6), GIB_X + (GIB_Lenght \ 2) + (GIB_D \ 2), GIB_Y + (GIB_Lenght \ 2) - ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 2) \ 6))
        ibg.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) - (GIB_D \ 2), GIB_Y + (GIB_Lenght \ 2) - ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 1) \ 6), GIB_X + (GIB_Lenght \ 2) + (GIB_D \ 2), GIB_Y + (GIB_Lenght \ 2) - ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 1) \ 6))

        ibg.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) - GIB_D, GIB_Y + (GIB_Lenght \ 2) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 6) \ 6), GIB_X + (GIB_Lenght \ 2) + GIB_D, GIB_Y + (GIB_Lenght \ 2) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 6) \ 6))
        ibg.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) - (GIB_D \ 2), GIB_Y + (GIB_Lenght \ 2) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 5) \ 6), GIB_X + (GIB_Lenght \ 2) + (GIB_D \ 2), GIB_Y + (GIB_Lenght \ 2) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 5) \ 6))
        ibg.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) - (GIB_D \ 2), GIB_Y + (GIB_Lenght \ 2) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 4) \ 6), GIB_X + (GIB_Lenght \ 2) + (GIB_D \ 2), GIB_Y + (GIB_Lenght \ 2) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 4) \ 6))
        ibg.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) - GIB_D, GIB_Y + (GIB_Lenght \ 2) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 3) \ 6), GIB_X + (GIB_Lenght \ 2) + GIB_D, GIB_Y + (GIB_Lenght \ 2) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 3) \ 6))
        ibg.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) - (GIB_D \ 2), GIB_Y + (GIB_Lenght \ 2) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 2) \ 6), GIB_X + (GIB_Lenght \ 2) + (GIB_D \ 2), GIB_Y + (GIB_Lenght \ 2) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 2) \ 6))
        ibg.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) - (GIB_D \ 2), GIB_Y + (GIB_Lenght \ 2) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 1) \ 6), GIB_X + (GIB_Lenght \ 2) + (GIB_D \ 2), GIB_Y + (GIB_Lenght \ 2) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 1) \ 6))

        'Gimbal Indicator linee di Background Yaw, da sinistra a destra
        ibg.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) - ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 6) \ 6), GIB_Y + (GIB_Height \ 2) - GIB_D, GIB_X + (GIB_Lenght \ 2) - ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 6) \ 6), GIB_Y + (GIB_Height \ 2) + GIB_D)
        ibg.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) - ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 5) \ 6), GIB_Y + (GIB_Height \ 2) - (GIB_D \ 2), GIB_X + (GIB_Lenght \ 2) - ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 5) \ 6), GIB_Y + (GIB_Height \ 2) + (GIB_D \ 2))
        ibg.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) - ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 4) \ 6), GIB_Y + (GIB_Height \ 2) - (GIB_D \ 2), GIB_X + (GIB_Lenght \ 2) - ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 4) \ 6), GIB_Y + (GIB_Height \ 2) + (GIB_D \ 2))
        ibg.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) - ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 3) \ 6), GIB_Y + (GIB_Height \ 2) - GIB_D, GIB_X + (GIB_Lenght \ 2) - ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 3) \ 6), GIB_Y + (GIB_Height \ 2) + GIB_D)
        ibg.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) - ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 2) \ 6), GIB_Y + (GIB_Height \ 2) - (GIB_D \ 2), GIB_X + (GIB_Lenght \ 2) - ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 2) \ 6), GIB_Y + (GIB_Height \ 2) + (GIB_D \ 2))
        ibg.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) - ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 1) \ 6), GIB_Y + (GIB_Height \ 2) - (GIB_D \ 2), GIB_X + (GIB_Lenght \ 2) - ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 1) \ 6), GIB_Y + (GIB_Height \ 2) + (GIB_D \ 2))

        ibg.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) + ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 6) \ 6), GIB_Y + (GIB_Height \ 2) - GIB_D, GIB_X + (GIB_Lenght \ 2) + ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 6) \ 6), GIB_Y + (GIB_Height \ 2) + GIB_D)
        ibg.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) + ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 5) \ 6), GIB_Y + (GIB_Height \ 2) - (GIB_D \ 2), GIB_X + (GIB_Lenght \ 2) + ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 5) \ 6), GIB_Y + (GIB_Height \ 2) + (GIB_D \ 2))
        ibg.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) + ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 4) \ 6), GIB_Y + (GIB_Height \ 2) - (GIB_D \ 2), GIB_X + (GIB_Lenght \ 2) + ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 4) \ 6), GIB_Y + (GIB_Height \ 2) + (GIB_D \ 2))
        ibg.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) + ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 3) \ 6), GIB_Y + (GIB_Height \ 2) - GIB_D, GIB_X + (GIB_Lenght \ 2) + ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 3) \ 6), GIB_Y + (GIB_Height \ 2) + GIB_D)
        ibg.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) + ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 2) \ 6), GIB_Y + (GIB_Height \ 2) - (GIB_D \ 2), GIB_X + (GIB_Lenght \ 2) + ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 2) \ 6), GIB_Y + (GIB_Height \ 2) + (GIB_D \ 2))
        ibg.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) + ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 1) \ 6), GIB_Y + (GIB_Height \ 2) - (GIB_D \ 2), GIB_X + (GIB_Lenght \ 2) + ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 1) \ 6), GIB_Y + (GIB_Height \ 2) + (GIB_D \ 2))

        'Gimbal Indicator Numeri a metà linee
        Legend_Borders.Size = New Size(GIB_D * 3 \ 2, GIB_D)
        Legend.LineAlignment = StringAlignment.Center
        Legend.Alignment = StringAlignment.Center
        'Pitch
        Legend_Borders.Location = New Point(GIB_X + (GIB_Lenght \ 2) - (Legend_Borders.Width \ 2), GIB_Y + (GIB_Lenght \ 2) - ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 3) \ 6) - (Legend_Borders.Height \ 2))
        ibg.FillRectangle(GIB_Brush4, Legend_Borders)
        ibg.DrawString("45", Me.Font, GIB_Brush5, RectangleF.op_Implicit(Legend_Borders), Legend)
        Legend_Borders.Location = New Point(GIB_X + (GIB_Lenght \ 2) - (Legend_Borders.Width \ 2), GIB_Y + (GIB_Lenght \ 2) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 3) \ 6) - (Legend_Borders.Height \ 2))
        ibg.FillRectangle(GIB_Brush3, Legend_Borders)
        ibg.DrawString("- 45", Me.Font, GIB_Brush5, RectangleF.op_Implicit(Legend_Borders), Legend)
        'Yaw
        Legend_Borders.Location = New Point(GIB_X + (GIB_Lenght \ 2) - ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 3) \ 6) - (Legend_Borders.Width \ 2), GIB_Y + (GIB_Height \ 2) - (Legend_Borders.Height \ 2))
        ibg.FillRectangle(GIB_Brush4, Legend_Borders.X, Legend_Borders.Y, Legend_Borders.Width, Legend_Borders.Height \ 2)
        ibg.FillRectangle(GIB_Brush3, Legend_Borders.X, Legend_Borders.Y + Legend_Borders.Height \ 2, Legend_Borders.Width, Legend_Borders.Height \ 2)
        ibg.DrawString("- 90", Me.Font, GIB_Brush5, RectangleF.op_Implicit(Legend_Borders), Legend)
        Legend_Borders.Location = New Point(GIB_X + (GIB_Lenght \ 2) + ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 3) \ 6) - (Legend_Borders.Width \ 2), GIB_Y + (GIB_Height \ 2) - (Legend_Borders.Height \ 2))
        ibg.FillRectangle(GIB_Brush4, Legend_Borders.X, Legend_Borders.Y, Legend_Borders.Width, Legend_Borders.Height \ 2)
        ibg.FillRectangle(GIB_Brush3, Legend_Borders.X, Legend_Borders.Y + Legend_Borders.Height \ 2, Legend_Borders.Width, Legend_Borders.Height \ 2)
        ibg.DrawString("90", Me.Font, GIB_Brush5, RectangleF.op_Implicit(Legend_Borders), Legend)



        'Surfaces Indicator line color
        If Pitch_Action = PID_Autority Then
            SI_Pen2.Color = Color.Red
        Else
            SI_Pen2.Color = Color.Green
        End If

        If Yaw_Action = PID_Autority Then
            SI_Pen3.Color = Color.Red
        Else
            SI_Pen3.Color = Color.Green
        End If

        If Roll_Action = PID_Autority Then
            SI_Pen1.Color = Color.Red
        Else
            SI_Pen1.Color = Color.Green
        End If
        'Surfaces Indicator Background
        ibg.FillRectangle(SIB_Brush1, SIB_X, SIB_Y + (SIB_D \ 2), SIB_Lenght, SIB_Height - SIB_D)
        ibg.FillRectangle(SIB_Brush1, SIB_X + (SIB_D \ 2), SIB_Y, SIB_Lenght - SIB_D, SIB_Height)
        ibg.FillEllipse(SIB_Brush1, SIB_X, SIB_Y, SIB_D, SIB_D)
        ibg.FillEllipse(SIB_Brush1, SIB_X + SIB_Lenght - SIB_D, SIB_Y, SIB_D, SIB_D)
        ibg.FillEllipse(SIB_Brush1, SIB_X, SIB_Y + SIB_Height - SIB_D, SIB_D, SIB_D)
        ibg.FillEllipse(SIB_Brush1, SIB_X + SIB_Lenght - SIB_D, SIB_Y + SIB_Height - SIB_D, SIB_D, SIB_D)
        'Surfaces Indicator Icona aereo
        ibg.FillEllipse(SIB_Brush2, SIB_X + (SIB_Lenght \ 2) - SIB_D, SIB_Y + (SIB_D * 3) + (SIB_Height \ 2), SIB_D * 2, SIB_D * 2) 'Fusoliera
        ibg.FillRectangle(SIB_Brush2, SIB_X + SIB_D, SIB_Y + (SIB_D * 3) + (SIB_Height \ 2), SIB_Lenght - (SIB_D * 2), SIB_D \ 2) 'ala
        ibg.FillRectangle(SIB_Brush2, SIB_X + (SIB_Lenght \ 2) - (SIB_D \ 8), SIB_Y + (SIB_D * 4), SIB_D \ 4, (SIB_Height \ 2) - SIB_D) 'timone
        ibg.FillRectangle(SIB_Brush2, SIB_X + (SIB_Lenght \ 2) - (SIB_D * 4), SIB_Y + (SIB_D * 4), SIB_D * 8, SIB_D \ 4) 'stabilizzatore
        'Surfaces Indicator linee di posizione massima
        ibg.DrawLine(SI_Pen1, SIB_X + SIB_D + SIB_D, SIB_Y + (SIB_D * 3) + (SIB_Height \ 2) - SI_Pen1.Width - ((PID_Autority * SIB_D) \ PID_factor), SIB_X + SIB_D + SIB_D + (SIB_Lenght - (SIB_D)) \ 2 - (SIB_D * 5), SIB_Y + (SIB_D * 3) + (SIB_Height \ 2) - SI_Pen1.Width - ((PID_Autority * SIB_D) \ PID_factor))   'alettone sx sopra
        ibg.DrawLine(SI_Pen1, SIB_X + SIB_D + SIB_D, SIB_Y + (SIB_D * 3) + (SIB_D \ 2) + (SIB_Height \ 2) - SI_Pen1.Width + ((PID_Autority * SIB_D) \ PID_factor), SIB_X + SIB_D + SIB_D + (SIB_Lenght - (SIB_D)) \ 2 - (SIB_D * 5), SIB_Y + (SIB_D * 3) + (SIB_D \ 2) + (SIB_Height \ 2) - SI_Pen1.Width + ((PID_Autority * SIB_D) \ PID_factor))   'alettone sx sotto
        ibg.DrawLine(SI_Pen1, SIB_X + SIB_Lenght - (SIB_D + SIB_D + ((SIB_Lenght - (SIB_D)) \ 2 - (SIB_D * 5))), SIB_Y + (SIB_D * 3) + (SIB_Height \ 2) - SI_Pen1.Width - ((PID_Autority * SIB_D) \ PID_factor), SIB_X + SIB_Lenght - (SIB_D + SIB_D + ((SIB_Lenght - (SIB_D)) \ 2 - (SIB_D * 5))) + (SIB_Lenght - (SIB_D)) \ 2 - (SIB_D * 5), SIB_Y + (SIB_D * 3) + (SIB_Height \ 2) - SI_Pen1.Width - ((PID_Autority * SIB_D) \ PID_factor))   'alettone dx sopra
        ibg.DrawLine(SI_Pen1, SIB_X + SIB_Lenght - (SIB_D + SIB_D + ((SIB_Lenght - (SIB_D)) \ 2 - (SIB_D * 5))), SIB_Y + (SIB_D * 3) + (SIB_D \ 2) + (SIB_Height \ 2) - SI_Pen1.Width + ((PID_Autority * SIB_D) \ PID_factor), SIB_X + SIB_Lenght - (SIB_D + SIB_D + ((SIB_Lenght - (SIB_D)) \ 2 - (SIB_D * 5))) + (SIB_Lenght - (SIB_D)) \ 2 - (SIB_D * 5), SIB_Y + (SIB_D * 3) + (SIB_D \ 2) + (SIB_Height \ 2) - SI_Pen1.Width + ((PID_Autority * SIB_D) \ PID_factor))   'alettone dx sotto
        ibg.DrawLine(SI_Pen2, SIB_X + (SIB_Lenght \ 2) - (SIB_D * 3), SIB_Y + (SIB_D * 4) - SI_Pen1.Width - ((PID_Autority * SIB_D) \ PID_factor), SIB_X + (SIB_Lenght \ 2) - (SIB_D * 3) + (SIB_D * 6), SIB_Y + (SIB_D * 4) - SI_Pen1.Width - ((PID_Autority * SIB_D) \ PID_factor))  'stabilizzatore sopra
        ibg.DrawLine(SI_Pen2, SIB_X + (SIB_Lenght \ 2) - (SIB_D * 3), SIB_Y + (SIB_D * 4) + SIB_D \ 4 - SI_Pen1.Width + ((PID_Autority * SIB_D) \ PID_factor), SIB_X + (SIB_Lenght \ 2) - (SIB_D * 3) + (SIB_D * 6), SIB_Y + (SIB_D * 4) + SIB_D \ 4 - SI_Pen1.Width + ((PID_Autority * SIB_D) \ PID_factor))  'stabilizzatore sotto
        ibg.DrawLine(SI_Pen3, SIB_X + (SIB_Lenght \ 2) - (SIB_D \ 8) - SI_Pen1.Width - ((PID_Autority * SIB_D) \ PID_factor), SIB_Y + (SIB_D * 4) + (SIB_D * 2), SIB_X + (SIB_Lenght \ 2) - (SIB_D \ 8) - SI_Pen1.Width - ((PID_Autority * SIB_D) \ PID_factor), SIB_Y + (SIB_D * 4) + (SIB_D * 2) + (SIB_Height \ 2) - (SIB_D * 4))    'timone sx
        ibg.DrawLine(SI_Pen3, SIB_X + (SIB_Lenght \ 2) - (SIB_D \ 8) + SIB_D \ 4 - SI_Pen1.Width + ((PID_Autority * SIB_D) \ PID_factor), SIB_Y + (SIB_D * 4) + (SIB_D * 2), SIB_X + (SIB_Lenght \ 2) - (SIB_D \ 8) + SIB_D \ 4 - SI_Pen1.Width + ((PID_Autority * SIB_D) \ PID_factor), SIB_Y + (SIB_D * 4) + (SIB_D * 2) + (SIB_Height \ 2) - (SIB_D * 4))    'timone dx

        'Map Background
        ibg.FillRectangle(MB_Brush1, MB_X, MB_Y + (MB_D \ 2), MB_Lenght, MB_Height - MB_D)
        ibg.FillRectangle(MB_Brush1, MB_X + (MB_D \ 2), MB_Y, MB_Lenght - MB_D, MB_Height)
        ibg.FillEllipse(MB_Brush1, MB_X, MB_Y, MB_D, MB_D)
        ibg.FillEllipse(MB_Brush1, MB_X + MB_Lenght - MB_D, MB_Y, MB_D, MB_D)
        ibg.FillEllipse(MB_Brush1, MB_X, MB_Y + MB_Height - MB_D, MB_D, MB_D)
        ibg.FillEllipse(MB_Brush1, MB_X + MB_Lenght - MB_D, MB_Y + MB_Height - MB_D, MB_D, MB_D)
        'Map Linee di Background
        ibg.DrawLine(MB_Pen1, MB_X + (MB_D \ 2), MB_Y + (MB_Height \ 2), MB_X + MB_Lenght - (MB_D \ 2), MB_Y + (MB_Height \ 2))   'orizzontale
        ibg.DrawLine(MB_Pen1, MB_X + (MB_D \ 2), MB_Y + (MB_Height \ 2) - MB_D - (MB_D \ 2), MB_X + (MB_D \ 2), MB_Y + (MB_Height \ 2) + MB_D + (MB_D \ 2))   'limite sx
        ibg.DrawLine(MB_Pen1, MB_X + MB_Lenght - (MB_D \ 2), MB_Y + (MB_Height \ 2) - MB_D - (MB_D \ 2), MB_X + MB_Lenght - (MB_D \ 2), MB_Y + (MB_Height \ 2) + MB_D + (MB_D \ 2))   'limite dx
        ibg.DrawLine(MB_Pen1, MB_X + (MB_Lenght \ 2), MB_Y + (MB_D \ 2), MB_X + (MB_Lenght \ 2), MB_Y + MB_Height - (MB_D \ 2))   'verticale
        ibg.DrawLine(MB_Pen1, MB_X + (MB_Lenght \ 2) - MB_D - (MB_D \ 2), MB_Y + (MB_D \ 2), MB_X + (MB_Lenght \ 2) + MB_D + (MB_D \ 2), MB_Y + (MB_D \ 2))   'limite sopra
        ibg.DrawLine(MB_Pen1, MB_X + (MB_Lenght \ 2) - MB_D - (MB_D \ 2), MB_Y + MB_Height - (MB_D \ 2), MB_X + (MB_Lenght \ 2) + MB_D + (MB_D \ 2), MB_Y + MB_Height - (MB_D \ 2))   'limite sotto
        'Map legenda
        Dim Sopra, Sotto, Destra, Sinistra As String
        If Button_Map_Style.Text = "Deg Absolute" Or (Altitude(0) = -1) Then 'Se è selezionato il bottone o non sono ancora stati ticevuti dati
            Sopra = (1 / M_Zoom).ToString
            Sotto = (-1 / M_Zoom).ToString
            Destra = (1 / M_Zoom).ToString
            Sinistra = (-1 / M_Zoom).ToString
        ElseIf Button_Map_Style.Text = "Deg From starting point" Then
            Sopra = (Latitude(0) + (1 / M_Zoom)).ToString
            Sotto = (Latitude(0) - (1 / M_Zoom)).ToString
            Destra = (Longitude(0) + (1 / M_Zoom)).ToString
            Sinistra = (Longitude(0) - (1 / M_Zoom)).ToString
        Else 'Button_Map_Style.Text = "Meters From starting point" 
            'Qui vanno convertiti i gradi, ma non ho voglia di farlo ora 
            Sopra = (1 / M_Zoom).ToString
            Sotto = (-1 / M_Zoom).ToString
            Destra = (1 / M_Zoom).ToString
            Sinistra = (-1 / M_Zoom).ToString
        End If
        Legend_Borders.Size = New Size(MB_D * 2, MB_D)
        'legenda sopra
        Legend.Alignment = StringAlignment.Center
        Legend_Borders.Location = New Point(MB_X + (MB_Lenght \ 2) - (MB_D * 1), MB_Y + (MB_D \ 2) - (MB_D \ 2))
        ibg.FillRectangle(MB_Brush1, Legend_Borders.X, Legend_Borders.Y, Legend_Borders.Width, Legend_Borders.Height)
        ibg.DrawString(Sopra, Me.Font, GIB_Brush5, RectangleF.op_Implicit(Legend_Borders), Legend)
        'legenda sotto
        Legend_Borders.Location = New Point(MB_X + (MB_Lenght \ 2) - (MB_D * 1), MB_Y + MB_Height - (MB_D \ 2) - (MB_D \ 2))
        ibg.FillRectangle(MB_Brush1, Legend_Borders.X, Legend_Borders.Y, Legend_Borders.Width, Legend_Borders.Height)
        ibg.DrawString(Sotto, Me.Font, GIB_Brush5, RectangleF.op_Implicit(Legend_Borders), Legend)
        'legenda destra
        Legend.Alignment = StringAlignment.Far
        Legend_Borders.Location = New Point(MB_X + MB_Lenght - (MB_D \ 2) - (MB_D * 2) + MB_Pen1.Width, MB_Y + (MB_Height \ 2) - (MB_D \ 2))
        ibg.FillRectangle(MB_Brush1, Legend_Borders.X, Legend_Borders.Y, Legend_Borders.Width, Legend_Borders.Height)
        ibg.DrawString(Destra, Me.Font, GIB_Brush5, RectangleF.op_Implicit(Legend_Borders), Legend)
        'legenda sinistra
        Legend.Alignment = StringAlignment.Near
        Legend_Borders.Location = New Point(MB_X + (MB_D \ 2), MB_Y + (MB_Height \ 2) - (MB_D \ 2))
        ibg.FillRectangle(MB_Brush1, Legend_Borders.X, Legend_Borders.Y, Legend_Borders.Width, Legend_Borders.Height)
        ibg.DrawString(Sinistra, Me.Font, GIB_Brush5, RectangleF.op_Implicit(Legend_Borders), Legend)




        'scala altezze
        Dim col As New Color()
        For i = 0 To MB_D * 5
            col = Color.FromArgb(0 + ((255 * i) \ (MB_D * 5)), 255 - ((255 * i) \ (MB_D * 5)), 0)
            MB_Pen2.Color = col
            ibg.DrawLine(MB_Pen2, MB_X + MB_Lenght + (MB_D \ 2), MB_Y + (MB_D * 5) + i, MB_X + MB_Lenght + MB_D, MB_Y + (MB_D * 5) + i)
        Next
        For i = MB_D * 5 To MB_D * 10
            col = Color.FromArgb(255 - ((255 * (i - MB_D * 5)) \ (MB_D * 5)), 0, 0 + ((255 * (i - MB_D * 5)) \ (MB_D * 5)))
            MB_Pen2.Color = col
            ibg.DrawLine(MB_Pen2, MB_X + MB_Lenght + (MB_D \ 2), MB_Y + (MB_D * 5) + i, MB_X + MB_Lenght + MB_D, MB_Y + (MB_D * 5) + i)
        Next

        If Button_Altitude_Style.Text = "From sea level" Then
            'label altezza 100%
            Legend_Borders.Location = New Point(MB_X + MB_Lenght + (MB_D \ 2) + MB_D, MB_Y + (MB_D * 5) - (MB_D \ 2))
            ibg.FillRectangle(DefBrush, Legend_Borders.X, Legend_Borders.Y, Legend_Borders.Width, Legend_Borders.Height)
            ibg.DrawString((Max_Altitude).ToString, Me.Font, MB_Brush1, RectangleF.op_Implicit(Legend_Borders), Legend)
            'label altezza 50%
            Legend_Borders.Location = New Point(MB_X + MB_Lenght + (MB_D \ 2) + MB_D, MB_Y + (MB_D * 5) + (MB_D * 2))
            ibg.FillRectangle(DefBrush, Legend_Borders.X, Legend_Borders.Y, Legend_Borders.Width, Legend_Borders.Height)
            ibg.DrawString((Max_Altitude * 3 / 4).ToString, Me.Font, MB_Brush1, RectangleF.op_Implicit(Legend_Borders), Legend)
            'label altezza 0%
            Legend_Borders.Location = New Point(MB_X + MB_Lenght + (MB_D \ 2) + MB_D, MB_Y + (MB_D * 5) - (MB_D \ 2) + (MB_D * 5))
            ibg.FillRectangle(DefBrush, Legend_Borders.X, Legend_Borders.Y, Legend_Borders.Width, Legend_Borders.Height)
            ibg.DrawString((Max_Altitude / 2).ToString, Me.Font, MB_Brush1, RectangleF.op_Implicit(Legend_Borders), Legend)
            'label altezza -50%
            Legend_Borders.Location = New Point(MB_X + MB_Lenght + (MB_D \ 2) + MB_D, MB_Y + (MB_D * 5) + (MB_D * 7))
            ibg.FillRectangle(DefBrush, Legend_Borders.X, Legend_Borders.Y, Legend_Borders.Width, Legend_Borders.Height)
            ibg.DrawString((Max_Altitude * 1 / 4).ToString, Me.Font, MB_Brush1, RectangleF.op_Implicit(Legend_Borders), Legend)
            'Label altezza - 100%
            Legend_Borders.Location = New Point(MB_X + MB_Lenght + (MB_D \ 2) + MB_D, MB_Y + (MB_D * 5) - (MB_D \ 2) + (MB_D * 10))
            ibg.FillRectangle(DefBrush, Legend_Borders.X, Legend_Borders.Y, Legend_Borders.Width, Legend_Borders.Height)
            ibg.DrawString((0).ToString, Me.Font, MB_Brush1, RectangleF.op_Implicit(Legend_Borders), Legend)


        Else    'Button_Altitude_Style.Text = "From starting point"
            'label altezza 100%
            Legend_Borders.Location = New Point(MB_X + MB_Lenght + (MB_D \ 2) + MB_D, MB_Y + (MB_D * 5) - (MB_D \ 2))
            ibg.FillRectangle(DefBrush, Legend_Borders.X, Legend_Borders.Y, Legend_Borders.Width, Legend_Borders.Height)
            ibg.DrawString((Max_Altitude).ToString, Me.Font, MB_Brush1, RectangleF.op_Implicit(Legend_Borders), Legend)
            'label altezza 50%
            Legend_Borders.Location = New Point(MB_X + MB_Lenght + (MB_D \ 2) + MB_D, MB_Y + (MB_D * 5) + (MB_D * 2))
            ibg.FillRectangle(DefBrush, Legend_Borders.X, Legend_Borders.Y, Legend_Borders.Width, Legend_Borders.Height)
            ibg.DrawString((Max_Altitude / 2).ToString, Me.Font, MB_Brush1, RectangleF.op_Implicit(Legend_Borders), Legend)
            'label altezza 0%
            Legend_Borders.Location = New Point(MB_X + MB_Lenght + (MB_D \ 2) + MB_D, MB_Y + (MB_D * 5) - (MB_D \ 2) + (MB_D * 5))
            ibg.FillRectangle(DefBrush, Legend_Borders.X, Legend_Borders.Y, Legend_Borders.Width, Legend_Borders.Height)
            ibg.DrawString("Starting Altitude", Me.Font, MB_Brush1, RectangleF.op_Implicit(Legend_Borders), Legend)
            'label altezza -50%
            Legend_Borders.Location = New Point(MB_X + MB_Lenght + (MB_D \ 2) + MB_D, MB_Y + (MB_D * 5) + (MB_D * 7))
            ibg.FillRectangle(DefBrush, Legend_Borders.X, Legend_Borders.Y, Legend_Borders.Width, Legend_Borders.Height)
            ibg.DrawString((-Max_Altitude / 2).ToString, Me.Font, MB_Brush1, RectangleF.op_Implicit(Legend_Borders), Legend)
            'Label altezza - 100%
            Legend_Borders.Location = New Point(MB_X + MB_Lenght + (MB_D \ 2) + MB_D, MB_Y + (MB_D * 5) - (MB_D \ 2) + (MB_D * 10))
            ibg.FillRectangle(DefBrush, Legend_Borders.X, Legend_Borders.Y, Legend_Borders.Width, Legend_Borders.Height)
            ibg.DrawString((-Max_Altitude).ToString, Me.Font, MB_Brush1, RectangleF.op_Implicit(Legend_Borders), Legend)
        End If


    End Sub

    Public Sub Gimbal_Indicator()

        'X centro:  GIB_X + (GIB_Lenght\2)      Max estensione in X: ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 6) \ 6)
        'Y centro:  GIB_Y + (GIB_Height\2)      Max estensione in Y: 
        'mezza larghezza: Corte:    GIB_D      Lunghe: (GIB_D \ 2) 

        Dim GI_Triangle As Point()  'serve a disegnare gli indicatori per pitch e yaw
        Dim A, B As Int32
        Dim Angle, Cos_Value, Sin_Value As Double
        GI_Triangle = New Point(2) {}



        'Gimbal Indicator Pitch
        GI_Triangle(0).X = GIB_X + (GIB_Lenght \ 2)
        GI_Triangle(0).Y = GIB_Y + (GIB_Height \ 2) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D)) * Pitch) \ Pitch_Limit
        GI_Triangle(1).X = GIB_X + (GIB_Lenght \ 2) - (GIB_D \ 2)
        GI_Triangle(1).Y = GIB_Y + (GIB_Height \ 2) + (GIB_D \ 4) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D)) * Pitch) \ Pitch_Limit
        GI_Triangle(2).X = GIB_X + (GIB_Lenght \ 2) - (GIB_D \ 2)
        GI_Triangle(2).Y = GIB_Y + (GIB_Height \ 2) - (GIB_D \ 4) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D)) * Pitch) \ Pitch_Limit
        ibg.FillPolygon(GI_Brush1, GI_Triangle)
        'Gimbal Indicator Pitch__Wanted
        GI_Triangle(0).X = GIB_X + (GIB_Lenght \ 2)
        GI_Triangle(0).Y = GIB_Y + (GIB_Height \ 2) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D)) * Pitch_Wanted) \ Pitch_Limit
        GI_Triangle(1).X = GIB_X + (GIB_Lenght \ 2) + (GIB_D \ 2)
        GI_Triangle(1).Y = GIB_Y + (GIB_Height \ 2) + (GIB_D \ 4) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D)) * Pitch_Wanted) \ Pitch_Limit
        GI_Triangle(2).X = GIB_X + (GIB_Lenght \ 2) + (GIB_D \ 2)
        GI_Triangle(2).Y = GIB_Y + (GIB_Height \ 2) - (GIB_D \ 4) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D)) * Pitch_Wanted) \ Pitch_Limit
        ibg.FillPolygon(GI_Brush2, GI_Triangle)
        'Gimbal Indicator Yaw
        GI_Triangle(0).X = GIB_X + (GIB_Lenght \ 2) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D)) * Yaw) \ Yaw_Limit
        GI_Triangle(0).Y = GIB_Y + (GIB_Height \ 2)
        GI_Triangle(1).X = GIB_X + (GIB_Lenght \ 2) + (GIB_D \ 4) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D)) * Yaw) \ Yaw_Limit
        GI_Triangle(1).Y = GIB_Y + (GIB_Height \ 2) - (GIB_D \ 2)
        GI_Triangle(2).X = GIB_X + (GIB_Lenght \ 2) - (GIB_D \ 4) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D)) * Yaw) \ Yaw_Limit
        GI_Triangle(2).Y = GIB_Y + (GIB_Height \ 2) - (GIB_D \ 2)
        ibg.FillPolygon(GI_Brush1, GI_Triangle)
        'Gimbal Indicator Yaw_Wanted
        GI_Triangle(0).X = GIB_X + (GIB_Lenght \ 2) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D)) * Yaw_Wanted) \ Yaw_Limit
        GI_Triangle(0).Y = GIB_Y + (GIB_Height \ 2)
        GI_Triangle(1).X = GIB_X + (GIB_Lenght \ 2) + (GIB_D \ 4) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D)) * Yaw_Wanted) \ Yaw_Limit
        GI_Triangle(1).Y = GIB_Y + (GIB_Height \ 2) + (GIB_D \ 2)
        GI_Triangle(2).X = GIB_X + (GIB_Lenght \ 2) - (GIB_D \ 4) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D)) * Yaw_Wanted) \ Yaw_Limit
        GI_Triangle(2).Y = GIB_Y + (GIB_Height \ 2) + (GIB_D \ 2)
        ibg.FillPolygon(GI_Brush2, GI_Triangle)


        'Gimbal Indicator Roll_Wanted
        If (Roll_Wanted <= 45 And Roll_Wanted >= -45) Or (Roll_Wanted >= 135 Or Roll_Wanted <= -135) Then
            Angle = (Roll_Wanted * Math.PI) / 180
            Sin_Value = Math.Sin(Angle * 2)
            Cos_Value = Math.Cos(Angle * 2)
            A = Math.Round((((GIB_Height - GIB_D * 2) \ 2) * Sin_Value) \ Math.Sin((45 * Math.PI) / 180))
            ibg.DrawLine(GI_Pen2, GIB_X + GIB_D, GIB_Y + (GIB_Height \ 2) - A, GIB_X + GIB_Lenght - GIB_D, GIB_Y + (GIB_Height \ 2) + A)
            Sin_Value = Math.Sin(Angle)
            Cos_Value = Math.Cos(Angle)
            A = Math.Round((GIB_D) * Sin_Value)
            B = Math.Round((GIB_D) * Cos_Value)
            ibg.DrawLine(GI_Pen2, GIB_X + (GIB_Lenght \ 2), GIB_Y + (GIB_Height \ 2), GIB_X + (GIB_Lenght \ 2) + A, GIB_Y + (GIB_Height \ 2) - B)
        Else
            Angle = ((Roll_Wanted - 90) * Math.PI) / 180
            Sin_Value = Math.Sin(Angle * 2)
            Cos_Value = Math.Cos(Angle * 2)
            A = Math.Round((((GIB_Height - GIB_D * 2) \ 2) * Sin_Value) \ Math.Sin((45 * Math.PI) / 180))
            ibg.DrawLine(GI_Pen2, GIB_X + (GIB_Lenght \ 2) + A, GIB_Y + GIB_D, GIB_X + (GIB_Lenght \ 2) - A, GIB_Y + GIB_Lenght - GIB_D)
            Sin_Value = Math.Sin(Angle)
            Cos_Value = Math.Cos(Angle)
            A = Math.Round((GIB_D) * Sin_Value)
            B = Math.Round((GIB_D) * Cos_Value)
            ibg.DrawLine(GI_Pen2, GIB_X + (GIB_Lenght \ 2), GIB_Y + (GIB_Height \ 2), GIB_X + (GIB_Lenght \ 2) + B, GIB_Y + (GIB_Height \ 2) + A)
        End If

        'Gimbal Indicator Roll
        If (Roll <= 45 And Roll >= -45) Or (Roll >= 135 Or Roll <= -135) Then
            Angle = (Roll * Math.PI) / 180
            Sin_Value = Math.Sin(Angle * 2)
            Cos_Value = Math.Cos(Angle * 2)
            A = Math.Round((((GIB_Height - GIB_D * 2) \ 2) * Sin_Value) \ Math.Sin((45 * Math.PI) / 180))
            ibg.DrawLine(GI_Pen1, GIB_X + GIB_D, GIB_Y + (GIB_Height \ 2) - A, GIB_X + GIB_Lenght - GIB_D, GIB_Y + (GIB_Height \ 2) + A)
            Sin_Value = Math.Sin(Angle)
            Cos_Value = Math.Cos(Angle)
            A = Math.Round((GIB_D) * Sin_Value)
            B = Math.Round((GIB_D) * Cos_Value)
            ibg.DrawLine(GI_Pen1, GIB_X + (GIB_Lenght \ 2), GIB_Y + (GIB_Height \ 2), GIB_X + (GIB_Lenght \ 2) + A, GIB_Y + (GIB_Height \ 2) - B)
        Else
            Angle = ((Roll - 90) * Math.PI) / 180
            Sin_Value = Math.Sin(Angle * 2)
            Cos_Value = Math.Cos(Angle * 2)
            A = Math.Round((((GIB_Height - GIB_D * 2) \ 2) * Sin_Value) \ Math.Sin((45 * Math.PI) / 180))
            ibg.DrawLine(GI_Pen1, GIB_X + (GIB_Lenght \ 2) + A, GIB_Y + GIB_D, GIB_X + (GIB_Lenght \ 2) - A, GIB_Y + GIB_Lenght - GIB_D)
            Sin_Value = Math.Sin(Angle)
            Cos_Value = Math.Cos(Angle)
            A = Math.Round((GIB_D) * Sin_Value)
            B = Math.Round((GIB_D) * Cos_Value)
            ibg.DrawLine(GI_Pen1, GIB_X + (GIB_Lenght \ 2), GIB_Y + (GIB_Height \ 2), GIB_X + (GIB_Lenght \ 2) + B, GIB_Y + (GIB_Height \ 2) + A)
        End If






    End Sub

    Public Sub Surfaces_Indicator()
        'Surfaces Indicator Superfici azionate
        ibg.FillRectangle(SI_Brush3, SIB_X + (SIB_Lenght \ 2) - (SIB_D \ 8), SIB_Y + (SIB_D * 4) + (SIB_D * 2), SIB_D \ 4, (SIB_Height \ 2) - (SIB_D * 4)) 'timone
        If Yaw_Action > 0 Then
            ibg.FillRectangle(SI_Brush3, SIB_X + (SIB_Lenght \ 2) + (SIB_D \ 8), SIB_Y + (SIB_D * 4) + (SIB_D * 2), ((Yaw_Action * SIB_D) \ PID_factor), (SIB_Height \ 2) - (SIB_D * 4)) 'timone
        Else
            ibg.FillRectangle(SI_Brush3, SIB_X + (SIB_Lenght \ 2) - (SIB_D \ 8) + ((Yaw_Action * SIB_D) \ PID_factor), SIB_Y + (SIB_D * 4) + (SIB_D * 2), -((Yaw_Action * SIB_D) \ PID_factor), (SIB_Height \ 2) - (SIB_D * 4)) 'timone
        End If

        ibg.FillRectangle(SI_Brush2, SIB_X + (SIB_Lenght \ 2) - (SIB_D * 3), SIB_Y + (SIB_D * 4), SIB_D * 6, SIB_D \ 4) 'stabilizzatore
        If Pitch_Action > 0 Then
            ibg.FillRectangle(SI_Brush2, SIB_X + (SIB_Lenght \ 2) - (SIB_D * 3), SIB_Y + (SIB_D * 4) - ((Pitch_Action * SIB_D) \ PID_factor), SIB_D * 6, ((Pitch_Action * SIB_D) \ PID_factor)) 'stabilizzatore
        Else
            ibg.FillRectangle(SI_Brush2, SIB_X + (SIB_Lenght \ 2) - (SIB_D * 3), SIB_Y + (SIB_D * 4), SIB_D * 6, (SIB_D \ 4) - ((Pitch_Action * SIB_D) \ PID_factor)) 'stabilizzatore
        End If

        ibg.FillRectangle(SI_Brush1, SIB_X + SIB_D + SIB_D, SIB_Y + (SIB_D * 3) + (SIB_Height \ 2), (SIB_Lenght - (SIB_D)) \ 2 - (SIB_D * 5), SIB_D \ 2) 'alettone sx
        ibg.FillRectangle(SI_Brush1, SIB_X + SIB_Lenght - (SIB_D + SIB_D + ((SIB_Lenght - (SIB_D)) \ 2 - (SIB_D * 5))), SIB_Y + (SIB_D * 3) + (SIB_Height \ 2), (SIB_Lenght - (SIB_D)) \ 2 - (SIB_D * 5), SIB_D \ 2) 'alettone dx
        If Roll_Action > 0 Then
            ibg.FillRectangle(SI_Brush1, SIB_X + SIB_D + SIB_D, SIB_Y + (SIB_D * 3) + (SIB_Height \ 2), (SIB_Lenght - (SIB_D)) \ 2 - (SIB_D * 5), SIB_D \ 2 + ((Roll_Action * SIB_D) \ PID_factor)) 'alettone sx
            ibg.FillRectangle(SI_Brush1, SIB_X + SIB_Lenght - (SIB_D + SIB_D + ((SIB_Lenght - (SIB_D)) \ 2 - (SIB_D * 5))), SIB_Y + (SIB_D * 3) + (SIB_Height \ 2) - ((Roll_Action * SIB_D) \ PID_factor), (SIB_Lenght - (SIB_D)) \ 2 - (SIB_D * 5), SIB_D \ 2 + ((Roll_Action * SIB_D) \ PID_factor)) 'alettone dx
        Else
            ibg.FillRectangle(SI_Brush1, SIB_X + SIB_D + SIB_D, SIB_Y + (SIB_D * 3) + (SIB_Height \ 2) + ((Roll_Action * SIB_D) \ PID_factor), (SIB_Lenght - (SIB_D)) \ 2 - (SIB_D * 5), -((Roll_Action * SIB_D) \ PID_factor)) 'alettone sx
            ibg.FillRectangle(SI_Brush1, SIB_X + SIB_Lenght - (SIB_D + SIB_D + ((SIB_Lenght - (SIB_D)) \ 2 - (SIB_D * 5))), SIB_Y + (SIB_D * 3) + (SIB_Height \ 2), (SIB_Lenght - (SIB_D)) \ 2 - (SIB_D * 5), SIB_D \ 2 - ((Roll_Action * SIB_D) \ PID_factor)) 'alettone dx
        End If

    End Sub

    Public Sub Map()
        If DataNumber > 0 Then  'solo se il primo dato è già stato ricevuto
            Dim colore As Color
            Dim punto As New Rectangle
            Dim Zoom As Double
            Dim ind As Int64
            Dim dn As Int64
            punto.Size = New Size(3, 3)

            Zoom = M_Zoom
            dn = DataNumber


            For ind = 0 To DataNumber Step 1
                If (Latitude(ind) <= (Latitude(0) + (0.9 / Zoom))) And (Latitude(ind) >= (Latitude(0) - (0.9 / Zoom))) Then
                    If (Longitude(ind) <= (Longitude(0) + (1 / Zoom))) And (Longitude(ind) >= (Longitude(0) - (1 / Zoom))) Then
                        'MsgBox("d = " + DataNumber.ToString + "ind = " + ind.ToString)

                        If Button_Altitude_Style.Text = "From sea level" Then


                            If Altitude(ind) - Altitude(0) + StartingAltitude >= Max_Altitude \ 2 And Altitude(ind) - Altitude(0) + StartingAltitude <= Max_Altitude Then

                                colore = Color.FromArgb(255 - ((255 * (Altitude(ind) - Altitude(0) + StartingAltitude - (Max_Altitude / 2))) \ (Max_Altitude / 2)), 0 + ((255 * (Altitude(ind) - Altitude(0) + StartingAltitude - (Max_Altitude / 2))) \ (Max_Altitude / 2)), 0)

                            ElseIf Altitude(ind) - Altitude(0) + StartingAltitude <= Max_Altitude \ 2 And Altitude(ind) - Altitude(0) + StartingAltitude >= 0 Then

                                colore = Color.FromArgb(255 + ((255 * (Altitude(ind) - Altitude(0) + StartingAltitude - (Max_Altitude / 2))) \ (Max_Altitude / 2)), 0, 0 - ((255 * (Altitude(ind) - Altitude(0) + StartingAltitude - (Max_Altitude / 2))) \ (Max_Altitude / 2)))

                            Else
                                colore = Color.White
                            End If
                            M_Pen1.Color = colore


                        Else    'Button_Altitude_Style.Text = "From starting point"

                            If Altitude(ind) >= Altitude(0) And Altitude(ind) <= (Max_Altitude + Altitude(0)) Then
                                colore = Color.FromArgb(255 - ((255 * (Altitude(ind) - Altitude(0))) \ Max_Altitude), 0 + ((255 * (Altitude(ind) - Altitude(0))) \ Max_Altitude), 0)
                            ElseIf Altitude(ind) <= Altitude(0) And Altitude(ind) >= (Altitude(0) - Max_Altitude) Then
                                colore = Color.FromArgb(255 + ((255 * (Altitude(ind) - Altitude(0))) \ Max_Altitude), 0, 0 - ((255 * (Altitude(ind) - Altitude(0))) \ Max_Altitude))
                            Else
                                colore = Color.White
                            End If
                            M_Pen1.Color = colore

                        End If



                        punto.X = MB_X + (MB_Lenght \ 2) - 1 + Decimal.Round((Longitude(ind) - Longitude(0)) * Zoom * (MB_Lenght \ 2))
                        punto.Y = MB_Y + (MB_Lenght \ 2) - 1 - Decimal.Round((Latitude(ind) - Latitude(0)) * Zoom * (MB_Lenght \ 2))
                        ibg.DrawRectangle(M_Pen1, punto)

                    End If
                End If


            Next

        End If

    End Sub



    Public Sub Window_Paint() 'ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
        If PanelToolStripMenuItem_Instruments.Checked = True Then

            PictureBoxInstruments.Visible = True
            'PictureBoxInstruments.BringToFront()
            Background()

            PictureBoxInstruments.Image = InstrBit
        Else

            PictureBoxInstruments.Visible = False
        End If


    End Sub

    '---------- SUB DI GESTIONE DEGLI ERRORI----------'
    Public Sub Error_Handler(ErrorString As String)
        Dim Error_Name As String
        Dim Error_Array As Char()
        Dim max As Integer
        max = ErrorString.IndexOf("$")  'ultimo carattere utile della stringa di errore

        Error_Array = ErrorString.ToCharArray
        Error_Name = Error_Array(1)
        For index = 2 To max
            Error_Name += Error_Array(index)
        Next

        Select Case (Error_Name)

            Case "gy521"    'errore nella connessione del giroscopio/accelerometro


        End Select



    End Sub

    '---------- SUB DI ELABORAZIONE DATI ----------'

    'Reset di tutti i dati
    Public Sub Reset_Values()

        'Dimensioni degli strumenti
        SIB_Lenght = SIB_D * 25
        SIB_Height = SIB_D * 15
        GIB_Lenght = GIB_D * 15
        GIB_Height = GIB_D * 15
        MB_Lenght = MB_D * 15
        MB_Height = MB_D * 15

        Raw_Data = ""
        Pitch = 0
        Roll = 0
        Yaw = 0
        Pitch_Wanted = 0
        Roll_Wanted = 0
        Yaw_Wanted = 0
        Pitch_Action = 0
        Roll_Action = 0
        Yaw_Action = 0

        Pitch_P.Text = "0"
        Pitch_I.Text = "0"
        Pitch_D.Text = "0"
        Roll_P.Text = "0"
        Roll_I.Text = "0"
        Roll_D.Text = "0"
        Yaw_P.Text = "0"
        Yaw_I.Text = "0"
        Yaw_D.Text = "0"
        Freq_Hz.Text = "0"

        IsRecording = False
        Message = ""
        DoSend = False

        StartingAltitude = 0
        DataNumber = 0
        Altitude(0) = -1   'Serve per le impostazioni della mappa 
        M_Zoom = 1
        divZoom = True
        MA_cfr = 1
        MA_exp = 2

    End Sub


    '---------- SUB DI COMUNICAZIONE SERIALE ----------'

    'Gestisce la funzione "Select Serial Port"
    Sub GetSerialPortNames()
        'Show all available COM ports.
        ' Get a list of serial port names.

        ' Show all available COM ports.
        For Each sp As String In My.Computer.Ports.SerialPortNames
            ListBoxSerialPorts.Items.Add(sp)
        Next

        'Do While ListBoxSerialPorts.SelectedItem Is Nothing
        '    ' ButtonOk.Enabled = False
        'Loop


    End Sub

    'Gestisce il bottone "Refresh" 
    Private Sub Button_Refresh_Click(sender As Object, e As EventArgs) Handles Button_Refresh.Click
        GetSerialPortNames()
    End Sub

    'Abilita il bottone "OK"
    Private Sub ButtonOk_Show(sender As Object, e As EventArgs) Handles ListBoxSerialPorts.SelectedValueChanged

        If ListBoxSerialPorts.SelectedItem IsNot Nothing Then
            ButtonOk.Enabled = True
        Else
            ButtonOk.Enabled = False
        End If

    End Sub

    '---------- SUB DI GESTIONE DELLA FINESTRA----------'

    'Gestisce l'apertura della finestra
    Private Sub NewFlight_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Flight_Plan = New String(FP_Dimension) {}
        Altitude = New Int32(Data_Dimension) {}
        Latitude = New Double(Data_Dimension) {}
        Longitude = New Double(Data_Dimension) {}

        Reset_Values()

        USB_Port = ""
        ButtonOk.Enabled = False
        Raw_Data = ""


        FlightDataCaller = New Thread(AddressOf Get_Raw_Flight_Data)
        FDCStopThread = False



        ListView_Waypoints.Items.Add("Flight plan not found")

        GetSerialPortNames()



    End Sub

    Private Sub NewFlight_Close(sender As Object, e As EventArgs) Handles MyBase.Closed

        FDCStopThread = True


    End Sub

    '---------- SUB DI GESTIONE DEL MENU----------'

    'Gestisce il bottone "Start flight" del menu "menu"
    Private Sub StartFlightToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StartFlightToolStripMenuItem.Click

    End Sub

    'Gestisce il bottone "Read Telemetry" del menu "menu"
    Private Sub ReadTelemetryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReadTelemetryToolStripMenuItem.Click
        If ReadTelemetryToolStripMenuItem.Checked Then
            FDCStopThread = False
            FlightDataCaller.Start()
        Else
            FDCStopThread = True
        End If


    End Sub

    'Gestisce il bottone "setup" del menu "show"
    Private Sub SetupPanelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PanelToolStripMenu_Setup.Click
        If PanelToolStripMenu_Setup.Checked = True Then
            If PanelToolStripMenuItem_Instruments.Checked Then
                PanelToolStripMenuItem_Instruments.PerformClick()
            End If

            TextBox_Starting_Altitude.Text = Convert.ToString(StartingAltitude)

            LabelSelectSerialPort.Visible = True
            LabelSelectSerialPort.BringToFront()
            ListBoxSerialPorts.Visible = True
            ListBoxSerialPorts.BringToFront()
            ButtonOk.Visible = True
            ButtonOk.BringToFront()
            CheckBox_Record_Flight.Visible = True
            CheckBox_Record_Flight.BringToFront()
            Button_Load_FP.Visible = True
            Button_Load_FP.BringToFront()
            Button_Upload_Setup.Visible = True
            Button_Upload_Setup.BringToFront()
            Label_Invalid_File.BringToFront()
            ListView_Waypoints.Visible = True
            ListView_Waypoints.BringToFront()
            CheckedListBox_Airplane_Setup.Visible = True
            CheckedListBox_Airplane_Setup.BringToFront()
            Button_Refresh.Visible = True
            Button_Refresh.BringToFront()
            Label_Sarting_Altitude.Visible = True
            Label_Sarting_Altitude.BringToFront()
            TextBox_Starting_Altitude.Visible = True
            TextBox_Starting_Altitude.BringToFront()


            Label_PID.Visible = True
            Label_PID.BringToFront()
            Label1.Visible = True
            Label1.BringToFront()
            Label2.Visible = True
            Label2.BringToFront()
            Label3.Visible = True
            Label3.BringToFront()
            Label4.Visible = True
            Label4.BringToFront()
            Label5.Visible = True
            Label5.BringToFront()
            Label6.Visible = True
            Label6.BringToFront()
            Label7.Visible = True
            Label7.BringToFront()
            Label8.Visible = True
            Label8.BringToFront()
            Label9.Visible = True
            Label9.BringToFront()
            Label10.Visible = True
            Label10.BringToFront()



            Pitch_P.Visible = True
            Pitch_P.BringToFront()
            Pitch_I.Visible = True
            Pitch_I.BringToFront()
            Pitch_D.Visible = True
            Pitch_D.BringToFront()
            Roll_P.Visible = True
            Roll_P.BringToFront()
            Roll_I.Visible = True
            Roll_I.BringToFront()
            Roll_D.Visible = True
            Roll_D.BringToFront()
            Yaw_P.Visible = True
            Yaw_P.BringToFront()
            Yaw_I.Visible = True
            Yaw_I.BringToFront()
            Yaw_D.Visible = True
            Yaw_D.BringToFront()
            Freq_Hz.Visible = True
            Freq_Hz.BringToFront()


        Else

            LabelSelectSerialPort.Visible = False
            ListBoxSerialPorts.Visible = False
            ButtonOk.Visible = False
            CheckBox_Record_Flight.Visible = False
            Button_Load_FP.Visible = False
            Button_Upload_Setup.Visible = False
            Label_Invalid_File.Visible = False
            ListView_Waypoints.Visible = False
            CheckedListBox_Airplane_Setup.Visible = False
            Button_Refresh.Visible = False
            Label_Sarting_Altitude.Visible = False
            TextBox_Starting_Altitude.Visible = False

            Label_PID.Visible = False
            Label1.Visible = False
            Label2.Visible = False
            Label3.Visible = False
            Label4.Visible = False
            Label5.Visible = False
            Label6.Visible = False
            Label7.Visible = False
            Label8.Visible = False
            Label9.Visible = False
            Label10.Visible = False

            Pitch_P.Visible = False
            Pitch_I.Visible = False
            Pitch_D.Visible = False
            Roll_P.Visible = False
            Roll_I.Visible = False
            Roll_D.Visible = False
            Yaw_P.Visible = False
            Yaw_I.Visible = False
            Yaw_D.Visible = False
            Freq_Hz.Visible = False

        End If



    End Sub

    'Gestisce il bottone "instruments" del menu "show"
    Private Sub PanelToolStripMenuItem_Instruments_Click(sender As Object, e As EventArgs) Handles PanelToolStripMenuItem_Instruments.Click

        If PanelToolStripMenuItem_Instruments.Checked Then

            If PanelToolStripMenu_Setup.Checked Then
                PanelToolStripMenu_Setup.PerformClick()
            End If

            Label_Map_Style.Location = New Point(MB_X + MB_Lenght + (MB_D \ 2), MB_Y + MB_D + MB_D \ 2 + 5)
            Button_Map_Style.Location = New Point(MB_X + MB_Lenght + (MB_D \ 2) + (MB_D * 3), MB_Y + MB_D + MB_D \ 2)

            Label_Altitude_Style.Location = New Point(MB_X + MB_Lenght + (MB_D \ 2), MB_Y + MB_D * 2 + MB_D \ 2 + 5)
            Button_Altitude_Style.Location = New Point(MB_X + MB_Lenght + (MB_D \ 2) + (MB_D * 3), MB_Y + MB_D * 2 + MB_D \ 2)

            Label_Zoom.Location = New Point(MB_X + MB_Lenght + (MB_D \ 2), MB_Y + MB_D * 3 + MB_D \ 2 + 5)
            Button_Zoom_Plus.Location = New Point(MB_X + MB_Lenght + (MB_D \ 2) + (MB_D * 3), MB_Y + MB_D * 3 + MB_D \ 2)
            Button_Zoom_Minus.Location = New Point(MB_X + MB_Lenght + (MB_D \ 2) + (MB_D * 3) + Button_Zoom_Plus.Width, MB_Y + MB_D * 3 + MB_D \ 2)

            Label_Alt_Lim.Location = New Point(MB_X + MB_Lenght + (MB_D \ 2), MB_Y + MB_D * 4 + MB_D \ 2 + 5)
            Button_Alt_Plus.Location = New Point(MB_X + MB_Lenght + (MB_D \ 2) + (MB_D * 3), MB_Y + MB_D * 4 + MB_D \ 2)
            Button_Alt_Minus.Location = New Point(MB_X + MB_Lenght + (MB_D \ 2) + (MB_D * 3) + Button_Zoom_Plus.Width, MB_Y + MB_D * 4 + MB_D \ 2)

            Button_Map_Style.Visible = True
            Button_Map_Style.BringToFront()
            Button_Altitude_Style.Visible = True
            Button_Altitude_Style.BringToFront()
            Button_Zoom_Plus.Visible = True
            Button_Zoom_Plus.BringToFront()
            Button_Zoom_Minus.Visible = True
            Button_Zoom_Minus.BringToFront()
            Label_Zoom.Visible = True
            Label_Zoom.BringToFront()
            Label_Map_Style.Visible = True
            Label_Map_Style.BringToFront()
            Label_Altitude_Style.Visible = True
            Label_Altitude_Style.BringToFront()

            Button_Alt_Plus.Visible = True
            Button_Alt_Plus.BringToFront()
            Button_Alt_Minus.Visible = True
            Button_Alt_Minus.BringToFront()
            Label_Alt_Lim.Visible = True
            Label_Alt_Lim.BringToFront()
            PictureBoxInstruments.Visible = True

            If Not FlightDataCaller.IsAlive Then
                Window_Paint()
            End If

        Else
            Button_Map_Style.Visible = False
            Button_Altitude_Style.Visible = False
            Button_Zoom_Plus.Visible = False
            Button_Zoom_Minus.Visible = False
            Label_Zoom.Visible = False
            Label_Map_Style.Visible = False
            Label_Altitude_Style.Visible = False
            Button_Alt_Plus.Visible = False
            Button_Alt_Minus.Visible = False
            Label_Alt_Lim.Visible = False
            PictureBoxInstruments.Visible = False
        End If

    End Sub

    'Gestisce il bottone "Communication window" del menu "show"
    Private Sub CommunicationWindowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CommunicationWindowToolStripMenuItem.Click

        cm.TopMost = True
        If cm.IsDisposed Then
            cm = New CreateMessage
            cm.Show()
        Else
            cm.Show()
        End If
        cm.TopMost = True


    End Sub

    '---------- SUB DI GESTIONE DEL SETUP ----------'

    'Gestisce il bottone "Ok"
    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click

        USB_Port = ListBoxSerialPorts.SelectedItem

        com1 = My.Computer.Ports.OpenSerialPort(USB_Port)
        com1.BaudRate = BaudRate
        ButtonOk.Enabled = False

        FlightDataCaller.Start()



    End Sub

    'Gestisce il checkbox "Record Flight"
    Private Sub CheckBox_Record_Flight_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_Record_Flight.CheckedChanged
        If CheckBox_Record_Flight.Checked Then
            IsRecording = True
        Else
            IsRecording = False
        End If


    End Sub

    'Gestisce il textbox "Starting Altitude"
    Private Sub TextBox_Starting_Altitude_TextChanged(sender As Object, e As EventArgs) Handles TextBox_Starting_Altitude.TextChanged
        StartingAltitude = Convert.ToInt32(TextBox_Starting_Altitude.Text)
    End Sub

    'Gestisce il bottone "Load Flight Plan"
    Private Sub Button_Load_FP_Click(sender As Object, e As EventArgs) Handles Button_Load_FP.Click

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

            ListView_Waypoints.Items.Clear()
            ListView_Waypoints.Items.Add("Flight plan Not found")

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

                If Local_Flight_Plan(j).Equals("End") Then
                    StopLoop = True
                ElseIf Not Local_Flight_Plan(j).Contains("$") OrElse Not Local_Flight_Plan(j).ElementAt(Local_Flight_Plan(j).Length - 1) = "$" Then
                    StopLoop = True
                    IsFileValid = False

                End If

                j += 1

            End While
            sR.Close()

        End If

        If IsFileValid = True Then
            Dim i As Integer
            i = 0



            Do While Not Local_Flight_Plan(i).Equals("End")
                Flight_Plan(i) = Local_Flight_Plan(i)
                i += 1
            Loop
            Flight_Plan(i) = Local_Flight_Plan(i)   'passa anche "End"

            Label_Invalid_File.Text = "Done"
            Label_Invalid_File.Visible = True

            Dim z As ListViewItem

            ListView_Waypoints.Items.Clear()


            For Each s As String In Flight_Plan
                z = New ListViewItem(s)
                ListView_Waypoints.Items.Add(z)

                If s.Equals("End") Then
                    Exit For
                End If

            Next

        Else
            Label_Invalid_File.Text = "Error: Invalid file"
            Label_Invalid_File.Visible = True
        End If






    End Sub

    'Gestisce il bottone "Upload Setup"
    Private Sub Button_Upload_Setup_Click(sender As Object, e As EventArgs) Handles Button_Upload_Setup.Click

        LabelSure.Visible = True
        Button_Yes.Visible = True
        Button_No.Visible = True
        LabelSure.BringToFront()
        Button_Yes.BringToFront()
        Button_No.BringToFront()

    End Sub


    '---------- SUB DI "UPLOAD SETUP" ----------'

    'Gestisce il bottone "Yes"
    Private Sub Button_Yes_Click(sender As Object, e As EventArgs) Handles Button_Yes.Click
        FirstFPStringCreation()
        SecondFPStringCreation()
        Upload()
    End Sub

    'Gestisce il bottone "No"
    Private Sub Button_No_Click(sender As Object, e As EventArgs) Handles Button_No.Click
        LabelSure.Visible = False
        Button_Yes.Visible = False
        Button_No.Visible = False
    End Sub

    'Crea la prima stringa da mandare all'aereo come parte del piano di volo
    Private Sub FirstFPStringCreation()

        'serie di if per valutare quali elementi sono stati cliccati e 
        'per comporre una stringa che sarà la prima da passare a radiocomando ed aereo
        'per poterli settare correttamente e in modo più efficace

        FirstFPString = ""

        If CheckedListBox_Airplane_Setup.SelectedItem = "Rudder Yaw Control" Then
            FirstFPString += "ry$"
        End If
        If CheckedListBox_Airplane_Setup.SelectedItem = "Differential Thrust Yaw Control" Then
            FirstFPString += "dy$"
        End If
        If CheckedListBox_Airplane_Setup.SelectedItem = "No Yaw Control" Then
            FirstFPString += "ny$"
        End If
        If CheckedListBox_Airplane_Setup.SelectedItem = "No Roll Control" Then
            FirstFPString += "nr$"
        End If
        If CheckedListBox_Airplane_Setup.SelectedItem = "Transmit video" Then
            FirstFPString += "tv$"
        End If
        If CheckedListBox_Airplane_Setup.SelectedItem = "Save video in onboard SD card" Then
            FirstFPString += "sv$"
        End If
        If CheckedListBox_Airplane_Setup.SelectedItem = "Save flight data in onboard SD card" Then
            FirstFPString += "sf$"
        End If

        FirstFPString += "_"
    End Sub

    'Crea la seconda stringa da mandare all'aereo come parte del piano di volo
    Private Sub SecondFPStringCreation()

        SecondFPString = ""
        SecondFPString += "pp"
        SecondFPString += Pitch_P.Text
        SecondFPString += "$pi"
        SecondFPString += Pitch_I.Text
        SecondFPString += "$pd"
        SecondFPString += Pitch_D.Text
        SecondFPString += "$rp"
        SecondFPString += Roll_P.Text
        SecondFPString += "$ri"
        SecondFPString += Roll_I.Text
        SecondFPString += "$rd"
        SecondFPString += Roll_D.Text
        SecondFPString += "$yp"
        SecondFPString += Yaw_P.Text
        SecondFPString += "$yi"
        SecondFPString += Yaw_I.Text
        SecondFPString += "$yd"
        SecondFPString += Yaw_D.Text
        SecondFPString += "fq"
        SecondFPString += Freq_Hz.Text
        SecondFPString += "_"

    End Sub

    'Carica il piano di volo
    Private Sub Upload()

    End Sub


    '---------- SUB DI GESTIONE DELLA MAPPA ----------'
    'Gestisce il bottone "Map Style"
    Private Sub Button_Map_Style_Click(sender As Object, e As EventArgs) Handles Button_Map_Style.Click
        If Button_Map_Style.Text = "Deg Absolute" Then
            Button_Map_Style.Text = "Deg From starting point"
        ElseIf Button_Map_Style.Text = "Deg From starting point" Then
            Button_Map_Style.Text = "Meters From starting point"
        Else
            Button_Map_Style.Text = "Deg Absolute"
        End If

        If Not FlightDataCaller.IsAlive Then
            Window_Paint()
        End If
    End Sub

    'Gestisce il bottone "Altitude Style"
    Private Sub Button_Altitude_Style_Click(sender As Object, e As EventArgs) Handles Button_Altitude_Style.Click
        If Button_Altitude_Style.Text = "From starting point" Then
            Button_Altitude_Style.Text = "From sea level"
        Else
            Button_Altitude_Style.Text = "From starting point"
        End If

        If Not FlightDataCaller.IsAlive Then
            Window_Paint()
        End If
    End Sub

    'Gestisce il bottone "Zoom -"
    Private Sub Button_Zoom_Minus_Click(sender As Object, e As EventArgs) Handles Button_Zoom_Minus.Click
        If divZoom = True Then
            M_Zoom = M_Zoom / 5
            divZoom = False

        Else
            M_Zoom = M_Zoom / 2
            divZoom = True
        End If

        If Not FlightDataCaller.IsAlive Then
            Window_Paint()
        End If

    End Sub

    'Gestisce il bottone "Zoom +"
    Private Sub Button_Zoom_Plus_Click(sender As Object, e As EventArgs) Handles Button_Zoom_Plus.Click
        If divZoom = True Then
            M_Zoom = M_Zoom * 2
            divZoom = False

        Else
            M_Zoom = M_Zoom * 5
            divZoom = True
        End If

        If Not FlightDataCaller.IsAlive Then
            Window_Paint()
        End If
    End Sub

    'Gestisce il bottone "Altitude -"
    Private Sub Button_Alt_Minus_Click(sender As Object, e As EventArgs) Handles Button_Alt_Minus.Click

        If Max_Altitude > 1 Then

            If MA_cfr = 1 Then
                MA_cfr = 5
                MA_exp -= 1

            ElseIf MA_cfr = 2 Then
                MA_cfr = 1

            Else 'MA_cfr = 5
                MA_cfr = 2

            End If

            Max_Altitude = MA_cfr * (10 ^ MA_exp)

        End If


        If Not FlightDataCaller.IsAlive Then
            Window_Paint()
        End If

    End Sub

    'Gestisce il bottone "Altitude +"
    Private Sub Button_Alt_Plus_Click(sender As Object, e As EventArgs) Handles Button_Alt_Plus.Click

        If MA_exp < 5 Then

            If MA_cfr = 1 Then
                MA_cfr = 2


            ElseIf MA_cfr = 2 Then
                MA_cfr = 5

            Else 'MA_cfr = 5
                MA_cfr = 1
                MA_exp += 1
            End If

            Max_Altitude = MA_cfr * (10 ^ MA_exp)

        End If

        If Not FlightDataCaller.IsAlive Then
            Window_Paint()
        End If

    End Sub

End Class




