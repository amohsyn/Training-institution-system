using GApp.Core.Context;
using GApp.UnitTest.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TrainingIS_UI_Tests.SeanceTrainings
{
    [TestClass]
    public class CreateSeanceTrainings_By_Former_Tests : Base_UI_Tests
    {


        public CreateSeanceTrainings_By_Former_Tests() : base("essarraj.fouad@gmail.com", "Formateur@123456", "/SeanceTrainings")
        {
           

        }




        [TestMethod]
        [Order(1)]
        public void A_Add_Existante_SeanceTraining()
        {

            this.GoTo_Index();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Select SeanceDate : 11/09/2018
            Selecte_Date_10_09_2018();

            // Select S1
            this.Select("SeanceNumberId", "25");

            // Save Clic
            b.FindElement(By.Id("Create_Seance_Training_Submit")).Click();

            WaitForAjax();

            string Liste_Of_Absence_Title = b.FindElement(By.XPath("//div[@id='Absences_Trainees']/h2[1]")).Text;
            Assert.AreEqual(Liste_Of_Absence_Title, "Liste des stagiaires");
        }

        [TestMethod]
        [Order(2)]
        public void B_Add_Absence_And_Statistic_Change_Test()
        {

            var Number_of_Absence = Convert.ToInt32(b.FindElement(By.XPath("//tr[@id='Trainee_19']/td[4]")).Text);
            var Number_of_Absence_In_Current_Module = Convert.ToInt32(b.FindElement(By.XPath("//tr[@id='Trainee_19']/td[5]")).Text);

            b.FindElement(By.XPath("//tr[@id='Trainee_19']/td[7]/Span[1]")).Click();

            WaitForAjax();

            var Number_of_Absence_After_Click = Convert.ToInt32(b.FindElement(By.XPath("//tr[@id='Trainee_19']/td[4]")).Text);
            var Number_of_Absence_In_Current_Module_After_Click = Convert.ToInt32(b.FindElement(By.XPath("//tr[@id='Trainee_19']/td[5]")).Text);

            Assert.AreEqual(Number_of_Absence + 1, Number_of_Absence_After_Click);
            Assert.AreEqual(Number_of_Absence_In_Current_Module + 1, Number_of_Absence_In_Current_Module_After_Click);



        }

        [TestMethod]
        [Order(3)]
        public void C_Absence_Notification_Test()
        {

            // Trainee_15 : Madani Karim
            string bell_xpath = "//tr[@id='Trainee_15']/td[6]/Span[1]";
            bool is_bell_exist = this.IsElementXPathExist(bell_xpath);

            // Assert bill not exist
            Assert.IsFalse(is_bell_exist);

            // Act : Add absence to Madani Karim
            this.AjaxClick("//tr[@id='Trainee_15']/td[7]/Span[1]");

            // Assert bill exist
            is_bell_exist = this.IsElementXPathExist(bell_xpath);
            Assert.IsTrue(is_bell_exist);
        }

        [TestMethod]
        [Order(4)]
        public void D_Save_The_Content_Of_SeanceTraining_Test()
        {
            b.FindElement(By.Id("Contained")).SendKeys("Introduction à la formation");
            b.FindElement(By.Id("Save_SeanceTraining_button")).Click();
            Assert.IsTrue(this.Alert.Is_Info_Alert());
            Assert.IsTrue(this.Is_In_IndexPage());
            this.Alert.Close();
        }



        [TestMethod]
        [Order(5)]
        public void E_Create_New_Seance_Planning_Test()
        {

            this.GoTo_Index();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Select SeanceDate : 11/09/2018
            Selecte_Date_12_09_2018();

            // Select S1
            this.Select("SeanceNumberId", "25");

            // Save Clic
            b.FindElement(By.Id("Create_Seance_Training_Submit")).Click();

            // WaitForAjax();
            string msg_error = this.Alert.GetMessage();

            Assert.IsTrue(msg_error.Contains("Vous essayer de modifier une séance"));


            this.Alert.Close();



        }




        /// <summary>
        /// Select 10/09/2018 
        /// </summary>
        private void Selecte_Date_10_09_2018()
        {
            b.FindElement(By.Id("SeanceDate")).Click();
            b.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Date de la séance'])[1]/following::th[2]")).Click();
            b.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Sa'])[1]/following::th[2]")).Click();
            b.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Dec'])[1]/following::span[8]")).Click();
            b.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Aug'])[1]/following::span[1]")).Click();
            b.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Sa'])[1]/following::td[16]")).Click();
        }

        /// <summary>
        /// Select 10/09/2018 
        /// </summary>
        private void Selecte_Date_12_09_2018()
        {
            b.FindElement(By.Id("SeanceDate")).Click();
            b.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Date de la séance'])[1]/following::th[2]")).Click();
            b.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Sa'])[1]/following::th[2]")).Click();
            b.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Dec'])[1]/following::span[8]")).Click();
            b.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Aug'])[1]/following::span[1]")).Click();
            b.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Sa'])[1]/following::td[18]")).Click();
        }



    }
}
