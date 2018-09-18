using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS_UI_Tests.Services.Helpers;

namespace TrainingIS_UI_Tests.Services.GAppPages
{
    public class GAppPage
    {
        protected IWebDriver b;
        protected string Entity_Path;
        protected string URL;
        protected AjaxHelper Ajax; 

        public GAppPage(IWebDriver b, string URL, string entity_Path)
        {
            this.b = b;
            this.Entity_Path = entity_Path;
            this.URL = URL;

            this.Ajax = new AjaxHelper(b);
        }

        public void Click(string Html_Id)
        {
            var element = b.FindElement(By.Id(Html_Id));
            element.Click();
            this.Ajax.WaitForAjax();

        }
    }
}
