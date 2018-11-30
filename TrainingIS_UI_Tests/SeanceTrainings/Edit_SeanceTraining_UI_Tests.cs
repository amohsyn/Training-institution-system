using GApp.UnitTest.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingIS_UI_Tests.SeanceTrainings
{
    public partial class Edit_SeanceTraining_UI_Tests
    {
        protected override void Constructor(UI_Test_Context UI_Test_Context)
        {
            base.Constructor(UI_Test_Context);
            this.GAppContext.Current_User_Name = "Formateur13@gapp.com";

            this.UI_Test_Context.Login = "Formateur13@gapp.com";
            this.UI_Test_Context.Password = "Formateur@123456";
            this.UI_Test_Context.ControllerName = "SeanceTrainings";
        }
    }
}
