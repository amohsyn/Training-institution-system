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
using TrainingIS.Entities.Resources.SeancePlanningResources;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingIS.Entities 
{
    [EntityMetataData(isMale = false)]
    public class Absence : BaseEntity
    {
        public override string ToString()
        {
            string trainee = this.Trainee?.ToString();
            string group = this.SeanceTraining?.SeancePlanning?.Training?.Group?.ToString();
            string seanceNumber = this.SeanceTraining?.SeancePlanning?.SeanceNumber?.ToString();
            string module = this.SeanceTraining?.SeancePlanning?.Training?.ModuleTraining?.Code;
            return string.Format("{0}-{1} [{2}, {3}, {4}]", trainee, this.AbsenceDate.ToShortDateString(), group, seanceNumber, module);
        }

        public override string CalculateReference()
        {
            string reference = string.Format("{0}-{1}", this.Trainee.Reference, this.SeanceTraining.Reference);
            return reference;
        }

        [Required]
        [Display(Name = "AbsenceDate", AutoGenerateFilter = true, ResourceType = typeof(msg_Absence))]
        [DataType(DataType.Date)]
        [Filter]
        public DateTime AbsenceDate { set; get; }

        // SeanceTrainings
        [Display(Name = "SingularName", ResourceType = typeof(msg_SeanceTraining))]
        public virtual SeanceTraining SeanceTraining { set; get; }
        [Display(Name = "SingularName", ResourceType = typeof(msg_SeanceTraining))]
        [Required]
        public long SeanceTrainingId { set; get; }

        // Trainee
        [SearchBy("Trainee.FirstName")]
        [SearchBy("Trainee.LastName")]
        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_Trainee))]
        public virtual Trainee Trainee { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_Trainee))]
        public long TraineeId { set; get; }

        [Required]
        [Display(Name = "isHaveAuthorization", AutoGenerateFilter = true, ResourceType = typeof(msg_Absence))]
        public bool isHaveAuthorization { set; get; }

        [SearchBy("FormerComment")]
        [Display(Name = "FormerComment", ResourceType = typeof(msg_Absence))]
        public String FormerComment { set; get; }

        [SearchBy("TraineeComment")]
        [Display(Name = "TraineeComment", ResourceType = typeof(msg_Absence))]
        public String TraineeComment { set; get; }

        [SearchBy("SupervisorComment")]
        [Display(Name = "SupervisorComment", ResourceType = typeof(msg_Absence))]
        public String SupervisorComment { set; get; }

        [Display(Name = "Valide", AutoGenerateFilter = true, ResourceType = typeof(msg_Absence))]
        public bool Valide { set; get; }

        // JustificationAbsence
        [Display(AutoGenerateField =false)]
        public virtual JustificationAbsence JustificationAbsence { set; get; }


    }
}
