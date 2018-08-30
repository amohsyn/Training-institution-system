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
using TrainingIS.Entities.Resources.TraineeResources; 
using TrainingIS.Entities.Resources.SchoollevelResources; 
using TrainingIS.Entities.Resources.SpecialtyResources; 
using TrainingIS.Entities.Resources.YearStudyResources; 
using TrainingIS.Entities.Resources.GroupResources; 
using GApp.Entities.Resources.PersonResources; 
using TrainingIS.Entities.Resources.NationalityResources; 
 
namespace TrainingIS.Entities.ModelsViews
{
	[DetailsView(typeof(Trainee))]
	[IndexView(typeof(Trainee))]
    public class Default_Details_Trainee_Model : BaseModel
    {
		[Required]
		[Unique]
		[Display(Name = "CEF", ResourceType = typeof(msg_Trainee))]
		public String CNE  {set; get;}  
   
		[Display(Name = "DateRegistration", ResourceType = typeof(msg_Trainee))]
		public DateTime DateRegistration  {set; get;}  
   
		[Display(Name = "isActif", ResourceType = typeof(msg_Trainee))]
		public IsActifEnum isActif  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_Schoollevel))]
		public Schoollevel Schoollevel  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_Specialty))]
		public Specialty Specialty  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_YearStudy))]
		public YearStudy YearStudy  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_Group))]
		public Group Group  {set; get;}  
   
		[Required]
		[Display(Name = "FirstName", ResourceType = typeof(msg_Person))]
		public String FirstName  {set; get;}  
   
		[Required]
		[Display(Name = "LastName", ResourceType = typeof(msg_Person))]
		public String LastName  {set; get;}  
   
		[Display(Name = "FirstNameArabe", ResourceType = typeof(msg_Person))]
		public String FirstNameArabe  {set; get;}  
   
		[Display(Name = "LastNameArabe", ResourceType = typeof(msg_Person))]
		public String LastNameArabe  {set; get;}  
   
		[Required]
		[Display(Name = "Sex", ResourceType = typeof(msg_Person))]
		public SexEnum Sex  {set; get;}  
   
		[Display(Name = "Birthdate", ResourceType = typeof(msg_Person))]
		public DateTime Birthdate  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_Nationality))]
		public Nationality Nationality  {set; get;}  
   
		[Display(Name = "BirthPlace", ResourceType = typeof(msg_Person))]
		public String BirthPlace  {set; get;}  
   
		[Unique]
		[Display(Name = "CIN", ResourceType = typeof(msg_Person))]
		public String CIN  {set; get;}  
   
		[Display(Name = "Cellphone", ResourceType = typeof(msg_Person))]
		public String Cellphone  {set; get;}  
   
		[Unique]
		[Display(Name = "Email", ResourceType = typeof(msg_Person))]
		[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "")]
		[DataType(DataType.EmailAddress)]
		public String Email  {set; get;}  
   
		[Display(Name = "Address", ResourceType = typeof(msg_Person))]
		public String Address  {set; get;}  
   
		[Display(Name = "FaceBook", ResourceType = typeof(msg_Person))]
		public String FaceBook  {set; get;}  
   
		[Display(Name = "WebSite", ResourceType = typeof(msg_Person))]
		public String WebSite  {set; get;}  
   
    }
}    
