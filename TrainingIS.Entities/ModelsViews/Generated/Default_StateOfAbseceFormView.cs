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

namespace TrainingIS.Entities.ModelsViews
{
    
    public class Default_StateOfAbseceFormView : BaseModelView
    {
		[Required]
		public String Name  {set; get;}  
   
		[Required]
		public StateOfAbseceCategories Category  {set; get;}  
   
		[Required]
		public Int32 Value  {set; get;}  
   
		[Required]
		public Int64 TraineeId  {set; get;}  
   
    }
}
