using GApp.Core.Entities.ModelsViews;
using System; 
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TrainingIS.Entities.Resources.AppResources;
using TrainingIS.Entities.Resources.AppRoleResources;
using GApp.Core.MetaDatas.Attributes; 
using TrainingIS.Entities.Resources.TrainingTypeResources; 
using TrainingIS.Entities.Resources.TrainingYearResources; 
using TrainingIS.Entities.Resources.SpecialtyResources; 
using TrainingIS.Entities.Resources.YearStudyResources; 
using TrainingIS.Entities.Resources.AppResources; 

namespace TrainingIS.Entities.ModelsViews
{
    
    public class Default_GroupDetailsView : BaseModelView
    {
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_TrainingType))]
		public Int64 TrainingTypeId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_TrainingYear))]
		public Int64 TrainingYearId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_Specialty))]
		public Int64 SpecialtyId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_YearStudy))]
		public Int64 YearStudyId  {set; get;}  
   
		[Required]
		[Display(Name = "Code", ResourceType = typeof(msg_app))]
		public String Code  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		public String Description  {set; get;}  
   
    }
}
