using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingIS_UI_Tests.Meetings
{
    [TestCategory("Sanctions")]
    public partial class Create_Meeting_UI_Tests
    {
        [TestMethod]
        public override void Meeting_Create_Test()
        {
          // Changed by 
          // - Meeting_Create_CD_Test
        }

        [TestMethod]
       
        public   void Meeting_Create_CD_Test()
        {
            this.GoTo_Index_And_Login_If_Not_Ahenticated();

            // Index create click 
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();
             

            // Create CD : Conseil deciolinaire
            var Create_Meeting_CD = b.FindElement(By.Id("Create_By_WorkGroup_CD"));
            Create_Meeting_CD.Click();

            // Check Code 
            var WorkGroup_Code = b.FindElement(By.Id("WorkGroup_Code"));
            Assert.AreEqual(WorkGroup_Code.Text, "CD");

            // Cehck Date 
            var MeetingDate_Element = b.FindElement(By.Id("MeetingDate"));
            DateTime dateTime = DateTime.Now.Date;
            DateTime MeetingDate = Convert.ToDateTime(MeetingDate_Element.GetAttribute("value"));

            // Check Missions
            // Mission_Working_GroupId
            var All_Missions = this.Select.Get_All_Texts("Mission_Working_GroupId");



        }
    }
}
