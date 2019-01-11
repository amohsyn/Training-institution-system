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
using GApp.Entities.Resources.GAppResources; 
 

namespace TrainingIS.Entities.ModelsViews
{
	[IndexView(typeof(RoleApp))]
	[SearchBy("Reference")]
    public class Default_RoleApp_Index_Model : BaseModel
    {
		[Required]
		[Display(Name = "Code", Order = 0, ResourceType = typeof(msg_GApp))]
		[GAppDataTable(PropertyPath = "Code", FilterBy = "Code", SearchBy = "Code", OrderBy = "Code",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public String Code  {set; get;}  
   
		[Display(Name = "Description", Order = 0, ResourceType = typeof(msg_GApp))]
		[GAppDataTable(PropertyPath = "Description", FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public String Description  {set; get;}  
   
    }
}    
