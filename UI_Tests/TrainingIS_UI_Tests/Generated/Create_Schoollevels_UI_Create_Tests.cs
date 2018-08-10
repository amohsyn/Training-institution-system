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
    public class Schoollevel_UI_Index_Tests : Base_UI_Tests
    {
       

        public Schoollevel_UI_Index_Tests()
        {
            this.Entity_Path = "/Schoollevels";
        }
       
        [TestMethod]
        public void Schoollevel_Index_Show_Test()
        {
            this.GoTo_Index();
        }

        [TestMethod]
        public void Schoollevel_Create_Test()
        {
            this.GoTo_Index();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            Schoollevel Schoollevel = new SchoollevelsControllerTests_Service().CreateValideSchoollevelInstance();
            Default_SchoollevelFormView Default_SchoollevelFormView = new Default_SchoollevelFormViewBLM(new TrainingIS.DAL.UnitOfWork())
                .ConverTo_Default_SchoollevelFormView(Schoollevel);



 
			var Code = b.FindElement(By.Id(nameof(Default_SchoollevelFormView.Code)));
            Code.SendKeys(Default_SchoollevelFormView.Code.ToString());

 
			var Name = b.FindElement(By.Id(nameof(Default_SchoollevelFormView.Name)));
            Name.SendKeys(Default_SchoollevelFormView.Name.ToString());

 
			var Description = b.FindElement(By.Id(nameof(Default_SchoollevelFormView.Description)));
            Description.SendKeys(Default_SchoollevelFormView.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
