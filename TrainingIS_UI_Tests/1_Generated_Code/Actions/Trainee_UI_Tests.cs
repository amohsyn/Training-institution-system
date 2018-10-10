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
namespace TrainingIS_UI_Tests.Trainees
{
    public class Base_Trainee_UI_Tests : Base_UI_Tests
    {
       

        public Base_Trainee_UI_Tests()
        {
            this.Entity_Path = "/Trainees";
        }
       
        [TestMethod]
        public virtual void Trainee_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

        [TestMethod]
        public virtual void Trainee_Create_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Trainee
            Trainee Trainee = new TraineesControllerTests_Service().CreateValideTraineeInstance(null,GAppContext);
            Default_Form_Trainee_Model Default_Form_Trainee_Model = new Default_Form_Trainee_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_Trainee_Model(Trainee);



	 


 
			var CNE = b.FindElement(By.Id(nameof(Default_Form_Trainee_Model.CNE)));
            CNE.SendKeys(Default_Form_Trainee_Model.CNE.ToString());

			
			this.DateTimePicker.SelectDate(nameof(Default_Form_Trainee_Model.DateRegistration), Default_Form_Trainee_Model.DateRegistration.ToString());

			this.Select.SelectValue("isActif", Convert.ToInt32(Default_Form_Trainee_Model.isActif).ToString());

			this.Select.SelectValue("SchoollevelId", Default_Form_Trainee_Model.SchoollevelId.ToString());

			this.Select.SelectValue("SpecialtyId", Default_Form_Trainee_Model.SpecialtyId.ToString());

			this.Select.SelectValue("YearStudyId", Default_Form_Trainee_Model.YearStudyId.ToString());

			this.Select.SelectValue("GroupId", Default_Form_Trainee_Model.GroupId.ToString());

	 


 
			var FirstName = b.FindElement(By.Id(nameof(Default_Form_Trainee_Model.FirstName)));
            FirstName.SendKeys(Default_Form_Trainee_Model.FirstName.ToString());

	 


 
			var LastName = b.FindElement(By.Id(nameof(Default_Form_Trainee_Model.LastName)));
            LastName.SendKeys(Default_Form_Trainee_Model.LastName.ToString());

	 


 
			var FirstNameArabe = b.FindElement(By.Id(nameof(Default_Form_Trainee_Model.FirstNameArabe)));
            FirstNameArabe.SendKeys(Default_Form_Trainee_Model.FirstNameArabe.ToString());

	 


 
			var LastNameArabe = b.FindElement(By.Id(nameof(Default_Form_Trainee_Model.LastNameArabe)));
            LastNameArabe.SendKeys(Default_Form_Trainee_Model.LastNameArabe.ToString());

			this.Select.SelectValue("Sex", Convert.ToInt32(Default_Form_Trainee_Model.Sex).ToString());

			
			this.DateTimePicker.SelectDate(nameof(Default_Form_Trainee_Model.Birthdate), Default_Form_Trainee_Model.Birthdate.ToString());

			this.Select.SelectValue("NationalityId", Default_Form_Trainee_Model.NationalityId.ToString());

	 


 
			var BirthPlace = b.FindElement(By.Id(nameof(Default_Form_Trainee_Model.BirthPlace)));
            BirthPlace.SendKeys(Default_Form_Trainee_Model.BirthPlace.ToString());

	 


 
			var CIN = b.FindElement(By.Id(nameof(Default_Form_Trainee_Model.CIN)));
            CIN.SendKeys(Default_Form_Trainee_Model.CIN.ToString());

	 


 
			//var Photo = b.FindElement(By.Id(nameof(Default_Form_Trainee_Model.Photo)));
   //         Photo.SendKeys(Default_Form_Trainee_Model.Photo.ToString());

	 


 
			var Cellphone = b.FindElement(By.Id(nameof(Default_Form_Trainee_Model.Cellphone)));
            Cellphone.SendKeys(Default_Form_Trainee_Model.Cellphone.ToString());

	 


 
			var Email = b.FindElement(By.Id(nameof(Default_Form_Trainee_Model.Email)));
            Email.SendKeys(Default_Form_Trainee_Model.Email.ToString());

	 


 
			var Address = b.FindElement(By.Id(nameof(Default_Form_Trainee_Model.Address)));
            Address.SendKeys(Default_Form_Trainee_Model.Address.ToString());

	 


 
			var FaceBook = b.FindElement(By.Id(nameof(Default_Form_Trainee_Model.FaceBook)));
            FaceBook.SendKeys(Default_Form_Trainee_Model.FaceBook.ToString());

	 


 
			var WebSite = b.FindElement(By.Id(nameof(Default_Form_Trainee_Model.WebSite)));
            WebSite.SendKeys(Default_Form_Trainee_Model.WebSite.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }

    [TestClass]
	public partial class Trainee_UI_Tests : Base_Trainee_UI_Tests
    {

    }
}
