using GApp.Core.Entities.ModelsViews;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TrainingIS.Entities.Resources.AppResources;
using TrainingIS.Entities.Resources.AppRoleResources;
using GApp.Core.MetaDatas.Attributes;

namespace TrainingIS.Entities.ModelsViews
{
    
    public class Default_TraineeFormView : BaseModelView
    {
		public String Cellphone  {set; get;}  
   
		public String TutorCellPhone  {set; get;}  
   
		public String Email  {set; get;}  
   
		public String Address  {set; get;}  
   
		public String FaceBook  {set; get;}  
   
		public String WebSite  {set; get;}  
   
		[Required]
		[Unique]
		public String CNE  {set; get;}  
   
		public IsActifEnum isActif  {set; get;}  
   
		public DateTime DateRegistration  {set; get;}  
   
		[Required]
		public Int64 NationalityId  {set; get;}  
   
		public Int64 SchoollevelId  {set; get;}  
   
		[Required]
		public Int64 GroupId  {set; get;}  
   
		[Required]
		public String FirstName  {set; get;}  
   
		[Required]
		public String LastName  {set; get;}  
   
		[Required]
		public String FirstNameArabe  {set; get;}  
   
		[Required]
		public String LastNameArabe  {set; get;}  
   
		[Required]
		public DateTime Birthdate  {set; get;}  
   
		[Required]
		public String BirthPlace  {set; get;}  
   
		[Required]
		public SexEnum Sex  {set; get;}  
   
		[Required]
		[Unique]
		public String CIN  {set; get;}  
   
    }
}
