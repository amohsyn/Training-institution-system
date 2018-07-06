﻿using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Resources.FormerResources;
using TrainingIS.Entities.Resources.PersonResources;
using TrainingIS.Entities.Resources.TraineeResources;

namespace TrainingIS.Entities
{
    public class Former : BaseEntity
    {
        public override string ToString()
        {
            return this.FirstName + " " + this.LastName;
        }

        // 
        // civil status
        //
        [Required]
        [Display(Name = "FirstName", ResourceType = typeof(msg_Person))]
        public string FirstName { set; get; }

        [Required]
        [Display(Name = "LastName", ResourceType = typeof(msg_Person))]
        public string LastName { set; get; }

        [Required]
        [Display(Name = "Sex", ResourceType = typeof(msg_Person))]
        public bool Sex { set; get; }

        [Display(Name = "CIN", ResourceType = typeof(msg_Person))]
        public string CIN { set; get; }


        // Contact information
        [Display(Name = "Cellphone", ResourceType = typeof(msg_Person))]
        public string Cellphone { set; get; }

        [Display(Name = "Email", ResourceType = typeof(msg_Person))]
        public string Email { set; get; }

        [Display(Name = "Address", ResourceType = typeof(msg_Person))]
        public string Address { set; get; }

        [Display(Name = "FaceBook", ResourceType = typeof(msg_Person))]
        public string FaceBook { set; get; }

        [Display(Name = "WebSite", ResourceType = typeof(msg_Person))]
        public string WebSite { set; get; }

        // job information
        [Required]
        [Display(Name = "RegistrationNumber", ResourceType = typeof(msg_Former))]
        public string RegistrationNumber { set; get; }


}
}
