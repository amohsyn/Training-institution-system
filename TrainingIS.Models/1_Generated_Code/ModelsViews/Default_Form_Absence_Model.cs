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
	[EditView(typeof(Absence))]
	[CreateView(typeof(Absence))]
    public class Default_Form_Absence_Model : BaseModel
    {
		[Filter]
		[Required]
		[Display(Name = "AbsenceDate", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_Absence))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "AbsenceDate", SearchBy = "AbsenceDate", OrderBy = "AbsenceDate",  PropertyPath = "AbsenceDate")]
		[DataType(DataType.Date)]
		public DateTime AbsenceDate  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_SeanceTraining))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "SeanceTrainingId", SearchBy = "SeanceTrainingId", OrderBy = "SeanceTrainingId",  PropertyPath = "SeanceTrainingId")]
		public Int64 SeanceTrainingId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_Trainee))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "TraineeId", SearchBy = "TraineeId", OrderBy = "TraineeId",  PropertyPath = "TraineeId")]
		public Int64 TraineeId  {set; get;}  
   
		[Required]
		[Display(Name = "isHaveAuthorization", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_Absence))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "isHaveAuthorization", SearchBy = "isHaveAuthorization", OrderBy = "isHaveAuthorization",  PropertyPath = "isHaveAuthorization")]
		public Boolean isHaveAuthorization  {set; get;}  
   
		[SearchBy("FormerComment")]
		[Display(Name = "FormerComment", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_Absence))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "FormerComment", SearchBy = "FormerComment", OrderBy = "FormerComment",  PropertyPath = "FormerComment")]
		public String FormerComment  {set; get;}  
   
		[SearchBy("TraineeComment")]
		[Display(Name = "TraineeComment", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_Absence))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "TraineeComment", SearchBy = "TraineeComment", OrderBy = "TraineeComment",  PropertyPath = "TraineeComment")]
		public String TraineeComment  {set; get;}  
   
		[SearchBy("SupervisorComment")]
		[Display(Name = "SupervisorComment", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_Absence))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "SupervisorComment", SearchBy = "SupervisorComment", OrderBy = "SupervisorComment",  PropertyPath = "SupervisorComment")]
		public String SupervisorComment  {set; get;}  
   
		[Display(Name = "Valide", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_Absence))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "Valide", SearchBy = "Valide", OrderBy = "Valide",  PropertyPath = "Valide")]
		public Boolean Valide  {set; get;}  
   
    }
}    
