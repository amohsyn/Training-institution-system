using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using TrainingIS.Entities.Resources.GroupResources;
using TrainingIS.Entities.Resources.TraineeResources;

namespace TrainingIS.Entities
{
    public class Trainee : BaseEntity
    {
        // civil status
        [Display(Name = "FirstName", ResourceType = typeof(msg_Trainee))]
        public string FirstName { set; get; }

        [Display(Name = "LastName", ResourceType = typeof(msg_Trainee))]
        public string LastName { set; get; }

        [Display(Name = "Sex", ResourceType = typeof(msg_Trainee))]
        public bool Sex { set; get; }

        [Display(Name = "CIN", ResourceType = typeof(msg_Trainee))]
        public string CIN { set; get; }

        [Display(Name = "CNE", ResourceType = typeof(msg_Trainee))]
        public string CNE { set; get; }

        // Contact information
        [Display(Name = "Cellphone", ResourceType = typeof(msg_Trainee))]
        public string Cellphone { set; get; }

        [Display(Name = "Email", ResourceType = typeof(msg_Trainee))]
        public string Email { set; get; }

        [Display(Name = "Address", ResourceType = typeof(msg_Trainee))]
        public string Address { set; get; }

        [Display(Name = "FaceBook", ResourceType = typeof(msg_Trainee))]
        public string FaceBook { set; get; }

        [Display(Name = "WebSite", ResourceType = typeof(msg_Trainee))]
        public string WebSite { set; get; }


        // Assignment
        [Display(Name = "SingularName", ResourceType = typeof(msg_Group))]
        public Group Group { set; get; }
        public long? GroupId { set; get; }

        // Gestion des tâches
        //public virtual List<ProjectTask> ProjectTasks { set; get; }

    }
}

