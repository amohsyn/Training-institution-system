using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Ressources.AppRessources;
using TrainingIS.Entities.Ressources.SpecialtyRessources;
using TrainingIS.Entities.Ressources.TrainingYearRessources;

namespace TrainingIS.Entities
{
    public class Group : BaseEntity
    {
        [Display(Name = "Code", ResourceType = typeof(msg_app))]
        public string Code { get; set; }

        [Display(Name = "Name", ResourceType = typeof(msg_app))]
        public string Name { set; get; }

        [Display(Name = "Description", ResourceType = typeof(msg_app))]
        public string Description { set; get; }

        [Display(Name = "SingularName", ResourceType = typeof(msg_Specialty))]
        public virtual Specialty Specialty { set; get; }
        public long? SpecialtyId { set; get; }

        [Display(Name = "SingularName", ResourceType = typeof(msg_TrainingYear))]
        public virtual TrainingYear TrainingYear { set; get; }
        public long? TrainingYearId { set; get; }
    }
}
