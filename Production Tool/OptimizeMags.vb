'-----------------------------------------------------------------------------------------------------------------------------------------
' Module: OptimizeMags.vb
'
' Description: Optimizes the Magazines so we can put the highest used parts in a magazine and group magazines by board
'
' Special Keys:
'	delete = If we have an item highlighted in our chosenBoards listbox, then we remove it from the listbox.
'-----------------------------------------------------------------------------------------------------------------------------------------
Imports System.Data.SqlClient

Public Class OptimizeMags

	Dim DataTable_Information As DataTable
	Dim DataSet_Information As DataSet

	Private Sub OptimizeMags_Load() Handles MyBase.Load
		KeyPreview = True

		GetBoards()
	End Sub

	Private Sub SetupTable()
		DataTable_Information = New DataTable
		DataTable_Information.Columns.Add(DB_HEADER_ITEM_NUMBER)
		DataTable_Information.Columns.Add(HEADER_NUMBER_OF_BOARDS, GetType(Integer))
		DataTable_Information.Columns.Add(HEADER_TIMES_PLACED, GetType(Integer))
		DataTable_Information.Columns.Add(DB_HEADER_FEEDER_TYPE)
		DataTable_Information.Columns.Add(DB_HEADER_NAME)
		DataTable_Information.Columns.Add(DB_HEADER_SLOT_NUMBER)
		DataTable_Information.Columns.Add(DB_HEADER_FEEDER_NUMBER)
	End Sub

	Private Sub B_Optimize_Click() Handles B_Optimize.Click
		Cursor = Cursors.WaitCursor
		If LOGDATA = True Then
			Try
				GenerateReport()
			Catch ex As Exception
				UnhandledExceptionMessage(ex)
			End Try
		Else
			GenerateReport()
		End If
		Cursor = Cursors.Default
	End Sub

	Private Sub GenerateReport()
		Dim message As String = ""
		If sqlapi.CheckDirtyBit(message) = True Then
			MsgBox(message)
			Return
		End If

		'Number of Boards = The number of boards that the Component is found on.
		'Times Placed = The number of times that the component is placed.
		Dim boardCountQuery As String = "SELECT COUNT(DISTINCT [" & DB_HEADER_BOARD_NAME & "]) AS '" & HEADER_NUMBER_OF_BOARDS & "', COUNT([" & DB_HEADER_ITEM_NUMBER & "]) AS '" & HEADER_TIMES_PLACED & "', [" & DB_HEADER_ITEM_NUMBER & "] FROM " & TABLE_PCADBOM

		If ChosenBoards_ListBox.Items.Count = 0 Then
			boardCountQuery = boardCountQuery & " WHERE [" & DB_HEADER_REF_DES & "] NOT LIKE '" & REFERENCE_DESIGNATOR_OPTION & "%' AND [" & DB_HEADER_PROCESS & "] LIKE '" & PROCESS_SMT & "%' GROUP BY [" & DB_HEADER_ITEM_NUMBER & "] ORDER BY '" & HEADER_TIMES_PLACED & "' DESC, [" & DB_HEADER_ITEM_NUMBER & "]"
			Populate(boardCountQuery)
		ElseIf ChosenBoards_ListBox.SelectedItems.Count = 1 Then
			boardCountQuery = boardCountQuery & " WHERE [" & DB_HEADER_BOARD_NAME & "] = '" & ChosenBoards_ListBox.Items(0) & "' AND [" & DB_HEADER_REF_DES & "] NOT LIKE '" & REFERENCE_DESIGNATOR_OPTION & "%' AND [" & DB_HEADER_PROCESS & "] = '" & PROCESS_SMT & "' GROUP BY [" & DB_HEADER_ITEM_NUMBER & "] ORDER BY '" & HEADER_TIMES_PLACED & "' DESC, [" & DB_HEADER_ITEM_NUMBER & "]"
			Populate(boardCountQuery)
		Else
			boardCountQuery = boardCountQuery & " WHERE ([" & DB_HEADER_BOARD_NAME & "] = '" & ChosenBoards_ListBox.Items(0) & "'"
			For index = 1 To ChosenBoards_ListBox.Items.Count - 1
				boardCountQuery = boardCountQuery & " OR [" & DB_HEADER_BOARD_NAME & "] = '" & ChosenBoards_ListBox.Items(index) & "'"
			Next
			boardCountQuery = boardCountQuery & ") AND [" & DB_HEADER_REF_DES & "] NOT LIKE '" & REFERENCE_DESIGNATOR_OPTION & "%' AND [" & DB_HEADER_PROCESS & "] = '" & PROCESS_SMT & "' GROUP BY [" & DB_HEADER_ITEM_NUMBER & "] ORDER BY '" & HEADER_TIMES_PLACED & "' DESC, [" & DB_HEADER_ITEM_NUMBER & "]"
			Populate(boardCountQuery)
		End If

		Excel_Button.Enabled = True
	End Sub

	Private Sub Populate(ByRef boardCountQuery As String)
		SetupTable()

		Dim cmd As New SqlCommand(boardCountQuery, myConn)
		Dim PCAD_Count As New DataTable()
		PCAD_Count.Load(cmd.ExecuteReader)

		cmd.CommandText = "SELECT * FROM " & TABLE_ALPHA_ITEMS
		Dim ALPHA_Items As New DataTable()
		ALPHA_Items.Load(cmd.ExecuteReader)

		cmd.CommandText = "SELECT * FROM " & TABLE_MAGAZINE_DATA
		Dim MAG_Data As New DataTable()
		MAG_Data.Load(cmd.ExecuteReader)

		Dim total As Integer = 0
		Dim index As Integer = 0

		For Each dsrow As DataRow In PCAD_Count.Rows
			If dsrow(DB_HEADER_ITEM_NUMBER).contains(PREFIX_PCB) = True Then
				Continue For
			End If

			Dim feederSize As String = ""
			Dim magazine As String = ""
			Dim slot As String = ""
			Dim feeder As String = ""
			Dim boardQuantity As Integer = dsrow(HEADER_NUMBER_OF_BOARDS)
			Dim placeQuantity As Integer = dsrow(HEADER_TIMES_PLACED)

			total = total + placeQuantity

			Dim dr() As DataRow = ALPHA_Items.Select("[" & DB_HEADER_ITEM_NUMBER & "] = '" & dsrow(DB_HEADER_ITEM_NUMBER) & "'")
			If dr.Length <> 0 Then
				If dr(0)(DB_HEADER_FEEDER_TYPE).ToString.Length <> 0 Then
					feederSize = dr(0)(DB_HEADER_FEEDER_TYPE).ToString
				Else
					feederSize = "--mm"
				End If
			Else
				feederSize = "????"
			End If

			dr = MAG_Data.Select("[" & DB_HEADER_ITEM_NUMBER & "] = '" & dsrow(DB_HEADER_ITEM_NUMBER) & "'")
			If dr.Length <> 0 Then
				magazine = dr(0)(DB_HEADER_NAME)
				slot = dr(0)(DB_HEADER_SLOT_NUMBER)
				feeder = dr(0)(DB_HEADER_FEEDER_NUMBER)
			End If

			DataTable_Information.Rows.Add(dsrow(DB_HEADER_ITEM_NUMBER), boardQuantity.ToString("D2"), placeQuantity.ToString("D3"), feederSize, magazine, slot, feeder)

			index += 1
		Next

		Dim selectedboards As Integer = ChosenBoards_ListBox.Items.Count
		If selectedboards = 0 Then
			selectedboards = Boards_ListBox.Items.Count
		End If
		L_Boards.Text = "# of Boards [" & selectedboards & "]   NoC: " & DataTable_Information.Rows.Count
		L_Times.Text = "TcP: " & total

		Dim objDv As New DataView(DataTable_Information)
		objDv.Sort = "[" & HEADER_NUMBER_OF_BOARDS & "] DESC"
		DataSet_Information = New DataSet()
		DataSet_Information.Tables.Add(objDv.ToTable)

		DGV_MagInfo.DataSource = DataSet_Information.Tables(0)
		DGV_MagInfo.AutoResizeColumns(DataGridViewAutoSizeColumnMode.AllCells)
		Excel_Button.Enabled = True
	End Sub

	Private Sub GetBoards()
		'Load unique boards from PCAD's version of the BOM into Products listbox so we can select from it
		Dim myReader As SqlDataReader = Nothing

		'SQL command that will give us the name of every board in the table
		Dim myCmd As New SqlCommand("SELECT Distinct([" & DB_HEADER_BOARD_NAME & "]) FROM " & TABLE_PCADBOM & " ORDER BY [" & DB_HEADER_BOARD_NAME & "]", myConn)
		myReader = myCmd.ExecuteReader

		'Check to see if we have rows from the database.
		If myReader.HasRows = True Then
			While myReader.Read
				'Add the boardname to our list.
				Boards_ListBox.Items.Add(myReader.GetString(0))
			End While
		End If
		myReader.Close()
	End Sub

	Private Sub B_CreateExcel_Click() Handles Excel_Button.Click
		Dim report As New GenerateReport()
		report.GenerateOptimizeMagazine(ChosenBoards_ListBox, DataSet_Information, L_Boards.Text, L_Times.Text)
	End Sub

	Private Sub B_Close_Click() Handles B_Close.Click
		Close()
	End Sub

	Private Sub MyBase_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
		'If we press the Delete key, call our remove function.
		If e.KeyCode.Equals(Keys.Delete) Then
			Call Remove_Button_Click()
		End If
	End Sub

