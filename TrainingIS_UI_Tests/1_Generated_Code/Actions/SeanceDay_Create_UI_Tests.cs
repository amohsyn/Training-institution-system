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
namespace TrainingIS_UI_Tests.SeanceDays
{
    public class Base_SeanceDay_Create_UI_Tests : Base_UI_Tests
    {
       

        public Base_SeanceDay_Create_UI_Tests()
        {
            this.Entity_Path = "/SeanceDays";
        }
       
        [TestMethod]
        public virtual void SeanceDay_Index_Show_Test()
        {
            this.GoTo_Index();
        }

        [TestMethod]
        public virtual void SeanceDay_Create_Test()
        {
            this.GoTo_Index();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            SeanceDay SeanceDay = new SeanceDaysControllerTests_Service().CreateValideSeanceDayInstance(null,GAppContext);
            Default_Form_SeanceDay_Model Default_Form_SeanceDay_Model = new Default_Form_SeanceDay_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_SeanceDay_Model(SeanceDay);



 
			var Name = b.FindElement(By.Id(nameof(Default_Form_SeanceDay_Model.Name)));
            Name.SendKeys(Default_Form_SeanceDay_Model.Name.ToString());

 
			var Code = b.FindElement(By.Id(nameof(Default_Form_SeanceDay_Model.Code)));
            Code.SendKeys(Default_Form_SeanceDay_Model.Code.ToString());

 
			var Day = b.FindElement(By.Id(nameof(Default_Form_SeanceDay_Model.Day)));
            Day.SendKeys(Default_Form_SeanceDay_Model.Day.ToString());

 
			var Description = b.FindElement(By.Id(nameof(Default_Form_SeanceDay_Model.Description)));
            Description.SendKeys(Default_Form_SeanceDay_Model.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }

    [TestClass]
	public partial class SeanceDay_Create_UI_Tests : Base_SeanceDay_Create_UI_Tests
    {

    }
}
