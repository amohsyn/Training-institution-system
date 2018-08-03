using Microsoft.VisualStudio.TestTools.UnitTesting;
using GApp.Dev.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.WebApp.Manager.Generator;
using TrainingIS.DAL;
using TrainingIS.Entities.ModelsViews.Generated;

namespace GApp.Dev.Generator.Tests
{
    [TestClass()]
    public class CodeStringCsharpTests
    {
        [TestMethod()]
        public void PropertyTest()
        {
            EntityService<TrainingISModel> entityService = new EntityService<TrainingISModel>();
            List<Type> Entities = entityService.getAllEntities();

            foreach (var typeofEntity in Entities)
            {
                CodeStringCsharp codeStringCsharp = new CodeStringCsharp( Operations.Edit);

                ModelView_CodeGenerator<TrainingISModel> ModelView_CodeGenerator = new ModelView_CodeGenerator<TrainingISModel>(typeofEntity, new DefaultModelView_MetaData().ModelsViewsTypes);
                List<string> namesSapces = new List<string>();
                foreach (var item in typeofEntity.GetProperties())
                {
                    //if(item.Name == "SchoollevelId")
                    //{


                    var code_property = codeStringCsharp.Property(item, namesSapces);
                    Assert.IsTrue(!string.IsNullOrEmpty(code_property));
                    //}



                }
            }
        }

        [TestMethod()]
        public void GenerateCodePropertiesTest()
        {
            EntityService<TrainingISModel> entityService = new EntityService<TrainingISModel>();
            List<Type> Entities = entityService.getAllEntities();

            foreach (var typeofEntity in Entities)
            {
                CodeStringCsharp codeStringCsharp = new CodeStringCsharp();
                List<string> NamesSapces = new List<string>();
                List<string> CodeProperties = new List<string>();
                codeStringCsharp.GenerateCodeProperties(typeofEntity.GetProperties().ToList(), NamesSapces, CodeProperties);
 
                
            }
        }
    }
}