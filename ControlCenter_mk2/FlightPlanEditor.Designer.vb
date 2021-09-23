<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FlightPlanEditor
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
        Me.ButtonSave = New System.Windows.Forms.Button()
        Me.ButtonLoad = New System.Windows.Forms.Button()
        Me.ButtonNew = New System.Windows.Forms.Button()
        Me.ButtonInstruction_Replace = New System.Windows.Forms.Button()
        Me.ButtonInstruction_Add = New System.Windows.Forms.Button()
        Me.ButtonInstruction_Delete = New System.Windows.Forms.Button()
        Me.ButtonNo = New System.Windows.Forms.Button()
        Me.ButtonYes = New System.Windows.Forms.Button()
        Me.LabelInstruction_Number = New System.Windows.Forms.Label()
        Me.NumericUpDownInstruction_Number = New System.Windows.Forms.NumericUpDown()
        Me.LabelInstruction_Type = New System.Windows.Forms.Label()
        Me.ComboBoxInstruction_Type_1 = New System.Windows.Forms.ComboBox()
        Me.ComboBoxInstruction_Type_2 = New System.Windows.Forms.ComboBox()
        Me.LabelInstruction_Values = New System.Windows.Forms.Label()
        Me.LabelInstruction_1 = New System.Windows.Forms.Label()
        Me.TextBoxInstruction_1 = New System.Windows.Forms.TextBox()
        Me.TextBoxInstruction_2 = New System.Windows.Forms.TextBox()
        Me.LabelInstruction_2 = New System.Windows.Forms.Label()
        Me.TextBoxInstruction_3 = New System.Windows.Forms.TextBox()
        Me.LabelInstruction_3 = New System.Windows.Forms.Label()
        Me.TextBoxInstruction_4 = New System.Windows.Forms.TextBox()
        Me.LabelInstruction_4 = New System.Windows.Forms.Label()
        Me.TextBoxInstruction_5 = New System.Windows.Forms.TextBox()
        Me.LabelInstruction_5 = New System.Windows.Forms.Label()
        Me.TextBoxInstruction_6 = New System.Windows.Forms.TextBox()
        Me.LabelInstruction_6 = New System.Windows.Forms.Label()
        Me.LabelSure = New System.Windows.Forms.Label()
        Me.ListViewOverview = New System.Windows.Forms.ListView()
        Me.LabelOverview = New System.Windows.Forms.Label()
        Me.LabelCodedString = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label_Invalid_File1 = New System.Windows.Forms.Label()
        CType(Me.NumericUpDownInstruction_Number, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ButtonSave
        '
        Me.ButtonSave.Location = New System.Drawing.Point(90, 12)
        Me.ButtonSave.Name = "ButtonSave"
        Me.ButtonSave.Size = New System.Drawing.Size(75, 23)
        Me.ButtonSave.TabIndex = 0
        Me.ButtonSave.Text = "Save"
        Me.ButtonSave.UseVisualStyleBackColor = True
        '
        'ButtonLoad
        '
        Me.ButtonLoad.Location = New System.Drawing.Point(171, 12)
        Me.ButtonLoad.Name = "ButtonLoad"
        Me.ButtonLoad.Size = New System.Drawing.Size(75, 23)
        Me.ButtonLoad.TabIndex = 1
        Me.ButtonLoad.Text = "Load"
        Me.ButtonLoad.UseVisualStyleBackColor = True
        '
        'ButtonNew
        '
        Me.ButtonNew.Location = New System.Drawing.Point(9, 12)
        Me.ButtonNew.Name = "ButtonNew"
        Me.ButtonNew.Size = New System.Drawing.Size(75, 23)
        Me.ButtonNew.TabIndex = 2
        Me.ButtonNew.Text = "New"
        Me.ButtonNew.UseVisualStyleBackColor = True
        '
        'ButtonInstruction_Replace
        '
        Me.ButtonInstruction_Replace.Location = New System.Drawing.Point(174, 488)
        Me.ButtonInstruction_Replace.Name = "ButtonInstruction_Replace"
        Me.ButtonInstruction_Replace.Size = New System.Drawing.Size(75, 23)
        Me.ButtonInstruction_Replace.TabIndex = 5
        Me.ButtonInstruction_Replace.Text = "Replace"
        Me.ButtonInstruction_Replace.UseVisualStyleBackColor = True
        '
        'ButtonInstruction_Add
        '
        Me.ButtonInstruction_Add.Location = New System.Drawing.Point(93, 488)
        Me.ButtonInstruction_Add.Name = "ButtonInstruction_Add"
        Me.ButtonInstruction_Add.Size = New System.Drawing.Size(75, 23)
        Me.ButtonInstruction_Add.TabIndex = 4
        Me.ButtonInstruction_Add.Text = "Add"
        Me.ButtonInstruction_Add.UseVisualStyleBackColor = True
        '
        'ButtonInstruction_Delete
        '
        Me.ButtonInstruction_Delete.Location = New System.Drawing.Point(12, 488)
        Me.ButtonInstruction_Delete.Name = "ButtonInstruction_Delete"
        Me.ButtonInstruction_Delete.Size = New System.Drawing.Size(75, 23)
        Me.ButtonInstruction_Delete.TabIndex = 3
        Me.ButtonInstruction_Delete.Text = "Delete"
        Me.ButtonInstruction_Delete.UseVisualStyleBackColor = True
        '
        'ButtonNo
        '
        Me.ButtonNo.Location = New System.Drawing.Point(174, 526)
        Me.ButtonNo.Name = "ButtonNo"
        Me.ButtonNo.Size = New System.Drawing.Size(75, 23)
        Me.ButtonNo.TabIndex = 7
        Me.ButtonNo.Text = "No"
        Me.ButtonNo.UseVisualStyleBackColor = True
        '
        'ButtonYes
        '
        Me.ButtonYes.Location = New System.Drawing.Point(93, 526)
        Me.ButtonYes.Name = "ButtonYes"
        Me.ButtonYes.Size = New System.Drawing.Size(75, 23)
        Me.ButtonYes.TabIndex = 6
        Me.ButtonYes.Text = "Yes"
        Me.ButtonYes.UseVisualStyleBackColor = True
        '
        'LabelInstruction_Number
        '
        Me.LabelInstruction_Number.AutoSize = True
        Me.LabelInstruction_Number.Location = New System.Drawing.Point(13, 60)
        Me.LabelInstruction_Number.Name = "LabelInstruction_Number"
        Me.LabelInstruction_Number.Size = New System.Drawing.Size(96, 13)
        Me.LabelInstruction_Number.TabIndex = 8
        Me.LabelInstruction_Number.Text = "Instruction Number"
        '
        'NumericUpDownInstruction_Number
        '
        Me.NumericUpDownInstruction_Number.Location = New System.Drawing.Point(12, 77)
        Me.NumericUpDownInstruction_Number.Name = "NumericUpDownInstruction_Number"
        Me.NumericUpDownInstruction_Number.Size = New System.Drawing.Size(120, 20)
        Me.NumericUpDownInstruction_Number.TabIndex = 9
        '
        'LabelInstruction_Type
        '
        Me.LabelInstruction_Type.AutoSize = True
        Me.LabelInstruction_Type.Location = New System.Drawing.Point(13, 122)
        Me.LabelInstruction_Type.Name = "LabelInstruction_Type"
        Me.LabelInstruction_Type.Size = New System.Drawing.Size(83, 13)
        Me.LabelInstruction_Type.TabIndex = 10
        Me.LabelInstruction_Type.Text = "Instruction Type"
        '
        'ComboBoxInstruction_Type_1
        '
        Me.ComboBoxInstruction_Type_1.FormattingEnabled = True
        Me.ComboBoxInstruction_Type_1.Location = New System.Drawing.Point(16, 149)
        Me.ComboBoxInstruction_Type_1.Name = "ComboBoxInstruction_Type_1"
        Me.ComboBoxInstruction_Type_1.Size = New System.Drawing.Size(152, 21)
        Me.ComboBoxInstruction_Type_1.TabIndex = 11
        '
        'ComboBoxInstruction_Type_2
        '
        Me.ComboBoxInstruction_Type_2.FormattingEnabled = True
        Me.ComboBoxInstruction_Type_2.Location = New System.Drawing.Point(174, 149)
        Me.ComboBoxInstruction_Type_2.Name = "ComboBoxInstruction_Type_2"
        Me.ComboBoxInstruction_Type_2.Size = New System.Drawing.Size(152, 21)
        Me.ComboBoxInstruction_Type_2.TabIndex = 12
        '
        'LabelInstruction_Values
        '
        Me.LabelInstruction_Values.AutoSize = True
        Me.LabelInstruction_Values.Location = New System.Drawing.Point(13, 187)
        Me.LabelInstruction_Values.Name = "LabelInstruction_Values"
        Me.LabelInstruction_Values.Size = New System.Drawing.Size(91, 13)
        Me.LabelInstruction_Values.TabIndex = 13
        Me.LabelInstruction_Values.Text = "Instruction Values"
        '
        'LabelInstruction_1
        '
        Me.LabelInstruction_1.AutoSize = True
        Me.LabelInstruction_1.Location = New System.Drawing.Point(16, 221)
        Me.LabelInstruction_1.Name = "LabelInstruction_1"
        Me.LabelInstruction_1.Size = New System.Drawing.Size(39, 13)
        Me.LabelInstruction_1.TabIndex = 14
        Me.LabelInstruction_1.Text = "Label1"
        '
        'TextBoxInstruction_1
        '
        Me.TextBoxInstruction_1.Location = New System.Drawing.Point(93, 218)
        Me.TextBoxInstruction_1.Name = "TextBoxInstruction_1"
        Me.TextBoxInstruction_1.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxInstruction_1.TabIndex = 15
        '
        'TextBoxInstruction_2
        '
        Me.TextBoxInstruction_2.Location = New System.Drawing.Point(93, 255)
        Me.TextBoxInstruction_2.Name = "TextBoxInstruction_2"
        Me.TextBoxInstruction_2.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxInstruction_2.TabIndex = 17
        '
        'LabelInstruction_2
        '
        Me.LabelInstruction_2.AutoSize = True
        Me.LabelInstruction_2.Location = New System.Drawing.Point(16, 258)
        Me.LabelInstruction_2.Name = "LabelInstruction_2"
        Me.LabelInstruction_2.Size = New System.Drawing.Size(39, 13)
        Me.LabelInstruction_2.TabIndex = 16
        Me.LabelInstruction_2.Text = "Label2"
        '
        'TextBoxInstruction_3
        '
        Me.TextBoxInstruction_3.Location = New System.Drawing.Point(93, 296)
        Me.TextBoxInstruction_3.Name = "TextBoxInstruction_3"
        Me.TextBoxInstruction_3.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxInstruction_3.TabIndex = 21
        '
        'LabelInstruction_3
        '
        Me.LabelInstruction_3.AutoSize = True
        Me.LabelInstruction_3.Location = New System.Drawing.Point(16, 299)
        Me.LabelInstruction_3.Name = "LabelInstruction_3"
        Me.LabelInstruction_3.Size = New System.Drawing.Size(39, 13)
        Me.LabelInstruction_3.TabIndex = 20
        Me.LabelInstruction_3.Text = "Label3"
        '
        'TextBoxInstruction_4
        '
        Me.TextBoxInstruction_4.Location = New System.Drawing.Point(93, 331)
        Me.TextBoxInstruction_4.Name = "TextBoxInstruction_4"
        Me.TextBoxInstruction_4.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxInstruction_4.TabIndex = 19
        '
        'LabelInstruction_4
        '
        Me.LabelInstruction_4.AutoSize = True
        Me.LabelInstruction_4.Location = New System.Drawing.Point(16, 334)
        Me.LabelInstruction_4.Name = "LabelInstruction_4"
        Me.LabelInstruction_4.Size = New System.Drawing.Size(39, 13)
        Me.LabelInstruction_4.TabIndex = 18
        Me.LabelInstruction_4.Text = "Label4"
        '
        'TextBoxInstruction_5
        '
        Me.TextBoxInstruction_5.Location = New System.Drawing.Point(93, 367)
        Me.TextBoxInstruction_5.Name = "TextBoxInstruction_5"
        Me.TextBoxInstruction_5.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxInstruction_5.TabIndex = 25
        '
        'LabelInstruction_5
        '
        Me.LabelInstruction_5.AutoSize = True
        Me.LabelInstruction_5.Location = New System.Drawing.Point(16, 370)
        Me.LabelInstruction_5.Name = "LabelInstruction_5"
        Me.LabelInstruction_5.Size = New System.Drawing.Size(39, 13)
        Me.LabelInstruction_5.TabIndex = 24
        Me.LabelInstruction_5.Text = "Label5"
        '
        'TextBoxInstruction_6
        '
        Me.TextBoxInstruction_6.Location = New System.Drawing.Point(93, 403)
        Me.TextBoxInstruction_6.Name = "TextBoxInstruction_6"
        Me.TextBoxInstruction_6.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxInstruction_6.TabIndex = 23
        '
        'LabelInstruction_6
        '
        Me.LabelInstruction_6.AutoSize = True
        Me.LabelInstruction_6.Location = New System.Drawing.Point(16, 406)
        Me.LabelInstruction_6.Name = "LabelInstruction_6"
        Me.LabelInstruction_6.Size = New System.Drawing.Size(39, 13)
        Me.LabelInstruction_6.TabIndex = 22
        Me.LabelInstruction_6.Text = "Label6"
        '
        'LabelSure
        '
        Me.LabelSure.AutoSize = True
        Me.LabelSure.Location = New System.Drawing.Point(12, 531)
        Me.LabelSure.Name = "LabelSure"
        Me.LabelSure.Size = New System.Drawing.Size(72, 13)
        Me.LabelSure.TabIndex = 26
        Me.LabelSure.Text = "Are you sure?"
        '
        'ListViewOverview
        '
        Me.ListViewOverview.GridLines = True
        Me.ListViewOverview.HideSelection = False
        Me.ListViewOverview.Location = New System.Drawing.Point(367, 100)
        Me.ListViewOverview.Name = "ListViewOverview"
        Me.ListViewOverview.Size = New System.Drawing.Size(559, 463)
        Me.ListViewOverview.TabIndex = 27
        Me.ListViewOverview.UseCompatibleStateImageBehavior = False
        Me.ListViewOverview.View = System.Windows.Forms.View.Details
        '
        'LabelOverview
        '
        Me.LabelOverview.AutoSize = True
        Me.LabelOverview.Location = New System.Drawing.Point(364, 77)
        Me.LabelOverview.Name = "LabelOverview"
        Me.LabelOverview.Size = New System.Drawing.Size(52, 13)
        Me.LabelOverview.TabIndex = 28
        Me.LabelOverview.Text = "Overview"
        '
        'LabelCodedString
        '
        Me.LabelCodedString.AutoSize = True
        Me.LabelCodedString.Location = New System.Drawing.Point(364, 17)
        Me.LabelCodedString.Name = "LabelCodedString"
        Me.LabelCodedString.Size = New System.Drawing.Size(68, 13)
        Me.LabelCodedString.TabIndex = 29
        Me.LabelCodedString.Text = "Coded String"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(367, 43)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(559, 20)
        Me.TextBox1.TabIndex = 30
        '
        'Label_Invalid_File1
        '
        Me.Label_Invalid_File1.AutoSize = True
        Me.Label_Invalid_File1.Location = New System.Drawing.Point(252, 17)
        Me.Label_Invalid_File1.Name = "Label_Invalid_File1"
        Me.Label_Invalid_File1.Size = New System.Drawing.Size(82, 13)
        Me.Label_Invalid_File1.TabIndex = 31
        Me.Label_Invalid_File1.Text = "Error: Invalid file"
        Me.Label_Invalid_File1.Visible = False
        '
        'FlightPlanEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(938, 573)
        Me.Controls.Add(Me.Label_Invalid_File1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.LabelCodedString)
        Me.Controls.Add(Me.LabelOverview)
        Me.Controls.Add(Me.ListViewOverview)
        Me.Controls.Add(Me.LabelSure)
        Me.Controls.Add(Me.TextBoxInstruction_5)
        Me.Controls.Add(Me.LabelInstruction_5)
        Me.Controls.Add(Me.TextBoxInstruction_6)
        Me.Controls.Add(Me.LabelInstruction_6)
        Me.Controls.Add(Me.TextBoxInstruction_3)
        Me.Controls.Add(Me.LabelInstruction_3)
        Me.Controls.Add(Me.TextBoxInstruction_4)
        Me.Controls.Add(Me.LabelInstruction_4)
        Me.Controls.Add(Me.TextBoxInstruction_2)
        Me.Controls.Add(Me.LabelInstruction_2)
        Me.Controls.Add(Me.TextBoxInstruction_1)
        Me.Controls.Add(Me.LabelInstruction_1)
        Me.Controls.Add(Me.LabelInstruction_Values)
        Me.Controls.Add(Me.ComboBoxInstruction_Type_2)
        Me.Controls.Add(Me.ComboBoxInstruction_Type_1)
        Me.Controls.Add(Me.LabelInstruction_Type)
        Me.Controls.Add(Me.NumericUpDownInstruction_Number)
        Me.Controls.Add(Me.LabelInstruction_Number)
        Me.Controls.Add(Me.ButtonNo)
        Me.Controls.Add(Me.ButtonYes)
        Me.Controls.Add(Me.ButtonInstruction_Replace)
        Me.Controls.Add(Me.ButtonInstruction_Add)
        Me.Controls.Add(Me.ButtonInstruction_Delete)
        Me.Controls.Add(Me.ButtonNew)
        Me.Controls.Add(Me.ButtonLoad)
        Me.Controls.Add(Me.ButtonSave)
        Me.Name = "FlightPlanEditor"
        Me.Text = "Flight Plan Editor"
        CType(Me.NumericUpDownInstruction_Number, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ButtonSave As Button
    Friend WithEvents ButtonLoad As Button
    Friend WithEvents ButtonNew As Button
    Friend WithEvents ButtonInstruction_Replace As Button
    Friend WithEvents ButtonInstruction_Add As Button
    Friend WithEvents ButtonInstruction_Delete As Button
    Friend WithEvents ButtonNo As Button
    Friend WithEvents ButtonYes As Button
    Friend WithEvents LabelInstruction_Number As Label
    Friend WithEvents NumericUpDownInstruction_Number As NumericUpDown
    Friend WithEvents LabelInstruction_Type As Label
    Friend WithEvents ComboBoxInstruction_Type_1 As ComboBox
    Friend WithEvents ComboBoxInstruction_Type_2 As ComboBox
    Friend WithEvents LabelInstruction_Values As Label
    Friend WithEvents LabelInstruction_1 As Label
    Friend WithEvents TextBoxInstruction_1 As TextBox
    Friend WithEvents TextBoxInstruction_2 As TextBox
    Friend WithEvents LabelInstruction_2 As Label
    Friend WithEvents TextBoxInstruction_3 As TextBox
    Friend WithEvents LabelInstruction_3 As Label
    Friend WithEvents TextBoxInstruction_4 As TextBox
    Friend WithEvents LabelInstruction_4 As Label
    Friend WithEvents TextBoxInstruction_5 As TextBox
    Friend WithEvents LabelInstruction_5 As Label
    Friend WithEvents TextBoxInstruction_6 As TextBox
    Friend WithEvents LabelInstruction_6 As Label
    Friend WithEvents LabelSure As Label
    Friend WithEvents ListViewOverview As ListView
    Friend WithEvents LabelOverview As Label
    Friend WithEvents LabelCodedString As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label_Invalid_File1 As Label
End Class
