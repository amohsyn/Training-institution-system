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
		[Display(Name = "CEF", GroupName = "RegistrationForm", Order = 30, ResourceType = typeof(msg_Trainee))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "CNE", SearchBy = "CNE", OrderBy = "CNE",  PropertyPath = "CNE")]
		public String CNE  {set; get;}  
   
		[Display(Name = "DateRegistration", GroupName = "RegistrationForm", Order = 31, ResourceType = typeof(msg_Trainee))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "DateRegistration", SearchBy = "DateRegistration", OrderBy = "DateRegistration",  PropertyPath = "DateRegistration")]
		public DateTime DateRegistration  {set; get;}  
   
		[Display(Name = "isActif", GroupName = "RegistrationForm", Order = 32, ResourceType = typeof(msg_Trainee))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "isActif", SearchBy = "isActif", OrderBy = "isActif",  PropertyPath = "isActif")]
		public IsActifEnum isActif  {set; get;}  
   
		[Display(Name = "SingularName", GroupName = "RegistrationForm", Order = 33, ResourceType = typeof(msg_Schoollevel))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "Schoollevel.Id", SearchBy = "Schoollevel.Reference", OrderBy = "Schoollevel.Reference",  PropertyPath = "Schoollevel")]
		public Schoollevel Schoollevel  {set; get;}  
   
		[Display(Name = "SingularName", GroupName = "Assignements", Order = 0, ResourceType = typeof(msg_Specialty))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "Specialty.Id", SearchBy = "Specialty.Reference", OrderBy = "Specialty.Reference",  PropertyPath = "Specialty")]
		public Specialty Specialty  {set; get;}  
   
		[Display(Name = "SingularName", GroupName = "Assignements", Order = 0, ResourceType = typeof(msg_YearStudy))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "YearStudy.Id", SearchBy = "YearStudy.Reference", OrderBy = "YearStudy.Reference",  PropertyPath = "YearStudy")]
		public YearStudy YearStudy  {set; get;}  
   
		[Display(Name = "SingularName", GroupName = "Assignements", Order = 40, ResourceType = typeof(msg_Group))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "Group.Id", SearchBy = "Group.Reference", OrderBy = "Group.Reference",  PropertyPath = "Group")]
		public Group Group  {set; get;}  
   
		[Required]
		[Display(Name = "FirstName", GroupName = "CivilStatus", Order = 1, ResourceType = typeof(msg_Person))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "FirstName", SearchBy = "FirstName", OrderBy = "FirstName",  PropertyPath = "FirstName")]
		public String FirstName  {set; get;}  
   
		[Required]
		[Display(Name = "LastName", GroupName = "CivilStatus", Order = 2, ResourceType = typeof(msg_Person))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "LastName", SearchBy = "LastName", OrderBy = "LastName",  PropertyPath = "LastName")]
		public String LastName  {set; get;}  
   
		[Display(Name = "FirstNameArabe", GroupName = "CivilStatus", Order = 3, ResourceType = typeof(msg_Person))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "FirstNameArabe", SearchBy = "FirstNameArabe", OrderBy = "FirstNameArabe",  PropertyPath = "FirstNameArabe")]
		public String FirstNameArabe  {set; get;}  
   
		[Display(Name = "LastNameArabe", GroupName = "CivilStatus", Order = 4, ResourceType = typeof(msg_Person))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "LastNameArabe", SearchBy = "LastNameArabe", OrderBy = "LastNameArabe",  PropertyPath = "LastNameArabe")]
		public String LastNameArabe  {set; get;}  
   
		[Required]
		[Display(Name = "Sex", GroupName = "CivilStatus", Order = 5, ResourceType = typeof(msg_Person))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Sex", SearchBy = "Sex", OrderBy = "Sex",  PropertyPath = "Sex")]
		public SexEnum Sex  {set; get;}  
   
		[Display(Name = "Birthdate", GroupName = "CivilStatus", Order = 6, ResourceType = typeof(msg_Person))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Birthdate", SearchBy = "Birthdate", OrderBy = "Birthdate",  PropertyPath = "Birthdate")]
		public DateTime Birthdate  {set; get;}  
   
		[Display(Name = "SingularName", GroupName = "SingularName", Order = 7, ResourceType = typeof(msg_Nationality))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Nationality.Id", SearchBy = "Nationality.Reference", OrderBy = "Nationality.Reference",  PropertyPath = "Nationality")]
		public Nationality Nationality  {set; get;}  
   
		[Display(Name = "BirthPlace", GroupName = "CivilStatus", Order = 9, ResourceType = typeof(msg_Person))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "BirthPlace", SearchBy = "BirthPlace", OrderBy = "BirthPlace",  PropertyPath = "BirthPlace")]
		public String BirthPlace  {set; get;}  
   
		[Unique]
		[Display(Name = "CIN", GroupName = "CivilStatus", Order = 10, ResourceType = typeof(msg_Person))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "CIN", SearchBy = "CIN", OrderBy = "CIN",  PropertyPath = "CIN")]
		public String CIN  {set; get;}  
   
		[Display(Name = "Photo", GroupName = "Photo", Order = 1, ResourceType = typeof(msg_Person))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Photo.Id", SearchBy = "Photo.Reference", OrderBy = "Photo.Reference",  PropertyPath = "Photo")]
		public GPicture Photo  {set; get;}  
   
		[Display(AutoGenerateField =false)]
		public String Photo_Reference  {set; get;}  
   
		[Display(Name = "Cellphone", GroupName = "ContactInformation", Order = 20, ResourceType = typeof(msg_Person))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Cellphone", SearchBy = "Cellphone", OrderBy = "Cellphone",  PropertyPath = "Cellphone")]
		public String Cellphone  {set; get;}  
   
		[Unique]
		[Display(Name = "Email", GroupName = "ContactInformation", Order = 21, ResourceType = typeof(msg_Person))]
		[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "")]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Email", SearchBy = "Email", OrderBy = "Email",  PropertyPath = "Email")]
		[DataType(DataType.EmailAddress)]
		public String Email  {set; get;}  
   
		[Display(Name = "Address", GroupName = "ContactInformation", Order = 22, ResourceType = typeof(msg_Person))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Address", SearchBy = "Address", OrderBy = "Address",  PropertyPath = "Address")]
		public String Address  {set; get;}  
   
		[Display(Name = "FaceBook", GroupName = "ContactInformation", Order = 23, ResourceType = typeof(msg_Person))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "FaceBook", SearchBy = "FaceBook", OrderBy = "FaceBook",  PropertyPath = "FaceBook")]
		public String FaceBook  {set; get;}  
   
		[Display(Name = "WebSite", GroupName = "ContactInformation", Order = 24, ResourceType = typeof(msg_Person))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "WebSite", SearchBy = "WebSite", OrderBy = "WebSite",  PropertyPath = "WebSite")]
		public String WebSite  {set; get;}  
   
    }
}    
