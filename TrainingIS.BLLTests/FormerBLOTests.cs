using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingIS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestData;
using TrainingIS.Entities;

namespace TrainingIS.BLL.Tests
{
    [TestClass()]
    public class FormerBLOTests : Base_BLO_Tests
    {
        public FormerTestDataFactory FormerTestData { set; get; }
        public string Create_Reference = "Test_Insert_Reference";

        public FormerBLOTests()
        {
            FormerTestData = new FormerTestDataFactory(this.UnitOfWork, this.GAppContext);
            Create_Reference = "Test_Insert_Reference";

        }

        [TestMethod()]
        public void SaveTest()
        {
            FormerBLO formerBLO = new FormerBLO(this.UnitOfWork, this.GAppContext);
            Former former = FormerTestData.CreateValideFormerInstance();
            former.Reference = this.Create_Reference;
            former.CreateUserAccount = true;
            former.Login = "Test_Create_Former_Login";
            former.Password = "Test_Create_Former_Login@123456";
            formerBLO.Save(former);
 
        }

        [TestInitialize]
        [TestCleanup]
        public void CleanData()
        {
            FormerBLO formerBLO = new FormerBLO(this.UnitOfWork, this.GAppContext);
            var former = formerBLO.FindBaseEntityByReference(Create_Reference);
            if (former != null)
                formerBLO.Delete(former);
        }
    }
}