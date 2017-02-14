using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Npoi.Core.XSSF;
using Npoi.Core.SS.UserModel;
using Npoi.Core.XSSF.UserModel;
using System.Collections;

namespace Evolution.Framework.Data
{
    public class DataInitTool
    {
        public static IWorkbook workbook;
        public static void OpenExcel(string fileName,string webRootPath)
        {
            var pathToFile = webRootPath
                + Path.DirectorySeparatorChar.ToString()
                + "Data_Init"
                + Path.DirectorySeparatorChar.ToString()
                + fileName;
            try
            {
                FileStream fs = new FileStream(pathToFile, FileMode.Open);
                workbook = new XSSFWorkbook(fs);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public static void ProcessFile(string fileName, string webRootPath, Action<string[]> processCode)
        {
            var pathToFile = webRootPath
                            + Path.DirectorySeparatorChar.ToString()
                            + "Data_Init"
                            + Path.DirectorySeparatorChar.ToString()
                            + fileName;
            using (StreamReader sr = File.OpenText(pathToFile))
            {
                string firstLine = sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    string dataLine = sr.ReadLine();
                    string[] dataColum = dataLine.Split(',');
                    processCode(dataColum);
                }
            }
        }
        public static void ProcessSheet(string sheetName, Action<string[]> processCode)
        {
            ISheet sheet = workbook.GetSheet(sheetName);
            IRow headerRow = sheet.GetRow(0);
            int rowCount = sheet.LastRowNum;
            for(int i=1;i<=rowCount;i++)
            {
                IRow row = sheet.GetRow(i);
                if (row.GetCell(0).IsEmpty()||string.IsNullOrEmpty(row.GetCell(0).ToString())) continue;
                List<string> arr = new List<string>();
                for(int t=0;t<=headerRow.LastCellNum;t++)
                {
                    ICell cell = row.GetCell(t);
                    if (cell == null)
                        arr.Add("");
                    else
                        arr.Add(cell.ToString());
                    //if (cell.CellType == CellType.String)
                    //    arr.Add(cell.StringCellValue);
                    //else if (cell.CellType == CellType.Numeric)
                    //    arr.Add(cell.NumericCellValue.ToString());
                    //else if(cell.CellType == CellType.Formula)
                    //    arr.Add(cell.)
                }
                string[] dataColum = arr.ToArray();
                processCode(dataColum);
            }


            //using (StreamReader sr = File.OpenText(pathToFile))
            //{
            //    string firstLine = sr.ReadLine();
            //    while (!sr.EndOfStream)
            //    {
            //        string dataLine = sr.ReadLine();
            //        string[] dataColum = dataLine.Split(',');
            //        processCode(dataColum);
            //    }
            //}
        }
    }
}
