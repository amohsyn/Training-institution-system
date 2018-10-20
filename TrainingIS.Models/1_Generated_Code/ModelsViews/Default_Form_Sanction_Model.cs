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
using TrainingIS.Entities.Resources.SanctionCategoryResources;  
 
namespace TrainingIS.Entities.ModelsViews
{
	[EditView(typeof(Sanction))]
	[CreateView(typeof(Sanction))]
    public class Default_Form_Sanction_Model : BaseModel
    {
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_SanctionCategory))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "SanctionCategoryId", SearchBy = "SanctionCategoryId", OrderBy = "SanctionCategoryId",  PropertyPath = "SanctionCategoryId")]
		public Int64 SanctionCategoryId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_SanctionCategory))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "MeetingId", SearchBy = "MeetingId", OrderBy = "MeetingId",  PropertyPath = "MeetingId")]
		public Int64 MeetingId  {set; get;}  
   
    }
}    
