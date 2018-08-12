using GApp.Models.DataAnnotations;
using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Resources.SeancePlanningResources;
using TrainingIS.Entities.Resources.SeanceTrainingResources;
using TrainingIS.Entities.Base;
namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = false)]
    public class SeanceTraining : BaseEntity
    {
        public override string ToString()
        {
            return this.Reference;
        }
        public override string CalculateReference()
        {
            string reference = string.Format("{0}-{1}", this.SeancePlanning,this.SeanceDate);
            return reference;
        }

        [Required]
        [Display(Name = "SeanceDate", ResourceType = typeof(msg_SeanceTraining))]
        public DateTime? SeanceDate { set; get; }

        // SeancePlanning
        [Display(Name = "SingularName", ResourceType = typeof(msg_SeancePlanning))]
        public virtual SeancePlanning SeancePlanning { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_SeancePlanning))]
        public long SeancePlanningId { set; get; }


    }
}
