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
    public class Specialty_UI_Index_Tests : Base_UI_Tests
    {
       

        public Specialty_UI_Index_Tests()
        {
            this.Entity_Path = "/Specialties";
        }
       
        [TestMethod]
        public void Specialty_Index_Show_Test()
        {
            this.GoTo_Index();
        }

        [TestMethod]
        public void Specialty_Create_Test()
        {
            this.GoTo_Index();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            Specialty Specialty = new SpecialtiesControllerTests_Service().CreateValideSpecialtyInstance();
            Default_SpecialtyFormView Default_SpecialtyFormView = new Default_SpecialtyFormViewBLM(new TrainingIS.DAL.UnitOfWork())
                .ConverTo_Default_SpecialtyFormView(Specialty);



 
			var Code = b.FindElement(By.Id(nameof(Default_SpecialtyFormView.Code)));
            Code.SendKeys(Default_SpecialtyFormView.Code.ToString());

 
			var Name = b.FindElement(By.Id(nameof(Default_SpecialtyFormView.Name)));
            Name.SendKeys(Default_SpecialtyFormView.Name.ToString());

 
			var Description = b.FindElement(By.Id(nameof(Default_SpecialtyFormView.Description)));
            Description.SendKeys(Default_SpecialtyFormView.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
