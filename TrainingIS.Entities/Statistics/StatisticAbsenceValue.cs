using GApp.Entities;
using GApp.Entities.Resources.AppResources;
using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using TrainingIS.Entities.Resources.StatisticValueResources;

namespace TrainingIS.Entities
{
    public class StatisticAbsenceValue : StatisticValue
    {
        public Int64 SeanceTrainingCount { get; set; }
        public Int64 AbsenceCount{ get; set; }
        public float Percentage { get; set; }

        // Trainee
        public string TraineeCNE { set; get; }
        public string TraineeFirstName { set; get; }
        public string TraineeLastName { set; get; }

        // Group
        public string GroupCode { set; get; }

        //Former
        public string FormerFirstName { set; get; }
        public string FormerLastName { set; get; }

        //ModuleTraining
        public string ModuleTrainingCode { set; get; }
        public string ModuleTrainingName { set; get; }

        //SeanceNumber
        public string SeanceNumberCode { set; get; }

        //SeanceDay
        public string SeanceDayCode { set; get; }


    }
}
