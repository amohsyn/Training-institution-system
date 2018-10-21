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
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_TrainingType))]
		[GAppDataTable(PropertyPath = "TrainingTypeId", FilterBy = "TrainingTypeId", SearchBy = "TrainingTypeId", OrderBy = "TrainingTypeId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 TrainingTypeId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_TrainingYear))]
		[GAppDataTable(PropertyPath = "TrainingYearId", FilterBy = "TrainingYearId", SearchBy = "TrainingYearId", OrderBy = "TrainingYearId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 TrainingYearId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Specialty))]
		[GAppDataTable(PropertyPath = "SpecialtyId", FilterBy = "SpecialtyId", SearchBy = "SpecialtyId", OrderBy = "SpecialtyId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 SpecialtyId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_YearStudy))]
		[GAppDataTable(PropertyPath = "YearStudyId", FilterBy = "YearStudyId", SearchBy = "YearStudyId", OrderBy = "YearStudyId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 YearStudyId  {set; get;}  
   
		[Required]
		[Display(Name = "Code", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Code", FilterBy = "Code", SearchBy = "Code", OrderBy = "Code",  AutoGenerateFilter = false,isColumn = true )]
		public String Code  {set; get;}  
   
		[Display(Name = "Description", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Description", FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  AutoGenerateFilter = false,isColumn = true )]
		public String Description  {set; get;}  
   
		[Many (TypeOfEntity = typeof(Trainee))]
		public List<String> Selected_Trainees {set; get;}  
   
    }
}    
