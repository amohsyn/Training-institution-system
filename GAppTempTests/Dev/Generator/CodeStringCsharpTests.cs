using Microsoft.VisualStudio.TestTools.UnitTesting;
using GApp.Dev.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.WebApp.Manager.Generator;
using TrainingIS.DAL;

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
                CodeStringCsharp codeStringCsharp = new CodeStringCsharp();

                EntityGeneratorWork<TrainingISModel> entityGen = new EntityGeneratorWork<TrainingISModel>(typeofEntity);
                foreach (var item in typeofEntity.GetProperties())
                {
                    //if(item.Name == "SchoollevelId")
                    //{
                       
                       
                        var code_property = codeStringCsharp.Property(item);
                        Assert.IsTrue(!string.IsNullOrEmpty(code_property));
                    //}
                   


                }
            }
        }
    }
}