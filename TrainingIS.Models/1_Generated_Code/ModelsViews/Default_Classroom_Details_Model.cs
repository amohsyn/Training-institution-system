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
using GApp.Entities.Resources.AppResources; 
using TrainingIS.Entities.Resources.ClassroomCategoryResources; 
 

namespace TrainingIS.Entities.ModelsViews
{
	[DetailsView(typeof(Classroom))]
    public class Default_Classroom_Details_Model : BaseModel
    {
		[Required]
		[Unique]
		[Display(Name = "Code", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Code", FilterBy = "Code", SearchBy = "Code", OrderBy = "Code",  AutoGenerateFilter = false,isColumn = true )]
		public String Code  {set; get;}  
   
		[Display(Name = "Name", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Name", FilterBy = "Name", SearchBy = "Name", OrderBy = "Name",  AutoGenerateFilter = false,isColumn = true )]
		public String Name  {set; get;}  
   
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_ClassroomCategory))]
		[GAppDataTable(PropertyPath = "ClassroomCategory", FilterBy = "ClassroomCategory.Id", SearchBy = "ClassroomCategory.Reference", OrderBy = "ClassroomCategory.Reference",  AutoGenerateFilter = false,isColumn = true )]
		public ClassroomCategory ClassroomCategory  {set; get;}  
   
		[Display(Name = "Description", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Description", FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  AutoGenerateFilter = false,isColumn = true )]
		public String Description  {set; get;}  
   
    }
}    
