﻿using System;
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
namespace TrainingIS_UI_Tests.Trainings
{
    public class Base_Training_Create_UI_Tests : Base_UI_Tests
    {
       

        public Base_Training_Create_UI_Tests()
        {
            this.Entity_Path = "/Trainings";
        }
       
        [TestMethod]
        public virtual void Training_Index_Show_Test()
        {
            this.GoTo_Index();
        }

        [TestMethod]
        public virtual void Training_Create_Test()
        {
            this.GoTo_Index();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            Training Training = new TrainingsControllerTests_Service().CreateValideTrainingInstance(null,GAppContext);
            Default_Form_Training_Model Default_Form_Training_Model = new Default_Form_Training_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_Training_Model(Training);



			string xpath_TrainingYearId = string.Format("//select[@id='{0}']/option[@value='{1}']", "TrainingYearId", Default_Form_Training_Model.TrainingYearId.ToString());
            b.FindElement(By.XPath(xpath_TrainingYearId)).Click(); 

			string xpath_ModuleTrainingId = string.Format("//select[@id='{0}']/option[@value='{1}']", "ModuleTrainingId", Default_Form_Training_Model.ModuleTrainingId.ToString());
            b.FindElement(By.XPath(xpath_ModuleTrainingId)).Click(); 

 
			var Hourly_Mass_To_Teach = b.FindElement(By.Id(nameof(Default_Form_Training_Model.Hourly_Mass_To_Teach)));
            Hourly_Mass_To_Teach.SendKeys(Default_Form_Training_Model.Hourly_Mass_To_Teach.ToString());

			string xpath_FormerId = string.Format("//select[@id='{0}']/option[@value='{1}']", "FormerId", Default_Form_Training_Model.FormerId.ToString());
            b.FindElement(By.XPath(xpath_FormerId)).Click(); 

			string xpath_GroupId = string.Format("//select[@id='{0}']/option[@value='{1}']", "GroupId", Default_Form_Training_Model.GroupId.ToString());
            b.FindElement(By.XPath(xpath_GroupId)).Click(); 

 
			var Code = b.FindElement(By.Id(nameof(Default_Form_Training_Model.Code)));
            Code.SendKeys(Default_Form_Training_Model.Code.ToString());

 
			var Description = b.FindElement(By.Id(nameof(Default_Form_Training_Model.Description)));
            Description.SendKeys(Default_Form_Training_Model.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }

    [TestClass]
	public partial class Training_Create_UI_Tests : Base_Training_Create_UI_Tests
    {

    }
}
