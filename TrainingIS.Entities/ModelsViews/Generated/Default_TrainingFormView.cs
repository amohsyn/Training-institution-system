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
using TrainingIS.Entities.Resources.TrainingYearResources; 
using TrainingIS.Entities.Resources.ModuleTrainingResources; 
using TrainingIS.Entities.Resources.FormerResources; 
using TrainingIS.Entities.Resources.GroupResources; 
using TrainingIS.Entities.Resources.AppResources; 

namespace TrainingIS.Entities.ModelsViews
{
    
    public class Default_TrainingFormView : BaseModelView
    {
		[Display(Name = "SingularName", ResourceType = typeof(msg_TrainingYear))]
		public TrainingYear TrainingYear  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_ModuleTraining))]
		public ModuleTraining ModuleTraining  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_Former))]
		public Former Former  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_Group))]
		public Group Group  {set; get;}  
   
		[Display(Name = "Code", ResourceType = typeof(msg_app))]
		public String Code  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		public String Description  {set; get;}  
   
    }
}
