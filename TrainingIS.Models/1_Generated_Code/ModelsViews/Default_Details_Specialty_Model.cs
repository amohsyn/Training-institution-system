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
	[DetailsView(typeof(Specialty))]
	[IndexView(typeof(Specialty))]
    public class Default_Details_Specialty_Model : BaseModel
    {
		[Display(Name = "SingularName", ResourceType = typeof(msg_Sector))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "Sector.Id", SearchBy = "Sector.Reference", OrderBy = "Sector.Reference",  PropertyPath = "Sector")]
		public Sector Sector  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_TrainingLevel))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "TrainingLevel.Id", SearchBy = "TrainingLevel.Reference", OrderBy = "TrainingLevel.Reference",  PropertyPath = "TrainingLevel")]
		public TrainingLevel TrainingLevel  {set; get;}  
   
		[Required]
		[Unique]
		[Display(Name = "Code", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Code", SearchBy = "Code", OrderBy = "Code",  PropertyPath = "Code")]
		public String Code  {set; get;}  
   
		[Required]
		[Display(Name = "Name", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Name", SearchBy = "Name", OrderBy = "Name",  PropertyPath = "Name")]
		public String Name  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  PropertyPath = "Description")]
		public String Description  {set; get;}  
   
    }
}    
