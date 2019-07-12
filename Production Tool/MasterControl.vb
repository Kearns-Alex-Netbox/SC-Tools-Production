'-----------------------------------------------------------------------------------------------------------------------------------------
' Module: MasterControl.vb
'
' Description: Lou's special Process Release utilities
'   Tab 1 QuickBooks: Items: List of QB items with search ability
'   Tab 2 Alpha Items: List of Alpha items with search ability
'   Tab 3 Alpha Packages: List of Alpha packages with search ability
'   Tab 4 Alpha Magazines: Lit of Alpha Magazines with search ability
'-----------------------------------------------------------------------------------------------------------------------------------------
Imports System.Data.SqlClient

Public Class MasterControl

#Region "Tab 1: QB Items Variables"
	Dim QBitems_da As New SqlDataAdapter
	Dim QBitems_ds As New DataSet
	Dim QBitems_myCmd = New SqlCommand("", myConn)

	Dim QBitems_scrollValue As Integer
	Dim QBitems_Command As String = "SELECT [" & DB_HEADER_ITEM_PREFIX & "]" &
										", [" & DB_HEADER_ITEM_PREFIX & "] + ':' + [" & DB_HEADER_ITEM_NUMBER & "] AS '" & DB_HEADER_ITEM_NUMBER & "'" &
										", [" & DB_HEADER_TYPE & "]" &
										", [" & DB_HEADER_DESCRIPTION & "]" &
										", [" & DB_HEADER_VENDOR & "]" &
										", [" & DB_HEADER_MPN & "]" &
										", [" & DB_HEADER_QUANTITY & "]" &
										", [" & DB_HEADER_COST & "]" &
										", [" & DB_HEADER_LEAD_TIME & "]" &
										", [" & DB_HEADER_MIN_ORDER_QTY & "]" &
										", [" & DB_HEADER_REORDER_QTY & "]" &
										", [" & DB_HEADER_VENDOR2 & "]" &
										", [" & DB_HEADER_MPN2 & "]" &
										", [" & DB_HEADER_VENDOR3 & "]" &
										", [" & DB_HEADER_MPN3 & "]" &
										" FROM " & TABLE_QB_ITEMS
	Dim QBitems_countCommand As String = "SELECT COUNT(*) FROM " & TABLE_QB_ITEMS
	Dim QBitems_entriesToShow As Integer = 250
	Dim QBitems_numberOfRecords As Integer
	Dim QBitems_sort As String = ""
	Dim QBitems_searchCommand As String = ""
	Dim QBitems_searchCountCommand As String = ""
	Dim QBitems_Freeze As Integer = 1
#End Region

#Region "Tab 2: ALPHA Items Variables"
	Dim ALPHAitems_da As New SqlDataAdapter
	Dim ALPHAitems_ds As New DataSet
	Dim ALPHAitems_myCmd = New SqlCommand("", myConn)

	Dim ALPHAitems_scrollValue As Integer
	Dim ALPHAitems_Command As String = "SELECT [" & DB_HEADER_ITEM_NUMBER & "]" &
									   ", [" & DB_HEADER_FEEDER_TYPE & "]" &
									   ", [" & DB_HEADER_MAGAZINE_TYPE & "]" &
									   ", [" & DB_HEADER_PACKAGE & "] FROM " & TABLE_ALPHA_ITEMS
	Dim ALPHAitems_countCommand As String = "SELECT COUNT(*) FROM " & TABLE_ALPHA_ITEMS
	Dim ALPHAitems_entriesToShow As Integer = 250
	Dim ALPHAitems_numberOfRecords As Integer
	Dim ALPHAitems_sort As String = ""
	Dim ALPHAitems_searchCommand As String = ""
	Dim ALPHAitems_searchCountCommand As String = ""
	Dim ALPHAitems_Freeze As Integer = 0
#End Region

#Region "Tab 3: ALPHA Packages Variables"
	Dim ALPHApackage_da As New SqlDataAdapter
	Dim ALPHApackage_ds As New DataSet
	Dim ALPHApackage_myCmd = New SqlCommand("", myConn)

	Dim ALPHApackage_scrollValue As Integer
	Dim ALPHApackage_Command As String = "SELECT [" & DB_HEADER_PACKAGE_NAME & "]" &
										 ", [" & DB_HEADER_BODY_LENGTH & "]" &
										 ", [" & DB_HEADER_BODY_WIDTH & "]" &
										 ", [" & DB_HEADER_OVERALL_LENGTH & "]" &
										 ", [" & DB_HEADER_OVERALL_WIDTH & "]" &
										 ", [" & DB_HEADER_NORMAL_HEIGHT & "]" &
										 ", [" & DB_HEADER_MAX_HEIGHT & "]" &
										 ", [" & DB_HEADER_MIN_HEIGHT & "]" &
										 ", [" & DB_HEADER_ANGLE_1 & "]" &
										 ", [" & DB_HEADER_LEVEL_1 & "]" &
										 ", [" & DB_HEADER_POSITION_1 & "]" &
										 ", [" & DB_HEADER_FORCE_1 & "]" &
										 ", [" & DB_HEADER_NORMAL_SIZE_1 & "]" &
										 ", [" & DB_HEADER_MAX_SIZE_1 & "]" &
										 ", [" & DB_HEADER_MIN_SIZE_1 & "]" &
										 ", [" & DB_HEADER_VERIFY_MECHANICAL_1 & "]" &
										 ", [" & DB_HEADER_ANGLE_2 & "]" &
										 ", [" & DB_HEADER_LEVEL_2 & "]" &
										 ", [" & DB_HEADER_POSITION_2 & "]" &
										 ", [" & DB_HEADER_FORCE_2 & "]" &
										 ", [" & DB_HEADER_NORMAL_SIZE_2 & "]" &
										 ", [" & DB_HEADER_MAX_SIZE_2 & "]" &
										 ", [" & DB_HEADER_MIN_SIZE_2 & "]" &
										 ", [" & DB_HEADER_VERIFY_MECHANICAL_2 & "]" &
										 " FROM " & TABLE_ALPHA_PACKAGE
	Dim ALPHApackage_countCommand As String = "SELECT COUNT(*) FROM " & TABLE_ALPHA_PACKAGE
	Dim ALPHApackage_entriesToShow As Integer = 250
	Dim ALPHApackage_numberOfRecords As Integer
	Dim ALPHApackage_sort As String = ""
	Dim ALPHApackage_searchCommand As String = ""
	Dim ALPHApackage_searchCountCommand As String = ""
	Dim ALPHApackage_Freeze As Integer = 0
