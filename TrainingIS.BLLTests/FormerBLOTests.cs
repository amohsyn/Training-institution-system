using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingIS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestData;
using TrainingIS.Entities;
using GApp.UnitTest.DataAnnotations;

namespace TrainingIS.BLL.Tests
{

    // [CleanTestDB] Generate Problem with Save Former
    public partial class FormerBLOTests
    {
        public FormerTestDataFactory FormerTestData { set; get; }
        public string Create_Reference = "Test_Insert_Reference";
        FormerBLO formerBLO;

        public FormerBLOTests()
        {
            FormerTestData = new FormerTestDataFactory(this.UnitOfWork, this.GAppContext);
            Create_Reference = "Test_Insert_Reference";
            formerBLO  = new FormerBLO(this.UnitOfWork, this.GAppContext);
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
            int r =  formerBLO.Save(former);
            

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

        [TestMethod()]
        public void DeleteTest()
        {
            // Create Valide Former 
            Former former = FormerTestData.CreateValideFormerInstance();
            former.CreateUserAccount = true;
            former.Reference = this.Create_Reference;

            formerBLO.Save(former);

            formerBLO.Delete(former);


     
        }
    }
}