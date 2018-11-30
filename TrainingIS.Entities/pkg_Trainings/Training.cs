using GApp.Models.DataAnnotations;
using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.Entities.Resources.AppResources;
using TrainingIS.Entities.Resources.FormerResources;
using TrainingIS.Entities.Resources.GroupResources;
using TrainingIS.Entities.Resources.ModuleTrainingResources;
using TrainingIS.Entities.Resources.TrainingYearResources;
using TrainingIS.Entities.Base;


namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = false)]
    public class Training : BaseEntity
    {
        public override string ToString()
        {
            string reference = string.Format("{0}-{1}-{2}-[{3}]",
                this.Group?.Code,
                this.ModuleTraining?.Code,
                this.Former?.ToString(),
                this.TrainingYear?.Reference
                );
            return reference;
        }
        public override string CalculateReference()
        {
            string reference = string.Format("{0}-{1}-{2}",
                this.ModuleTraining.Reference,
                this.Former.Reference,
                this.Group.Reference,
                this.TrainingYear.Reference
                );
            return reference;
        }

        // TrainingYear
        [Display(Name = "SingularName", ResourceType = typeof(msg_TrainingYear))]
        public virtual TrainingYear TrainingYear { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_TrainingYear))]
        public long TrainingYearId { set; get; }

        // Module
        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_ModuleTraining))]

        public virtual ModuleTraining ModuleTraining { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_ModuleTraining))]
        public long ModuleTrainingId { set; get; }

        [Display(Name = "Hourly_Mass_To_Teach", ResourceType = typeof(msg_ModuleTraining))]
        public float Hourly_Mass_To_Teach { get; set; }

        // Former
        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_Former))]
        public virtual Former Former { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_Former))]
        public long FormerId { set; get; }

        // Groupe
        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_Group))]
        public virtual Group Group { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_Group))]
        public long GroupId { set; get; }

        [Display(Name = "Code", ResourceType = typeof(msg_app))]
        public string Code { get; set; }

        [Display(Name = "Description", ResourceType = typeof(msg_app))]
        public string Description { set; get; }


    }
}
