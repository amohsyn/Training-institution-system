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
    public class Base_Group_UI_Tests : Base_UI_Tests
    {
       

        public Base_Group_UI_Tests()
        {
            this.Entity_Path = "/Groups";
        }
       
        [TestMethod]
        public virtual void Group_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

        [TestMethod]
        public virtual void Group_Create_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Group
            Group Group = new GroupsControllerTests_Service().CreateValideGroupInstance(null,GAppContext);
            CreateGroupView CreateGroupView = new CreateGroupViewBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_CreateGroupView(Group);



			this.Select.SelectValue("TrainingYearId", CreateGroupView.TrainingYearId.ToString());

			this.Select.SelectValue("SpecialtyId", CreateGroupView.SpecialtyId.ToString());

			this.Select.SelectValue("TrainingTypeId", CreateGroupView.TrainingTypeId.ToString());

			this.Select.SelectValue("YearStudyId", CreateGroupView.YearStudyId.ToString());

	 


 
			var Code = b.FindElement(By.Id(nameof(CreateGroupView.Code)));
            Code.SendKeys(CreateGroupView.Code.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }

    [TestClass]
	public partial class Group_UI_Tests : Base_Group_UI_Tests
    {

    }
}
