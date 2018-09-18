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

        public void Init(string Html_Id = "")
        {
            string dataTable = "dataTable" ;
            if (!string.IsNullOrEmpty(Html_Id))
                dataTable = Html_Id  ;

            var Lines_Elements = b.FindElements(By.XPath(string.Format("//Table[@id='{0}']/tbody/tr", dataTable)));
            Lines = new List<GAppLineDataTable>();


            int line_number = 1;
            foreach (var Line_element in Lines_Elements) { 
            

                string Line_Path = string.Format("//Table[@id='{0}']/tbody/tr[{1}]", dataTable, line_number);
                var Line_Element = b.FindElement(By.XPath(Line_Path));

                GAppLineDataTable gAppLineDataTable = new GAppLineDataTable(b, Line_Element);
                this.Lines.Add(gAppLineDataTable);
                line_number++;
 
            }
        }

       
    }
}
