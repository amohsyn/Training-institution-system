using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL;
using TrainingIS.Entities;
using TrainingIS.Entities.enums;

namespace TrainingIS_UI_Tests.Sanctions
{
    [TestCategory("Sanctions")]
    public partial class Create_Sanction_UI_Tests
    {
        [TestInitialize]
        public override void InitData()
        {
            base.InitData();
        }

        [TestCleanup]
        public override void CleanData()
        {
            TraineeBLO traineeBLO = new TraineeBLO(this.UnitOfWork, this.GAppContext);

            Trainee Trainee_With_2_InValide_Sanctions = traineeBLO.FindBaseEntityByReference("CNE85");

            // Delete the Valide Sanction
            var Valid_Sanctions = this.SanctionBLO.Find_Valide_Sanction(Trainee_With_2_InValide_Sanctions.Id);
            foreach (var sanction in Valid_Sanctions)
            {
                // Delete Meeting
                if(sanction.Meeting != null)
                {

                    MeetingBLO meetingBLO = new MeetingBLO(this.UnitOfWork, this.GAppContext);
                    meetingBLO.Delete(sanction.Meeting.Id);
                }
                // Delete the sanction
                this.SanctionBLO.Delete(sanction);
                this.SanctionBLO.Update_InValide_Sanction(Trainee_With_2_InValide_Sanctions.Id);
            }
        }

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


        [TestMethod]
        public void Validate_Sanction_Without_Validate_Previous_Sanctions()
        {
            // BLO 
            TraineeBLO traineeBLO = new TraineeBLO(this.UnitOfWork, this.GAppContext);
          
            this.GoTo_Index_And_Login_If_Not_Ahenticated();

            // Arrange Find The Trainee with 2 InValideSanction
            Trainee Trainee_With_2_InValide_Sanctions = traineeBLO.FindBaseEntityByReference("CNE85");
            var Sanctions = this.SanctionBLO.Find_InValide_Sanction(Trainee_With_2_InValide_Sanctions.Id);
            var Sanction_to_valide = Sanctions.OrderBy(s => s.SanctionCategory.WorkflowOrder).Last();

            // Search the Sanction_to_valide
            this.DataTable.Search(Sanction_to_valide.Reference);

            // Click on Validate_Sanction
            this.DataTable.Init("Sanctions_Entities");
            this.DataTable.Lines[0].Add_Element.Click();

            // Assert
            Assert.AreEqual(Sanctions.Count, 2);
            Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            Assert.IsTrue(this.Alert.Is_Error_Alert());
        }

        [TestMethod]
        public void Validate_Sanction()
        {
            // BLO 
            TraineeBLO traineeBLO = new TraineeBLO(this.UnitOfWork, this.GAppContext);

            this.GoTo_Index_And_Login_If_Not_Ahenticated();

            // Arrange Find The Trainee with 2 InValideSanction
            Trainee Trainee_With_2_InValide_Sanctions = traineeBLO.FindBaseEntityByReference("CNE85");
            var Sanctions = this.SanctionBLO.Find_InValide_Sanction(Trainee_With_2_InValide_Sanctions.Id);
            var Sanction_to_valide = Sanctions.OrderBy(s => s.SanctionCategory.WorkflowOrder).First();

            // Search the Sanction_to_valide
            this.DataTable.Search(Sanction_to_valide.Reference);

            // Click on Validate_Sanction
            this.DataTable.Init("Sanctions_Entities");
            this.DataTable.Lines[0].Add_Element.Click();

            // Assert
            Assert.AreEqual(Sanctions.Count, 2);
            //Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            //Assert.IsTrue(this.Alert.Is_Error_Alert());
        }

        [TestMethod]
        public void Validate_The_Sanction_Without_WorkGroup_Taht_Trait_This_SanctionCategory()
        {
            // [Bug] must Test this function
        }
        [TestMethod]
        public void Validate_The_Sanction_Without_WorkGroup_Taht_Trait_Mission_of__SanctionCategory()
        {
            // [Bug] must Test this function
        }
    }
}
