using GApp.Entities;
using GApp.Entities.Resources.AppResources;
using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Resources.AdministratorResources;
using TrainingIS.Entities.Resources.FormerResources;
using TrainingIS.Entities.Resources.Mission_Working_GroupResources;
using TrainingIS.Entities.Resources.TraineeResources;

namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = false)]
    public class WorkGroup : BaseEntity
    {
        public override string ToString()
        {
            return this.Code;
        }
        public override string CalculateReference()
        {
            string reference = string.Format("{0}", this.Code);
            return reference;
        }

        [Required]
        [Display(Name = "Code", ResourceType = typeof(msg_app))]
        public string Code { get; set; }

        [Required]
        [Display(Name = "Name", ResourceType = typeof(msg_app))]
        public string Name { get; set; }

 
        [Many(userInterfaces = UserInterfaces.MultiSelect)]
        [Display(Name = "PluralName", ResourceType = typeof(msg_Former))]
        public virtual List<Former> Formers { get; set; }

        [Many(userInterfaces = UserInterfaces.MultiSelect)]
        [Display(Name = "PluralName", ResourceType = typeof(msg_Administrator))]
        public virtual List<Administrator> Administrators { get; set; }

        [Many(userInterfaces = UserInterfaces.MultiSelect)]
        [Display(Name = "PluralName", ResourceType = typeof(msg_Trainee))]
        public virtual List<Trainee> Trainees { get; set; }

        [Many(userInterfaces = UserInterfaces.MultiSelect)]
        [Display(Name = "PluralName", ResourceType = typeof(msg_Mission_Working_Group))]
        public virtual List<Mission_Working_Group> Mission_Working_Groups { get; set; }
        



        [Display(Name = "Description", ResourceType = typeof(msg_app))]
        public string Description { set; get; }


    }
}
