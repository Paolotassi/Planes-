<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class NewFlight
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla mediante l'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.LabelSelectSerialPort = New System.Windows.Forms.Label()
        Me.ListBoxSerialPorts = New System.Windows.Forms.ListBox()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenu_Menu = New System.Windows.Forms.ToolStripMenuItem()
        Me.StartFlightToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ResetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReadTelemetryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenu_Show = New System.Windows.Forms.ToolStripMenuItem()
        Me.PanelToolStripMenu_Setup = New System.Windows.Forms.ToolStripMenuItem()
        Me.PanelToolStripMenuItem_Instruments = New System.Windows.Forms.ToolStripMenuItem()
        Me.CommunicationWindowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Button_Upload_Setup = New System.Windows.Forms.Button()
        Me.Button_Load_FP = New System.Windows.Forms.Button()
        Me.CheckBox_Record_Flight = New System.Windows.Forms.CheckBox()
        Me.Label_Invalid_File = New System.Windows.Forms.Label()
        Me.ListView_Waypoints = New System.Windows.Forms.ListView()
        Me.ColumnHeaderWaypoints = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.PictureBoxInstruments = New System.Windows.Forms.PictureBox()
        Me.Label_Airplane_Setup = New System.Windows.Forms.Label()
        Me.CheckedListBox_Airplane_Setup = New System.Windows.Forms.CheckedListBox()
        Me.LabelSure = New System.Windows.Forms.Label()
        Me.Button_Yes = New System.Windows.Forms.Button()
        Me.Button_No = New System.Windows.Forms.Button()
        Me.Label_PID = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Pitch_P = New System.Windows.Forms.TextBox()
        Me.Pitch_I = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Pitch_D = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Roll_D = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Roll_I = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Roll_P = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Yaw_D = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Yaw_I = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Yaw_P = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Freq_Hz = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Button_Map_Style = New System.Windows.Forms.Button()
        Me.Button_Zoom_Plus = New System.Windows.Forms.Button()
        Me.Button_Zoom_Minus = New System.Windows.Forms.Button()
        Me.Label_Zoom = New System.Windows.Forms.Label()
        Me.Label_Map_Style = New System.Windows.Forms.Label()
        Me.Label_Alt_Lim = New System.Windows.Forms.Label()
        Me.Button_Alt_Minus = New System.Windows.Forms.Button()
        Me.Button_Alt_Plus = New System.Windows.Forms.Button()
        Me.Button_Refresh = New System.Windows.Forms.Button()
        Me.Label_Altitude_Style = New System.Windows.Forms.Label()
        Me.Button_Altitude_Style = New System.Windows.Forms.Button()
        Me.Label_Sarting_Altitude = New System.Windows.Forms.Label()
        Me.TextBox_Starting_Altitude = New System.Windows.Forms.TextBox()
        Me.MenuStrip2.SuspendLayout()
        CType(Me.PictureBoxInstruments, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelSelectSerialPort
        '
        Me.LabelSelectSerialPort.AutoSize = True
        Me.LabelSelectSerialPort.Location = New System.Drawing.Point(12, 195)
        Me.LabelSelectSerialPort.Name = "LabelSelectSerialPort"
        Me.LabelSelectSerialPort.Size = New System.Drawing.Size(88, 13)
        Me.LabelSelectSerialPort.TabIndex = 0
        Me.LabelSelectSerialPort.Text = "Select Serial Port"
        Me.LabelSelectSerialPort.Visible = False
        '
        'ListBoxSerialPorts
        '
        Me.ListBoxSerialPorts.FormattingEnabled = True
        Me.ListBoxSerialPorts.Location = New System.Drawing.Point(12, 211)
        Me.ListBoxSerialPorts.Name = "ListBoxSerialPorts"
        Me.ListBoxSerialPorts.Size = New System.Drawing.Size(117, 95)
        Me.ListBoxSerialPorts.TabIndex = 1
        Me.ListBoxSerialPorts.Visible = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(12, 312)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(53, 23)
        Me.ButtonOk.TabIndex = 2
        Me.ButtonOk.TabStop = False
        Me.ButtonOk.Text = "Ok"
        Me.ButtonOk.UseVisualStyleBackColor = True
        Me.ButtonOk.Visible = False
        '
        'MenuStrip2
        '
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenu_Menu, Me.ToolStripMenu_Show})
        Me.MenuStrip2.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Size = New System.Drawing.Size(2000, 24)
        Me.MenuStrip2.TabIndex = 8
        Me.MenuStrip2.Text = "MenuStrip2"
        '
        'ToolStripMenu_Menu
        '
        Me.ToolStripMenu_Menu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StartFlightToolStripMenuItem, Me.ResetToolStripMenuItem, Me.ReadTelemetryToolStripMenuItem})
        Me.ToolStripMenu_Menu.Name = "ToolStripMenu_Menu"
        Me.ToolStripMenu_Menu.Size = New System.Drawing.Size(50, 20)
        Me.ToolStripMenu_Menu.Text = "Menu"
        '
        'StartFlightToolStripMenuItem
        '
        Me.StartFlightToolStripMenuItem.Enabled = False
        Me.StartFlightToolStripMenuItem.Name = "StartFlightToolStripMenuItem"
        Me.StartFlightToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.StartFlightToolStripMenuItem.Text = "Start Flight"
        Me.StartFlightToolStripMenuItem.ToolTipText = "Invalid Setup"
        '
        'ResetToolStripMenuItem
        '
        Me.ResetToolStripMenuItem.Name = "ResetToolStripMenuItem"
        Me.ResetToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.ResetToolStripMenuItem.Text = "Reset"
        '
        'ReadTelemetryToolStripMenuItem
        '
        Me.ReadTelemetryToolStripMenuItem.CheckOnClick = True
        Me.ReadTelemetryToolStripMenuItem.Name = "ReadTelemetryToolStripMenuItem"
        Me.ReadTelemetryToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.ReadTelemetryToolStripMenuItem.Text = "Read telemetry"
        Me.ReadTelemetryToolStripMenuItem.ToolTipText = "Shortcut to avoid the setup fase"
        '
        'ToolStripMenu_Show
        '
        Me.ToolStripMenu_Show.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PanelToolStripMenu_Setup, Me.PanelToolStripMenuItem_Instruments, Me.CommunicationWindowToolStripMenuItem})
        Me.ToolStripMenu_Show.Name = "ToolStripMenu_Show"
        Me.ToolStripMenu_Show.Size = New System.Drawing.Size(48, 20)
        Me.ToolStripMenu_Show.Text = "Show"
        '
        'PanelToolStripMenu_Setup
        '
        Me.PanelToolStripMenu_Setup.CheckOnClick = True
        Me.PanelToolStripMenu_Setup.Name = "PanelToolStripMenu_Setup"
        Me.PanelToolStripMenu_Setup.Size = New System.Drawing.Size(206, 22)
        Me.PanelToolStripMenu_Setup.Text = "Setup Panel"
        '
        'PanelToolStripMenuItem_Instruments
        '
        Me.PanelToolStripMenuItem_Instruments.CheckOnClick = True
        Me.PanelToolStripMenuItem_Instruments.Name = "PanelToolStripMenuItem_Instruments"
        Me.PanelToolStripMenuItem_Instruments.Size = New System.Drawing.Size(206, 22)
        Me.PanelToolStripMenuItem_Instruments.Text = "Instruments panel"
        '
        'CommunicationWindowToolStripMenuItem
        '
        Me.CommunicationWindowToolStripMenuItem.Name = "CommunicationWindowToolStripMenuItem"
        Me.CommunicationWindowToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
        Me.CommunicationWindowToolStripMenuItem.Text = "Communication window"
        '
        'Button_Upload_Setup
        '
        Me.Button_Upload_Setup.Location = New System.Drawing.Point(12, 410)
        Me.Button_Upload_Setup.Name = "Button_Upload_Setup"
        Me.Button_Upload_Setup.Size = New System.Drawing.Size(471, 23)
        Me.Button_Upload_Setup.TabIndex = 10
        Me.Button_Upload_Setup.Text = "Upload Setup"
        Me.Button_Upload_Setup.UseVisualStyleBackColor = True
        Me.Button_Upload_Setup.Visible = False
        '
        'Button_Load_FP
        '
        Me.Button_Load_FP.Location = New System.Drawing.Point(12, 364)
        Me.Button_Load_FP.Name = "Button_Load_FP"
        Me.Button_Load_FP.Size = New System.Drawing.Size(117, 23)
        Me.Button_Load_FP.TabIndex = 11
        Me.Button_Load_FP.Text = "Load Flight Plan"
        Me.Button_Load_FP.UseVisualStyleBackColor = True
        Me.Button_Load_FP.Visible = False
        '
        'CheckBox_Record_Flight
        '
        Me.CheckBox_Record_Flight.AutoSize = True
        Me.CheckBox_Record_Flight.Location = New System.Drawing.Point(15, 341)
        Me.CheckBox_Record_Flight.Name = "CheckBox_Record_Flight"
        Me.CheckBox_Record_Flight.Size = New System.Drawing.Size(89, 17)
        Me.CheckBox_Record_Flight.TabIndex = 12
        Me.CheckBox_Record_Flight.Text = "Record Flight"
        Me.CheckBox_Record_Flight.UseVisualStyleBackColor = True
        Me.CheckBox_Record_Flight.Visible = False
        '
        'Label_Invalid_File
        '
        Me.Label_Invalid_File.AutoSize = True
        Me.Label_Invalid_File.Location = New System.Drawing.Point(135, 369)
        Me.Label_Invalid_File.Name = "Label_Invalid_File"
        Me.Label_Invalid_File.Size = New System.Drawing.Size(82, 13)
        Me.Label_Invalid_File.TabIndex = 13
        Me.Label_Invalid_File.Text = "Error: Invalid file"
        Me.Label_Invalid_File.Visible = False
        '
        'ListView_Waypoints
        '
        Me.ListView_Waypoints.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeaderWaypoints})
        Me.ListView_Waypoints.GridLines = True
        Me.ListView_Waypoints.HideSelection = False
        Me.ListView_Waypoints.Location = New System.Drawing.Point(453, 27)
        Me.ListView_Waypoints.Name = "ListView_Waypoints"
        Me.ListView_Waypoints.Size = New System.Drawing.Size(249, 327)
        Me.ListView_Waypoints.TabIndex = 14
        Me.ListView_Waypoints.UseCompatibleStateImageBehavior = False
        Me.ListView_Waypoints.View = System.Windows.Forms.View.Details
        Me.ListView_Waypoints.Visible = False
        '
        'ColumnHeaderWaypoints
        '
        Me.ColumnHeaderWaypoints.Text = "Waypoints"
        Me.ColumnHeaderWaypoints.Width = 245
        '
        'PictureBoxInstruments
        '
        Me.PictureBoxInstruments.Location = New System.Drawing.Point(0, 27)
        Me.PictureBoxInstruments.Name = "PictureBoxInstruments"
        Me.PictureBoxInstruments.Size = New System.Drawing.Size(2000, 2000)
        Me.PictureBoxInstruments.TabIndex = 15
        Me.PictureBoxInstruments.TabStop = False
        Me.PictureBoxInstruments.Visible = False
        '
        'Label_Airplane_Setup
        '
        Me.Label_Airplane_Setup.AutoSize = True
        Me.Label_Airplane_Setup.Location = New System.Drawing.Point(9, 27)
        Me.Label_Airplane_Setup.Name = "Label_Airplane_Setup"
        Me.Label_Airplane_Setup.Size = New System.Drawing.Size(76, 13)
        Me.Label_Airplane_Setup.TabIndex = 16
        Me.Label_Airplane_Setup.Text = "Airplane Setup"
        Me.Label_Airplane_Setup.Visible = False
        '
        'CheckedListBox_Airplane_Setup
        '
        Me.CheckedListBox_Airplane_Setup.FormattingEnabled = True
        Me.CheckedListBox_Airplane_Setup.Items.AddRange(New Object() {"Rudder yaw control", "Differential thrust yaw control", "No yaw control", "No roll control", "Transmit video", "Save video in onboard SD card", "Save flight data in onboard SD card"})
        Me.CheckedListBox_Airplane_Setup.Location = New System.Drawing.Point(12, 43)
        Me.CheckedListBox_Airplane_Setup.Name = "CheckedListBox_Airplane_Setup"
        Me.CheckedListBox_Airplane_Setup.Size = New System.Drawing.Size(199, 109)
        Me.CheckedListBox_Airplane_Setup.TabIndex = 17
        Me.CheckedListBox_Airplane_Setup.Visible = False
        '
        'LabelSure
        '
        Me.LabelSure.AutoSize = True
        Me.LabelSure.Location = New System.Drawing.Point(121, 446)
        Me.LabelSure.Name = "LabelSure"
        Me.LabelSure.Size = New System.Drawing.Size(72, 13)
        Me.LabelSure.TabIndex = 18
        Me.LabelSure.Text = "Are you sure?"
        Me.LabelSure.Visible = False
        '
        'Button_Yes
        '
        Me.Button_Yes.Location = New System.Drawing.Point(199, 441)
        Me.Button_Yes.Name = "Button_Yes"
        Me.Button_Yes.Size = New System.Drawing.Size(75, 23)
        Me.Button_Yes.TabIndex = 19
        Me.Button_Yes.Text = "Yes"
        Me.Button_Yes.UseVisualStyleBackColor = True
        Me.Button_Yes.Visible = False
        '
        'Button_No
        '
        Me.Button_No.Location = New System.Drawing.Point(280, 441)
        Me.Button_No.Name = "Button_No"
        Me.Button_No.Size = New System.Drawing.Size(75, 23)
        Me.Button_No.TabIndex = 20
        Me.Button_No.Text = "No"
        Me.Button_No.UseVisualStyleBackColor = True
        Me.Button_No.Visible = False
        '
        'Label_PID
        '
        Me.Label_PID.AutoSize = True
        Me.Label_PID.Location = New System.Drawing.Point(230, 27)
        Me.Label_PID.Name = "Label_PID"
        Me.Label_PID.Size = New System.Drawing.Size(103, 13)
        Me.Label_PID.TabIndex = 21
        Me.Label_PID.Text = "PID Controller Setup"
        Me.Label_PID.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(230, 55)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(93, 13)
        Me.Label10.TabIndex = 22
        Me.Label10.Text = "Pitch: Proportional"
        Me.Label10.Visible = False
        '
        'Pitch_P
        '
        Me.Pitch_P.Location = New System.Drawing.Point(329, 52)
        Me.Pitch_P.Name = "Pitch_P"
        Me.Pitch_P.Size = New System.Drawing.Size(100, 20)
        Me.Pitch_P.TabIndex = 23
        Me.Pitch_P.Visible = False
        '
        'Pitch_I
        '
        Me.Pitch_I.Location = New System.Drawing.Point(329, 78)
        Me.Pitch_I.Name = "Pitch_I"
        Me.Pitch_I.Size = New System.Drawing.Size(100, 20)
        Me.Pitch_I.TabIndex = 25
        Me.Pitch_I.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(230, 81)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 13)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "Pitch: Integral"
        Me.Label1.Visible = False
        '
        'Pitch_D
        '
        Me.Pitch_D.Location = New System.Drawing.Point(329, 104)
        Me.Pitch_D.Name = "Pitch_D"
        Me.Pitch_D.Size = New System.Drawing.Size(100, 20)
        Me.Pitch_D.TabIndex = 27
        Me.Pitch_D.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(230, 107)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(85, 13)
        Me.Label2.TabIndex = 26
        Me.Label2.Text = "Pitch: Derivative"
        Me.Label2.Visible = False
        '
        'Roll_D
        '
        Me.Roll_D.Location = New System.Drawing.Point(329, 184)
        Me.Roll_D.Name = "Roll_D"
        Me.Roll_D.Size = New System.Drawing.Size(100, 20)
        Me.Roll_D.TabIndex = 33
        Me.Roll_D.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(230, 187)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(79, 13)
        Me.Label3.TabIndex = 32
        Me.Label3.Text = "Roll: Derivative"
        Me.Label3.Visible = False
        '
        'Roll_I
        '
        Me.Roll_I.Location = New System.Drawing.Point(329, 158)
        Me.Roll_I.Name = "Roll_I"
        Me.Roll_I.Size = New System.Drawing.Size(100, 20)
        Me.Roll_I.TabIndex = 31
        Me.Roll_I.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(230, 161)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 13)
        Me.Label4.TabIndex = 30
        Me.Label4.Text = "Roll: Integral"
        Me.Label4.Visible = False
        '
        'Roll_P
        '
        Me.Roll_P.Location = New System.Drawing.Point(329, 132)
        Me.Roll_P.Name = "Roll_P"
        Me.Roll_P.Size = New System.Drawing.Size(100, 20)
        Me.Roll_P.TabIndex = 29
        Me.Roll_P.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(230, 135)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(87, 13)
        Me.Label5.TabIndex = 28
        Me.Label5.Text = "Roll: Proportional"
        Me.Label5.Visible = False
        '
        'Yaw_D
        '
        Me.Yaw_D.Location = New System.Drawing.Point(329, 264)
        Me.Yaw_D.Name = "Yaw_D"
        Me.Yaw_D.Size = New System.Drawing.Size(100, 20)
        Me.Yaw_D.TabIndex = 39
        Me.Yaw_D.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(230, 267)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(82, 13)
        Me.Label6.TabIndex = 38
        Me.Label6.Text = "Yaw: Derivative"
        Me.Label6.Visible = False
        '
        'Yaw_I
        '
        Me.Yaw_I.Location = New System.Drawing.Point(329, 238)
        Me.Yaw_I.Name = "Yaw_I"
        Me.Yaw_I.Size = New System.Drawing.Size(100, 20)
        Me.Yaw_I.TabIndex = 37
        Me.Yaw_I.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(230, 241)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(69, 13)
        Me.Label7.TabIndex = 36
        Me.Label7.Text = "Yaw: Integral"
        Me.Label7.Visible = False
        '
        'Yaw_P
        '
        Me.Yaw_P.Location = New System.Drawing.Point(329, 212)
        Me.Yaw_P.Name = "Yaw_P"
        Me.Yaw_P.Size = New System.Drawing.Size(100, 20)
        Me.Yaw_P.TabIndex = 35
        Me.Yaw_P.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(230, 215)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(90, 13)
        Me.Label8.TabIndex = 34
        Me.Label8.Text = "Yaw: Proportional"
        Me.Label8.Visible = False
        '
        'Freq_Hz
        '
        Me.Freq_Hz.Location = New System.Drawing.Point(329, 290)
        Me.Freq_Hz.Name = "Freq_Hz"
        Me.Freq_Hz.Size = New System.Drawing.Size(100, 20)
        Me.Freq_Hz.TabIndex = 41
        Me.Freq_Hz.Visible = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(230, 293)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(79, 13)
        Me.Label9.TabIndex = 40
        Me.Label9.Text = "Frequency (Hz)"
        Me.Label9.Visible = False
        '
        'Button_Map_Style
        '
        Me.Button_Map_Style.Location = New System.Drawing.Point(88, 543)
        Me.Button_Map_Style.Name = "Button_Map_Style"
        Me.Button_Map_Style.Size = New System.Drawing.Size(161, 23)
        Me.Button_Map_Style.TabIndex = 42
        Me.Button_Map_Style.Text = "Deg Absolute"
        Me.Button_Map_Style.UseVisualStyleBackColor = True
        Me.Button_Map_Style.Visible = False
        '
        'Button_Zoom_Plus
        '
        Me.Button_Zoom_Plus.Location = New System.Drawing.Point(106, 591)
        Me.Button_Zoom_Plus.Name = "Button_Zoom_Plus"
        Me.Button_Zoom_Plus.Size = New System.Drawing.Size(23, 23)
        Me.Button_Zoom_Plus.TabIndex = 43
        Me.Button_Zoom_Plus.Text = "+"
        Me.Button_Zoom_Plus.UseVisualStyleBackColor = True
        Me.Button_Zoom_Plus.Visible = False
        '
        'Button_Zoom_Minus
        '
        Me.Button_Zoom_Minus.Location = New System.Drawing.Point(124, 591)
        Me.Button_Zoom_Minus.Name = "Button_Zoom_Minus"
        Me.Button_Zoom_Minus.Size = New System.Drawing.Size(23, 23)
        Me.Button_Zoom_Minus.TabIndex = 44
        Me.Button_Zoom_Minus.Text = "-"
        Me.Button_Zoom_Minus.UseVisualStyleBackColor = True
        Me.Button_Zoom_Minus.Visible = False
        '
        'Label_Zoom
        '
        Me.Label_Zoom.AutoSize = True
        Me.Label_Zoom.Location = New System.Drawing.Point(63, 596)
        Me.Label_Zoom.Name = "Label_Zoom"
        Me.Label_Zoom.Size = New System.Drawing.Size(37, 13)
        Me.Label_Zoom.TabIndex = 45
        Me.Label_Zoom.Text = "Zoom "
        Me.Label_Zoom.Visible = False
        '
        'Label_Map_Style
        '
        Me.Label_Map_Style.AutoSize = True
        Me.Label_Map_Style.Location = New System.Drawing.Point(35, 552)
        Me.Label_Map_Style.Name = "Label_Map_Style"
        Me.Label_Map_Style.Size = New System.Drawing.Size(54, 13)
        Me.Label_Map_Style.TabIndex = 46
        Me.Label_Map_Style.Text = "Map Style"
        Me.Label_Map_Style.Visible = False
        '
        'Label_Alt_Lim
        '
        Me.Label_Alt_Lim.AutoSize = True
        Me.Label_Alt_Lim.Location = New System.Drawing.Point(40, 630)
        Me.Label_Alt_Lim.Name = "Label_Alt_Lim"
        Me.Label_Alt_Lim.Size = New System.Drawing.Size(60, 13)
        Me.Label_Alt_Lim.TabIndex = 49
        Me.Label_Alt_Lim.Text = "Altitude limt"
        Me.Label_Alt_Lim.Visible = False
        '
        'Button_Alt_Minus
        '
        Me.Button_Alt_Minus.Location = New System.Drawing.Point(124, 625)
        Me.Button_Alt_Minus.Name = "Button_Alt_Minus"
        Me.Button_Alt_Minus.Size = New System.Drawing.Size(23, 23)
        Me.Button_Alt_Minus.TabIndex = 48
        Me.Button_Alt_Minus.Text = "-"
        Me.Button_Alt_Minus.UseVisualStyleBackColor = True
        Me.Button_Alt_Minus.Visible = False
        '
        'Button_Alt_Plus
        '
        Me.Button_Alt_Plus.Location = New System.Drawing.Point(106, 625)
        Me.Button_Alt_Plus.Name = "Button_Alt_Plus"
        Me.Button_Alt_Plus.Size = New System.Drawing.Size(23, 23)
        Me.Button_Alt_Plus.TabIndex = 47
        Me.Button_Alt_Plus.Text = "+"
        Me.Button_Alt_Plus.UseVisualStyleBackColor = True
        Me.Button_Alt_Plus.Visible = False
        '
        'Button_Refresh
        '
        Me.Button_Refresh.Location = New System.Drawing.Point(72, 312)
        Me.Button_Refresh.Name = "Button_Refresh"
        Me.Button_Refresh.Size = New System.Drawing.Size(57, 23)
        Me.Button_Refresh.TabIndex = 50
        Me.Button_Refresh.Text = "Refresh"
        Me.Button_Refresh.UseVisualStyleBackColor = True
        Me.Button_Refresh.Visible = False
        '
        'Label_Altitude_Style
        '
        Me.Label_Altitude_Style.AutoSize = True
        Me.Label_Altitude_Style.Location = New System.Drawing.Point(35, 686)
        Me.Label_Altitude_Style.Name = "Label_Altitude_Style"
        Me.Label_Altitude_Style.Size = New System.Drawing.Size(68, 13)
        Me.Label_Altitude_Style.TabIndex = 52
        Me.Label_Altitude_Style.Text = "Altitude Style"
        Me.Label_Altitude_Style.Visible = False
        '
        'Button_Altitude_Style
        '
        Me.Button_Altitude_Style.Location = New System.Drawing.Point(109, 681)
        Me.Button_Altitude_Style.Name = "Button_Altitude_Style"
        Me.Button_Altitude_Style.Size = New System.Drawing.Size(161, 23)
        Me.Button_Altitude_Style.TabIndex = 51
        Me.Button_Altitude_Style.Text = "From starting point"
        Me.Button_Altitude_Style.UseVisualStyleBackColor = True
        Me.Button_Altitude_Style.Visible = False
        '
        'Label_Sarting_Altitude
        '
        Me.Label_Sarting_Altitude.AutoSize = True
        Me.Label_Sarting_Altitude.Location = New System.Drawing.Point(15, 164)
        Me.Label_Sarting_Altitude.Name = "Label_Sarting_Altitude"
        Me.Label_Sarting_Altitude.Size = New System.Drawing.Size(83, 13)
        Me.Label_Sarting_Altitude.TabIndex = 53
        Me.Label_Sarting_Altitude.Text = "Starting altitude:"
        Me.Label_Sarting_Altitude.Visible = False
        '
        'TextBox_Starting_Altitude
        '
        Me.TextBox_Starting_Altitude.Location = New System.Drawing.Point(104, 161)
        Me.TextBox_Starting_Altitude.Name = "TextBox_Starting_Altitude"
        Me.TextBox_Starting_Altitude.Size = New System.Drawing.Size(107, 20)
        Me.TextBox_Starting_Altitude.TabIndex = 54
        Me.TextBox_Starting_Altitude.Text = "0"
        Me.TextBox_Starting_Altitude.Visible = False
        '
        'NewFlight
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(1184, 961)
        Me.Controls.Add(Me.TextBox_Starting_Altitude)
        Me.Controls.Add(Me.Label_Sarting_Altitude)
        Me.Controls.Add(Me.Label_Altitude_Style)
        Me.Controls.Add(Me.Button_Altitude_Style)
        Me.Controls.Add(Me.Button_Refresh)
        Me.Controls.Add(Me.Label_Alt_Lim)
        Me.Controls.Add(Me.Button_Alt_Minus)
        Me.Controls.Add(Me.Button_Alt_Plus)
        Me.Controls.Add(Me.Label_Map_Style)
        Me.Controls.Add(Me.Label_Zoom)
        Me.Controls.Add(Me.Button_Zoom_Minus)
        Me.Controls.Add(Me.Button_Zoom_Plus)
        Me.Controls.Add(Me.Button_Map_Style)
        Me.Controls.Add(Me.Freq_Hz)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Yaw_D)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Yaw_I)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Yaw_P)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Roll_D)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Roll_I)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Roll_P)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Pitch_D)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Pitch_I)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Pitch_P)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label_PID)
        Me.Controls.Add(Me.Button_No)
        Me.Controls.Add(Me.Button_Yes)
        Me.Controls.Add(Me.LabelSure)
        Me.Controls.Add(Me.CheckedListBox_Airplane_Setup)
        Me.Controls.Add(Me.Label_Airplane_Setup)
        Me.Controls.Add(Me.ListView_Waypoints)
        Me.Controls.Add(Me.Label_Invalid_File)
        Me.Controls.Add(Me.CheckBox_Record_Flight)
        Me.Controls.Add(Me.Button_Load_FP)
        Me.Controls.Add(Me.Button_Upload_Setup)
        Me.Controls.Add(Me.ButtonOk)
        Me.Controls.Add(Me.ListBoxSerialPorts)
        Me.Controls.Add(Me.LabelSelectSerialPort)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Controls.Add(Me.PictureBoxInstruments)
        Me.Name = "NewFlight"
        Me.Text = "New Flight"
        Me.MenuStrip2.ResumeLayout(False)
        Me.MenuStrip2.PerformLayout()
        CType(Me.PictureBoxInstruments, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LabelSelectSerialPort As Label
    Friend WithEvents ListBoxSerialPorts As ListBox
    Friend WithEvents ButtonOk As Button
    Friend WithEvents MenuStrip2 As MenuStrip
    Friend WithEvents ToolStripMenu_Menu As ToolStripMenuItem
    Friend WithEvents ToolStripMenu_Show As ToolStripMenuItem
    Friend WithEvents PanelToolStripMenu_Setup As ToolStripMenuItem
    Friend WithEvents PanelToolStripMenuItem_Instruments As ToolStripMenuItem
    Friend WithEvents StartFlightToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ResetToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Button_Upload_Setup As Button
    Friend WithEvents Button_Load_FP As Button
    Friend WithEvents CheckBox_Record_Flight As CheckBox
    Friend WithEvents Label_Invalid_File As Label
    Friend WithEvents ListView_Waypoints As ListView
    Friend WithEvents ColumnHeaderWaypoints As ColumnHeader
    Friend WithEvents PictureBoxInstruments As PictureBox
    Friend WithEvents Label_Airplane_Setup As Label
    Friend WithEvents CheckedListBox_Airplane_Setup As CheckedListBox
    Friend WithEvents LabelSure As Label
    Friend WithEvents Button_Yes As Button
    Friend WithEvents Button_No As Button
    Friend WithEvents Label_PID As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Pitch_P As TextBox
    Friend WithEvents Pitch_I As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Pitch_D As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Roll_D As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Roll_I As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Roll_P As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Yaw_D As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Yaw_I As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Yaw_P As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Freq_Hz As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents CommunicationWindowToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Button_Map_Style As Button
    Friend WithEvents Button_Zoom_Plus As Button
    Friend WithEvents Button_Zoom_Minus As Button
    Friend WithEvents Label_Zoom As Label
    Friend WithEvents Label_Map_Style As Label
    Friend WithEvents Label_Alt_Lim As Label
    Friend WithEvents Button_Alt_Minus As Button
    Friend WithEvents Button_Alt_Plus As Button
    Friend WithEvents ReadTelemetryToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Button_Refresh As Button
    Friend WithEvents Label_Altitude_Style As Label
    Friend WithEvents Button_Altitude_Style As Button
    Friend WithEvents Label_Sarting_Altitude As Label
    Friend WithEvents TextBox_Starting_Altitude As TextBox
End Class
