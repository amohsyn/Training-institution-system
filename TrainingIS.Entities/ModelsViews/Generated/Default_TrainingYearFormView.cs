using GApp.Core.Entities.ModelsViews;
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
using TrainingIS.Entities.Resources.TrainingYearResources; 

namespace TrainingIS.Entities.ModelsViews
{
    
    public class Default_TrainingYearFormView : BaseModelView
    {
		[Required]
		[Display(Name = "Code", ResourceType = typeof(msg_app))]
		public String Code  {set; get;}  
   
		[Required]
		[Display(Name = "StartDate", ResourceType = typeof(msg_TrainingYear))]
		public DateTime StartDate  {set; get;}  
   
		[Required]
		[Display(Name = "EndtDate", ResourceType = typeof(msg_TrainingYear))]
		public DateTime EndtDate  {set; get;}  
   
    }
}
