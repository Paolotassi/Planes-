Imports System.Windows.Forms
Imports System
Imports System.IO.Ports

Public Class Button_Open_Instrument_Panel

    'Dim Serial_Port As String
    Dim Raw_Data As String

    'Gestisce la funzione "Open Instrument Panel"
    Public Sub Button_Open_IP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Open_IP.Click

        'Apri la finestra
        Dim ip As New Instrument_Panel
        ip.TopMost = True
        ip.Show()
        ip.Raw_Data = Raw_Data


    End Sub


    Public Sub Get_Raw_Flight_Data()


    End Sub

    'Gestisce la funzione "Select Serial Port"
    Sub GetSerialPortNames()
        'Show all available COM ports.
        ' Get a list of serial port names.

        SerialPort1.Open()

        Dim ports As String() = SerialPorts.GetPortNames()

        ' Show all available COM ports.
        For Each sp As String In My.Computer.Ports.SerialPortNames
            ListBox_Serial_Ports.Items.Add(sp)
        Next



    End Sub

    'Gestisce il bottone "Ok"
    Private Sub Button_Ok_Click(sender As Object, e As EventArgs) Handles Button_Ok.Click

    End Sub

    Private Sub Button_Open_Instrument_Panel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Esegue le varie procedure
        GetSerialPortNames()

        Get_Raw_Flight_Data()


    End Sub


End Class