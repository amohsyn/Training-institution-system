using GApp.Core.Entities.ModelsViews;
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
	[DetailsView(typeof(JustificationAbsence))]
	[IndexView(typeof(JustificationAbsence))]
	[ExportView(typeof(JustificationAbsence))]
    public class Default_Details_JustificationAbsence_Model : BaseModel
    {
		[SearchBy("Trainee.FirstName")]
		[SearchBy("Trainee.LastName")]
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Trainee))]
		[GAppDataTable(PropertyPath = "Trainee", FilterBy = "Trainee.Id", SearchBy = "Trainee.Reference", OrderBy = "Trainee.Reference",  AutoGenerateFilter = true,isColumn = true )]
		public Trainee Trainee  {set; get;}  
   
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Category_JustificationAbsence))]
		[GAppDataTable(PropertyPath = "Category_JustificationAbsence", FilterBy = "Category_JustificationAbsence.Id", SearchBy = "Category_JustificationAbsence.Reference", OrderBy = "Category_JustificationAbsence.Reference",  AutoGenerateFilter = true,isColumn = true )]
		public Category_JustificationAbsence Category_JustificationAbsence  {set; get;}  
   
		[Required]
		[Display(Name = "StartDate", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "StartDate", FilterBy = "StartDate", SearchBy = "StartDate", OrderBy = "StartDate",  AutoGenerateFilter = false,isColumn = true )]
		[DataType(DataType.DateTime)]
		public DateTime StartDate  {set; get;}  
   
		[Required]
		[Display(Name = "EndtDate", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "EndtDate", FilterBy = "EndtDate", SearchBy = "EndtDate", OrderBy = "EndtDate",  AutoGenerateFilter = false,isColumn = true )]
		[DataType(DataType.DateTime)]
		public DateTime EndtDate  {set; get;}  
   
		[Display(Name = "Description", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Description", FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  AutoGenerateFilter = false,isColumn = true )]
		public String Description  {set; get;}  
   
    }
}    
