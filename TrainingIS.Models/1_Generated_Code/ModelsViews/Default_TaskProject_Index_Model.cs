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
using TrainingIS.Entities.Resources.ProjectResources; 
using TrainingIS.Entities.Resources.TaskProjectResources; 
using GApp.Entities.Resources.AppResources; 
 

namespace TrainingIS.Entities.ModelsViews
{
	[IndexView(typeof(TaskProject))]
	[SearchBy("Reference")]
    public class Default_TaskProject_Index_Model : BaseModel
    {
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Project))]
		[GAppDataTable(PropertyPath = "Project", FilterBy = "Project.Id", SearchBy = "Project.Reference", OrderBy = "Project.Reference",  AutoGenerateFilter = true,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public Project Project  {set; get;}  
   
		[Display(Name = "TaskState", Order = 0, ResourceType = typeof(msg_TaskProject))]
		[GAppDataTable(PropertyPath = "TaskState", FilterBy = "TaskState", SearchBy = "TaskState", OrderBy = "TaskState",  AutoGenerateFilter = true,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public TaskStates TaskState  {set; get;}  
   
		[Required]
		[Display(Name = "Name", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Name", FilterBy = "Name", SearchBy = "Name", OrderBy = "Name",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public String Name  {set; get;}  
   
		[Display(Name = "Description", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Description", FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public String Description  {set; get;}  
   
		[Display(Name = "StartDate", Order = 0, ResourceType = typeof(msg_TaskProject))]
		[GAppDataTable(PropertyPath = "StartDate", FilterBy = "StartDate", SearchBy = "StartDate", OrderBy = "StartDate",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public DateTime StartDate  {set; get;}  
   
		[Display(Name = "EndtDate", Order = 0, ResourceType = typeof(msg_TaskProject))]
		[GAppDataTable(PropertyPath = "EndtDate", FilterBy = "EndtDate", SearchBy = "EndtDate", OrderBy = "EndtDate",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public DateTime EndtDate  {set; get;}  
   
		[Display(Name = "isPublic", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "isPublic", FilterBy = "isPublic", SearchBy = "isPublic", OrderBy = "isPublic",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public Boolean isPublic  {set; get;}  
   
    }
}    
