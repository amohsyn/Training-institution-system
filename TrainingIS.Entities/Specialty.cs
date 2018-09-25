using GApp.Models.DataAnnotations;
using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using GApp.Entities.Resources.AppResources;
using TrainingIS.Entities.Resources.SpecialtyResources;
using TrainingIS.Entities.Base;
using TrainingIS.Entities.Resources.TrainingLevelResources;
using TrainingIS.Entities.Resources.SectorResources;

namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = false)]
    public class Specialty : BaseEntity
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

        

        // TrainingLevel
        [Display(Name = "SingularName", AutoGenerateFilter =true, ResourceType = typeof(msg_Sector))]
        public virtual Sector Sector { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_Sector))]
        public long SectorId { set; get; }

        // TrainingLevel
        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_TrainingLevel))]
        public virtual TrainingLevel TrainingLevel { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_TrainingLevel))]
        public long TrainingLevelId { set; get; }


        [Unique]
        [Required]
        [Display(Name = "Code", ResourceType = typeof(msg_app))]
        public string Code { get; set; }

        [Required]
        [Display(Name = "Name", ResourceType = typeof(msg_app))]
        public string Name { set; get; }

        [Display(Name = "Description", ResourceType = typeof(msg_app))]
        public string Description { set; get; }

       
    }
   
}