#End Region

#Region "Tab 4: ALPHA Magazines Variables"
	Dim ALPHAmags_da As New SqlDataAdapter
	Dim ALPHAmags_ds As New DataSet
	Dim ALPHAmags_myCmd = New SqlCommand("", myConn)

	Dim ALPHAmags_scrollValue As Integer
	Dim ALPHAmags_Command As String = "SELECT [" & DB_HEADER_NAME & "]" &
									  ", [" & DB_HEADER_SERIAL_NUMBER & "]" &
									  ", [" & DB_HEADER_MACHINE_NUMBER & "]" &
									  ", [" & DB_HEADER_SLOT_NUMBER & "]" &
									  ", [" & DB_HEADER_FEEDER_NUMBER & "]" &
									  ", [" & DB_HEADER_ANGLE & "]" &
									  ", [" & DB_HEADER_QUANTITY & "]" &
									  ", [" & DB_HEADER_ITEM_NUMBER & "]" &
									  " FROM " & TABLE_MAGAZINE_DATA
	Dim ALPHAmags_countCommand As String = "SELECT COUNT(*) FROM " & TABLE_MAGAZINE_DATA
	Dim ALPHAmags_entriesToShow As Integer = 250
	Dim ALPHAmags_numberOfRecords As Integer
	Dim ALPHAmags_sort As String = ""
	Dim ALPHAmags_searchCommand As String = ""
	Dim ALPHAmags_searchCountCommand As String = ""
	Dim ALPHAmags_Freeze As Integer = 0
#End Region

	Dim myCmd As New SqlCommand("", myConn)

	Private Sub MasterControl_Load() Handles MyBase.Load
		Dim message As String = ""
		If sqlapi.CheckDirtyBit(message) = True Then
			MsgBox(message)
			Return
		End If

		'Inherited Sub that will center the opening form to screen.
		CenterToParent()

		Dim result As String = ""

		Try
			'Tab Page 1 - QuickBooks
			sqlapi.GetNumberOfRecords(QBitems_myCmd, QBitems_countCommand, QBitems_numberOfRecords, result)
			L_QB_Results.Text = "Number of results: " & QBitems_numberOfRecords

			QBitems_myCmd.CommandText = QBitems_Command & " ORDER BY [" & DB_HEADER_ITEM_PREFIX & "] + ':' + [" & DB_HEADER_ITEM_NUMBER & "]"
			QBitems_da = New SqlDataAdapter(QBitems_myCmd)
			QBitems_ds = New DataSet()

			sqlapi.RetriveData(QBitems_Freeze, QBitems_da, QBitems_ds, DGV_QB_Items, QBitems_scrollValue, QBitems_entriesToShow, QBitems_numberOfRecords,
							   B_QB_Next, B_QB_Last, B_QB_First, B_QB_Previous)

			'Get Drop Down Items.
			GetColumnDropDownItems(CB_QB_Sort, QBitems_ds)
			GetColumnDropDownItems(CB_QB_Search, QBitems_ds)
			GetColumnDropDownItems(CB_QB_Search2, QBitems_ds)

			CB_QB_Operand1.SelectedIndex = 0
			CB_QB_Operand2.SelectedIndex = 0

			'Tab Page 2 - Alpha Items
			sqlapi.GetNumberOfRecords(ALPHAitems_myCmd, ALPHAitems_countCommand, ALPHAitems_numberOfRecords, result)
			L_ALPHA_Results.Text = "Number of results: " & ALPHAitems_numberOfRecords

			ALPHAitems_myCmd.CommandText = ALPHAitems_Command & " ORDER BY [" & DB_HEADER_ITEM_NUMBER & "]"
			ALPHAitems_da = New SqlDataAdapter(ALPHAitems_myCmd)
			ALPHAitems_ds = New DataSet()

			sqlapi.RetriveData(ALPHAitems_Freeze, ALPHAitems_da, ALPHAitems_ds, DGV_ALPHA_Items, ALPHAitems_scrollValue, ALPHAitems_entriesToShow,
							   ALPHAitems_numberOfRecords, B_ALPHA_Next, B_ALPHA_Last, B_ALPHA_First, B_ALPHA_Previous)

			'Get Drop Down Items.
			GetColumnDropDownItems(CB_ALPHA_Sort, ALPHAitems_ds)
			GetColumnDropDownItems(CB_ALPHA_Search, ALPHAitems_ds)
			GetColumnDropDownItems(CB_ALPHA_Search2, ALPHAitems_ds)

			CB_ALPHA_Operand1.SelectedIndex = 0
			CB_ALPHA_Operand2.SelectedIndex = 0

			'Tab Page 3 - Alpha Packages
			sqlapi.GetNumberOfRecords(ALPHApackage_myCmd, ALPHApackage_countCommand, ALPHApackage_numberOfRecords, result)
			L_Package_Results.Text = "Number of results: " & ALPHApackage_numberOfRecords

			ALPHApackage_myCmd.CommandText = ALPHApackage_Command & " ORDER BY [" & DB_HEADER_PACKAGE_NAME & "]"
			ALPHApackage_da = New SqlDataAdapter(ALPHApackage_myCmd)
			ALPHApackage_ds = New DataSet()

			sqlapi.RetriveData(ALPHApackage_Freeze, ALPHApackage_da, ALPHApackage_ds, DGV_ALPHA_Package, ALPHApackage_scrollValue, ALPHApackage_entriesToShow,
							   ALPHApackage_numberOfRecords, B_Package_Next, B_Package_Last, B_Package_First, B_Package_Previous)

			'Get Drop Down Items.
			GetColumnDropDownItems(CB_Package_Sort, ALPHApackage_ds)
			GetColumnDropDownItems(CB_Package_Search, ALPHApackage_ds)
			GetColumnDropDownItems(CB_Package_Search2, ALPHApackage_ds)

			CB_Package_Operand1.SelectedIndex = 0
			CB_Package_Operand2.SelectedIndex = 0

			'Tab Page 4 - Alpha Magazines
			sqlapi.GetNumberOfRecords(ALPHAmags_myCmd, ALPHAmags_countCommand, ALPHAmags_numberOfRecords, result)
			L_Mags_Results.Text = "Number of results: " & ALPHAmags_numberOfRecords

			ALPHAmags_myCmd.CommandText = ALPHAmags_Command & " ORDER BY [" & DB_HEADER_NAME & "]"
			ALPHAmags_da = New SqlDataAdapter(ALPHAmags_myCmd)
			ALPHAmags_ds = New DataSet()

			sqlapi.RetriveData(ALPHAmags_Freeze, ALPHAmags_da, ALPHAmags_ds, DGV_ALPHA_Mags, ALPHAmags_scrollValue, ALPHAmags_entriesToShow, ALPHAmags_numberOfRecords,
							   B_Mags_Next, B_Mags_Last, B_Mags_First, B_Mags_Previous)

			'Get Drop Down Items.
			GetColumnDropDownItems(CB_Mags_Sort, ALPHAmags_ds)
			GetColumnDropDownItems(CB_Mags_Search, ALPHAmags_ds)
			GetColumnDropDownItems(CB_Mags_Search2, ALPHAmags_ds)

			CB_Mags_Operand1.SelectedIndex = 0
			CB_Mags_Operand2.SelectedIndex = 0

			KeyPreview = True
		Catch ex As Exception
			MsgBox(ex.Message)
		End Try
	End Sub

	Private Sub B_CreateExcel_Click() Handles B_CreateExcel.Click
		Dim report As New GenerateReport()

		'Depending on which tab is open will determine which report to create.
		Select Case TabControl1.SelectedTab.Name
			Case "TP_QB_items"
				Dim Temp_ds As New DataSet
				QBitems_da.Fill(Temp_ds, "TABLE")
				report.GenerateQB_itemslistReport(Temp_ds)
			Case "TP_ALPHA_items"
				Dim Temp_ds As New DataSet
				ALPHAitems_da.Fill(Temp_ds, "TABLE")
				report.GenerateQB_itemslistReport(Temp_ds)
			Case "TP_ALPHA_Packages"
				Dim Temp_ds As New DataSet
				ALPHApackage_da.Fill(Temp_ds, "TABLE")
				report.GenerateQB_itemslistReport(Temp_ds)
			Case "TP_ALPHA_Mags"
				Dim Temp_ds As New DataSet
				ALPHAmags_da.Fill(Temp_ds, "TABLE")
				report.GenerateQB_itemslistReport(Temp_ds)
			Case Else
				MsgBox("Generating a report on this tab has not been coded yet.")
		End Select
	End Sub

	Private Sub B_Close_Click() Handles B_Close.Click
		Close()
	End Sub

	Private Sub GetColumnDropDownItems(ByRef cb As ComboBox, ByRef ds As DataSet)
		For Each dc As DataColumn In ds.Tables(0).Columns
			cb.Items.Add(dc.ColumnName)
		Next

		If cb.Items.Count <> 0 Then
			cb.SelectedIndex = 0
		End If
		cb.DropDownHeight = 200
	End Sub

