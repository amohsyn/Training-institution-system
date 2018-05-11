using GApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingIS.Entities
{
    public class Group : BaseEntity
    {
        public string Name { set; get; }
        public string Description { set; get; }
    
        public string Code { set; get; }
        public virtual Specialty Specialty { set; get; }
    }
}
