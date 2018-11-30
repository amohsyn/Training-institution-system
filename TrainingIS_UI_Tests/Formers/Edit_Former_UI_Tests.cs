using GApp.UnitTest.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingIS_UI_Tests.Formers
{
    public partial class Edit_Former_UI_Tests
    {
        protected override void Constructor(UI_Test_Context UI_Test_Context)
        {
            base.Constructor(UI_Test_Context);
            this.Valide_Entity_Instance.Email = "CRUD_Former_Tests@gapp.com";
        }
    }
}
