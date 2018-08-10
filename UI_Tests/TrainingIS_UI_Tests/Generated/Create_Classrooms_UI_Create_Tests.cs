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
    public class Classroom_UI_Index_Tests : Base_UI_Tests
    {
       

        public Classroom_UI_Index_Tests()
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

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            Classroom Classroom = new ClassroomsControllerTests_Service().CreateValideClassroomInstance();
            Default_ClassroomFormView Default_ClassroomFormView = new Default_ClassroomFormViewBLM(new TrainingIS.DAL.UnitOfWork())
                .ConverTo_Default_ClassroomFormView(Classroom);



 
			var Code = b.FindElement(By.Id(nameof(Default_ClassroomFormView.Code)));
            Code.SendKeys(Default_ClassroomFormView.Code.ToString());

 
			var Name = b.FindElement(By.Id(nameof(Default_ClassroomFormView.Name)));
            Name.SendKeys(Default_ClassroomFormView.Name.ToString());

			string xpath_ClassroomCategoryId = string.Format("//select[@id='{0}']/option[@value='{1}']", "ClassroomCategoryId", Default_ClassroomFormView.ClassroomCategoryId.ToString());
            b.FindElement(By.XPath(xpath_ClassroomCategoryId)).Click(); 

 
			var Description = b.FindElement(By.Id(nameof(Default_ClassroomFormView.Description)));
            Description.SendKeys(Default_ClassroomFormView.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
