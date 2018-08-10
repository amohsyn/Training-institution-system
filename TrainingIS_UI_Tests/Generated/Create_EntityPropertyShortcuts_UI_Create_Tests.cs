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
    public class EntityPropertyShortcut_UI_Index_Tests : Base_UI_Tests
    {
       

        public EntityPropertyShortcut_UI_Index_Tests()
        {
            this.Entity_Path = "/EntityPropertyShortcuts";
        }
       
        [TestMethod]
        public void EntityPropertyShortcut_Index_Show_Test()
        {
            this.GoTo_Index();
        }

        [TestMethod]
        public void EntityPropertyShortcut_Create_Test()
        {
            this.GoTo_Index();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            EntityPropertyShortcut EntityPropertyShortcut = new EntityPropertyShortcutsControllerTests_Service().CreateValideEntityPropertyShortcutInstance();
            Default_EntityPropertyShortcutFormView Default_EntityPropertyShortcutFormView = new Default_EntityPropertyShortcutFormViewBLM(new TrainingIS.DAL.UnitOfWork())
                .ConverTo_Default_EntityPropertyShortcutFormView(EntityPropertyShortcut);



 
			var EntityName = b.FindElement(By.Id(nameof(Default_EntityPropertyShortcutFormView.EntityName)));
            EntityName.SendKeys(Default_EntityPropertyShortcutFormView.EntityName.ToString());

 
			var PropertyName = b.FindElement(By.Id(nameof(Default_EntityPropertyShortcutFormView.PropertyName)));
            PropertyName.SendKeys(Default_EntityPropertyShortcutFormView.PropertyName.ToString());

 
			var PropertyShortcutName = b.FindElement(By.Id(nameof(Default_EntityPropertyShortcutFormView.PropertyShortcutName)));
            PropertyShortcutName.SendKeys(Default_EntityPropertyShortcutFormView.PropertyShortcutName.ToString());

 
			var Description = b.FindElement(By.Id(nameof(Default_EntityPropertyShortcutFormView.Description)));
            Description.SendKeys(Default_EntityPropertyShortcutFormView.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
