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
namespace TrainingIS_UI_Tests
{
    [TestClass]
    public class Classroom_Create_UI_Tests : Base_UI_Tests
    {
       

        public Classroom_Create_UI_Tests()
        {
            this.Entity_Path = "/Classrooms";
        }
       
        [TestMethod]
        public void Classroom_Index_Show_Test()
        {
            this.GoTo_Index();
        }

        [TestMethod]
        public void Classroom_Create_Test()
        {
            this.GoTo_Index();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            Classroom Classroom = new ClassroomsControllerTests_Service().CreateValideClassroomInstance(null,GAppContext);
            Default_Form_Classroom_Model Default_Form_Classroom_Model = new Default_Form_Classroom_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_Classroom_Model(Classroom);



 
			var Code = b.FindElement(By.Id(nameof(Default_Form_Classroom_Model.Code)));
            Code.SendKeys(Default_Form_Classroom_Model.Code.ToString());

 
			var Name = b.FindElement(By.Id(nameof(Default_Form_Classroom_Model.Name)));
            Name.SendKeys(Default_Form_Classroom_Model.Name.ToString());

			string xpath_ClassroomCategoryId = string.Format("//select[@id='{0}']/option[@value='{1}']", "ClassroomCategoryId", Default_Form_Classroom_Model.ClassroomCategoryId.ToString());
            b.FindElement(By.XPath(xpath_ClassroomCategoryId)).Click(); 

 
			var Description = b.FindElement(By.Id(nameof(Default_Form_Classroom_Model.Description)));
            Description.SendKeys(Default_Form_Classroom_Model.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
