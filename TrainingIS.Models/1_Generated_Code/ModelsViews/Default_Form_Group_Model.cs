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
using TrainingIS.Entities.Resources.TrainingTypeResources;  
using TrainingIS.Entities.Resources.TrainingYearResources;  
using TrainingIS.Entities.Resources.SpecialtyResources;  
using TrainingIS.Entities.Resources.YearStudyResources;  
using GApp.Entities.Resources.AppResources;  
 
namespace TrainingIS.Entities.ModelsViews
{
	[EditView(typeof(Group))]
	[CreateView(typeof(Group))]
    public class Default_Form_Group_Model : BaseModel
    {
		[Display(Name = "SingularName", ResourceType = typeof(msg_TrainingType))]
		public TrainingType TrainingType  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_TrainingYear))]
		public TrainingYear TrainingYear  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_Specialty))]
		public Specialty Specialty  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_YearStudy))]
		public YearStudy YearStudy  {set; get;}  
   
		[Required]
		[Display(Name = "Code", ResourceType = typeof(msg_app))]
		public String Code  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		public String Description  {set; get;}  
   
    }
}    
