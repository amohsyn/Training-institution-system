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
    
    public class Default_TrainingFormView : BaseModelView
    {
		[Required]
		public Int64 TrainingYearId  {set; get;}  
   
		[Required]
		public Int64 ModuleTrainingId  {set; get;}  
   
		[Required]
		public Int64 FormerId  {set; get;}  
   
		[Required]
		public Int64 GroupId  {set; get;}  
   
		public String Code  {set; get;}  
   
		public String Description  {set; get;}  
   
    }
}
