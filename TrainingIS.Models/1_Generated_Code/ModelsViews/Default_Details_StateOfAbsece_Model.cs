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
using GApp.Entities.Resources.AppResources; 
using TrainingIS.Entities.Resources.StateOfAbseceResources; 
using TrainingIS.Entities.Resources.TraineeResources; 
 
namespace TrainingIS.Entities.ModelsViews
{
	[DetailsView(typeof(StateOfAbsece))]
	[IndexView(typeof(StateOfAbsece))]
    public class Default_Details_StateOfAbsece_Model : BaseModel
    {
		[Required]
		[Display(Name = "Name", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Name")]
		public String Name  {set; get;}  
   
		[Required]
		[Display(Name = "Category", ResourceType = typeof(msg_StateOfAbsece))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Category")]
		public StateOfAbseceCategories Category  {set; get;}  
   
		[Required]
		[Display(Name = "Value", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Value")]
		public Int32 Value  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_Trainee))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Trainee.Reference")]
		public Trainee Trainee  {set; get;}  
   
    }
}    
