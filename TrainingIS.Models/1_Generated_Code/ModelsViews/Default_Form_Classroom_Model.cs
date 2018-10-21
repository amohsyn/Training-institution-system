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
	[EditView(typeof(Classroom))]
	[CreateView(typeof(Classroom))]
    public class Default_Form_Classroom_Model : BaseModel
    {
		[Required]
		[Unique]
		[Display(Name = "Code", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Code", SearchBy = "Code", OrderBy = "Code",  PropertyPath = "Code")]
		public String Code  {set; get;}  
   
		[Display(Name = "Name", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Name", SearchBy = "Name", OrderBy = "Name",  PropertyPath = "Name")]
		public String Name  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_ClassroomCategory))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "ClassroomCategoryId", SearchBy = "ClassroomCategoryId", OrderBy = "ClassroomCategoryId",  PropertyPath = "ClassroomCategoryId")]
		public Int64 ClassroomCategoryId  {set; get;}  
   
		[Display(Name = "Description", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  PropertyPath = "Description")]
		public String Description  {set; get;}  
   
    }
}    
