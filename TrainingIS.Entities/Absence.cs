using GApp.Models.DataAnnotations;
using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Resources.AbsenceResources;
using TrainingIS.Entities.Resources.SeanceTrainingResources;
using TrainingIS.Entities.Resources.TraineeResources;
using TrainingIS.Entities.Base;
namespace TrainingIS.Entities 
{
    [EntityMetataData(isMale = true)]
    public class Absence : BaseEntity
    {
        public override string ToString()
        {
            return this.Reference;
        }

        public override string CalculateReference()
        {
            string reference = string.Format("{0}-{1}", this.Trainee.Reference, this.SeanceTraining.Reference);
            return reference;
        }

        // Trainee
        [Display(Name = "SingularName", ResourceType = typeof(msg_Trainee))]
        public virtual Trainee Trainee { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_Trainee))]
        public long TraineeId { set; get; }



        [Required]
        [Display(Name = "isHaveAuthorization", ResourceType = typeof(msg_Absence))]
        public bool isHaveAuthorization { set; get; }

        // SeanceTraining
        [Display(Name = "SingularName", ResourceType = typeof(msg_SeanceTraining))]
        public virtual SeanceTraining SeanceTraining { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_SeanceTraining))]
        public long SeanceTrainingId { set; get; }

 
        [Display(Name = "FormerComment", ResourceType = typeof(msg_Absence))]
        public String FormerComment { set; get; }

        [Display(Name = "TraineeComment", ResourceType = typeof(msg_Absence))]
        public String TraineeComment { set; get; }

        [Display(Name = "SupervisorComment", ResourceType = typeof(msg_Absence))]
        public String SupervisorComment { set; get; }
        
    }
}
