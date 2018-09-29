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
using TrainingIS.Entities.Resources.SeanceTrainingResources;  
using TrainingIS.Entities.Resources.SeancePlanningResources;  
 
namespace TrainingIS.Entities.ModelsViews
{
	[EditView(typeof(SeanceTraining))]
	[CreateView(typeof(SeanceTraining))]
    public class Default_Form_SeanceTraining_Model : BaseModel
    {
		[Required]
		[Display(Name = "SeanceDate", ResourceType = typeof(msg_SeanceTraining))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "SeanceDate", SearchBy = "SeanceDate", OrderBy = "SeanceDate",  PropertyPath = "SeanceDate")]
		[DataType(DataType.Date)]
		public DateTime SeanceDate  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_SeancePlanning))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "SeancePlanningId", SearchBy = "SeancePlanningId", OrderBy = "SeancePlanningId",  PropertyPath = "SeancePlanningId")]
		public Int64 SeancePlanningId  {set; get;}  
   
		[Display(Name = "Contained", ResourceType = typeof(msg_SeanceTraining))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Contained", SearchBy = "Contained", OrderBy = "Contained",  PropertyPath = "Contained")]
		public String Contained  {set; get;}  
   
		[Display(Name = "FormerValidation", ResourceType = typeof(msg_SeanceTraining))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "FormerValidation", SearchBy = "FormerValidation", OrderBy = "FormerValidation",  PropertyPath = "FormerValidation")]
		public Boolean FormerValidation  {set; get;}  
   
		[Many (TypeOfEntity = typeof(Absence))]
		public List<String> Selected_Absences {set; get;}  
   
    }
}    
