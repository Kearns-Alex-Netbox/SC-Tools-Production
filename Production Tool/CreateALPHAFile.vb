'-----------------------------------------------------------------------------------------------------------------------------------------
' Module: CreateALPHAFile.vb
'
' Description: Compares our PCAD file to our PNP file [if we have one] and ALPHA component information to make sure they are both the same.
'		Special ALPHA files are created through this menu and placed in a location where ALPHA can load it into it's memory. [Hard Code]
'   BOM -> ALPHA Components Tab 1: Compares all of the items found in the PCAD BOM to ALPHA's list of items to see f they exist. If they do
'		exist then they are checked to see if they are accociated with a package or not. Items not found and items with no package are
'		displayed to the user.
'   BOM -> ALPHA Magazines Tab 2: Checks to see if the items in the PCAD BOM are already in a magazine and which magazine slot and feeder
'		to not remove or change. Items that are not in a magazine are displayed to the user.
'   BOM -> PNP Tab 3: Compares our PCAD BOM to our PNP file to make sure it is one-to-one. Items that are not found are displayed to the
'		user.
'
' Buttons:
'	Refresh Mags = Looks in the directory that ALPHA exports to for a .mag file and compares it to the file in our release directory. If
'		the file is newer, it is then imported into the database for the user to use for the comparisions. This does not refresh the
'		report.
'	Generate ALPHA File = Takes the PNP file and turns it into a format that ALPHA can read into it's memory. This file is saved to the
'		directory where ALPHA can access it. [Hard Code] The format is very specific and needs to match exactly as ALPHA expects it.
'
' Status:
'	Out of Stock = We have a negitive (-) quantity after running the report. These items are need to be purchased to satisfy the report.
'	Reload Magazine = We have enough quantity for running the job, but not enough in the ALPHA magazine. These slots and feeders will 
'		need to be reloaded during the job at some point.
'	Not in Database = the item was not found in the database.
'-----------------------------------------------------------------------------------------------------------------------------------------
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic.FileIO

