using GApp.Models.DataAnnotations;
using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.Entities.Resources.AppResources;
using TrainingIS.Entities.Resources.StateOfAbseceResources;
using TrainingIS.Entities.Resources.TraineeResources;
using TrainingIS.Entities.Base;
namespace TrainingIS.Entities
{
    public enum StateOfAbseceCategories { TrainingYear, Month, Week, DayOfWeek, Module }

    [EntityMetataData(isMale = true)]
    public class StateOfAbsece : BaseEntity
    {
        public override string ToString()
        {
            string reference = string.Format("{0}-{1}-{2}", this.Category, this.Name, this.Value);
            return reference;
        }
        public override string CalculateReference()
        {
            string reference = string.Format("{0}-{1}-{2}-{3}", this.Category,this.Name,this.Trainee.Reference,this.Trainee.Group.TrainingYear.Reference);
            return reference;
        }

        [Required]
        [Display(Name = "Name", ResourceType = typeof(msg_app))]
        public String Name { set; get; }

        [Required]
        [Display(Name = "Category", ResourceType = typeof(msg_StateOfAbsece))]
        public StateOfAbseceCategories Category { set; get; }

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
