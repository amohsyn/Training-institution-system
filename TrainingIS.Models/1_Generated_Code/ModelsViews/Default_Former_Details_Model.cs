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
using GApp.Entities.Resources.PersonResources; 
using TrainingIS.Entities.Resources.FormerSpecialtyResources; 
using TrainingIS.Entities.Resources.FormerResources; 
using TrainingIS.Entities.Resources.NationalityResources; 
 

namespace TrainingIS.Entities.ModelsViews
{
	[DetailsView(typeof(Former))]
    public class Default_Former_Details_Model : BaseModel
    {
		[Display(Name = "Photo", GroupName = "Photo", Order = 1, ResourceType = typeof(msg_Person))]
		[GAppDataTable(PropertyPath = "Photo", FilterBy = "Photo.Id", SearchBy = "Photo.Description", OrderBy = "Photo.UpdateDate",  AutoGenerateFilter = false,isColumn = true )]
		public GPicture Photo  {set; get;}  
   
		[Display(AutoGenerateField =false)]
		public String Photo_Reference  {set; get;}  
   
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_FormerSpecialty))]
		[GAppDataTable(PropertyPath = "FormerSpecialty", FilterBy = "FormerSpecialty.Id", SearchBy = "FormerSpecialty.Reference", OrderBy = "FormerSpecialty.Reference",  AutoGenerateFilter = true,isColumn = true )]
		public FormerSpecialty FormerSpecialty  {set; get;}  
   
		[Display(Name = "WeeklyHourlyMass", Order = 0, ResourceType = typeof(msg_Former))]
		[GAppDataTable(PropertyPath = "WeeklyHourlyMass", FilterBy = "WeeklyHourlyMass", SearchBy = "WeeklyHourlyMass", OrderBy = "WeeklyHourlyMass",  AutoGenerateFilter = false,isColumn = true )]
		public Single WeeklyHourlyMass  {set; get;}  
   
		[Required]
		[Unique]
		[Display(Name = "RegistrationNumber", GroupName = "JobInformation", Order = 30, ResourceType = typeof(msg_Former))]
		[GAppDataTable(PropertyPath = "RegistrationNumber", FilterBy = "RegistrationNumber", SearchBy = "RegistrationNumber", OrderBy = "RegistrationNumber",  AutoGenerateFilter = false,isColumn = false )]
		public String RegistrationNumber  {set; get;}  
   
		[Display(Name = "CreateUserAccount", Order = 0, ResourceType = typeof(msg_Former))]
		[GAppDataTable(PropertyPath = "CreateUserAccount", FilterBy = "CreateUserAccount", SearchBy = "CreateUserAccount", OrderBy = "CreateUserAccount",  AutoGenerateFilter = false,isColumn = false )]
		public Boolean CreateUserAccount  {set; get;}  
   
		[Required]
		[Display(Name = "Login", Order = 0, ResourceType = typeof(msg_Former))]
		[GAppDataTable(PropertyPath = "Login", FilterBy = "Login", SearchBy = "Login", OrderBy = "Login",  AutoGenerateFilter = false,isColumn = false )]
		public String Login  {set; get;}  
   
		[Required]
		[Display(Name = "Password", Order = 0, ResourceType = typeof(msg_Former))]
		[GAppDataTable(PropertyPath = "Password", FilterBy = "Password", SearchBy = "Password", OrderBy = "Password",  AutoGenerateFilter = false,isColumn = false )]
		public String Password  {set; get;}  
   
		[Required]
		[Display(Name = "FirstName", GroupName = "CivilStatus", Order = 1, ResourceType = typeof(msg_Person))]
		[GAppDataTable(PropertyPath = "FirstName", FilterBy = "FirstName", SearchBy = "FirstName", OrderBy = "FirstName",  AutoGenerateFilter = false,isColumn = true )]
		public String FirstName  {set; get;}  
   
		[Required]
		[Display(Name = "LastName", GroupName = "CivilStatus", Order = 2, ResourceType = typeof(msg_Person))]
		[GAppDataTable(PropertyPath = "LastName", FilterBy = "LastName", SearchBy = "LastName", OrderBy = "LastName",  AutoGenerateFilter = false,isColumn = true )]
		public String LastName  {set; get;}  
   
		[Display(Name = "FirstNameArabe", GroupName = "CivilStatus", Order = 3, ResourceType = typeof(msg_Person))]
		[GAppDataTable(PropertyPath = "FirstNameArabe", FilterBy = "FirstNameArabe", SearchBy = "FirstNameArabe", OrderBy = "FirstNameArabe",  AutoGenerateFilter = false,isColumn = false )]
		public String FirstNameArabe  {set; get;}  
   
		[Display(Name = "LastNameArabe", GroupName = "CivilStatus", Order = 4, ResourceType = typeof(msg_Person))]
		[GAppDataTable(PropertyPath = "LastNameArabe", FilterBy = "LastNameArabe", SearchBy = "LastNameArabe", OrderBy = "LastNameArabe",  AutoGenerateFilter = false,isColumn = false )]
		public String LastNameArabe  {set; get;}  
   
		[Required]
		[Display(Name = "Sex", GroupName = "CivilStatus", Order = 5, ResourceType = typeof(msg_Person))]
		[GAppDataTable(PropertyPath = "Sex", FilterBy = "Sex", SearchBy = "Sex", OrderBy = "Sex",  AutoGenerateFilter = false,isColumn = false )]
		public SexEnum Sex  {set; get;}  
   
		[Display(Name = "Birthdate", GroupName = "CivilStatus", Order = 6, ResourceType = typeof(msg_Person))]
		[GAppDataTable(PropertyPath = "Birthdate", FilterBy = "Birthdate", SearchBy = "Birthdate", OrderBy = "Birthdate",  AutoGenerateFilter = false,isColumn = false )]
		public DateTime Birthdate  {set; get;}  
   
		[Display(Name = "SingularName", Order = 7, ResourceType = typeof(msg_Nationality))]
		[GAppDataTable(PropertyPath = "Nationality", FilterBy = "Nationality.Id", SearchBy = "Nationality.Reference", OrderBy = "Nationality.Reference",  AutoGenerateFilter = false,isColumn = false )]
		public Nationality Nationality  {set; get;}  
   
		[Display(Name = "BirthPlace", GroupName = "CivilStatus", Order = 9, ResourceType = typeof(msg_Person))]
		[GAppDataTable(PropertyPath = "BirthPlace", FilterBy = "BirthPlace", SearchBy = "BirthPlace", OrderBy = "BirthPlace",  AutoGenerateFilter = false,isColumn = false )]
		public String BirthPlace  {set; get;}  
   
		[Unique]
		[Display(Name = "CIN", GroupName = "CivilStatus", Order = 10, ResourceType = typeof(msg_Person))]
		[GAppDataTable(PropertyPath = "CIN", FilterBy = "CIN", SearchBy = "CIN", OrderBy = "CIN",  AutoGenerateFilter = false,isColumn = false )]
		public String CIN  {set; get;}  
   
		[Display(Name = "Cellphone", GroupName = "ContactInformation", Order = 20, ResourceType = typeof(msg_Person))]
		[GAppDataTable(PropertyPath = "Cellphone", FilterBy = "Cellphone", SearchBy = "Cellphone", OrderBy = "Cellphone",  AutoGenerateFilter = false,isColumn = false )]
		public String Cellphone  {set; get;}  
   
		[Unique]
		[Display(Name = "Email", GroupName = "ContactInformation", Order = 21, ResourceType = typeof(msg_Person))]
		[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "")]
		[GAppDataTable(PropertyPath = "Email", FilterBy = "Email", SearchBy = "Email", OrderBy = "Email",  AutoGenerateFilter = false,isColumn = false )]
		[DataType(DataType.EmailAddress)]
		public String Email  {set; get;}  
   
		[Display(Name = "Address", GroupName = "ContactInformation", Order = 22, ResourceType = typeof(msg_Person))]
		[GAppDataTable(PropertyPath = "Address", FilterBy = "Address", SearchBy = "Address", OrderBy = "Address",  AutoGenerateFilter = false,isColumn = false )]
		public String Address  {set; get;}  
   
		[Display(Name = "FaceBook", GroupName = "ContactInformation", Order = 23, ResourceType = typeof(msg_Person))]
		[GAppDataTable(PropertyPath = "FaceBook", FilterBy = "FaceBook", SearchBy = "FaceBook", OrderBy = "FaceBook",  AutoGenerateFilter = false,isColumn = false )]
		public String FaceBook  {set; get;}  
   
		[Display(Name = "WebSite", GroupName = "ContactInformation", Order = 24, ResourceType = typeof(msg_Person))]
		[GAppDataTable(PropertyPath = "WebSite", FilterBy = "WebSite", SearchBy = "WebSite", OrderBy = "WebSite",  AutoGenerateFilter = false,isColumn = false )]
		public String WebSite  {set; get;}  
   
    }
}    
