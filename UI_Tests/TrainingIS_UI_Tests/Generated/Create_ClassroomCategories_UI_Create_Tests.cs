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
    public class ClassroomCategory_UI_Index_Tests : Base_UI_Tests
    {
       

        public ClassroomCategory_UI_Index_Tests()
        {
            this.Entity_Path = "/ClassroomCategories";
        }
       
        [TestMethod]
        public void ClassroomCategory_Index_Show_Test()
        {
            this.GoTo_Index();
        }

        [TestMethod]
        public void ClassroomCategory_Create_Test()
        {
            this.GoTo_Index();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            ClassroomCategory ClassroomCategory = new ClassroomCategoriesControllerTests_Service().CreateValideClassroomCategoryInstance();
            Default_ClassroomCategoryFormView Default_ClassroomCategoryFormView = new Default_ClassroomCategoryFormViewBLM(new TrainingIS.DAL.UnitOfWork())
                .ConverTo_Default_ClassroomCategoryFormView(ClassroomCategory);



 
			var Code = b.FindElement(By.Id(nameof(Default_ClassroomCategoryFormView.Code)));
            Code.SendKeys(Default_ClassroomCategoryFormView.Code.ToString());

 
			var Name = b.FindElement(By.Id(nameof(Default_ClassroomCategoryFormView.Name)));
            Name.SendKeys(Default_ClassroomCategoryFormView.Name.ToString());

 
			var Description = b.FindElement(By.Id(nameof(Default_ClassroomCategoryFormView.Description)));
            Description.SendKeys(Default_ClassroomCategoryFormView.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
