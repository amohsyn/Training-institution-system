using Microsoft.VisualStudio.TestTools.UnitTesting;
using GApp.WebApp.Manager.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;

namespace GApp.WebApp.Manager.Generator.Tests
{
    [TestClass()]
    public class EntityServiceTests
    {
        [TestMethod()]
        public void getAllEntitiesTest()
        {
            EntityService<TrainingISModel> entityService = new EntityService<TrainingISModel>();
            var all_entities = entityService.getAllEntities();
            Assert.IsTrue(all_entities.Count > 0);

        }
    }
}