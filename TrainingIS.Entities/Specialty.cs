﻿using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using TrainingIS.Entities.Resources.AppResources;
using TrainingIS.Entities.Resources.SpecialtyResources;

namespace TrainingIS.Entities
{
     
    public class Specialty : BaseEntity
    {
        [Required]
        [Display(Name = "Code", ResourceType = typeof(msg_app))]
        public string Code { get; set; }

        [Required]
        [Display(Name = "Name", ResourceType = typeof(msg_app))]
        public string Name { set; get; }

        [Display(Name = "Description", ResourceType = typeof(msg_app))]
        public string Description { set; get; }

        public override string ToString()
        {
            return this.Code;
        }
    }
   
}