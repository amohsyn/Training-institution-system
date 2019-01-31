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
using TrainingIS.Entities.Resources.TrainingResources;

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

        #region Group
        // TrainingYear
        [Display(Name = "TrainingYear", GroupName ="Group", ResourceType = typeof(msg_Training))]
        public virtual TrainingYear TrainingYear { set; get; }
        [Required]
        [Display(Name = "TrainingYear", GroupName = "Group", ResourceType = typeof(msg_Training))]
        public long TrainingYearId { set; get; }

        // Groupe
        [Display(Name = "Group", GroupName = "Group", AutoGenerateFilter = true, ResourceType = typeof(msg_Training))]
        public virtual Group Group { set; get; }
        [Required]
        [Display(Name = "Group", GroupName = "Group", ResourceType = typeof(msg_Training))]
        public long GroupId { set; get; }
        #endregion

        #region Module 
        // Module
        [Display(Name = "ModuleTraining", GroupName = "Module", AutoGenerateFilter = true, ResourceType = typeof(msg_Training))]
        public virtual ModuleTraining ModuleTraining { set; get; }
        [Required]
        [Display(Name = "ModuleTraining", GroupName = "Module", ResourceType = typeof(msg_Training))]
        public long ModuleTrainingId { set; get; }

        [Display(Name = "Hourly_Mass_To_Teach", GroupName = "Module", ResourceType = typeof(msg_Training))]
        [GAppDataTable(PropertyPath = "Hourly_Mass_To_Teach", FilterBy = "Hourly_Mass_To_Teach", isSeachBy = false, OrderBy = "Hourly_Mass_To_Teach", AutoGenerateFilter = false, isColumn = true)]
        public float Hourly_Mass_To_Teach { get; set; }
        #endregion


        #region Former
        // Former
        [Display(Name = "Former", GroupName = "Former", AutoGenerateFilter = true, ResourceType = typeof(msg_Training))]
        public virtual Former Former { set; get; }
        [Required]
        [Display(Name = "Former", GroupName = "Former", ResourceType = typeof(msg_Training))]
        public long FormerId { set; get; }
        #endregion
 
       
        // To Delete
        [Display(Name = "Code", AutoGenerateField =false, ResourceType = typeof(msg_app))]
        public string Code { get; set; }

        [Display(Name = "Description", AutoGenerateField =false, ResourceType = typeof(msg_app))]
        public string Description { set; get; }


    }
}
