using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace TrainingIS_UI_Tests.Services
{
    public class WebDriverService
    {
        private IWebDriver b;

        public WebDriverService(IWebDriver b)
        {
            this.b = b;
        }
    }
}
