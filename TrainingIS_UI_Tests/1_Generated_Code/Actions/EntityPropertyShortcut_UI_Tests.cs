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
namespace TrainingIS_UI_Tests.EntityPropertyShortcuts
{
    public class Base_EntityPropertyShortcut_UI_Tests : Base_UI_Tests
    {
       

        public Base_EntityPropertyShortcut_UI_Tests()
        {
            this.Entity_Path = "/EntityPropertyShortcuts";
        }
       
        [TestMethod]
        public virtual void EntityPropertyShortcut_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

        [TestMethod]
        public virtual void EntityPropertyShortcut_Create_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert EntityPropertyShortcut
            EntityPropertyShortcut EntityPropertyShortcut = new EntityPropertyShortcutsControllerTests_Service().CreateValideEntityPropertyShortcutInstance(null,GAppContext);
            Default_Form_EntityPropertyShortcut_Model Default_Form_EntityPropertyShortcut_Model = new Default_Form_EntityPropertyShortcut_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_EntityPropertyShortcut_Model(EntityPropertyShortcut);



	 


 
			var EntityName = b.FindElement(By.Id(nameof(Default_Form_EntityPropertyShortcut_Model.EntityName)));
            EntityName.SendKeys(Default_Form_EntityPropertyShortcut_Model.EntityName.ToString());

	 


 
			var PropertyName = b.FindElement(By.Id(nameof(Default_Form_EntityPropertyShortcut_Model.PropertyName)));
            PropertyName.SendKeys(Default_Form_EntityPropertyShortcut_Model.PropertyName.ToString());

	 


 
			var PropertyShortcutName = b.FindElement(By.Id(nameof(Default_Form_EntityPropertyShortcut_Model.PropertyShortcutName)));
            PropertyShortcutName.SendKeys(Default_Form_EntityPropertyShortcut_Model.PropertyShortcutName.ToString());

	 


 
			var Description = b.FindElement(By.Id(nameof(Default_Form_EntityPropertyShortcut_Model.Description)));
            Description.SendKeys(Default_Form_EntityPropertyShortcut_Model.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }

    [TestClass]
	public partial class EntityPropertyShortcut_UI_Tests : Base_EntityPropertyShortcut_UI_Tests
    {

    }
}