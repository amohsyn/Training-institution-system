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

namespace TrainingIS.BLL.Tests
{
    public class Base_BLO_Tests : BaseUnitTest
    {

        public UnitOfWork<TrainingISModel> UnitOfWork;
        public GAppContext GAppContext;

        public Base_BLO_Tests()
        {
            this.UnitOfWork = new UnitOfWork<TrainingISModel>();
            
            this.GAppContext = new GAppContext(RoleBLO.Root_ROLE);
            this.GAppContext.Session.Add(UnitOfWorkBLO.UnitOfWork_Key, this.UnitOfWork);
        }

    }
}
