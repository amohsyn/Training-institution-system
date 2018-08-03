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
using System.Reflection;

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

        [TestMethod()]
        public void Fin_ManyProperty_In_ModelViewTest()
        {
            Dictionary<Type, List<Type>> ModelsViewsTypes = new DefaultModelView_MetaData().ModelsViewsTypes;
            ModelViewsService<TrainingISModel> _ModelViewsService = new ModelViewsService<TrainingISModel>(ModelsViewsTypes);
            Dictionary<Type, List<Type>> AllViewsModels = _ModelViewsService.getAllViewsModels(ModelsViewsTypes);

            foreach (Type entityType in AllViewsModels.Keys)
            {
                ConvertoTo_CodeGenerator<TrainingISModel> ConvertoTo_CodeGenerator = new ConvertoTo_CodeGenerator<TrainingISModel>(entityType);
                RelationShip_CodeGenerator<TrainingISModel> RelationShip_CodeGenerator = new RelationShip_CodeGenerator<TrainingISModel>(entityType);
                foreach (Type viewModelType in AllViewsModels[entityType])
                {
                    foreach (var enityProperty in entityType.GetProperties())
                    {
                        PropertyInfo viewModelProperty = ConvertoTo_CodeGenerator.Fin_ManyProperty_In_ModelView(viewModelType, enityProperty);
                    }

                }
            }
        }
    }
}