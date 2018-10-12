using GApp.Core.Context;
using GApp.DAL;
using GApp.UnitTest;
using GApp.UnitTest.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TrainingIS.DAL;
using TrainingIS.Entities;

namespace TrainingIS.BLL.Tests
{
    public class Base_BLO_Tests : BaseUnitTest
    {

        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

        public Base_BLO_Tests()
        {
            this.UnitOfWork = new UnitOfWork<TrainingISModel>();
           
            // Create GAppContext Instance
            this.GAppContext = new GAppContext(RoleBLO.Root_ROLE);
            TrainingYear CurrentTrainingYear = new TrainingYearBLO(this.UnitOfWork, this.GAppContext).getCurrentTrainingYear();

            // Fill GAppContext
            this.GAppContext.Session.Add(UnitOfWorkBLO.UnitOfWork_Key, this.UnitOfWork);
            this.GAppContext.Session.Add(TrainingYearBLO.Current_TrainingYear_Key, CurrentTrainingYear);

            // Show SQL 
          //  this.UnitOfWork.context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

    }
}