Public Class CreateALPHAFile

	Const MICROMETTER_CONVERTER As Integer = 25.4

	Dim DataTable_ALPHAmissing As New DataTable
	Dim DataTable_MissingPackage As New DataTable

	Dim DataTable_MagazineMissing As New DataTable
	Dim DataTable_MagazineKeep As New DataTable

	Dim DataTable_PNPmissing As New DataTable
	Dim DataTable_PNPextra As New DataTable

	Dim fu1X As Integer = 0
	Dim fu1Y As Integer = 0
	Dim fu2X As Integer = 0
	Dim fu2Y As Integer = 0
	Dim fu3X As Integer = 0
	Dim fu3Y As Integer = 0
	Dim boardRotation As Integer = 0
	Dim fiducialName As String = "Netbox.10"
	Dim PNPready As Boolean = False

	Dim numberToBuild As Integer = 0

	'Styles used to help see results on the datagrid.
	Dim OUT_COLOR As Color = Color.FromArgb(255, 151, 163)
	Dim ORDER_COLOR As Color = Color.FromArgb(255, 235, 156)
	Dim DATABASE_COLOR As Color = Color.Orange

	Private Sub CreateALPHAFile_Load() Handles MyBase.Load
		'Populate Board drop-down.
		GetBoardDropDownItems(CB_Board)
		If CB_Board.Items.Count <> 0 Then
			CB_Board.SelectedIndex = 0
		End If

		CB_Board.DropDownHeight = 200

		KeyPreview = True

		SetupDataTables()
		ResizeTables(TabPage1)
	End Sub

	Private Sub SetupDataTables()
		'PCAD <-> ALPHA Components
		DataTable_ALPHAmissing.Columns.Add(DB_HEADER_ITEM_NUMBER, GetType(String))
		DataTable_ALPHAmissing.Columns.Add(DB_HEADER_MPN, GetType(String))

		DataTable_MissingPackage.Columns.Add(DB_HEADER_ITEM_NUMBER, GetType(String))

		'PCAD <-> ALPHA Magazine
		DataTable_MagazineMissing.Columns.Add(DB_HEADER_ITEM_NUMBER, GetType(String))
		DataTable_MagazineMissing.Columns.Add(HEADER_QTY_AVAIL, GetType(String))
		DataTable_MagazineMissing.Columns.Add(HEADER_QTY_NEEDED, GetType(String))

		DataTable_MagazineKeep.Columns.Add(DB_HEADER_ITEM_NUMBER, GetType(String))
		DataTable_MagazineKeep.Columns.Add(DB_HEADER_NAME, GetType(String))
		DataTable_MagazineKeep.Columns.Add(DB_HEADER_SLOT_NUMBER, GetType(String))
		DataTable_MagazineKeep.Columns.Add(DB_HEADER_FEEDER_NUMBER, GetType(Integer))
		DataTable_MagazineKeep.Columns.Add(DB_HEADER_QUANTITY, GetType(Integer))
		DataTable_MagazineKeep.Columns.Add(HEADER_QTY_AVAIL, GetType(String))
		DataTable_MagazineKeep.Columns.Add(HEADER_QTY_NEEDED, GetType(String))

		'ALPHA <-> PNP
		DataTable_PNPmissing.Columns.Add(DB_HEADER_REF_DES, GetType(String))
		DataTable_PNPmissing.Columns.Add(DB_HEADER_ITEM_PREFIX, GetType(String))
		DataTable_PNPmissing.Columns.Add(DB_HEADER_ITEM_NUMBER, GetType(String))

		DataTable_PNPextra.Columns.Add(DB_HEADER_REF_DES, GetType(String))
		DataTable_PNPextra.Columns.Add(DB_HEADER_ITEM_PREFIX, GetType(String))
		DataTable_PNPextra.Columns.Add(DB_HEADER_ITEM_NUMBER, GetType(String))
	End Sub

	Private Sub Compare_Button_Click() Handles Compare_Button.Click
		Cursor = Cursors.WaitCursor
		If LOGDATA = True Then
			Try
				If ChangeCheck(True) = True Then
					GenerateReport()
				End If
			Catch ex As Exception
				UnhandledExceptionMessage(ex)
			End Try
		Else
			If ChangeCheck(True) = True Then
				GenerateReport()
			End If
		End If
		Cursor = Cursors.Default
	End Sub

	Private Sub GenerateALPHA_Button_Click() Handles GenerateALPHA_Button.Click
		Cursor = Cursors.WaitCursor
		If LOGDATA = True Then
			Try
				GenerateALPHA()
			Catch ex As Exception
				UnhandledExceptionMessage(ex)
			End Try
		Else
			GenerateALPHA()
		End If
		Cursor = Cursors.Default
	End Sub

	Private Sub GenerateReport()
		Dim message As String = ""
		If sqlapi.CheckDirtyBit(message) = True Then
			MsgBox(message)
			Return
		End If

		'Check that the stop level is a positive whole number
		Try
			numberToBuild = CInt(BuildNumber_TextBox.Text)

			If numberToBuild < 0 Then
				MsgBox("Please input a positive whole number for the number to build.")
				Return
			End If
		Catch ex As Exception
			MsgBox("Please input a positive whole number for the number to build.")
			Return
		End Try

		DataTable_ALPHAmissing.Clear()
		DataTable_MagazineMissing.Clear()
		DataTable_MagazineKeep.Clear()
		DataTable_PNPmissing.Clear()
		DataTable_PNPextra.Clear()
		DataTable_MissingPackage.Clear()

		If ParsePNPFile(CB_Board.Text) = False Then
			Return
		End If

		Compare(CB_Board.Text)

		If PNPready = True Then
			'GenerateALPHA_Button.Enabled = True
		End If

		FormatDGV()

		Excel_Button.Enabled = True
	End Sub

	Private Function ParsePNPFile(ByRef board As String) As Boolean
		PNPready = False

		'Clear the Database for the new PNP file that we are going to create.
		Dim myCmd As New SqlCommand("DELETE FROM " & TABLE_TEMP_PNP, myConn)
		myCmd.ExecuteNonQuery()
		Dim originalName As String = board
		Dim fileNameParsed() As String = originalName.Split(".")
		If fileNameParsed.Length < 4 Then
			MsgBox("The file you have selected does not parse into 5 parts (boardname).(Rev#).(#).(Option).(bom)" & vbNewLine & "Please check the name of your file for correct naming conventions.")
			Return False
		End If
		Dim fileName As String = fileNameParsed(INDEX_BOARD) & "." & fileNameParsed(INDEX_REVISION1) & "." & fileNameParsed(INDEX_REVISION2) & "." & fileNameParsed(INDEX_OPTION) & "."

		'Indexs
		Dim INDEX_refdes As Integer = -1
		Dim INDEX_stockNumber As Integer = -1
		Dim INDEX_PosX As Integer = -1
		Dim INDEX_PosY As Integer = -1
		Dim INDEX_Rotation As Integer = -1
		Dim INDEX_Process As Integer = -1
		Dim INDEX_Value As Integer = -1

		'Optional
		Dim INDEX_options As Integer = -1
		Dim INDEX_swap As Integer = -1

		Dim fu1_check As Integer = -1
		Dim fu2_check As Integer = -1
		Dim fu3_check As Integer = -1
		Dim br_check As Integer = -1
		Dim fn_check As Integer = -1

		'Start our transaction. Must assign both transaction object and connection to the command object for a pending local transaction.
		Dim transaction As SqlTransaction = Nothing
		transaction = myConn.BeginTransaction("Temp Transaction")
		myCmd.Connection = myConn
		myCmd.Transaction = transaction

		Try
			Dim foundIssue As Boolean = False
			Using myParser As New TextFieldParser(My.Settings.ReleaseLocation & "\PCAD\" & fileNameParsed(INDEX_BOARD) & "\" & fileNameParsed(INDEX_REVISION1) & "." & fileNameParsed(INDEX_REVISION2) & "\" & fileNameParsed(INDEX_BOARD) & "." & fileNameParsed(INDEX_REVISION1) & "." & fileNameParsed(INDEX_REVISION2) & "..pnp.csv")
				myParser.TextFieldType = FieldType.Delimited
				myParser.SetDelimiters(",")
				Dim currentRow As String()

				'First three rows are the header. We do not need any of this information.
				currentRow = myParser.ReadFields()
				currentRow = myParser.ReadFields()
				currentRow = myParser.ReadFields()
				Dim index As Integer = 0
				Dim missingFields As String = ""
				Dim fieldErrors As Boolean = False

				'Parse the header row to grab Indexs. They can be generated in any order.
				For Each header In currentRow
					Select Case header.ToLower
						Case "refdes"
							INDEX_refdes = index
						Case "stock number"
							INDEX_stockNumber = index
						Case "locationx"
							INDEX_PosX = index
						Case "locationy"
							INDEX_PosY = index
						Case "rotation"
							INDEX_Rotation = index
						Case "option"
							INDEX_options = index
						Case "process"
							INDEX_Process = index
						Case "swap"
							INDEX_swap = index
						Case "value"
							INDEX_Value = index
					End Select
					index += 1
				Next

				'Check to see if we are missing the important fields.
				If INDEX_refdes = -1 Then
					fieldErrors = True
					missingFields = missingFields & "refdes |"
				End If
				If INDEX_stockNumber = -1 Then
					fieldErrors = True
					missingFields = missingFields & " stockNumber |"
				End If
				If INDEX_PosX = -1 Then
					fieldErrors = True
					missingFields = missingFields & " locationX |"
				End If
				If INDEX_PosY = -1 Then
					fieldErrors = True
					missingFields = missingFields & " locationY |"
				End If
				If INDEX_Rotation = -1 Then
					fieldErrors = True
					missingFields = missingFields & " Rotation |"
				End If
				If INDEX_Process = -1 Then
					fieldErrors = True
					missingFields = missingFields & " Process |"
				End If
				If INDEX_Value = -1 Then
					fieldErrors = True
					missingFields = missingFields & " Value"
				End If

				If fieldErrors = True Then
					MsgBox("PNP File is missing the following Fields: " & missingFields)
					sqlapi.RollBack(transaction, errorMessage:=New List(Of String))
					Return False
				End If

				While Not myParser.EndOfData
					Dim referenceDesignator As String = ""
					Dim itemPrefix As String = ""
					Dim itemNumber As String = ""
					Dim posX As String = ""
					Dim posY As String = ""
					Dim rotation As String = ""
					Dim process As String = ""
					'Optional
					Dim optionValue As String = ""
					currentRow = myParser.ReadFields()

					If currentRow(0).Length = 0 Then
						Continue While
					End If

					'Check to see if we have the option field or not.
					If INDEX_options <> -1 Then
						Dim include As Boolean = False
						optionValue = currentRow(INDEX_options)

						'Check to see if we have an option.
						If optionValue.Length <> 0 Then
							For index = 0 To optionValue.Length - 1

								'Check each letter of the option feild to see if the file calls for it.
								If fileNameParsed(INDEX_OPTION).Contains(optionValue(index)) = True Then
									include = True
									Exit For
								End If
							Next
							If include = False Then
								Continue While
							End If
						End If
					End If

					'- - - Parse Reference Designator - - -

					If INDEX_swap <> -1 Then
						'Check to see if we have a swap.
						If currentRow(INDEX_swap).Length <> 0 Then
							referenceDesignator = currentRow(INDEX_swap)
						Else
							referenceDesignator = currentRow(INDEX_refdes)
						End If
					Else
						referenceDesignator = currentRow(INDEX_refdes)
					End If

					'Check to see if we are wroking with our FUs.
					Try
						'The 'FU' Reference Designator is very important. This is where we are storing all of the information
						'	that deals with our fiducial marks on the board. 
						'	We should not have any value with the exception of the FU1 where the board roation and fiducial name are found.
						'	Each should have an x,y coordinate.
						Select Case referenceDesignator
							Case "FU1"
								fu1_check = 0
								fu1X = currentRow(INDEX_PosX)
								fu1Y = currentRow(INDEX_PosY)
								If currentRow(INDEX_Value).Contains("|") = True Then
									Dim parsed() As String = currentRow(INDEX_Value).Split("|")
									For Each item In parsed
										Select Case item.Substring(0, 2).ToLower
											Case "br"
												br_check = 0
												boardRotation = item.Substring(2)
											Case "fn"
												fn_check = 0
												fiducialName = item.Substring(2)
										End Select
									Next
									Continue While
								Else
									MsgBox("FU1 '|' format")
									myParser.Close()
									transaction.Rollback()
									Return False
								End If
							Case "FU2"
								fu2_check = 0
								fu2X = currentRow(INDEX_PosX)
								fu2Y = currentRow(INDEX_PosY)
								Continue While
							Case "FU3"
								fu3_check = 0
								fu3X = currentRow(INDEX_PosX)
								fu3Y = currentRow(INDEX_PosY)
								Continue While
						End Select
					Catch ex As Exception

					End Try

					'- - - Parse Stock Number - - -

					If INDEX_stockNumber <> -1 Then
						itemNumber = currentRow(INDEX_stockNumber).Substring(currentRow(INDEX_stockNumber).IndexOf(":") + 1)

						'- - - Parse Prefix - - -

						'Check to see if we have a colon.
						If currentRow(INDEX_stockNumber).Contains(":") = True Then
							itemPrefix = currentRow(INDEX_stockNumber).Substring(0, currentRow(INDEX_stockNumber).IndexOf(":"))
						End If
					End If

					'- - - Parse X position - - -

					If INDEX_PosX <> -1 Then
						posX = currentRow(INDEX_PosX).Substring(0, currentRow(INDEX_PosX).IndexOf("."))
					End If

					'- - - Parse Y position - - -

					If INDEX_PosY <> -1 Then
						posY = currentRow(INDEX_PosY).Substring(0, currentRow(INDEX_PosY).IndexOf("."))
					End If

					'- - - Parse Rotation - - -

					If INDEX_Rotation <> -1 Then
						rotation = currentRow(INDEX_Rotation).Substring(0, currentRow(INDEX_Rotation).IndexOf("."))
					End If

					'- - - Parse Process - - -

					If INDEX_Process <> -1 Then
						process = currentRow(INDEX_Process)

						'Check to makes sure that we have only 'SMT' process'.
						If String.Compare(process, PROCESS_SMT, True) <> 0 Then
							If String.Compare(process, PROCESS_NOTUSED, True) <> 0 Then
								MsgBox(referenceDesignator & " Process is " & process)
								myParser.Close()
								transaction.Rollback()
								Return False
							Else
								Continue While
							End If
						End If
					End If

					myCmd.CommandText = "INSERT INTO " & TABLE_TEMP_PNP & " ([" & DB_HEADER_REF_DES & "], [" & DB_HEADER_ITEM_PREFIX & "], [" & DB_HEADER_ITEM_NUMBER & "], [" & DB_HEADER_POS_X & "], [" & DB_HEADER_POS_Y & "], [" & DB_HEADER_ROTATION & "], [" & DB_HEADER_PROCESS & "]) " &
										"VALUES('" & referenceDesignator & "','" & itemPrefix & "','" & itemNumber & "','" & posX & "','" & posY & "','" & rotation & "','" & process & "')"
					myCmd.ExecuteNonQuery()

				End While
			End Using

			'Check to see if we have found each of our Fiducial information parts
			'Without them we cannot create an alpha file.
			If br_check = -1 Or fn_check = -1 Or fu1_check = -1 Or fu2_check = -1 Or fu3_check = -1 Then
				Dim infostring As String = "Fiducial Information missing:"
				If br_check = -1 Then
					infostring = infostring & " BR"
				End If
				If fn_check = -1 Then
					infostring = infostring & " FN"
				End If
				If fu1_check = -1 Then
					infostring = infostring & " FU1"
				End If
				If fu2_check = -1 Then
					infostring = infostring & " FU2"
				End If
				If fu3_check = -1 Then
					infostring = infostring & " FU3"
				End If

				MsgBox(infostring)
				transaction.Rollback()
				Return False
			Else
				PNPready = True
			End If

			transaction.Commit()
		Catch ex As Exception
			If Not transaction Is Nothing Then
				sqlapi.RollBack(transaction, errorMessage:=New List(Of String))
				DataTable_ALPHAmissing.Rows.Add(ex.Message)
				DGV_ALPHA_Missing.DataSource = Nothing
				DGV_ALPHA_Missing.DataSource = DataTable_ALPHAmissing

				DGV_Magazine_Missing.DataSource = Nothing
				DGV_Magazine_Missing.DataSource = DataTable_ALPHAmissing

				DGV_Magazine_Keep.DataSource = Nothing
				DGV_Magazine_Keep.DataSource = DataTable_ALPHAmissing

				DGV_PNP_Missing.DataSource = Nothing
				DGV_PNP_Missing.DataSource = DataTable_ALPHAmissing

				DGV_PNP_Extra.DataSource = Nothing
				DGV_PNP_Extra.DataSource = DataTable_ALPHAmissing

				DGV_Missing_Package.DataSource = Nothing
				DGV_Missing_Package.DataSource = DataTable_ALPHAmissing
				Return False
			End If
		End Try
		Return True
	End Function

	Private Sub Compare(ByRef board As String)
		Dim myCmd As New SqlCommand("", myConn)

		'Check for the PCB Qty. If we do Not have enough Or "Not in Database" then put this record in the "missing table"
		'Else, put it in the "do not touch table".
		myCmd.CommandText = "SELECT [" & DB_HEADER_ITEM_NUMBER & "] FROM " & TABLE_PCADBOM & " WHERE [" & DB_HEADER_ITEM_PREFIX & "] ='" & PREFIX_PCB & "' AND [" & DB_HEADER_BOARD_NAME & "] ='" & CB_Board.Text & "'"
		Dim itemNumber As String = myCmd.ExecuteScalar

		myCmd.CommandText = "IF EXISTS(SELECT * FROM " & TABLE_QB_ITEMS & " WHERE [" & DB_HEADER_ITEM_NUMBER & "] = '" & itemNumber & "') SELECT [" & DB_HEADER_QUANTITY & "] FROM " & TABLE_QB_ITEMS & " WHERE [" & DB_HEADER_ITEM_NUMBER & "] = '" & itemNumber & "' ELSE SELECT '-1'"
		Dim totalPCBQty As String = myCmd.ExecuteScalar

		If totalPCBQty = -1 Then
			DataTable_MagazineMissing.Rows.Add(itemNumber, CInt(totalPCBQty), numberToBuild)
		Else
			If totalPCBQty < numberToBuild Then
				DataTable_MagazineMissing.Rows.Add(itemNumber, CInt(totalPCBQty), numberToBuild)
			Else
				DataTable_MagazineKeep.Rows.Add(itemNumber, Nothing, Nothing, Nothing, CInt(totalPCBQty), CInt(totalPCBQty), numberToBuild)
			End If
		End If

		'Set up all of the tables from the Database that we need to use so we do not
		' keep hitting the database for each item for each check.
		myCmd.CommandText = "SELECT * FROM " & TABLE_PCADBOM & " WHERE [" & DB_HEADER_BOARD_NAME & "] = '" & board & "' AND [" & DB_HEADER_PROCESS & "] = '" & PROCESS_SMT & "' ORDER BY [" & DB_HEADER_ITEM_NUMBER & "]"
		Dim PCAD_BOM As New DataTable()
		PCAD_BOM.Load(myCmd.ExecuteReader)

		myCmd.CommandText = "SELECT * FROM " & TABLE_TEMP_PNP & " ORDER BY [" & DB_HEADER_REF_DES & "]"
		Dim Temp_PNP As New DataTable()
		Temp_PNP.Load(myCmd.ExecuteReader)

		myCmd.CommandText = "SELECT * FROM " & TABLE_MAGAZINE_DATA & " WHERE [" & DB_HEADER_MACHINE_NUMBER & "] <> '0' ORDER BY [" & DB_HEADER_SLOT_NUMBER & "], [" & DB_HEADER_FEEDER_NUMBER & "]"
		Dim Mag_Data As New DataTable()
		Mag_Data.Load(myCmd.ExecuteReader)

		myCmd.CommandText = "SELECT * FROM " & TABLE_QB_ITEMS
		Dim QB_Items As New DataTable()
		QB_Items.Load(myCmd.ExecuteReader)

		myCmd.CommandText = "SELECT * FROM " & TABLE_ALPHA_ITEMS
		Dim ALPHA_Items As New DataTable()
		ALPHA_Items.Load(myCmd.ExecuteReader)

		'-----------------------------------------------------'
		'                                                     '
		'   Check for components that are missing in ALPHA.   '
		'                                                     '
		'-----------------------------------------------------'
		For Each dsRow As DataRow In PCAD_BOM.Rows
			Dim dr() As DataRow = ALPHA_Items.Select("[" & DB_HEADER_ITEM_NUMBER & "] = '" & dsRow(DB_HEADER_ITEM_NUMBER) & "'")
			'Check to see if the Item Number is found in ALPHA Items.
			If dr.Length = 0 Then
				'Check to see that we have not added it to the dataTable already.
				If DataTable_ALPHAmissing.Select("[" & DB_HEADER_ITEM_NUMBER & "] = '" & dsRow(DB_HEADER_ITEM_NUMBER) & "'").Length = 0 Then
					DataTable_ALPHAmissing.Rows.Add(dsRow(DB_HEADER_ITEM_NUMBER), dsRow(DB_HEADER_MPN))
					DataTable_MissingPackage.Rows.Add(dsRow(DB_HEADER_ITEM_NUMBER))
				End If
			Else
				'Check to see that we have not added it to the dataTable already.
				If dr(0)(DB_HEADER_PACKAGE).ToString.Length = 0 And DataTable_MissingPackage.Select("[" & DB_HEADER_ITEM_NUMBER & "] = '" & dsRow(DB_HEADER_ITEM_NUMBER) & "'").Length = 0 Then
					DataTable_MissingPackage.Rows.Add(dsRow(DB_HEADER_ITEM_NUMBER))
				End If
			End If

			'-----------------------------------------------'
			'                                               '
			'   Check for components inside the magazine.   '
			'                                               '
			'-----------------------------------------------'

			dr = Mag_Data.Select("[" & DB_HEADER_MACHINE_NUMBER & "] <> '0' AND [" & DB_HEADER_ITEM_NUMBER & "] = '" & dsRow(DB_HEADER_ITEM_NUMBER) & "'")
			If dr.Length = 0 Then
				'Check to see that we have not added it to the dataTable already.
				If DataTable_MagazineMissing.Select("[" & DB_HEADER_ITEM_NUMBER & "] = '" & dsRow(DB_HEADER_ITEM_NUMBER) & "'").Length = 0 Then
					'Grab the  QB information to go along with the row
					myCmd.CommandText = "IF EXISTS(SELECT * FROM " & TABLE_QB_ITEMS & " WHERE [" & DB_HEADER_ITEM_NUMBER & "] = '" & dsRow(DB_HEADER_ITEM_NUMBER) & "') SELECT [" & DB_HEADER_QUANTITY & "] FROM " & TABLE_QB_ITEMS & " WHERE [" & DB_HEADER_ITEM_NUMBER & "] = '" & dsRow(DB_HEADER_ITEM_NUMBER) & "' ELSE SELECT '-1'"
					Dim totalQty As String = myCmd.ExecuteScalar

					dr = PCAD_BOM.Select("[" & DB_HEADER_ITEM_NUMBER & "] = '" & dsRow(DB_HEADER_ITEM_NUMBER) & "'")
					Dim totalOnBoard As String = dr.Length

					Dim totalNeeded As String = totalOnBoard * numberToBuild

					DataTable_MagazineMissing.Rows.Add(dsRow(DB_HEADER_ITEM_NUMBER), CInt(totalQty), totalNeeded)
				End If
			End If

			'-------------------------------------------------------'
			'                                                       '
			'   Check for components that are missing in the PNP.   '
			'                                                       '
			'-------------------------------------------------------'

			'Check to see if the Stock Number exists.
			dr = Temp_PNP.Select("[" & DB_HEADER_ITEM_NUMBER & "] = '" & dsRow(DB_HEADER_ITEM_NUMBER) & "' AND [" & DB_HEADER_REF_DES & "] = '" & dsRow(DB_HEADER_REF_DES) & "' AND [" & DB_HEADER_ITEM_PREFIX & "] = '" & dsRow(DB_HEADER_ITEM_PREFIX) & "'")
			If dr.Length = 0 Then
				DataTable_PNPmissing.Rows.Add(dsRow(DB_HEADER_REF_DES), dsRow(DB_HEADER_ITEM_PREFIX), dsRow(DB_HEADER_ITEM_NUMBER))
			End If
		Next

		'------------------------------------------------------------'
		'                                                            '
		'   Check for components that are already in the magazine.   '
		'                                                            '
		'------------------------------------------------------------'
		For Each dsRow As DataRow In Mag_Data.Rows
			'Check to see if the Stock Number exists.
			Dim dr() As DataRow = PCAD_BOM.Select("[" & DB_HEADER_ITEM_NUMBER & "] = '" & dsRow(DB_HEADER_ITEM_NUMBER) & "'")

			If dr.Length <> 0 Then
				'Grab the  QB information to go along with the row
				myCmd.CommandText = "IF EXISTS(SELECT * FROM " & TABLE_QB_ITEMS & " WHERE [" & DB_HEADER_ITEM_NUMBER & "] = '" & dsRow(DB_HEADER_ITEM_NUMBER) & "') SELECT [" & DB_HEADER_QUANTITY & "] FROM " & TABLE_QB_ITEMS & " WHERE [" & DB_HEADER_ITEM_NUMBER & "] = '" & dsRow(DB_HEADER_ITEM_NUMBER) & "' ELSE SELECT '-1'"
				Dim totalQty As String = myCmd.ExecuteScalar

				Dim totalOnBoard As String = dr.Length

				Dim totalNeeded As String = totalOnBoard * numberToBuild

				DataTable_MagazineKeep.Rows.Add(dsRow(DB_HEADER_ITEM_NUMBER), dsRow(DB_HEADER_NAME), dsRow(DB_HEADER_SLOT_NUMBER), dsRow(DB_HEADER_FEEDER_NUMBER), dsRow(DB_HEADER_QUANTITY), CInt(totalQty), totalNeeded)
			End If
		Next

		'-----------------------------------------------------'
		'                                                     '
		'   Check for components that are extra in the PNP.   '
		'                                                     '
		'-----------------------------------------------------'
		For Each dsRow As DataRow In Temp_PNP.Rows
			'Check to see if the Stock Number exists.
			Dim dr() As DataRow = PCAD_BOM.Select("[" & DB_HEADER_ITEM_NUMBER & "] = '" & dsRow(DB_HEADER_ITEM_NUMBER) & "' AND [" & DB_HEADER_REF_DES & "] = '" & dsRow(DB_HEADER_REF_DES) & "' AND [" & DB_HEADER_ITEM_PREFIX & "] = '" & dsRow(DB_HEADER_ITEM_PREFIX) & "'")
			If dr.Length = 0 Then
				DataTable_PNPextra.Rows.Add(dsRow(DB_HEADER_REF_DES), dsRow(DB_HEADER_ITEM_PREFIX), dsRow(DB_HEADER_ITEM_NUMBER))
			End If
		Next

		'Check to see if we added any items to the table. If not, then they are the same.
		If DataTable_ALPHAmissing.Rows.Count = 0 Then
			DataTable_ALPHAmissing.Rows.Add("There are no missing components in ALPHA Components.")
		End If
		If DataTable_MissingPackage.Rows.Count = 0 Then
			DataTable_MissingPackage.Rows.Add("All ALPHA Components have a package.")
		End If
		If DataTable_MagazineMissing.Rows.Count = 0 Then
			DataTable_MagazineMissing.Rows.Add("There are no missing components in ALPHA's Magazines.")
		End If
		If DataTable_MagazineKeep.Rows.Count = 0 Then
			DataTable_MagazineKeep.Rows.Add("There are no extra components in ALPHA's Magazines.")
		End If
		If DataTable_PNPmissing.Rows.Count = 0 Then
			DataTable_PNPmissing.Rows.Add("There are no missing components in the PNP File.")
		End If
		If DataTable_PNPextra.Rows.Count = 0 Then
			DataTable_PNPextra.Rows.Add("There are no extra components in the PNP File.")
		End If

		DGV_ALPHA_Missing.DataSource = Nothing
		DGV_ALPHA_Missing.DataSource = DataTable_ALPHAmissing

		DGV_Missing_Package.DataSource = Nothing
		DGV_Missing_Package.DataSource = DataTable_MissingPackage

		DGV_Magazine_Missing.DataSource = Nothing
		DGV_Magazine_Missing.DataSource = DataTable_MagazineMissing

		DGV_Magazine_Keep.DataSource = Nothing
		DataTable_MagazineKeep.DefaultView.Sort = "[" & DB_HEADER_SLOT_NUMBER & "] ASC, [" & DB_HEADER_FEEDER_NUMBER & "] ASC"
		DataTable_MagazineKeep = DataTable_MagazineKeep.DefaultView.ToTable
		DGV_Magazine_Keep.DataSource = DataTable_MagazineKeep

		DGV_PNP_Missing.DataSource = Nothing
		DGV_PNP_Missing.DataSource = DataTable_PNPmissing

		DGV_PNP_Extra.DataSource = Nothing
		DGV_PNP_Extra.DataSource = DataTable_PNPextra
	End Sub

	Private Sub Close_Button_Click() Handles Close_Button.Click
		Close()
	End Sub

	Private Sub GenerateALPHA()
		Dim message As String = ""
		If sqlapi.CheckDirtyBit(message) = True Then
			MsgBox(message)
			Return
		End If

		Dim myReader As SqlDataReader = Nothing
		Dim reportList As New List(Of String)

		'Create the header for the ALPHA file.
		reportList.Add("# *** PCBS ***")
		reportList.Add("F1 " & CB_Board.Text)
		reportList.Add("F21 All_Tools")

		'X/Y offset (also known as X/Y coordinents for fiducial mark 1).
		Dim AX As Integer = fu1X
		Dim AY As Integer = fu1Y

		'Variables used to set what the new X/Y coordinent will be after calculating offset.
		Dim newX As Integer = 0
		Dim newY As Integer = 0

		newX = RotationX(boardRotation, fu1X, AX, fu1Y, AY)
		newY = RotationY(boardRotation, fu1X, AX, fu1Y, AY)

		'These should equal 0.
		Dim newfu1X = newX
		Dim newfu1Y = newY

		newX = RotationX(boardRotation, fu2X, AX, fu2Y, AY)
		newY = RotationY(boardRotation, fu2X, AX, fu2Y, AY)

		Dim newfu2X = newX
		Dim newfu2Y = newY

		newX = RotationX(boardRotation, fu3X, AX, fu3Y, AY)
		newY = RotationY(boardRotation, fu3X, AX, fu3Y, AY)

		Dim newfu3X = newX
		Dim newfu3Y = newY

		'Add the fiducial marks to the ALPHA file.
		reportList.Add("F3 " & newfu1X & " " & newfu1Y & " " & fiducialName)
		reportList.Add("F3 " & newfu2X & " " & newfu2Y & " " & fiducialName)
		reportList.Add("F3 " & newfu3X & " " & newfu3Y & " " & fiducialName)

		'Create and add our query into our data set for comparison.
		Dim myCmd As New SqlCommand("SELECT * FROM " & TABLE_TEMP_PNP & " ORDER BY [" & DB_HEADER_REF_DES & "]", myConn)
		Dim Temp_PNP As New DataTable()
		Temp_PNP.Load(myCmd.ExecuteReader)

		'Get new locations for all of the parts.
		For Each dsRow As DataRow In Temp_PNP.Rows
			newX = RotationX(boardRotation, dsRow(DB_HEADER_POS_X), AX, dsRow(DB_HEADER_POS_Y), AY)
			newY = RotationY(boardRotation, dsRow(DB_HEADER_POS_X), AX, dsRow(DB_HEADER_POS_Y), AY)

			'Add the new information to the ALPHA file.
			'F8 is where we put the X location, Y location, rotation, group, mount-skip, dispense-skip, component
			'F9 is wehre we put the reference designator.
			'Hard coded rotation '0' to force manual check of the pnp with each part.
			reportList.Add("F8 " & newX & " " & newY & " 0 0 N N " & dsRow(DB_HEADER_ITEM_NUMBER))
			reportList.Add("F9 " & dsRow(DB_HEADER_REF_DES))
		Next

		Dim report As New GenerateReport()
		report.GenerateALPHAfile(reportList, CB_Board.Text)
	End Sub

	Private Function RotationX(ByRef bR As Integer, ByRef posX As Integer, ByRef aX As Integer,
								ByRef posY As Integer, ByRef aY As Integer) As Integer
		Dim answer As Integer

		'Depending on our rotation of the board, we need to use the correct formula to change where the new X position will be.
		'aX/aY are the offsets from the fiducial mark.
		Select Case bR
			Case 0
				answer = (posX - aX) * MICROMETTER_CONVERTER
			Case 90
				answer = (-posY + aY) * MICROMETTER_CONVERTER
			Case 180
				answer = (-posX + aX) * MICROMETTER_CONVERTER
			Case 270
				answer = (posY - aY) * MICROMETTER_CONVERTER
		End Select
		Return answer
	End Function

	Private Function RotationY(ByRef bR As Integer, ByRef posX As Integer, ByRef aX As Integer,
								ByRef posY As Integer, ByRef aY As Integer) As Integer
		Dim answer As Integer

		'Depending on our rotation of the board, we need to use the correct formula to change where the new Y position will be.
		'aX/aY are the offsets from the fiducial mark.
		Select Case bR
			Case 0
				answer = (posY - aY) * MICROMETTER_CONVERTER
			Case 90
				answer = (posX - aX) * MICROMETTER_CONVERTER
			Case 180
				answer = (-posY + aY) * MICROMETTER_CONVERTER
			Case 270
				answer = (-posX + aX) * MICROMETTER_CONVERTER
		End Select
		Return answer
	End Function

	Private Sub Excel_Button_Click() Handles Excel_Button.Click
		Dim report As New GenerateReport
		report.GenerateALPHAfileReport(CB_Board.Text, DataTable_ALPHAmissing, DataTable_MissingPackage, DataTable_MagazineMissing, DataTable_MagazineKeep, numberToBuild & ": " & CB_Board.Text, DataTable_PNPmissing, DataTable_PNPextra)
	End Sub

	Sub GetBoardDropDownItems(ByRef box As ComboBox)
		Dim BoardNames As New DataTable()
		Dim myCmd As New SqlCommand("SELECT Distinct([" & DB_HEADER_BOARD_NAME & "]) FROM " & TABLE_PCADBOM & " ORDER BY [" & DB_HEADER_BOARD_NAME & "]", myConn)

		BoardNames.Load(myCmd.ExecuteReader)

		For Each dr As DataRow In BoardNames.Rows
			box.Items.Add(dr(DB_HEADER_BOARD_NAME))
		Next
	End Sub

	Private Sub CB_Board_SelectedValueChanged() Handles CB_Board.SelectedValueChanged
		GenerateALPHA_Button.Enabled = False
		Excel_Button.Enabled = False
	End Sub

	Private Sub PCADtoALPHA_Resize() Handles Me.Resize
		ResizeTables(TabControl1.SelectedTab)
	End Sub

	Public Sub ResizeTables(ByRef Tab As TabPage)
		'Math done to get all of the datagrids on each of the tabs centered. All hard-coded numbers are fine adjustment buffers.
		Dim topAndBottomPadding As Integer = 36      'Both top and bottom of the table get about 18 px of padding.
		Dim leftAndRightPadding As Integer = 13      'Both left and right of the table get about 6 px of padding.
		Dim labelYLocation As Integer = 3
		Dim DGVYLocation As Integer = 26

		Dim newWidth As Integer = Tab.Width / 2

		'Tab Page 1
		DGV_ALPHA_Missing.Width = newWidth - leftAndRightPadding
		DGV_ALPHA_Missing.Height = Tab.Height - topAndBottomPadding

		L_NoPackage.Location = New Point(newWidth + 3, labelYLocation)
		DGV_Missing_Package.Location = New Point(newWidth, DGVYLocation)
		DGV_Missing_Package.Width = newWidth - leftAndRightPadding
		DGV_Missing_Package.Height = Tab.Height - topAndBottomPadding

		'Tab Page 2
		L_DoNotTouch.Location = New Point(newWidth + 3, labelYLocation)
		DGV_Magazine_Keep.Location = New Point(newWidth, DGVYLocation)
		DGV_Magazine_Keep.Width = newWidth - leftAndRightPadding
		DGV_Magazine_Keep.Height = Tab.Height - topAndBottomPadding

		DGV_Magazine_Missing.Width = newWidth - leftAndRightPadding
		DGV_Magazine_Missing.Height = Tab.Height - topAndBottomPadding

		'Tab Page 3
		L_ExtraPNP.Location = New Point(newWidth + 3, labelYLocation)
		DGV_PNP_Extra.Location = New Point(newWidth, DGVYLocation)
		DGV_PNP_Extra.Width = newWidth - leftAndRightPadding
		DGV_PNP_Extra.Height = Tab.Height - topAndBottomPadding

		DGV_PNP_Missing.Width = newWidth - leftAndRightPadding
		DGV_PNP_Missing.Height = Tab.Height - topAndBottomPadding

		TabControl1.Refresh()
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

	Private Sub FormatDGV()
		'Go through the DGV and highlight the different alerts that we want the user to be able to see right away.

		'Not in Magazine table.
		For index = 0 To DGV_Magazine_Missing.Rows.Count - 1
			If DGV_Magazine_Missing.Rows(index).Cells(DB_HEADER_ITEM_NUMBER).Value = "There are no missing components in ALPHA's Magazines." Then
				Continue For
			End If
			'First Check to see if we are "Not in database"
			If DGV_Magazine_Missing.Rows(index).Cells(HEADER_QTY_AVAIL).Value = -1 Then
				DGV_Magazine_Missing.Rows(index).DefaultCellStyle.BackColor = DATABASE_COLOR
			Else
				'Check to see if we will be short.
				If CInt(DGV_Magazine_Missing.Rows(index).Cells(HEADER_QTY_AVAIL).Value) < CInt(DGV_Magazine_Missing.Rows(index).Cells(HEADER_QTY_NEEDED).Value) Then
					DGV_Magazine_Missing.Rows(index).DefaultCellStyle.BackColor = OUT_COLOR
				End If
			End If
		Next

		'Already in Magazine table.
		For index = 0 To DGV_Magazine_Keep.Rows.Count - 1
			'First Check to see if we are "Not in database"
			If DGV_Magazine_Keep.Rows(index).Cells(HEADER_QTY_AVAIL).Value = -1 Then
				DGV_Magazine_Keep.Rows(index).DefaultCellStyle.BackColor = DATABASE_COLOR
			Else
				'Second Check to see if we need to swap out during the build.
				If CInt(DGV_Magazine_Keep.Rows(index).Cells(DB_HEADER_QUANTITY).Value) < CInt(DGV_Magazine_Keep.Rows(index).Cells(HEADER_QTY_NEEDED).Value) Then
					DGV_Magazine_Keep.Rows(index).DefaultCellStyle.BackColor = ORDER_COLOR
				End If

				'Third Check to see if we will be short
				If CInt(DGV_Magazine_Keep.Rows(index).Cells(HEADER_QTY_AVAIL).Value) < CInt(DGV_Magazine_Keep.Rows(index).Cells(HEADER_QTY_NEEDED).Value) Then
					DGV_Magazine_Keep.Rows(index).DefaultCellStyle.BackColor = OUT_COLOR
				End If
			End If
		Next
	End Sub

	Private Sub TabPage2_Enter() Handles TabPage2.Enter
		FormatDGV()
	End Sub

End Class