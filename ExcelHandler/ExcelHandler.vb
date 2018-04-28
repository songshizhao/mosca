''---------------------------------------
'处理excel模块
'
''---------------------------------------

Public Class ExcelInstance
    Public Property Filepath As String
    ''' <summary>excel程序对象</summary>
    Public Property ExcelApp As Object
    ''' <summary>excel工作表集合（list of sheets）</summary>
    Public Property ExcelBook As Object
    ''' <summary>excel的一个sheet表 </summary>
    Public Property ExcelSheet As Object
    ''' <summary>excel的一个sheet表集合 </summary>
    Public Property ExcelSheets As List(Of Object) = New List(Of Object)()

    '默认的执行对象
    Sub ExcelHandler()

    End Sub
    ''' <summary>新建一个Excel文件，新建后excel App/sheet/book属性均设定</summary>
    Sub CreatNewExcel(path As String, sheetnames As List(Of String), Optional IsVisible As Boolean = True, Optional IsAlertDisplay As Boolean = False, Optional IsOverWritingAlert As Boolean = False)
        Try
            Filepath = path
            ExcelApp = CreateObject("Excel.Application")
            ExcelApp.DisplayAlerts = IsAlertDisplay
            ExcelApp.AlertBeforeOverwriting = IsOverWritingAlert
            ExcelApp.Visible = IsVisible
            ExcelBook = ExcelApp.Workbooks.add()
            For Each sheetname As String In sheetnames
                Debug.WriteLine(sheetname)
                ExcelSheet = ExcelBook.Worksheets.add()
                ExcelSheet.name = sheetname
                'sheet添加到属性集合sheets
                ExcelSheets.Add(ExcelSheet)
            Next

        Catch ex As Exception
            Debug.WriteLine(ex.Message)
        End Try
    End Sub

    ''' <summary>打开一个excel</summary>
    ''' <param name="visible">excel是否可见</param>
    Private Sub Open_Excel(path As String, sheetnames As List(Of String), Optional visible As Boolean = True)

        Try
            Filepath = path
            ExcelApp = CreateObject("Excel.Application")
            ExcelApp.Visible = visible
            ExcelBook = ExcelApp.Workbooks.open(Filepath)
            For Each sheetname In sheetnames
                ExcelSheet = ExcelBook.Worksheets(sheetname)
                'sheet添加到属性集合sheets
                ExcelSheets.Add(ExcelSheet)
            Next
        Catch ex As Exception

        End Try

    End Sub

    ''' <summary>
    ''' 保存当前excel
    ''' </summary>
    ''' <param name="path"></param>
    ''' <param name="IsAppShouldClose"></param>
    Sub SaveExcel(Optional path As String = "", Optional IsAppShouldClose As Boolean = True)

        Try
            For Each Excel_Sheet In ExcelSheets
                Excel_Sheet.Cells.EntireColumn.AutoFit()
            Next

            If (path = "") Then
                path = Filepath
            End If
            ExcelBook.SaveAs(path)

            If IsAppShouldClose Then
                CloseCurrentExcel()
            End If
        Catch ex As Exception

        End Try



    End Sub


    ''' <summary>关闭当前excel，释放对象</summary>
    Sub CloseCurrentExcel()
        If (ExcelApp IsNot Nothing) Then
            ExcelApp.quit()
            ExcelApp = Nothing
            ExcelBook = Nothing
            ExcelSheet = Nothing
        End If
    End Sub

    ''' <summary>
    ''' 向当前的excel-sheet对象中一个单元格中添加数据（T为泛型）
    ''' </summary>
    ''' <typeparam name="T">泛型</typeparam>
    ''' <param name="row"></param>
    ''' <param name="column"></param>
    ''' <param name="content"></param>
    Sub Output_On_Cell(Of T)(ExcelSheet As Object, row As Integer, column As Integer, content As T)



        '设置活动表
        ExcelSheet.Activate

        ExcelSheet.Cells(row, column).value = content


        'fr(i) = excel_sheet.
    End Sub

End Class
