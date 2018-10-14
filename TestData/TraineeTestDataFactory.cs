using GApp.Core.Context;
using GApp.DAL;
using GApp.UnitTest.TestData;
using GApp.UnitTest.TestData.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;
using TrainingIS.Entities;

namespace TestData
{
    public partial class TraineeTestDataFactory  
    {
        private readonly TrainingISModel _context;

        public override List<Trainee> Generate()
        {
            var _Date = new List<Trainee>();

            var Groups = _context.Set<Group>().ToList();

            int Nbr_Trainne_Per_Group = 28;
            foreach (var group in Groups)
            {
                for (int i = 0; i < Nbr_Trainne_Per_Group; i++)
                {
                    string _Entity_Reference = new Guid().ToString();
                    this.Data.Add(new Trainee
                    {
                        FirstName = string.Format("Madani_{0}", i + 1),
                        LastName = string.Format("Ali_{0}", i + 1),
                        CIN = _Entity_Reference,
                        CNE = _Entity_Reference,
                        Group = group
                    });
                }
            }
            return _Date;
        }

        
    }
}
