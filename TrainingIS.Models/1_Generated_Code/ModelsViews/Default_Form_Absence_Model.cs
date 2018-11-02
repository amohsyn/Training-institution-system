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
	[CreateView(typeof(Absence))]
    public class Default_Form_Absence_Model : BaseModel
    {
		[Filter]
		[Required]
		[Display(Name = "AbsenceDate", GroupName = "SeanceTraining", Order = 1, ResourceType = typeof(msg_Absence))]
		[GAppDataTable(PropertyPath = "AbsenceDate", FilterBy = "AbsenceDate", SearchBy = "AbsenceDate", OrderBy = "AbsenceDate",  AutoGenerateFilter = true,isColumn = true )]
		[DataType(DataType.Date)]
		public DateTime AbsenceDate  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", GroupName = "SeanceTraining", Order = 2, ResourceType = typeof(msg_SeanceTraining))]
		[GAppDataTable(PropertyPath = "SeanceTrainingId", FilterBy = "SeanceTrainingId", SearchBy = "SeanceTrainingId", OrderBy = "SeanceTrainingId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 SeanceTrainingId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", GroupName = "Trainee", Order = 2, ResourceType = typeof(msg_Trainee))]
		[GAppDataTable(PropertyPath = "TraineeId", FilterBy = "TraineeId", SearchBy = "TraineeId", OrderBy = "TraineeId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 TraineeId  {set; get;}  
   
		[SearchBy("FormerComment")]
		[Display(Name = "FormerComment", GroupName = "Comments", Order = 1, ResourceType = typeof(msg_Absence))]
		[GAppDataTable(PropertyPath = "FormerComment", FilterBy = "FormerComment", SearchBy = "FormerComment", OrderBy = "FormerComment",  AutoGenerateFilter = false,isColumn = true )]
		public String FormerComment  {set; get;}  
   
		[SearchBy("TraineeComment")]
		[Display(Name = "TraineeComment", GroupName = "Comments", Order = 1, ResourceType = typeof(msg_Absence))]
		[GAppDataTable(PropertyPath = "TraineeComment", FilterBy = "TraineeComment", SearchBy = "TraineeComment", OrderBy = "TraineeComment",  AutoGenerateFilter = false,isColumn = true )]
		public String TraineeComment  {set; get;}  
   
		[SearchBy("SupervisorComment")]
		[Display(Name = "SupervisorComment", GroupName = "Comments", Order = 1, ResourceType = typeof(msg_Absence))]
		[GAppDataTable(PropertyPath = "SupervisorComment", FilterBy = "SupervisorComment", SearchBy = "SupervisorComment", OrderBy = "SupervisorComment",  AutoGenerateFilter = false,isColumn = true )]
		public String SupervisorComment  {set; get;}  
   
		[Required]
		[Display(Name = "isHaveAuthorization", GroupName = "States", Order = 1, ResourceType = typeof(msg_Absence))]
		[GAppDataTable(PropertyPath = "isHaveAuthorization", FilterBy = "isHaveAuthorization", SearchBy = "isHaveAuthorization", OrderBy = "isHaveAuthorization",  AutoGenerateFilter = true,isColumn = true )]
		public Boolean isHaveAuthorization  {set; get;}  
   
		[Display(Name = "Valide", GroupName = "States", Order = 2, ResourceType = typeof(msg_Absence))]
		[GAppDataTable(PropertyPath = "Valide", FilterBy = "Valide", SearchBy = "Valide", OrderBy = "Valide",  AutoGenerateFilter = true,isColumn = true )]
		public Boolean Valide  {set; get;}  
   
		[Display(Name = "AbsenceState", GroupName = "States", Order = 3, ResourceType = typeof(msg_Absence))]
		[GAppDataTable(PropertyPath = "AbsenceState", FilterBy = "AbsenceState", SearchBy = "AbsenceState", OrderBy = "AbsenceState",  AutoGenerateFilter = true,isColumn = true )]
		public AbsenceStates AbsenceState  {set; get;}  
   
		[Unique]
		[Display(Name = "Reference", Order = 0, ResourceType = typeof(msg_BaseEntity))]
		[GAppDataTable(PropertyPath = "Reference", FilterBy = "Reference", SearchBy = "Reference", OrderBy = "Reference",  AutoGenerateFilter = false,isColumn = false )]
		public String Reference  {set; get;}  
   
    }
}    
