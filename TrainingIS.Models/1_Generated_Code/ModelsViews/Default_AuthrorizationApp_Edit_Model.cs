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
using GApp.Entities.Resources.RoleAppResources; 
using GApp.Entities.Resources.ControllerAppResources; 
using GApp.Entities.Resources.AuthrorizationAppResources; 
using GApp.Entities.Resources.ActionControllerAppResources; 
using GApp.Models.DataAnnotations; 
using GApp.Entities.Resources.BaseEntity; 
 

namespace TrainingIS.Entities.ModelsViews
{
	[EditView(typeof(AuthrorizationApp))]
    public class Default_AuthrorizationApp_Edit_Model : Default_Form_AuthrorizationApp_Model
    {

    }
}    
