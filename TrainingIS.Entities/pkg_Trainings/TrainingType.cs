﻿using GApp.Models.DataAnnotations;
using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.Entities.Resources.AppResources;
using TrainingIS.Entities.Base;
namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = true)]
    public class TrainingType : BaseEntity
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
