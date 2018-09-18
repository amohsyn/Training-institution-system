using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace TrainingIS_UI_Tests.Services.GAppPages
{
    public class GAppEditPage : GAppPage
    {
        public GAppEditPage(IWebDriver b, string URL, string entity_Path) : base(b, URL, entity_Path)
        {
        }

        public IWebElement Get_Title_Element()
        {
            string element_path = "//form/div[@class='form-horizontal']/h4";
            return b.FindElement(By.XPath(element_path));
        }
    }
}
