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
using GApp.Entities.Resources.AppResources; 
using TrainingIS.Entities.Resources.StateOfAbseceResources; 
using TrainingIS.Entities.Resources.TraineeResources; 
using GApp.Entities.Resources.BaseEntity; 
 

namespace TrainingIS.Entities.ModelsViews
{
	[EditView(typeof(StateOfAbsece))]
    public class Default_StateOfAbsece_Edit_Model : Default_Form_StateOfAbsece_Model
    {

    }
}    
