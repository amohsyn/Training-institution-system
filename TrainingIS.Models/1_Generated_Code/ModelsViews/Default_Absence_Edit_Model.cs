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
using TrainingIS.Entities.Resources.AbsenceResources; 
using TrainingIS.Entities.Resources.SeanceTrainingResources; 
using TrainingIS.Entities.Resources.TraineeResources; 
using GApp.Entities.Resources.BaseEntity; 
 

namespace TrainingIS.Entities.ModelsViews
{
	[EditView(typeof(Absence))]
    public class Default_Absence_Edit_Model : Default_Form_Absence_Model
    {

    }
}    
