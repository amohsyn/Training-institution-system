﻿using GApp.Core.Entities.ModelsViews;
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
using System.ComponentModel.DataAnnotations;

using GApp.Entities.Resources.PersonResources;  
using TrainingIS.Entities.Resources.TraineeResources;  
using TrainingIS.Entities.Resources.SchoollevelResources;  
using TrainingIS.Entities.Resources.SpecialtyResources;  
using TrainingIS.Entities.Resources.YearStudyResources;  
using TrainingIS.Entities.Resources.GroupResources;  
using TrainingIS.Entities.Resources.NationalityResources;  
using GApp.Entities.Resources.BaseEntity;  
 
namespace TrainingIS.Entities.ModelsViews
{
    [FormView(typeof(Trainee))]
    public class Default_Form_Trainee_Model : BaseModel
    {
		[Display(Name = "Photo", GroupName = "Photo", Order = 1, ResourceType = typeof(msg_Person))]
		[GAppDataTable(PropertyPath = "Photo", FilterBy = "Photo.Id", SearchBy = "Photo.Description", OrderBy = "Photo.UpdateDate",  AutoGenerateFilter = false,isColumn = true )]
		public GPicture Photo  {set; get;}  
   
		[Display(AutoGenerateField =false)]
		public String Photo_Reference  {set; get;}  
   
		[Required]
		[Unique]
		[Display(Name = "CEF", GroupName = "RegistrationForm", Order = 30, ResourceType = typeof(msg_Trainee))]
		[GAppDataTable(PropertyPath = "CNE", FilterBy = "CNE", SearchBy = "CNE", OrderBy = "CNE",  AutoGenerateFilter = false,isColumn = true )]
		public String CNE  {set; get;}  
   
		[Display(Name = "DateRegistration", GroupName = "RegistrationForm", Order = 31, ResourceType = typeof(msg_Trainee))]
		[GAppDataTable(PropertyPath = "DateRegistration", FilterBy = "DateRegistration", SearchBy = "DateRegistration", OrderBy = "DateRegistration",  AutoGenerateFilter = false,isColumn = true )]
		public DateTime DateRegistration  {set; get;}  
   
		[Display(Name = "isActif", GroupName = "RegistrationForm", Order = 32, ResourceType = typeof(msg_Trainee))]
		[GAppDataTable(PropertyPath = "isActif", FilterBy = "isActif", SearchBy = "isActif", OrderBy = "isActif",  AutoGenerateFilter = true,isColumn = true )]
		public IsActifEnum isActif  {set; get;}  
   
		[Display(Name = "SingularName", Order = 19, ResourceType = typeof(msg_Schoollevel))]
		[GAppDataTable(PropertyPath = "SchoollevelId", FilterBy = "SchoollevelId", SearchBy = "SchoollevelId", OrderBy = "SchoollevelId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 SchoollevelId  {set; get;}  
   
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Specialty))]
		[GAppDataTable(PropertyPath = "SpecialtyId", FilterBy = "SpecialtyId", SearchBy = "SpecialtyId", OrderBy = "SpecialtyId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 SpecialtyId  {set; get;}  
   
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_YearStudy))]
		[GAppDataTable(PropertyPath = "YearStudyId", FilterBy = "YearStudyId", SearchBy = "YearStudyId", OrderBy = "YearStudyId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 YearStudyId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", Order = 20, ResourceType = typeof(msg_Group))]
		[GAppDataTable(PropertyPath = "GroupId", FilterBy = "GroupId", SearchBy = "GroupId", OrderBy = "GroupId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 GroupId  {set; get;}  
   
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
   
		[Display(Name = "SingularName", Order = 8, ResourceType = typeof(msg_Nationality))]
		[GAppDataTable(PropertyPath = "NationalityId", FilterBy = "NationalityId", SearchBy = "NationalityId", OrderBy = "NationalityId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 NationalityId  {set; get;}  
   
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
   
		[Unique]
		[Display(Name = "Reference", Order = 0, ResourceType = typeof(msg_BaseEntity))]
		[GAppDataTable(PropertyPath = "Reference", FilterBy = "Reference", SearchBy = "Reference", OrderBy = "Reference",  AutoGenerateFilter = false,isColumn = false )]
		public String Reference  {set; get;}  
   
    }
}    
