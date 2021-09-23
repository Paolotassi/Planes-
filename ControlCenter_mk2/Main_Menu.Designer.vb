<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main_Menu
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Button_Create_Flight_Plan = New System.Windows.Forms.Button()
        Me.Button_New_Flight = New System.Windows.Forms.Button()
        Me.Button_Review_Flight = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Button_Create_Flight_Plan
        '
        Me.Button_Create_Flight_Plan.Location = New System.Drawing.Point(42, 40)
        Me.Button_Create_Flight_Plan.Name = "Button_Create_Flight_Plan"
        Me.Button_Create_Flight_Plan.Size = New System.Drawing.Size(212, 23)
        Me.Button_Create_Flight_Plan.TabIndex = 0
        Me.Button_Create_Flight_Plan.Text = "Flight Plan Editor"
        Me.Button_Create_Flight_Plan.UseVisualStyleBackColor = True
        '
        'Button_New_Flight
        '
        Me.Button_New_Flight.Location = New System.Drawing.Point(42, 123)
        Me.Button_New_Flight.Name = "Button_New_Flight"
        Me.Button_New_Flight.Size = New System.Drawing.Size(212, 23)
        Me.Button_New_Flight.TabIndex = 2
        Me.Button_New_Flight.Text = "New Flight"
        Me.Button_New_Flight.UseVisualStyleBackColor = True
        '
        'Button_Review_Flight
        '
        Me.Button_Review_Flight.Location = New System.Drawing.Point(42, 162)
        Me.Button_Review_Flight.Name = "Button_Review_Flight"
        Me.Button_Review_Flight.Size = New System.Drawing.Size(212, 23)
        Me.Button_Review_Flight.TabIndex = 3
        Me.Button_Review_Flight.Text = "Review Flight"
        Me.Button_Review_Flight.UseVisualStyleBackColor = True
        '
        'Main_Menu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(301, 255)
        Me.Controls.Add(Me.Button_Review_Flight)
        Me.Controls.Add(Me.Button_New_Flight)
        Me.Controls.Add(Me.Button_Create_Flight_Plan)
        Me.Name = "Main_Menu"
        Me.Text = "Control Center"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Button_Create_Flight_Plan As Button
    Friend WithEvents Button_New_Flight As Button
    Friend WithEvents Button_Review_Flight As Button
End Class
