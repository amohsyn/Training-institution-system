using System;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TrainingIS.Entities;
using TrainingIS.BLL.ModelsViews;
using GApp.Entities;
using GApp.DAL;
using TrainingIS.DAL;
using GApp.Core.Context;
using GApp.UnitTest.UI_Tests;
using GApp.UnitTest.Context;
using TestData;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL;
using System.Linq;
using TrainingIS_UI_Tests.Base;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS_UI_Tests.SeanceNumbers
{
    public partial class Create_SeanceNumber_UI_Tests
    {
        public override void SeanceNumber_UI_Create(SeanceNumber SeanceNumber)
        {
            this.GoTo_Index_And_Login_If_Not_Ahenticated();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert SeanceNumber
            Default_SeanceNumber_Create_Model Default_SeanceNumber_Create_Model = new Default_SeanceNumber_Create_ModelBLM(new UnitOfWork<TrainingISModel>(), GAppContext)
                .ConverTo_Default_SeanceNumber_Create_Model(SeanceNumber);

            var Code = b.FindElement(By.Id(nameof(Default_SeanceNumber_Create_Model.Code)));
            Code.SendKeys(Default_SeanceNumber_Create_Model.Code.ToString());


            b.FindElement(By.Id("StartTime")).SendKeys("0800");
            b.FindElement(By.Id("EndTime")).SendKeys("1030");


            var Description = b.FindElement(By.Id(nameof(Default_SeanceNumber_Create_Model.Description)));
            Description.SendKeys(Default_SeanceNumber_Create_Model.Description.ToString());
            var Reference = b.FindElement(By.Id(nameof(Default_SeanceNumber_Create_Model.Reference)));
            Reference.SendKeys(Default_SeanceNumber_Create_Model.Reference.ToString());
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();
        }
    }
}
