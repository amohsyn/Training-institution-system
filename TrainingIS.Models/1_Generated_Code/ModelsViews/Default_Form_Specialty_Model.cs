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
using System.ComponentModel.DataAnnotations;

using TrainingIS.Entities.Resources.SectorResources;  
using TrainingIS.Entities.Resources.TrainingLevelResources;  
using GApp.Entities.Resources.AppResources;  
using GApp.Entities.Resources.BaseEntity;  
 
namespace TrainingIS.Entities.ModelsViews
{
    [FormView(typeof(Specialty))]
    public class Default_Form_Specialty_Model : BaseModel
    {
		[Required]
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Sector))]
		[GAppDataTable(PropertyPath = "SectorId", FilterBy = "SectorId", SearchBy = "SectorId", OrderBy = "SectorId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 SectorId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_TrainingLevel))]
		[GAppDataTable(PropertyPath = "TrainingLevelId", FilterBy = "TrainingLevelId", SearchBy = "TrainingLevelId", OrderBy = "TrainingLevelId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 TrainingLevelId  {set; get;}  
   
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
   
		[Unique]
		[Display(Name = "Reference", Order = 0, ResourceType = typeof(msg_BaseEntity))]
		[GAppDataTable(PropertyPath = "Reference", FilterBy = "Reference", SearchBy = "Reference", OrderBy = "Reference",  AutoGenerateFilter = false,isColumn = false )]
		public String Reference  {set; get;}  
   
    }
}    
