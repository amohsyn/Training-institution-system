using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Resources.AppResources;

namespace TrainingIS.Entities.Base
{
    /// <summary>
    /// Base entity of the entities with not complet reference : that it is missing
    /// the TrainingYear.Reference at the ent
    /// </summary>
    public class NotCompleteReferenceEntity : BaseEntity
    {
        [Required]
        [Display(Name = "Code", ResourceType = typeof(msg_app))]
        public string Code { get; set; }

        [Display(Name = "Description", ResourceType = typeof(msg_app))]
        public string Description { set; get; }
    }
}
