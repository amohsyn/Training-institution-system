﻿using GApp.Core.Entities.ModelsViews;
using System; 
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TrainingIS.Entities.Resources.AppResources;
using GApp.Core.MetaDatas.Attributes; 
using TrainingIS.Entities.Resources.AppResources; 
using TrainingIS.Entities.Resources.ClassroomCategoryResources; 

namespace TrainingIS.Entities.ModelsViews
{
    
    public class Default_ClassroomDetailsView : BaseModelView
    {
		[Required]
		[Unique]
		[Display(Name = "Code", ResourceType = typeof(msg_app))]
		public String Code  {set; get;}  
   
		[Display(Name = "Name", ResourceType = typeof(msg_app))]
		public String Name  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_ClassroomCategory))]
		public ClassroomCategory ClassroomCategory  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		public String Description  {set; get;}  
   
    }
}
