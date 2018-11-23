using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestData.TestData_Descriptions;
using TrainingIS.BLL;
using TrainingIS.Entities;

namespace TrainingIS_UI_Tests.Absences
{
    public partial class Edit_Absence_UI_Tests
    {
        [TestInitialize]
        public override void InitData()
        {
            //BLO
            SeanceTrainingBLO seanceTrainingBLO = new SeanceTrainingBLO(this.UnitOfWork, this.GAppContext);
            TraineeBLO traineeBLO = new TraineeBLO(this.UnitOfWork, this.GAppContext);

            SeanceTraining Absence_CRUD_Tests_seanceTraining = seanceTrainingBLO.FindBaseEntityByReference(SeanceTraining_TestData_Description.CRUD_Absences_Tests_SeanceTraining_Reference);
            Trainee Absence_CRUD_Tests_Trainee = traineeBLO.FindBaseEntityByReference(Trainee_TestData_Description.CRUD_Absences_Tests_Trainee_Reference);

            this.Valide_Entity_Instance = this.AbsenceBLO.CreateInstance();
            this.Valide_Entity_Instance.SeanceTraining = Absence_CRUD_Tests_seanceTraining;
            this.Valide_Entity_Instance.Trainee = Absence_CRUD_Tests_Trainee;
            this.Valide_Entity_Instance.Reference = Entity_Reference;
        }

        public override void Absence_Edit_Test()
        {
            // Add Absence to be Edited
            this.AbsenceBLO.Save(this.Valide_Entity_Instance);

            this.GoTo_Index_And_Login_If_Not_Ahenticated();

            // Search the created entity
            this.DataTable.Search(this.Valide_Entity_Instance.Reference);

            // Edit the entity
            this.DataTable.Init("Absences_Entities");
            this.DataTable.Lines[0].Edit_Element.Click();

            // Submit Edit Form
            this.Html.Click("Edit_Entity_Submit");

            // Assert
            this.IndexPage.Is_In_IndexPage();
            this.Alert.Is_Info_Alert();
        }
    }
}
