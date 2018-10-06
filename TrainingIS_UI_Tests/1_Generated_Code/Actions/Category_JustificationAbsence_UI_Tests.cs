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
namespace TrainingIS_UI_Tests.Category_JustificationAbsences
{
    public class Base_Category_JustificationAbsence_UI_Tests : Base_UI_Tests
    {
       

        public Base_Category_JustificationAbsence_UI_Tests()
        {
            this.Entity_Path = "/Category_JustificationAbsences";
        }
       
        [TestMethod]
        public virtual void Category_JustificationAbsence_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

        [TestMethod]
        public virtual void Category_JustificationAbsence_Create_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Category_JustificationAbsence
            Category_JustificationAbsence Category_JustificationAbsence = new Category_JustificationAbsencesControllerTests_Service().CreateValideCategory_JustificationAbsenceInstance(null,GAppContext);
            Default_Form_Category_JustificationAbsence_Model Default_Form_Category_JustificationAbsence_Model = new Default_Form_Category_JustificationAbsence_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_Category_JustificationAbsence_Model(Category_JustificationAbsence);



	 


 
			var Name = b.FindElement(By.Id(nameof(Default_Form_Category_JustificationAbsence_Model.Name)));
            Name.SendKeys(Default_Form_Category_JustificationAbsence_Model.Name.ToString());

	 


 
			var Description = b.FindElement(By.Id(nameof(Default_Form_Category_JustificationAbsence_Model.Description)));
            Description.SendKeys(Default_Form_Category_JustificationAbsence_Model.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }

    [TestClass]
	public partial class Category_JustificationAbsence_UI_Tests : Base_Category_JustificationAbsence_UI_Tests
    {

    }
}