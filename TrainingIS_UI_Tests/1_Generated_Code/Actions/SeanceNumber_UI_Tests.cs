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
namespace TrainingIS_UI_Tests.SeanceNumbers
{
    public class Base_SeanceNumber_UI_Tests : Base_UI_Tests
    {
       

        public Base_SeanceNumber_UI_Tests()
        {
            this.Entity_Path = "/SeanceNumbers";
        }
       
        [TestMethod]
        public virtual void SeanceNumber_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

        [TestMethod]
        public virtual void SeanceNumber_Create_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert SeanceNumber
            SeanceNumber SeanceNumber = new SeanceNumbersControllerTests_Service().CreateValideSeanceNumberInstance(null,GAppContext);
            Default_Form_SeanceNumber_Model Default_Form_SeanceNumber_Model = new Default_Form_SeanceNumber_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_SeanceNumber_Model(SeanceNumber);



	 


 
			var Code = b.FindElement(By.Id(nameof(Default_Form_SeanceNumber_Model.Code)));
            Code.SendKeys(Default_Form_SeanceNumber_Model.Code.ToString());

			
			this.DateTimePicker.SelectDate(nameof(Default_Form_SeanceNumber_Model.StartTime), Default_Form_SeanceNumber_Model.StartTime.ToString());

			
			this.DateTimePicker.SelectDate(nameof(Default_Form_SeanceNumber_Model.EndTime), Default_Form_SeanceNumber_Model.EndTime.ToString());

	 


 
			var Description = b.FindElement(By.Id(nameof(Default_Form_SeanceNumber_Model.Description)));
            Description.SendKeys(Default_Form_SeanceNumber_Model.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }

    [TestClass]
	public partial class SeanceNumber_UI_Tests : Base_SeanceNumber_UI_Tests
    {

    }
}
