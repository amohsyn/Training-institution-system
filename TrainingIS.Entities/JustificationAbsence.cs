using GApp.Entities;
using GApp.Entities.Resources.AppResources;
using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Resources.Category_JustificationAbsenceResources;
using TrainingIS.Entities.Resources.TraineeResources;

namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = false)]
    public class JustificationAbsence : BaseEntity
    {
        public override string ToString()
        {
            string reference = string.Format("{0}-{1}", this.Trainee, this.Category_JustificationAbsence);
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

        // Category_JustificationAbsence
        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_Category_JustificationAbsence))]
        public virtual Category_JustificationAbsence Category_JustificationAbsence { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_Category_JustificationAbsence))]
        public long Category_JustificationAbsenceId { set; get; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "StartDate", ResourceType = typeof(msg_app))]
        public DateTime StartDate { set; get; }
 
        [Required]
        [Display(Name = "EndtDate", ResourceType = typeof(msg_app))]
        public DateTime EndtDate { set; get; }

        [RichText]
        [Display(Name = "Description", ResourceType = typeof(msg_app))]
        public string Description { set; get; }

  
    }
}
