using GApp.Core.Entities.ModelsViews;
using System; 
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TrainingIS.Entities.Resources.AppResources;
using GApp.Core.MetaDatas.Attributes; 
using TrainingIS.Entities.Resources.PersonResources; 
using TrainingIS.Entities.Resources.TraineeResources; 
using TrainingIS.Entities.Resources.NationalityResources; 
using TrainingIS.Entities.Resources.SchoollevelResources; 
using TrainingIS.Entities.Resources.GroupResources; 

namespace TrainingIS.Entities.ModelsViews
{
    
    public class Default_TraineeDetailsView : BaseModelView
    {
		[Display(Name = "Cellphone", ResourceType = typeof(msg_Person))]
		public String Cellphone  {set; get;}  
   
		[Display(Name = "TutorCellPhone", ResourceType = typeof(msg_Trainee))]
		public String TutorCellPhone  {set; get;}  
   
		[Display(Name = "Email", ResourceType = typeof(msg_Person))]
		public String Email  {set; get;}  
   
		[Display(Name = "Address", ResourceType = typeof(msg_Person))]
		public String Address  {set; get;}  
   
		[Display(Name = "FaceBook", ResourceType = typeof(msg_Person))]
		public String FaceBook  {set; get;}  
   
		[Display(Name = "WebSite", ResourceType = typeof(msg_Person))]
		public String WebSite  {set; get;}  
   
		[Required]
		[Unique]
		[Display(Name = "CEF", ResourceType = typeof(msg_Trainee))]
		public String CNE  {set; get;}  
   
		[Display(Name = "isActif", ResourceType = typeof(msg_Trainee))]
		public IsActifEnum isActif  {set; get;}  
   
		[Display(Name = "DateRegistration", ResourceType = typeof(msg_Trainee))]
		public DateTime DateRegistration  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_Nationality))]
		public Nationality Nationality  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_Schoollevel))]
		public Schoollevel Schoollevel  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_Group))]
		public Group Group  {set; get;}  
   
		public List<StateOfAbsece> StateOfAbseces  {set; get;}  
   
		[Required]
		[Display(Name = "FirstName", ResourceType = typeof(msg_Person))]
		public String FirstName  {set; get;}  
   
		[Required]
		[Display(Name = "LastName", ResourceType = typeof(msg_Person))]
		public String LastName  {set; get;}  
   
		[Required]
		[Display(Name = "FirstNameArabe", ResourceType = typeof(msg_Person))]
		public String FirstNameArabe  {set; get;}  
   
		[Required]
		[Display(Name = "LastNameArabe", ResourceType = typeof(msg_Person))]
		public String LastNameArabe  {set; get;}  
   
		[Required]
		[Display(Name = "Birthdate", ResourceType = typeof(msg_Person))]
		public DateTime Birthdate  {set; get;}  
   
		[Required]
		[Display(Name = "BirthPlace", ResourceType = typeof(msg_Person))]
		public String BirthPlace  {set; get;}  
   
		[Required]
		[Display(Name = "Sex", ResourceType = typeof(msg_Person))]
		public SexEnum Sex  {set; get;}  
   
		[Required]
		[Unique]
		[Display(Name = "CIN", ResourceType = typeof(msg_Person))]
		public String CIN  {set; get;}  
   
    }
}
