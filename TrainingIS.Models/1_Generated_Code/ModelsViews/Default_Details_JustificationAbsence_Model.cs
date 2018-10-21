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
    public class Default_Details_JustificationAbsence_Model : BaseModel
    {
		[SearchBy("Trainee.FirstName")]
		[SearchBy("Trainee.LastName")]
		[Display(Name = "SingularName", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_Trainee))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "Trainee.Id", SearchBy = "Trainee.Reference", OrderBy = "Trainee.Reference",  PropertyPath = "Trainee")]
		public Trainee Trainee  {set; get;}  
   
		[Display(Name = "SingularName", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_Category_JustificationAbsence))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "Category_JustificationAbsence.Id", SearchBy = "Category_JustificationAbsence.Reference", OrderBy = "Category_JustificationAbsence.Reference",  PropertyPath = "Category_JustificationAbsence")]
		public Category_JustificationAbsence Category_JustificationAbsence  {set; get;}  
   
		[Required]
		[Display(Name = "StartDate", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "StartDate", SearchBy = "StartDate", OrderBy = "StartDate",  PropertyPath = "StartDate")]
		[DataType(DataType.DateTime)]
		public DateTime StartDate  {set; get;}  
   
		[Required]
		[Display(Name = "EndtDate", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "EndtDate", SearchBy = "EndtDate", OrderBy = "EndtDate",  PropertyPath = "EndtDate")]
		[DataType(DataType.DateTime)]
		public DateTime EndtDate  {set; get;}  
   
		[Display(Name = "Description", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  PropertyPath = "Description")]
		public String Description  {set; get;}  
   
    }
}    
