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
using TrainingIS.Entities.Resources.TrainingYearResources; 
using GApp.Entities.Resources.BaseEntity; 
 

namespace TrainingIS.Entities.ModelsViews
{
	[EditView(typeof(TrainingYear))]
    public class Default_TrainingYear_Edit_Model : Default_Form_TrainingYear_Model
    {

    }
}    
