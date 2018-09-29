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
namespace TrainingIS_UI_Tests.Metiers
{
    public class Base_Metier_UI_Tests : Base_UI_Tests
    {
       

        public Base_Metier_UI_Tests()
        {
            this.Entity_Path = "/Metiers";
        }
       
        [TestMethod]
        public virtual void Metier_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

        [TestMethod]
        public virtual void Metier_Create_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Metier
            Metier Metier = new MetiersControllerTests_Service().CreateValideMetierInstance(null,GAppContext);
            Default_Form_Metier_Model Default_Form_Metier_Model = new Default_Form_Metier_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_Metier_Model(Metier);



	 


 
			var Code = b.FindElement(By.Id(nameof(Default_Form_Metier_Model.Code)));
            Code.SendKeys(Default_Form_Metier_Model.Code.ToString());

	 


 
			var Name = b.FindElement(By.Id(nameof(Default_Form_Metier_Model.Name)));
            Name.SendKeys(Default_Form_Metier_Model.Name.ToString());

	 


 
			var Description = b.FindElement(By.Id(nameof(Default_Form_Metier_Model.Description)));
            Description.SendKeys(Default_Form_Metier_Model.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }

    [TestClass]
	public partial class Metier_UI_Tests : Base_Metier_UI_Tests
    {

    }
}