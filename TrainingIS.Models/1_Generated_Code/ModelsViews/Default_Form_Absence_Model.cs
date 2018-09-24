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
		[Display(Name = "AbsenceDate", ResourceType = typeof(msg_Absence))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "AbsenceDate")]
		[DataType(DataType.Date)]
		public DateTime AbsenceDate  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_SeanceTraining))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "SeanceTrainingId")]
		public Int64 SeanceTrainingId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_Trainee))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "TraineeId")]
		public Int64 TraineeId  {set; get;}  
   
		[Filter]
		[Required]
		[Display(Name = "isHaveAuthorization", ResourceType = typeof(msg_Absence))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "isHaveAuthorization")]
		public Boolean isHaveAuthorization  {set; get;}  
   
		[Display(Name = "FormerComment", ResourceType = typeof(msg_Absence))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "FormerComment")]
		public String FormerComment  {set; get;}  
   
		[Display(Name = "TraineeComment", ResourceType = typeof(msg_Absence))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "TraineeComment")]
		public String TraineeComment  {set; get;}  
   
		[Display(Name = "SupervisorComment", ResourceType = typeof(msg_Absence))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "SupervisorComment")]
		public String SupervisorComment  {set; get;}  
   
		[Display(Name = "Valide", ResourceType = typeof(msg_Absence))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Valide")]
		public Boolean Valide  {set; get;}  
   
    }
}    
