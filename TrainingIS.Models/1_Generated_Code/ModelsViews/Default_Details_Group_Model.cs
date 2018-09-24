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
		[Display(Name = "SingularName", ResourceType = typeof(msg_TrainingType))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "TrainingType.Reference")]
		public TrainingType TrainingType  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_TrainingYear))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "TrainingYear.Reference")]
		public TrainingYear TrainingYear  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_Specialty))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Specialty.Reference")]
		public Specialty Specialty  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_YearStudy))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "YearStudy.Reference")]
		public YearStudy YearStudy  {set; get;}  
   
		[Required]
		[Display(Name = "Code", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Code")]
		public String Code  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Description")]
		public String Description  {set; get;}  
   
		public List<Trainee> Trainees  {set; get;}  
   
    }
}    
