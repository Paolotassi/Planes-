<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainMenu
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
        Me.ButtonFlightPlanEditor = New System.Windows.Forms.Button()
        Me.ButtonNewFlight = New System.Windows.Forms.Button()
        Me.ButtonReviewFlight = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ButtonFlightPlanEditor
        '
        Me.ButtonFlightPlanEditor.Location = New System.Drawing.Point(74, 46)
        Me.ButtonFlightPlanEditor.Name = "ButtonFlightPlanEditor"
        Me.ButtonFlightPlanEditor.Size = New System.Drawing.Size(198, 23)
        Me.ButtonFlightPlanEditor.TabIndex = 0
        Me.ButtonFlightPlanEditor.Text = "Flight Plan Editor"
        Me.ButtonFlightPlanEditor.UseVisualStyleBackColor = True
        '
        'ButtonNewFlight
        '
        Me.ButtonNewFlight.Location = New System.Drawing.Point(74, 95)
        Me.ButtonNewFlight.Name = "ButtonNewFlight"
        Me.ButtonNewFlight.Size = New System.Drawing.Size(198, 23)
        Me.ButtonNewFlight.TabIndex = 1
        Me.ButtonNewFlight.Text = "New Flight"
        Me.ButtonNewFlight.UseVisualStyleBackColor = True
        '
        'ButtonReviewFlight
        '
        Me.ButtonReviewFlight.Location = New System.Drawing.Point(74, 142)
        Me.ButtonReviewFlight.Name = "ButtonReviewFlight"
        Me.ButtonReviewFlight.Size = New System.Drawing.Size(198, 23)
        Me.ButtonReviewFlight.TabIndex = 2
        Me.ButtonReviewFlight.Text = "Review Flight"
        Me.ButtonReviewFlight.UseVisualStyleBackColor = True
        '
        'MainMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(381, 348)
        Me.Controls.Add(Me.ButtonReviewFlight)
        Me.Controls.Add(Me.ButtonNewFlight)
        Me.Controls.Add(Me.ButtonFlightPlanEditor)
        Me.Name = "MainMenu"
        Me.Text = "Main Menu"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Button_Create_Flight_Plan As Button
    Friend WithEvents Button_New_Flight As Button
    Friend WithEvents Button_Review_Flight As Button
    Friend WithEvents ButtonFlightPlanEditor As Button
    Friend WithEvents ButtonNewFlight As Button
    Friend WithEvents ButtonReviewFlight As Button
End Class
