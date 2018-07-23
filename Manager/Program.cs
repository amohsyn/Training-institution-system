using GApp.Core.MetaDatas.ReadConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;
using TrainingIS.Entities;
using TrainingIS.WebApp.Manager.Scaffold;
using System.Data.Entity;

namespace GApp.Web.Manager
{
    class Program
    {
        static void Main(string[] args)
        {
            //var v = Tags.EditorFor("TrainingIS.Entities.Trainee", "Sex");
            //Console.WriteLine(v);


            //EntityMetaDataConfiguratrion entityMetaDataConfiguratrion = EntityMetaDataConfiguratrion.CreateConfigEntity("TrainingIS.Entities.Trainee");
            //entityMetaDataConfiguratrion.entityMetataDataAttribute.isGenerateBLO

            EntityService entityService = new EntityService();

            Type type = typeof(Trainee);
            var g = entityService.GetForeignKeyNames(type);


            

          
          
            Console.Read();

        }
    }
}
