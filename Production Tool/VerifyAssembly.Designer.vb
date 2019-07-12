<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VerifyAssembly
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
		Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Excel_Button = New System.Windows.Forms.Button()
		Me.GenerateReport_Button = New System.Windows.Forms.Button()
		Me.Close_Button = New System.Windows.Forms.Button()
		Me.DGV_ALPHA_Extra = New System.Windows.Forms.DataGridView()
		Me.DGV_ALPHA_Missing = New System.Windows.Forms.DataGridView()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.L_ALPHA_PCAD = New System.Windows.Forms.Label()
		Me.RefNum_NumericUpDown = New System.Windows.Forms.NumericUpDown()
		CType(Me.DGV_ALPHA_Extra,System.ComponentModel.ISupportInitialize).BeginInit
		CType(Me.DGV_ALPHA_Missing,System.ComponentModel.ISupportInitialize).BeginInit
		CType(Me.RefNum_NumericUpDown,System.ComponentModel.ISupportInitialize).BeginInit
		Me.SuspendLayout
		'
		'Label3
		'
		Me.Label3.AutoSize = true
		Me.Label3.Font = New System.Drawing.Font("Consolas", 12!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline),System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Label3.Location = New System.Drawing.Point(10, 9)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(153, 19)
		Me.Label3.TabIndex = 45
		Me.Label3.Text = "Verrify Assembly"
		'
		'Label1
		'
		Me.Label1.AutoSize = true
		Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Label1.Location = New System.Drawing.Point(10, 34)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(106, 20)
		Me.Label1.TabIndex = 46
		Me.Label1.Text = "Build Ref. No."
		'
		'Excel_Button
		'
		Me.Excel_Button.AutoSize = true
		Me.Excel_Button.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.Excel_Button.Enabled = false
		Me.Excel_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!)
		Me.Excel_Button.Location = New System.Drawing.Point(365, 29)
		Me.Excel_Button.Name = "Excel_Button"
		Me.Excel_Button.Size = New System.Drawing.Size(109, 30)
		Me.Excel_Button.TabIndex = 49
		Me.Excel_Button.Text = "Create Excel"
		Me.Excel_Button.UseVisualStyleBackColor = true
		'
		'GenerateReport_Button
		'
		Me.GenerateReport_Button.AutoSize = true
		Me.GenerateReport_Button.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.GenerateReport_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!)
		Me.GenerateReport_Button.Location = New System.Drawing.Point(219, 29)
		Me.GenerateReport_Button.Name = "GenerateReport_Button"
		Me.GenerateReport_Button.Size = New System.Drawing.Size(140, 30)
		Me.GenerateReport_Button.TabIndex = 48
		Me.GenerateReport_Button.Text = "Generate Report"
		Me.GenerateReport_Button.UseVisualStyleBackColor = true
		'
		'Close_Button
		'
		Me.Close_Button.AutoSize = true
		Me.Close_Button.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.Close_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!)
		Me.Close_Button.Location = New System.Drawing.Point(480, 29)
		Me.Close_Button.Name = "Close_Button"
		Me.Close_Button.Size = New System.Drawing.Size(59, 30)
		Me.Close_Button.TabIndex = 50
		Me.Close_Button.Text = "Close"
		Me.Close_Button.UseVisualStyleBackColor = true
		'
		'DGV_ALPHA_Extra
		'
		Me.DGV_ALPHA_Extra.AllowUserToAddRows = false
		Me.DGV_ALPHA_Extra.AllowUserToDeleteRows = false
		DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer))
		Me.DGV_ALPHA_Extra.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
		Me.DGV_ALPHA_Extra.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
		Me.DGV_ALPHA_Extra.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
		Me.DGV_ALPHA_Extra.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
		Me.DGV_ALPHA_Extra.BackgroundColor = System.Drawing.SystemColors.Control
		Me.DGV_ALPHA_Extra.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
		DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
		DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
		DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
		Me.DGV_ALPHA_Extra.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
		Me.DGV_ALPHA_Extra.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
		DataGridViewCellStyle3.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
		DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
		DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
		Me.DGV_ALPHA_Extra.DefaultCellStyle = DataGridViewCellStyle3
		Me.DGV_ALPHA_Extra.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2
		Me.DGV_ALPHA_Extra.Location = New System.Drawing.Point(496, 85)
		Me.DGV_ALPHA_Extra.Name = "DGV_ALPHA_Extra"
		DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
		DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
		DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
		DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
		Me.DGV_ALPHA_Extra.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
		Me.DGV_ALPHA_Extra.Size = New System.Drawing.Size(476, 320)
		Me.DGV_ALPHA_Extra.TabIndex = 53
		'
		'DGV_ALPHA_Missing
		'
		Me.DGV_ALPHA_Missing.AllowUserToAddRows = false
		Me.DGV_ALPHA_Missing.AllowUserToDeleteRows = false
		DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer))
		Me.DGV_ALPHA_Missing.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
		Me.DGV_ALPHA_Missing.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
		Me.DGV_ALPHA_Missing.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
		Me.DGV_ALPHA_Missing.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
		Me.DGV_ALPHA_Missing.BackgroundColor = System.Drawing.SystemColors.Control
		Me.DGV_ALPHA_Missing.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
		DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
		DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
		DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
		Me.DGV_ALPHA_Missing.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle6
		Me.DGV_ALPHA_Missing.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window
		DataGridViewCellStyle7.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText
		DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
		DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
		Me.DGV_ALPHA_Missing.DefaultCellStyle = DataGridViewCellStyle7
		Me.DGV_ALPHA_Missing.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2
		Me.DGV_ALPHA_Missing.Location = New System.Drawing.Point(14, 85)
		Me.DGV_ALPHA_Missing.Name = "DGV_ALPHA_Missing"
		DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control
		DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText
		DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
		DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
		Me.DGV_ALPHA_Missing.RowHeadersDefaultCellStyle = DataGridViewCellStyle8
		Me.DGV_ALPHA_Missing.Size = New System.Drawing.Size(476, 320)
		Me.DGV_ALPHA_Missing.TabIndex = 51
		'
		'Label5
		'
		Me.Label5.AutoSize = true
		Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Label5.Location = New System.Drawing.Point(10, 62)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(309, 20)
		Me.Label5.TabIndex = 52
		Me.Label5.Text = "Components Assmebly, not in ALPHA"
		'
		'L_ALPHA_PCAD
		'
		Me.L_ALPHA_PCAD.AutoSize = true
		Me.L_ALPHA_PCAD.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.L_ALPHA_PCAD.Location = New System.Drawing.Point(492, 62)
		Me.L_ALPHA_PCAD.Name = "L_ALPHA_PCAD"
		Me.L_ALPHA_PCAD.Size = New System.Drawing.Size(328, 20)
		Me.L_ALPHA_PCAD.TabIndex = 54
		Me.L_ALPHA_PCAD.Text = "Components in ALPHA, not in Assembly"
		'
		'RefNum_NumericUpDown
		'
		Me.RefNum_NumericUpDown.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.RefNum_NumericUpDown.Location = New System.Drawing.Point(122, 32)
		Me.RefNum_NumericUpDown.Maximum = New Decimal(New Integer() {100000, 0, 0, 0})
		Me.RefNum_NumericUpDown.Name = "RefNum_NumericUpDown"
		Me.RefNum_NumericUpDown.Size = New System.Drawing.Size(91, 26)
		Me.RefNum_NumericUpDown.TabIndex = 55
		Me.RefNum_NumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
		'
		'VerifyAssembly
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(982, 417)
		Me.Controls.Add(Me.RefNum_NumericUpDown)
		Me.Controls.Add(Me.DGV_ALPHA_Extra)
		Me.Controls.Add(Me.DGV_ALPHA_Missing)
		Me.Controls.Add(Me.Label5)
		Me.Controls.Add(Me.L_ALPHA_PCAD)
		Me.Controls.Add(Me.Excel_Button)
		Me.Controls.Add(Me.GenerateReport_Button)
		Me.Controls.Add(Me.Close_Button)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.Label3)
		Me.Name = "VerifyAssembly"
		Me.Text = "VerifyAssembly"
		CType(Me.DGV_ALPHA_Extra,System.ComponentModel.ISupportInitialize).EndInit
		CType(Me.DGV_ALPHA_Missing,System.ComponentModel.ISupportInitialize).EndInit
		CType(Me.RefNum_NumericUpDown,System.ComponentModel.ISupportInitialize).EndInit
		Me.ResumeLayout(false)
		Me.PerformLayout

End Sub

	Friend WithEvents Label3 As Label
	Friend WithEvents Label1 As Label
	Friend WithEvents Excel_Button As Button
	Friend WithEvents GenerateReport_Button As Button
	Friend WithEvents Close_Button As Button
	Friend WithEvents DGV_ALPHA_Extra As DataGridView
	Friend WithEvents DGV_ALPHA_Missing As DataGridView
	Friend WithEvents Label5 As Label
	Friend WithEvents L_ALPHA_PCAD As Label
	Friend WithEvents RefNum_NumericUpDown As NumericUpDown
End Class
