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
using TrainingIS.Entities.Resources.SpecialtyResources;  
using TrainingIS.Entities.Resources.MetierResources;  
using TrainingIS.Entities.Resources.YearStudyResources;  
using GApp.Entities.Resources.AppResources;  
using TrainingIS.Entities.Resources.ModuleTrainingResources;  
 
namespace TrainingIS.Entities.ModelsViews
{
	[EditView(typeof(ModuleTraining))]
	[CreateView(typeof(ModuleTraining))]
    public class Default_Form_ModuleTraining_Model : BaseModel
    {
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_Specialty))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "SpecialtyId", SearchBy = "SpecialtyId", OrderBy = "SpecialtyId",  PropertyPath = "SpecialtyId")]
		public Int64 SpecialtyId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_Metier))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "MetierId", SearchBy = "MetierId", OrderBy = "MetierId",  PropertyPath = "MetierId")]
		public Int64 MetierId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_YearStudy))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "YearStudyId", SearchBy = "YearStudyId", OrderBy = "YearStudyId",  PropertyPath = "YearStudyId")]
		public Int64 YearStudyId  {set; get;}  
   
		[Required]
		[Display(Name = "Name", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "Name", SearchBy = "Name", OrderBy = "Name",  PropertyPath = "Name")]
		public String Name  {set; get;}  
   
		[Display(Name = "Code", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Code", SearchBy = "Code", OrderBy = "Code",  PropertyPath = "Code")]
		public String Code  {set; get;}  
   
		[Display(Name = "HourlyMass", ResourceType = typeof(msg_ModuleTraining))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "HourlyMass", SearchBy = "HourlyMass", OrderBy = "HourlyMass",  PropertyPath = "HourlyMass")]
		public Single HourlyMass  {set; get;}  
   
		[Display(Name = "Hourly_Mass_To_Teach", ResourceType = typeof(msg_ModuleTraining))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Hourly_Mass_To_Teach", SearchBy = "Hourly_Mass_To_Teach", OrderBy = "Hourly_Mass_To_Teach",  PropertyPath = "Hourly_Mass_To_Teach")]
		public Single Hourly_Mass_To_Teach  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  PropertyPath = "Description")]
		public String Description  {set; get;}  
   
    }
}    
