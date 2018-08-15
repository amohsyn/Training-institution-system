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
	[EditView(typeof(StateOfAbsece))]
	[CreateView(typeof(StateOfAbsece))]
    public class Default_Form_StateOfAbsece_Model : BaseModel
    {
		[Required]
		[Display(Name = "Name", ResourceType = typeof(msg_app))]
		public String Name  {set; get;}  
   
		[Required]
		[Display(Name = "Category", ResourceType = typeof(msg_StateOfAbsece))]
		public StateOfAbseceCategories Category  {set; get;}  
   
		[Required]
		[Display(Name = "Value", ResourceType = typeof(msg_app))]
		public Int32 Value  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_Trainee))]
		public Int64 TraineeId  {set; get;}  
   
    }
}    
