﻿using GApp.Core.MetaDatas.Attributes;
using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Resources.AppResources;
using TrainingIS.Entities.Resources.SeanceDayResources;
using TrainingIS.Entities.Resources.SeanceNumberResources;
using TrainingIS.Entities.Resources.TrainingResources;

namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = false)]
    public class SeancePlanning : BaseEntity
    {
        public override string ToString()
        {
            return this.Reference;
        }
        public override string CalculateReference()
        {
            string reference = string.Format("{0}-{1}", this.Training.Reference,this.SeanceNumber.Reference);
            return reference;
        }

        // Training
        [Display(Name = "SingularName", ResourceType = typeof(msg_Training))]
        public virtual Training Training { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_Training))]
        public long TrainingId { set; get; }

        // SeanceDay
        [Display(Name = "SingularName", ResourceType = typeof(msg_SeanceDay))]
        public virtual SeanceDay SeanceDay { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_SeanceDay))]
        public long SeanceDayId { set; get; }

        // SeanceNumber
        [Display(Name = "SingularName", ResourceType = typeof(msg_SeanceNumber))]
        public virtual SeanceNumber SeanceNumber { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_SeanceNumber))]
        public long SeanceNumberId { set; get; }


        [Display(Name = "Description", ResourceType = typeof(msg_app))]
        public string Description { set; get; }
    }
}