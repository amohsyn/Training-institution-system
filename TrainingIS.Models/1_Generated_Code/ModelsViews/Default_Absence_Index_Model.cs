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
 

namespace TrainingIS.Entities.ModelsViews
{
	[IndexView(typeof(Absence))]
	[SearchBy("Reference")]
    public class Default_Absence_Index_Model : BaseModel
    {
		[Filter]
		[Required]
		[Display(Name = "AbsenceDate", GroupName = "SeanceTraining", Order = 1, ResourceType = typeof(msg_Absence))]
		[GAppDataTable(PropertyPath = "AbsenceDate", FilterBy = "AbsenceDate", SearchBy = "AbsenceDate", OrderBy = "AbsenceDate",  AutoGenerateFilter = true,isColumn = true , isOrderBy = true, isSeachBy =true)]
		[DataType(DataType.Date)]
		public DateTime AbsenceDate  {set; get;}  
   
		[Display(Name = "SingularName", GroupName = "SeanceTraining", Order = 2, ResourceType = typeof(msg_SeanceTraining))]
		[GAppDataTable(PropertyPath = "SeanceTraining", FilterBy = "SeanceTraining.Id", SearchBy = "SeanceTraining.Reference", OrderBy = "SeanceTraining.Reference",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public SeanceTraining SeanceTraining  {set; get;}  
   
		[SearchBy("Trainee.FirstName")]
		[SearchBy("Trainee.LastName")]
		[Display(Name = "SingularName", GroupName = "Trainee", Order = 1, ResourceType = typeof(msg_Trainee))]
		[GAppDataTable(PropertyPath = "Trainee", FilterBy = "Trainee.Id", SearchBy = "Trainee.Reference", OrderBy = "Trainee.Reference",  AutoGenerateFilter = true,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public Trainee Trainee  {set; get;}  
   
		[SearchBy("FormerComment")]
		[Display(Name = "FormerComment", GroupName = "Comments", Order = 1, ResourceType = typeof(msg_Absence))]
		[GAppDataTable(PropertyPath = "FormerComment", FilterBy = "FormerComment", SearchBy = "FormerComment", OrderBy = "FormerComment",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public String FormerComment  {set; get;}  
   
		[SearchBy("TraineeComment")]
		[Display(Name = "TraineeComment", GroupName = "Comments", Order = 1, ResourceType = typeof(msg_Absence))]
		[GAppDataTable(PropertyPath = "TraineeComment", FilterBy = "TraineeComment", SearchBy = "TraineeComment", OrderBy = "TraineeComment",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public String TraineeComment  {set; get;}  
   
		[SearchBy("SupervisorComment")]
		[Display(Name = "SupervisorComment", GroupName = "Comments", Order = 1, ResourceType = typeof(msg_Absence))]
		[GAppDataTable(PropertyPath = "SupervisorComment", FilterBy = "SupervisorComment", SearchBy = "SupervisorComment", OrderBy = "SupervisorComment",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public String SupervisorComment  {set; get;}  
   
		[Display(Name = "AbsenceState", GroupName = "States", Order = 3, ResourceType = typeof(msg_Absence))]
		[GAppDataTable(PropertyPath = "AbsenceState", FilterBy = "AbsenceState", SearchBy = "AbsenceState", OrderBy = "AbsenceState",  AutoGenerateFilter = true,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public AbsenceStates AbsenceState  {set; get;}  
   
    }
}    
