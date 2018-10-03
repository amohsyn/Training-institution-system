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
using GApp.Entities.Resources.AppResources;  
using TrainingIS.Entities.Resources.TaskProjectResources;  
 
namespace TrainingIS.Entities.ModelsViews
{
	[EditView(typeof(Project))]
	[CreateView(typeof(Project))]
    public class Default_Form_Project_Model : BaseModel
    {
		public String OwnerId  {set; get;}  
   
		[Required]
		[Display(Name = "Name", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Name", SearchBy = "Name", OrderBy = "Name",  PropertyPath = "Name")]
		public String Name  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  PropertyPath = "Description")]
		public String Description  {set; get;}  
   
		[Display(Name = "StartDate", ResourceType = typeof(msg_TaskProject))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "StartDate", SearchBy = "StartDate", OrderBy = "StartDate",  PropertyPath = "StartDate")]
		public DateTime StartDate  {set; get;}  
   
		[Display(Name = "EndtDate", ResourceType = typeof(msg_TaskProject))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "EndtDate", SearchBy = "EndtDate", OrderBy = "EndtDate",  PropertyPath = "EndtDate")]
		public DateTime EndtDate  {set; get;}  
   
		[Display(Name = "isPublic", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "isPublic", SearchBy = "isPublic", OrderBy = "isPublic",  PropertyPath = "isPublic")]
		public Boolean isPublic  {set; get;}  
   
    }
}    
