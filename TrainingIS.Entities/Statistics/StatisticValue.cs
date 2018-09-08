using GApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingIS.Entities
{
    public class StatisticValue :  BaseEntity
    {
        public string Name { get; set; }

 
        public Int64 Number { get; set; }

        public Int64 Value { get; set; }

        public virtual Statistic Statistic { set; get; }
    }
}
