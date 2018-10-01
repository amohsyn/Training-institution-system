using GApp.Entities;
using GApp.Entities.Resources.AppResources;
using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entitie_excludes;
using TrainingIS.Entities.Resources.TaskProjectResources;

namespace TrainingIS.Entities.Base
{
    public class BaseTaskProject : BaseEntity
    {
        [Required]
        [Display(AutoGenerateField = false)]
        public virtual ApplicationUser Owner { set; get; }

        [Required]
        [Display(Name = "Name", ResourceType = typeof(msg_app))]
        public string Name { get; set; }


        [Display(Name = "Description", ResourceType = typeof(msg_app))]
        public string Description { set; get; }


        [Display(Name = "StartDate", ResourceType = typeof(msg_TaskProject))]
        public DateTime StartDate { set; get; }

    
        [Display(Name = "EndtDate", ResourceType = typeof(msg_TaskProject))]
        public DateTime EndtDate { set; get; }

        [Display(Name = "isPublic", ResourceType = typeof(msg_app))]
        public bool isPublic { set; get; }
    }
}
