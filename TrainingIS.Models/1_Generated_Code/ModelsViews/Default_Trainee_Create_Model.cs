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
using GApp.Entities.Resources.PersonResources; 
using TrainingIS.Entities.Resources.TraineeResources; 
using TrainingIS.Entities.Resources.SchoollevelResources; 
using TrainingIS.Entities.Resources.SpecialtyResources; 
using TrainingIS.Entities.Resources.YearStudyResources; 
using TrainingIS.Entities.Resources.GroupResources; 
using TrainingIS.Entities.Resources.NationalityResources; 
using GApp.Entities.Resources.BaseEntity; 
 

namespace TrainingIS.Entities.ModelsViews
{
	[CreateView(typeof(Trainee))]
    public class Default_Trainee_Create_Model : Default_Form_Trainee_Model
    {

    }
}    
