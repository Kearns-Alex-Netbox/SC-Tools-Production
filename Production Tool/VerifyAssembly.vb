'-----------------------------------------------------------------------------------------------------------------------------------------
' Module: VerifyAssembly.vb
'
' Description: Compares the searched Build Ref. No. in Quickbooks to see if what is in ALPHA is the correct build. 
'	*****NEEDS TO HAVE A CONNECTION TO QUICKBOOKS*****
'		The table that is being accessed holds evey build ever and each unique component used in the build. This will continue to grow the
'		more jobs that are run and the more complex a board is. Because of this, we are choosing to always get this data from the QB 
'		database rathaer than import all of these extra rows and columns into our database. 

'Select Case
'  {fn SUBSTRING([ItemInventoryAssemblyRefFullName], {fn LOCATE('-',[ItemInventoryAssemblyRefFullName])} + 1, 1000)} AS "Board Name"
', {fn SUBSTRING([ComponentItemLineItemRefFullName], 1, {fn LOCATE(':',[ComponentItemLineItemRefFullName])} - 1 )} AS "Item Prefix"
', {fn SUBSTRING([ComponentItemLineItemRefFullName], {fn LOCATE(':',[ComponentItemLineItemRefFullName])} + 1, 1000)} AS "Item Number"
'FROM BuildAssemblyComponentItemLine 
'where RefNumber = '421'
'-----------------------------------------------------------------------------------------------------------------------------------------
Imports System.Data.SqlClient

Public Class VerifyAssembly

	Dim DataTable_ALPHAmissing As New DataTable
	Dim DataTable_ALPHAextra As New DataTable

	Dim myCmd As New SqlCommand("", myConn)

	Private Sub VerifyAssembly_Load() Handles MyBase.Load
		SetupDataTables()
		ResizeTables()
	End Sub

	Private Sub GenerateReport_Button_Click() Handles GenerateReport_Button.Click

	End Sub

	Private Sub SetupDataTables()
		'Assembly <-> ALPHA
		DataTable_ALPHAmissing.Columns.Add("", GetType(String))
		DataTable_ALPHAmissing.Columns.Add(DB_HEADER_ITEM_PREFIX, GetType(String))
		DataTable_ALPHAmissing.Columns.Add(DB_HEADER_ITEM_NUMBER, GetType(String))

		DataTable_ALPHAextra.Columns.Add("", GetType(String))
	End Sub

	Private Sub VerifyAssembly_Resize() Handles Me.Resize
		ResizeTables()
	End Sub

	Public Sub ResizeTables()
		'Math done to get all of the datagrids on each of the tabs centered. All hard-coded numbers are fine adjustment buffers.
		Dim topAndBottomPadding As Integer = 36      'Both top and bottom of the table get about 18 px of padding.
		Dim leftAndRightPadding As Integer = 16      'Both left and right of the table get about 8 px of padding.
		Dim labelYLocation As Integer = 62
		Dim DGVYLocation As Integer = 85

		Dim newWidth As Integer = Width / 2
		Dim newHeigth As Integer = Height / 2

		L_ALPHA_PCAD.Location = New Point(newWidth + 3, labelYLocation)
		DGV_ALPHA_Extra.Location = New Point(newWidth, DGVYLocation)
		DGV_ALPHA_Extra.Width = newWidth - leftAndRightPadding
		DGV_ALPHA_Extra.Height = Height - topAndBottomPadding

		DGV_ALPHA_Missing.Width = newWidth - leftAndRightPadding
		DGV_ALPHA_Missing.Height = Height - topAndBottomPadding
	End Sub

	Private Sub Close_Button_Click() Handles Close_Button.Click
		Close()
	End Sub

	Private Sub Excel_Button_Click() Handles Excel_Button.Click
		Dim report As New GenerateReport()
		'report.VerifyAssemblyReport(TB_BuildRefNo.Text, DataTable_ALPHAmissing, DataTable_ALPHAextra)
	End Sub

	
End Class