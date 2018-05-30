using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Ressources.TrainingYearRessources;

namespace TrainingIS.Entities
{
    public class TrainingYear : BaseEntity
    {
        public TrainingYear()
        {
            this.StartDate = DateTime.Now;
            this.EndtDate = DateTime.Now;
        }

        [Display(Name = "Code",ResourceType = typeof(TrainingYearRessource))]
        public string Code { set; get; }

        [Display(Name = "StartDate", ResourceType = typeof(TrainingYearRessource))]
        public DateTime StartDate { set; get; }

        [Display(Name = "EndtDate", ResourceType = typeof(TrainingYearRessource))]
        public DateTime EndtDate { set; get; }
    }
}
