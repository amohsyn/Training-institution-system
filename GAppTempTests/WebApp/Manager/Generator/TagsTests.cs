using Microsoft.VisualStudio.TestTools.UnitTesting;
using GApp.WebApp.Manager.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using TrainingIS.DAL;

namespace GApp.WebApp.Manager.Generator.Tests
{
    [TestClass()]
    public class TagsTests
    {
        [TestMethod()]
        public void EditorFor_For_All_Properties()
        {
           EntityService<TrainingISModel> entityService = new EntityService<TrainingISModel>();
           var Entities = entityService.getAllEntities();
            foreach (var entityType in Entities)
            {
                EntityGeneratorWork<TrainingISModel> entityGeneratorWork = new EntityGeneratorWork<TrainingISModel>(entityType);
                Tags<TrainingISModel> Tags = new Tags<TrainingISModel>(entityGeneratorWork);

                foreach (var item in entityGeneratorWork.GetCreatedProperties())
                {
                   string EditorFormTag = Tags.EditorFor(item);
                    Assert.IsTrue(!string.IsNullOrEmpty(EditorFormTag));
                }
                foreach (var item in entityGeneratorWork.GetEditProperties())
                {
                    string EditorFormTag = Tags.EditorFor(item);
                    Assert.IsTrue(!string.IsNullOrEmpty(EditorFormTag));
                }
                foreach (var item in entityGeneratorWork.GetIndexProperties())
                {
                    string EditorFormTag = Tags.EditorFor(item);
                    Assert.IsTrue(!string.IsNullOrEmpty(EditorFormTag));
                }
                foreach (var item in entityGeneratorWork.GetDetailsProperties())
                {
                    string EditorFormTag = Tags.EditorFor(item);
                    Assert.IsTrue(!string.IsNullOrEmpty(EditorFormTag));
                }
            }
        }
    }
}