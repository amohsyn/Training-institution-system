using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using TrainingIS_UI_Tests.Services.GAppDataTables;

namespace TrainingIS_UI_Tests.Services
{
    public class GAppDataTable
    {
        private IWebDriver b;

        private string Edit_Link_Path_Format { get; }
        public List<GAppLineDataTable> Lines { get; set; }

        public GAppDataTable(IWebDriver b)
        {
            this.b = b;
           
           
        }

        public void FindElements(string Html_Id = "")
        {
            var Lines_Elements = b.FindElements(By.XPath("//Table[@id='dataTable']/tbody/tr"));
            Lines = new List<GAppLineDataTable>();


            int line_number = 1;
            foreach (var Line_element in Lines_Elements) { 
            

                string Line_Path = string.Format("//Table[@id='dataTable']/tbody/tr[{0}]", line_number);
                var Line_Element = b.FindElement(By.XPath(Line_Path));

                GAppLineDataTable gAppLineDataTable = new GAppLineDataTable(b, Line_Element);
                this.Lines.Add(gAppLineDataTable);
                line_number++;
 
            }
        }

       
    }
}
