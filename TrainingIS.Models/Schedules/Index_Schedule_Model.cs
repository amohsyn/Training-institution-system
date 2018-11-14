using GApp.Core.Entities.ModelsViews;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.Models.DataAnnotations;
using GApp.Models;
using GApp.Entities;
using TrainingIS.Entities.Resources.TrainingYearResources;
using TrainingIS.Entities.Resources.ScheduleResources;
using GApp.Entities.Resources.AppResources;

namespace TrainingIS.Entities.ModelsViews
{

    [IndexView(typeof(Schedule))]
    public class Index_Schedule_Model : Details_Schedule_Model
    {
        

    }
}
