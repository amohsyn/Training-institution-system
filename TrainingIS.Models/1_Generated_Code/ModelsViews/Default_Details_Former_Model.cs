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
using TrainingIS.Entities.Resources.FormerSpecialtyResources; 
using TrainingIS.Entities.Resources.FormerResources; 
using GApp.Entities.Resources.PersonResources; 
using TrainingIS.Entities.Resources.NationalityResources; 
 

namespace TrainingIS.Entities.ModelsViews
{
	[DetailsView(typeof(Former))]
	[IndexView(typeof(Former))]
    public class Default_Details_Former_Model : BaseModel
    {
		[Display(Name = "SingularName", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_FormerSpecialty))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "FormerSpecialty.Id", SearchBy = "FormerSpecialty.Reference", OrderBy = "FormerSpecialty.Reference",  PropertyPath = "FormerSpecialty")]
		public FormerSpecialty FormerSpecialty  {set; get;}  
   
		[Display(Name = "WeeklyHourlyMass", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_Former))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "WeeklyHourlyMass", SearchBy = "WeeklyHourlyMass", OrderBy = "WeeklyHourlyMass",  PropertyPath = "WeeklyHourlyMass")]
		public Int32 WeeklyHourlyMass  {set; get;}  
   
		[Required]
		[Unique]
		[Display(Name = "RegistrationNumber", GroupName = "JobInformation", Order = 30, ResourceType = typeof(msg_Former))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "RegistrationNumber", SearchBy = "RegistrationNumber", OrderBy = "RegistrationNumber",  PropertyPath = "RegistrationNumber")]
		public String RegistrationNumber  {set; get;}  
   
		[Display(Name = "CreateUserAccount", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_Former))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "CreateUserAccount", SearchBy = "CreateUserAccount", OrderBy = "CreateUserAccount",  PropertyPath = "CreateUserAccount")]
		public Boolean CreateUserAccount  {set; get;}  
   
		[Required]
		[Display(Name = "Login", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_Former))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Login", SearchBy = "Login", OrderBy = "Login",  PropertyPath = "Login")]
		public String Login  {set; get;}  
   
		[Required]
		[Display(Name = "Password", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_Former))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Password", SearchBy = "Password", OrderBy = "Password",  PropertyPath = "Password")]
		public String Password  {set; get;}  
   
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
