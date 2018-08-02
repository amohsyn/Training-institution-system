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
        public void ModelsViewsTypes()
        {
            //EntityService<TrainingISModel> entityService = new EntityService<TrainingISModel>();
            //List<Type> Entities = entityService.getAllEntities();

            //foreach (var typeofEntity in Entities)
            //{
            //    ModelView_CodeGenerator<TrainingISModel> ModelView_CodeGenerator = new ModelView_CodeGenerator<TrainingISModel>(typeofEntity, new DefaultModelView_MetaData().ModelsViewsTypes);
            //    foreach (var item in ModelView_CodeGenerator.getRequiredProperties())
            //    {
            //        var models_views = ModelView_CodeGenerator.ModelsViewsTypes;
            //        Assert.IsNotNull(models_views);

                    
            //    }
            //}
        }

        public void Csharp_Code_Generator()
        {
            

        }
    }
}