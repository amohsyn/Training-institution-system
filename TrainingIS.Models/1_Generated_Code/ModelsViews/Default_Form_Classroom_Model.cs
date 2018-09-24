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
		[Display(Name = "Code", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Code")]
		public String Code  {set; get;}  
   
		[Display(Name = "Name", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Name")]
		public String Name  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_ClassroomCategory))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "ClassroomCategoryId")]
		public Int64 ClassroomCategoryId  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Description")]
		public String Description  {set; get;}  
   
    }
}    
