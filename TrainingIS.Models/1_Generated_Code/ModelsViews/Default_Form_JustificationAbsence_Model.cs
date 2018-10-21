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
using TrainingIS.Entities.Resources.Category_JustificationAbsenceResources;  
using GApp.Entities.Resources.AppResources;  
 
namespace TrainingIS.Entities.ModelsViews
{
	[EditView(typeof(JustificationAbsence))]
	[CreateView(typeof(JustificationAbsence))]
    public class Default_Form_JustificationAbsence_Model : BaseModel
    {
		[Required]
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Trainee))]
		[GAppDataTable(PropertyPath = "TraineeId", FilterBy = "TraineeId", SearchBy = "TraineeId", OrderBy = "TraineeId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 TraineeId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Category_JustificationAbsence))]
		[GAppDataTable(PropertyPath = "Category_JustificationAbsenceId", FilterBy = "Category_JustificationAbsenceId", SearchBy = "Category_JustificationAbsenceId", OrderBy = "Category_JustificationAbsenceId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 Category_JustificationAbsenceId  {set; get;}  
   
		[Required]
		[Display(Name = "StartDate", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "StartDate", FilterBy = "StartDate", SearchBy = "StartDate", OrderBy = "StartDate",  AutoGenerateFilter = false,isColumn = true )]
		[DataType(DataType.DateTime)]
		public DateTime StartDate  {set; get;}  
   
		[Required]
		[Display(Name = "EndtDate", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "EndtDate", FilterBy = "EndtDate", SearchBy = "EndtDate", OrderBy = "EndtDate",  AutoGenerateFilter = false,isColumn = true )]
		[DataType(DataType.DateTime)]
		public DateTime EndtDate  {set; get;}  
   
		[Display(Name = "Description", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Description", FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  AutoGenerateFilter = false,isColumn = true )]
		public String Description  {set; get;}  
   
    }
}    
