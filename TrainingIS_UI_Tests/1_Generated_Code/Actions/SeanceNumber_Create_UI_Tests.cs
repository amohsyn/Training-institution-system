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
using TrainingIS.Entities.ModelsViews;
namespace TrainingIS_UI_Tests
{
    [TestClass]
    public class SeanceNumber_Create_UI_Tests : Base_UI_Tests
    {
       

        public SeanceNumber_Create_UI_Tests()
        {
            this.Entity_Path = "/SeanceNumbers";
        }
       
        [TestMethod]
        public void SeanceNumber_Index_Show_Test()
        {
            this.GoTo_Index();
        }

        [TestMethod]
        public void SeanceNumber_Create_Test()
        {
            this.GoTo_Index();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            SeanceNumber SeanceNumber = new SeanceNumbersControllerTests_Service().CreateValideSeanceNumberInstance();
            Default_Form_SeanceNumber_Model Default_Form_SeanceNumber_Model = new Default_Form_SeanceNumber_ModelBLM(new UnitOfWork<TrainingISModel>())
                .ConverTo_Default_Form_SeanceNumber_Model(SeanceNumber);



 
			var Code = b.FindElement(By.Id(nameof(Default_Form_SeanceNumber_Model.Code)));
            Code.SendKeys(Default_Form_SeanceNumber_Model.Code.ToString());

 
			var StartTime = b.FindElement(By.Id(nameof(Default_Form_SeanceNumber_Model.StartTime)));
            StartTime.SendKeys(Default_Form_SeanceNumber_Model.StartTime.ToString());

 
			var EndTime = b.FindElement(By.Id(nameof(Default_Form_SeanceNumber_Model.EndTime)));
            EndTime.SendKeys(Default_Form_SeanceNumber_Model.EndTime.ToString());

 
			var Description = b.FindElement(By.Id(nameof(Default_Form_SeanceNumber_Model.Description)));
            Description.SendKeys(Default_Form_SeanceNumber_Model.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}