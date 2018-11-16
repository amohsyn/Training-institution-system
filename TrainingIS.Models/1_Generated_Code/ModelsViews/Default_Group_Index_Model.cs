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
using TrainingIS.Entities.Resources.TrainingTypeResources; 
using TrainingIS.Entities.Resources.TrainingYearResources; 
using TrainingIS.Entities.Resources.SpecialtyResources; 
using TrainingIS.Entities.Resources.YearStudyResources; 
using GApp.Entities.Resources.AppResources; 
 

namespace TrainingIS.Entities.ModelsViews
{
	[IndexView(typeof(Group))]
	[SearchBy("Reference")]
    public class Default_Group_Index_Model : BaseModel
    {
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_TrainingType))]
		[GAppDataTable(PropertyPath = "TrainingType", FilterBy = "TrainingType.Id", SearchBy = "TrainingType.Reference", OrderBy = "TrainingType.Reference",  AutoGenerateFilter = true,isColumn = true )]
		public TrainingType TrainingType  {set; get;}  
   
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_TrainingYear))]
		[GAppDataTable(PropertyPath = "TrainingYear", FilterBy = "TrainingYear.Id", SearchBy = "TrainingYear.Reference", OrderBy = "TrainingYear.Reference",  AutoGenerateFilter = true,isColumn = true )]
		public TrainingYear TrainingYear  {set; get;}  
   
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Specialty))]
		[GAppDataTable(PropertyPath = "Specialty", FilterBy = "Specialty.Id", SearchBy = "Specialty.Reference", OrderBy = "Specialty.Reference",  AutoGenerateFilter = true,isColumn = true )]
		public Specialty Specialty  {set; get;}  
   
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_YearStudy))]
		[GAppDataTable(PropertyPath = "YearStudy", FilterBy = "YearStudy.Id", SearchBy = "YearStudy.Reference", OrderBy = "YearStudy.Reference",  AutoGenerateFilter = true,isColumn = true )]
		public YearStudy YearStudy  {set; get;}  
   
		[Required]
		[Display(Name = "Code", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Code", FilterBy = "Code", SearchBy = "Code", OrderBy = "Code",  AutoGenerateFilter = false,isColumn = true )]
		public String Code  {set; get;}  
   
		[Display(Name = "Description", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Description", FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  AutoGenerateFilter = false,isColumn = true )]
		public String Description  {set; get;}  
   
		public List<Trainee> Trainees  {set; get;}  
   
    }
}    
