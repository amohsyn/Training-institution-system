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
using TrainingIS.Entities.Resources.AppResources; 

namespace TrainingIS.Entities.ModelsViews
{
    
    public class Default_TrainingTypeDetailsView : BaseModelView
    {
		[Required]
		[Display(Name = "Code", ResourceType = typeof(msg_app))]
		public String Code  {set; get;}  
   
		[Required]
		[Display(Name = "Name", ResourceType = typeof(msg_app))]
		public String Name  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		public String Description  {set; get;}  
   
    }
}
