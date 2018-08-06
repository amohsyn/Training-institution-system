using Microsoft.VisualStudio.TestTools.UnitTesting;
using GApp.WebApp.Manager.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;
using TrainingIS.Entities;

namespace GApp.WebApp.Manager.Generator.Tests
{
    [TestClass()]
    public class RelationShip_CodeGeneratorTests
    {
        [TestMethod()]
        public void RelationShip_CodeGeneratorTest()
        {
            EntityService<TrainingISModel> entityService = new EntityService<TrainingISModel>();
            List<Type> Entities = entityService.getAllEntities();

            foreach (var typeofEntity in Entities)
            {

                if(typeofEntity == typeof(Classroom))
                {
                    RelationShip_CodeGenerator<TrainingISModel> relationShip_CodeGenerator = new RelationShip_CodeGenerator<TrainingISModel>(typeofEntity);

                    var ForeignKeyNames = relationShip_CodeGenerator.ForeignKeyNames;
                }
               


            }


        }
    }
}