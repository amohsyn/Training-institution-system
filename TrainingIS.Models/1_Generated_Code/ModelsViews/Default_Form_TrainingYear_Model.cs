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
using GApp.Entities.Resources.AppResources;  
using TrainingIS.Entities.Resources.TrainingYearResources;  
 
namespace TrainingIS.Entities.ModelsViews
{
	[EditView(typeof(TrainingYear))]
	[CreateView(typeof(TrainingYear))]
    public class Default_Form_TrainingYear_Model : BaseModel
    {
		[Required]
		[Unique]
		[Display(Name = "Code", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Code", FilterBy = "Code", SearchBy = "Code", OrderBy = "Code",  AutoGenerateFilter = false,isColumn = true )]
		public String Code  {set; get;}  
   
		[Required]
		[Display(Name = "StartDate", Order = 0, ResourceType = typeof(msg_TrainingYear))]
		[GAppDataTable(PropertyPath = "StartDate", FilterBy = "StartDate", SearchBy = "StartDate", OrderBy = "StartDate",  AutoGenerateFilter = false,isColumn = true )]
		public DateTime StartDate  {set; get;}  
   
		[Required]
		[Display(Name = "EndtDate", Order = 0, ResourceType = typeof(msg_TrainingYear))]
		[GAppDataTable(PropertyPath = "EndtDate", FilterBy = "EndtDate", SearchBy = "EndtDate", OrderBy = "EndtDate",  AutoGenerateFilter = false,isColumn = true )]
		public DateTime EndtDate  {set; get;}  
   
    }
}    
