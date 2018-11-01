using GApp.Entities;
using GApp.Entities.Resources.AppResources;
using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using TrainingIS.Entities.Resources.StatisticAbsenceValueResources;
using TrainingIS.Entities.Resources.StatisticValueResources;

namespace TrainingIS.Entities
{
    public class StatisticAbsenceValue : StatisticValue
    {
        [DisplayFormat(DataFormatString = "{0:F1}")]
        [Display(Name = "Presence", ResourceType = typeof(msg_StatisticAbsenceValue))]
        public Int64 Presence { get; set; }

        [Display(Name = "AbsenceCount", ResourceType = typeof(msg_StatisticAbsenceValue))]
        public Int64 AbsenceCount{ get; set; }

        [Display(Name = "Percentage", ResourceType = typeof(msg_StatisticAbsenceValue))]
        public decimal Percentage { get; set; }

        // Trainee
        [Display(Name = "TraineeCNE", ResourceType = typeof(msg_StatisticAbsenceValue))]
        public string TraineeCNE { set; get; }

        [Display(Name = "TraineeFirstName", ResourceType = typeof(msg_StatisticAbsenceValue))]
        public string TraineeFirstName { set; get; }

        [Display(Name = "TraineeLastName", ResourceType = typeof(msg_StatisticAbsenceValue))]
        public string TraineeLastName { set; get; }

        // Group
        [Display(Name = "GroupCode", ResourceType = typeof(msg_StatisticAbsenceValue))]
        public string GroupCode { set; get; }

        //Former
        [Display(Name = "FormerFirstName", ResourceType = typeof(msg_StatisticAbsenceValue))]
        public string FormerFirstName { set; get; }

        [Display(Name = "FormerLastName", ResourceType = typeof(msg_StatisticAbsenceValue))]
        public string FormerLastName { set; get; }

        //ModuleTraining
        [Display(Name = "ModuleTrainingCode", ResourceType = typeof(msg_StatisticAbsenceValue))]
        public string ModuleTrainingCode { set; get; }

        [Display(Name = "ModuleTrainingName", ResourceType = typeof(msg_StatisticAbsenceValue))]
        public string ModuleTrainingName { set; get; }

        //SeanceNumber
        [Display(Name = "SeanceNumberCode", ResourceType = typeof(msg_StatisticAbsenceValue))]
        public string SeanceNumberCode { set; get; }

        //SeanceDay
        [Display(Name = "SeanceDayCode", ResourceType = typeof(msg_StatisticAbsenceValue))]
        public string SeanceDayCode { set; get; }


    }
}
