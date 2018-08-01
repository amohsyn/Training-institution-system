using Microsoft.VisualStudio.TestTools.UnitTesting;
using GApp.WebApp.Manager.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;
using GApp.Entities;
using TrainingIS.Entities;
using System.Reflection;

namespace GApp.WebApp.Manager.Generator.Tests
{
    [TestClass()]
    public class EntityGeneratorWorkTests
    {
        [TestMethod()]
        public void Code_Of_DefaultValueTest()
        {

            EntityService<TrainingISModel> entityService = new EntityService<TrainingISModel>();
            List<Type> Entities = entityService.getAllEntities();

            foreach (var typeofEntity in Entities)
            {
                EntityGeneratorWork<TrainingISModel> entityGen = new EntityGeneratorWork<TrainingISModel>(typeofEntity);
                foreach (var item in entityGen.getRequiredProperties())
                {
                    var default_code = entityGen.Code_Of_DefaultValue(item.PropertyType);
                    Assert.IsTrue(!string.IsNullOrEmpty(default_code));
                }

                foreach (var item in entityGen.getUniqueProperties())
                {
                    var default_code = entityGen.Code_Of_DefaultValue(item.PropertyType);
                    Assert.IsTrue(!string.IsNullOrEmpty(default_code));
                }
            }



        }

        [TestMethod()]
        public void GetIndexPropertiesTest()
        {
            EntityService<TrainingISModel> entityService = new EntityService<TrainingISModel>();
            List<Type> Entities = entityService.getAllEntities();

            foreach (var typeofEntity in Entities)
            {
                EntityGeneratorWork<TrainingISModel> entityGen = new EntityGeneratorWork<TrainingISModel>(typeofEntity);
                foreach (var item in entityGen.GetIndexProperties())
                {
                    var local_name = item.getLocalName();
                    Assert.IsNotNull(local_name);
                }


            }
        }

        [TestMethod()]
        public void getAllViewsModelsTest()
        {
           
        }
    }
}