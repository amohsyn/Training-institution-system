//using System;
//using AutoFixture;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using OpenQA.Selenium;
//using OpenQA.Selenium.Chrome;
//using TrainingIS.Entities;
//using TrainingIS.WebApp.Controllers.Tests;
//using OpenQA.Selenium.Support.UI;

//namespace TrainingIS_UI_Tests
//{
//    [TestClass]
//    public class Group_UI_Index_Tests : Base_UI_Tests
//    {


//        public Group_UI_Index_Tests()
//        {
//            this.Entity_Path = "/Groups";
//        }

//        [TestMethod]
//        public void Group_Index_Show_Test()
//        {
//            this.GoTo_Index();
//        }

//        [TestMethod]
//        public void Group_Create_Test()
//        {
//            this.GoTo_Index();

//            // Index create click Test
//            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
//            CreateElement.Click();

//            // Insert Former
//            Group Group = new GroupsControllerTests_Service().CreateValideGroupInstance();


           

//            SelectElement TrainingYearId = new SelectElement(b.FindElement(By.Id(nameof(Group.TrainingYearId))));
//            TrainingYearId.SelectByValue(Group.TrainingYearId.ToString());


//            SelectElement SpecialtyId = new SelectElement(b.FindElement(By.Id(nameof(Group.SpecialtyId))));
//            TrainingYearId.SelectByValue(Group.SpecialtyId.ToString());

          
//            string xpath_SpecialtyId = string.Format("//select[@id='{0}']/option[@value='{1}']", "SpecialtyId", Group.SpecialtyId.ToString());
//            b.FindElement(By.XPath(xpath_SpecialtyId)).Click();


//            SelectElement TrainingTypeId = new SelectElement(b.FindElement(By.Id(nameof(Group.TrainingTypeId))));
//            TrainingYearId.SelectByValue(Group.TrainingTypeId.ToString());


//            SelectElement YearStudyId = new SelectElement(b.FindElement(By.Id(nameof(Group.YearStudyId))));
//            TrainingYearId.SelectByValue(Group.YearStudyId.ToString());

            

//            var Code = b.FindElement(By.Id(nameof(Group.Code)));
//            Code.SendKeys(Group.Code);

//            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
//            Create_Entity_Form.Submit();


//            Assert.IsTrue(this.Is_In_IndexPage());
//            Assert.IsTrue(this.Is_Info_Alert());
//        }


//    }
//}
