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
using TrainingIS.Entities.Resources.AbsenceResources; 
using TrainingIS.Entities.Resources.SeanceTrainingResources; 
using TrainingIS.Entities.Resources.TraineeResources; 
 

namespace TrainingIS.Entities.ModelsViews
{
	[DetailsView(typeof(Absence))]
	[IndexView(typeof(Absence))]
    public class Default_Details_Absence_Model : BaseModel
    {
		[Filter]
		[Required]
		[Display(Name = "AbsenceDate", Order = 0, ResourceType = typeof(msg_Absence))]
		[GAppDataTable(PropertyPath = "AbsenceDate", FilterBy = "AbsenceDate", SearchBy = "AbsenceDate", OrderBy = "AbsenceDate",  AutoGenerateFilter = true,isColumn = true )]
		[DataType(DataType.Date)]
		public DateTime AbsenceDate  {set; get;}  
   
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_SeanceTraining))]
		[GAppDataTable(PropertyPath = "SeanceTraining", FilterBy = "SeanceTraining.Id", SearchBy = "SeanceTraining", OrderBy = "SeanceTraining",  AutoGenerateFilter = false,isColumn = true )]
		public SeanceTraining SeanceTraining  {set; get;}  
   
		[SearchBy("Trainee.FirstName")]
		[SearchBy("Trainee.LastName")]
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Trainee))]
		[GAppDataTable(PropertyPath = "Trainee", FilterBy = "Trainee.Id", SearchBy = "Trainee", OrderBy = "Trainee",  AutoGenerateFilter = true,isColumn = true )]
		public Trainee Trainee  {set; get;}  
   
		[Required]
		[Display(Name = "isHaveAuthorization", Order = 0, ResourceType = typeof(msg_Absence))]
		[GAppDataTable(PropertyPath = "isHaveAuthorization", FilterBy = "isHaveAuthorization", SearchBy = "isHaveAuthorization", OrderBy = "isHaveAuthorization",  AutoGenerateFilter = true,isColumn = true )]
		public Boolean isHaveAuthorization  {set; get;}  
   
		[SearchBy("FormerComment")]
		[Display(Name = "FormerComment", Order = 0, ResourceType = typeof(msg_Absence))]
		[GAppDataTable(PropertyPath = "FormerComment", FilterBy = "FormerComment", SearchBy = "FormerComment", OrderBy = "FormerComment",  AutoGenerateFilter = false,isColumn = true )]
		public String FormerComment  {set; get;}  
   
		[SearchBy("TraineeComment")]
		[Display(Name = "TraineeComment", Order = 0, ResourceType = typeof(msg_Absence))]
		[GAppDataTable(PropertyPath = "TraineeComment", FilterBy = "TraineeComment", SearchBy = "TraineeComment", OrderBy = "TraineeComment",  AutoGenerateFilter = false,isColumn = true )]
		public String TraineeComment  {set; get;}  
   
		[SearchBy("SupervisorComment")]
		[Display(Name = "SupervisorComment", Order = 0, ResourceType = typeof(msg_Absence))]
		[GAppDataTable(PropertyPath = "SupervisorComment", FilterBy = "SupervisorComment", SearchBy = "SupervisorComment", OrderBy = "SupervisorComment",  AutoGenerateFilter = false,isColumn = true )]
		public String SupervisorComment  {set; get;}  
   
		[Display(Name = "Valide", Order = 0, ResourceType = typeof(msg_Absence))]
		[GAppDataTable(PropertyPath = "Valide", FilterBy = "Valide", SearchBy = "Valide", OrderBy = "Valide",  AutoGenerateFilter = true,isColumn = true )]
		public Boolean Valide  {set; get;}  
   
    }
}    
