<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CompareSplit
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
		Me.Close_Button = New System.Windows.Forms.Button()
		Me.BrowseMaster_Button = New System.Windows.Forms.Button()
		Me.BrowsePart1_Button = New System.Windows.Forms.Button()
		Me.BrowsePart2_Button = New System.Windows.Forms.Button()
		Me.Excel_Button = New System.Windows.Forms.Button()
		Me.MasterPath_TextBox = New System.Windows.Forms.TextBox()
		Me.Part1Path_TextBox = New System.Windows.Forms.TextBox()
		Me.Part2Path_TextBox = New System.Windows.Forms.TextBox()
		Me.RefdesReport_Button = New System.Windows.Forms.Button()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Results_RichTextBox = New System.Windows.Forms.RichTextBox()
		Me.ItemReport_Button = New System.Windows.Forms.Button()
		Me.Print_Button = New System.Windows.Forms.Button()
		Me.Button_Full = New System.Windows.Forms.Button()
		Me.Button_XY = New System.Windows.Forms.Button()
		Me.TextBox_X = New System.Windows.Forms.TextBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.TextBox_Y = New System.Windows.Forms.TextBox()
		Me.SuspendLayout
		'
		'Close_Button
		'
		Me.Close_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.Close_Button.AutoSize = true
		Me.Close_Button.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.Close_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!)
		Me.Close_Button.Location = New System.Drawing.Point(809, 139)
		Me.Close_Button.Name = "Close_Button"
		Me.Close_Button.Size = New System.Drawing.Size(59, 30)
		Me.Close_Button.TabIndex = 51
		Me.Close_Button.Text = "Close"
		Me.Close_Button.UseVisualStyleBackColor = true
		'
		'BrowseMaster_Button
		'
		Me.BrowseMaster_Button.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.BrowseMaster_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!)
		Me.BrowseMaster_Button.Location = New System.Drawing.Point(12, 31)
		Me.BrowseMaster_Button.Name = "BrowseMaster_Button"
		Me.BrowseMaster_Button.Size = New System.Drawing.Size(72, 30)
		Me.BrowseMaster_Button.TabIndex = 52
		Me.BrowseMaster_Button.Text = "Master"
		Me.BrowseMaster_Button.UseVisualStyleBackColor = true
		'
		'BrowsePart1_Button
		'
		Me.BrowsePart1_Button.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.BrowsePart1_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!)
		Me.BrowsePart1_Button.Location = New System.Drawing.Point(12, 67)
		Me.BrowsePart1_Button.Name = "BrowsePart1_Button"
		Me.BrowsePart1_Button.Size = New System.Drawing.Size(72, 30)
		Me.BrowsePart1_Button.TabIndex = 53
		Me.BrowsePart1_Button.Text = "Part 1"
		Me.BrowsePart1_Button.UseVisualStyleBackColor = true
		'
		'BrowsePart2_Button
		'
		Me.BrowsePart2_Button.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.BrowsePart2_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!)
		Me.BrowsePart2_Button.Location = New System.Drawing.Point(12, 103)
		Me.BrowsePart2_Button.Name = "BrowsePart2_Button"
		Me.BrowsePart2_Button.Size = New System.Drawing.Size(72, 30)
		Me.BrowsePart2_Button.TabIndex = 54
		Me.BrowsePart2_Button.Text = "Part 2"
		Me.BrowsePart2_Button.UseVisualStyleBackColor = true
		'
		'Excel_Button
		'
		Me.Excel_Button.AutoSize = true
		Me.Excel_Button.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.Excel_Button.Enabled = false
		Me.Excel_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!)
		Me.Excel_Button.Location = New System.Drawing.Point(637, 139)
		Me.Excel_Button.Name = "Excel_Button"
		Me.Excel_Button.Size = New System.Drawing.Size(109, 30)
		Me.Excel_Button.TabIndex = 56
		Me.Excel_Button.Text = "Create Excel"
		Me.Excel_Button.UseVisualStyleBackColor = true
		'
		'MasterPath_TextBox
		'
		Me.MasterPath_TextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.MasterPath_TextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.MasterPath_TextBox.Location = New System.Drawing.Point(90, 33)
		Me.MasterPath_TextBox.Name = "MasterPath_TextBox"
		Me.MasterPath_TextBox.Size = New System.Drawing.Size(778, 26)
		Me.MasterPath_TextBox.TabIndex = 55
		Me.MasterPath_TextBox.Text = "\\Server1\Shares\Production\AlphaBackup"
		'
		'Part1Path_TextBox
		'
		Me.Part1Path_TextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.Part1Path_TextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Part1Path_TextBox.Location = New System.Drawing.Point(90, 69)
		Me.Part1Path_TextBox.Name = "Part1Path_TextBox"
		Me.Part1Path_TextBox.Size = New System.Drawing.Size(778, 26)
		Me.Part1Path_TextBox.TabIndex = 57
		Me.Part1Path_TextBox.Text = "\\Server1\Shares\Production\AlphaBackup"
		'
		'Part2Path_TextBox
		'
		Me.Part2Path_TextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.Part2Path_TextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Part2Path_TextBox.Location = New System.Drawing.Point(90, 105)
		Me.Part2Path_TextBox.Name = "Part2Path_TextBox"
		Me.Part2Path_TextBox.Size = New System.Drawing.Size(778, 26)
		Me.Part2Path_TextBox.TabIndex = 58
		Me.Part2Path_TextBox.Text = "\\Server1\Shares\Production\AlphaBackup"
		'
		'RefdesReport_Button
		'
		Me.RefdesReport_Button.AutoSize = true
		Me.RefdesReport_Button.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.RefdesReport_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!)
		Me.RefdesReport_Button.Location = New System.Drawing.Point(117, 139)
		Me.RefdesReport_Button.Name = "RefdesReport_Button"
		Me.RefdesReport_Button.Size = New System.Drawing.Size(124, 30)
		Me.RefdesReport_Button.TabIndex = 59
		Me.RefdesReport_Button.Text = "Refdes Report"
		Me.RefdesReport_Button.UseVisualStyleBackColor = true
		'
		'Label3
		'
		Me.Label3.AutoSize = true
		Me.Label3.Font = New System.Drawing.Font("Consolas", 12!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline),System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Label3.Location = New System.Drawing.Point(10, 9)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(126, 19)
		Me.Label3.TabIndex = 60
		Me.Label3.Text = "Compare Split"
		'
		'Results_RichTextBox
		'
		Me.Results_RichTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.Results_RichTextBox.Font = New System.Drawing.Font("Consolas", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Results_RichTextBox.Location = New System.Drawing.Point(14, 175)
		Me.Results_RichTextBox.Name = "Results_RichTextBox"
		Me.Results_RichTextBox.Size = New System.Drawing.Size(854, 233)
		Me.Results_RichTextBox.TabIndex = 61
		Me.Results_RichTextBox.Text = ""
		'
		'ItemReport_Button
		'
		Me.ItemReport_Button.AutoSize = true
		Me.ItemReport_Button.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.ItemReport_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!)
		Me.ItemReport_Button.Location = New System.Drawing.Point(247, 139)
		Me.ItemReport_Button.Name = "ItemReport_Button"
		Me.ItemReport_Button.Size = New System.Drawing.Size(104, 30)
		Me.ItemReport_Button.TabIndex = 62
		Me.ItemReport_Button.Text = "Item Report"
		Me.ItemReport_Button.UseVisualStyleBackColor = true
		'
		'Print_Button
		'
		Me.Print_Button.AutoSize = true
		Me.Print_Button.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.Print_Button.Enabled = false
		Me.Print_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!)
		Me.Print_Button.Location = New System.Drawing.Point(752, 139)
		Me.Print_Button.Name = "Print_Button"
		Me.Print_Button.Size = New System.Drawing.Size(51, 30)
		Me.Print_Button.TabIndex = 63
		Me.Print_Button.Text = "Print"
		Me.Print_Button.UseVisualStyleBackColor = true
		'
		'Button_Full
		'
		Me.Button_Full.AutoSize = true
		Me.Button_Full.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.Button_Full.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!)
		Me.Button_Full.Location = New System.Drawing.Point(14, 139)
		Me.Button_Full.Name = "Button_Full"
		Me.Button_Full.Size = New System.Drawing.Size(97, 30)
		Me.Button_Full.TabIndex = 64
		Me.Button_Full.Text = "Full Report"
		Me.Button_Full.UseVisualStyleBackColor = true
		'
		'Button_XY
		'
		Me.Button_XY.AutoSize = true
		Me.Button_XY.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.Button_XY.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!)
		Me.Button_XY.Location = New System.Drawing.Point(357, 139)
		Me.Button_XY.Name = "Button_XY"
		Me.Button_XY.Size = New System.Drawing.Size(98, 30)
		Me.Button_XY.TabIndex = 65
		Me.Button_XY.Text = "X/Y Report"
		Me.Button_XY.UseVisualStyleBackColor = true
		'
		'TextBox_X
		'
		Me.TextBox_X.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.TextBox_X.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.TextBox_X.Location = New System.Drawing.Point(485, 141)
		Me.TextBox_X.Name = "TextBox_X"
		Me.TextBox_X.Size = New System.Drawing.Size(42, 26)
		Me.TextBox_X.TabIndex = 66
		'
		'Label1
		'
		Me.Label1.AutoSize = true
		Me.Label1.Font = New System.Drawing.Font("Consolas", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Label1.Location = New System.Drawing.Point(461, 145)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(27, 19)
		Me.Label1.TabIndex = 67
		Me.Label1.Text = "X:"
		'
		'Label2
		'
		Me.Label2.AutoSize = true
		Me.Label2.Font = New System.Drawing.Font("Consolas", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Label2.Location = New System.Drawing.Point(533, 145)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(27, 19)
		Me.Label2.TabIndex = 69
		Me.Label2.Text = "Y:"
		'
		'TextBox_Y
		'
		Me.TextBox_Y.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.TextBox_Y.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.TextBox_Y.Location = New System.Drawing.Point(557, 141)
		Me.TextBox_Y.Name = "TextBox_Y"
		Me.TextBox_Y.Size = New System.Drawing.Size(42, 26)
		Me.TextBox_Y.TabIndex = 68
		'
		'CompareSplit
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(884, 419)
		Me.Controls.Add(Me.TextBox_Y)
		Me.Controls.Add(Me.TextBox_X)
		Me.Controls.Add(Me.Button_XY)
		Me.Controls.Add(Me.Button_Full)
		Me.Controls.Add(Me.Print_Button)
		Me.Controls.Add(Me.ItemReport_Button)
		Me.Controls.Add(Me.Results_RichTextBox)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.RefdesReport_Button)
		Me.Controls.Add(Me.Part2Path_TextBox)
		Me.Controls.Add(Me.Part1Path_TextBox)
		Me.Controls.Add(Me.Excel_Button)
		Me.Controls.Add(Me.MasterPath_TextBox)
		Me.Controls.Add(Me.BrowsePart2_Button)
		Me.Controls.Add(Me.BrowsePart1_Button)
		Me.Controls.Add(Me.BrowseMaster_Button)
		Me.Controls.Add(Me.Close_Button)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.Label1)
		Me.Name = "CompareSplit"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Compare Split"
		Me.ResumeLayout(false)
		Me.PerformLayout

End Sub

	Friend WithEvents Close_Button As Button
	Friend WithEvents BrowseMaster_Button As Button
	Friend WithEvents BrowsePart1_Button As Button
	Friend WithEvents BrowsePart2_Button As Button
	Friend WithEvents Excel_Button As Button
	Friend WithEvents MasterPath_TextBox As TextBox
	Friend WithEvents Part1Path_TextBox As TextBox
	Friend WithEvents Part2Path_TextBox As TextBox
	Friend WithEvents RefdesReport_Button As Button
	Friend WithEvents Label3 As Label
	Friend WithEvents Results_RichTextBox As RichTextBox
	Friend WithEvents ItemReport_Button As Button
	Friend WithEvents Print_Button As Button
	Friend WithEvents Button_Full As Button
	Friend WithEvents Button_XY As Button
	Friend WithEvents TextBox_X As TextBox
	Friend WithEvents Label1 As Label
	Friend WithEvents Label2 As Label
	Friend WithEvents TextBox_Y As TextBox
End Class
