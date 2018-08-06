using Microsoft.VisualStudio.TestTools.UnitTesting;
using GApp.WebApp.Manager.Generator.MetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;
using TrainingIS.Entities.ModelsViews.Generated;

namespace GApp.WebApp.Manager.Generator.MetaData.Tests
{
    [TestClass()]
    public class ModelViewMetaDataTests
    {
        [TestMethod()]
        public void GetAllSelectFilterTest()
        {

            EntityService<TrainingISModel> entityService = new EntityService<TrainingISModel>();
            List<Type> Entities = entityService.getAllEntities();

            foreach (var typeofEntity in Entities)
            {

                ModelView_CodeGenerator<TrainingISModel> ModelView_CodeGenerator = new ModelView_CodeGenerator<TrainingISModel>(typeofEntity, new DefaultModelView_MetaData().ModelsViewsTypes);
                ConvertoTo_CodeGenerator<TrainingISModel> ConvertoTo_CodeGenerator = new ConvertoTo_CodeGenerator<TrainingISModel>(typeofEntity);

                foreach (var item in ModelView_CodeGenerator.getAllViewsModels())
                {
                    foreach (var models_views in ModelView_CodeGenerator.getAllViewsModels())
                    {
                        foreach (var model_view in models_views.Value)
                        {
                            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(model_view);

                            if(model_view.Name == "AuthrorizationAppFormView")
                            {
                                var select_filters = modelViewMetaData.GetAllSelectFilter();
                                foreach (var select_filter in select_filters)
                                {

                                }

                            }
                           
                        }
                    }
                }


            }
        }
    }
}