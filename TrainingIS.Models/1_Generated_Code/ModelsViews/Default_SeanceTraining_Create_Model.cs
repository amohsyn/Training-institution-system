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
using TrainingIS.Entities.Resources.SeanceTrainingResources; 
using TrainingIS.Entities.Resources.SeancePlanningResources; 
using GApp.Entities.Resources.BaseEntity; 
 

namespace TrainingIS.Entities.ModelsViews
{
	[CreateView(typeof(SeanceTraining))]
    public class Default_SeanceTraining_Create_Model : Default_Form_SeanceTraining_Model
    {

    }
}    
