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
using TrainingIS.Entities.Resources.SectorResources; 
using TrainingIS.Entities.Resources.TrainingLevelResources; 
using GApp.Entities.Resources.AppResources; 
 

namespace TrainingIS.Entities.ModelsViews
{
	[IndexView(typeof(Specialty))]
	[SearchBy("Reference")]
    public class Default_Specialty_Index_Model : BaseModel
    {
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Sector))]
		[GAppDataTable(PropertyPath = "Sector", FilterBy = "Sector.Id", SearchBy = "Sector.Reference", OrderBy = "Sector.Reference",  AutoGenerateFilter = true,isColumn = true )]
		public Sector Sector  {set; get;}  
   
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_TrainingLevel))]
		[GAppDataTable(PropertyPath = "TrainingLevel", FilterBy = "TrainingLevel.Id", SearchBy = "TrainingLevel.Reference", OrderBy = "TrainingLevel.Reference",  AutoGenerateFilter = true,isColumn = true )]
		public TrainingLevel TrainingLevel  {set; get;}  
   
		[Required]
		[Unique]
		[Display(Name = "Code", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Code", FilterBy = "Code", SearchBy = "Code", OrderBy = "Code",  AutoGenerateFilter = false,isColumn = true )]
		public String Code  {set; get;}  
   
		[Required]
		[Display(Name = "Name", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Name", FilterBy = "Name", SearchBy = "Name", OrderBy = "Name",  AutoGenerateFilter = false,isColumn = true )]
		public String Name  {set; get;}  
   
		[Display(Name = "Description", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Description", FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  AutoGenerateFilter = false,isColumn = true )]
		public String Description  {set; get;}  
   
    }
}    
