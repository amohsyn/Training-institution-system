using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Default_Data
{
    class Program
    {
        static void Main(string[] args)
        {
            new Classrooms().InsertDefaultData();
            new Specialtys().InsertDefaultData();
            new TrainingTypes().InsertDefaultData();
            new TrainingYears().InsertDefaultData();
            new Groups().InsertDefaultData();
            new Trainees().InsertDefaultData();
        }
    }
}
