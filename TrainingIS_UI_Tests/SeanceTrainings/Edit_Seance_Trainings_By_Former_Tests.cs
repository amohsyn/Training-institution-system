using GApp.Core.Context;
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
    public class Edit_Seance_Trainings_By_Former_Tests : PageTest
    {
        public Edit_Seance_Trainings_By_Former_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context)
        {
        }

        public Edit_Seance_Trainings_By_Former_Tests() : base(null) { }

        protected override void Constructor(UI_Test_Context UI_Test_Context)
        {
            base.Constructor(UI_Test_Context);
            this.UI_Test_Context.Login = "essarraj.fouad@gmail.com";
            this.UI_Test_Context.Password = "Formateur@123456";
            this.UI_Test_Context.ControllerName = "SeanceTrainings";
        }
 
 
        [TestMethod]
        [Order(1)]
        public void Edit_ALL_SeanceTraining()
        {

            this.GoTo_Index_And_Login_If_Not_Ahenticated();

            this.DataTable.Init("SeanceTrainings_Entities");
            int count = this.DataTable.Lines.Count;
            Assert.AreEqual(count, 4);

           
            for (int index = 0; index < count; index++)
            {

                this.DataTable.Init("SeanceTrainings_Entities");
                var line = this.DataTable.Lines[index];
                string Expected_Url = line.Edit_Element.GetAttribute("href");
                string SeancePlanning_StringValue = line[2].Text;


                line.Edit_Element.Click();
                string Title = this.EditPage.Get_Title_Element().Text;
                this.Ajax.WaitForAjax();

                Assert.IsTrue(b.Url.Contains(Expected_Url));
                Assert.IsTrue(Title.Contains(SeancePlanning_StringValue));

                this.IndexPage.GoTo_Index();
            }

               
            
        }
    }
}
