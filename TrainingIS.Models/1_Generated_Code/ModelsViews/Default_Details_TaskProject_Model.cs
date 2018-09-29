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
using GApp.Models.DataAnnotations; 
 

namespace TrainingIS.Entities.ModelsViews
{
	[DetailsView(typeof(TaskProject))]
	[IndexView(typeof(TaskProject))]
    public class Default_Details_TaskProject_Model : BaseModel
    {
		[Display(Name = "SingularName", ResourceType = typeof(msg_Project))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "Project.Id", SearchBy = "Project.Reference", OrderBy = "Project.Reference",  PropertyPath = "Project")]
		public Project Project  {set; get;}  
   
		[Display(Name = "TaskState", ResourceType = typeof(msg_TaskProject))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "TaskState", SearchBy = "TaskState", OrderBy = "TaskState",  PropertyPath = "TaskState")]
		public TaskStates TaskState  {set; get;}  
   
		[Required]
		[Display(Name = "Name", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Name", SearchBy = "Name", OrderBy = "Name",  PropertyPath = "Name")]
		public String Name  {set; get;}  
   
		[Required]
		[Display(Name = "Code", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Code", SearchBy = "Code", OrderBy = "Code",  PropertyPath = "Code")]
		[ReadFrom(PropertyName = "Name", ReadOnly = false)]
		public String Code  {set; get;}  
   
		[Display(Name = "StartDate", ResourceType = typeof(msg_TaskProject))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "StartDate", SearchBy = "StartDate", OrderBy = "StartDate",  PropertyPath = "StartDate")]
		public DateTime StartDate  {set; get;}  
   
		[Display(Name = "EndtDate", ResourceType = typeof(msg_TaskProject))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "EndtDate", SearchBy = "EndtDate", OrderBy = "EndtDate",  PropertyPath = "EndtDate")]
		public DateTime EndtDate  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  PropertyPath = "Description")]
		public String Description  {set; get;}  
   
		[Display(Name = "isPublic", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "isPublic", SearchBy = "isPublic", OrderBy = "isPublic",  PropertyPath = "isPublic")]
		public Boolean isPublic  {set; get;}  
   
    }
}    