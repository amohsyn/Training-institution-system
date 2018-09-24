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
	[EditView(typeof(Trainee))]
	[CreateView(typeof(Trainee))]
    public class Default_Form_Trainee_Model : BaseModel
    {
		[Required]
		[Unique]
		[Display(Name = "CEF", ResourceType = typeof(msg_Trainee))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "CNE")]
		public String CNE  {set; get;}  
   
		[Display(Name = "DateRegistration", ResourceType = typeof(msg_Trainee))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "DateRegistration")]
		public DateTime DateRegistration  {set; get;}  
   
		[Display(Name = "isActif", ResourceType = typeof(msg_Trainee))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "isActif")]
		public IsActifEnum isActif  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_Schoollevel))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "SchoollevelId")]
		public Int64 SchoollevelId  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_Specialty))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "SpecialtyId")]
		public Int64 SpecialtyId  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_YearStudy))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "YearStudyId")]
		public Int64 YearStudyId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_Group))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "GroupId")]
		public Int64 GroupId  {set; get;}  
   
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
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "NationalityId")]
		public Int64 NationalityId  {set; get;}  
   
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
