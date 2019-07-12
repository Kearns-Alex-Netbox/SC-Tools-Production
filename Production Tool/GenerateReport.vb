'-----------------------------------------------------------------------------------------------------------------------------------------
' Module: GenerateReport.vb
'
' Description: Generates each report through Excel with special formatting.
'
'-----------------------------------------------------------------------------------------------------------------------------------------
Imports System.IO
Imports Microsoft.Office.Interop

Public Class GenerateReport

	Public Sub GenerateImportReport(ByRef list As RichTextBox)
		Try
			Dim xlApp As New Excel.Application
			Dim xlWorkBook As Excel.Workbook
			Dim xlWorkSheet As Excel.Worksheet
			Dim misValue As Object = Reflection.Missing.Value
			Dim INDEX_row As Integer = 1
			Dim INDEX_column As Integer = 1

			xlWorkBook = xlApp.Workbooks.Add(misValue)
			xlWorkSheet = xlWorkBook.Sheets("sheet1")

			xlWorkSheet.PageSetup.CenterHeader = "Import Output Report   " & Date.Now

			For Each line In list.Lines
				xlWorkSheet.Cells(INDEX_row, 1) = line
				INDEX_row += 1
			Next

			xlWorkSheet.Range("A1:X1").EntireColumn.AutoFit()
			xlWorkSheet.Range("A1:X1").EntireColumn.NumberFormat = "0"
			xlWorkSheet.Range("A1:X1").EntireColumn.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft

			xlApp.DisplayAlerts = False
			xlApp.Visible = True

			releaseObject(xlWorkSheet)
			releaseObject(xlWorkBook)
			releaseObject(xlApp)
		Catch ex As Exception
			MsgBox("File was not written: " & ex.Message)
		End Try
	End Sub

	Public Sub CompareAllReport(ByRef name As String,
								ByRef ALPHA_Missing As DataTable, ByRef ALPHA_Extra As DataTable,
								ByRef QB_Missing As DataTable, ByRef QB_Extra As DataTable,
								ByRef QB_Missing2 As DataTable, ByRef QB_Extra2 As DataTable,
								ByRef PCAD_Quantity As DataTable, ByRef QB_Quantity As DataTable,
								ByRef ALPHA_Quantity As DataTable, ByRef QB_Quantity2 As DataTable)
		Try
			Dim xlApp As New Excel.Application
			Dim xlWorkBook As Excel.Workbook
			Dim xlWorkSheet As Excel.Worksheet
			Dim misValue As Object = Reflection.Missing.Value
			xlWorkBook = xlApp.Workbooks.Add(misValue)

			'----- SHEET 1 -----'

			xlWorkSheet = xlWorkBook.Sheets("sheet1")
			xlWorkSheet.Name = "PCAD-ALPHA"
			xlWorkSheet.PageSetup.CenterHeader = "Difference Report [PCAD/ALPHA] for: " & name & "   " & Date.Now.Date

			'ROW 1
			Dim INDEX_row As Integer = 1
			Dim INDEX_Column As Integer = 1

			'ROW 2
			INDEX_row += 1

			For Each header In ALPHA_Missing.Columns
				xlWorkSheet.Cells(INDEX_row, INDEX_Column) = header.columnName
				INDEX_Column += 1
			Next

			xlWorkSheet.Cells(1, 1) = "ALPHA Missing parts [In PCAD Not in ALPHA]"
			xlWorkSheet.Range(xlWorkSheet.Cells(1, 1), xlWorkSheet.Cells(1, INDEX_Column - 1)).MergeCells = True

			INDEX_Column += 1
			Dim nextColumn2 = INDEX_Column

			For Each header In ALPHA_Extra.Columns
				xlWorkSheet.Cells(INDEX_row, INDEX_Column) = header.columnName
				INDEX_Column += 1
			Next

			xlWorkSheet.Cells(1, nextColumn2) = "ALPHA Extra parts [In ALPHA Not in PCAD]"
			xlWorkSheet.Range(xlWorkSheet.Cells(1, nextColumn2), xlWorkSheet.Cells(1, INDEX_Column - 1)).MergeCells = True

			'ROW 3
			INDEX_row += 1
			INDEX_Column = 1

			For row = 0 To ALPHA_Missing.Rows.Count - 1
				For column = 0 To ALPHA_Missing.Columns.Count - 1
					xlWorkSheet.Cells(INDEX_row, INDEX_Column) = ALPHA_Missing(row)(column)
					INDEX_Column += 1
				Next
				INDEX_Column = 1
				INDEX_row += 1
			Next

			INDEX_Column = nextColumn2
			INDEX_row = 3

			For row = 0 To ALPHA_Extra.Rows.Count - 1
				For column = 0 To ALPHA_Extra.Columns.Count - 1
					xlWorkSheet.Cells(INDEX_row, INDEX_Column) = ALPHA_Extra(row)(column)
					INDEX_Column += 1
				Next
				INDEX_Column = nextColumn2
				INDEX_row += 1
			Next

			xlWorkSheet.Range("A1:X1").EntireColumn.AutoFit()
			xlWorkSheet.Range("A1:X1").EntireColumn.NumberFormat = "0"
			xlWorkSheet.Range("A1:A2").EntireRow.Font.Bold = True
			xlWorkSheet.Range("A1:X1").EntireColumn.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft

			'----- SHEET 2 -----'

			xlWorkSheet = xlWorkBook.Sheets("sheet2")
			xlWorkSheet.Name = "PCAD-QB"
			xlWorkSheet.PageSetup.CenterHeader = "Difference Report [PCAD/QB] for: " & name & "   " & Date.Now.Date

			'ROW 1
			INDEX_row = 1
			INDEX_Column = 1

			'ROW 2
			INDEX_row += 1

			For Each header In QB_Missing.Columns
				xlWorkSheet.Cells(INDEX_row, INDEX_Column) = header.columnName
				INDEX_Column += 1
			Next

			xlWorkSheet.Cells(1, 1) = "QB Missing parts [In PCAD Not in QB]"
			xlWorkSheet.Range(xlWorkSheet.Cells(1, 1), xlWorkSheet.Cells(1, INDEX_Column - 1)).MergeCells = True

			INDEX_Column += 1
			nextColumn2 = INDEX_Column

			For Each header In QB_Extra.Columns
				xlWorkSheet.Cells(1, nextColumn2) = header.columnName
				INDEX_Column += 1
			Next

			xlWorkSheet.Cells(1, nextColumn2) = "QB Extra parts [In QB Not in PCAD]"
			xlWorkSheet.Range(xlWorkSheet.Cells(1, nextColumn2), xlWorkSheet.Cells(1, INDEX_Column - 1)).MergeCells = True

			'ROW 3
			INDEX_row += 1
			INDEX_Column = 1

			For row = 0 To QB_Missing.Rows.Count - 1
				For column = 0 To QB_Missing.Columns.Count - 1
					xlWorkSheet.Cells(INDEX_row, INDEX_Column) = QB_Missing(row)(column)
					INDEX_Column += 1
				Next
				INDEX_Column = 1
				INDEX_row += 1
			Next

			INDEX_Column = nextColumn2
			INDEX_row = 3

			For row = 0 To QB_Extra.Rows.Count - 1
				For column = 0 To QB_Extra.Columns.Count - 1
					xlWorkSheet.Cells(INDEX_row, INDEX_Column) = QB_Extra(row)(column)
					INDEX_Column += 1
				Next
				INDEX_Column = nextColumn2
				INDEX_row += 1
			Next

			xlWorkSheet.Range("A1:X1").EntireColumn.AutoFit()
			xlWorkSheet.Range("A1:X1").EntireColumn.NumberFormat = "0"
			xlWorkSheet.Range("A1:A2").EntireRow.Font.Bold = True
			xlWorkSheet.Range("A1:X1").EntireColumn.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft

			'----- SHEET 3 -----'

			xlWorkSheet = xlWorkBook.Sheets("sheet3")
			xlWorkSheet.Name = "ALPHA-QB"
			xlWorkSheet.PageSetup.CenterHeader = "Difference Report [ALPHA/QB] for: " & name & "   " & Date.Now.Date

			'ROW 1
			INDEX_row = 1
			INDEX_Column = 1

			'ROW 2
			INDEX_row += 1

			For Each header In QB_Missing2.Columns
				xlWorkSheet.Cells(INDEX_row, INDEX_Column) = header.columnName
				INDEX_Column += 1
			Next

			xlWorkSheet.Cells(1, 1) = "QB Missing parts [In ALPHA Not in QB]"
			xlWorkSheet.Range(xlWorkSheet.Cells(1, 1), xlWorkSheet.Cells(1, INDEX_Column - 1)).MergeCells = True

			INDEX_Column += 1
			nextColumn2 = INDEX_Column

			For Each header In QB_Extra2.Columns
				xlWorkSheet.Cells(INDEX_row, INDEX_Column) = header.columnName
				INDEX_Column += 1
			Next

			xlWorkSheet.Cells(1, nextColumn2) = "QB Extra parts [In QB Not in ALPHA]"
			xlWorkSheet.Range(xlWorkSheet.Cells(1, nextColumn2), xlWorkSheet.Cells(1, INDEX_Column - 1)).MergeCells = True

			'ROW 3
			INDEX_row += 1
			INDEX_Column = 1

			For row = 0 To QB_Missing2.Rows.Count - 1
				For column = 0 To QB_Missing2.Columns.Count - 1
					xlWorkSheet.Cells(INDEX_row, INDEX_Column) = QB_Missing2(row)(column)
					INDEX_Column += 1
				Next
				INDEX_Column = 1
				INDEX_row += 1
			Next

			INDEX_row = 3
			INDEX_Column = nextColumn2

			For row = 0 To QB_Extra2.Rows.Count - 1
				For column = 0 To QB_Extra2.Columns.Count - 1
					xlWorkSheet.Cells(INDEX_row, INDEX_Column) = QB_Extra2(row)(column)
					INDEX_Column += 1
				Next
				INDEX_Column = nextColumn2
				INDEX_row += 1
			Next

			xlWorkSheet.Range("A1:X1").EntireColumn.AutoFit()
			xlWorkSheet.Range("A1:X1").EntireColumn.NumberFormat = "0"
			xlWorkSheet.Range("A1:A2").EntireRow.Font.Bold = True
			xlWorkSheet.Range("A1:X1").EntireColumn.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft

			'----- SHEET 4 -----'

			xlWorkSheet = xlWorkBook.Sheets.Add(After:=xlWorkBook.Sheets(xlWorkBook.Sheets.Count))
			xlWorkSheet.Name = "Quantities"
			xlWorkSheet.PageSetup.CenterHeader = "Difference Report [Quantities] for: " & name & "   " & Date.Now.Date

			'ROW 1
			INDEX_row = 1
			INDEX_Column = 1

			'ROW 2
			INDEX_row += 1

			For Each header In PCAD_Quantity.Columns
				xlWorkSheet.Cells(INDEX_row, INDEX_Column) = header.columnName
				INDEX_Column += 1
			Next

			xlWorkSheet.Cells(1, 1) = "PCAD Against QB"
			xlWorkSheet.Range(xlWorkSheet.Cells(1, 1), xlWorkSheet.Cells(1, INDEX_Column - 1)).MergeCells = True

			INDEX_Column += 1
			nextColumn2 = INDEX_Column

			For Each header In QB_Quantity.Columns
				xlWorkSheet.Cells(INDEX_row, INDEX_Column) = header.columnName
				INDEX_Column += 1
			Next

			xlWorkSheet.Cells(1, nextColumn2) = "QB Against PCAD"
			xlWorkSheet.Range(xlWorkSheet.Cells(1, nextColumn2), xlWorkSheet.Cells(1, INDEX_Column - 1)).MergeCells = True

			INDEX_Column += 1
			Dim nextColumn3 = INDEX_Column

			For Each header In ALPHA_Quantity.Columns
				xlWorkSheet.Cells(INDEX_row, INDEX_Column) = header.columnName
				INDEX_Column += 1
			Next

			xlWorkSheet.Cells(1, nextColumn3) = "ALPHA Against QB"
			xlWorkSheet.Range(xlWorkSheet.Cells(1, nextColumn3), xlWorkSheet.Cells(1, INDEX_Column - 1)).MergeCells = True

			INDEX_Column += 1
			Dim nextColumn4 = INDEX_Column

			For Each header In QB_Quantity2.Columns
				xlWorkSheet.Cells(INDEX_row, INDEX_Column) = header.columnName
				INDEX_Column += 1
			Next

			xlWorkSheet.Cells(1, nextColumn4) = "QB Against ALPHA"
			xlWorkSheet.Range(xlWorkSheet.Cells(1, nextColumn4), xlWorkSheet.Cells(1, INDEX_Column - 1)).MergeCells = True

			'ROW 3
			INDEX_row += 1
			INDEX_Column = 1

			For row = 0 To PCAD_Quantity.Rows.Count - 1
				For column = 0 To PCAD_Quantity.Columns.Count - 1
					xlWorkSheet.Cells(INDEX_row, INDEX_Column) = PCAD_Quantity(row)(column)
					INDEX_Column += 1
				Next
				INDEX_Column = 1
				INDEX_row += 1
			Next

			INDEX_row = 3
			INDEX_Column = nextColumn2

			For row = 0 To QB_Quantity.Rows.Count - 1
				For column = 0 To QB_Quantity.Columns.Count - 1
					xlWorkSheet.Cells(INDEX_row, INDEX_Column) = QB_Quantity(row)(column)
					INDEX_Column += 1
				Next
				INDEX_Column = nextColumn2
				INDEX_row += 1
			Next

			INDEX_row = 3
			INDEX_Column = nextColumn3

			For row = 0 To ALPHA_Quantity.Rows.Count - 1
				For column = 0 To ALPHA_Quantity.Columns.Count - 1
					xlWorkSheet.Cells(INDEX_row, INDEX_Column) = ALPHA_Quantity(row)(column)
					INDEX_Column += 1
				Next
				INDEX_Column = nextColumn3
				INDEX_row += 1
			Next

			INDEX_row = 3
			INDEX_Column = nextColumn4

			For row = 0 To QB_Quantity2.Rows.Count - 1
				For column = 0 To QB_Quantity2.Columns.Count - 1
					xlWorkSheet.Cells(INDEX_row, INDEX_Column) = QB_Quantity2(row)(column)
					INDEX_Column += 1
				Next
				INDEX_Column = nextColumn4
				INDEX_row += 1
			Next

			xlWorkSheet.Range("A1:X1").EntireColumn.AutoFit()
			xlWorkSheet.Range("A1:X1").EntireColumn.NumberFormat = "0"
			xlWorkSheet.Range("A1:A2").EntireRow.Font.Bold = True
			xlWorkSheet.Range("A1:X1").EntireColumn.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft

			xlWorkBook.Sheets("PCAD-ALPHA").Select()

			xlApp.DisplayAlerts = False
			xlApp.Visible = True

			releaseObject(xlWorkSheet)
			releaseObject(xlWorkBook)
			releaseObject(xlApp)
		Catch ex As Exception
			MsgBox("File was not written: " & ex.Message)
		End Try
	End Sub

	Public Sub GenerateBoardOptionReport(ByRef table As DataTable)
		Try
			Dim xlApp As New Excel.Application
			Dim xlWorkBook As Excel.Workbook
			Dim xlWorkSheet As Excel.Worksheet
			Dim misValue As Object = Reflection.Missing.Value
			Dim INDEX_row As Integer = 1
			Dim INDEX_column As Integer = 1

			xlWorkBook = xlApp.Workbooks.Add(misValue)
			xlWorkSheet = xlWorkBook.Sheets("sheet1")

			xlWorkSheet.PageSetup.CenterHeader = "Board Option Report   " & Date.Now

			For Each header In table.Columns
				xlWorkSheet.Cells(INDEX_row, INDEX_column) = header.columnName
				INDEX_column += 1
			Next

			INDEX_column = 1
			INDEX_row += 1
			For row = 0 To table.Rows.Count - 1
				For column = 0 To table.Columns.Count - 1
					xlWorkSheet.Cells(INDEX_row, INDEX_column) = table(row)(column)
					INDEX_column += 1
				Next
				INDEX_column = 1
				INDEX_row += 1
			Next

			xlWorkSheet.Range("A1:C1").EntireColumn.AutoFit()
			xlWorkSheet.Range("A1:X1").EntireColumn.NumberFormat = "0"
			xlWorkSheet.Range("A1").EntireRow.Font.Bold = True
			xlWorkSheet.Range("A1:X1").EntireColumn.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft

			xlApp.DisplayAlerts = False
			xlApp.Visible = True

			releaseObject(xlWorkSheet)
			releaseObject(xlWorkBook)
			releaseObject(xlApp)
		Catch ex As Exception
			MsgBox("File was not written: " & ex.Message)
		End Try
	End Sub

	Public Sub GenerateALPHAfile(ByRef list As List(Of String), ByRef boardName As String)
		Try
			Dim TempFile As New StreamWriter("\\Server1\Shares\Production\AlphaBackup\" & boardName & ".gen", False)
			For Each item In list
				TempFile.WriteLine(item)
			Next

			TempFile.Close()
		Catch ex As Exception
			MsgBox(ex.Message)
		End Try
	End Sub

	Public Sub GenerateALPHAfileReport(ByRef board As String,
									  ByRef ALPHAmising As DataTable, ByRef missingPackage As DataTable,
									  ByRef magazineMissing As DataTable, ByRef magazineExtra As DataTable, ByRef build As String,
									  ByRef PNPmissing As DataTable, ByRef PNPextra As DataTable)
		Try
			Dim xlApp As New Excel.Application
			Dim xlWorkBook As Excel.Workbook
			Dim xlWorkSheet As Excel.Worksheet
			Dim misValue As Object = Reflection.Missing.Value
			xlWorkBook = xlApp.Workbooks.Add(misValue)

			'----- SHEET 1 -----'

			xlWorkSheet = xlWorkBook.Sheets("sheet1")
			xlWorkSheet.Name = "ALPHA Missing"
			xlWorkSheet.PageSetup.CenterHeader = board & " ALPHA File Report   " & Date.Now.Date

			'ROW 1
			Dim INDEX_row As Integer = 1
			Dim INDEX_column As Integer = 1

			'ROW 2
			INDEX_row += 1

			For Each header In ALPHAmising.Columns
				xlWorkSheet.Cells(INDEX_row, INDEX_column) = header.columnName
				INDEX_column += 1
			Next

			xlWorkSheet.Cells(1, 1) = "ALPHA Missing Components"
			xlWorkSheet.Range(xlWorkSheet.Cells(1, 1), xlWorkSheet.Cells(1, INDEX_column - 1)).MergeCells = True

			INDEX_column += 1
			Dim nextColumn2 = INDEX_column

			For Each header In missingPackage.Columns
				xlWorkSheet.Cells(INDEX_row, INDEX_column) = header.columnName
				INDEX_column += 1
			Next

			xlWorkSheet.Cells(1, nextColumn2) = "ALPHA Components Missing Package"
			xlWorkSheet.Range(xlWorkSheet.Cells(1, nextColumn2), xlWorkSheet.Cells(1, INDEX_column - 1)).MergeCells = True

			'ROW 3
			INDEX_row += 1
			INDEX_column = 1

			For row = 0 To ALPHAmising.Rows.Count - 1
				For column = 0 To ALPHAmising.Columns.Count - 1
					xlWorkSheet.Cells(INDEX_row, INDEX_column) = ALPHAmising(row)(column)
					INDEX_column += 1
				Next
				INDEX_column = 1
				INDEX_row += 1
			Next

			INDEX_column = nextColumn2
			INDEX_row = 3

			For row = 0 To missingPackage.Rows.Count - 1
				For column = 0 To missingPackage.Columns.Count - 1
					xlWorkSheet.Cells(INDEX_row, INDEX_column) = missingPackage(row)(column)
					INDEX_column += 1
				Next
				INDEX_column = nextColumn2
				INDEX_row += 1
			Next

			xlWorkSheet.Range("A1:A2").EntireRow.Font.Bold = True
			xlWorkSheet.Range("A1:X1").EntireColumn.AutoFit()
			xlWorkSheet.Range("A1:X1").EntireColumn.NumberFormat = "0"
			xlWorkSheet.Range("A1:X1").EntireColumn.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
			xlWorkSheet.Range("A1:A1").EntireRow.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter

			'----- SHEET 2 -----'

			xlWorkSheet = xlWorkBook.Sheets("sheet2")
			xlWorkSheet.Name = "Magazine"
			xlWorkSheet.PageSetup.CenterHeader = board & " ALPHA File Report   " & Date.Now.Date

			Dim Style_Out As Excel.Style = xlWorkSheet.Application.ActiveWorkbook.Styles.Add("Part Out")
			Style_Out.Interior.Color = ColorTranslator.ToOle(Color.FromArgb(255, 151, 163))

			Dim Style_Swap As Excel.Style = xlWorkSheet.Application.ActiveWorkbook.Styles.Add("Swap Feeder")
			Style_Swap.Interior.Color = ColorTranslator.ToOle(Color.FromArgb(255, 235, 156))

			Dim Style_NotFound As Excel.Style = xlWorkSheet.Application.ActiveWorkbook.Styles.Add("Not Found")
			Style_NotFound.Interior.Color = ColorTranslator.ToOle(Color.Orange)

			'ROW 1
			INDEX_row = 1
			INDEX_column = 1

			xlWorkSheet.Cells(INDEX_row, INDEX_column) = build

			'ROW 2
			INDEX_row += 3

			For Each header In magazineMissing.Columns
				xlWorkSheet.Cells(INDEX_row, INDEX_column) = header.columnName
				INDEX_column += 1
			Next

			xlWorkSheet.Cells(3, 1) = "Components Not loaded in Magazines"
			xlWorkSheet.Range(xlWorkSheet.Cells(3, 1), xlWorkSheet.Cells(3, INDEX_column - 1)).MergeCells = True

			INDEX_column += 1
			nextColumn2 = INDEX_column

			For Each header In magazineExtra.Columns
				xlWorkSheet.Cells(INDEX_row, INDEX_column) = header.columnName
				INDEX_column += 1
			Next

			xlWorkSheet.Cells(3, nextColumn2) = "Do not swap out these Feeders"
			xlWorkSheet.Range(xlWorkSheet.Cells(3, nextColumn2), xlWorkSheet.Cells(3, INDEX_column - 1)).MergeCells = True

			'ROW 3
			INDEX_row += 1
			INDEX_column = 1

			For row = 0 To magazineMissing.Rows.Count - 1
				Dim index As Integer = 0
				For column = 0 To magazineMissing.Columns.Count - 1
					xlWorkSheet.Cells(INDEX_row, INDEX_column) = magazineMissing(row)(column)
					INDEX_column += 1
					index += 1
				Next

				'Format the cell rows to show the user what needs to be addressed.
				If magazineMissing(row)(HEADER_QTY_AVAIL) = NOT_IN_DATABASE Then
					xlWorkSheet.Range(xlWorkSheet.Cells(INDEX_row, 1), xlWorkSheet.Cells(INDEX_row, 1 + index - 1)).Style = Style_NotFound
				Else
					'Check to see if we will be short.
					If CInt(magazineMissing(row)(HEADER_QTY_AVAIL)) < CInt(magazineMissing(row)(HEADER_QTY_NEEDED)) Then
						xlWorkSheet.Range(xlWorkSheet.Cells(INDEX_row, 1), xlWorkSheet.Cells(INDEX_row, 1 + index - 1)).Style = Style_Out
					End If
				End If


				INDEX_column = 1
				INDEX_row += 1
			Next

			INDEX_column = nextColumn2
			INDEX_row = 4

			For row = 0 To magazineExtra.Rows.Count - 1
				Dim index = 0
				For column = 0 To magazineExtra.Columns.Count - 1
					xlWorkSheet.Cells(INDEX_row, INDEX_column) = magazineExtra(row)(column)
					INDEX_column += 1
					index += 1
				Next

				'Format the cell rows to show the user what needs to be addressed.
				If magazineExtra(row)(HEADER_QTY_AVAIL) = NOT_IN_DATABASE Then
					xlWorkSheet.Range(xlWorkSheet.Cells(INDEX_row, nextColumn2), xlWorkSheet.Cells(INDEX_row, nextColumn2 + index - 1)).Style = Style_NotFound
				Else
					'Second Check to see if we need to swap out during the build.
					If CInt(magazineExtra(row)(DB_HEADER_QUANTITY)) < CInt(magazineExtra(row)(HEADER_QTY_NEEDED)) Then
						xlWorkSheet.Range(xlWorkSheet.Cells(INDEX_row, nextColumn2), xlWorkSheet.Cells(INDEX_row, nextColumn2 + index - 1)).Style = Style_Swap
					End If

					'Third Check to see if we will be short
					If CInt(magazineExtra(row)(HEADER_QTY_AVAIL)) < CInt(magazineExtra(row)(HEADER_QTY_NEEDED)) Then
						xlWorkSheet.Range(xlWorkSheet.Cells(INDEX_row, nextColumn2), xlWorkSheet.Cells(INDEX_row, nextColumn2 + index - 1)).Style = Style_Out
					End If
				End If

				INDEX_column = nextColumn2
				INDEX_row += 1
			Next

			xlWorkSheet.Range("A3:A3").EntireRow.Font.Bold = True
			xlWorkSheet.Range("A1:X1").EntireColumn.AutoFit()
			xlWorkSheet.Range("A1:X1").EntireColumn.NumberFormat = "0"
			xlWorkSheet.Range("A1:X1").EntireColumn.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
			xlWorkSheet.Range("A3:A3").EntireRow.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter

			'----- SHEET 3 -----'

			xlWorkSheet = xlWorkBook.Sheets("sheet3")
			xlWorkSheet.Name = "PNP Missing"
			xlWorkSheet.PageSetup.CenterHeader = board & " ALPHA File Report   " & Date.Now.Date

			'ROW 1
			INDEX_row = 1
			INDEX_column = 1

			'ROW 2
			INDEX_row += 1

			For Each header In PNPmissing.Columns
				xlWorkSheet.Cells(INDEX_row, INDEX_column) = header.columnName
				INDEX_column += 1
			Next

			xlWorkSheet.Cells(1, 1) = "PNP Missing Components"
			xlWorkSheet.Range(xlWorkSheet.Cells(1, 1), xlWorkSheet.Cells(1, INDEX_column - 1)).MergeCells = True

			INDEX_column += 1
			nextColumn2 = INDEX_column

			For Each header In PNPextra.Columns
				xlWorkSheet.Cells(INDEX_row, INDEX_column) = header.columnName
				INDEX_column += 1
			Next

			xlWorkSheet.Cells(1, nextColumn2) = "PNP Extra Components"
			xlWorkSheet.Range(xlWorkSheet.Cells(1, nextColumn2), xlWorkSheet.Cells(1, INDEX_column - 1)).MergeCells = True

			'ROW 3
			INDEX_row += 1
			INDEX_column = 1

			For row = 0 To PNPmissing.Rows.Count - 1
				For column = 0 To PNPmissing.Columns.Count - 1
					xlWorkSheet.Cells(INDEX_row, INDEX_column) = PNPmissing(row)(column)
					INDEX_column += 1
				Next
				INDEX_column = 1
				INDEX_row += 1
			Next

			INDEX_row = 3
			INDEX_column = nextColumn2

			For row = 0 To PNPextra.Rows.Count - 1
				For column = 0 To PNPextra.Columns.Count - 1
					xlWorkSheet.Cells(INDEX_row, INDEX_column) = PNPextra(row)(column)
					INDEX_column += 1
				Next
				INDEX_column = nextColumn2
				INDEX_row += 1
			Next

			xlWorkSheet.Range("A1:A2").EntireRow.Font.Bold = True
			xlWorkSheet.Range("A1:X1").EntireColumn.AutoFit()
			xlWorkSheet.Range("A1:X1").EntireColumn.NumberFormat = "0"
			xlWorkSheet.Range("A1:X1").EntireColumn.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
			xlWorkSheet.Range("A1:A1").EntireRow.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter

			xlApp.DisplayAlerts = False
			xlApp.Visible = True

			releaseObject(xlWorkSheet)
			releaseObject(xlWorkBook)
			releaseObject(xlApp)
		Catch ex As Exception
			MsgBox("File was not written: " & ex.Message)
		End Try
	End Sub

	Public Sub GenerateOptimizeMagazine(ByRef boards As ListBox, ByRef ds As DataSet, ByRef NoC As String, ByRef TcP As String)
		Try
			Dim xlApp As New Excel.Application
			Dim xlWorkBook As Excel.Workbook
			Dim xlWorkSheet As Excel.Worksheet
			Dim misValue As Object = Reflection.Missing.Value
			Dim INDEX_row As Integer = 1
			Dim INDEX_column As Integer = 1

			xlWorkBook = xlApp.Workbooks.Add(misValue)
			xlWorkSheet = xlWorkBook.Sheets("sheet1")

			xlWorkSheet.PageSetup.CenterHeader = "Optimize Magazine Report   " & Date.Now

			xlWorkSheet.Cells(INDEX_row, INDEX_column) = "Optimized for these Boards"
			INDEX_row += 1
			For Each item In boards.Items
				xlWorkSheet.Cells(INDEX_row, INDEX_column) = item
				INDEX_row += 1
			Next

			INDEX_column = 3
			INDEX_row = 1
			xlWorkSheet.Cells(INDEX_row, INDEX_column) = NoC & "   " & TcP
			xlWorkSheet.Range("C1:F1").MergeCells = True
			INDEX_row += 1

			For Each header In ds.Tables(0).Columns
				xlWorkSheet.Cells(INDEX_row, INDEX_column) = header.columnName
				INDEX_column += 1
			Next

			INDEX_column = 3
			INDEX_row += 1

			For row = 0 To ds.Tables(0).Rows.Count - 1
				For column = 0 To ds.Tables(0).Columns.Count - 1
					xlWorkSheet.Cells(INDEX_row, INDEX_column) = ds.Tables(0)(row)(column)
					INDEX_column += 1
				Next
				INDEX_column = 3
				INDEX_row += 1
			Next

			xlWorkSheet.Range("A1:X100").EntireColumn.AutoFit()
			xlWorkSheet.Range("A1:X1").EntireColumn.NumberFormat = "0"
			xlWorkSheet.Range("A1").EntireRow.Font.Bold = True
			xlWorkSheet.Range("A1:X1").EntireColumn.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft

			xlApp.DisplayAlerts = False
			xlApp.Visible = True

			releaseObject(xlWorkSheet)
			releaseObject(xlWorkBook)
			releaseObject(xlApp)
		Catch ex As Exception
			MsgBox("File was not written: " & ex.Message)
		End Try
	End Sub

	Public Sub GeneratePartUsageReport(ByRef table As DataTable)
		Try
			Dim xlApp As New Excel.Application
			Dim xlWorkBook As Excel.Workbook
			Dim xlWorkSheet As Excel.Worksheet
			Dim misValue As Object = Reflection.Missing.Value
			Dim INDEX_row As Integer = 1
			Dim INDEX_column As Integer = 1

			Dim costIndex As Integer = 0

			xlWorkBook = xlApp.Workbooks.Add(misValue)
			xlWorkSheet = xlWorkBook.Sheets("sheet1")

			xlWorkSheet.PageSetup.CenterHeader = "Part Usage Report   " & Date.Now

			For Each header In table.Columns
				xlWorkSheet.Cells(INDEX_row, INDEX_column) = header.columnName
				If header.ColumnName = DB_HEADER_COST Then
					costIndex = INDEX_column
				End If
				INDEX_column += 1
			Next

			INDEX_column = 1
			INDEX_row += 1
			For row = 0 To table.Rows.Count - 1
				For column = 0 To table.Columns.Count - 1
					xlWorkSheet.Cells(INDEX_row, INDEX_column) = table(row)(column)
					INDEX_column += 1
				Next
				INDEX_column = 1
				INDEX_row += 1
			Next

			Dim range As Excel.Range
			range = xlWorkSheet.UsedRange
			range.EntireColumn.AutoFit()
			range.EntireColumn.NumberFormat = "0"
			xlWorkSheet.Range("A1").EntireRow.Font.Bold = True

			'Cost
			If costIndex <> 0 Then
				xlWorkSheet.Cells(1, costIndex).EntireColumn.NumberFormat = "_($* #,##0.00000##_);_($* (#,##0.00000##);_($* ""-""??_);_(@_)"
			End If

			Dim borders As Excel.Borders = range.Borders
			borders.LineStyle = Excel.XlLineStyle.xlContinuous

			xlWorkSheet.Range("A1:AY1").EntireColumn.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft

			xlApp.DisplayAlerts = False
			xlApp.Visible = True

			releaseObject(xlWorkSheet)
			releaseObject(xlWorkBook)
			releaseObject(xlApp)
		Catch ex As Exception
			MsgBox("File was not written: " & ex.Message)
		End Try
	End Sub

	Public Sub GenerateQB_itemslistReport(ByRef ds As DataSet)
		Try
			Dim xlApp As New Excel.Application
			Dim xlWorkBook As Excel.Workbook
			Dim xlWorkSheet As Excel.Worksheet
			Dim misValue As Object = Reflection.Missing.Value
			Dim INDEX_row As Integer = 1
			Dim INDEX_column As Integer = 1

			xlWorkBook = xlApp.Workbooks.Add(misValue)
			xlWorkSheet = xlWorkBook.Sheets("sheet1")

			xlWorkSheet.PageSetup.CenterHeader = "QB Items Report   " & Date.Now

			For Each dc As DataColumn In ds.Tables(0).Columns
				xlWorkSheet.Cells(1, INDEX_column) = dc.ColumnName
				INDEX_column += 1
			Next

			INDEX_row += 1
			'Reset the Column index
			INDEX_column = 1

			For Each dr As DataRow In ds.Tables(0).Rows
				For Each dc As DataColumn In ds.Tables(0).Columns
					xlWorkSheet.Cells(INDEX_row, INDEX_column) = dr(dc).ToString
					INDEX_column += 1
				Next
				INDEX_row += 1
				'Reset the Column index
				INDEX_column = 1
			Next

			xlWorkSheet.Range("A1:X1").EntireColumn.AutoFit()
			xlWorkSheet.Range("A1:X1").EntireColumn.NumberFormat = "0"
			xlWorkSheet.Range("A1").EntireRow.Font.Bold = True
			xlWorkSheet.Range("A1:X1").EntireColumn.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft

			xlApp.DisplayAlerts = False
			xlApp.Visible = True

			releaseObject(xlWorkSheet)
			releaseObject(xlWorkBook)
			releaseObject(xlApp)
		Catch ex As Exception
			MsgBox("File was not written: " & ex.Message)
		End Try
	End Sub

	Public Sub RichTextToExcel(byref richText As RichTextBox) 
		Try
			Dim xlApp As New Excel.Application
			Dim xlWorkBook As Excel.Workbook
			Dim xlWorkSheet As Excel.Worksheet
			Dim misValue As Object = Reflection.Missing.Value
			Dim INDEX_row As Integer = 1
			Dim INDEX_column As Integer = 1

			xlWorkBook = xlApp.Workbooks.Add(misValue)
			xlWorkSheet = xlWorkBook.Sheets("sheet1")

			For Each line In richText.Lines
				xlWorkSheet.Cells(INDEX_row, INDEX_column) = line
				INDEX_row += 1
			Next

			xlWorkSheet.Range("A1:X1").EntireColumn.AutoFit()
			xlWorkSheet.Range("A1:X1").EntireColumn.NumberFormat = "0"
			xlWorkSheet.Range("A1:X1").EntireColumn.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft

			xlApp.DisplayAlerts = False
			xlApp.Visible = True

			releaseObject(xlWorkSheet)
			releaseObject(xlWorkBook)
			releaseObject(xlApp)
		Catch ex As Exception
			MsgBox("File was not written: " & ex.Message)
		End Try
	End Sub

	Private Sub releaseObject(ByVal obj As Object)
		Try
			Runtime.InteropServices.Marshal.ReleaseComObject(obj)
			obj = Nothing
		Catch ex As Exception
			obj = Nothing
		Finally
			GC.Collect()
		End Try
	End Sub

End Class