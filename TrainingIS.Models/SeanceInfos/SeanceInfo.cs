using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using TrainingIS.Models.enums;

namespace TrainingIS.Models.SeanceInfos
{

    public class SeanceInfo
    {

        public SeanceInfo_States SeanceInfo_State { set; get; }

        public CalendarDay CalendarDay { set; get; }

        public SeancePlanning SeancePlanning { set; get; }

        public SeanceTraining SeanceTraining { set; get; }

        public int? Plurality_In_Minute { set; get; }

        public Group Group { set; get; }
        public ModuleTraining ModuleTraining { set; get; }
        public SeanceNumber SeanceNumber { set; get; }
        

        // Unique Reference
        public string Reference
        {
            get
            {
                string reference = string.Format("{0}-{1}", this.SeancePlanning.Reference,this.CalendarDay.Reference);
                return "";
            }
        }

    }
}
