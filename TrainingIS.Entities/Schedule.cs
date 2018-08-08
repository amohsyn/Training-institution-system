﻿using GApp.Core.MetaDatas.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Base;
using TrainingIS.Entities.Resources.AppResources;
using TrainingIS.Entities.Resources.ScheduleResources;
using TrainingIS.Entities.Resources.TrainingYearResources;

namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = true)]
    public class Schedule : TrainingIS_BaseEntity
    {
        public override string ToString()
        {
            return  this.Reference;
        }
        public override string CalculateReference()
        {
            string reference = string.Format("{0} -> {1} [{2}]",
                this.StartDate.ToString(@"MM/dd", CultureInfo.InvariantCulture),
                this.EndtDate.ToString(@"MM/dd/yyyy",CultureInfo.InvariantCulture),
                this.TrainingYear.Reference
                );
            return reference;
        }

        // TrainingYear
        [Display(Name = "SingularName", ResourceType = typeof(msg_TrainingYear))]
        public virtual TrainingYear TrainingYear { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_TrainingYear))]
        public long TrainingYearId { set; get; }

        [Required]
        [Display(Name = "StartDate", ResourceType = typeof(msg_Schedule))]
        public DateTime StartDate { set; get; }

        [Required]
        [Display(Name = "EndtDate", ResourceType = typeof(msg_Schedule))]
        public DateTime EndtDate { set; get; }


        [Display(Name = "Description", ResourceType = typeof(msg_app))]
        public string Description { set; get; }
    }
}
