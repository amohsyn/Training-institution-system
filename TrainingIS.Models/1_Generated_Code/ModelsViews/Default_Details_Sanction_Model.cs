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
 

namespace TrainingIS.Entities.ModelsViews
{
	[DetailsView(typeof(Sanction))]
	[IndexView(typeof(Sanction))]
    public class Default_Details_Sanction_Model : BaseModel
    {
		[SearchBy("Trainee.FirstName")]
		[SearchBy("Trainee.LastName")]
		[Display(Name = "SingularName", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_Trainee))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "Trainee.Id", SearchBy = "Trainee.Reference", OrderBy = "Trainee.Reference",  PropertyPath = "Trainee")]
		public Trainee Trainee  {set; get;}  
   
		[Display(Name = "SingularName", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_SanctionCategory))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "SanctionCategory.Id", SearchBy = "SanctionCategory.Reference", OrderBy = "SanctionCategory.Reference",  PropertyPath = "SanctionCategory")]
		public SanctionCategory SanctionCategory  {set; get;}  
   
		[Display(Name = "SingularName", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_SanctionCategory))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "Meeting.Id", SearchBy = "Meeting.Reference", OrderBy = "Meeting.Reference",  PropertyPath = "Meeting")]
		public Meeting Meeting  {set; get;}  
   
    }
}    
