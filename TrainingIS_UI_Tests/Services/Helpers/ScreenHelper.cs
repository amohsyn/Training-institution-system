using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace TrainingIS_UI_Tests.Services.Helpers
{
    public class ScreenHelper : BaseWebDriverService
    {
        public ScreenHelper(IWebDriver b) : base(b)
        {
        }

        public void ScreenCapture(string fileName)
        {
            Screenshot ss = ((ITakesScreenshot)b).GetScreenshot();
            ss.SaveAsFile(fileName + "_" + DateTime.Now.ToString("yyyy-mm-dd-HHmmss") + ".png", ScreenshotImageFormat.Png);
        }
    }
}
