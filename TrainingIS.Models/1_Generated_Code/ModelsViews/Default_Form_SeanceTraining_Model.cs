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
using TrainingIS.Entities.Resources.SeanceTrainingResources;  
using TrainingIS.Entities.Resources.SeancePlanningResources;  
 
namespace TrainingIS.Entities.ModelsViews
{
	[EditView(typeof(SeanceTraining))]
	[CreateView(typeof(SeanceTraining))]
    public class Default_Form_SeanceTraining_Model : BaseModel
    {
		[Required]
		[Display(Name = "SeanceDate", ResourceType = typeof(msg_SeanceTraining))]
		public DateTime SeanceDate  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_SeancePlanning))]
		public SeancePlanning SeancePlanning  {set; get;}  
   
    }
}    