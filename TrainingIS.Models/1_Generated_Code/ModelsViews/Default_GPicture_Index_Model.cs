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
	[IndexView(typeof(GPicture))]
	[SearchBy("Reference")]
    public class Default_GPicture_Index_Model : BaseModel
    {
		public String Name  {set; get;}  
   
		public String Description  {set; get;}  
   
		public String Original_Thumbnail  {set; get;}  
   
		public String Large_Thumbnail  {set; get;}  
   
		public String Medium_Thumbnail  {set; get;}  
   
		public String Small_Thumbnail  {set; get;}  
   
		public String Old_Reference  {set; get;}  
   
    }
}    
