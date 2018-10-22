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
		[Display(Name = "SeanceDate", Order = 0, ResourceType = typeof(msg_SeanceTraining))]
		[GAppDataTable(PropertyPath = "SeanceDate", FilterBy = "SeanceDate", SearchBy = "SeanceDate", OrderBy = "SeanceDate",  AutoGenerateFilter = true,isColumn = true )]
		[DataType(DataType.Date)]
		public DateTime SeanceDate  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_SeancePlanning))]
		[GAppDataTable(PropertyPath = "SeancePlanningId", FilterBy = "SeancePlanningId", SearchBy = "SeancePlanningId", OrderBy = "SeancePlanningId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 SeancePlanningId  {set; get;}  
   
		[Display(Name = "Contained", Order = 0, ResourceType = typeof(msg_SeanceTraining))]
		[GAppDataTable(PropertyPath = "Contained", FilterBy = "Contained", SearchBy = "Contained", OrderBy = "Contained",  AutoGenerateFilter = false,isColumn = true )]
		public String Contained  {set; get;}  
   
		[Display(Name = "FormerValidation", Order = 0, ResourceType = typeof(msg_SeanceTraining))]
		[GAppDataTable(PropertyPath = "FormerValidation", FilterBy = "FormerValidation", SearchBy = "FormerValidation", OrderBy = "FormerValidation",  AutoGenerateFilter = true,isColumn = true )]
		public Boolean FormerValidation  {set; get;}  
   
		[Many (TypeOfEntity = typeof(Absence))]
		[GAppDataTable(PropertyPath = "Absences", FilterBy = "", SearchBy = "", OrderBy = "Absences.Count",  AutoGenerateFilter = false,isColumn = true )]
		public List<String> Selected_Absences {set; get;}  
   
    }
}    
