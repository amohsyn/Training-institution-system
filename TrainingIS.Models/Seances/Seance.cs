using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;

namespace TrainingIS.Models.Seances
{
    public class SeanceModel
    {
        public SeanceTraining SeanceTraining { set; get; }
        public SeancePlanning SeancePlanning { set; get; }

        public bool IsSeanceTrainingCreated
        {
            get
            {
                return (this.SeanceTraining == null) ? false : true;
            }
        }

        
    }
}
