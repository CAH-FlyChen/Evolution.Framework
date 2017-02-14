//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Evolution.Framework.Excel
//{
//    public interface IExcel
//    {
//        /// <summary> 打开文件 </summary>
//        bool Open(string sheetName);
//        /// <summary> 文件版本 </summary>
//        ExcelVersion Version { get; }
//        /// <summary> 文件路径 </summary>
//        string FilePath { get; set; }
//        /// <summary> 文件是否已经打开 </summary>
//        bool IfOpen { get; }
//        /// <summary> 文件包含工作表的数量 </summary>
//        int SheetCount { get; }
//        /// <summary> 当前工作表序号 </summary>
//        int CurrentSheetIndex { get; set; }
//        /// <summary> 获取当前工作表中行数 </summary>
//        int GetRowCount();
//        /// <summary> 获取当前工作表中列数 </summary>
//        int GetColumnCount();
//        /// <summary> 获取当前工作表中某一行中单元格的数量 </summary>
//        /// <param name="Row">行序号</param>
//        int GetCellCountInRow(int Row);
//        /// <summary> 获取当前工作表中某一单元格的值（按字符串返回） </summary>
//        /// <param name="Row">行序号</param>
//        /// <param name="Col">列序号</param>
//        string GetCellValue(int Row, int Col);
//        /// <summary> 关闭文件 </summary>
//        void Close();

//        bool ExistSheet(string sheetName);
//    }

//    public enum ExcelVersion
//    {
//        /// <summary> Excel2003之前版本 ,xls </summary>
//        Excel03,
//        /// <summary> Excel2007版本 ,xlsx  </summary>
//        Excel07
//    }
//}
