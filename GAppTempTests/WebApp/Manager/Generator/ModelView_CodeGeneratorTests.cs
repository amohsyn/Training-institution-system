using Microsoft.VisualStudio.TestTools.UnitTesting;
using GApp.WebApp.Manager.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews.Generated;
using TrainingIS.DAL;
using GApp.Core.MetaDatas.Attributes;
using System.Reflection;

namespace GApp.WebApp.Manager.Generator.Tests
{
    [TestClass()]
    public class ModelView_CodeGeneratorTests
    {
        [TestMethod()]
        public void getAllViewsModelsTest()
        {
            Dictionary<Type, List<Type>> ModelsViewsTypes = new DefaultModelView_MetaData().ModelsViewsTypes;
            ModelViewsService<TrainingISModel> _ModelViewsService = new ModelViewsService<TrainingISModel>(ModelsViewsTypes);
            Dictionary<Type, List<Type>> AllViewsModels = _ModelViewsService.getAllViewsModels(ModelsViewsTypes);

            foreach (Type entityType in AllViewsModels.Keys)
            {
                ConvertoTo_CodeGenerator<TrainingISModel> ConvertoTo_CodeGenerator = new ConvertoTo_CodeGenerator<TrainingISModel>(entityType);
                foreach (Type viewModelType in AllViewsModels[entityType])
                {

                    foreach (var viewModelProperty in viewModelType.GetProperties())
                    {

                        if (viewModelProperty.IsDefined(typeof(ManyAttribute)))
                        {
                            int i = 0;
                        }
                        else
                        {
                            string value_code = ConvertoTo_CodeGenerator.Search_Property_In_Object(entityType, viewModelProperty);
                            if (string.IsNullOrEmpty(value_code)) continue;
                        }


                    }


                }
            }
        }
    }
}