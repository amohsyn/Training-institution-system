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
namespace TrainingIS_UI_Tests.StateOfAbseces
{
    public class Base_StateOfAbsece_UI_Tests : Base_UI_Tests
    {
       

        public Base_StateOfAbsece_UI_Tests()
        {
            this.Entity_Path = "/StateOfAbseces";
        }
       
        [TestMethod]
        public virtual void StateOfAbsece_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

        [TestMethod]
        public virtual void StateOfAbsece_Create_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert StateOfAbsece
            StateOfAbsece StateOfAbsece = new StateOfAbsecesControllerTests_Service().CreateValideStateOfAbseceInstance(null,GAppContext);
            Default_Form_StateOfAbsece_Model Default_Form_StateOfAbsece_Model = new Default_Form_StateOfAbsece_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_StateOfAbsece_Model(StateOfAbsece);



	 


 
			var Name = b.FindElement(By.Id(nameof(Default_Form_StateOfAbsece_Model.Name)));
            Name.SendKeys(Default_Form_StateOfAbsece_Model.Name.ToString());

			this.Select.SelectValue("Category", Convert.ToInt32(Default_Form_StateOfAbsece_Model.Category).ToString());

	 


 
			var Value = b.FindElement(By.Id(nameof(Default_Form_StateOfAbsece_Model.Value)));
            Value.SendKeys(Default_Form_StateOfAbsece_Model.Value.ToString());

			this.Select.SelectValue("TraineeId", Default_Form_StateOfAbsece_Model.TraineeId.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }

    [TestClass]
	public partial class StateOfAbsece_UI_Tests : Base_StateOfAbsece_UI_Tests
    {

    }
}
