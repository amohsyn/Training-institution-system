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
using TrainingIS.Entities.Resources.TraineeResources;  
using TrainingIS.Entities.Resources.Category_JustificationAbsenceResources;  
using GApp.Entities.Resources.AppResources;  
 
namespace TrainingIS.Entities.ModelsViews
{
	[EditView(typeof(JustificationAbsence))]
	[CreateView(typeof(JustificationAbsence))]
    public class Default_Form_JustificationAbsence_Model : BaseModel
    {
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_Trainee))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "TraineeId", SearchBy = "TraineeId", OrderBy = "TraineeId",  PropertyPath = "TraineeId")]
		public Int64 TraineeId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_Category_JustificationAbsence))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Category_JustificationAbsenceId", SearchBy = "Category_JustificationAbsenceId", OrderBy = "Category_JustificationAbsenceId",  PropertyPath = "Category_JustificationAbsenceId")]
		public Int64 Category_JustificationAbsenceId  {set; get;}  
   
		[Required]
		[Display(Name = "StartDate", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "StartDate", SearchBy = "StartDate", OrderBy = "StartDate",  PropertyPath = "StartDate")]
		[DataType(DataType.DateTime)]
		public DateTime StartDate  {set; get;}  
   
		[Required]
		[Display(Name = "EndtDate", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "EndtDate", SearchBy = "EndtDate", OrderBy = "EndtDate",  PropertyPath = "EndtDate")]
		[DataType(DataType.DateTime)]
		public DateTime EndtDate  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  PropertyPath = "Description")]
		public String Description  {set; get;}  
   
    }
}    