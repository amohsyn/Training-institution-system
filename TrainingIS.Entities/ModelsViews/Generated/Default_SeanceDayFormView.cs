﻿using GApp.Core.Entities.ModelsViews;
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
    
    public class Default_SeanceDayFormView : BaseModelView
    {
		[Required]
		public String Name  {set; get;}  
   
		[Required]
		public String Code  {set; get;}  
   
		public String Description  {set; get;}  
   
    }
}
