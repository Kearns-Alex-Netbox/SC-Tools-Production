<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MessageBoxQB
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
		Me.Message_RichTextBox = New System.Windows.Forms.RichTextBox()
		Me.One_Button = New System.Windows.Forms.Button()
		Me.Two_Button = New System.Windows.Forms.Button()
		Me.Three_Button = New System.Windows.Forms.Button()
		Me.Remind_CheckBox = New System.Windows.Forms.CheckBox()
		Me.SuspendLayout()
		'
		'Message_RichTextBox
		'
		Me.Message_RichTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Message_RichTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Message_RichTextBox.Location = New System.Drawing.Point(13, 13)
		Me.Message_RichTextBox.Name = "Message_RichTextBox"
		Me.Message_RichTextBox.ReadOnly = True
		Me.Message_RichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical
		Me.Message_RichTextBox.Size = New System.Drawing.Size(420, 207)
		Me.Message_RichTextBox.TabIndex = 4
		Me.Message_RichTextBox.TabStop = False
		Me.Message_RichTextBox.Text = ""
		'
		'One_Button
		'
		Me.One_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.One_Button.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.One_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
		Me.One_Button.Location = New System.Drawing.Point(145, 226)
		Me.One_Button.Name = "One_Button"
		Me.One_Button.Size = New System.Drawing.Size(92, 30)
		Me.One_Button.TabIndex = 0
		Me.One_Button.Text = "One"
		Me.One_Button.UseVisualStyleBackColor = True
		'
		'Two_Button
		'
		Me.Two_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.Two_Button.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.Two_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
		Me.Two_Button.Location = New System.Drawing.Point(243, 226)
		Me.Two_Button.Name = "Two_Button"
		Me.Two_Button.Size = New System.Drawing.Size(92, 30)
		Me.Two_Button.TabIndex = 1
		Me.Two_Button.Text = "Two"
		Me.Two_Button.UseVisualStyleBackColor = True
		'
		'Three_Button
		'
		Me.Three_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.Three_Button.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.Three_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
		Me.Three_Button.Location = New System.Drawing.Point(341, 226)
		Me.Three_Button.Name = "Three_Button"
		Me.Three_Button.Size = New System.Drawing.Size(92, 30)
		Me.Three_Button.TabIndex = 2
		Me.Three_Button.Text = "Three"
		Me.Three_Button.UseVisualStyleBackColor = True
		'
		'Remind_CheckBox
		'
		Me.Remind_CheckBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.Remind_CheckBox.AutoSize = True
		Me.Remind_CheckBox.Checked = True
		Me.Remind_CheckBox.CheckState = System.Windows.Forms.CheckState.Checked
		Me.Remind_CheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Remind_CheckBox.Location = New System.Drawing.Point(13, 224)
		Me.Remind_CheckBox.Name = "Remind_CheckBox"
		Me.Remind_CheckBox.Size = New System.Drawing.Size(125, 34)
		Me.Remind_CheckBox.TabIndex = 3
		Me.Remind_CheckBox.Text = "Remind Me Again" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(This Session)"
		Me.Remind_CheckBox.UseVisualStyleBackColor = True
		'
		'MessageBoxQB
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(445, 263)
		Me.ControlBox = False
		Me.Controls.Add(Me.Remind_CheckBox)
		Me.Controls.Add(Me.Three_Button)
		Me.Controls.Add(Me.Two_Button)
		Me.Controls.Add(Me.One_Button)
		Me.Controls.Add(Me.Message_RichTextBox)
		Me.Name = "MessageBoxQB"
		Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Message Box"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents Message_RichTextBox As RichTextBox
	Friend WithEvents One_Button As Button
	Friend WithEvents Two_Button As Button
	Friend WithEvents Three_Button As Button
	Friend WithEvents Remind_CheckBox As CheckBox
End Class
