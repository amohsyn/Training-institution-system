using GApp.Core.MetaDatas.ReadConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;
using TrainingIS.Entities;
using System.Data.Entity;
using GApp.WebApp.Manager.Generator;

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

           // EntityService entityService = new EntityService();

            Type type = typeof(Group);
            //var value = Tags.EditorFor(type.GetProperty("TrainingTypeId"));

            //foreach (var item in type.GetProperties())
            //{
            //    Console.WriteLine(Tags.EditorFor(type.FullName, item.Name));
            //}

            EntityGeneratorWork<TrainingISModel> entityGeneratorWork = new EntityGeneratorWork<TrainingISModel>(type);
            var v = entityGeneratorWork.GetCreatedProperties();


            // Console.WriteLine(value);
            Console.Read();

        }
    }
}
