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
using TrainingIS.Entities.Resources.ScheduleResources; 
using TrainingIS.Entities.Resources.TrainingResources; 
using TrainingIS.Entities.Resources.SeanceDayResources; 
using TrainingIS.Entities.Resources.SeanceNumberResources; 
using TrainingIS.Entities.Resources.ClassroomResources; 
using GApp.Entities.Resources.AppResources; 
using GApp.Entities.Resources.BaseEntity; 
 

namespace TrainingIS.Entities.ModelsViews
{
	[CreateView(typeof(SeancePlanning))]
    public class Default_SeancePlanning_Create_Model : Default_Form_SeancePlanning_Model
    {

    }
}    
