using GApp.DAL;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL.ModelsViews;
using TrainingIS.DAL;
using TrainingIS.Entities;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS_UI_Tests.Administrators
{
    public partial class Create_Administrator_UI_Tests
    {
        public override void Administrator_UI_Create(Administrator Administrator)
        {
            this.GoTo_Index_And_Login_If_Not_Ahenticated();

            // Index create click Test
            var CreateElement = b.FindElement(OpenQA.Selenium.By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Administrator
            Default_Administrator_Create_Model Default_Administrator_Create_Model = new Default_Administrator_Create_ModelBLM(new UnitOfWork<TrainingISModel>(), GAppContext)
                .ConverTo_Default_Administrator_Create_Model(Administrator);

            //var Photo = b.FindElement(By.Id(nameof(Default_Administrator_Create_Model.Photo)));
            //Photo.SendKeys(Default_Administrator_Create_Model.Photo.ToString());

            var RegistrationNumber = b.FindElement(By.Id(nameof(Default_Administrator_Create_Model.RegistrationNumber)));
            RegistrationNumber.SendKeys(Default_Administrator_Create_Model.RegistrationNumber.ToString());
            var CreateUserAccount = b.FindElement(By.Id(nameof(Default_Administrator_Create_Model.CreateUserAccount)));
            if (Default_Administrator_Create_Model.CreateUserAccount)
                CreateUserAccount.Click();
            var Login = b.FindElement(By.Id(nameof(Default_Administrator_Create_Model.Login)));
            Login.SendKeys(Default_Administrator_Create_Model.Login.ToString());
            var Password = b.FindElement(By.Id(nameof(Default_Administrator_Create_Model.Password)));
            Password.SendKeys(Default_Administrator_Create_Model.Password.ToString());
            var FirstName = b.FindElement(By.Id(nameof(Default_Administrator_Create_Model.FirstName)));
            FirstName.SendKeys(Default_Administrator_Create_Model.FirstName.ToString());
            var LastName = b.FindElement(By.Id(nameof(Default_Administrator_Create_Model.LastName)));
            LastName.SendKeys(Default_Administrator_Create_Model.LastName.ToString());
            var FirstNameArabe = b.FindElement(By.Id(nameof(Default_Administrator_Create_Model.FirstNameArabe)));
            FirstNameArabe.SendKeys(Default_Administrator_Create_Model.FirstNameArabe.ToString());
            var LastNameArabe = b.FindElement(By.Id(nameof(Default_Administrator_Create_Model.LastNameArabe)));
            LastNameArabe.SendKeys(Default_Administrator_Create_Model.LastNameArabe.ToString());
            this.Select.SelectValue("Sex", Convert.ToInt32(Default_Administrator_Create_Model.Sex).ToString());

            this.DateTimePicker.SelectDate(nameof(Default_Administrator_Create_Model.Birthdate), Default_Administrator_Create_Model.Birthdate.ToString());
            this.Select.SelectValue("NationalityId", Default_Administrator_Create_Model.NationalityId.ToString());
            var BirthPlace = b.FindElement(By.Id(nameof(Default_Administrator_Create_Model.BirthPlace)));
            BirthPlace.SendKeys(Default_Administrator_Create_Model.BirthPlace.ToString());
            var CIN = b.FindElement(By.Id(nameof(Default_Administrator_Create_Model.CIN)));
            CIN.SendKeys(Default_Administrator_Create_Model.CIN.ToString());
            var Cellphone = b.FindElement(By.Id(nameof(Default_Administrator_Create_Model.Cellphone)));
            Cellphone.SendKeys(Default_Administrator_Create_Model.Cellphone.ToString());
            var Email = b.FindElement(By.Id(nameof(Default_Administrator_Create_Model.Email)));
            Email.SendKeys(Default_Administrator_Create_Model.Email.ToString());
            var Address = b.FindElement(By.Id(nameof(Default_Administrator_Create_Model.Address)));
            Address.SendKeys(Default_Administrator_Create_Model.Address.ToString());
            var FaceBook = b.FindElement(By.Id(nameof(Default_Administrator_Create_Model.FaceBook)));
            FaceBook.SendKeys(Default_Administrator_Create_Model.FaceBook.ToString());
            var WebSite = b.FindElement(By.Id(nameof(Default_Administrator_Create_Model.WebSite)));
            WebSite.SendKeys(Default_Administrator_Create_Model.WebSite.ToString());
            var Reference = b.FindElement(By.Id(nameof(Default_Administrator_Create_Model.Reference)));
            Reference.SendKeys(Default_Administrator_Create_Model.Reference.ToString());
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();
        }
    }
}
