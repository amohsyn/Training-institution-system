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
using GApp.Entities.Resources.AppResources;

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
        [Display(Name = "SeanceDate", AutoGenerateFilter = true, ResourceType = typeof(msg_SeanceTraining))]
        [DataType(DataType.Date)]
        public DateTime? SeanceDate { set; get; }

        // SeancePlanning
        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_SeancePlanning))]
        public virtual SeancePlanning SeancePlanning { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_SeancePlanning))]
        public long SeancePlanningId { set; get; }

        // Duration in minute
        // the information must be persisted Because the SeanceNumber can be changed after
        // the seanceTraining creation
        [Display(AutoGenerateField =false)]
        public int Duration { set; get; }

        [Display(AutoGenerateField = false)]
        public int Plurality { set; get; }


        [Display(Name = "Contained", ResourceType = typeof(msg_SeanceTraining))]
        public string Contained { set; get; }

        [Display(Name = "FormerValidation", AutoGenerateFilter = true, ResourceType = typeof(msg_SeanceTraining))]
        public bool FormerValidation { set; get; }

        [Display(AutoGenerateFilter = false)]
        public virtual List<Absence> Absences { set; get; }



        public string GetList_Absents_Trainees()
        {
            string result = string.Join(",", this.Absences.Select(a => a.Trainee.GetFullName()).ToList());
            return result;
        }




    }
}
