using GApp.Entities;
using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = false, isGenerateBLO = false)]
    public class GPicture : BaseEntity
    {

        

        public string Name { set; get; }
        public string Description { set; get; }

        public string Original_Thumbnail { set; get; }
        public string Large_Thumbnail { set; get; }
        public string Medium_Thumbnail { set; get; }
        public string Small_Thumbnail { set; get; }

        /// <summary>
        /// Used to save the old_reference in Update case
        /// </summary>
        [NotMapped]
        public string Old_Reference { set; get; }

    }
}
