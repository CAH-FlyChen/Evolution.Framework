using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
namespace Evolution.Framework.Test
{
    public class Excel
    {
        public class TestModel
        {
            public TestModel(string name,int age)
            {
                this.Name = name;
                this.Age = age;
            }
            public string Name { get; set; }
            public int Age { get; set; }
        }
        [Fact]
        public void WriteToExcel()
        {
            List<TestModel> ms = new List<TestModel>()
            {
                new TestModel("zhangsan",18),
                new TestModel("lisi",20)
            };
            ExcelHelper.SaveToExcel<TestModel>(ms, "c:\\tmp\\abc.xlsx");
        }
    }
}
