///*******************************************************************************
// * Copyright © 2016 Evolution.Framework 版权所有
// * Author: Evolution
// * Description: Evolution快速开发平台
// * Website：
//*********************************************************************************/
//using OfficeOpenXml;
//using OfficeOpenXml.Table;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;

//namespace Evolution.Framework
//{
//    public class ExcelHelper
//    {
//        #region 保存数据列表到Excel（泛型）+void SaveToExcel<T>(IEnumerable<T> data, string FileName, string OpenPassword = "")
//        /// <summary>
//        /// 保存数据列表到Excel（泛型）
//        /// </summary>
//        /// <typeparam name="T">集合数据类型</typeparam>
//        /// <param name="data">数据列表</param>
//        /// <param name="FileName">Excel文件</param>
//        /// <param name="OpenPassword">Excel打开密码</param>
//        public static void SaveToExcel<T>(IEnumerable<T> data, string FilePath, string OpenPassword = "")
//        {
//            try
//            {
//                MemoryStream ms = new MemoryStream();
//                using (ExcelPackage ep = new ExcelPackage())
//                {
//                    ExcelWorksheet ws = ep.Workbook.Worksheets.Add(typeof(T).Name);
//                    ws.Cells["A1"].LoadFromCollection(data, true, TableStyles.Medium10);
//                    //ws.Cells["A1"].Value = "abc";
//                    ep.SaveAs(ms);
//                }
//                FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate);
//                BinaryWriter w = new BinaryWriter(fs);
//                w.Write(ms.ToArray());
//                fs.Dispose();
//                ms.Dispose();
//            }
//            catch (InvalidOperationException ex)
//            {
//                Console.WriteLine(ex.Message);
//            }
//        }
//        #endregion

//        #region 从Excel中加载数据（泛型）+IEnumerable<T> LoadFromExcel<T>(string FileName) where T : new()
//        /// <summary>
//        /// 从Excel中加载数据（泛型）
//        /// </summary>
//        /// <typeparam name="T">每行数据的类型</typeparam>
//        /// <param name="FileName">Excel文件名</param>
//        /// <returns>泛型列表</returns>
//        #pragma warning disable 0168
//        private static IEnumerable<T> LoadFromExcel<T>(string FileName) where T : new()
//        {
//            FileInfo existingFile = new FileInfo(FileName);
//            List<T> resultList = new List<T>();
//            Dictionary<string, int> dictHeader = new Dictionary<string, int>();

//            using (ExcelPackage package = new ExcelPackage(existingFile))
//            {
//                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

//                int colStart = worksheet.Dimension.Start.Column;  //工作区开始列
//                int colEnd = worksheet.Dimension.End.Column;       //工作区结束列
//                int rowStart = worksheet.Dimension.Start.Row;       //工作区开始行号
//                int rowEnd = worksheet.Dimension.End.Row;       //工作区结束行号

//                //将每列标题添加到字典中
//                for (int i = colStart; i <= colEnd; i++)
//                {
//                    dictHeader[worksheet.Cells[rowStart, i].Value.ToString()] = i;
//                }

//                List<PropertyInfo> propertyInfoList = new List<PropertyInfo>(typeof(T).GetProperties());

//                for (int row = rowStart + 1; row < rowEnd; row++)
//                {
//                    T result = new T();

//                    //为对象T的各属性赋值
//                    foreach (PropertyInfo p in propertyInfoList)
//                    {
//                        try
//                        {
//                            ExcelRange cell = worksheet.Cells[row, dictHeader[p.Name]]; //与属性名对应的单元格

//                            if (cell.Value == null)
//                                continue;
//                            switch (p.PropertyType.Name.ToLower())
//                            {
//                                case "string":
//                                    p.SetValue(result, cell.GetValue<String>());
//                                    break;
//                                case "int16":
//                                    p.SetValue(result, cell.GetValue<Int16>());
//                                    break;
//                                case "int32":
//                                    p.SetValue(result, cell.GetValue<Int32>());
//                                    break;
//                                case "int64":
//                                    p.SetValue(result, cell.GetValue<Int64>());
//                                    break;
//                                case "decimal":
//                                    p.SetValue(result, cell.GetValue<Decimal>());
//                                    break;
//                                case "double":
//                                    p.SetValue(result, cell.GetValue<Double>());
//                                    break;
//                                case "datetime":
//                                    p.SetValue(result, cell.GetValue<DateTime>());
//                                    break;
//                                case "boolean":
//                                    p.SetValue(result, cell.GetValue<Boolean>());
//                                    break;
//                                case "byte":
//                                    p.SetValue(result, cell.GetValue<Byte>());
//                                    break;
//                                case "char":
//                                    p.SetValue(result, cell.GetValue<Char>());
//                                    break;
//                                case "single":
//                                    p.SetValue(result, cell.GetValue<Single>());
//                                    break;
//                                default:
//                                    break;
//                            }
//                        }
//                        catch (KeyNotFoundException ex)
//                        { }
//                    }
//                    resultList.Add(result);
//                }
//            }
//            return resultList;
//        }
//        #endregion
//    }
//}
