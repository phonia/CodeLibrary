using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using EXCEL = Microsoft.Office.Interop.Excel;

namespace ExcelHelper
{
    public class ExcelComHelper:IDisposable
    {
        private EXCEL.Application _app = new EXCEL.Application();
        private EXCEL.Workbook _workbook = null;

        /// <summary>
        /// 将表格数据导入到DataTable
        /// </summary>
        /// <param name="filePath">表格路径</param>
        /// <param name="index">表格索引</param>
        /// <param name="header">表头索引</param>
        /// <returns></returns>
        public  DataTable InputExcelData(string filePath, int index = 1, int header = 1)
        {
            EXCEL.Sheets sheets = null;
            _workbook = _app.Workbooks.Open(Filename: filePath);
            sheets = _workbook.Sheets;
            EXCEL.Worksheet worksheet = (EXCEL.Worksheet)sheets.get_Item(index);
            int iRowCount = worksheet.UsedRange.Rows.Count;
            int iColCount = worksheet.UsedRange.Columns.Count;

            EXCEL.Range range = null;
            DataTable dt = new DataTable();
            for (int i = 1; i <= iColCount; i++)
            {
                range = (EXCEL.Range)worksheet.Cells[RowIndex: header, ColumnIndex: i];
                DataColumn dc = new DataColumn();
                dc.DataType = typeof(System.String);
                dc.ColumnName = range.Text.ToString().Trim();
                dt.Columns.Add(dc);
            }

            for (int i = header + 1; i <= iRowCount; i++)
            {
                DataRow dr = dt.NewRow();
                for (int j = 1; j <= iColCount; j++)
                {
                    range = (EXCEL.Range)worksheet.Cells[RowIndex: i, ColumnIndex: j];
                    dr[j - 1] = range.Value2 == null ? "" : range.Text.ToString().Trim();
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }

        /// <summary>
        /// 数据导出到表格涉及表格格式等问题 此处方法仅作为样板存在
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="path">存储路径</param>
        /// <param name="sheetName">表名</param>
        /// <param name="showHeader">是否有表头</param>
        /// <param name="headers">表头名</param>
        /// <returns></returns>
        public bool OutputToExcel(DataTable dt,string path,string sheetName=null,bool showHeader=true,List<string> headers=null)
        {
            if (dt == null||dt.Columns.Count<=0) return false;
            _workbook = _app.Workbooks.Add(EXCEL.XlWBATemplate.xlWBATWorksheet);
            EXCEL.Worksheet workSheet = (EXCEL.Worksheet)_workbook.Worksheets[1];
            EXCEL.Range range = null;

            

            int iColCount = dt.Columns.Count;
            int iRow = 1;
            int iCol = 1;
            if (!String.IsNullOrEmpty(sheetName))
            {
                range = workSheet.get_Range(workSheet.Cells[iRow, 1], workSheet.Cells[RowIndex: iRow, ColumnIndex: iColCount]);
                range.Merge(true);
                range.Value2 = sheetName;
                range.HorizontalAlignment = EXCEL.XlHAlign.xlHAlignCenter;
                range.VerticalAlignment = EXCEL.XlVAlign.xlVAlignCenter;
                iRow++;
            }

            if (showHeader == true)
            {
                for (int i = 1; i <= iColCount ||(headers!=null&&i<=headers.Count); i++)
                {
                    range = (EXCEL.Range)workSheet.Cells[iRow, i];
                    range.Value2 = 
                        headers != null && i <= headers.Count ? headers[i-1] :
                        i <= iColCount ? dt.Columns[i-1].ColumnName : "";
                    range.HorizontalAlignment = EXCEL.XlHAlign.xlHAlignCenter;
                    range.VerticalAlignment = EXCEL.XlVAlign.xlVAlignCenter;
                }
                iRow++;
            }

            //设置表格格式
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    range = (EXCEL.Range)workSheet.Cells[RowIndex: i + iRow, ColumnIndex: j+1];
                    range.Value2 = dt.Rows[i][j].ToString();
                    range.HorizontalAlignment = EXCEL.XlHAlign.xlHAlignCenter;
                    range.VerticalAlignment = EXCEL.XlVAlign.xlVAlignCenter;
                }
            }

            _workbook.SaveAs(Filename: path);

            return true;
        }

        public void Dispose()
        {
            if (_workbook != null)
            {
                _workbook.Close(SaveChanges: false);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(_workbook);
                _workbook = null;
            }
            _app.Workbooks.Close();
            _app.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(_app);
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
