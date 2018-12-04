using GApp.UnitTest.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;

namespace TrainingIS_UI_Tests.Formers
{
    public partial class Edit_Former_UI_Tests
    {
        [TestCleanup]
        public override void CleanData()
        {
            // Clean Create Data Test
            Former Create_Data_Test = FormerBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (Create_Data_Test != null)
                FormerBLO.Delete(Create_Data_Test);

            // Clean Data By Email
            Former Former_By_Email = FormerBLO.Find_By_Email(this.Valide_Entity_Instance.Email);
            if (Former_By_Email != null)
                FormerBLO.Delete(Former_By_Email);
        }

        protected override void Constructor(UI_Test_Context UI_Test_Context)
        {
            base.Constructor(UI_Test_Context);
            this.Valide_Entity_Instance.Email = "CRUD_Former_Tests@gapp.com";
        }
    }
}
