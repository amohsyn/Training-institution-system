using AutoFixture;
using GApp.UnitTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using TrainingIS.WebApp.Services;
using TrainingIS_UI_Tests.SeanceTrainings;
using TrainingIS_UI_Tests.Services;
using TrainingIS_UI_Tests.Services.GAppPages;

namespace TrainingIS_UI_Tests
{
    [TestClass]
    public class Base_UI_Tests  : BaseUnitTest
    {
        #region Static members
        protected static IWebDriver b;
        protected static string _URL = "http://localhost:60901/";
        static Base_UI_Tests()
        {
            b = new ChromeDriver();
            b.Manage().Window.Maximize();
        }
        #endregion


        #region Properties
        protected Fixture _Fixture = null;
        protected string Entity_Path = "";
        public string Login { get; set; }
        public string Password { get; set; }
        #endregion

        #region GApp Components
        public GAppAlert Alert { get; set; }
        public GAppDataTable DataTable { get; set; }
        public GAppIndexPage IndexPage { get; set; }
        public GAppEditPage EditPage { set; get; }

        #endregion

        #region Constructor

        public Base_UI_Tests():this("Root","Root@123456","/")
        {}

        public Base_UI_Tests(string login, string password, string Entity_Path) :base()
        {

            this.Login = login;
            this.Password = password;
            this.Entity_Path = Entity_Path;

            // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            // GApp Components
            this.Alert = new GAppAlert(b);
            this.DataTable = new GAppDataTable(b);
            this.IndexPage = new GAppIndexPage(b, _URL, this.Entity_Path);
            this.EditPage = new GAppEditPage(b, _URL, this.Entity_Path);


            this.Login_If_Not_Ahenticated();
        }
        #endregion


        #region Tests Methodes
        [AssemblyCleanup()]
        public static void AssemblyCleanup()
        {
            b.Close();
            b.Dispose();
        }
        #endregion

        protected void GoTo_Index()
        {
            var Former_URL = _URL + this.Entity_Path;
            b.Navigate().GoToUrl(Former_URL);
        }

        public  void Login_If_Not_Ahenticated()
        {
            b.Navigate().GoToUrl(_URL);
            Current_Test_Order = 1;

            if (!IsAuthenticated())
            {
                var LoginInput = b.FindElement(By.Id("Login"));
                var PasswordInput = b.FindElement(By.Id("Password"));
                LoginInput.SendKeys(this.Login);
                PasswordInput.SendKeys(this.Password);
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

        public bool IsElementXPathExist(string XPath)
        {
            try
            {
                var element = b.FindElement(By.XPath(XPath));
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

 
        #region Ajax
        public void WaitForAjax()
        {
            while (true) // Handle timeout somewhere
            {
                var ajaxIsComplete = (bool)(b as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0");
                if (ajaxIsComplete)
                    break;
                Thread.Sleep(100);
            }
        }
        /// <summary>
        /// Click and WaiteForAjax
        /// </summary>
        /// <param name="xPath">Xpath</param>
        public void AjaxClick(string xPath)
        {
            b.FindElement(By.XPath(xPath)).Click();
            WaitForAjax();
        }
        #endregion


        public void Select(string SelectInput_Id, string Selected_Value)
        {
            string xpath_TraineeId = string.Format("//select[@id='{0}']/option[@value='{1}']", SelectInput_Id, Selected_Value);
            b.FindElement(By.XPath(xpath_TraineeId)).Click();
        }

        private bool IsElementPresent(By by)
        {
            try
            {
                b.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
 
        [Obsolete("Use this.Alert")]
        public bool Is_Info_Alert()
        {
            var sweet_alert = b.FindElement(By.ClassName("sa-success"));
            return sweet_alert.GetAttribute("style").Contains("display: block");
        }


    }
}