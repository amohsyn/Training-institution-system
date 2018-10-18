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
using TrainingIS.Entities.Resources.FormerResources; 
using TrainingIS.Entities.Resources.AdministratorResources; 
using TrainingIS.Entities.Resources.TraineeResources; 
using TrainingIS.Entities.Resources.Mission_Working_GroupResources; 
 

namespace TrainingIS.Entities.ModelsViews
{
	[DetailsView(typeof(WorkGroup))]
	[IndexView(typeof(WorkGroup))]
    public class Default_Details_WorkGroup_Model : BaseModel
    {
		[Required]
		[Display(Name = "Code", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Code", SearchBy = "Code", OrderBy = "Code",  PropertyPath = "Code")]
		public String Code  {set; get;}  
   
		[Required]
		[Display(Name = "Name", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Name", SearchBy = "Name", OrderBy = "Name",  PropertyPath = "Name")]
		public String Name  {set; get;}  
   
		[Display(Name = "PluralName", ResourceType = typeof(msg_Former))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Formers", SearchBy = "Formers", OrderBy = "Formers",  PropertyPath = "Formers")]
		public List<Former> Formers  {set; get;}  
   
		[Display(Name = "PluralName", ResourceType = typeof(msg_Administrator))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Administrators", SearchBy = "Administrators", OrderBy = "Administrators",  PropertyPath = "Administrators")]
		public List<Administrator> Administrators  {set; get;}  
   
		[Display(Name = "PluralName", ResourceType = typeof(msg_Trainee))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Trainees", SearchBy = "Trainees", OrderBy = "Trainees",  PropertyPath = "Trainees")]
		public List<Trainee> Trainees  {set; get;}  
   
		[Display(Name = "PluralName", ResourceType = typeof(msg_Mission_Working_Group))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Mission_Working_Groups", SearchBy = "Mission_Working_Groups", OrderBy = "Mission_Working_Groups",  PropertyPath = "Mission_Working_Groups")]
		public List<Mission_Working_Group> Mission_Working_Groups  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  PropertyPath = "Description")]
		public String Description  {set; get;}  
   
    }
}    
