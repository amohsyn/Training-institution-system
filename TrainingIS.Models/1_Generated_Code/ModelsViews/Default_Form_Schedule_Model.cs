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
using TrainingIS.Entities.Resources.TrainingYearResources;  
using TrainingIS.Entities.Resources.ScheduleResources;  
using GApp.Entities.Resources.AppResources;  
 
namespace TrainingIS.Entities.ModelsViews
{
	[EditView(typeof(Schedule))]
	[CreateView(typeof(Schedule))]
    public class Default_Form_Schedule_Model : BaseModel
    {
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_TrainingYear))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "TrainingYearId")]
		public Int64 TrainingYearId  {set; get;}  
   
		[Required]
		[Display(Name = "StartDate", ResourceType = typeof(msg_Schedule))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "StartDate")]
		public DateTime StartDate  {set; get;}  
   
		[Required]
		[Display(Name = "EndtDate", ResourceType = typeof(msg_Schedule))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "EndtDate")]
		public DateTime EndtDate  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Description")]
		public String Description  {set; get;}  
   
    }
}    
