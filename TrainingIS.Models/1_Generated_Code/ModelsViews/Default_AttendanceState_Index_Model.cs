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
using TrainingIS.Entities.Resources.AttendanceStateResources; 
 

namespace TrainingIS.Entities.ModelsViews
{
	[IndexView(typeof(AttendanceState))]
	[SearchBy("Reference")]
    public class Default_AttendanceState_Index_Model : BaseModel
    {
		[SearchBy("Trainee.FirstName")]
		[SearchBy("Trainee.LastName")]
		[Required]
		[Display(Name = "SingularName", GroupName = "Trainee", Order = 0, ResourceType = typeof(msg_Trainee))]
		[GAppDataTable(PropertyPath = "Trainee", FilterBy = "Trainee.Id", SearchBy = "Trainee.Reference", OrderBy = "Trainee.Reference",  AutoGenerateFilter = true,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public Trainee Trainee  {set; get;}  
   
		[Display(Name = "Valid_Note", GroupName = "Note", Order = 1, ResourceType = typeof(msg_AttendanceState))]
		[GAppDataTable(PropertyPath = "Valid_Note", FilterBy = "Valid_Note", SearchBy = "Valid_Note", OrderBy = "Valid_Note",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public Single Valid_Note  {set; get;}  
   
		[Display(Name = "Invalid_Note", GroupName = "Note", Order = 2, ResourceType = typeof(msg_AttendanceState))]
		[GAppDataTable(PropertyPath = "Invalid_Note", FilterBy = "Invalid_Note", SearchBy = "Invalid_Note", OrderBy = "Invalid_Note",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public Single Invalid_Note  {set; get;}  
   
		[Display(Name = "Valid_Sanction", GroupName = "Note", Order = 1, ResourceType = typeof(msg_AttendanceState))]
		[GAppDataTable(PropertyPath = "Valid_Sanction", FilterBy = "Valid_Sanction.Id", SearchBy = "Valid_Sanction.Reference", OrderBy = "Valid_Sanction.Reference",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public Sanction Valid_Sanction  {set; get;}  
   
		[Display(Name = "Invalid_Sanction", GroupName = "Note", Order = 2, ResourceType = typeof(msg_AttendanceState))]
		[GAppDataTable(PropertyPath = "Invalid_Sanction", FilterBy = "Invalid_Sanction.Id", SearchBy = "Invalid_Sanction.Reference", OrderBy = "Invalid_Sanction.Reference",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public Sanction Invalid_Sanction  {set; get;}  
   
    }
}    
