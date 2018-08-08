using Microsoft.VisualStudio.TestTools.UnitTesting;
using GApp.WebApp.Manager.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using TrainingIS.DAL;
using TrainingIS.Entities.ModelsViews.Generated;

namespace GApp.WebApp.Manager.Generator.Tests
{
    [TestClass()]
    public class TagsTests
    {
        [TestMethod()]
        public void DisplayFor_All_Group_Index_PropertiesTest()
        {
            //Type entityType = typeof(Group);

            //Tags<TrainingISModel> Tags = new Tags<TrainingISModel>(entityType);
            //foreach (var item in entityGeneratorWork.GetIndexProperties())
            //{
            //    string DisplayFor = Tags.DisplayFor("item", item);
            //    Assert.IsTrue(!string.IsNullOrEmpty(DisplayFor));
            //}
        }

        [TestMethod()]
        public void EditorFor_For_All_Properties()
        {
            EntityService<TrainingISModel> entityService = new EntityService<TrainingISModel>();
            var Entities = entityService.getAllEntities();
           
            foreach (var entityType in Entities)
            {
                ModelView_CodeGenerator<TrainingISModel> ModelView_CodeGenerator =
               new ModelView_CodeGenerator<TrainingISModel>(entityType, new DefaultModelView_MetaData().ModelsViewsTypes);

                if (entityType.Name == "AuthrorizationApp")
                {


                   

                    foreach (var item in ModelView_CodeGenerator.GetCreatedProperties())
                    {
                        Tags<TrainingISModel> Tags = new Tags<TrainingISModel>(entityType, ModelView_CodeGenerator.getCreateModelView_Type());
                        string EditorFormTag = Tags.EditorFor(item);
                        Assert.IsTrue(!string.IsNullOrEmpty(EditorFormTag));
                    }
                    //foreach (var item in ModelView_CodeGenerator.GetEditProperties())
                    //{
                    //    string EditorFormTag = Tags.EditorFor(item);
                    //    Assert.IsTrue(!string.IsNullOrEmpty(EditorFormTag));
                    //}
                    //foreach (var item in ModelView_CodeGenerator.GetIndexProperties())
                    //{
                    //    string EditorFormTag = Tags.EditorFor(item);
                    //    Assert.IsTrue(!string.IsNullOrEmpty(EditorFormTag));
                    //}
                    //foreach (var item in ModelView_CodeGenerator.GetDetailsProperties())
                    //{
                    //    string EditorFormTag = Tags.EditorFor(item);
                    //    Assert.IsTrue(!string.IsNullOrEmpty(EditorFormTag));
                    //}
                }
            }
        }
    }
}