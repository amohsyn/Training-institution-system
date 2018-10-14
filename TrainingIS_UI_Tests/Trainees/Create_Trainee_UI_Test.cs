using GApp.Core.Context;
using GApp.DAL;
using GApp.UnitTest.Context;
using GApp.UnitTest.UI_Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestData;
using TrainingIS.BLL;
using TrainingIS.DAL;
using TrainingIS.Entities;

namespace TrainingIS_UI_Tests.Trainees
{
    public partial class Create_Trainee_UI_Tests
    {
        protected override void Constructor(UI_Test_Context UI_Test_Context)
        {
            base.Constructor(UI_Test_Context);
            this.UI_Test_Context.Login = "Supervisor";
            this.UI_Test_Context.Password = "Supervisor@123456";
        }

        [TestInitialize]
        public override void InitData()
        {
            this.CleanData();
            this.Valide_Entity_Insrance.Email = string.Format("madani_{0}@gmail.com", this.Entity_Reference);
        }

    }
}
