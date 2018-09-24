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
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_TrainingType))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "TrainingTypeId")]
		public Int64 TrainingTypeId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_TrainingYear))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "TrainingYearId")]
		public Int64 TrainingYearId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_Specialty))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "SpecialtyId")]
		public Int64 SpecialtyId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_YearStudy))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "YearStudyId")]
		public Int64 YearStudyId  {set; get;}  
   
		[Required]
		[Display(Name = "Code", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Code")]
		public String Code  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Description")]
		public String Description  {set; get;}  
   
		[Many (TypeOfEntity = typeof(Trainee))]
		public List<String> Selected_Trainees {set; get;}  
   
    }
}    
