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
using TrainingIS.Entities.Resources.FormerResources; 
using TrainingIS.Entities.Resources.FormerSpecialtyResources; 
using GApp.Entities.Resources.PersonResources; 
using TrainingIS.Entities.Resources.NationalityResources; 
 
namespace TrainingIS.Entities.ModelsViews
{
	[DetailsView(typeof(Former))]
	[IndexView(typeof(Former))]
    public class Default_Details_Former_Model : BaseModel
    {
		[Required]
		[Unique]
		[Display(Name = "RegistrationNumber", ResourceType = typeof(msg_Former))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "RegistrationNumber")]
		public String RegistrationNumber  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_FormerSpecialty))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "FormerSpecialty.Reference")]
		public FormerSpecialty FormerSpecialty  {set; get;}  
   
		[Display(Name = "WeeklyHourlyMass", ResourceType = typeof(msg_Former))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "WeeklyHourlyMass")]
		public Int32 WeeklyHourlyMass  {set; get;}  
   
		[Display(Name = "CreateUserAccount", ResourceType = typeof(msg_Former))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "CreateUserAccount")]
		public Boolean CreateUserAccount  {set; get;}  
   
		[Required]
		[Display(Name = "Login", ResourceType = typeof(msg_Former))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Login")]
		public String Login  {set; get;}  
   
		[Required]
		[Display(Name = "Password", ResourceType = typeof(msg_Former))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Password")]
		public String Password  {set; get;}  
   
		[Required]
		[Display(Name = "FirstName", ResourceType = typeof(msg_Person))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "FirstName")]
		public String FirstName  {set; get;}  
   
		[Required]
		[Display(Name = "LastName", ResourceType = typeof(msg_Person))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "LastName")]
		public String LastName  {set; get;}  
   
		[Display(Name = "FirstNameArabe", ResourceType = typeof(msg_Person))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "FirstNameArabe")]
		public String FirstNameArabe  {set; get;}  
   
		[Display(Name = "LastNameArabe", ResourceType = typeof(msg_Person))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "LastNameArabe")]
		public String LastNameArabe  {set; get;}  
   
		[Required]
		[Display(Name = "Sex", ResourceType = typeof(msg_Person))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Sex")]
		public SexEnum Sex  {set; get;}  
   
		[Display(Name = "Birthdate", ResourceType = typeof(msg_Person))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Birthdate")]
		public DateTime Birthdate  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_Nationality))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Nationality.Reference")]
		public Nationality Nationality  {set; get;}  
   
		[Display(Name = "BirthPlace", ResourceType = typeof(msg_Person))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "BirthPlace")]
		public String BirthPlace  {set; get;}  
   
		[Unique]
		[Display(Name = "CIN", ResourceType = typeof(msg_Person))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "CIN")]
		public String CIN  {set; get;}  
   
		[Display(Name = "Cellphone", ResourceType = typeof(msg_Person))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Cellphone")]
		public String Cellphone  {set; get;}  
   
		[Unique]
		[Display(Name = "Email", ResourceType = typeof(msg_Person))]
		[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "")]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Email")]
		[DataType(DataType.EmailAddress)]
		public String Email  {set; get;}  
   
		[Display(Name = "Address", ResourceType = typeof(msg_Person))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Address")]
		public String Address  {set; get;}  
   
		[Display(Name = "FaceBook", ResourceType = typeof(msg_Person))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "FaceBook")]
		public String FaceBook  {set; get;}  
   
		[Display(Name = "WebSite", ResourceType = typeof(msg_Person))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "WebSite")]
		public String WebSite  {set; get;}  
   
    }
}    
