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
using GApp.Entities.Resources.GAppResources;  
 
namespace TrainingIS.Entities.ModelsViews
{
	[EditView(typeof(RoleApp))]
	[CreateView(typeof(RoleApp))]
    public class Default_Form_RoleApp_Model : BaseModel
    {
		[Required]
		[Display(Name = "Code", ResourceType = typeof(msg_GApp))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Code")]
		public String Code  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_GApp))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Description")]
		public String Description  {set; get;}  
   
    }
}    
