using System;
using System.ComponentModel.DataAnnotations;
using TrainingIS.Entities.Resources.TrainingYearResources;
using TrainingIS.Entities.Resources.ModuleTrainingResources;
using TrainingIS.Entities.Resources.FormerResources;
using TrainingIS.Entities.Resources.GroupResources;
using TrainingIS.Entities.Resources.SpecialtyResources;
using System.Collections.Generic;
using GApp.Core.Entities.ModelsViews;
using GApp.Entities;
using GApp.Models;
using GApp.Models.DataAnnotations;
using GApp.Entities.Resources.AppResources;

namespace TrainingIS.Entities.ModelsViews.Trainings
{
    [CreateView(typeof(Training))]
    public class Create_Training_Model : Form_Training_Model
    {
      
    }
}
