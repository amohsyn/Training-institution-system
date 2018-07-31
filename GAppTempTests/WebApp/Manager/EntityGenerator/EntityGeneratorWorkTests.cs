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
    public class ModelView_CodeGenerator_Tests
    {
        [TestMethod()]
        public void getIndexModelView_TypeTest()
        {
            EntityService<TrainingISModel> entityService = new EntityService<TrainingISModel>();
            List<Type> Entities = entityService.getAllEntities();

            foreach (var typeofEntity in Entities)
            {
                EntityGeneratorWork<TrainingISModel> entityGen = new EntityGeneratorWork<TrainingISModel>(typeofEntity);
                foreach (var item in entityGen.getRequiredProperties())
                {
                    var models_views = entityGen.ModelsViewsTypes;
                    Assert.IsNotNull(models_views);
                }
            }
        }
    }
}