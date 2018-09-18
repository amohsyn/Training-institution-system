using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace TrainingIS_UI_Tests.Services.Helpers
{
    public class JavaScriptHelper : BaseWebDriverService
    {
        IJavaScriptExecutor js;
        public JavaScriptHelper(IWebDriver b) : base(b)
        {
            this.js = b as IJavaScriptExecutor;
        }

        public void ExecuteScript(string JavsScript_Code)
        {
            js.ExecuteScript(JavsScript_Code);
        }
    }
}
