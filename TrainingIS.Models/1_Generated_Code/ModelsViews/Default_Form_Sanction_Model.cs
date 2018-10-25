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
using TrainingIS.Entities.Resources.TraineeResources;  
using TrainingIS.Entities.Resources.SanctionCategoryResources;  
using TrainingIS.Entities.Resources.MeetingResources;  
 
namespace TrainingIS.Entities.ModelsViews
{
	[EditView(typeof(Sanction))]
	[CreateView(typeof(Sanction))]
    public class Default_Form_Sanction_Model : BaseModel
    {
		[Required]
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Trainee))]
		[GAppDataTable(PropertyPath = "TraineeId", FilterBy = "TraineeId", SearchBy = "TraineeId", OrderBy = "TraineeId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 TraineeId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_SanctionCategory))]
		[GAppDataTable(PropertyPath = "SanctionCategoryId", FilterBy = "SanctionCategoryId", SearchBy = "SanctionCategoryId", OrderBy = "SanctionCategoryId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 SanctionCategoryId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "MeetingId", FilterBy = "MeetingId", SearchBy = "MeetingId", OrderBy = "MeetingId",  AutoGenerateFilter = true,isColumn = true )]
		public Int64 MeetingId  {set; get;}  
   
    }
}    