#Region "Tab 1: QB Items"
	Private Sub B_QB_First_Click() Handles B_QB_First.Click
		sqlapi.FirstPage(QBitems_scrollValue, QBitems_ds, QBitems_da, QBitems_entriesToShow)
		B_QB_First.Enabled = False
		B_QB_Previous.Enabled = False
		B_QB_Next.Enabled = True
		B_QB_Last.Enabled = True
	End Sub

	Private Sub B_QB_Previous_Click() Handles B_QB_Previous.Click
		sqlapi.PreviousPage(QBitems_scrollValue, QBitems_entriesToShow, QBitems_ds, QBitems_da, B_QB_First, B_QB_Previous)
		B_QB_Next.Enabled = True
		B_QB_Last.Enabled = True
	End Sub

	Private Sub B_QB_Next_Click() Handles B_QB_Next.Click
		sqlapi.NextPage(QBitems_scrollValue, QBitems_entriesToShow, QBitems_numberOfRecords, QBitems_ds, QBitems_da, B_QB_Next, B_QB_Last)
		B_QB_First.Enabled = True
		B_QB_Previous.Enabled = True
	End Sub

	Private Sub B_QB_Last_Click() Handles B_QB_Last.Click
		sqlapi.LastPage(QBitems_scrollValue, QBitems_entriesToShow, QBitems_numberOfRecords, QBitems_ds, QBitems_da)
		B_QB_First.Enabled = True
		B_QB_Previous.Enabled = True
		B_QB_Next.Enabled = False
		B_QB_Last.Enabled = False
	End Sub

	Private Sub B_QB_ListAll_Click() Handles B_QB_ListAll.Click
		Cursor = Cursors.WaitCursor
		If LOGDATA = True Then
			Try
				sqlapi.ListAll(1, QBitems_searchCommand, QBitems_searchCountCommand, QBitems_myCmd, QBitems_countCommand, QBitems_numberOfRecords, L_QB_Results,
							   QBitems_Command & " ORDER BY [" & DB_HEADER_ITEM_PREFIX & "] + ':' + [" & DB_HEADER_ITEM_NUMBER & "]", QBitems_ds, QBitems_da, DGV_QB_Items, QBitems_scrollValue, QBitems_entriesToShow, B_QB_Next,
							   B_QB_Last, B_QB_First, B_QB_Previous)
			Catch ex As Exception
				UnhandledExceptionMessage(ex)
			End Try
		Else
			sqlapi.ListAll(1, QBitems_searchCommand, QBitems_searchCountCommand, QBitems_myCmd, QBitems_countCommand, QBitems_numberOfRecords, L_QB_Results,
						   QBitems_Command & " ORDER BY [" & DB_HEADER_ITEM_PREFIX & "] + ':' + [" & DB_HEADER_ITEM_NUMBER & "]", QBitems_ds, QBitems_da, DGV_QB_Items, QBitems_scrollValue, QBitems_entriesToShow, B_QB_Next,
						   B_QB_Last, B_QB_First, B_QB_Previous)
		End If
		Cursor = Cursors.Default
	End Sub

	Private Sub B_QB_Search_Click() Handles B_QB_Search.Click
		Cursor = Cursors.WaitCursor
		If LOGDATA = True Then
			Try
				sqlapi.Search(QBitems_Freeze, TB_QB_Search, CB_QB_Operand1, QBitems_searchCommand, QBitems_Command, QBitems_searchCountCommand, QBitems_countCommand, TB_QB_Search2, CB_QB_Operand2,
							  CB_QB_Search, CB_QB_Search2, QBitems_myCmd, QBitems_ds, QBitems_da, DGV_QB_Items, QBitems_numberOfRecords, L_QB_Results, QBitems_scrollValue,
							  QBitems_entriesToShow, B_QB_Next, B_QB_Last, B_QB_First, B_QB_Previous, "[" & DB_HEADER_ITEM_PREFIX & "] + ':' + [" & DB_HEADER_ITEM_NUMBER & "]")
			Catch ex As Exception
				UnhandledExceptionMessage(ex)
			End Try
		Else
			sqlapi.Search(QBitems_Freeze, TB_QB_Search, CB_QB_Operand1, QBitems_searchCommand, QBitems_Command, QBitems_searchCountCommand, QBitems_countCommand, TB_QB_Search2, CB_QB_Operand2,
						  CB_QB_Search, CB_QB_Search2, QBitems_myCmd, QBitems_ds, QBitems_da, DGV_QB_Items, QBitems_numberOfRecords, L_QB_Results, QBitems_scrollValue,
						  QBitems_entriesToShow, B_QB_Next, B_QB_Last, B_QB_First, B_QB_Previous, "[" & DB_HEADER_ITEM_PREFIX & "] + ':' + [" & DB_HEADER_ITEM_NUMBER & "]")
		End If
		Cursor = Cursors.Default
	End Sub

	Private Sub B_QB_Sort_Click() Handles B_QB_Sort.Click
		Cursor = Cursors.WaitCursor
		If LOGDATA = True Then
			Try
				sqlapi.Sort(QBitems_Freeze, QBitems_searchCommand, QBitems_Command, CB_QB_Sort, RB_QB_AscendingSort, QBitems_myCmd, QBitems_ds, QBitems_da, DGV_QB_Items,
							QBitems_scrollValue, QBitems_entriesToShow, QBitems_numberOfRecords, B_QB_Next, B_QB_Last, B_QB_First, B_QB_Previous)
			Catch ex As Exception
				UnhandledExceptionMessage(ex)
			End Try
		Else
			sqlapi.Sort(QBitems_Freeze, QBitems_searchCommand, QBitems_Command, CB_QB_Sort, RB_QB_AscendingSort, QBitems_myCmd, QBitems_ds, QBitems_da, DGV_QB_Items,
						QBitems_scrollValue, QBitems_entriesToShow, QBitems_numberOfRecords, B_QB_Next, B_QB_Last, B_QB_First, B_QB_Previous)
		End If
		Cursor = Cursors.Default

	End Sub

	Private Sub TB_QB_Search_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles TB_QB_Search.KeyDown
		If e.KeyCode.Equals(Keys.Enter) Then
			Call B_QB_Search_Click()
		End If
	End Sub

	Private Sub TB_QB_Search2_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles TB_QB_Search2.KeyDown
		If e.KeyCode.Equals(Keys.Enter) Then
			Call B_QB_Search_Click()
		End If
	End Sub

	Private Sub CB_QB_Display_SelectedValueChanged() Handles CB_QB_Display.SelectedValueChanged
		QBitems_entriesToShow = CInt(CB_QB_Display.Text)
	End Sub

	Private Sub CB_QB_Search_Click() Handles CB_QB_Search.Click
		CB_QB_Search.SelectedIndex = 0
	End Sub

	Private Sub CB_QB_Search_DropDownClosed() Handles CB_QB_Search.DropDownClosed
		Dim newcmd As New SqlCommand("SELECT DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" & TABLE_QB_ITEMS & "' AND COLUMN_NAME = '" & CB_QB_Search.Text & "'", myConn)

		Dim type As String = newcmd.ExecuteScalar

		If type = "decimal" Or type = "int" Then
			CB_QB_Operand1.SelectedIndex = 3
		Else
			CB_QB_Operand1.SelectedIndex = 0
		End If
	End Sub

	Private Sub CB_QB_Search2_DropDownClosed() Handles CB_QB_Search2.DropDownClosed
		Dim newcmd As New SqlCommand("SELECT DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" & TABLE_QB_ITEMS & "' AND COLUMN_NAME = '" & CB_QB_Search2.Text & "'", myConn)

		Dim type As String = newcmd.ExecuteScalar

		If type = "decimal" Or type = "int" Then
			CB_QB_Operand2.SelectedIndex = 3
		Else
			CB_QB_Operand2.SelectedIndex = 0
		End If
	End Sub

	Private Sub CB_QB_Sort_Click() Handles CB_QB_Sort.Click
		CB_QB_Sort.SelectedIndex = 0
	End Sub

	Private Sub DGV_QB_Items_RowPostPaint(ByVal sender As Object, ByVal e As DataGridViewRowPostPaintEventArgs) Handles DGV_QB_Items.RowPostPaint
		'Go through each row of the DGV and add the row number to the row header.
		Using b As SolidBrush = New SolidBrush(DGV_QB_Items.RowHeadersDefaultCellStyle.ForeColor)
			e.Graphics.DrawString(e.RowIndex + 1 + QBitems_scrollValue, DGV_QB_Items.DefaultCellStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
		End Using
	End Sub
#End Region

#Region "Tab 2: ALPHA Items"
	Private Sub B_ALPHA_First_Click() Handles B_ALPHA_First.Click
		sqlapi.FirstPage(ALPHAitems_scrollValue, ALPHAitems_ds, ALPHAitems_da, ALPHAitems_entriesToShow)
		B_ALPHA_First.Enabled = False
		B_ALPHA_Previous.Enabled = False
		B_ALPHA_Next.Enabled = True
		B_ALPHA_Last.Enabled = True
	End Sub

	Private Sub B_ALPHA_Previous_Click() Handles B_ALPHA_Previous.Click
		sqlapi.PreviousPage(ALPHAitems_scrollValue, ALPHAitems_entriesToShow, ALPHAitems_ds, ALPHAitems_da, B_ALPHA_First, B_ALPHA_Previous)
		B_ALPHA_Next.Enabled = True
		B_ALPHA_Last.Enabled = True
	End Sub

	Private Sub B_ALPHA_Next_Click() Handles B_ALPHA_Next.Click
		sqlapi.NextPage(ALPHAitems_scrollValue, ALPHAitems_entriesToShow, ALPHAitems_numberOfRecords, ALPHAitems_ds, ALPHAitems_da, B_ALPHA_Next, B_ALPHA_Last)
		B_ALPHA_First.Enabled = True
		B_ALPHA_Previous.Enabled = True
	End Sub

	Private Sub B_ALPHA_Last_Click() Handles B_ALPHA_Last.Click
		sqlapi.LastPage(ALPHAitems_scrollValue, ALPHAitems_entriesToShow, ALPHAitems_numberOfRecords, ALPHAitems_ds, ALPHAitems_da)
		B_ALPHA_First.Enabled = True
		B_ALPHA_Previous.Enabled = True
		B_ALPHA_Next.Enabled = False
		B_ALPHA_Last.Enabled = False
	End Sub

	Private Sub B_ALPHA_ListAll_Click() Handles B_ALPHA_ListAll.Click
		Cursor = Cursors.WaitCursor
		If LOGDATA = True Then
			Try
				sqlapi.ListAll(ALPHAitems_Freeze, ALPHAitems_searchCommand, ALPHAitems_searchCountCommand, ALPHAitems_myCmd, ALPHAitems_countCommand,
							   ALPHAitems_numberOfRecords, L_ALPHA_Results, ALPHAitems_Command & " ORDER BY [" & DB_HEADER_ITEM_NUMBER & "]", ALPHAitems_ds, ALPHAitems_da,
							   DGV_ALPHA_Items, ALPHAitems_scrollValue, ALPHAitems_entriesToShow, B_ALPHA_Next, B_ALPHA_Last, B_ALPHA_First, B_ALPHA_Previous)
			Catch ex As Exception
				UnhandledExceptionMessage(ex)
			End Try
		Else
			sqlapi.ListAll(ALPHAitems_Freeze, ALPHAitems_searchCommand, ALPHAitems_searchCountCommand, ALPHAitems_myCmd, ALPHAitems_countCommand,
						   ALPHAitems_numberOfRecords, L_ALPHA_Results, ALPHAitems_Command & " ORDER BY [" & DB_HEADER_ITEM_NUMBER & "]", ALPHAitems_ds, ALPHAitems_da,
						   DGV_ALPHA_Items, ALPHAitems_scrollValue, ALPHAitems_entriesToShow, B_ALPHA_Next, B_ALPHA_Last, B_ALPHA_First, B_ALPHA_Previous)
		End If
		Cursor = Cursors.Default
	End Sub

	Private Sub B_ALPHA_Search_Click() Handles B_ALPHA_Search.Click
		Cursor = Cursors.WaitCursor
		If LOGDATA = True Then
			Try
				sqlapi.Search(ALPHAitems_Freeze, TB_ALPHA_Search, CB_ALPHA_Operand1, ALPHAitems_searchCommand, ALPHAitems_Command, ALPHAitems_searchCountCommand, ALPHAitems_countCommand,
							  TB_ALPHA_Search2, CB_ALPHA_Operand2, CB_ALPHA_Search, CB_ALPHA_Search2, ALPHAitems_myCmd, ALPHAitems_ds, ALPHAitems_da, DGV_ALPHA_Items, ALPHAitems_numberOfRecords,
							  L_ALPHA_Results, ALPHAitems_scrollValue, ALPHAitems_entriesToShow, B_ALPHA_Next, B_ALPHA_Last, B_ALPHA_First, B_ALPHA_Previous, "[" & DB_HEADER_ITEM_NUMBER & "]")
			Catch ex As Exception
				UnhandledExceptionMessage(ex)
			End Try
		Else
			sqlapi.Search(ALPHAitems_Freeze, TB_ALPHA_Search, CB_ALPHA_Operand1, ALPHAitems_searchCommand, ALPHAitems_Command, ALPHAitems_searchCountCommand, ALPHAitems_countCommand,
						  TB_ALPHA_Search2, CB_ALPHA_Operand2, CB_ALPHA_Search, CB_ALPHA_Search2, ALPHAitems_myCmd, ALPHAitems_ds, ALPHAitems_da, DGV_ALPHA_Items, ALPHAitems_numberOfRecords,
						  L_ALPHA_Results, ALPHAitems_scrollValue, ALPHAitems_entriesToShow, B_ALPHA_Next, B_ALPHA_Last, B_ALPHA_First, B_ALPHA_Previous, "[" & DB_HEADER_ITEM_NUMBER & "]")
		End If
		Cursor = Cursors.Default
	End Sub

	Private Sub B_ALPHA_Sort_Click() Handles B_ALPHA_Sort.Click
		Cursor = Cursors.WaitCursor
		If LOGDATA = True Then
			Try
				sqlapi.Sort(ALPHAitems_Freeze, ALPHAitems_searchCommand, ALPHAitems_Command, CB_ALPHA_Sort, RB_ALPHA_AscendingSort, ALPHAitems_myCmd, ALPHAitems_ds,
							ALPHAitems_da, DGV_ALPHA_Items, ALPHAitems_scrollValue, ALPHAitems_entriesToShow, ALPHAitems_numberOfRecords, B_ALPHA_Next, B_ALPHA_Last,
							B_ALPHA_First, B_ALPHA_Previous)
			Catch ex As Exception
				UnhandledExceptionMessage(ex)
			End Try
		Else
			sqlapi.Sort(ALPHAitems_Freeze, ALPHAitems_searchCommand, ALPHAitems_Command, CB_ALPHA_Sort, RB_ALPHA_AscendingSort, ALPHAitems_myCmd, ALPHAitems_ds,
						ALPHAitems_da, DGV_ALPHA_Items, ALPHAitems_scrollValue, ALPHAitems_entriesToShow, ALPHAitems_numberOfRecords, B_ALPHA_Next, B_ALPHA_Last,
						B_ALPHA_First, B_ALPHA_Previous)
		End If
		Cursor = Cursors.Default
	End Sub

	Private Sub TB_ALPHA_Search_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles TB_ALPHA_Search.KeyDown
		If e.KeyCode.Equals(Keys.Enter) Then
			Call B_ALPHA_Search_Click()
		End If
	End Sub

	Private Sub TB_ALPHA_Search2_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles TB_ALPHA_Search2.KeyDown
		If e.KeyCode.Equals(Keys.Enter) Then
			Call B_ALPHA_Search_Click()
		End If
	End Sub

	Private Sub CB_ALPHA_Display_SelectedValueChanged() Handles CB_ALPHA_Display.SelectedValueChanged
		ALPHAitems_entriesToShow = CInt(CB_ALPHA_Display.Text)
	End Sub

	Private Sub CB_ALPHA_Search_Click() Handles CB_ALPHA_Search.Click
		CB_ALPHA_Search.SelectedIndex = 0
	End Sub

	Private Sub CB_ALPHA_Search_DropDownClosed() Handles CB_ALPHA_Search.DropDownClosed
		Dim newcmd As New SqlCommand("SELECT DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" & TABLE_ALPHA_ITEMS & "' AND COLUMN_NAME = '" & CB_ALPHA_Search.Text & "'", myConn)

		Dim type As String = newcmd.ExecuteScalar

		If type = "decimal" Or type = "int" Then
			CB_ALPHA_Operand1.SelectedIndex = 3
		Else
			CB_ALPHA_Operand1.SelectedIndex = 0
		End If
	End Sub

	Private Sub CB_ALPHA_Search2_DropDownClosed() Handles CB_ALPHA_Search2.DropDownClosed
		Dim newcmd As New SqlCommand("SELECT DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" & TABLE_ALPHA_ITEMS & "' AND COLUMN_NAME = '" & CB_ALPHA_Search2.Text & "'", myConn)

		Dim type As String = newcmd.ExecuteScalar

		If type = "decimal" Or type = "int" Then
			CB_ALPHA_Operand2.SelectedIndex = 3
		Else
			CB_ALPHA_Operand2.SelectedIndex = 0
		End If
	End Sub

	Private Sub CB_ALPHA_Sort_Click() Handles CB_ALPHA_Sort.Click
		CB_ALPHA_Sort.SelectedIndex = 0
	End Sub

	Private Sub DGV_ALPHA_Items_RowPostPaint(ByVal sender As Object, ByVal e As DataGridViewRowPostPaintEventArgs) Handles DGV_ALPHA_Items.RowPostPaint
		'Go through each row of the DGV and add the row number to the row header.
		Using b As SolidBrush = New SolidBrush(DGV_ALPHA_Items.RowHeadersDefaultCellStyle.ForeColor)
			e.Graphics.DrawString(e.RowIndex + 1 + ALPHAitems_scrollValue, DGV_ALPHA_Items.DefaultCellStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
		End Using
	End Sub
#End Region

#Region "Tab 3: ALPHA Packages"
	Private Sub B_Package_First_Click() Handles B_Package_First.Click
		sqlapi.FirstPage(ALPHApackage_scrollValue, ALPHApackage_ds, ALPHApackage_da, ALPHApackage_entriesToShow)
		B_Package_First.Enabled = False
		B_Package_Previous.Enabled = False
		B_Package_Next.Enabled = True
		B_Package_Last.Enabled = True
	End Sub

	Private Sub B_Package_Previous_Click() Handles B_Package_Previous.Click
		sqlapi.PreviousPage(ALPHApackage_scrollValue, ALPHApackage_entriesToShow, ALPHApackage_ds, ALPHApackage_da, B_Package_First, B_Package_Previous)
		B_Package_Next.Enabled = True
		B_Package_Last.Enabled = True
	End Sub

	Private Sub B_Package_Next_Click() Handles B_Package_Next.Click
		sqlapi.NextPage(ALPHApackage_scrollValue, ALPHApackage_entriesToShow, ALPHApackage_numberOfRecords, ALPHApackage_ds, ALPHApackage_da, B_Package_Next, B_Package_Last)
		B_Package_First.Enabled = True
		B_Package_Previous.Enabled = True
	End Sub

	Private Sub B_Package_Last_Click() Handles B_Package_Last.Click
		sqlapi.LastPage(ALPHApackage_scrollValue, ALPHApackage_entriesToShow, ALPHApackage_numberOfRecords, ALPHApackage_ds, ALPHApackage_da)
		B_Package_First.Enabled = True
		B_Package_Previous.Enabled = True
		B_Package_Next.Enabled = False
		B_Package_Last.Enabled = False
	End Sub

	Private Sub B_Package_ListAll_Click() Handles B_Package_ListAll.Click
		Cursor = Cursors.WaitCursor
		If LOGDATA = True Then
			Try
				sqlapi.ListAll(ALPHApackage_Freeze, ALPHApackage_searchCommand, ALPHApackage_searchCountCommand, ALPHApackage_myCmd, ALPHApackage_countCommand,
							   ALPHApackage_numberOfRecords, L_Package_Results, ALPHApackage_Command & " ORDER BY [" & DB_HEADER_PACKAGE_NAME & "]", ALPHApackage_ds, ALPHApackage_da,
							   DGV_ALPHA_Package, ALPHApackage_scrollValue, ALPHApackage_entriesToShow, B_Package_Next, B_Package_Last, B_Package_First, B_Package_Previous)
			Catch ex As Exception
				UnhandledExceptionMessage(ex)
			End Try
		Else
			sqlapi.ListAll(ALPHApackage_Freeze, ALPHApackage_searchCommand, ALPHApackage_searchCountCommand, ALPHApackage_myCmd, ALPHApackage_countCommand,
						   ALPHApackage_numberOfRecords, L_Package_Results, ALPHApackage_Command & " ORDER BY [" & DB_HEADER_PACKAGE_NAME & "]", ALPHApackage_ds, ALPHApackage_da,
						   DGV_ALPHA_Package, ALPHApackage_scrollValue, ALPHApackage_entriesToShow, B_Package_Next, B_Package_Last, B_Package_First, B_Package_Previous)
		End If
		Cursor = Cursors.Default
	End Sub

	Private Sub B_Package_Search_Click() Handles B_Package_Search.Click
		Cursor = Cursors.WaitCursor
		If LOGDATA = True Then
			Try
				sqlapi.Search(ALPHApackage_Freeze, TB_Package_Search, CB_Package_Operand1, ALPHApackage_searchCommand, ALPHApackage_Command, ALPHApackage_searchCountCommand,
							  ALPHApackage_countCommand, TB_Package_Search2, CB_Package_Operand2, CB_Package_Search, CB_Package_Search2, ALPHApackage_myCmd, ALPHApackage_ds, ALPHApackage_da,
							  DGV_ALPHA_Package, ALPHApackage_numberOfRecords, L_Package_Results, ALPHApackage_scrollValue, ALPHApackage_entriesToShow, B_Package_Next,
							  B_Package_Last, B_Package_First, B_Package_Previous, "[" & DB_HEADER_PACKAGE_NAME & "]")
			Catch ex As Exception
				UnhandledExceptionMessage(ex)
			End Try
		Else
			sqlapi.Search(ALPHApackage_Freeze, TB_Package_Search, CB_Package_Operand1, ALPHApackage_searchCommand, ALPHApackage_Command, ALPHApackage_searchCountCommand,
						  ALPHApackage_countCommand, TB_Package_Search2, CB_Package_Operand2, CB_Package_Search, CB_Package_Search2, ALPHApackage_myCmd, ALPHApackage_ds, ALPHApackage_da,
						  DGV_ALPHA_Package, ALPHApackage_numberOfRecords, L_Package_Results, ALPHApackage_scrollValue, ALPHApackage_entriesToShow, B_Package_Next,
						  B_Package_Last, B_Package_First, B_Package_Previous, "[" & DB_HEADER_PACKAGE_NAME & "]")
		End If
		Cursor = Cursors.Default
	End Sub

	Private Sub B_Package_Sort_Click() Handles B_Package_Sort.Click
		Cursor = Cursors.WaitCursor
		If LOGDATA = True Then
			Try
				sqlapi.Sort(ALPHApackage_Freeze, ALPHApackage_searchCommand, ALPHApackage_Command, CB_Package_Sort, RB_Package_Ascending, ALPHApackage_myCmd,
												ALPHApackage_ds, ALPHApackage_da, DGV_ALPHA_Package, ALPHApackage_scrollValue, ALPHApackage_entriesToShow,
												ALPHApackage_numberOfRecords, B_Package_Next, B_Package_Last, B_Package_First, B_Package_Previous)
			Catch ex As Exception
				UnhandledExceptionMessage(ex)
			End Try
		Else
			sqlapi.Sort(ALPHApackage_Freeze, ALPHApackage_searchCommand, ALPHApackage_Command, CB_Package_Sort, RB_Package_Ascending, ALPHApackage_myCmd,
								ALPHApackage_ds, ALPHApackage_da, DGV_ALPHA_Package, ALPHApackage_scrollValue, ALPHApackage_entriesToShow,
								ALPHApackage_numberOfRecords, B_Package_Next, B_Package_Last, B_Package_First, B_Package_Previous)
		End If
		Cursor = Cursors.Default
	End Sub

	Private Sub TB_Package_Search_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles TB_Package_Search.KeyDown
		If e.KeyCode.Equals(Keys.Enter) Then
			Call B_Package_Search_Click()
		End If
	End Sub

	Private Sub TB_Package_Search2_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles TB_Package_Search2.KeyDown
		If e.KeyCode.Equals(Keys.Enter) Then
			Call B_Package_Search_Click()
		End If
	End Sub

	Private Sub CB_Package_Display_SelectedValueChanged() Handles CB_Package_Display.SelectedValueChanged
		ALPHApackage_entriesToShow = CInt(CB_Package_Display.Text)
	End Sub

	Private Sub CB_Package_Search_Click() Handles CB_Package_Search.Click
		CB_Package_Search.SelectedIndex = 0
	End Sub

	Private Sub CB_Package_Search_DropDownClosed() Handles CB_Package_Search.DropDownClosed
		Dim newcmd As New SqlCommand("SELECT DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" & TABLE_ALPHA_PACKAGE & "' AND COLUMN_NAME = '" & CB_Package_Search.Text & "'", myConn)

		Dim type As String = newcmd.ExecuteScalar

		If type = "decimal" Or type = "int" Then
			CB_Package_Operand1.SelectedIndex = 3
		Else
			CB_Package_Operand1.SelectedIndex = 0
		End If
	End Sub

	Private Sub CB_Package_Search2_DropDownClosed() Handles CB_Package_Search2.DropDownClosed
		Dim newcmd As New SqlCommand("SELECT DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" & TABLE_ALPHA_PACKAGE & "' AND COLUMN_NAME = '" & CB_Package_Search2.Text & "'", myConn)

		Dim type As String = newcmd.ExecuteScalar

		If type = "decimal" Or type = "int" Then
			CB_Package_Operand2.SelectedIndex = 3
		Else
			CB_Package_Operand2.SelectedIndex = 0
		End If
	End Sub

	Private Sub CB_Package_Sort_Click() Handles CB_Package_Sort.Click
		CB_Package_Sort.SelectedIndex = 0
	End Sub

	Private Sub DGV_ALPHA_Package_RowPostPaint(ByVal sender As Object, ByVal e As DataGridViewRowPostPaintEventArgs) Handles DGV_ALPHA_Package.RowPostPaint
		'Go through each row of the DGV and add the row number to the row header.
		Using b As SolidBrush = New SolidBrush(DGV_ALPHA_Package.RowHeadersDefaultCellStyle.ForeColor)
			e.Graphics.DrawString(e.RowIndex + 1 + ALPHApackage_scrollValue, DGV_ALPHA_Package.DefaultCellStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
		End Using
	End Sub
#End Region

#Region "Tab 4: ALPHA Magizines"
	Private Sub B_Mags_First_Click() Handles B_Mags_First.Click
		sqlapi.FirstPage(ALPHAmags_scrollValue, ALPHAmags_ds, ALPHAmags_da, ALPHAmags_entriesToShow)
		B_Mags_First.Enabled = False
		B_Mags_Previous.Enabled = False
		B_Mags_Next.Enabled = True
		B_Mags_Last.Enabled = True
	End Sub

	Private Sub B_Mags_Previous_Click() Handles B_Mags_Previous.Click
		sqlapi.PreviousPage(ALPHAmags_scrollValue, ALPHAmags_entriesToShow, ALPHAmags_ds, ALPHAmags_da, B_Mags_First, B_Mags_Previous)
		B_Mags_Next.Enabled = True
		B_Mags_Last.Enabled = True
	End Sub

	Private Sub B_Mags_Next_Click() Handles B_Mags_Next.Click
		sqlapi.NextPage(ALPHAmags_scrollValue, ALPHAmags_entriesToShow, ALPHAmags_numberOfRecords, ALPHAmags_ds, ALPHAmags_da, B_Mags_Next, B_Mags_Last)
		B_Mags_First.Enabled = True
		B_Mags_Previous.Enabled = True
	End Sub

	Private Sub B_Mags_Last_Click() Handles B_Mags_Last.Click
		sqlapi.LastPage(ALPHAmags_scrollValue, ALPHAmags_entriesToShow, ALPHAmags_numberOfRecords, ALPHAmags_ds, ALPHAmags_da)
		B_Mags_First.Enabled = True
		B_Mags_Previous.Enabled = True
		B_Mags_Next.Enabled = False
		B_Mags_Last.Enabled = False
	End Sub

	Private Sub B_Mags_ListAll_Click() Handles B_Mags_ListAll.Click
		Cursor = Cursors.WaitCursor
		If LOGDATA = True Then
			Try
				sqlapi.ListAll(ALPHAmags_Freeze, ALPHAmags_searchCommand, ALPHAmags_searchCountCommand, ALPHAmags_myCmd, ALPHAmags_countCommand, ALPHAmags_numberOfRecords,
							   L_Mags_Results, ALPHAmags_Command & " ORDER BY [" & DB_HEADER_NAME & "]", ALPHAmags_ds, ALPHAmags_da, DGV_ALPHA_Mags, ALPHAmags_scrollValue,
							   ALPHAmags_entriesToShow, B_Mags_Next, B_Mags_Last, B_Mags_First, B_Mags_Previous)
			Catch ex As Exception
				UnhandledExceptionMessage(ex)
			End Try
		Else
			sqlapi.ListAll(ALPHAmags_Freeze, ALPHAmags_searchCommand, ALPHAmags_searchCountCommand, ALPHAmags_myCmd, ALPHAmags_countCommand, ALPHAmags_numberOfRecords,
						   L_Mags_Results, ALPHAmags_Command & " ORDER BY [" & DB_HEADER_NAME & "]", ALPHAmags_ds, ALPHAmags_da, DGV_ALPHA_Mags, ALPHAmags_scrollValue,
						   ALPHAmags_entriesToShow, B_Mags_Next, B_Mags_Last, B_Mags_First, B_Mags_Previous)
		End If
		Cursor = Cursors.Default
	End Sub

	Private Sub B_Mags_Search_Click() Handles B_Mags_Search.Click
		Cursor = Cursors.WaitCursor
		If LOGDATA = True Then
			Try
				sqlapi.Search(ALPHAmags_Freeze, TB_Mags_Search, CB_Mags_Operand1, ALPHAmags_searchCommand, ALPHAmags_Command, ALPHAmags_searchCountCommand, ALPHAmags_countCommand,
							  TB_Mags_Search2, CB_Mags_Operand2, CB_Mags_Search, CB_Mags_Search2, ALPHAmags_myCmd, ALPHAmags_ds, ALPHAmags_da, DGV_ALPHA_Mags, ALPHAmags_numberOfRecords,
							  L_Mags_Results, ALPHAmags_scrollValue, ALPHAmags_entriesToShow, B_Mags_Next, B_Mags_Last, B_Mags_First, B_Mags_Previous, "[" & DB_HEADER_NAME & "]")
			Catch ex As Exception
				UnhandledExceptionMessage(ex)
			End Try
		Else
			sqlapi.Search(ALPHAmags_Freeze, TB_Mags_Search, CB_Mags_Operand1, ALPHAmags_searchCommand, ALPHAmags_Command, ALPHAmags_searchCountCommand, ALPHAmags_countCommand,
						  TB_Mags_Search2, CB_Mags_Operand2, CB_Mags_Search, CB_Mags_Search2, ALPHAmags_myCmd, ALPHAmags_ds, ALPHAmags_da, DGV_ALPHA_Mags, ALPHAmags_numberOfRecords,
						  L_Mags_Results, ALPHAmags_scrollValue, ALPHAmags_entriesToShow, B_Mags_Next, B_Mags_Last, B_Mags_First, B_Mags_Previous, "[" & DB_HEADER_NAME & "]")
		End If
		Cursor = Cursors.Default
	End Sub

	Private Sub B_Mags_Sort_Click() Handles B_Mags_Sort.Click
		Cursor = Cursors.WaitCursor
		If LOGDATA = True Then
			Try
				sqlapi.Sort(ALPHAmags_Freeze, ALPHAmags_searchCommand, ALPHAmags_Command, CB_Mags_Sort, RB_Mags_Ascending, ALPHAmags_myCmd, ALPHAmags_ds, ALPHAmags_da,
							DGV_ALPHA_Mags, ALPHAmags_scrollValue, ALPHAmags_entriesToShow, ALPHAmags_numberOfRecords, B_Mags_Next, B_Mags_Last, B_Mags_First, B_Mags_Previous)
			Catch ex As Exception
				UnhandledExceptionMessage(ex)
			End Try
		Else
			sqlapi.Sort(ALPHAmags_Freeze, ALPHAmags_searchCommand, ALPHAmags_Command, CB_Mags_Sort, RB_Mags_Ascending, ALPHAmags_myCmd, ALPHAmags_ds, ALPHAmags_da,
						DGV_ALPHA_Mags, ALPHAmags_scrollValue, ALPHAmags_entriesToShow, ALPHAmags_numberOfRecords, B_Mags_Next, B_Mags_Last, B_Mags_First, B_Mags_Previous)
		End If
		Cursor = Cursors.Default
	End Sub

	Private Sub TB_Mags_Search_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles TB_Mags_Search.KeyDown
		If e.KeyCode.Equals(Keys.Enter) Then
			Call B_Mags_Search_Click()
		End If
	End Sub

	Private Sub TB_Mags_Search2_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles TB_Mags_Search2.KeyDown
		If e.KeyCode.Equals(Keys.Enter) Then
			Call B_Mags_Search_Click()
		End If
	End Sub

	Private Sub CB_Mags_Display_SelectedValueChanged() Handles CB_Mags_Display.SelectedValueChanged
		ALPHAmags_entriesToShow = CInt(CB_Mags_Display.Text)
	End Sub

	Private Sub CB_Mags_Search_Click() Handles CB_Mags_Search.Click
		CB_Mags_Search.SelectedIndex = 0
	End Sub

	Private Sub CB_Mags_Search_DropDownClosed() Handles CB_Mags_Search.DropDownClosed
		Dim newcmd As New SqlCommand("SELECT DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" & TABLE_MAGAZINE_DATA & "' AND COLUMN_NAME = '" & CB_Mags_Search.Text & "'", myConn)

		Dim type As String = newcmd.ExecuteScalar

		If type = "decimal" Or type = "int" Then
			CB_Mags_Operand1.SelectedIndex = 3
		Else
			CB_Mags_Operand1.SelectedIndex = 0
		End If
	End Sub

	Private Sub CB_Mags_Search2_DropDownClosed() Handles CB_Mags_Search2.DropDownClosed
		Dim newcmd As New SqlCommand("SELECT DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" & TABLE_MAGAZINE_DATA & "' AND COLUMN_NAME = '" & CB_Mags_Search2.Text & "'", myConn)

		Dim type As String = newcmd.ExecuteScalar

		If type = "decimal" Or type = "int" Then
			CB_Mags_Operand2.SelectedIndex = 3
		Else
			CB_Mags_Operand2.SelectedIndex = 0
		End If
	End Sub

	Private Sub CB_Mags_Sort_Click() Handles CB_Mags_Sort.Click
		CB_Mags_Sort.SelectedIndex = 0
	End Sub

	Private Sub DGV_ALPHA_Mags_RowPostPaint(ByVal sender As Object, ByVal e As DataGridViewRowPostPaintEventArgs) Handles DGV_ALPHA_Mags.RowPostPaint
		'Go through each row of the DGV and add the row number to the row header.
		Using b As SolidBrush = New SolidBrush(DGV_ALPHA_Mags.RowHeadersDefaultCellStyle.ForeColor)
			e.Graphics.DrawString(e.RowIndex + 1 + ALPHAmags_scrollValue, DGV_ALPHA_Mags.DefaultCellStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
		End Using
	End Sub
#End Region

End Class