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
    
    public class Default_SeancePlanningFormView : BaseModelView
    {
		[Required]
		public Int64 TrainingId  {set; get;}  
   
		[Required]
		public Int64 SeanceDayId  {set; get;}  
   
		[Required]
		public Int64 SeanceNumberId  {set; get;}  
   
		public String Description  {set; get;}  
   
    }
}
