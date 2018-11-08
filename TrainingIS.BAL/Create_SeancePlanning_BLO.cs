using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.Core.Context;
using GApp.DAL;
using TrainingIS.DAL;
using TrainingIS.Entities;

namespace TrainingIS.BLL
{
    public class Create_SeancePlanning_BLO : Base_NotDb_BLO
    {
        TrainingYear First_TrainingYear { set; get; }
        List<Classroom> Classrooms { set; get; }
        List<SeanceDay> SeanceDays { set; get; }
        List<SeanceNumber> SeanceNumbers { set; get; }
        List<Schedule> Schedules_First_TrainingYear;

        List<string> SeancePlannings_references { set; get; }

        public Create_SeancePlanning_BLO(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {

            SeancePlannings_references = new List<string>();

            First_TrainingYear = this.UnitOfWork.context.TrainingYears.OrderBy(t => t.Ordre).First();
            Schedules_First_TrainingYear = this.UnitOfWork.context
                .Schedules
                .Where(s => s.TrainingYear.Id == First_TrainingYear.Id)
                .ToList();

            Classrooms = this.UnitOfWork.context.Classrooms.ToList();
            SeanceDays = this.UnitOfWork.context.SeanceDays.ToList();
            SeanceNumbers = this.UnitOfWork.context.SeanceNumbers.ToList();

        }

        /// <summary>
        /// Create SeancePlanning from training according to the following constraints
        /// - One group in one [SeanceDay, ClassRoom,SeanceNumber, Date]
        /// </summary>
        /// <param name="training"></param>
        public SeancePlanning Create_SeancePlanning_From_Training(Training training)
        {
            foreach (var Schedule in Schedules_First_TrainingYear)
            {
                foreach (var SeanceDay in SeanceDays)
                {
                    foreach (var Classroom in Classrooms)
                    {
                        string Training_By_SeanceDay_Class_Reference = string.Format("{0}-{1}-{2}-{3}",
                                Classroom.Reference,
                                SeanceDay.Reference,
                                Schedule.Reference,
                                training.Reference
                               );

                        // cehck if Training exist in current SeanceDay and Classroom
                        if (this.SeancePlannings_references
                            .Where(e => e.Contains(Training_By_SeanceDay_Class_Reference)).FirstOrDefault() != null)
                        {
                            continue;
                        }

                        foreach (var SeanceNumber in SeanceNumbers)
                        {
                            string Class_Room_Busy_reference = string.Format("{0}-{1}-{2}-{3}",
                                SeanceNumber.Reference,
                                Classroom.Reference,
                                SeanceDay.Reference,
                                Schedule.Reference
                               );


                            string SeancePlannings_reference = string.Format("{0}-{1}",
                                 SeanceNumber.Reference,
                                Training_By_SeanceDay_Class_Reference
                                );

                            // cehck the classe room if he is busy
                            if (this.SeancePlannings_references
                                .Where(e => e.Contains(Class_Room_Busy_reference)).FirstOrDefault() != null)
                            {
                                continue;
                            }

                            this.SeancePlannings_references.Add(SeancePlannings_reference);

                            SeancePlanning seancePlanning = new SeancePlanning();
                            seancePlanning.Schedule = Schedule;
                            seancePlanning.Training = training;
                            seancePlanning.SeanceDay = SeanceDay;
                            seancePlanning.SeanceNumber = SeanceNumber;
                            seancePlanning.Classroom = Classroom;
                            seancePlanning.Reference = seancePlanning.CalculateReference();
                            return seancePlanning;
                        }

                    }
                }
            }
            return null;
        }

    }
}
