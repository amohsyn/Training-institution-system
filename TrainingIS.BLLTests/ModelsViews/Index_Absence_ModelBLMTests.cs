using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingIS.BLL.ModelsViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL.Tests;
using TrainingIS.Entities;

namespace TrainingIS.BLL.ModelsViews.Tests
{
    [TestClass()]
    public class Index_Absence_ModelBLMTests : Base_BLO_Tests
    {
        [TestMethod()]
        public void FindAll_Search_Test()
        {
            Index_Absence_ModelBLM index_Absence_ModelBLM = new Index_Absence_ModelBLM(this.UnitOfWork, this.GAppContext);

            int total;
            var index_absences = index_Absence_ModelBLM.Find(null, null, "Madani", null, null, null, out total);
            Assert.AreEqual(total, 2);
        }

        [TestMethod()]
        public void FindAll_Order_By_Trainee_Test()
        {
            Index_Absence_ModelBLM index_Absence_ModelBLM = new Index_Absence_ModelBLM(this.UnitOfWork, this.GAppContext);

            int total;

            var index_absences = index_Absence_ModelBLM.Find("Trainee_Asc", null, "Madani", null, null, null, out total);
            Assert.AreEqual(index_absences.First().Trainee.LastName, "Karim");


            index_absences = index_Absence_ModelBLM.Find("Trainee_Desc",null, "Madani", null, null, null, out total);
            Assert.AreEqual(index_absences.First().Trainee.LastName, "Yousra");

            Assert.AreEqual(total, 2);
        }

        [TestMethod()]
        public void FindAll_Current_Page_Test()
        {
            Index_Absence_ModelBLM index_Absence_ModelBLM = new Index_Absence_ModelBLM(this.UnitOfWork, this.GAppContext);

            int total;

            var index_absences = index_Absence_ModelBLM.Find("Trainee_Asc",null, null,null, 2, 3, out total);

            Assert.AreEqual(total, index_absences.Count());
            Assert.AreEqual(index_absences.First().Trainee.FirstName, "Andalousi");
            Assert.AreEqual(index_absences.First().Trainee.LastName, "Karim");

        }

        [TestMethod()]
        public void FindTest()
        {
            Index_Absence_ModelBLM index_Absence_ModelBLM = new Index_Absence_ModelBLM(this.UnitOfWork, this.GAppContext);

            int total;
            string orderBy = "SeanceTraining.SeancePlanning.Training.Group.Code";
            string filterBy = "[SeanceTraining.SeancePlanning.Training.Group.Reference,=TDI101-2019];[Trainee.FirstName,=Madani]";
            string searchBy = "";
            var index_absences = index_Absence_ModelBLM.Find(orderBy, filterBy, searchBy,null, 1, 3, out total);


            Assert.AreEqual(total, index_absences.Count());
            Assert.AreEqual(total, 1);
            Assert.AreEqual(index_absences.First().Trainee.FirstName, "Madani");
        }

        [TestMethod()]
        public void Find_SearchBy_Test()
        {
            Index_Absence_ModelBLM index_Absence_ModelBLM = new Index_Absence_ModelBLM(this.UnitOfWork, this.GAppContext);

            int total;

            List<string> SearchCreteria = new List<string>
            {
                "SeanceTraining.SeancePlanning.Training.Group.Reference",
                "Trainee.FirstName",
                "Trainee.LastName",
                "isHaveAuthorization"
            };


            string orderBy = "";
            string filterBy = "";
            string searchBy = "Madani";
            var index_absences = index_Absence_ModelBLM.Find(orderBy, filterBy, searchBy, SearchCreteria, 1, 3, out total);

            
            Assert.AreEqual(total, index_absences.Count());
            Assert.AreEqual(total, 2);
            foreach (var item in index_absences)
            {
                Assert.AreEqual(item.Trainee.FirstName, "Madani");
            }

        }
    }
}