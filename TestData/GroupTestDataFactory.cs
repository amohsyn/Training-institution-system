using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL;
using TrainingIS.Entities;

namespace TestData
{
    public partial class GroupTestDataFactory
    {
        protected override List<Group> Generate_TestData()
        {
            List<Group> Data = new List<Group>();
            GroupBLO groupBLO = new GroupBLO(this.UnitOfWork, this.GAppContext);

            var TrainingYears = this.UnitOfWork.context.TrainingYears.ToList();
            var Specialties = this.UnitOfWork.context.Specialties.ToList();
            var YearStatdies = this.UnitOfWork.context.YearStudies.ToList();

            foreach (var trainingYear in TrainingYears)
            {
                foreach (var specialty in Specialties)
                {
                    foreach (var yearStatdy in YearStatdies)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            Group group = groupBLO.CreateInstance();
                            group.TrainingYear = trainingYear;
                            group.Specialty = specialty;
                            group.YearStudy = yearStatdy;
                            group.Code = string.Format("{0}{1}0{2}", specialty.Code, yearStatdy.Code, i + 1);
                            group.Reference = group.CalculateReference();
                            Data.Add(group);
                        }
                    }
                   
                }
            }




            return Data;
        }
    }
}
