using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using System.IO;

namespace Evolution.Framework.Excel
{
    public class Excel2007:IExcel
    {
        public Excel2007(string path)
        { filePath = path; }

        private string filePath = "";
        private ExcelWorkbook book = null;
        private int sheetCount = 0;
        private bool ifOpen = false;
        private int currentSheetIndex = 0;
        private ExcelWorksheet currentSheet = null;
        private ExcelPackage ep = null;

        public bool Open(string sheetName)
        {
            try
            {
                ep = new ExcelPackage(new FileInfo(filePath));
                
                if (ep == null) return false;
                book =ep.Workbook;
                sheetCount = book.Worksheets.Count;
                currentSheetIndex = 0;
                currentSheet = book.Worksheets[sheetName];
                ifOpen = true;
            }
            catch (Exception ex)
            {
                throw new Exception("打开文件失败，详细信息：" + ex.Message);
            }
            return true;
        }

        public void Close()
        {
            if (!ifOpen || ep == null) return;
            ep.Dispose();
        }


        public bool ExistSheet(string sheetName)
        {
            int c = book.Worksheets.Where(t => t.Name == sheetName).Count();
            return c > 0;
        }


        public ExcelVersion Version
        { get { return ExcelVersion.Excel07; } }

        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }

        public bool IfOpen
        { get { return ifOpen; } }

        public int SheetCount
        { get { return sheetCount; } }

        public int CurrentSheetIndex
        {
            get  { return currentSheetIndex; }
            set
            {
                if (value != currentSheetIndex)
                {
                    if (value >= sheetCount)
                        throw new Exception("工作表序号超出范围");
                    currentSheetIndex = value;
                    currentSheet =book.Worksheets[currentSheetIndex+1];
                }
            }
        }

        public int GetRowCount()
        {
            if (currentSheet == null) return 0;
            return currentSheet.Dimension.End.Row;
        }

        public int GetColumnCount()
        {
            if (currentSheet == null) return 0;
            return currentSheet.Dimension.End.Column;
        }

        public int GetCellCountInRow(int Row)
        {
            if (currentSheet == null) return 0;
            if (Row >= currentSheet.Dimension.End.Row) return 0;
            return currentSheet.Dimension.End.Column;
        }

        public string GetCellValue(int Row, int Col)
        {
            if (currentSheet == null) return "";
            if (Row >= currentSheet.Dimension.End.Row || Col >= currentSheet.Dimension.End.Column) return "";
            object tmpO =currentSheet.GetValue(Row + 1, Col + 1);
            if (tmpO == null) return "";
            return tmpO.ToString();
        }        
    }
}
