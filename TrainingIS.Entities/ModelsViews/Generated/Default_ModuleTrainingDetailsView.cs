using GApp.Core.Entities.ModelsViews;
using System; 
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TrainingIS.Entities.Resources.AppResources;
using GApp.Core.MetaDatas.Attributes; 
using TrainingIS.Entities.Resources.SpecialtyResources; 
using TrainingIS.Entities.Resources.AppResources; 

namespace TrainingIS.Entities.ModelsViews
{
    
    public class Default_ModuleTrainingDetailsView : BaseModelView
    {
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_Specialty))]
		public Int64 SpecialtyId  {set; get;}  
   
		[Required]
		[Display(Name = "Name", ResourceType = typeof(msg_app))]
		public String Name  {set; get;}  
   
		[Display(Name = "Code", ResourceType = typeof(msg_app))]
		public String Code  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		public String Description  {set; get;}  
   
    }
}
