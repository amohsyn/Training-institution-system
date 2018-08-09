using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;

namespace TrainingIS_UI_Tests
{
    [TestClass]
    public class Base_UI_Tests : IDisposable
    {
        protected static IWebDriver b;
        protected static string _URL = "http://localhost:60901/";

        #region Properties
        protected Fixture _Fixture = null;
        protected string Entity_Path = "";
        #endregion

        public Base_UI_Tests()
        {
            // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            // WebDrover 
            b = new ChromeDriver();
            this.Login_If_Not_Ahenticated();
        }

        

        

        protected void GoTo_Index()
        {
            var Former_URL = _URL + this.Entity_Path;
            b.Navigate().GoToUrl(Former_URL);
        }

        public  void Login_If_Not_Ahenticated()
        {
            b.Navigate().GoToUrl(_URL);

            if (!IsAuthenticated())
            {
                var LoginInput = b.FindElement(By.Id("Login"));
                var PasswordInput = b.FindElement(By.Id("Password"));
                LoginInput.SendKeys("Root");
                PasswordInput.SendKeys("Root@123456");
                PasswordInput.Submit();
            }
            else
            {
                // LofOut
                var element = b.FindElement(By.Id("logoutForm"));
                element.Submit();
                Login_If_Not_Ahenticated();

            }
        }

        private  bool IsAuthenticated()
        {
            return IsElementIdExist("logoutForm");
        }

        public  bool IsElementIdExist(string ElementId)
        {
            try
            {
                var element = b.FindElement(By.Id(ElementId));
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        protected bool Is_In_IndexPage()
        {
            return this.IsElementIdExist("Index_Page_Title");
        }

        protected bool Is_Info_Alert()
        {
            var sweet_alert = b.FindElement(By.ClassName("sa-success"));
            return sweet_alert.GetAttribute("style").Contains("display: block");
        }
        public void Dispose()
        {
            //b.Close();
            //b.Dispose();
        }
    }
}