﻿using GApp.Entities;
using GApp.Entities.Resources.AppResources;
using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Base;

namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = true)]
    public class Project : BaseTaskProject
    {
        public override string ToString()
        {
            return this.Name;
        }
        public override string CalculateReference()
        {
            string reference = string.Format("{0}", this.Name);
            return reference;
        }

    }
}
