﻿using GApp.Core.Context;
using GApp.UnitTest.Attributes;
using GApp.UnitTest.Context;
using GApp.UnitTest.UI_Tests;
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
    [TestCategory("SeanceTrainings")]
    public class CreateSeanceTrainings_By_Former_Tests : PageTest
    {

        public CreateSeanceTrainings_By_Former_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context)
        {
        }

        public CreateSeanceTrainings_By_Former_Tests() : base(null) { }

        protected override void Constructor(UI_Test_Context UI_Test_Context)
        {
            base.Constructor(UI_Test_Context);
            this.UI_Test_Context.Login = "Formateur13@gapp.com";
            this.UI_Test_Context.Password = "Formateur@123456";
            this.UI_Test_Context.ControllerName = "SeanceTrainings";
        }

        

        [TestMethod]
        public void Add_Existante_SeanceTraining()
        {

            this.IndexPage.GoTo_Index();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Select SeanceDate : 
            this.DateTimePicker.SelectDate("SeanceDate", "03/09/2018");

            // Select S1
            this.Select.SelectText("SeanceNumberId", "S2");

            // Save Clic
            b.FindElement(By.Id("Create_Seance_Training_Submit")).Click();

            this.Ajax.WaitForAjax();

            string Liste_Of_Absence_Title = b.FindElement(By.XPath("//div[@id='Absences_Trainees']/h2[1]")).Text;
            Assert.AreEqual(Liste_Of_Absence_Title, "Liste des stagiaires");
        }

        [TestMethod]
        public void Add_Absence_And_Statistic_Change_Test()
        {
            this.Add_Existante_SeanceTraining();

            var Number_of_Absence = Convert.ToInt32(b.FindElement(By.XPath("//tr[@id='Trainee_19']/td[5]")).Text);
            var Number_of_Absence_In_Current_Module = Convert.ToInt32(b.FindElement(By.XPath("//tr[@id='Trainee_19']/td[6]")).Text);

            b.FindElement(By.XPath("//tr[@id='Trainee_19']/td[8]/Span[1]")).Click();

            this.Ajax.WaitForAjax();

            var Number_of_Absence_After_Click = Convert.ToInt32(b.FindElement(By.XPath("//tr[@id='Trainee_19']/td[5]")).Text);
            var Number_of_Absence_In_Current_Module_After_Click = Convert.ToInt32(b.FindElement(By.XPath("//tr[@id='Trainee_19']/td[6]")).Text);

            Assert.AreEqual(Number_of_Absence - 1, Number_of_Absence_After_Click);
            Assert.AreEqual(Number_of_Absence_In_Current_Module - 1, Number_of_Absence_In_Current_Module_After_Click);



        }

        [TestMethod]
        public void Absence_Notification_Test()
        {
            this.Add_Existante_SeanceTraining();

            // Trainee_15 : Madani Kamal
            string bell_xpath = "//tr[4]/td[7]/Span[1]";
            bool is_bell_exist = this.Elements.IsElementExist(By.XPath(bell_xpath));

            // Assert bill not exist
            Assert.IsFalse(is_bell_exist);

            // Act : Add absence to Madani Karim
            this.Ajax.AjaxClick("//tr[4]/td[8]/Span[1]");

            // Assert bill exist
            is_bell_exist = this.Elements.IsElementExist(By.XPath(bell_xpath));   
            Assert.IsTrue(is_bell_exist);
        }


        [TestMethod]
        public void Save_The_Content_Of_SeanceTraining_Test()
        {
            this.Add_Existante_SeanceTraining();

            b.FindElement(By.Id("Contained")).SendKeys("Introduction à la formation");
            b.FindElement(By.Id("Save_SeanceTraining_button")).Click();
            Assert.IsTrue(this.Alert.Is_Info_Alert());
            Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            this.Alert.Close();
        }

        [TestMethod]
        public void Create_New_Seance_Planning_Test()
        {

            this.IndexPage.GoTo_Index();
            
            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Select SeanceDate : 
            this.DateTimePicker.SelectDate("SeanceDate", "12/09/2018");
         

            // Select S1
            this.Select.SelectValue("SeanceNumberId", "25");

            // Save Clic
            b.FindElement(By.Id("Create_Seance_Training_Submit")).Click();

            // WaitForAjax();
            string msg_error = this.Alert.GetMessage();

            Assert.IsTrue(msg_error.Contains("Vous essayer de modifier une séance"));


            this.Alert.Close();

        }




       

       



    }
}
