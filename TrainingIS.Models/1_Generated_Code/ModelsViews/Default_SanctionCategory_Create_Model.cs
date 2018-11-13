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
using TrainingIS.Entities.Resources.DisciplineCategoryResources; 
using GApp.Entities.Resources.AppResources; 
using TrainingIS.Entities.Resources.SanctionCategoryResources; 
using GApp.Entities.Resources.BaseEntity; 
 

namespace TrainingIS.Entities.ModelsViews
{
	[CreateView(typeof(SanctionCategory))]
    public class Default_SanctionCategory_Create_Model : Default_Form_SanctionCategory_Model
    {

    }
}    
