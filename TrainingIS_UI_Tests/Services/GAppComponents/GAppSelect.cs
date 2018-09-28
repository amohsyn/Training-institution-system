using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace TrainingIS_UI_Tests.Services.GAppComponents
{
    public class GAppSelect : BaseWebDriverService
    {
        public GAppSelect(IWebDriver b) : base(b)
        {
        }

        public void SelectValue(string SelectInput_Id, string Selected_Value)
        {
            string xpath_TraineeId = "";
            int wait_number = 20;
            int wait = 0;
            while (true)
            {
                try
                {
                    xpath_TraineeId = string.Format("//select[@id='{0}']/option[@value='{1}']", SelectInput_Id, Selected_Value);
                    b.FindElement(By.XPath(xpath_TraineeId)).Click();
                    return;
                }
                catch (Exception)
                {
                    if (wait < wait_number)
                        Thread.Sleep(100);
                    else
                        throw;
                }
                wait++;
            }


        }

        public void SelectText(string SelectInput_Id, string Selected_Text)
        {
            string xpath_TraineeId = "";
            int wait_number = 20;
            int wait = 0;
            while (true)
            {
                try
                {
                    xpath_TraineeId = string.Format("//select[@id='{0}']/option/[normalize-space(text())=\"{1}\"]", SelectInput_Id, Selected_Text);
                    b.FindElement(By.XPath(xpath_TraineeId)).Click();
                    return;
                }
                catch (Exception)
                {
                    if (wait < wait_number)
                        Thread.Sleep(100);
                    else
                        throw;
                }
                wait++;
            }


        }
    }
}
