using GApp.Core.MetaDatas.ReadConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using TrainingIS.WebApp.Manager.Scaffold;

namespace GApp.Web.Manager
{
    class Program
    {
        static void Main(string[] args)
        {
            //var v = Tags.EditorFor("TrainingIS.Entities.SeanceNumber", "EndTime");
            //Console.WriteLine(v);
            //Console.Read();

            EntityMetaDataConfiguratrion entityMetaDataConfiguratrion = EntityMetaDataConfiguratrion.CreateConfigEntity("TrainingIS.Entities.Trainee");
           // entityMetaDataConfiguratrion.entityMetataDataAttribute.isGenerateBLO
        }
    }
}
