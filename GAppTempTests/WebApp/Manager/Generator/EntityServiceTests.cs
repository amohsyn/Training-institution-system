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
        public void getAllViewsModelsTest()
        {
            EntityService<TrainingISModel> entityService = new EntityService<TrainingISModel>();
            var AllViewsModels = entityService.getAllViewsModels();

            foreach (Type entityType in AllViewsModels.Keys)
            {
                foreach (Type viewModelType in AllViewsModels[entityType])
                {

                }
            }
        }
    }
}