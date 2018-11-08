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
using TrainingIS.Entities.enums;

namespace TrainingIS.Entities 
{
    [EntityMetataData(isMale = false)]
    public class Absence : BaseEntity
    {
        public override string ToString()
        {
            //string trainee = this.Trainee?.ToString();
            //string group = this.SeanceTraining?.SeancePlanning?.Training?.Group?.ToString();
            //string seanceNumber = this.SeanceTraining?.SeancePlanning?.SeanceNumber?.ToString();
            //string module = this.SeanceTraining?.SeancePlanning?.Training?.ModuleTraining?.Code;
            // return string.Format("{0}-{1} [{2}, {3}, {4}]", trainee, this.AbsenceDate.ToShortDateString(), group, seanceNumber, module);
            return this.AbsenceDate.ToShortDateString(); ;
        }

        public override string CalculateReference()
        {
            string reference = string.Format("{0}-{1}", this.Trainee.Reference, this.SeanceTraining.Reference);
            return reference;
        }

        //
        // SeanceTraining
        //

        [Required]
        [Display(Name = "AbsenceDate", GroupName = "SeanceTraining", Order =1, AutoGenerateFilter = true, ResourceType = typeof(msg_Absence))]
        [DataType(DataType.Date)]
        [Filter]
        public DateTime AbsenceDate { set; get; }


        // SeanceTrainings
        [Display(Name = "SingularName", GroupName = "SeanceTraining", Order =2 , ResourceType = typeof(msg_SeanceTraining))]
        public virtual SeanceTraining SeanceTraining { set; get; }
        [Display(Name = "SingularName", GroupName = "SeanceTraining", Order = 2, ResourceType = typeof(msg_SeanceTraining))]
        [Required]
        public long SeanceTrainingId { set; get; }

        //
        // Trainee
        //
        // Trainee
        [SearchBy("Trainee.FirstName")]
        [SearchBy("Trainee.LastName")]
        [Display(Name = "SingularName", GroupName = "Trainee", Order = 1,  AutoGenerateFilter = true, ResourceType = typeof(msg_Trainee))]
        public virtual Trainee Trainee { set; get; }
        [Required]
        [Display(Name = "SingularName", GroupName = "Trainee", Order = 2,  ResourceType = typeof(msg_Trainee))]
        public long TraineeId { set; get; }



        //
        // Comments
        //
        [SearchBy("FormerComment")]
        [Display(Name = "FormerComment", GroupName = "Comments", Order = 1, ResourceType = typeof(msg_Absence))]
        public String FormerComment { set; get; }

        [SearchBy("TraineeComment")]
        [Display(Name = "TraineeComment", GroupName = "Comments", Order = 1, ResourceType = typeof(msg_Absence))]
        public String TraineeComment { set; get; }

        [SearchBy("SupervisorComment")]
        [Display(Name = "SupervisorComment", GroupName = "Comments", Order = 1, ResourceType = typeof(msg_Absence))]
        public String SupervisorComment { set; get; }

        //
        // States
        // 
        [Obsolete("Use AbsenceState ")]
        [Required]
        [Display(Name = "isHaveAuthorization", GroupName = "States", Order = 1, AutoGenerateFilter = true, ResourceType = typeof(msg_Absence))]
        public bool isHaveAuthorization { set; get; }

        [Obsolete("Use AbsenceState ")]
        [Display(Name = "Valide", GroupName = "States", Order = 2, AutoGenerateFilter = true, ResourceType = typeof(msg_Absence))]
        public bool Valide { set; get; }

        [Display(Name = "AbsenceState", GroupName = "States", Order = 3, AutoGenerateFilter = true, ResourceType = typeof(msg_Absence))]
        public AbsenceStates AbsenceState { set; get; }

        // JustificationAbsence
        [Display(AutoGenerateField =false)]
        public virtual JustificationAbsence JustificationAbsence { set; get; }

        [Display(AutoGenerateField = false)]
        public virtual Sanction Sanction { set; get; }

       

    }
}
