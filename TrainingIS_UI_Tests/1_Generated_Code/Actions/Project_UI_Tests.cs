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
namespace TrainingIS_UI_Tests.Projects
{
    public class Base_Project_UI_Tests : Base_UI_Tests
    {
       

        public Base_Project_UI_Tests()
        {
            this.Entity_Path = "/Projects";
        }
       
        [TestMethod]
        public virtual void Project_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

        [TestMethod]
        public virtual void Project_Create_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Project
            Project Project = new ProjectsControllerTests_Service().CreateValideProjectInstance(null,GAppContext);
            Default_Form_Project_Model Default_Form_Project_Model = new Default_Form_Project_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_Project_Model(Project);



	 


 
			var Name = b.FindElement(By.Id(nameof(Default_Form_Project_Model.Name)));
            Name.SendKeys(Default_Form_Project_Model.Name.ToString());

	 




			
			this.DateTimePicker.SelectDate(nameof(Default_Form_Project_Model.StartDate), Default_Form_Project_Model.StartDate.ToString());

			
			this.DateTimePicker.SelectDate(nameof(Default_Form_Project_Model.EndtDate), Default_Form_Project_Model.EndtDate.ToString());

	 


 
			var Description = b.FindElement(By.Id(nameof(Default_Form_Project_Model.Description)));
            Description.SendKeys(Default_Form_Project_Model.Description.ToString());

			var isPublic = b.FindElement(By.Id(nameof(Default_Form_Project_Model.isPublic)));
			if (Default_Form_Project_Model.isPublic)
                isPublic.Click();
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }

    [TestClass]
	public partial class Project_UI_Tests : Base_Project_UI_Tests
    {

    }
}
