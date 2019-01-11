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
using TrainingIS.Entities.Resources.SanctionCategoryResources; 
using TrainingIS.Entities.Resources.SanctionResources; 
using TrainingIS.Entities.Resources.MeetingResources; 
using TrainingIS.Entities.Resources.JustificationAbsenceResources; 
 

namespace TrainingIS.Entities.ModelsViews
{
	[IndexView(typeof(Sanction))]
	[SearchBy("Reference")]
    public class Default_Sanction_Index_Model : BaseModel
    {
		[SearchBy("Trainee.FirstName")]
		[SearchBy("Trainee.LastName")]
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Trainee))]
		[GAppDataTable(PropertyPath = "Trainee", FilterBy = "Trainee.Id", SearchBy = "Trainee.Reference", OrderBy = "Trainee.Reference",  AutoGenerateFilter = true,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public Trainee Trainee  {set; get;}  
   
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_SanctionCategory))]
		[GAppDataTable(PropertyPath = "SanctionCategory", FilterBy = "SanctionCategory.Id", SearchBy = "SanctionCategory.Reference", OrderBy = "SanctionCategory.Reference",  AutoGenerateFilter = true,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public SanctionCategory SanctionCategory  {set; get;}  
   
		[Display(Name = "SanctionState", Order = 0, ResourceType = typeof(msg_Sanction))]
		[GAppDataTable(PropertyPath = "SanctionState", FilterBy = "SanctionState", SearchBy = "SanctionState", OrderBy = "SanctionState",  AutoGenerateFilter = true,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public SanctionStates SanctionState  {set; get;}  
   
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "Meeting", FilterBy = "Meeting.Id", SearchBy = "Meeting.Reference", OrderBy = "Meeting.Reference",  AutoGenerateFilter = true,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public Meeting Meeting  {set; get;}  
   
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_JustificationAbsence))]
		[GAppDataTable(PropertyPath = "JustificationAbsence", FilterBy = "JustificationAbsence.Id", SearchBy = "JustificationAbsence.Reference", OrderBy = "JustificationAbsence.Reference",  AutoGenerateFilter = true,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public JustificationAbsence JustificationAbsence  {set; get;}  
   
    }
}    
