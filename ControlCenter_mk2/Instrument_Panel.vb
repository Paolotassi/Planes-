
Imports System.Windows.Forms
Imports System.Drawing


Public Class Instrument_Panel


    '---------- VARIABILI DI DISEGNO ----------'

    'Surfaces Indicator Background
    Const SIB_X = 50
    Const SIB_Y = 50
    Const SIB_D = 24
    Dim SIB_Lenght As Int32
    Dim SIB_Height As Int32
    Private SIB_Brush1 As New SolidBrush(Color.Black)
    Private SIB_Brush2 As New SolidBrush(Color.White)

    'Surfaces Indicator 
    Private SI_Brush1 As New SolidBrush(Color.Orange)
    Private SI_Brush2 As New SolidBrush(Color.Yellow)
    Private SI_Brush3 As New SolidBrush(Color.BlueViolet)
    Private SI_Pen1 As New Pen(Color.Green, 1)
    Private SI_Pen2 As New Pen(Color.Green, 1)
    Private SI_Pen3 As New Pen(Color.Green, 1)
    Const PID_factor = 30

    'Gimbal Indicator Background
    Const GIB_X = 700
    Const GIB_Y = 150
    Const GIB_D = 24
    Dim GIB_Lenght As Int32
    Dim GIB_Height As Int32
    Private GIB_Brush1 As New SolidBrush(Color.Black)
    Private GIB_Brush2 As New SolidBrush(Color.White)
    Private GIB_Brush3 As New SolidBrush(Color.Brown)
    Private GIB_Brush4 As New SolidBrush(Color.Blue)
    Private GIB_Pen1 As New Pen(Color.Gray, 1)
    Private GIB_Brush5 As New SolidBrush(GIB_Pen1.Color)

    'Gimbal Indicator 
    Private GI_Brush1 As New SolidBrush(Color.Black)
    Private GI_Brush2 As New SolidBrush(Color.Green)
    Private GI_Pen1 As New Pen(Color.Black, 2)
    Private GI_Pen2 As New Pen(Color.Green, 2)

    '---------- VARIABILI ----------'

    'Variabili utili per la rielaborazione dei dati
    Const PID_Autority = 90   'il massimo valore che i PID possono assumere assumere (quindi hanno un range da + a - PID_Autority)
    Const Pitch_Limit = 90
    Const Yaw_Limit = 180
    Const Roll_Limit = 180
    'Dati grezzi
    Public Raw_Data As String
    Dim RD_Array As Char()
    'Dati rielaborati, pronti ad essere passati alle funzioni "grafiche"
    Dim Pitch, Roll, Yaw As Int32
    Dim Pitch_Wanted, Roll_Wanted, Yaw_Wanted As Int32
    Dim Pitch_Action, Roll_Action, Yaw_Action As Int32




    '---------- SUB DI DISEGNO ----------'

    Public Sub Gimbal_Indicator(ByVal e As System.Windows.Forms.PaintEventArgs)

        'X centro:  GIB_X + (GIB_Lenght\2)      Max estensione in X: ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 6) \ 6)
        'Y centro:  GIB_Y + (GIB_Height\2)      Max estensione in Y: 
        'mezza larghezza: Corte:    GIB_D      Lunghe: (GIB_D \ 2) 

        Dim GI_Triangle As Point()  'serve a disegnare gli indicatori per pitch e yaw
        Dim A, B As Int32
        Dim Angle, Cos_Value, Sin_Value As Double
        GI_Triangle = New Point(2) {}

        Dim Legend_Borders As New Rectangle(New Point(GIB_X + GIB_D + GIB_D \ 4, GIB_Y + GIB_Height + GIB_D \ 4), New Size(GIB_Lenght \ 2 - GIB_X + GIB_D + GIB_D \ 4, GIB_D))
        Dim Legend As New StringFormat(StringFormatFlags.NoClip)

        'Gimbal Indicator Background
        e.Graphics.FillRectangle(GIB_Brush1, GIB_X, GIB_Y + (GIB_D \ 2), GIB_Lenght, GIB_Height - GIB_D)
        e.Graphics.FillRectangle(GIB_Brush1, GIB_X + (GIB_D \ 2), GIB_Y, GIB_Lenght - GIB_D, GIB_Height)
        e.Graphics.FillEllipse(GIB_Brush1, GIB_X, GIB_Y, GIB_D, GIB_D)
        e.Graphics.FillEllipse(GIB_Brush1, GIB_X + GIB_Lenght - GIB_D, GIB_Y, GIB_D, GIB_D)
        e.Graphics.FillEllipse(GIB_Brush1, GIB_X, GIB_Y + GIB_Height - GIB_D, GIB_D, GIB_D)
        e.Graphics.FillEllipse(GIB_Brush1, GIB_X + GIB_Lenght - GIB_D, GIB_Y + GIB_Height - GIB_D, GIB_D, GIB_D)
        e.Graphics.FillRectangle(GIB_Brush4, GIB_X + GIB_D, GIB_Y + GIB_D, GIB_Lenght - (GIB_D * 2), ((GIB_Height - (GIB_D * 2)) \ 2))    'cielo
        e.Graphics.FillRectangle(GIB_Brush3, GIB_X + GIB_D, GIB_Y + (GIB_Height \ 2), GIB_Lenght - (GIB_D * 2), ((GIB_Height - (GIB_D * 2)) \ 2))    'terreno

        'Gimbal indicator legend
        'New Point(40, 40), New Size(80, 80)

        e.Graphics.FillRectangle(GI_Brush1, GIB_X, GIB_Y + GIB_Height + GIB_D \ 4, GIB_D, GIB_D)
        e.Graphics.FillRectangle(GI_Brush2, GIB_X + GIB_Lenght \ 2, GIB_Y + GIB_Height + GIB_D \ 4, GIB_D, GIB_D)
        Legend.LineAlignment = StringAlignment.Center
        Legend.Alignment = StringAlignment.Near
        e.Graphics.DrawString("Plane's attitude", Me.Font, Brushes.Black, RectangleF.op_Implicit(Legend_Borders), Legend)
        Legend_Borders.Location = New Point(GIB_X + GIB_Lenght \ 2 + GIB_D + GIB_D \ 4, GIB_Y + GIB_Height + GIB_D \ 4)
        e.Graphics.DrawString("Plane's wanted attitude", Me.Font, Brushes.Black, RectangleF.op_Implicit(Legend_Borders), Legend)

        'Gimbal Indicator linee di Background Pitch, dall'alto al basso
        e.Graphics.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) - GIB_D, GIB_Y + (GIB_Lenght \ 2) - ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 6) \ 6), GIB_X + (GIB_Lenght \ 2) + GIB_D, GIB_Y + (GIB_Lenght \ 2) - ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 6) \ 6))
        e.Graphics.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) - (GIB_D \ 2), GIB_Y + (GIB_Lenght \ 2) - ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 5) \ 6), GIB_X + (GIB_Lenght \ 2) + (GIB_D \ 2), GIB_Y + (GIB_Lenght \ 2) - ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 5) \ 6))
        e.Graphics.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) - (GIB_D \ 2), GIB_Y + (GIB_Lenght \ 2) - ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 4) \ 6), GIB_X + (GIB_Lenght \ 2) + (GIB_D \ 2), GIB_Y + (GIB_Lenght \ 2) - ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 4) \ 6))
        e.Graphics.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) - GIB_D, GIB_Y + (GIB_Lenght \ 2) - ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 3) \ 6), GIB_X + (GIB_Lenght \ 2) + GIB_D, GIB_Y + (GIB_Lenght \ 2) - ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 3) \ 6))
        e.Graphics.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) - (GIB_D \ 2), GIB_Y + (GIB_Lenght \ 2) - ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 2) \ 6), GIB_X + (GIB_Lenght \ 2) + (GIB_D \ 2), GIB_Y + (GIB_Lenght \ 2) - ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 2) \ 6))
        e.Graphics.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) - (GIB_D \ 2), GIB_Y + (GIB_Lenght \ 2) - ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 1) \ 6), GIB_X + (GIB_Lenght \ 2) + (GIB_D \ 2), GIB_Y + (GIB_Lenght \ 2) - ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 1) \ 6))

        e.Graphics.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) - GIB_D, GIB_Y + (GIB_Lenght \ 2) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 6) \ 6), GIB_X + (GIB_Lenght \ 2) + GIB_D, GIB_Y + (GIB_Lenght \ 2) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 6) \ 6))
        e.Graphics.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) - (GIB_D \ 2), GIB_Y + (GIB_Lenght \ 2) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 5) \ 6), GIB_X + (GIB_Lenght \ 2) + (GIB_D \ 2), GIB_Y + (GIB_Lenght \ 2) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 5) \ 6))
        e.Graphics.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) - (GIB_D \ 2), GIB_Y + (GIB_Lenght \ 2) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 4) \ 6), GIB_X + (GIB_Lenght \ 2) + (GIB_D \ 2), GIB_Y + (GIB_Lenght \ 2) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 4) \ 6))
        e.Graphics.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) - GIB_D, GIB_Y + (GIB_Lenght \ 2) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 3) \ 6), GIB_X + (GIB_Lenght \ 2) + GIB_D, GIB_Y + (GIB_Lenght \ 2) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 3) \ 6))
        e.Graphics.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) - (GIB_D \ 2), GIB_Y + (GIB_Lenght \ 2) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 2) \ 6), GIB_X + (GIB_Lenght \ 2) + (GIB_D \ 2), GIB_Y + (GIB_Lenght \ 2) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 2) \ 6))
        e.Graphics.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) - (GIB_D \ 2), GIB_Y + (GIB_Lenght \ 2) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 1) \ 6), GIB_X + (GIB_Lenght \ 2) + (GIB_D \ 2), GIB_Y + (GIB_Lenght \ 2) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 1) \ 6))

        'Gimbal Indicator linee di Background Yaw, da sinistra a destra
        e.Graphics.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) - ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 6) \ 6), GIB_Y + (GIB_Height \ 2) - GIB_D, GIB_X + (GIB_Lenght \ 2) - ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 6) \ 6), GIB_Y + (GIB_Height \ 2) + GIB_D)
        e.Graphics.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) - ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 5) \ 6), GIB_Y + (GIB_Height \ 2) - (GIB_D \ 2), GIB_X + (GIB_Lenght \ 2) - ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 5) \ 6), GIB_Y + (GIB_Height \ 2) + (GIB_D \ 2))
        e.Graphics.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) - ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 4) \ 6), GIB_Y + (GIB_Height \ 2) - (GIB_D \ 2), GIB_X + (GIB_Lenght \ 2) - ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 4) \ 6), GIB_Y + (GIB_Height \ 2) + (GIB_D \ 2))
        e.Graphics.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) - ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 3) \ 6), GIB_Y + (GIB_Height \ 2) - GIB_D, GIB_X + (GIB_Lenght \ 2) - ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 3) \ 6), GIB_Y + (GIB_Height \ 2) + GIB_D)
        e.Graphics.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) - ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 2) \ 6), GIB_Y + (GIB_Height \ 2) - (GIB_D \ 2), GIB_X + (GIB_Lenght \ 2) - ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 2) \ 6), GIB_Y + (GIB_Height \ 2) + (GIB_D \ 2))
        e.Graphics.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) - ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 1) \ 6), GIB_Y + (GIB_Height \ 2) - (GIB_D \ 2), GIB_X + (GIB_Lenght \ 2) - ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 1) \ 6), GIB_Y + (GIB_Height \ 2) + (GIB_D \ 2))

        e.Graphics.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) + ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 6) \ 6), GIB_Y + (GIB_Height \ 2) - GIB_D, GIB_X + (GIB_Lenght \ 2) + ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 6) \ 6), GIB_Y + (GIB_Height \ 2) + GIB_D)
        e.Graphics.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) + ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 5) \ 6), GIB_Y + (GIB_Height \ 2) - (GIB_D \ 2), GIB_X + (GIB_Lenght \ 2) + ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 5) \ 6), GIB_Y + (GIB_Height \ 2) + (GIB_D \ 2))
        e.Graphics.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) + ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 4) \ 6), GIB_Y + (GIB_Height \ 2) - (GIB_D \ 2), GIB_X + (GIB_Lenght \ 2) + ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 4) \ 6), GIB_Y + (GIB_Height \ 2) + (GIB_D \ 2))
        e.Graphics.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) + ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 3) \ 6), GIB_Y + (GIB_Height \ 2) - GIB_D, GIB_X + (GIB_Lenght \ 2) + ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 3) \ 6), GIB_Y + (GIB_Height \ 2) + GIB_D)
        e.Graphics.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) + ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 2) \ 6), GIB_Y + (GIB_Height \ 2) - (GIB_D \ 2), GIB_X + (GIB_Lenght \ 2) + ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 2) \ 6), GIB_Y + (GIB_Height \ 2) + (GIB_D \ 2))
        e.Graphics.DrawLine(GIB_Pen1, GIB_X + (GIB_Lenght \ 2) + ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 1) \ 6), GIB_Y + (GIB_Height \ 2) - (GIB_D \ 2), GIB_X + (GIB_Lenght \ 2) + ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 1) \ 6), GIB_Y + (GIB_Height \ 2) + (GIB_D \ 2))

        'Gimbal Indicator Numeri a metà linee
        Legend_Borders.Size = New Size(GIB_D * 3 \ 2, GIB_D)
        Legend.LineAlignment = StringAlignment.Center
        Legend.Alignment = StringAlignment.Center
        'Pitch
        Legend_Borders.Location = New Point(GIB_X + (GIB_Lenght \ 2) - (Legend_Borders.Width \ 2), GIB_Y + (GIB_Lenght \ 2) - ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 3) \ 6) - (Legend_Borders.Height \ 2))
        e.Graphics.FillRectangle(GIB_Brush4, Legend_Borders)
        e.Graphics.DrawString("45", Me.Font, GIB_Brush5, RectangleF.op_Implicit(Legend_Borders), Legend)
        Legend_Borders.Location = New Point(GIB_X + (GIB_Lenght \ 2) - (Legend_Borders.Width \ 2), GIB_Y + (GIB_Lenght \ 2) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D) * 3) \ 6) - (Legend_Borders.Height \ 2))
        e.Graphics.FillRectangle(GIB_Brush3, Legend_Borders)
        e.Graphics.DrawString("- 45", Me.Font, GIB_Brush5, RectangleF.op_Implicit(Legend_Borders), Legend)
        'Yaw
        Legend_Borders.Location = New Point(GIB_X + (GIB_Lenght \ 2) - ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 3) \ 6) - (Legend_Borders.Width \ 2), GIB_Y + (GIB_Height \ 2) - (Legend_Borders.Height \ 2))
        e.Graphics.FillRectangle(GIB_Brush4, Legend_Borders.X, Legend_Borders.Y, Legend_Borders.Width, Legend_Borders.Height \ 2)
        e.Graphics.FillRectangle(GIB_Brush3, Legend_Borders.X, Legend_Borders.Y + Legend_Borders.Height \ 2, Legend_Borders.Width, Legend_Borders.Height \ 2)
        e.Graphics.DrawString("- 90", Me.Font, GIB_Brush5, RectangleF.op_Implicit(Legend_Borders), Legend)
        Legend_Borders.Location = New Point(GIB_X + (GIB_Lenght \ 2) + ((((GIB_Lenght) \ 2 - (GIB_D \ 4) - GIB_D) * 3) \ 6) - (Legend_Borders.Width \ 2), GIB_Y + (GIB_Height \ 2) - (Legend_Borders.Height \ 2))
        e.Graphics.FillRectangle(GIB_Brush4, Legend_Borders.X, Legend_Borders.Y, Legend_Borders.Width, Legend_Borders.Height \ 2)
        e.Graphics.FillRectangle(GIB_Brush3, Legend_Borders.X, Legend_Borders.Y + Legend_Borders.Height \ 2, Legend_Borders.Width, Legend_Borders.Height \ 2)
        e.Graphics.DrawString("90", Me.Font, GIB_Brush5, RectangleF.op_Implicit(Legend_Borders), Legend)

        'Gimbal Indicator Pitch
        GI_Triangle(0).X = GIB_X + (GIB_Lenght \ 2)
        GI_Triangle(0).Y = GIB_Y + (GIB_Height \ 2) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D)) * Pitch) \ Pitch_Limit
        GI_Triangle(1).X = GIB_X + (GIB_Lenght \ 2) - (GIB_D \ 2)
        GI_Triangle(1).Y = GIB_Y + (GIB_Height \ 2) + (GIB_D \ 4) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D)) * Pitch) \ Pitch_Limit
        GI_Triangle(2).X = GIB_X + (GIB_Lenght \ 2) - (GIB_D \ 2)
        GI_Triangle(2).Y = GIB_Y + (GIB_Height \ 2) - (GIB_D \ 4) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D)) * Pitch) \ Pitch_Limit
        e.Graphics.FillPolygon(GI_Brush1, GI_Triangle)
        'Gimbal Indicator Pitch__Wanted
        GI_Triangle(0).X = GIB_X + (GIB_Lenght \ 2)
        GI_Triangle(0).Y = GIB_Y + (GIB_Height \ 2) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D)) * Pitch_Wanted) \ Pitch_Limit
        GI_Triangle(1).X = GIB_X + (GIB_Lenght \ 2) + (GIB_D \ 2)
        GI_Triangle(1).Y = GIB_Y + (GIB_Height \ 2) + (GIB_D \ 4) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D)) * Pitch_Wanted) \ Pitch_Limit
        GI_Triangle(2).X = GIB_X + (GIB_Lenght \ 2) + (GIB_D \ 2)
        GI_Triangle(2).Y = GIB_Y + (GIB_Height \ 2) - (GIB_D \ 4) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D)) * Pitch_Wanted) \ Pitch_Limit
        e.Graphics.FillPolygon(GI_Brush2, GI_Triangle)
        'Gimbal Indicator Yaw
        GI_Triangle(0).X = GIB_X + (GIB_Lenght \ 2) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D)) * Yaw) \ Yaw_Limit
        GI_Triangle(0).Y = GIB_Y + (GIB_Height \ 2)
        GI_Triangle(1).X = GIB_X + (GIB_Lenght \ 2) + (GIB_D \ 4) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D)) * Yaw) \ Yaw_Limit
        GI_Triangle(1).Y = GIB_Y + (GIB_Height \ 2) - (GIB_D \ 2)
        GI_Triangle(2).X = GIB_X + (GIB_Lenght \ 2) - (GIB_D \ 4) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D)) * Yaw) \ Yaw_Limit
        GI_Triangle(2).Y = GIB_Y + (GIB_Height \ 2) - (GIB_D \ 2)
        e.Graphics.FillPolygon(GI_Brush1, GI_Triangle)
        'Gimbal Indicator Yaw_Wanted
        GI_Triangle(0).X = GIB_X + (GIB_Lenght \ 2) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D)) * Yaw_Wanted) \ Yaw_Limit
        GI_Triangle(0).Y = GIB_Y + (GIB_Height \ 2)
        GI_Triangle(1).X = GIB_X + (GIB_Lenght \ 2) + (GIB_D \ 4) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D)) * Yaw_Wanted) \ Yaw_Limit
        GI_Triangle(1).Y = GIB_Y + (GIB_Height \ 2) + (GIB_D \ 2)
        GI_Triangle(2).X = GIB_X + (GIB_Lenght \ 2) - (GIB_D \ 4) + ((((GIB_Height) \ 2 - (GIB_D \ 4) - GIB_D)) * Yaw_Wanted) \ Yaw_Limit
        GI_Triangle(2).Y = GIB_Y + (GIB_Height \ 2) + (GIB_D \ 2)
        e.Graphics.FillPolygon(GI_Brush2, GI_Triangle)


        'Gimbal Indicator Roll_Wanted
        If (Roll_Wanted <= 45 And Roll_Wanted >= -45) Or (Roll_Wanted >= 135 Or Roll_Wanted <= -135) Then
            Angle = (Roll_Wanted * Math.PI) / 180
            Sin_Value = Math.Sin(Angle * 2)
            Cos_Value = Math.Cos(Angle * 2)
            A = Math.Round((((GIB_Height - GIB_D * 2) \ 2) * Sin_Value) \ Math.Sin((45 * Math.PI) / 180))
            e.Graphics.DrawLine(GI_Pen2, GIB_X + GIB_D, GIB_Y + (GIB_Height \ 2) - A, GIB_X + GIB_Lenght - GIB_D, GIB_Y + (GIB_Height \ 2) + A)
            Sin_Value = Math.Sin(Angle)
            Cos_Value = Math.Cos(Angle)
            A = Math.Round((GIB_D) * Sin_Value)
            B = Math.Round((GIB_D) * Cos_Value)
            e.Graphics.DrawLine(GI_Pen2, GIB_X + (GIB_Lenght \ 2), GIB_Y + (GIB_Height \ 2), GIB_X + (GIB_Lenght \ 2) + A, GIB_Y + (GIB_Height \ 2) - B)
        Else
            Angle = ((Roll_Wanted - 90) * Math.PI) / 180
            Sin_Value = Math.Sin(Angle * 2)
            Cos_Value = Math.Cos(Angle * 2)
            A = Math.Round((((GIB_Height - GIB_D * 2) \ 2) * Sin_Value) \ Math.Sin((45 * Math.PI) / 180))
            e.Graphics.DrawLine(GI_Pen2, GIB_X + (GIB_Lenght \ 2) + A, GIB_Y + GIB_D, GIB_X + (GIB_Lenght \ 2) - A, GIB_Y + GIB_Lenght - GIB_D)
            Sin_Value = Math.Sin(Angle)
            Cos_Value = Math.Cos(Angle)
            A = Math.Round((GIB_D) * Sin_Value)
            B = Math.Round((GIB_D) * Cos_Value)
            e.Graphics.DrawLine(GI_Pen2, GIB_X + (GIB_Lenght \ 2), GIB_Y + (GIB_Height \ 2), GIB_X + (GIB_Lenght \ 2) + B, GIB_Y + (GIB_Height \ 2) + A)
        End If

        'Gimbal Indicator Roll
        If (Roll <= 45 And Roll >= -45) Or (Roll >= 135 Or Roll <= -135) Then
            Angle = (Roll * Math.PI) / 180
            Sin_Value = Math.Sin(Angle * 2)
            Cos_Value = Math.Cos(Angle * 2)
            A = Math.Round((((GIB_Height - GIB_D * 2) \ 2) * Sin_Value) \ Math.Sin((45 * Math.PI) / 180))
            e.Graphics.DrawLine(GI_Pen1, GIB_X + GIB_D, GIB_Y + (GIB_Height \ 2) - A, GIB_X + GIB_Lenght - GIB_D, GIB_Y + (GIB_Height \ 2) + A)
            Sin_Value = Math.Sin(Angle)
            Cos_Value = Math.Cos(Angle)
            A = Math.Round((GIB_D) * Sin_Value)
            B = Math.Round((GIB_D) * Cos_Value)
            e.Graphics.DrawLine(GI_Pen1, GIB_X + (GIB_Lenght \ 2), GIB_Y + (GIB_Height \ 2), GIB_X + (GIB_Lenght \ 2) + A, GIB_Y + (GIB_Height \ 2) - B)
        Else
            Angle = ((Roll - 90) * Math.PI) / 180
            Sin_Value = Math.Sin(Angle * 2)
            Cos_Value = Math.Cos(Angle * 2)
            A = Math.Round((((GIB_Height - GIB_D * 2) \ 2) * Sin_Value) \ Math.Sin((45 * Math.PI) / 180))
            e.Graphics.DrawLine(GI_Pen1, GIB_X + (GIB_Lenght \ 2) + A, GIB_Y + GIB_D, GIB_X + (GIB_Lenght \ 2) - A, GIB_Y + GIB_Lenght - GIB_D)
            Sin_Value = Math.Sin(Angle)
            Cos_Value = Math.Cos(Angle)
            A = Math.Round((GIB_D) * Sin_Value)
            B = Math.Round((GIB_D) * Cos_Value)
            e.Graphics.DrawLine(GI_Pen1, GIB_X + (GIB_Lenght \ 2), GIB_Y + (GIB_Height \ 2), GIB_X + (GIB_Lenght \ 2) + B, GIB_Y + (GIB_Height \ 2) + A)
        End If






    End Sub

    Public Sub Surfaces_Indicator(ByVal e As System.Windows.Forms.PaintEventArgs)
        'Surfaces Indicator linee color
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
        e.Graphics.FillRectangle(SIB_Brush1, SIB_X, SIB_Y + (SIB_D \ 2), SIB_Lenght, SIB_Height - SIB_D)
        e.Graphics.FillRectangle(SIB_Brush1, SIB_X + (SIB_D \ 2), SIB_Y, SIB_Lenght - SIB_D, SIB_Height)
        e.Graphics.FillEllipse(SIB_Brush1, SIB_X, SIB_Y, SIB_D, SIB_D)
        e.Graphics.FillEllipse(SIB_Brush1, SIB_X + SIB_Lenght - SIB_D, SIB_Y, SIB_D, SIB_D)
        e.Graphics.FillEllipse(SIB_Brush1, SIB_X, SIB_Y + SIB_Height - SIB_D, SIB_D, SIB_D)
        e.Graphics.FillEllipse(SIB_Brush1, SIB_X + SIB_Lenght - SIB_D, SIB_Y + SIB_Height - SIB_D, SIB_D, SIB_D)
        'Surfaces Indicator Icona aereo
        e.Graphics.FillEllipse(SIB_Brush2, SIB_X + (SIB_Lenght \ 2) - SIB_D, SIB_Y + (SIB_D * 3) + (SIB_Height \ 2), SIB_D * 2, SIB_D * 2) 'Fusoliera
        e.Graphics.FillRectangle(SIB_Brush2, SIB_X + SIB_D, SIB_Y + (SIB_D * 3) + (SIB_Height \ 2), SIB_Lenght - (SIB_D * 2), SIB_D \ 2) 'ala
        e.Graphics.FillRectangle(SIB_Brush2, SIB_X + (SIB_Lenght \ 2) - (SIB_D \ 8), SIB_Y + (SIB_D * 4), SIB_D \ 4, (SIB_Height \ 2) - SIB_D) 'timone
        e.Graphics.FillRectangle(SIB_Brush2, SIB_X + (SIB_Lenght \ 2) - (SIB_D * 4), SIB_Y + (SIB_D * 4), SIB_D * 8, SIB_D \ 4) 'stabilizzatore
        'Surfaces Indicator linee di posizione massima
        e.Graphics.DrawLine(SI_Pen1, SIB_X + SIB_D + SIB_D, SIB_Y + (SIB_D * 3) + (SIB_Height \ 2) - SI_Pen1.Width - ((PID_Autority * SIB_D) \ PID_factor), SIB_X + SIB_D + SIB_D + (SIB_Lenght - (SIB_D)) \ 2 - (SIB_D * 5), SIB_Y + (SIB_D * 3) + (SIB_Height \ 2) - SI_Pen1.Width - ((PID_Autority * SIB_D) \ PID_factor))   'alettone sx sopra
        e.Graphics.DrawLine(SI_Pen1, SIB_X + SIB_D + SIB_D, SIB_Y + (SIB_D * 3) + (SIB_D \ 2) + (SIB_Height \ 2) - SI_Pen1.Width + ((PID_Autority * SIB_D) \ PID_factor), SIB_X + SIB_D + SIB_D + (SIB_Lenght - (SIB_D)) \ 2 - (SIB_D * 5), SIB_Y + (SIB_D * 3) + (SIB_D \ 2) + (SIB_Height \ 2) - SI_Pen1.Width + ((PID_Autority * SIB_D) \ PID_factor))   'alettone sx sotto
        e.Graphics.DrawLine(SI_Pen1, SIB_X + SIB_Lenght - (SIB_D + SIB_D + ((SIB_Lenght - (SIB_D)) \ 2 - (SIB_D * 5))), SIB_Y + (SIB_D * 3) + (SIB_Height \ 2) - SI_Pen1.Width - ((PID_Autority * SIB_D) \ PID_factor), SIB_X + SIB_Lenght - (SIB_D + SIB_D + ((SIB_Lenght - (SIB_D)) \ 2 - (SIB_D * 5))) + (SIB_Lenght - (SIB_D)) \ 2 - (SIB_D * 5), SIB_Y + (SIB_D * 3) + (SIB_Height \ 2) - SI_Pen1.Width - ((PID_Autority * SIB_D) \ PID_factor))   'alettone dx sopra
        e.Graphics.DrawLine(SI_Pen1, SIB_X + SIB_Lenght - (SIB_D + SIB_D + ((SIB_Lenght - (SIB_D)) \ 2 - (SIB_D * 5))), SIB_Y + (SIB_D * 3) + (SIB_D \ 2) + (SIB_Height \ 2) - SI_Pen1.Width + ((PID_Autority * SIB_D) \ PID_factor), SIB_X + SIB_Lenght - (SIB_D + SIB_D + ((SIB_Lenght - (SIB_D)) \ 2 - (SIB_D * 5))) + (SIB_Lenght - (SIB_D)) \ 2 - (SIB_D * 5), SIB_Y + (SIB_D * 3) + (SIB_D \ 2) + (SIB_Height \ 2) - SI_Pen1.Width + ((PID_Autority * SIB_D) \ PID_factor))   'alettone dx sotto
        e.Graphics.DrawLine(SI_Pen2, SIB_X + (SIB_Lenght \ 2) - (SIB_D * 3), SIB_Y + (SIB_D * 4) - SI_Pen1.Width - ((PID_Autority * SIB_D) \ PID_factor), SIB_X + (SIB_Lenght \ 2) - (SIB_D * 3) + (SIB_D * 6), SIB_Y + (SIB_D * 4) - SI_Pen1.Width - ((PID_Autority * SIB_D) \ PID_factor))  'stabilizzatore sopra
        e.Graphics.DrawLine(SI_Pen2, SIB_X + (SIB_Lenght \ 2) - (SIB_D * 3), SIB_Y + (SIB_D * 4) + SIB_D \ 4 - SI_Pen1.Width + ((PID_Autority * SIB_D) \ PID_factor), SIB_X + (SIB_Lenght \ 2) - (SIB_D * 3) + (SIB_D * 6), SIB_Y + (SIB_D * 4) + SIB_D \ 4 - SI_Pen1.Width + ((PID_Autority * SIB_D) \ PID_factor))  'stabilizzatore sotto
        e.Graphics.DrawLine(SI_Pen3, SIB_X + (SIB_Lenght \ 2) - (SIB_D \ 8) - SI_Pen1.Width - ((PID_Autority * SIB_D) \ PID_factor), SIB_Y + (SIB_D * 4) + (SIB_D * 2), SIB_X + (SIB_Lenght \ 2) - (SIB_D \ 8) - SI_Pen1.Width - ((PID_Autority * SIB_D) \ PID_factor), SIB_Y + (SIB_D * 4) + (SIB_D * 2) + (SIB_Height \ 2) - (SIB_D * 4))    'timone sx
        e.Graphics.DrawLine(SI_Pen3, SIB_X + (SIB_Lenght \ 2) - (SIB_D \ 8) + SIB_D \ 4 - SI_Pen1.Width + ((PID_Autority * SIB_D) \ PID_factor), SIB_Y + (SIB_D * 4) + (SIB_D * 2), SIB_X + (SIB_Lenght \ 2) - (SIB_D \ 8) + SIB_D \ 4 - SI_Pen1.Width + ((PID_Autority * SIB_D) \ PID_factor), SIB_Y + (SIB_D * 4) + (SIB_D * 2) + (SIB_Height \ 2) - (SIB_D * 4))    'timone dx
        'Surfaces Indicator Superfici azionate
        e.Graphics.FillRectangle(SI_Brush3, SIB_X + (SIB_Lenght \ 2) - (SIB_D \ 8), SIB_Y + (SIB_D * 4) + (SIB_D * 2), SIB_D \ 4, (SIB_Height \ 2) - (SIB_D * 4)) 'timone
        If Yaw_Action > 0 Then
            e.Graphics.FillRectangle(SI_Brush3, SIB_X + (SIB_Lenght \ 2) + (SIB_D \ 8), SIB_Y + (SIB_D * 4) + (SIB_D * 2), ((Yaw_Action * SIB_D) \ PID_factor), (SIB_Height \ 2) - (SIB_D * 4)) 'timone
        Else
            e.Graphics.FillRectangle(SI_Brush3, SIB_X + (SIB_Lenght \ 2) - (SIB_D \ 8) + ((Yaw_Action * SIB_D) \ PID_factor), SIB_Y + (SIB_D * 4) + (SIB_D * 2), -((Yaw_Action * SIB_D) \ PID_factor), (SIB_Height \ 2) - (SIB_D * 4)) 'timone
        End If

        e.Graphics.FillRectangle(SI_Brush2, SIB_X + (SIB_Lenght \ 2) - (SIB_D * 3), SIB_Y + (SIB_D * 4), SIB_D * 6, SIB_D \ 4) 'stabilizzatore
        If Pitch_Action > 0 Then
            e.Graphics.FillRectangle(SI_Brush2, SIB_X + (SIB_Lenght \ 2) - (SIB_D * 3), SIB_Y + (SIB_D * 4) - ((Pitch_Action * SIB_D) \ PID_factor), SIB_D * 6, ((Pitch_Action * SIB_D) \ PID_factor)) 'stabilizzatore
        Else
            e.Graphics.FillRectangle(SI_Brush2, SIB_X + (SIB_Lenght \ 2) - (SIB_D * 3), SIB_Y + (SIB_D * 4), SIB_D * 6, (SIB_D \ 4) - ((Pitch_Action * SIB_D) \ PID_factor)) 'stabilizzatore
        End If

        e.Graphics.FillRectangle(SI_Brush1, SIB_X + SIB_D + SIB_D, SIB_Y + (SIB_D * 3) + (SIB_Height \ 2), (SIB_Lenght - (SIB_D)) \ 2 - (SIB_D * 5), SIB_D \ 2) 'alettone sx
        e.Graphics.FillRectangle(SI_Brush1, SIB_X + SIB_Lenght - (SIB_D + SIB_D + ((SIB_Lenght - (SIB_D)) \ 2 - (SIB_D * 5))), SIB_Y + (SIB_D * 3) + (SIB_Height \ 2), (SIB_Lenght - (SIB_D)) \ 2 - (SIB_D * 5), SIB_D \ 2) 'alettone dx
        If Roll_Action > 0 Then
            e.Graphics.FillRectangle(SI_Brush1, SIB_X + SIB_D + SIB_D, SIB_Y + (SIB_D * 3) + (SIB_Height \ 2), (SIB_Lenght - (SIB_D)) \ 2 - (SIB_D * 5), SIB_D \ 2 + ((Roll_Action * SIB_D) \ PID_factor)) 'alettone sx
            e.Graphics.FillRectangle(SI_Brush1, SIB_X + SIB_Lenght - (SIB_D + SIB_D + ((SIB_Lenght - (SIB_D)) \ 2 - (SIB_D * 5))), SIB_Y + (SIB_D * 3) + (SIB_Height \ 2) - ((Roll_Action * SIB_D) \ PID_factor), (SIB_Lenght - (SIB_D)) \ 2 - (SIB_D * 5), SIB_D \ 2 + ((Roll_Action * SIB_D) \ PID_factor)) 'alettone dx
        Else
            e.Graphics.FillRectangle(SI_Brush1, SIB_X + SIB_D + SIB_D, SIB_Y + (SIB_D * 3) + (SIB_Height \ 2) + ((Roll_Action * SIB_D) \ PID_factor), (SIB_Lenght - (SIB_D)) \ 2 - (SIB_D * 5), -((Roll_Action * SIB_D) \ PID_factor)) 'alettone sx
            e.Graphics.FillRectangle(SI_Brush1, SIB_X + SIB_Lenght - (SIB_D + SIB_D + ((SIB_Lenght - (SIB_D)) \ 2 - (SIB_D * 5))), SIB_Y + (SIB_D * 3) + (SIB_Height \ 2), (SIB_Lenght - (SIB_D)) \ 2 - (SIB_D * 5), SIB_D \ 2 - ((Roll_Action * SIB_D) \ PID_factor)) 'alettone dx
        End If

    End Sub

    Private Sub Window_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint

        Gimbal_Indicator(e)

        Surfaces_Indicator(e)


    End Sub





    '---------- SUB DI DEBUG ----------'

    'Mette dati di prova negli strumenti
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Pitch_Action = Convert.ToInt32(TextBox1.Text)
        Roll_Action = Convert.ToInt32(TextBox1.Text)
        Yaw_Action = Convert.ToInt32(TextBox1.Text)

        Pitch = Convert.ToInt32(TextBox2.Text)
        Roll = Convert.ToInt32(TextBox3.Text)
        Yaw = Convert.ToInt32(TextBox3.Text)

        Pitch_Wanted = -Convert.ToInt32(TextBox2.Text)
        Roll_Wanted = -Convert.ToInt32(TextBox3.Text)
        Yaw_Wanted = -Convert.ToInt32(TextBox3.Text)
        Invalidate()
    End Sub


    '---------- SUB DI ELABORAZIONE DATI ----------'

    'Prende le stringhe ricevute dalla seriale e le trasforma in "dati rielaborati"
    Public Sub Data_Preparation()
        'La stringa di dati è strutturata con coppie di lettere seguite da una stringa di numeri che terminano con "$" (es. la0451325$). 
        'La stringa complessiva termina con "_"
        'Di seguito la legenda
        'la = latitudine    lo = longitudine        al = altitudine
        'pi = pitch         pw = pitch wanted       pa = pitch action
        'ro = roll          rw = roll wanted        ra = roll action
        'ya = yaw           yw = yaw wanted         ya = yaw action
        'sp = speed         po = power %

        Dim i, max As Integer
        Dim Part As String

        i = 0

        max = Raw_Data.IndexOf("\")

        RD_Array = Raw_Data.ToCharArray
        Do While i < max
            'mancano i casi della posizione e del motore

            Part = ToString(RD_Array(i) + RD_Array(i + 1))
            Select Case (Part)
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

            End Select

            i += 1

        Loop

    End Sub

    'Mostra i dati rielaborati, utilizzando grafici, indicatori e simili
    Public Sub Data_Show()


        Invalidate()

    End Sub

    '---------- SUB DI SETUP, RESET ----------'

    'Reser di tutti i dati
    Public Sub Reset_Values()

        Pitch = 0
        Roll = 0
        Yaw = 0
        Pitch_Wanted = 0
        Roll_Wanted = 0
        Yaw_Wanted = 0
        Pitch_Action = 0
        Roll_Action = 0
        Yaw_Action = 0

    End Sub

    'Setup della finesta
    'Prepara le immagini della schermata
    Private Sub Create_Flight_Plan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        Reset_Values()

        'Dimensioni degli strumenti

        SIB_Lenght = SIB_D * 25
        SIB_Height = SIB_D * 15

        GIB_Lenght = GIB_D * 15
        GIB_Height = GIB_D * 15


        Invalidate()

    End Sub

End Class