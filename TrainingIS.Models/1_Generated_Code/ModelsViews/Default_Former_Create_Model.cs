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
using TrainingIS.Entities.Resources.FormerSpecialtyResources; 
using TrainingIS.Entities.Resources.FormerResources; 
using TrainingIS.Entities.Resources.NationalityResources; 
using GApp.Entities.Resources.BaseEntity; 
 

namespace TrainingIS.Entities.ModelsViews
{
	[CreateView(typeof(Former))]
    public class Default_Former_Create_Model : Default_Form_Former_Model
    {

    }
}    
