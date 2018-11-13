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
using TrainingIS.Entities.Resources.TaskProjectResources; 
 

namespace TrainingIS.Entities.ModelsViews
{
	[DetailsView(typeof(Project))]
    public class Default_Project_Details_Model : BaseModel
    {
		[Required]
		[Display(Name = "Name", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Name", FilterBy = "Name", SearchBy = "Name", OrderBy = "Name",  AutoGenerateFilter = false,isColumn = true )]
		public String Name  {set; get;}  
   
		[Display(Name = "Description", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Description", FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  AutoGenerateFilter = false,isColumn = true )]
		public String Description  {set; get;}  
   
		[Display(Name = "StartDate", Order = 0, ResourceType = typeof(msg_TaskProject))]
		[GAppDataTable(PropertyPath = "StartDate", FilterBy = "StartDate", SearchBy = "StartDate", OrderBy = "StartDate",  AutoGenerateFilter = false,isColumn = true )]
		public DateTime StartDate  {set; get;}  
   
		[Display(Name = "EndtDate", Order = 0, ResourceType = typeof(msg_TaskProject))]
		[GAppDataTable(PropertyPath = "EndtDate", FilterBy = "EndtDate", SearchBy = "EndtDate", OrderBy = "EndtDate",  AutoGenerateFilter = false,isColumn = true )]
		public DateTime EndtDate  {set; get;}  
   
		[Display(Name = "isPublic", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "isPublic", FilterBy = "isPublic", SearchBy = "isPublic", OrderBy = "isPublic",  AutoGenerateFilter = false,isColumn = true )]
		public Boolean isPublic  {set; get;}  
   
    }
}    
