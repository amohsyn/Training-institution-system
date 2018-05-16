using GApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingIS.Entities
{
    public class TrainingYear : BaseEntity
    {
        public string Code { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndtDate { set; get; }
    }
}
