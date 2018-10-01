using GApp.Entities.Resources.AppResources;
using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Resources.Category_WarningTraineeResources;
using TrainingIS.Entities.Resources.TraineeResources;
using TrainingIS.Entities.Resources.WarningTraineeResources;

namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = false)]
    public class WarningTrainee
    {
        public override string ToString()
        {
            string reference = string.Format("{0}-{1}", this.Trainee, this.Category_WarningTrainee);
            return reference;
        }

        // Trainee
        [SearchBy("Trainee.FirstName")]
        [SearchBy("Trainee.LastName")]
        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_Trainee))]
        public virtual Trainee Trainee { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_Trainee))]
        public long TraineeId { set; get; }


        [Required]
        [Display(Name = "WarningDate", ResourceType = typeof(msg_WarningTrainee))]
        public DateTime WarningDate { set; get; }

        // Category_JustificationAbsence
        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_Category_WarningTrainee))]
        public virtual Category_WarningTrainee Category_WarningTrainee { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_Category_WarningTrainee))]
        public long Category_WarningTraineeId { set; get; }

        [Display(Name = "Description", ResourceType = typeof(msg_app))]
        public string Description { set; get; }
    }
}
