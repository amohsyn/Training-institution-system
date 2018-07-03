using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Resources.AppResources;
using TrainingIS.Entities.Resources.GroupResources;
using TrainingIS.Entities.Resources.SpecialtyResources;
using TrainingIS.Entities.Resources.TrainingTypeResources;
using TrainingIS.Entities.Resources.TrainingYearResources;

namespace TrainingIS.Entities
{
    public class Group : BaseEntity
    {
        public override string ToString()
        {
            return this.Code;
        }
        [Display(Name = "SingularName", ResourceType = typeof(msg_TrainingType))]
        public TrainingType TrainingType { set; get; }
        public long TrainingTypeId { set; get; }

        [Display(Name = "SingularName", ResourceType = typeof(msg_TrainingYear))]
        [DisplayName("StartDate")]
        public virtual TrainingYear TrainingYear { set; get; }
        public long? TrainingYearId { set; get; }

        [Display(Name = "SingularName", ResourceType = typeof(msg_Specialty))]
        public virtual Specialty Specialty { set; get; }
        public long? SpecialtyId { set; get; }

        [Display(Name = "Year", ResourceType = typeof(msg_Group))]
        public int Year { set; get; }

        [Display(Name = "Code", ResourceType = typeof(msg_app))]
        public string Code { get; set; }

        [Display(Name = "Number", ResourceType = typeof(msg_Group))]
        public string Number { set; get; }

        [Display(Name = "Description", ResourceType = typeof(msg_app))]
        public string Description { set; get; }

    }
}
