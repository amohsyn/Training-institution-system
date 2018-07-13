using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;

namespace GApp.Web.Manager
{
    class Program
    {
        static void Main(string[] args)
        {
           
            
            var v =   Scaffold.ScaffoldFunctions.EditorFor("TrainingIS.Entities.SeanceNumber", "EndTime");
            Console.WriteLine(v);
            Console.Read();
        }
    }
}
