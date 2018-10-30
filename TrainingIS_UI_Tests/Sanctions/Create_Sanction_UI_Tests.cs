using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingIS_UI_Tests.Sanctions
{
    public partial class Create_Sanction_UI_Tests
    {
        [TestMethod]
        public override void Sanction_Create_Test()
        {
            this.Add_Sanction_Message_Redirection_To_Add_Meeting_Test();
        }

    
        public void Add_Sanction_Message_Redirection_To_Add_Meeting_Test()
        {
            this.GoTo_Index_And_Login_If_Not_Ahenticated();

            // Create Sanction
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Assert Create Meeting Page 
            this.CreatePage.Is_In_CreatePage();

            // Assert Message Info
            this.Alert.Is_Info_Alert();
            string msg = this.Alert.GetMessage();
            Assert.AreEqual(msg, "Vous devez ajouter une réuion pour créer une sanction");
            
        }
    }
}
