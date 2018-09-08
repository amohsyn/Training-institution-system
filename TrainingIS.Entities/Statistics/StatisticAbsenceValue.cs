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
 
        public Int64 SeanceTrainingsCount { get; set; }
        public int GroupsTraineeCount { get; set; }
    }
}
