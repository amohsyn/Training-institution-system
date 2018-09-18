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
    public class Edit_Seance_Trainings_By_Former_Tests : Base_UI_Tests
    {
        public Edit_Seance_Trainings_By_Former_Tests() : base("essarraj.fouad@gmail.com", "Formateur@123456", "/SeanceTrainings")
        {}
 
        [TestMethod]
        [Order(1)]
        public void Edit_ALL_SeanceTraining()
        {

            this.IndexPage.GoTo_Index();

            this.DataTable.FindElements();
            int count = this.DataTable.Lines.Count;
            Assert.AreEqual(count, 4);

           
            for (int index = 0; index < count; index++)
            {

                this.DataTable.FindElements();
                var line = this.DataTable.Lines[index];
                string Expected_Url = line.Edit_Element.GetAttribute("href");
                string SeancePlanning_StringValue = line[2].Text;


                line.Edit_Element.Click();
                string Title = this.EditPage.Get_Title_Element().Text;


                Assert.IsTrue(b.Url.Contains(Expected_Url));
                Assert.IsTrue(Title.Contains(SeancePlanning_StringValue));

                this.IndexPage.GoTo_Index();
            }

               
            
        }
    }
}
