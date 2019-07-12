Imports Microsoft.Office.Interop

Public Class CompareSplit

	Const ALHPA_CODE_INDEX		As Integer = 0
	Const REFDES_INDEX			As Integer = 1
	Const ALHPA_COMPONENT_INDEX As Integer = 7

	Const STOCK_ID				As String = "F8"

	Private Sub CompareSplit_Load() Handles MyBase.Load
		CenterToParent()
		Excel_Button.Enabled = false
		Print_Button.Enabled = false
	End Sub

	Private Sub BrowseMaster_Button_Click() Handles BrowseMaster_Button.Click
		Settings.OpenLocation(MasterPath_TextBox.Text, "Master File", MasterPath_TextBox.Text, "all|*.*", false)
	End Sub

	Private Sub BrowsePart1_Button_Click() Handles BrowsePart1_Button.Click
		Settings.OpenLocation(Part1Path_TextBox.Text, "Master File", Part1Path_TextBox.Text, "all|*.*", false)
	End Sub

	Private Sub BrowsePart2_Button_Click() Handles BrowsePart2_Button.Click
		Settings.OpenLocation(Part2Path_TextBox.Text, "Master File", Part2Path_TextBox.Text, "all|*.*", false)
	End Sub

	Private Sub Button_Full_Click() Handles Button_Full.Click
		Results_RichTextBox.Clear()

		Results_RichTextBox.Text &= "**********REFDES TEST**********" & vbNewLine
		RefDesTest(False)

		Results_RichTextBox.Text &= vbNewLine & "**********ITEM TEST**********" & vbNewLine
		ItemTest(False)

		Results_RichTextBox.Text &= vbNewLine & "**********X/Y TEST**********" & vbNewLine
		XYTest(False)
	End Sub

	Private Sub RefdesReport_Button_Click() Handles RefdesReport_Button.Click
		RefDesTest(True)
	End Sub

	Private sub RefDesTest(byref clear As boolean)
		' run our file validation
		If FileValidation() = False Then
			Return
		End If

		' clear our RichText
		If clear = True Then
			Results_RichTextBox.Clear()
		End If

		' set up our tables
		Dim masterTable As New DataTable
		masterTable.Columns.Add(DB_HEADER_REF_DES, GetType(String))
		masterTable.Columns.Add(DB_HEADER_ITEM_NUMBER, GetType(String))

		Dim testTable As DataTable = masterTable.Clone()

		' read each file and put into a table
		If AddRefdesToTable(MasterPath_TextBox.Text, masterTable) = False Then
			Return
		End If

		If AddRefdesToTable(Part1Path_TextBox.Text, testTable) = False Then
			Return
		End If
		If AddRefdesToTable(Part2Path_TextBox.Text, testTable) = False Then
			Return
		End If

		' sort the data so we can see it clearly
		masterTable.DefaultView.Sort = "[" & DB_HEADER_REF_DES & "] ASC"
		masterTable = masterTable.DefaultView.ToTable

		testTable.DefaultView.Sort = "[" & DB_HEADER_REF_DES & "] ASC"
		testTable = testTable.DefaultView.ToTable

		' compare what we have
		CompareTables(masterTable, testTable)

		Excel_Button.Enabled = True
		Print_Button.Enabled = True
	End sub

	Private Function FileValidation() As Boolean
		Dim result As String = ""

		If IO.File.Exists(MasterPath_TextBox.Text) = False Then
			result &= "Master Path is not valid." & vbNewLine
		End If

		If IO.File.Exists(Part1Path_TextBox.Text) = False Then
			result &= "Part 1 Path is not valid." & vbNewLine
		End If

		If IO.File.Exists(Part2Path_TextBox.Text) = False Then
			result &= "Part 2 Path is not valid." & vbNewLine
		End If

		If result.Length <> 0 Then
			MsgBox(result.Trim())
			Return False
		End If

		Return True
	End Function

	Private Function AddRefdesToTable(byref filePath As String, byref table As DataTable) As Boolean
		Dim lineNumber As Integer = 0

		Try
			Using myParser As New FileIO.TextFieldParser(filePath)
				myParser.TextFieldType = FileIO.FieldType.Delimited
				myParser.SetDelimiters(SPACE_DILIMITER)

				Dim currentRow As String()

				While Not myParser.EndOfData
					currentRow = myParser.ReadFields()

					If currentRow(ALHPA_CODE_INDEX).Equals(STOCK_ID) = False Then
						lineNumber += 1
						Continue While
					End If

					Dim stock As String = currentRow(ALHPA_COMPONENT_INDEX).ToString()

					currentRow = myParser.ReadFields()

					table.Rows.Add(currentRow(REFDES_INDEX), stock)

					lineNumber += 2
				End While
			End Using
		Catch ex As Exception
			MsgBox(filePath & vbNewLine & "Error possibly around line " & lineNumber & ": " & ex.Message)
			Return False
		End Try

		Return true
	End Function

	Private sub CompareTables(byref masterTable As DataTable, byref testTable As DataTable)
		' cycle through the master table
		For Each row As DataRow In masterTable.Rows
			Dim dr() As DataRow = testTable.Select("[" & DB_HEADER_REF_DES & "] = '" & row(0) & "'")

			If dr.Length = 0 Then
				Results_RichTextBox.Text &= String.Format("{0,-5}: {1,-30} was not found in either file.", row(0), row(1)) & vbNewLine
			Elseif 1 < dr.Length then
				Results_RichTextBox.Text &= String.Format("{0,-5}: {1,-30} was found multiple times.", row(0), row(1)) & vbNewLine
			End If
		Next

		' cycle through the test table
		For Each row As DataRow In testTable.Rows
			Dim dr() As DataRow = masterTable.Select("[" & DB_HEADER_REF_DES & "] = '" & row(0) & "'")

			If dr.Length = 0 Then
				Results_RichTextBox.Text &= row(0) & String.Format("{0,-5}: {1,-30} Extra.", row(0), row(1)) & vbNewLine
			Elseif 1 < dr.Length then
				Results_RichTextBox.Text &= row(0) & String.Format("{0,-5}: {1,-30} was found multiple times in master file.", row(0), row(1)) & vbNewLine
			End If
		Next

		' check to see if we had no issues
		If Results_RichTextBox.Text.Length = 0 Then
			Results_RichTextBox.Text = "Both files combined match the master file's Refdes."
		End If
	End sub

	Private Sub ItemReport_Button_Click() Handles ItemReport_Button.Click
		ItemTest(True)
	End Sub

	Private sub ItemTest(Byref clear As boolean)
		' run our file validation
		If FileValidation() = False Then
			Return
		End If

		' clear our RichText
		If clear = True Then
			Results_RichTextBox.Clear()
		End If
		
		' set up our tables
		Dim masterList As New List(Of String)

		Dim list1 As New List(Of String)
		Dim list2 As New List(Of String)

		' read each file and put into a table
		If AddItemToList(MasterPath_TextBox.Text, masterList) = False Then
			Return
		End If
		If AddItemToList(Part1Path_TextBox.Text, list1) = False Then
			Return
		End If
		If AddItemToList(Part2Path_TextBox.Text, list2) = False Then
			Return
		End If
		
		' compare what we have
		CompareLists(masterList, list1, list2)

		Excel_Button.Enabled = True
		Print_Button.Enabled = True
	End sub

	Private Function AddItemToList(byref filePath As String, byref list As List(Of String)) As Boolean
		Dim lineNumber As Integer = 0

		Try
			Using myParser As New FileIO.TextFieldParser(filePath)
				myParser.TextFieldType = FileIO.FieldType.Delimited
				myParser.SetDelimiters(SPACE_DILIMITER)

				Dim currentRow As String()

				While Not myParser.EndOfData
					currentRow = myParser.ReadFields()

					If currentRow(ALHPA_CODE_INDEX).Equals(STOCK_ID) = False Then
						lineNumber += 1
						Continue While
					End If

					Dim stock As String = currentRow(ALHPA_COMPONENT_INDEX).ToString()

					If list.Contains(stock) = false Then
						list.Add(stock)
					End If

					lineNumber += 1
				End While
			End Using
		Catch ex As Exception
			MsgBox(filePath & vbNewLine & "Error possibly around line " & lineNumber & ": " & ex.Message)
			Return False
		End Try

		Return true
	End Function

	Private sub CompareLists(byref masterList As List(Of String), byref list1 As List(Of String), byref list2 As List(Of String))
		For Each item In masterList
			If list1.Contains(item) = False And list2.Contains(item) = False Then
				Results_RichTextBox.Text &= String.Format("{0,-30} was not found in either file.", item) & vbNewLine
			Else If list1.Contains(item) = true And list2.Contains(item) = true Then
				Results_RichTextBox.Text &= String.Format("{0,-30} was found in both files.", item) & vbNewLine
			End If
		Next

		For Each item In list1
			If masterList.Contains(item) = False Then
				Results_RichTextBox.Text &= String.Format("{0,-30} is extra in Part 1", item) & vbNewLine
			End If
		Next

		For Each item In list2
			If masterList.Contains(item) = False Then
				Results_RichTextBox.Text &= String.Format("{0,-30} is extra in Part 2", item) & vbNewLine
			End If
		Next

		' check to see if we had no issues
		If Results_RichTextBox.Text.Length = 0 Then
			Results_RichTextBox.Text = "All parts are in their own machine."
		End If
	End sub

	Private Sub Button_XY_Click() Handles Button_XY.Click
		XYTest(True)
	End Sub

	Private Sub XYTest(ByRef clear As boolean)
		' run our file validation
		If FileValidation() = False Then
			Return
		End If

		Dim offsetX As Integer = 0
		Dim offsetY As Integer = 0

		Try
			offsetX = TextBox_X.Text
			offsetY = TextBox_Y.Text
		Catch ex As Exception
			MsgBox("Please put in a number.")
			Return
		End Try

		' clear our RichText
		If clear = True Then
			Results_RichTextBox.Clear()
		End If

		' set up our tables
		Dim masterTable As New DataTable
		masterTable.Columns.Add(DB_HEADER_REF_DES, GetType(String))
		masterTable.Columns.Add("X", GetType(Integer))
		masterTable.Columns.Add("Y", GetType(Integer))

		Dim testTable As DataTable = masterTable.Clone()

		' read each file and put into a table
		If AddRefdesToTable(MasterPath_TextBox.Text, masterTable) = False Then
			Return
		End If

		If AddRefdesToTable(Part1Path_TextBox.Text, testTable) = False Then
			Return
		End If
		If AddRefdesToTable(Part2Path_TextBox.Text, testTable) = False Then
			Return
		End If

		' sort the data so we can see it clearly
		masterTable.DefaultView.Sort = "[" & DB_HEADER_REF_DES & "] ASC"
		masterTable = masterTable.DefaultView.ToTable

		testTable.DefaultView.Sort = "[" & DB_HEADER_REF_DES & "] ASC"
		testTable = testTable.DefaultView.ToTable

		' compare what we have
		CompareTables(masterTable, testTable)

		Excel_Button.Enabled = True
		Print_Button.Enabled = True
	End Sub
	
	Private Sub Excel_Button_Click() Handles Excel_Button.Click
		Dim report As new GenerateReport
		report.RichTextToExcel(Results_RichTextBox)
	End Sub

	Private Sub Print_Button_Click() Handles Print_Button.Click
		' Get the Word application object.
		Dim word_app As Word._Application = New Word.ApplicationClass()

		'Set Printer
		Dim p As New PrintDialog
		p.UseEXDialog = True
		If p.ShowDialog = Windows.Forms.DialogResult.OK Then
			word_app.WordBasic.FilePrintSetup(Printer:=p.PrinterSettings.PrinterName, DoNotSetAsSysDefault:=1)
			word_app.ActivePrinter = p.PrinterSettings.PrinterName

			' Make Word visible (optional).
			word_app.Visible = true

			' Create the Word document.
			Dim word_doc As Word._Document = word_app.Documents.Add()
			word_doc.Paragraphs.SpaceAfter = 0
			word_doc.Paragraphs.SpaceBefore = 0

			Dim para As Word.Paragraph = word_doc.Paragraphs.Add()

			para.Range.Font.Name = "Consolas"
			para.Range.Font.Size = 11
			para.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
			para.LineSpacing = 1.0
			para.Range.Text = Results_RichTextBox.Text

			Try
				word_app.PrintOut(Background:=False)
			Catch ex As Exception
				MsgBox(ex.Message)
			End Try

			' Close.
			Dim save_changes As Object = False
			word_doc.Close(save_changes)
			word_app.Quit(save_changes)

			'Release 
			word_doc = Nothing
			word_app = Nothing
		End If
	End Sub

	Private Sub Close_Button_Click() Handles Close_Button.Click
		Close()
	End Sub

	
End Class