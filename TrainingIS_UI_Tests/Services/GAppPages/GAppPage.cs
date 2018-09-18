using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingIS_UI_Tests.Services.GAppPages
{
    public class GAppPage
    {
        protected IWebDriver b;
        protected string Entity_Path;
        protected string URL;

        public GAppPage(IWebDriver b, string URL, string entity_Path)
        {
            this.b = b;
            this.Entity_Path = entity_Path;
            this.URL = URL;
        }
    }
}
