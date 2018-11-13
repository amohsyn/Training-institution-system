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
using TrainingIS.Entities.enums;
using TrainingIS.Entities.Resources.TrainingYearResources; 
using TrainingIS.Entities.Resources.ScheduleResources; 
using GApp.Entities.Resources.AppResources; 
using GApp.Entities.Resources.BaseEntity; 
 

namespace TrainingIS.Entities.ModelsViews
{
	[CreateView(typeof(Schedule))]
    public class Default_Schedule_Create_Model : Default_Form_Schedule_Model
    {

    }
}    
