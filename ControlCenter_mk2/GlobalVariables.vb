Module GlobalVariables

    '---------- VARIABILI INVIARE MESSAGGI DURANTE IL VOLO ----------'

    Public Message As String 'messaggio da inviare
    Public New_Flight_Plan As String()  'la prima stringa indica che istruzioni eliminare, dove inserirsi ecc
    Public DoSend As Boolean    'indica se è possibile inviare il messaggio



    '---------- VARIABILI PER IL SETUP ----------'

    Public IsRecording As Boolean 'indica se si devono salvare i dati ricevuti
    Public Flight_Plan As String()
    Public FirstFPString As String 'prima stringa del piano di volo
    Public SecondFPString As String 'seconda stringa del piano di volo
    Public Const FP_Dimension = 1000  'Dimensione massima del piano di volo
    Public Const Data_Dimension = 5000  'Dimensione massima dgli array di dati
    Public StartingAltitude As Int32    'altitudine di partenza; va specificata nel setup; default è 0    

    '---------- VARIABILI PER E SUI DATI DI VOLO ----------'

    'Variabili utili per la rielaborazione dei dati
    Public Const PID_Autority = 90   'il massimo valore che i PID possono assumere assumere (quindi hanno un range da + a - PID_Autority)
    Public Const Pitch_Limit = 90
    Public Const Yaw_Limit = 180
    Public Const Roll_Limit = 180
    Public Max_Altitude = 100   'massima altezza visibile inizialmente con lo schema di colori sulla mappa
    'Dati grezzi
    'Public rawData As String
    Public Raw_Data As String
    Public RD_Array As Char()
    'Dati rielaborati, pronti ad essere passati alle funzioni "grafiche"
    Public DataNumber As Int64
    Public Pitch, Roll, Yaw As Int32
    Public Pitch_Wanted, Roll_Wanted, Yaw_Wanted As Int32
    Public Pitch_Action, Roll_Action, Yaw_Action As Int32
    Public Flight_Time As UInt64
    'Dati sulla posizione
    Public Altitude As Int32()
    Public Latitude, Longitude As Double()
    Public nSat As UInt16

    '---------- VARIABILI DI DISEGNO ----------'
    'PaintEvent
    'Public G As Graphics = PictureBoxInstruments.CreateGraphics()
    'Surfaces Indicator Background
    Public Const SIB_X = 50
    Public Const SIB_Y = 100
    Public Const SIB_D = 24
    Public SIB_Lenght As Int32
    Public SIB_Height As Int32
    Public SIB_Brush1 As New SolidBrush(Color.Black)
    Public SIB_Brush2 As New SolidBrush(Color.White)

    'Surfaces Indicator 
    Public SI_Brush1 As New SolidBrush(Color.Orange)
    Public SI_Brush2 As New SolidBrush(Color.Yellow)
    Public SI_Brush3 As New SolidBrush(Color.BlueViolet)
    Public SI_Pen1 As New Pen(Color.Green, 1)
    Public SI_Pen2 As New Pen(Color.Green, 1)
    Public SI_Pen3 As New Pen(Color.Green, 1)
    Public Const PID_factor = 30

    'Gimbal Indicator Background
    Public Const GIB_X = 700
    Public Const GIB_Y = 100
    Public Const GIB_D = 24
    Public GIB_Lenght As Int32
    Public GIB_Height As Int32
    Public GIB_Brush1 As New SolidBrush(Color.Black)
    Public GIB_Brush2 As New SolidBrush(Color.White)
    Public GIB_Brush3 As New SolidBrush(Color.Brown)
    Public GIB_Brush4 As New SolidBrush(Color.Blue)
    Public GIB_Pen1 As New Pen(Color.Gray, 1)
    Public GIB_Brush5 As New SolidBrush(GIB_Pen1.Color)

    'Gimbal Indicator 
    Public GI_Brush1 As New SolidBrush(Color.Black)
    Public GI_Brush2 As New SolidBrush(Color.Green)
    Public GI_Pen1 As New Pen(Color.Black, 2)
    Public GI_Pen2 As New Pen(Color.Green, 2)

    'Map Background
    Public Const MB_X = 50
    Public Const MB_Y = 500
    Public Const MB_D = 24
    Public MB_Lenght As Int32
    Public MB_Height As Int32
    Public MB_Brush1 As New SolidBrush(Color.Black)
    Public MB_Pen1 As New Pen(Color.Green, 1)
    Public MB_Pen2 As New Pen(Color.Green, 1)

    'Map 
    Public M_Zoom As Double   'il fondo scala della mappa sarà [ 1 grado / zoom ]
    Public M_Brush1 As New SolidBrush(Color.Black)
    Public M_Brush2 As New SolidBrush(Color.Green)
    Public M_Pen1 As New Pen(Color.Green, 2)




End Module
