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
using TrainingIS.Entities.Resources.SanctionCategoryResources; 
 

namespace TrainingIS.Entities.ModelsViews
{
	[DetailsView(typeof(SanctionCategory))]
	[IndexView(typeof(SanctionCategory))]
    public class Default_Details_SanctionCategory_Model : BaseModel
    {
		[Required]
		[Display(Name = "Name", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Name", SearchBy = "Name", OrderBy = "Name",  PropertyPath = "Name")]
		public String Name  {set; get;}  
   
		[Required]
		[Display(Name = "Code", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Code", SearchBy = "Code", OrderBy = "Code",  PropertyPath = "Code")]
		public String Code  {set; get;}  
   
		[Display(Name = "DecisionAuthority", ResourceType = typeof(msg_SanctionCategory))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "DecisionAuthority", SearchBy = "DecisionAuthority", OrderBy = "DecisionAuthority",  PropertyPath = "DecisionAuthority")]
		public DecisionsAuthorities DecisionAuthority  {set; get;}  
   
		[Display(Name = "WorkflowOrder", ResourceType = typeof(msg_SanctionCategory))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "WorkflowOrder", SearchBy = "WorkflowOrder", OrderBy = "WorkflowOrder",  PropertyPath = "WorkflowOrder")]
		public Int32 WorkflowOrder  {set; get;}  
   
		[Display(Name = "Number_Of_Days_Of_Exclusion", ResourceType = typeof(msg_SanctionCategory))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Number_Of_Days_Of_Exclusion", SearchBy = "Number_Of_Days_Of_Exclusion", OrderBy = "Number_Of_Days_Of_Exclusion",  PropertyPath = "Number_Of_Days_Of_Exclusion")]
		public Int32 Number_Of_Days_Of_Exclusion  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  PropertyPath = "Description")]
		public String Description  {set; get;}  
   
    }
}    