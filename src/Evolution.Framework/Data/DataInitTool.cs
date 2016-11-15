using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Evolution.Framework.Data
{
    public class DataInitTool
    {
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
    }
}
