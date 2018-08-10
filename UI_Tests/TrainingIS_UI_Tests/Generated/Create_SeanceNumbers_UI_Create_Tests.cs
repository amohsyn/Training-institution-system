using System;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TrainingIS.Entities;
using TrainingIS.WebApp.Controllers.Tests;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;
namespace TrainingIS_UI_Tests
{
    [TestClass]
    public class SeanceNumber_UI_Index_Tests : Base_UI_Tests
    {
       

        public SeanceNumber_UI_Index_Tests()
        {
            this.Entity_Path = "/SeanceNumbers";
        }
       
        [TestMethod]
        public void SeanceNumber_Index_Show_Test()
        {
            this.GoTo_Index();
        }

        [TestMethod]
        public void SeanceNumber_Create_Test()
        {
            this.GoTo_Index();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            SeanceNumber SeanceNumber = new SeanceNumbersControllerTests_Service().CreateValideSeanceNumberInstance();
            Default_SeanceNumberFormView Default_SeanceNumberFormView = new Default_SeanceNumberFormViewBLM(new TrainingIS.DAL.UnitOfWork())
                .ConverTo_Default_SeanceNumberFormView(SeanceNumber);



 
			var Code = b.FindElement(By.Id(nameof(Default_SeanceNumberFormView.Code)));
            Code.SendKeys(Default_SeanceNumberFormView.Code.ToString());

 
			var StartTime = b.FindElement(By.Id(nameof(Default_SeanceNumberFormView.StartTime)));
            StartTime.SendKeys(Default_SeanceNumberFormView.StartTime.ToString());

 
			var EndTime = b.FindElement(By.Id(nameof(Default_SeanceNumberFormView.EndTime)));
            EndTime.SendKeys(Default_SeanceNumberFormView.EndTime.ToString());

 
			var Description = b.FindElement(By.Id(nameof(Default_SeanceNumberFormView.Description)));
            Description.SendKeys(Default_SeanceNumberFormView.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
