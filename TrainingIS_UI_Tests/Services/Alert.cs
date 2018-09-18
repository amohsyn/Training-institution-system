using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TrainingIS.WebApp.Services
{
    public class GAppAlert
    {
        protected IWebDriver b;

        public GAppAlert(IWebDriver b)
        {
            this.b = b;
        }

        public bool Is_Info_Alert()
        {
            var sweet_alert = b.FindElement(By.ClassName("sa-success"));
            return sweet_alert.GetAttribute("style").Contains("display: block");
        }

        public bool Is_Error_Alert()
        {
            var sweet_alert = b.FindElement(By.ClassName("sa-error"));
            return sweet_alert.GetAttribute("style").Contains("display: block");
        }
        public void Close()
        {
            Thread.Sleep(1000);
            string button_alert_xpath = "//div[@class='sweet-alert showSweetAlert visible']//button[@class='confirm']";
            b.FindElement(By.XPath(button_alert_xpath)).Click();
        }

        public string GetMessage()
        {
            Thread.Sleep(1000);
            string msg = "//div[@class='sweet-alert showSweetAlert visible']//p";
            return b.FindElement(By.XPath(msg)).Text;
        }
    }
}