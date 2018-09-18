using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using TrainingIS_UI_Tests.Services.Helpers;

namespace TrainingIS_UI_Tests.Services.GAppComponents
{
    public class GAppDateTimePicker : BaseWebDriverService
    {
        private JavaScriptHelper javaScriptHelper;
        public GAppDateTimePicker(IWebDriver b) : base(b)
        {
            javaScriptHelper = new JavaScriptHelper(b);
        }

        public void SelectDate(string Html_Id, string Date)
        {
           
            string js = string.Format(" $('#{0}').data('DateTimePicker').date('{1}');", Html_Id, Date);
            this.javaScriptHelper.ExecuteScript(js);
        }
    }
}
