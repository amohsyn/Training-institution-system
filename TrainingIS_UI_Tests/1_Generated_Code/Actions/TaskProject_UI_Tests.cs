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
namespace TrainingIS_UI_Tests.TaskProjects
{
    public class Base_TaskProject_UI_Tests : Base_UI_Tests
    {
       

        public Base_TaskProject_UI_Tests()
        {
            this.Entity_Path = "/TaskProjects";
        }
       
        [TestMethod]
        public virtual void TaskProject_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

        [TestMethod]
        public virtual void TaskProject_Create_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert TaskProject
            TaskProject TaskProject = new TaskProjectsControllerTests_Service().CreateValideTaskProjectInstance(null,GAppContext);
            Default_Form_TaskProject_Model Default_Form_TaskProject_Model = new Default_Form_TaskProject_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_TaskProject_Model(TaskProject);



			this.Select.SelectValue("ProjectId", Default_Form_TaskProject_Model.ProjectId.ToString());

			this.Select.SelectValue("TaskState", Convert.ToInt32(Default_Form_TaskProject_Model.TaskState).ToString());

			this.Select.SelectValue("OwnerId", Default_Form_TaskProject_Model.OwnerId.ToString());

	 


 
			var Name = b.FindElement(By.Id(nameof(Default_Form_TaskProject_Model.Name)));
            Name.SendKeys(Default_Form_TaskProject_Model.Name.ToString());

	 


 
			var Description = b.FindElement(By.Id(nameof(Default_Form_TaskProject_Model.Description)));
            Description.SendKeys(Default_Form_TaskProject_Model.Description.ToString());

			
			this.DateTimePicker.SelectDate(nameof(Default_Form_TaskProject_Model.StartDate), Default_Form_TaskProject_Model.StartDate.ToString());

			
			this.DateTimePicker.SelectDate(nameof(Default_Form_TaskProject_Model.EndtDate), Default_Form_TaskProject_Model.EndtDate.ToString());

			var isPublic = b.FindElement(By.Id(nameof(Default_Form_TaskProject_Model.isPublic)));
			if (Default_Form_TaskProject_Model.isPublic)
                isPublic.Click();
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }

    [TestClass]
	public partial class TaskProject_UI_Tests : Base_TaskProject_UI_Tests
    {

    }
}
