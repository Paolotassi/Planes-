<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Button_Open_Instrument_Panel
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
        Me.Button_Setup_Sequence = New System.Windows.Forms.Button()
        Me.ProgressBar_Setup_Sequence = New System.Windows.Forms.ProgressBar()
        Me.CheckBox_Setup_Sequence = New System.Windows.Forms.CheckBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Button_Load_Flight_Plan = New System.Windows.Forms.Button()
        Me.Button_Open_IP = New System.Windows.Forms.Button()
        Me.ListBox_Serial_Ports = New System.Windows.Forms.ListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button_Ok = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Button_Setup_Sequence
        '
        Me.Button_Setup_Sequence.Location = New System.Drawing.Point(12, 188)
        Me.Button_Setup_Sequence.Name = "Button_Setup_Sequence"
        Me.Button_Setup_Sequence.Size = New System.Drawing.Size(145, 23)
        Me.Button_Setup_Sequence.TabIndex = 0
        Me.Button_Setup_Sequence.Text = "Setup Sequence"
        Me.Button_Setup_Sequence.UseVisualStyleBackColor = True
        '
        'ProgressBar_Setup_Sequence
        '
        Me.ProgressBar_Setup_Sequence.Location = New System.Drawing.Point(192, 188)
        Me.ProgressBar_Setup_Sequence.Name = "ProgressBar_Setup_Sequence"
        Me.ProgressBar_Setup_Sequence.Size = New System.Drawing.Size(167, 23)
        Me.ProgressBar_Setup_Sequence.TabIndex = 1
        '
        'CheckBox_Setup_Sequence
        '
        Me.CheckBox_Setup_Sequence.AutoSize = True
        Me.CheckBox_Setup_Sequence.Location = New System.Drawing.Point(384, 191)
        Me.CheckBox_Setup_Sequence.Name = "CheckBox_Setup_Sequence"
        Me.CheckBox_Setup_Sequence.Size = New System.Drawing.Size(78, 19)
        Me.CheckBox_Setup_Sequence.TabIndex = 2
        Me.CheckBox_Setup_Sequence.Text = "Complete"
        Me.CheckBox_Setup_Sequence.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(384, 246)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(78, 19)
        Me.CheckBox1.TabIndex = 5
        Me.CheckBox1.Text = "Complete"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(192, 243)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(167, 23)
        Me.ProgressBar1.TabIndex = 4
        '
        'Button_Load_Flight_Plan
        '
        Me.Button_Load_Flight_Plan.Location = New System.Drawing.Point(12, 243)
        Me.Button_Load_Flight_Plan.Name = "Button_Load_Flight_Plan"
        Me.Button_Load_Flight_Plan.Size = New System.Drawing.Size(145, 23)
        Me.Button_Load_Flight_Plan.TabIndex = 3
        Me.Button_Load_Flight_Plan.Text = "Load Flight Plan"
        Me.Button_Load_Flight_Plan.UseVisualStyleBackColor = True
        '
        'Button_Open_IP
        '
        Me.Button_Open_IP.Location = New System.Drawing.Point(12, 298)
        Me.Button_Open_IP.Name = "Button_Open_IP"
        Me.Button_Open_IP.Size = New System.Drawing.Size(203, 23)
        Me.Button_Open_IP.TabIndex = 6
        Me.Button_Open_IP.Text = "Open Instrument Panel"
        Me.Button_Open_IP.UseVisualStyleBackColor = True
        '
        'ListBox_Serial_Ports
        '
        Me.ListBox_Serial_Ports.FormattingEnabled = True
        Me.ListBox_Serial_Ports.ItemHeight = 15
        Me.ListBox_Serial_Ports.Location = New System.Drawing.Point(12, 33)
        Me.ListBox_Serial_Ports.Name = "ListBox_Serial_Ports"
        Me.ListBox_Serial_Ports.Size = New System.Drawing.Size(120, 94)
        Me.ListBox_Serial_Ports.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(94, 15)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Select Serial Port"
        '
        'Button_Ok
        '
        Me.Button_Ok.Location = New System.Drawing.Point(31, 133)
        Me.Button_Ok.Name = "Button_Ok"
        Me.Button_Ok.Size = New System.Drawing.Size(75, 23)
        Me.Button_Ok.TabIndex = 9
        Me.Button_Ok.Text = "Ok"
        Me.Button_Ok.UseVisualStyleBackColor = True
        '
        'Button_Open_Instrument_Panel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Button_Ok)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ListBox_Serial_Ports)
        Me.Controls.Add(Me.Button_Open_IP)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Button_Load_Flight_Plan)
        Me.Controls.Add(Me.CheckBox_Setup_Sequence)
        Me.Controls.Add(Me.ProgressBar_Setup_Sequence)
        Me.Controls.Add(Me.Button_Setup_Sequence)
        Me.Name = "Button_Open_Instrument_Panel"
        Me.Text = "New_Flight"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button_Setup_Sequence As Button
    Friend WithEvents ProgressBar_Setup_Sequence As ProgressBar
    Friend WithEvents CheckBox_Setup_Sequence As CheckBox
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Button_Load_Flight_Plan As Button
    Friend WithEvents Button_Open_IP As Button
    Friend WithEvents ListBox_Serial_Ports As ListBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Button_Ok As Button
End Class
