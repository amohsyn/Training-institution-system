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
using TrainingIS.Entities.ModelsViews.GroupModelsViews;
namespace TrainingIS_UI_Tests.Groups
{
    public class Base_Group_Create_UI_Tests : Base_UI_Tests
    {
       

        public Base_Group_Create_UI_Tests()
        {
            this.Entity_Path = "/Groups";
        }
       
        [TestMethod]
        public virtual void Group_Index_Show_Test()
        {
            this.GoTo_Index();
        }

        [TestMethod]
        public virtual void Group_Create_Test()
        {
            this.GoTo_Index();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            Group Group = new GroupsControllerTests_Service().CreateValideGroupInstance(null,GAppContext);
            CreateGroupView CreateGroupView = new CreateGroupViewBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_CreateGroupView(Group);



			string xpath_TrainingYearId = string.Format("//select[@id='{0}']/option[@value='{1}']", "TrainingYearId", CreateGroupView.TrainingYearId.ToString());
            b.FindElement(By.XPath(xpath_TrainingYearId)).Click(); 

			string xpath_SpecialtyId = string.Format("//select[@id='{0}']/option[@value='{1}']", "SpecialtyId", CreateGroupView.SpecialtyId.ToString());
            b.FindElement(By.XPath(xpath_SpecialtyId)).Click(); 

			string xpath_TrainingTypeId = string.Format("//select[@id='{0}']/option[@value='{1}']", "TrainingTypeId", CreateGroupView.TrainingTypeId.ToString());
            b.FindElement(By.XPath(xpath_TrainingTypeId)).Click(); 

			string xpath_YearStudyId = string.Format("//select[@id='{0}']/option[@value='{1}']", "YearStudyId", CreateGroupView.YearStudyId.ToString());
            b.FindElement(By.XPath(xpath_YearStudyId)).Click(); 

 
			var Code = b.FindElement(By.Id(nameof(CreateGroupView.Code)));
            Code.SendKeys(CreateGroupView.Code.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }

    [TestClass]
	public partial class Group_Create_UI_Tests : Base_Group_Create_UI_Tests
    {

    }
}
