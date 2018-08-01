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
    
    public class Default_AbsenceFormView : BaseModelView
    {
		[Required]
		public Int64 TraineeId  {set; get;}  
   
		[Required]
		public Boolean isHaveAuthorization  {set; get;}  
   
		[Required]
		public Int64 SeanceTrainingId  {set; get;}  
   
		public String FormerComment  {set; get;}  
   
		public String TraineeComment  {set; get;}  
   
		public String SupervisorComment  {set; get;}  
   
    }
}
