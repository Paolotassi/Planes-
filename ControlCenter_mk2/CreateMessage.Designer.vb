<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CreateMessage
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ButtonSend = New System.Windows.Forms.Button()
        Me.CheckBox_PP = New System.Windows.Forms.CheckBox()
        Me.TextBox_PP = New System.Windows.Forms.TextBox()
        Me.TextBox_PI = New System.Windows.Forms.TextBox()
        Me.CheckBox_PI = New System.Windows.Forms.CheckBox()
        Me.TextBox_PD = New System.Windows.Forms.TextBox()
        Me.CheckBox_PD = New System.Windows.Forms.CheckBox()
        Me.TextBox_RP = New System.Windows.Forms.TextBox()
        Me.CheckBox_RP = New System.Windows.Forms.CheckBox()
        Me.TextBox_RI = New System.Windows.Forms.TextBox()
        Me.CheckBox_RI = New System.Windows.Forms.CheckBox()
        Me.TextBox_RD = New System.Windows.Forms.TextBox()
        Me.CheckBox_RD = New System.Windows.Forms.CheckBox()
        Me.TextBox_YP = New System.Windows.Forms.TextBox()
        Me.CheckBox_YP = New System.Windows.Forms.CheckBox()
        Me.TextBox_YI = New System.Windows.Forms.TextBox()
        Me.CheckBox_YI = New System.Windows.Forms.CheckBox()
        Me.TextBox_YD = New System.Windows.Forms.TextBox()
        Me.CheckBox_YD = New System.Windows.Forms.CheckBox()
        Me.TextBox_F = New System.Windows.Forms.TextBox()
        Me.CheckBox_F = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button_Open_FPE = New System.Windows.Forms.Button()
        Me.Button_FP_Load = New System.Windows.Forms.Button()
        Me.CheckBox_Insert = New System.Windows.Forms.CheckBox()
        Me.TextBox_Ins_After = New System.Windows.Forms.TextBox()
        Me.CheckBox_Replace = New System.Windows.Forms.CheckBox()
        Me.TextBox_Replace_From = New System.Windows.Forms.TextBox()
        Me.TextBox_Replace_To = New System.Windows.Forms.TextBox()
        Me.Label_R_To = New System.Windows.Forms.Label()
        Me.Label_D_To = New System.Windows.Forms.Label()
        Me.TextBox_Delete_To = New System.Windows.Forms.TextBox()
        Me.TextBox_Delete_From = New System.Windows.Forms.TextBox()
        Me.CheckBox_Delete = New System.Windows.Forms.CheckBox()
        Me.Label_R_From = New System.Windows.Forms.Label()
        Me.Label_D_From = New System.Windows.Forms.Label()
        Me.Label_IF = New System.Windows.Forms.Label()
        Me.ListView_WP = New System.Windows.Forms.ListView()
        Me.Waypoints = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LabelSure = New System.Windows.Forms.Label()
        Me.ButtonYes = New System.Windows.Forms.Button()
        Me.ButtonNo = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ButtonSend
        '
        Me.ButtonSend.Location = New System.Drawing.Point(17, 397)
        Me.ButtonSend.Name = "ButtonSend"
        Me.ButtonSend.Size = New System.Drawing.Size(338, 23)
        Me.ButtonSend.TabIndex = 0
        Me.ButtonSend.Text = "Send"
        Me.ButtonSend.UseVisualStyleBackColor = True
        '
        'CheckBox_PP
        '
        Me.CheckBox_PP.AutoSize = True
        Me.CheckBox_PP.Location = New System.Drawing.Point(17, 62)
        Me.CheckBox_PP.Name = "CheckBox_PP"
        Me.CheckBox_PP.Size = New System.Drawing.Size(134, 17)
        Me.CheckBox_PP.TabIndex = 1
        Me.CheckBox_PP.Text = "New Pitch Proportional"
        Me.CheckBox_PP.UseVisualStyleBackColor = True
        '
        'TextBox_PP
        '
        Me.TextBox_PP.Enabled = False
        Me.TextBox_PP.Location = New System.Drawing.Point(176, 60)
        Me.TextBox_PP.Name = "TextBox_PP"
        Me.TextBox_PP.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_PP.TabIndex = 2
        '
        'TextBox_PI
        '
        Me.TextBox_PI.Enabled = False
        Me.TextBox_PI.Location = New System.Drawing.Point(176, 86)
        Me.TextBox_PI.Name = "TextBox_PI"
        Me.TextBox_PI.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_PI.TabIndex = 4
        '
        'CheckBox_PI
        '
        Me.CheckBox_PI.AutoSize = True
        Me.CheckBox_PI.Location = New System.Drawing.Point(17, 88)
        Me.CheckBox_PI.Name = "CheckBox_PI"
        Me.CheckBox_PI.Size = New System.Drawing.Size(113, 17)
        Me.CheckBox_PI.TabIndex = 3
        Me.CheckBox_PI.Text = "New Pitch Integral"
        Me.CheckBox_PI.UseVisualStyleBackColor = True
        '
        'TextBox_PD
        '
        Me.TextBox_PD.Enabled = False
        Me.TextBox_PD.Location = New System.Drawing.Point(176, 112)
        Me.TextBox_PD.Name = "TextBox_PD"
        Me.TextBox_PD.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_PD.TabIndex = 8
        '
        'CheckBox_PD
        '
        Me.CheckBox_PD.AutoSize = True
        Me.CheckBox_PD.Location = New System.Drawing.Point(17, 114)
        Me.CheckBox_PD.Name = "CheckBox_PD"
        Me.CheckBox_PD.Size = New System.Drawing.Size(126, 17)
        Me.CheckBox_PD.TabIndex = 7
        Me.CheckBox_PD.Text = "New Pitch Derivative"
        Me.CheckBox_PD.UseVisualStyleBackColor = True
        '
        'TextBox_RP
        '
        Me.TextBox_RP.Enabled = False
        Me.TextBox_RP.Location = New System.Drawing.Point(176, 138)
        Me.TextBox_RP.Name = "TextBox_RP"
        Me.TextBox_RP.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_RP.TabIndex = 6
        '
        'CheckBox_RP
        '
        Me.CheckBox_RP.AutoSize = True
        Me.CheckBox_RP.Location = New System.Drawing.Point(17, 140)
        Me.CheckBox_RP.Name = "CheckBox_RP"
        Me.CheckBox_RP.Size = New System.Drawing.Size(128, 17)
        Me.CheckBox_RP.TabIndex = 5
        Me.CheckBox_RP.Text = "New Roll Proportional"
        Me.CheckBox_RP.UseVisualStyleBackColor = True
        '
        'TextBox_RI
        '
        Me.TextBox_RI.Enabled = False
        Me.TextBox_RI.Location = New System.Drawing.Point(176, 164)
        Me.TextBox_RI.Name = "TextBox_RI"
        Me.TextBox_RI.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_RI.TabIndex = 10
        '
        'CheckBox_RI
        '
        Me.CheckBox_RI.AutoSize = True
        Me.CheckBox_RI.Location = New System.Drawing.Point(17, 166)
        Me.CheckBox_RI.Name = "CheckBox_RI"
        Me.CheckBox_RI.Size = New System.Drawing.Size(107, 17)
        Me.CheckBox_RI.TabIndex = 9
        Me.CheckBox_RI.Text = "New Roll Integral"
        Me.CheckBox_RI.UseVisualStyleBackColor = True
        '
        'TextBox_RD
        '
        Me.TextBox_RD.Enabled = False
        Me.TextBox_RD.Location = New System.Drawing.Point(176, 190)
        Me.TextBox_RD.Name = "TextBox_RD"
        Me.TextBox_RD.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_RD.TabIndex = 20
        '
        'CheckBox_RD
        '
        Me.CheckBox_RD.AutoSize = True
        Me.CheckBox_RD.Location = New System.Drawing.Point(17, 192)
        Me.CheckBox_RD.Name = "CheckBox_RD"
        Me.CheckBox_RD.Size = New System.Drawing.Size(120, 17)
        Me.CheckBox_RD.TabIndex = 19
        Me.CheckBox_RD.Text = "New Roll Derivative"
        Me.CheckBox_RD.UseVisualStyleBackColor = True
        '
        'TextBox_YP
        '
        Me.TextBox_YP.Enabled = False
        Me.TextBox_YP.Location = New System.Drawing.Point(176, 216)
        Me.TextBox_YP.Name = "TextBox_YP"
        Me.TextBox_YP.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_YP.TabIndex = 18
        '
        'CheckBox_YP
        '
        Me.CheckBox_YP.AutoSize = True
        Me.CheckBox_YP.Location = New System.Drawing.Point(17, 218)
        Me.CheckBox_YP.Name = "CheckBox_YP"
        Me.CheckBox_YP.Size = New System.Drawing.Size(131, 17)
        Me.CheckBox_YP.TabIndex = 17
        Me.CheckBox_YP.Text = "New Yaw Proportional"
        Me.CheckBox_YP.UseVisualStyleBackColor = True
        '
        'TextBox_YI
        '
        Me.TextBox_YI.Enabled = False
        Me.TextBox_YI.Location = New System.Drawing.Point(176, 242)
        Me.TextBox_YI.Name = "TextBox_YI"
        Me.TextBox_YI.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_YI.TabIndex = 16
        '
        'CheckBox_YI
        '
        Me.CheckBox_YI.AutoSize = True
        Me.CheckBox_YI.Location = New System.Drawing.Point(17, 244)
        Me.CheckBox_YI.Name = "CheckBox_YI"
        Me.CheckBox_YI.Size = New System.Drawing.Size(110, 17)
        Me.CheckBox_YI.TabIndex = 15
        Me.CheckBox_YI.Text = "New Yaw Integral"
        Me.CheckBox_YI.UseVisualStyleBackColor = True
        '
        'TextBox_YD
        '
        Me.TextBox_YD.Enabled = False
        Me.TextBox_YD.Location = New System.Drawing.Point(176, 268)
        Me.TextBox_YD.Name = "TextBox_YD"
        Me.TextBox_YD.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_YD.TabIndex = 14
        '
        'CheckBox_YD
        '
        Me.CheckBox_YD.AutoSize = True
        Me.CheckBox_YD.Location = New System.Drawing.Point(17, 270)
        Me.CheckBox_YD.Name = "CheckBox_YD"
        Me.CheckBox_YD.Size = New System.Drawing.Size(123, 17)
        Me.CheckBox_YD.TabIndex = 13
        Me.CheckBox_YD.Text = "New Yaw Derivative"
        Me.CheckBox_YD.UseVisualStyleBackColor = True
        '
        'TextBox_F
        '
        Me.TextBox_F.Enabled = False
        Me.TextBox_F.Location = New System.Drawing.Point(176, 294)
        Me.TextBox_F.Name = "TextBox_F"
        Me.TextBox_F.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_F.TabIndex = 12
        '
        'CheckBox_F
        '
        Me.CheckBox_F.AutoSize = True
        Me.CheckBox_F.Location = New System.Drawing.Point(17, 296)
        Me.CheckBox_F.Name = "CheckBox_F"
        Me.CheckBox_F.Size = New System.Drawing.Size(101, 17)
        Me.CheckBox_F.TabIndex = 11
        Me.CheckBox_F.Text = "New Frequency"
        Me.CheckBox_F.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(114, 13)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "Change PID constants"
        '
        'Button_Open_FPE
        '
        Me.Button_Open_FPE.Location = New System.Drawing.Point(388, 56)
        Me.Button_Open_FPE.Name = "Button_Open_FPE"
        Me.Button_Open_FPE.Size = New System.Drawing.Size(153, 23)
        Me.Button_Open_FPE.TabIndex = 23
        Me.Button_Open_FPE.Text = "Open Flight Plan Editor"
        Me.Button_Open_FPE.UseVisualStyleBackColor = True
        '
        'Button_FP_Load
        '
        Me.Button_FP_Load.Location = New System.Drawing.Point(388, 88)
        Me.Button_FP_Load.Name = "Button_FP_Load"
        Me.Button_FP_Load.Size = New System.Drawing.Size(153, 23)
        Me.Button_FP_Load.TabIndex = 24
        Me.Button_FP_Load.Text = "Load Flight plan"
        Me.Button_FP_Load.UseVisualStyleBackColor = True
        '
        'CheckBox_Insert
        '
        Me.CheckBox_Insert.AutoSize = True
        Me.CheckBox_Insert.Location = New System.Drawing.Point(388, 297)
        Me.CheckBox_Insert.Name = "CheckBox_Insert"
        Me.CheckBox_Insert.Size = New System.Drawing.Size(165, 17)
        Me.CheckBox_Insert.TabIndex = 25
        Me.CheckBox_Insert.Text = "Insert after instruction number"
        Me.CheckBox_Insert.UseVisualStyleBackColor = True
        '
        'TextBox_Ins_After
        '
        Me.TextBox_Ins_After.Enabled = False
        Me.TextBox_Ins_After.Location = New System.Drawing.Point(559, 295)
        Me.TextBox_Ins_After.Name = "TextBox_Ins_After"
        Me.TextBox_Ins_After.Size = New System.Drawing.Size(68, 20)
        Me.TextBox_Ins_After.TabIndex = 26
        '
        'CheckBox_Replace
        '
        Me.CheckBox_Replace.AutoSize = True
        Me.CheckBox_Replace.Location = New System.Drawing.Point(388, 323)
        Me.CheckBox_Replace.Name = "CheckBox_Replace"
        Me.CheckBox_Replace.Size = New System.Drawing.Size(123, 17)
        Me.CheckBox_Replace.TabIndex = 27
        Me.CheckBox_Replace.Text = "Replace Instructions"
        Me.CheckBox_Replace.UseVisualStyleBackColor = True
        '
        'TextBox_Replace_From
        '
        Me.TextBox_Replace_From.Enabled = False
        Me.TextBox_Replace_From.Location = New System.Drawing.Point(589, 321)
        Me.TextBox_Replace_From.Name = "TextBox_Replace_From"
        Me.TextBox_Replace_From.Size = New System.Drawing.Size(68, 20)
        Me.TextBox_Replace_From.TabIndex = 28
        '
        'TextBox_Replace_To
        '
        Me.TextBox_Replace_To.Enabled = False
        Me.TextBox_Replace_To.Location = New System.Drawing.Point(685, 320)
        Me.TextBox_Replace_To.Name = "TextBox_Replace_To"
        Me.TextBox_Replace_To.Size = New System.Drawing.Size(68, 20)
        Me.TextBox_Replace_To.TabIndex = 29
        '
        'Label_R_To
        '
        Me.Label_R_To.AutoSize = True
        Me.Label_R_To.Enabled = False
        Me.Label_R_To.Location = New System.Drawing.Point(663, 324)
        Me.Label_R_To.Name = "Label_R_To"
        Me.Label_R_To.Size = New System.Drawing.Size(16, 13)
        Me.Label_R_To.TabIndex = 30
        Me.Label_R_To.Text = "to"
        '
        'Label_D_To
        '
        Me.Label_D_To.AutoSize = True
        Me.Label_D_To.Enabled = False
        Me.Label_D_To.Location = New System.Drawing.Point(663, 350)
        Me.Label_D_To.Name = "Label_D_To"
        Me.Label_D_To.Size = New System.Drawing.Size(16, 13)
        Me.Label_D_To.TabIndex = 34
        Me.Label_D_To.Text = "to"
        '
        'TextBox_Delete_To
        '
        Me.TextBox_Delete_To.Enabled = False
        Me.TextBox_Delete_To.Location = New System.Drawing.Point(685, 346)
        Me.TextBox_Delete_To.Name = "TextBox_Delete_To"
        Me.TextBox_Delete_To.Size = New System.Drawing.Size(68, 20)
        Me.TextBox_Delete_To.TabIndex = 33
        '
        'TextBox_Delete_From
        '
        Me.TextBox_Delete_From.Enabled = False
        Me.TextBox_Delete_From.Location = New System.Drawing.Point(589, 347)
        Me.TextBox_Delete_From.Name = "TextBox_Delete_From"
        Me.TextBox_Delete_From.Size = New System.Drawing.Size(68, 20)
        Me.TextBox_Delete_From.TabIndex = 32
        '
        'CheckBox_Delete
        '
        Me.CheckBox_Delete.AutoSize = True
        Me.CheckBox_Delete.Location = New System.Drawing.Point(388, 348)
        Me.CheckBox_Delete.Name = "CheckBox_Delete"
        Me.CheckBox_Delete.Size = New System.Drawing.Size(114, 17)
        Me.CheckBox_Delete.TabIndex = 31
        Me.CheckBox_Delete.Text = "Delete Instructions"
        Me.CheckBox_Delete.UseVisualStyleBackColor = True
        '
        'Label_R_From
        '
        Me.Label_R_From.AutoSize = True
        Me.Label_R_From.Enabled = False
        Me.Label_R_From.Location = New System.Drawing.Point(556, 324)
        Me.Label_R_From.Name = "Label_R_From"
        Me.Label_R_From.Size = New System.Drawing.Size(27, 13)
        Me.Label_R_From.TabIndex = 35
        Me.Label_R_From.Text = "from"
        '
        'Label_D_From
        '
        Me.Label_D_From.AutoSize = True
        Me.Label_D_From.Enabled = False
        Me.Label_D_From.Location = New System.Drawing.Point(556, 350)
        Me.Label_D_From.Name = "Label_D_From"
        Me.Label_D_From.Size = New System.Drawing.Size(27, 13)
        Me.Label_D_From.TabIndex = 36
        Me.Label_D_From.Text = "from"
        '
        'Label_IF
        '
        Me.Label_IF.AutoSize = True
        Me.Label_IF.Location = New System.Drawing.Point(547, 93)
        Me.Label_IF.Name = "Label_IF"
        Me.Label_IF.Size = New System.Drawing.Size(82, 13)
        Me.Label_IF.TabIndex = 37
        Me.Label_IF.Text = "Error: Invalid file"
        Me.Label_IF.Visible = False
        '
        'ListView_WP
        '
        Me.ListView_WP.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Waypoints})
        Me.ListView_WP.GridLines = True
        Me.ListView_WP.HideSelection = False
        Me.ListView_WP.Location = New System.Drawing.Point(388, 124)
        Me.ListView_WP.Name = "ListView_WP"
        Me.ListView_WP.Size = New System.Drawing.Size(219, 163)
        Me.ListView_WP.TabIndex = 38
        Me.ListView_WP.UseCompatibleStateImageBehavior = False
        Me.ListView_WP.View = System.Windows.Forms.View.Details
        '
        'Waypoints
        '
        Me.Waypoints.Text = "Waypoints"
        Me.Waypoints.Width = 214
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(385, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(131, 13)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "Modify/Change Flight plan"
        '
        'LabelSure
        '
        Me.LabelSure.AutoSize = True
        Me.LabelSure.Location = New System.Drawing.Point(34, 437)
        Me.LabelSure.Name = "LabelSure"
        Me.LabelSure.Size = New System.Drawing.Size(72, 13)
        Me.LabelSure.TabIndex = 39
        Me.LabelSure.Text = "Are you sure?"
        Me.LabelSure.Visible = False
        '
        'ButtonYes
        '
        Me.ButtonYes.Location = New System.Drawing.Point(139, 432)
        Me.ButtonYes.Name = "ButtonYes"
        Me.ButtonYes.Size = New System.Drawing.Size(93, 23)
        Me.ButtonYes.TabIndex = 40
        Me.ButtonYes.Text = "Yes"
        Me.ButtonYes.UseVisualStyleBackColor = True
        Me.ButtonYes.Visible = False
        '
        'ButtonNo
        '
        Me.ButtonNo.Location = New System.Drawing.Point(248, 432)
        Me.ButtonNo.Name = "ButtonNo"
        Me.ButtonNo.Size = New System.Drawing.Size(93, 23)
        Me.ButtonNo.TabIndex = 41
        Me.ButtonNo.Text = "No"
        Me.ButtonNo.UseVisualStyleBackColor = True
        Me.ButtonNo.Visible = False
        '
        'CreateMessage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(862, 583)
        Me.Controls.Add(Me.ButtonNo)
        Me.Controls.Add(Me.ButtonYes)
        Me.Controls.Add(Me.LabelSure)
        Me.Controls.Add(Me.ListView_WP)
        Me.Controls.Add(Me.Label_IF)
        Me.Controls.Add(Me.Label_D_From)
        Me.Controls.Add(Me.Label_R_From)
        Me.Controls.Add(Me.Label_D_To)
        Me.Controls.Add(Me.TextBox_Delete_To)
        Me.Controls.Add(Me.TextBox_Delete_From)
        Me.Controls.Add(Me.CheckBox_Delete)
        Me.Controls.Add(Me.Label_R_To)
        Me.Controls.Add(Me.TextBox_Replace_To)
        Me.Controls.Add(Me.TextBox_Replace_From)
        Me.Controls.Add(Me.CheckBox_Replace)
        Me.Controls.Add(Me.TextBox_Ins_After)
        Me.Controls.Add(Me.CheckBox_Insert)
        Me.Controls.Add(Me.Button_FP_Load)
        Me.Controls.Add(Me.Button_Open_FPE)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox_RD)
        Me.Controls.Add(Me.CheckBox_RD)
        Me.Controls.Add(Me.TextBox_YP)
        Me.Controls.Add(Me.CheckBox_YP)
        Me.Controls.Add(Me.TextBox_YI)
        Me.Controls.Add(Me.CheckBox_YI)
        Me.Controls.Add(Me.TextBox_YD)
        Me.Controls.Add(Me.CheckBox_YD)
        Me.Controls.Add(Me.TextBox_F)
        Me.Controls.Add(Me.CheckBox_F)
        Me.Controls.Add(Me.TextBox_RI)
        Me.Controls.Add(Me.CheckBox_RI)
        Me.Controls.Add(Me.TextBox_PD)
        Me.Controls.Add(Me.CheckBox_PD)
        Me.Controls.Add(Me.TextBox_RP)
        Me.Controls.Add(Me.CheckBox_RP)
        Me.Controls.Add(Me.TextBox_PI)
        Me.Controls.Add(Me.CheckBox_PI)
        Me.Controls.Add(Me.TextBox_PP)
        Me.Controls.Add(Me.CheckBox_PP)
        Me.Controls.Add(Me.ButtonSend)
        Me.Name = "CreateMessage"
        Me.Text = "Sending commands"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ButtonSend As Button
    Friend WithEvents CheckBox_PP As CheckBox
    Friend WithEvents TextBox_PP As TextBox
    Friend WithEvents TextBox_PI As TextBox
    Friend WithEvents CheckBox_PI As CheckBox
    Friend WithEvents TextBox_PD As TextBox
    Friend WithEvents CheckBox_PD As CheckBox
    Friend WithEvents TextBox_RP As TextBox
    Friend WithEvents CheckBox_RP As CheckBox
    Friend WithEvents TextBox_RI As TextBox
    Friend WithEvents CheckBox_RI As CheckBox
    Friend WithEvents TextBox_RD As TextBox
    Friend WithEvents CheckBox_RD As CheckBox
    Friend WithEvents TextBox_YP As TextBox
    Friend WithEvents CheckBox_YP As CheckBox
    Friend WithEvents TextBox_YI As TextBox
    Friend WithEvents CheckBox_YI As CheckBox
    Friend WithEvents TextBox_YD As TextBox
    Friend WithEvents CheckBox_YD As CheckBox
    Friend WithEvents TextBox_F As TextBox
    Friend WithEvents CheckBox_F As CheckBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Button_Open_FPE As Button
    Friend WithEvents Button_FP_Load As Button
    Friend WithEvents CheckBox_Insert As CheckBox
    Friend WithEvents TextBox_Ins_After As TextBox
    Friend WithEvents CheckBox_Replace As CheckBox
    Friend WithEvents TextBox_Replace_From As TextBox
    Friend WithEvents TextBox_Replace_To As TextBox
    Friend WithEvents Label_R_To As Label
    Friend WithEvents Label_D_To As Label
    Friend WithEvents TextBox_Delete_To As TextBox
    Friend WithEvents TextBox_Delete_From As TextBox
    Friend WithEvents CheckBox_Delete As CheckBox
    Friend WithEvents Label_R_From As Label
    Friend WithEvents Label_D_From As Label
    Friend WithEvents Label_IF As Label
    Friend WithEvents ListView_WP As ListView
    Friend WithEvents Waypoints As ColumnHeader
    Friend WithEvents Label2 As Label
    Friend WithEvents LabelSure As Label
    Friend WithEvents ButtonYes As Button
    Friend WithEvents ButtonNo As Button
End Class
