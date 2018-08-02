using Microsoft.VisualStudio.TestTools.UnitTesting;
using GApp.WebApp.Manager.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;
using GApp.WebApp.Manager.Views;
using TrainingIS.Entities.ModelsViews.Generated;

namespace GApp.WebApp.Manager.Generator.Tests
{
    [TestClass()]
    public class ConvertoTo_CodeGeneratorTests
    {
        [TestMethod()]
        public void Search_Property_In_ObjectTest()
        {
            EntityService<TrainingISModel> entityService = new EntityService<TrainingISModel>();
            List<Type> Entities = entityService.getAllEntities();

            foreach (var typeofEntity in Entities)
            {

                ModelView_CodeGenerator<TrainingISModel> ModelView_CodeGenerator = new ModelView_CodeGenerator<TrainingISModel>(typeofEntity, new DefaultModelView_MetaData().ModelsViewsTypes);
                ConvertoTo_CodeGenerator<TrainingISModel> ConvertoTo_CodeGenerator = new ConvertoTo_CodeGenerator<TrainingISModel>(typeofEntity);

                foreach (var item in ModelView_CodeGenerator.GetCreatedProperties())
                {
                    var default_code = ConvertoTo_CodeGenerator.Search_Property_In_Object(typeofEntity, item);
                   // Assert.IsTrue(!string.IsNullOrEmpty(default_code));
                }

            
            }
        }
    }
}