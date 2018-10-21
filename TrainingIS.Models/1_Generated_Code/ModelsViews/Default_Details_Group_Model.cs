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
	[DetailsView(typeof(Group))]
	[IndexView(typeof(Group))]
    public class Default_Details_Group_Model : BaseModel
    {
		[Display(Name = "SingularName", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_TrainingType))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "TrainingType.Id", SearchBy = "TrainingType.Reference", OrderBy = "TrainingType.Reference",  PropertyPath = "TrainingType")]
		public TrainingType TrainingType  {set; get;}  
   
		[Display(Name = "SingularName", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_TrainingYear))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "TrainingYear.Id", SearchBy = "TrainingYear.Reference", OrderBy = "TrainingYear.Reference",  PropertyPath = "TrainingYear")]
		public TrainingYear TrainingYear  {set; get;}  
   
		[Display(Name = "SingularName", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_Specialty))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "Specialty.Id", SearchBy = "Specialty.Reference", OrderBy = "Specialty.Reference",  PropertyPath = "Specialty")]
		public Specialty Specialty  {set; get;}  
   
		[Display(Name = "SingularName", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_YearStudy))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "YearStudy.Id", SearchBy = "YearStudy.Reference", OrderBy = "YearStudy.Reference",  PropertyPath = "YearStudy")]
		public YearStudy YearStudy  {set; get;}  
   
		[Required]
		[Display(Name = "Code", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Code", SearchBy = "Code", OrderBy = "Code",  PropertyPath = "Code")]
		public String Code  {set; get;}  
   
		[Display(Name = "Description", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  PropertyPath = "Description")]
		public String Description  {set; get;}  
   
		public List<Trainee> Trainees  {set; get;}  
   
    }
}    
