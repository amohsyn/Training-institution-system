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
	[ExportView(typeof(SeanceTraining))]
    public class Default_SeanceTraining_Export_Model : BaseModel
    {
		[Required]
		[Display(Name = "SeanceDate", Order = 0, ResourceType = typeof(msg_SeanceTraining))]
		[GAppDataTable(PropertyPath = "SeanceDate", FilterBy = "SeanceDate", SearchBy = "SeanceDate", OrderBy = "SeanceDate",  AutoGenerateFilter = true,isColumn = true )]
		[DataType(DataType.Date)]
		public DateTime SeanceDate  {set; get;}  
   
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_SeancePlanning))]
		[GAppDataTable(PropertyPath = "SeancePlanning", FilterBy = "SeancePlanning.Id", SearchBy = "SeancePlanning.Reference", OrderBy = "SeancePlanning.Reference",  AutoGenerateFilter = true,isColumn = true )]
		public SeancePlanning SeancePlanning  {set; get;}  
   
		[Display(Name = "Contained", Order = 0, ResourceType = typeof(msg_SeanceTraining))]
		[GAppDataTable(PropertyPath = "Contained", FilterBy = "Contained", SearchBy = "Contained", OrderBy = "Contained",  AutoGenerateFilter = false,isColumn = true )]
		public String Contained  {set; get;}  
   
		[Display(Name = "FormerValidation", Order = 0, ResourceType = typeof(msg_SeanceTraining))]
		[GAppDataTable(PropertyPath = "FormerValidation", FilterBy = "FormerValidation", SearchBy = "FormerValidation", OrderBy = "FormerValidation",  AutoGenerateFilter = true,isColumn = true )]
		public Boolean FormerValidation  {set; get;}  
   
		[GAppDataTable(PropertyPath = "Absences", FilterBy = "", SearchBy = "", OrderBy = "Absences.Count",  AutoGenerateFilter = false,isColumn = true )]
		public List<Absence> Absences  {set; get;}  
   
    }
}    
