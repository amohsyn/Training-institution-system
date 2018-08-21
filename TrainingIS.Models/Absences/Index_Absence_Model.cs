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
using TrainingIS.Entities.Resources.TraineeResources;
using TrainingIS.Entities.Resources.AbsenceResources;
using TrainingIS.Entities.Resources.SeanceTrainingResources;
using TrainingIS.Entities;
using TrainingIS.Entities.Resources.SeancePlanningResources;
using TrainingIS.Entities.Resources.GroupResources;

namespace TrainingIS.Models.Absences
{
    [IndexView(typeof(Absence))]
    public class Index_Absence_Model : BaseModel
    {
        

        [Display(Name = "SingularName", ResourceType = typeof(msg_Trainee))]
        public Trainee Trainee { set; get; }

        [Display(Name = "SingularName", Order = 40, ResourceType = typeof(msg_Group))]
        public Group Group
        {
            get
            {
                return this.SeancePlanning.Training.Group;
            }
        }

        [Required]
        [Display(Name = "isHaveAuthorization", ResourceType = typeof(msg_Absence))]
        public Boolean isHaveAuthorization { set; get; }


        [Display(Name = "SingularName", ResourceType = typeof(msg_SeancePlanning))]
        public SeancePlanning SeancePlanning { set; get; }

       
    }
}
