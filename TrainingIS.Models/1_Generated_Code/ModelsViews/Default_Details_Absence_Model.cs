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
	[DetailsView(typeof(Absence))]
	[IndexView(typeof(Absence))]
    public class Default_Details_Absence_Model : BaseModel
    {
		[Filter]
		[Required]
		[Display(Name = "AbsenceDate", ResourceType = typeof(msg_Absence))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "AbsenceDate", SearchBy = "AbsenceDate", OrderBy = "AbsenceDate",  PropertyPath = "AbsenceDate")]
		[DataType(DataType.Date)]
		public DateTime AbsenceDate  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_SeanceTraining))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "SeanceTraining.Id", SearchBy = "SeanceTraining.Reference", OrderBy = "SeanceTraining.Reference",  PropertyPath = "SeanceTraining")]
		public SeanceTraining SeanceTraining  {set; get;}  
   
		[SearchBy("Trainee.FirstName")]
		[SearchBy("Trainee.LastName")]
		[Display(Name = "SingularName", ResourceType = typeof(msg_Trainee))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "Trainee.Id", SearchBy = "Trainee.Reference", OrderBy = "Trainee.Reference",  PropertyPath = "Trainee")]
		public Trainee Trainee  {set; get;}  
   
		[Required]
		[Display(Name = "isHaveAuthorization", ResourceType = typeof(msg_Absence))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "isHaveAuthorization", SearchBy = "isHaveAuthorization", OrderBy = "isHaveAuthorization",  PropertyPath = "isHaveAuthorization")]
		public Boolean isHaveAuthorization  {set; get;}  
   
		[SearchBy("FormerComment")]
		[Display(Name = "FormerComment", ResourceType = typeof(msg_Absence))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "FormerComment", SearchBy = "FormerComment", OrderBy = "FormerComment",  PropertyPath = "FormerComment")]
		public String FormerComment  {set; get;}  
   
		[SearchBy("TraineeComment")]
		[Display(Name = "TraineeComment", ResourceType = typeof(msg_Absence))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "TraineeComment", SearchBy = "TraineeComment", OrderBy = "TraineeComment",  PropertyPath = "TraineeComment")]
		public String TraineeComment  {set; get;}  
   
		[SearchBy("SupervisorComment")]
		[Display(Name = "SupervisorComment", ResourceType = typeof(msg_Absence))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "SupervisorComment", SearchBy = "SupervisorComment", OrderBy = "SupervisorComment",  PropertyPath = "SupervisorComment")]
		public String SupervisorComment  {set; get;}  
   
		[Display(Name = "Valide", ResourceType = typeof(msg_Absence))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "Valide", SearchBy = "Valide", OrderBy = "Valide",  PropertyPath = "Valide")]
		public Boolean Valide  {set; get;}  
   
    }
}    
