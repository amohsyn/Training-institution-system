using System;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TrainingIS.Entities;
using TrainingIS.WebApp.Controllers.Tests;
using TrainingIS.BLL.ModelsViews;
using GApp.Entities;
using GApp.DAL;
using TrainingIS.DAL;
using TrainingIS.WebApp.Tests.Services;
using GApp.Core.Context;
using TrainingIS.Entities.ModelsViews;
namespace TrainingIS_UI_Tests.Specialties
{
    public class Base_Specialty_Create_UI_Tests : Base_UI_Tests
    {
       

        public Base_Specialty_Create_UI_Tests()
        {
            this.Entity_Path = "/Specialties";
        }
       
        [TestMethod]
        public virtual void Specialty_Index_Show_Test()
        {
            this.GoTo_Index();
        }

        [TestMethod]
        public virtual void Specialty_Create_Test()
        {
            this.GoTo_Index();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            Specialty Specialty = new SpecialtiesControllerTests_Service().CreateValideSpecialtyInstance(null,GAppContext);
            Default_Form_Specialty_Model Default_Form_Specialty_Model = new Default_Form_Specialty_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_Specialty_Model(Specialty);



			string xpath_SectorId = string.Format("//select[@id='{0}']/option[@value='{1}']", "SectorId", Default_Form_Specialty_Model.SectorId.ToString());
            b.FindElement(By.XPath(xpath_SectorId)).Click(); 

			string xpath_TrainingLevelId = string.Format("//select[@id='{0}']/option[@value='{1}']", "TrainingLevelId", Default_Form_Specialty_Model.TrainingLevelId.ToString());
            b.FindElement(By.XPath(xpath_TrainingLevelId)).Click(); 

 
			var Code = b.FindElement(By.Id(nameof(Default_Form_Specialty_Model.Code)));
            Code.SendKeys(Default_Form_Specialty_Model.Code.ToString());

 
			var Name = b.FindElement(By.Id(nameof(Default_Form_Specialty_Model.Name)));
            Name.SendKeys(Default_Form_Specialty_Model.Name.ToString());

 
			var Description = b.FindElement(By.Id(nameof(Default_Form_Specialty_Model.Description)));
            Description.SendKeys(Default_Form_Specialty_Model.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }

    [TestClass]
	public partial class Specialty_Create_UI_Tests : Base_Specialty_Create_UI_Tests
    {

    }
}
