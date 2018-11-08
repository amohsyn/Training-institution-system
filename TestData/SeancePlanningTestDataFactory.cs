using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL;
using TrainingIS.Entities;

namespace TestData
{
    public partial class SeancePlanningTestDataFactory
    {
        
        protected override List<SeancePlanning> Generate_TestData()
        {
            if (this.Data != null) return this.Data;
            this.Data = new List<SeancePlanning>();

            // BLO

            SeancePlanningBLO seancePlanningBLO = new SeancePlanningBLO(this.UnitOfWork, this.GAppContext);
            Create_SeancePlanning_BLO create_SeancePlanning_BLO = new Create_SeancePlanning_BLO(this.UnitOfWork, this.GAppContext);
            // Seance Goals
            int Number_of_Groups_Goal = 25;
            int Number_of_Seance_by_Group_Goal = 25;
            int Number_of_Seance_by_Former_Goal = 50;
            int Number_of_Former_Goal = 2;

          

            //
            // Data Members
            //
            var First_TrainingYear = this.UnitOfWork.context.TrainingYears.OrderBy(t=>t.Ordre).First();
            var Schedules_First_TrainingYear = this.UnitOfWork.context
                .Schedules
                .Where(s => s.TrainingYear.Id == First_TrainingYear.Id)
                .ToList();
           
            var Classrooms = this.UnitOfWork.context.Classrooms.ToList(); 
            var SeanceDays = this.UnitOfWork.context.SeanceDays.ToList(); 
            var SeanceNumbers = this.UnitOfWork.context.SeanceNumbers.ToList();
 
            var Trainings_of_Groups_Goals = this.UnitOfWork.context.Trainings
                .GroupBy(t => t.Group)
                .Select(g => new { Group = g.Key, Trainings = g.ToList() })
                .OrderBy(o => o.Group.Ordre)
                .Take(Number_of_Groups_Goal)
                .ToList();

            int Order = 1;

            foreach (var trainings_of_Group in Trainings_of_Groups_Goals)
            {
                var Trainings = trainings_of_Group.Trainings;

                // Next Training 
                int Current_Training_Index = 0;
                var Current_Training = Trainings[Current_Training_Index];
                int Hourly_Mass_To_Teach_Minute = 0;

                for (int i = 0; i < Number_of_Seance_by_Group_Goal; i++)
                {

                    SeancePlanning seancePlanning = create_SeancePlanning_BLO.
                        Create_SeancePlanning_From_Training(Current_Training);
                    seancePlanning.Ordre = Order++;


                    Data.Add(seancePlanning);

                    // Next Training 
                    Hourly_Mass_To_Teach_Minute += seancePlanning.SeanceNumber.Duration();
                    if (Hourly_Mass_To_Teach_Minute >= (Current_Training.ModuleTraining.Hourly_Mass_To_Teach * 60))
                    {
                        Current_Training_Index++;
                        Current_Training = Trainings[Current_Training_Index];
                    }

                }

               
            }

            //// Generate SeancePlannings
            //int Number_of_Groups = 0;
            //int Number_of_Seance_by_Group = 0;
            //int Number_of_Seance_by_Former = 0;
            //int Number_of_Former = 0;

            //int Training_Number = 0;


            //// Check Goals
            //if (Number_of_Groups >= Number_of_Groups_Goal
            //    && Number_of_Seance_by_Group >= Number_of_Seance_by_Group_Goal
            //    && Number_of_Seance_by_Former >= Number_of_Seance_by_Former_Goal
            //    && Number_of_Former >= Number_of_Former_Goal)
            //    return Data;




            return Data;
        }

       
    }
}
