<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Create_Flight_Plan
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
        Me.Label_Instruction_Number = New System.Windows.Forms.Label()
        Me.NumericUpDown_Instruction_Number = New System.Windows.Forms.NumericUpDown()
        Me.Label_Instruction_Type = New System.Windows.Forms.Label()
        Me.ComboBox_Instruction_Type_1 = New System.Windows.Forms.ComboBox()
        Me.ComboBox_Instruction_Type_2 = New System.Windows.Forms.ComboBox()
        Me.Label_Instruction_1 = New System.Windows.Forms.Label()
        Me.TextBox_Instruction_1 = New System.Windows.Forms.TextBox()
        Me.Label_Instruction_Values = New System.Windows.Forms.Label()
        Me.TextBox_Instruction_2 = New System.Windows.Forms.TextBox()
        Me.Label_Instruction_2 = New System.Windows.Forms.Label()
        Me.TextBox_Instruction_4 = New System.Windows.Forms.TextBox()
        Me.Label_Instruction_4 = New System.Windows.Forms.Label()
        Me.TextBox_Instruction_3 = New System.Windows.Forms.TextBox()
        Me.Label_Instruction_3 = New System.Windows.Forms.Label()
        Me.TextBox_Instruction_6 = New System.Windows.Forms.TextBox()
        Me.Label_Instruction_6 = New System.Windows.Forms.Label()
        Me.TextBox_Instruction_5 = New System.Windows.Forms.TextBox()
        Me.Label_Instruction_5 = New System.Windows.Forms.Label()
        Me.Button_Instruction_Add = New System.Windows.Forms.Button()
        Me.Button_Instruction_Replace = New System.Windows.Forms.Button()
        Me.Button_Instruction_Delete = New System.Windows.Forms.Button()
        Me.Label_Sure = New System.Windows.Forms.Label()
        Me.Button_No = New System.Windows.Forms.Button()
        Me.Button_Yes = New System.Windows.Forms.Button()
        Me.ListView_Overview = New System.Windows.Forms.ListView()
        Me.Label_Flight_Plan_Overview = New System.Windows.Forms.Label()
        Me.Button_Save = New System.Windows.Forms.Button()
        Me.Button_Load = New System.Windows.Forms.Button()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label_Coded_String = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Button_New = New System.Windows.Forms.Button()
        CType(Me.NumericUpDown_Instruction_Number, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label_Instruction_Number
        '
        Me.Label_Instruction_Number.AutoSize = True
        Me.Label_Instruction_Number.Location = New System.Drawing.Point(23, 77)
        Me.Label_Instruction_Number.Name = "Label_Instruction_Number"
        Me.Label_Instruction_Number.Size = New System.Drawing.Size(111, 15)
        Me.Label_Instruction_Number.TabIndex = 0
        Me.Label_Instruction_Number.Text = "Instruction Number"
        '
        'NumericUpDown_Instruction_Number
        '
        Me.NumericUpDown_Instruction_Number.Location = New System.Drawing.Point(23, 95)
        Me.NumericUpDown_Instruction_Number.Name = "NumericUpDown_Instruction_Number"
        Me.NumericUpDown_Instruction_Number.Size = New System.Drawing.Size(111, 23)
        Me.NumericUpDown_Instruction_Number.TabIndex = 1
        Me.NumericUpDown_Instruction_Number.ThousandsSeparator = True
        '
        'Label_Instruction_Type
        '
        Me.Label_Instruction_Type.AutoSize = True
        Me.Label_Instruction_Type.Location = New System.Drawing.Point(23, 134)
        Me.Label_Instruction_Type.Name = "Label_Instruction_Type"
        Me.Label_Instruction_Type.Size = New System.Drawing.Size(91, 15)
        Me.Label_Instruction_Type.TabIndex = 2
        Me.Label_Instruction_Type.Text = "Instruction Type"
        '
        'ComboBox_Instruction_Type_1
        '
        Me.ComboBox_Instruction_Type_1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_Instruction_Type_1.FormattingEnabled = True
        Me.ComboBox_Instruction_Type_1.Location = New System.Drawing.Point(23, 151)
        Me.ComboBox_Instruction_Type_1.Name = "ComboBox_Instruction_Type_1"
        Me.ComboBox_Instruction_Type_1.Size = New System.Drawing.Size(159, 23)
        Me.ComboBox_Instruction_Type_1.TabIndex = 3
        '
        'ComboBox_Instruction_Type_2
        '
        Me.ComboBox_Instruction_Type_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_Instruction_Type_2.FormattingEnabled = True
        Me.ComboBox_Instruction_Type_2.Location = New System.Drawing.Point(188, 151)
        Me.ComboBox_Instruction_Type_2.Name = "ComboBox_Instruction_Type_2"
        Me.ComboBox_Instruction_Type_2.Size = New System.Drawing.Size(159, 23)
        Me.ComboBox_Instruction_Type_2.TabIndex = 5
        '
        'Label_Instruction_1
        '
        Me.Label_Instruction_1.AutoSize = True
        Me.Label_Instruction_1.Location = New System.Drawing.Point(23, 230)
        Me.Label_Instruction_1.Name = "Label_Instruction_1"
        Me.Label_Instruction_1.Size = New System.Drawing.Size(41, 15)
        Me.Label_Instruction_1.TabIndex = 6
        Me.Label_Instruction_1.Text = "Label1"
        '
        'TextBox_Instruction_1
        '
        Me.TextBox_Instruction_1.Location = New System.Drawing.Point(134, 227)
        Me.TextBox_Instruction_1.Name = "TextBox_Instruction_1"
        Me.TextBox_Instruction_1.Size = New System.Drawing.Size(100, 23)
        Me.TextBox_Instruction_1.TabIndex = 7
        Me.TextBox_Instruction_1.Text = "0"
        Me.TextBox_Instruction_1.Visible = False
        '
        'Label_Instruction_Values
        '
        Me.Label_Instruction_Values.AutoSize = True
        Me.Label_Instruction_Values.Location = New System.Drawing.Point(23, 195)
        Me.Label_Instruction_Values.Name = "Label_Instruction_Values"
        Me.Label_Instruction_Values.Size = New System.Drawing.Size(100, 15)
        Me.Label_Instruction_Values.TabIndex = 8
        Me.Label_Instruction_Values.Text = "Instruction Values"
        '
        'TextBox_Instruction_2
        '
        Me.TextBox_Instruction_2.Location = New System.Drawing.Point(134, 266)
        Me.TextBox_Instruction_2.Name = "TextBox_Instruction_2"
        Me.TextBox_Instruction_2.Size = New System.Drawing.Size(100, 23)
        Me.TextBox_Instruction_2.TabIndex = 10
        Me.TextBox_Instruction_2.Text = "0"
        Me.TextBox_Instruction_2.Visible = False
        '
        'Label_Instruction_2
        '
        Me.Label_Instruction_2.AutoSize = True
        Me.Label_Instruction_2.Location = New System.Drawing.Point(23, 269)
        Me.Label_Instruction_2.Name = "Label_Instruction_2"
        Me.Label_Instruction_2.Size = New System.Drawing.Size(41, 15)
        Me.Label_Instruction_2.TabIndex = 9
        Me.Label_Instruction_2.Text = "Label2"
        Me.Label_Instruction_2.Visible = False
        '
        'TextBox_Instruction_4
        '
        Me.TextBox_Instruction_4.Location = New System.Drawing.Point(134, 345)
        Me.TextBox_Instruction_4.Name = "TextBox_Instruction_4"
        Me.TextBox_Instruction_4.Size = New System.Drawing.Size(100, 23)
        Me.TextBox_Instruction_4.TabIndex = 14
        Me.TextBox_Instruction_4.Text = "0"
        Me.TextBox_Instruction_4.Visible = False
        '
        'Label_Instruction_4
        '
        Me.Label_Instruction_4.AutoSize = True
        Me.Label_Instruction_4.Location = New System.Drawing.Point(23, 348)
        Me.Label_Instruction_4.Name = "Label_Instruction_4"
        Me.Label_Instruction_4.Size = New System.Drawing.Size(41, 15)
        Me.Label_Instruction_4.TabIndex = 13
        Me.Label_Instruction_4.Text = "Label4"
        Me.Label_Instruction_4.Visible = False
        '
        'TextBox_Instruction_3
        '
        Me.TextBox_Instruction_3.Location = New System.Drawing.Point(134, 306)
        Me.TextBox_Instruction_3.Name = "TextBox_Instruction_3"
        Me.TextBox_Instruction_3.Size = New System.Drawing.Size(100, 23)
        Me.TextBox_Instruction_3.TabIndex = 12
        Me.TextBox_Instruction_3.Text = "0"
        Me.TextBox_Instruction_3.Visible = False
        '
        'Label_Instruction_3
        '
        Me.Label_Instruction_3.AutoSize = True
        Me.Label_Instruction_3.Location = New System.Drawing.Point(23, 309)
        Me.Label_Instruction_3.Name = "Label_Instruction_3"
        Me.Label_Instruction_3.Size = New System.Drawing.Size(41, 15)
        Me.Label_Instruction_3.TabIndex = 11
        Me.Label_Instruction_3.Text = "Label3"
        Me.Label_Instruction_3.Visible = False
        '
        'TextBox_Instruction_6
        '
        Me.TextBox_Instruction_6.Location = New System.Drawing.Point(134, 424)
        Me.TextBox_Instruction_6.Name = "TextBox_Instruction_6"
        Me.TextBox_Instruction_6.Size = New System.Drawing.Size(100, 23)
        Me.TextBox_Instruction_6.TabIndex = 18
        Me.TextBox_Instruction_6.Text = "0"
        Me.TextBox_Instruction_6.Visible = False
        '
        'Label_Instruction_6
        '
        Me.Label_Instruction_6.AutoSize = True
        Me.Label_Instruction_6.Location = New System.Drawing.Point(23, 427)
        Me.Label_Instruction_6.Name = "Label_Instruction_6"
        Me.Label_Instruction_6.Size = New System.Drawing.Size(41, 15)
        Me.Label_Instruction_6.TabIndex = 17
        Me.Label_Instruction_6.Text = "Label6"
        Me.Label_Instruction_6.Visible = False
        '
        'TextBox_Instruction_5
        '
        Me.TextBox_Instruction_5.Location = New System.Drawing.Point(134, 385)
        Me.TextBox_Instruction_5.Name = "TextBox_Instruction_5"
        Me.TextBox_Instruction_5.Size = New System.Drawing.Size(100, 23)
        Me.TextBox_Instruction_5.TabIndex = 16
        Me.TextBox_Instruction_5.Text = "0"
        Me.TextBox_Instruction_5.Visible = False
        '
        'Label_Instruction_5
        '
        Me.Label_Instruction_5.AutoSize = True
        Me.Label_Instruction_5.Location = New System.Drawing.Point(23, 388)
        Me.Label_Instruction_5.Name = "Label_Instruction_5"
        Me.Label_Instruction_5.Size = New System.Drawing.Size(41, 15)
        Me.Label_Instruction_5.TabIndex = 15
        Me.Label_Instruction_5.Text = "Label5"
        Me.Label_Instruction_5.Visible = False
        '
        'Button_Instruction_Add
        '
        Me.Button_Instruction_Add.Location = New System.Drawing.Point(137, 489)
        Me.Button_Instruction_Add.Name = "Button_Instruction_Add"
        Me.Button_Instruction_Add.Size = New System.Drawing.Size(75, 23)
        Me.Button_Instruction_Add.TabIndex = 19
        Me.Button_Instruction_Add.Text = "Add"
        Me.Button_Instruction_Add.UseVisualStyleBackColor = True
        '
        'Button_Instruction_Replace
        '
        Me.Button_Instruction_Replace.Location = New System.Drawing.Point(248, 489)
        Me.Button_Instruction_Replace.Name = "Button_Instruction_Replace"
        Me.Button_Instruction_Replace.Size = New System.Drawing.Size(75, 23)
        Me.Button_Instruction_Replace.TabIndex = 20
        Me.Button_Instruction_Replace.Text = "Replace"
        Me.Button_Instruction_Replace.UseVisualStyleBackColor = True
        '
        'Button_Instruction_Delete
        '
        Me.Button_Instruction_Delete.Location = New System.Drawing.Point(23, 489)
        Me.Button_Instruction_Delete.Name = "Button_Instruction_Delete"
        Me.Button_Instruction_Delete.Size = New System.Drawing.Size(75, 23)
        Me.Button_Instruction_Delete.TabIndex = 21
        Me.Button_Instruction_Delete.Text = "Delete"
        Me.Button_Instruction_Delete.UseVisualStyleBackColor = True
        '
        'Label_Sure
        '
        Me.Label_Sure.AutoSize = True
        Me.Label_Sure.Location = New System.Drawing.Point(45, 553)
        Me.Label_Sure.Name = "Label_Sure"
        Me.Label_Sure.Size = New System.Drawing.Size(78, 15)
        Me.Label_Sure.TabIndex = 22
        Me.Label_Sure.Text = "Are you sure?"
        Me.Label_Sure.Visible = False
        '
        'Button_No
        '
        Me.Button_No.Location = New System.Drawing.Point(248, 549)
        Me.Button_No.Name = "Button_No"
        Me.Button_No.Size = New System.Drawing.Size(75, 23)
        Me.Button_No.TabIndex = 24
        Me.Button_No.Text = "No"
        Me.Button_No.UseVisualStyleBackColor = True
        Me.Button_No.Visible = False
        '
        'Button_Yes
        '
        Me.Button_Yes.Location = New System.Drawing.Point(137, 549)
        Me.Button_Yes.Name = "Button_Yes"
        Me.Button_Yes.Size = New System.Drawing.Size(75, 23)
        Me.Button_Yes.TabIndex = 23
        Me.Button_Yes.Text = "Yes"
        Me.Button_Yes.UseVisualStyleBackColor = True
        Me.Button_Yes.Visible = False
        '
        'ListView_Overview
        '
        Me.ListView_Overview.GridLines = True
        Me.ListView_Overview.HideSelection = False
        Me.ListView_Overview.Location = New System.Drawing.Point(398, 95)
        Me.ListView_Overview.Name = "ListView_Overview"
        Me.ListView_Overview.Size = New System.Drawing.Size(587, 551)
        Me.ListView_Overview.TabIndex = 25
        Me.ListView_Overview.UseCompatibleStateImageBehavior = False
        Me.ListView_Overview.View = System.Windows.Forms.View.Details
        '
        'Label_Flight_Plan_Overview
        '
        Me.Label_Flight_Plan_Overview.AutoSize = True
        Me.Label_Flight_Plan_Overview.Location = New System.Drawing.Point(398, 77)
        Me.Label_Flight_Plan_Overview.Name = "Label_Flight_Plan_Overview"
        Me.Label_Flight_Plan_Overview.Size = New System.Drawing.Size(56, 15)
        Me.Label_Flight_Plan_Overview.TabIndex = 26
        Me.Label_Flight_Plan_Overview.Text = "Overview"
        '
        'Button_Save
        '
        Me.Button_Save.Location = New System.Drawing.Point(104, 24)
        Me.Button_Save.Name = "Button_Save"
        Me.Button_Save.Size = New System.Drawing.Size(75, 23)
        Me.Button_Save.TabIndex = 27
        Me.Button_Save.Text = "Save"
        Me.Button_Save.UseVisualStyleBackColor = True
        '
        'Button_Load
        '
        Me.Button_Load.Location = New System.Drawing.Point(188, 24)
        Me.Button_Load.Name = "Button_Load"
        Me.Button_Load.Size = New System.Drawing.Size(75, 23)
        Me.Button_Load.TabIndex = 28
        Me.Button_Load.Text = "Load"
        Me.Button_Load.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.AcceptsReturn = True
        Me.TextBox1.AcceptsTab = True
        Me.TextBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TextBox1.Location = New System.Drawing.Point(398, 50)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox1.Size = New System.Drawing.Size(565, 24)
        Me.TextBox1.TabIndex = 29
        '
        'Label_Coded_String
        '
        Me.Label_Coded_String.AutoSize = True
        Me.Label_Coded_String.Location = New System.Drawing.Point(398, 32)
        Me.Label_Coded_String.Name = "Label_Coded_String"
        Me.Label_Coded_String.Size = New System.Drawing.Size(76, 15)
        Me.Label_Coded_String.TabIndex = 30
        Me.Label_Coded_String.Text = "Coded String"
        Me.Label_Coded_String.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Button_New
        '
        Me.Button_New.Location = New System.Drawing.Point(23, 24)
        Me.Button_New.Name = "Button_New"
        Me.Button_New.Size = New System.Drawing.Size(75, 23)
        Me.Button_New.TabIndex = 31
        Me.Button_New.Text = "New"
        Me.Button_New.UseVisualStyleBackColor = True
        '
        'Create_Flight_Plan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(997, 663)
        Me.Controls.Add(Me.Button_New)
        Me.Controls.Add(Me.Label_Coded_String)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Button_Load)
        Me.Controls.Add(Me.Button_Save)
        Me.Controls.Add(Me.Label_Flight_Plan_Overview)
        Me.Controls.Add(Me.ListView_Overview)
        Me.Controls.Add(Me.Button_No)
        Me.Controls.Add(Me.Button_Yes)
        Me.Controls.Add(Me.Label_Sure)
        Me.Controls.Add(Me.Button_Instruction_Delete)
        Me.Controls.Add(Me.Button_Instruction_Replace)
        Me.Controls.Add(Me.Button_Instruction_Add)
        Me.Controls.Add(Me.TextBox_Instruction_6)
        Me.Controls.Add(Me.Label_Instruction_6)
        Me.Controls.Add(Me.TextBox_Instruction_5)
        Me.Controls.Add(Me.Label_Instruction_5)
        Me.Controls.Add(Me.TextBox_Instruction_4)
        Me.Controls.Add(Me.Label_Instruction_4)
        Me.Controls.Add(Me.TextBox_Instruction_3)
        Me.Controls.Add(Me.Label_Instruction_3)
        Me.Controls.Add(Me.TextBox_Instruction_2)
        Me.Controls.Add(Me.Label_Instruction_2)
        Me.Controls.Add(Me.Label_Instruction_Values)
        Me.Controls.Add(Me.TextBox_Instruction_1)
        Me.Controls.Add(Me.Label_Instruction_1)
        Me.Controls.Add(Me.ComboBox_Instruction_Type_2)
        Me.Controls.Add(Me.ComboBox_Instruction_Type_1)
        Me.Controls.Add(Me.Label_Instruction_Type)
        Me.Controls.Add(Me.NumericUpDown_Instruction_Number)
        Me.Controls.Add(Me.Label_Instruction_Number)
        Me.Name = "Create_Flight_Plan"
        Me.Text = "Create Flight Plan"
        CType(Me.NumericUpDown_Instruction_Number, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label_Instruction_Number As Label
    Friend WithEvents NumericUpDown_Instruction_Number As NumericUpDown
    Friend WithEvents Label_Instruction_Type As Label
    Friend WithEvents ComboBox_Instruction_Type_1 As ComboBox
    Friend WithEvents ComboBox_Instruction_Type_2 As ComboBox
    Friend WithEvents Label_Instruction_1 As Label
    Friend WithEvents TextBox_Instruction_1 As TextBox
    Friend WithEvents Label_Instruction_Values As Label
    Friend WithEvents TextBox_Instruction_2 As TextBox
    Friend WithEvents Label_Instruction_2 As Label
    Friend WithEvents TextBox_Instruction_4 As TextBox
    Friend WithEvents Label_Instruction_4 As Label
    Friend WithEvents TextBox_Instruction_3 As TextBox
    Friend WithEvents Label_Instruction_3 As Label
    Friend WithEvents TextBox_Instruction_6 As TextBox
    Friend WithEvents Label_Instruction_6 As Label
    Friend WithEvents TextBox_Instruction_5 As TextBox
    Friend WithEvents Label_Instruction_5 As Label
    Friend WithEvents Button_Instruction_Add As Button
    Friend WithEvents Button_Instruction_Replace As Button
    Friend WithEvents Button_Instruction_Delete As Button
    Friend WithEvents Label_Sure As Label
    Friend WithEvents Button_No As Button
    Friend WithEvents Button_Yes As Button
    Friend WithEvents ListView_Overview As ListView
    Friend WithEvents Label_Flight_Plan_Overview As Label
    Friend WithEvents Button_Save As Button
    Friend WithEvents Button_Load As Button
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label_Coded_String As Label
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents Button_New As Button
End Class
