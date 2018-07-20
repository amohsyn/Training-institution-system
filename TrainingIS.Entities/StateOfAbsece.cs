using GApp.Core.MetaDatas.Attributes;
using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Resources.AppResources;
using TrainingIS.Entities.Resources.StateOfAbseceResources;
using TrainingIS.Entities.Resources.TraineeResources;

namespace TrainingIS.Entities
{


    [EntityMetataData(isMale = true)]
    public class StateOfAbsece : BaseEntity
    {

        public enum Categories { Year,Month,Module}

        public override string ToString()
        {
            return this.Reference;
        }
        public override string CalculateReference()
        {
            string reference = string.Format("{0}-{1}-{1}", this.Name, this.Category,this.Value);
            return reference;
        }

        [Required]
        [Display(Name = "Name", ResourceType = typeof(msg_app))]
        public String Name { set; get; }

        [Required]
        [Display(Name = "Category", ResourceType = typeof(msg_StateOfAbsece))]
        public Categories Category { set; get; }

        [Required]
        [Display(Name = "Value", ResourceType = typeof(msg_app))]
        public int Value { set; get; }

        // Trainee
        [Display(Name = "SingularName", ResourceType = typeof(msg_Trainee))]
        public virtual Trainee Trainee { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_Trainee))]
        public long TraineeId { set; get; }
    }
}
