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
    
    public class Default_FormerFormView : BaseModelView
    {
		[Required]
		public String FirstName  {set; get;}  
   
		[Required]
		public String LastName  {set; get;}  
   
		[Required]
		public Boolean Sex  {set; get;}  
   
		public String CIN  {set; get;}  
   
		public String Cellphone  {set; get;}  
   
		[Required]
		public String Email  {set; get;}  
   
		public String Address  {set; get;}  
   
		public String FaceBook  {set; get;}  
   
		public String WebSite  {set; get;}  
   
		[Required]
		public String RegistrationNumber  {set; get;}  
   
    }
}