#Region "Dual Listbox Methods"

	Private Sub Products_ListBox_DoubleClick() Handles Boards_ListBox.DoubleClick
		'Treat the double click as if we are adding the item that we are double clicking on.
		Call Add_Button_Click()
	End Sub

	Private Sub Add_Button_Click() Handles Add_Button.Click
		'Add to our Build List using whatever board is selected in our product list only if we have not added it already.
		If ChosenBoards_ListBox.Items.Contains(Boards_ListBox.SelectedItem) = False Then
			ChosenBoards_ListBox.Items.Add(Boards_ListBox.SelectedItem)
		End If

		'We have changed the situation so disable the excel button so the user does not create under misinformation.
		Excel_Button.Enabled = False
	End Sub

	Private Sub Clear_Button_Click() Handles Clear_Button.Click
		'Clears the whole list of boards that we want to see the options for.
		ChosenBoards_ListBox.Items.Clear()
		L_Boards.Text = "# of Boards   NoC: 0"
		L_Times.Text = "TcP: 0"

		'We have changed the situation so disable the excel button so the user does not create under misinformation.
		Excel_Button.Enabled = False
	End Sub

	Private Sub Remove_Button_Click() Handles Remove_Button.Click
		'Remove the selected board from our list of boards that we want to see the options for.
		If ChosenBoards_ListBox.SelectedItems.Count <> 0 Then
			ChosenBoards_ListBox.Items.Remove(ChosenBoards_ListBox.SelectedItem)

			'We have changed the situation so disable the excel button so the user does not create under misinformation.
			Excel_Button.Enabled = False
		End If
	End Sub

#End Region

	Private Sub DGV_MagInfo_RowPostPaint(ByVal sender As Object, ByVal e As DataGridViewRowPostPaintEventArgs) Handles DGV_MagInfo.RowPostPaint
		Using b As SolidBrush = New SolidBrush(DGV_MagInfo.RowHeadersDefaultCellStyle.ForeColor)
			e.Graphics.DrawString(e.RowIndex + 1, DGV_MagInfo.DefaultCellStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
		End Using
	End Sub

	Private Sub RefreshMags_Button_Click() Handles RefreshMags_Button.Click
		Cursor = Cursors.WaitCursor
		If LOGDATA = True Then
			Try
				RefreshMags()
			Catch ex As Exception
				UnhandledExceptionMessage(ex)
			End Try
		Else
			RefreshMags()
		End If
		Cursor = Cursors.Default

		'Turn the dirty bit off once we are done with our import.
		sqlapi.SetDirtyBit(0)
	End Sub

End Class