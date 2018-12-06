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
using System.ComponentModel.DataAnnotations;

using TrainingIS.Entities.Resources.TraineeResources;  
using TrainingIS.Entities.Resources.AttendanceStateResources;  
using GApp.Entities.Resources.BaseEntity;  
 
namespace TrainingIS.Entities.ModelsViews
{
    [FormView(typeof(AttendanceState))]
    public class Default_Form_AttendanceState_Model : BaseModel
    {
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Trainee))]
		[GAppDataTable(PropertyPath = "TraineeId", FilterBy = "TraineeId", SearchBy = "TraineeId", OrderBy = "TraineeId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 TraineeId  {set; get;}  
   
		[Display(Name = "Valid_Note", GroupName = "Note", Order = 1, ResourceType = typeof(msg_AttendanceState))]
		[GAppDataTable(PropertyPath = "Valid_Note", FilterBy = "Valid_Note", SearchBy = "Valid_Note", OrderBy = "Valid_Note",  AutoGenerateFilter = false,isColumn = true )]
		public Single Valid_Note  {set; get;}  
   
		[Display(Name = "Invalid_Note", GroupName = "Note", Order = 2, ResourceType = typeof(msg_AttendanceState))]
		[GAppDataTable(PropertyPath = "Invalid_Note", FilterBy = "Invalid_Note", SearchBy = "Invalid_Note", OrderBy = "Invalid_Note",  AutoGenerateFilter = false,isColumn = true )]
		public Single Invalid_Note  {set; get;}  
   
		[Unique]
		[Display(Name = "Reference", Order = 0, ResourceType = typeof(msg_BaseEntity))]
		[GAppDataTable(PropertyPath = "Reference", FilterBy = "Reference", SearchBy = "Reference", OrderBy = "Reference",  AutoGenerateFilter = false,isColumn = false )]
		public String Reference  {set; get;}  
   
    }
}    
