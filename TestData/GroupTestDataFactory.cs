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
            var TrainingTypes = this.UnitOfWork.context.TrainingTypes.ToList();

            int group_order = 1;
            foreach (var trainingYear in TrainingYears)
            {
                foreach (var specialty in Specialties)
                {
                    foreach (var yearStatdy in YearStatdies)
                    {
                        foreach (var TrainingType in TrainingTypes)
                        {
                            for (int i = 0; i < 2; i++)
                            {
                                Group group = groupBLO.CreateInstance();
                                group.TrainingYear = trainingYear;
                                group.Specialty = specialty;
                                group.YearStudy = yearStatdy;
                                group.Code = string.Format("{0}-{1}-{2}{3}", specialty.Code, TrainingType.Code, yearStatdy, i + 1);
                                group.Reference = group.CalculateReference();
                                group.TrainingType = TrainingType;
                                group.Ordre = group_order++;
                                Data.Add(group);
                            }
                        }
                        
                    }
                   
                }
            }




            return Data;
        }
    }
}
