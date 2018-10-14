using AutoFixture;
using GApp.UnitTest;
using GApp.UnitTest.UI_Tests;
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

namespace TrainingIS_UI_Tests
{
    [TestClass]
    public class Base_UI_Tests  : UI_Test
    {
       
        static Base_UI_Tests()
        {
            _URL = "http://localhost:60901/";
        }

        #region Constructor
        public virtual void Init()
        {

        }

        public Base_UI_Tests():base(new GApp.UnitTest.Context.UI_Test_Context
        { Login= "Root",
            Password = "Root@123456",
            ControllerName = "/"
        })
        {
            this.Init();
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

  
      

        #region Obsolete 

        [Obsolete("Use PageIndex")]
        protected void GoTo_Index()
        {
            this.Login_If_Not_Ahenticated();
            var Former_URL = _URL + this.Entity_Path;
            b.Navigate().GoToUrl(Former_URL);
        }
        [Obsolete("Use PageIndex")]
        protected bool Is_In_IndexPage()
        {
            return this.IsElementIdExist("Index_Page_Title");
        }
        [Obsolete("Use Ajax Helper")]
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
        [Obsolete("Use this.Alert")]
        public bool Is_Info_Alert()
        {
            var sweet_alert = b.FindElement(By.ClassName("sa-success"));
            return sweet_alert.GetAttribute("style").Contains("display: block");
        }
        /// <summary>
        /// Click and WaiteForAjax
        /// </summary>
        /// <param name="xPath">Xpath</param>
        [Obsolete("Use Ajax Helper")]
        
        #endregion


    }
}