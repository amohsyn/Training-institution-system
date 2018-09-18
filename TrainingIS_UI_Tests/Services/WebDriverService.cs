using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace TrainingIS_UI_Tests.Services
{
    public class BaseWebDriverService
    {
        protected IWebDriver b;

        public BaseWebDriverService(IWebDriver b)
        {
            this.b = b;
        }
    }
}
