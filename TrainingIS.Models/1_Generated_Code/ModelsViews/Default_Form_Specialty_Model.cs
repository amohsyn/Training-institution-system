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
	[EditView(typeof(Specialty))]
	[CreateView(typeof(Specialty))]
    public class Default_Form_Specialty_Model : BaseModel
    {
		[Required]
		[Display(Name = "SingularName", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_Sector))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "SectorId", SearchBy = "SectorId", OrderBy = "SectorId",  PropertyPath = "SectorId")]
		public Int64 SectorId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_TrainingLevel))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "TrainingLevelId", SearchBy = "TrainingLevelId", OrderBy = "TrainingLevelId",  PropertyPath = "TrainingLevelId")]
		public Int64 TrainingLevelId  {set; get;}  
   
		[Required]
		[Unique]
		[Display(Name = "Code", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Code", SearchBy = "Code", OrderBy = "Code",  PropertyPath = "Code")]
		public String Code  {set; get;}  
   
		[Required]
		[Display(Name = "Name", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Name", SearchBy = "Name", OrderBy = "Name",  PropertyPath = "Name")]
		public String Name  {set; get;}  
   
		[Display(Name = "Description", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  PropertyPath = "Description")]
		public String Description  {set; get;}  
   
    }
}    
