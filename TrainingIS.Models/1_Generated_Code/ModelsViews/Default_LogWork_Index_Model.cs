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
using TrainingIS.Entities.enums;
 

namespace TrainingIS.Entities.ModelsViews
{
	[IndexView(typeof(LogWork))]
	[SearchBy("Reference")]
    public class Default_LogWork_Index_Model : BaseModel
    {
		[Required]
		public String UserId  {set; get;}  
   
		[Required]
		public OperationWorkTypes OperationWorkType  {set; get;}  
   
		public String OperationReference  {set; get;}  
   
		public String EntityType  {set; get;}  
   
		public String Description  {set; get;}  
   
    }
}    
