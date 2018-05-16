using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace TrainingIS.Entities
{
   
    public class Specialty : BaseEntity
    {

        public string Name { set; get; }
        public string Description { set; get; }
        public string Code { get; set; }


        public override string ToString()
        {
            return this.Code;
        }
    }
   
}