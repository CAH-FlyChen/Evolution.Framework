using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution.Framework.Excel
{
    public class ExcelLib
    {
        /// <summary> 获取Excel对象 </summary>
        /// <param name="filePath">Excel文件路径</param>
        /// <returns></returns>
        public static IExcel GetExcel(string filePath)
        {
            if (filePath.Trim() == "")
                throw new Exception("文件名不能为空");

            if (!filePath.Trim().EndsWith("xlsx"))
                throw new Exception("不支持该文件类型");


            if (filePath.Trim().EndsWith("xlsx"))
            {
                IExcel res = new Excel2007(filePath.Trim());
                return res;
            }
            else return null;
        }
    }
}
