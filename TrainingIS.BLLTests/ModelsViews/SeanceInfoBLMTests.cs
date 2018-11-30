using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingIS.BLL.ModelsViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL.Tests;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews.Tests
{
    [TestClass()]
    public class SeanceInfoBLMTests : Base_BLO_Tests
    {
        [TestMethod()]
        public void Find_All_Planified_SeanceTrainingTest()
        {
            SeanceInfoBLM seanceInfoBLM = new SeanceInfoBLM(this.UnitOfWork, this.GAppContext);

            int totalRecords;
            FilterRequestParams filterRequestParams = new FilterRequestParams();

            var ls = seanceInfoBLM.Find(filterRequestParams,null, out totalRecords);
        }
    }
}