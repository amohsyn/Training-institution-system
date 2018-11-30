using GApp.Entities;
using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using TrainingIS.Entities.Resources.GroupResources;
using TrainingIS.Entities.Resources.ModuleTrainingResources;
using TrainingIS.Entities.Resources.SeanceNumberResources;
using TrainingIS.Entities.Resources.SeanceTrainingResources;
using TrainingIS.Models.enums;

namespace TrainingIS.Models.SeanceInfos
{
    /// <summary>
    /// a SeanceInfo contraine all information about : SeancePlanning, SeanceTraining, Plurality
    /// </summary>
    [SearchBy("SeanceTraining.Reference")]
    public class SeanceInfo : BaseEntity
    {
        [GAppDataTable(AutoGenerateFilter = true, FilterBy = "Group.Id", SearchBy = "Group.Reference", OrderBy = "Group.Reference", PropertyPath = "Group")]
        [Display(Name = "SingularName", ResourceType = typeof(msg_Group))]
        public Group Group { set; get; }

        [GAppDataTable(AutoGenerateFilter = true, FilterBy = "ModuleTraining.Id", SearchBy = "ModuleTraining.Reference", OrderBy = "ModuleTraining.Reference", PropertyPath = "ModuleTraining")]
        [Display(Name = "SingularName", ResourceType = typeof(msg_ModuleTraining))]
        public ModuleTraining ModuleTraining { set; get; }

        [Display(Name = "SeanceDate", ShortName = "Date", ResourceType = typeof(msg_SeanceTraining))]
        [GAppDataTable(FilterBy = "CalendarDay.Id", SearchBy = "CalendarDay.Date", OrderBy = "CalendarDay.Date", PropertyPath = "CalendarDay")]
        public CalendarDay CalendarDay { set; get; }

        [GAppDataTable(SearchBy = "SeancePlanning.SeanceNumber.Reference", OrderBy = "SeancePlanning.SeanceNumber.Reference", PropertyPath = "SeancePlanning.SeanceNumber.Reference")]
        [Display(Name = "SingularName", ShortName = "S", ResourceType = typeof(msg_SeanceNumber))]
        public SeanceNumber SeanceNumber { set; get; }


        [Display(Name = "Contained", ResourceType = typeof(msg_SeanceTraining))]
        [GAppDataTable(AutoGenerateFilter = false, FilterBy = "SeanceTraining.Id", SearchBy = "SeanceTraining.Contained", OrderBy = "SeanceTraining.Contained", PropertyPath = "SeanceTraining.Contained")]
        public String Contained {
            get
            {
                if (this.SeanceTraining != null)
                    return this.SeanceTraining.Contained;
                return "";
            }
        }

        [GAppDataTable(PropertyPath = "SeanceTraining.Absences.Count")]
        [Display(Name = "Absences_Count", ResourceType = typeof(msg_SeanceTraining))]
        public int Absences_Count { set; get; }

        [Display(Name = "Plurality", ResourceType = typeof(msg_SeanceTraining))]
        [GAppDataTable(AutoGenerateFilter = false, FilterBy = "SeanceTraining.Plurality", SearchBy = "SeanceTraining.Plurality", OrderBy = "SeanceTraining.Plurality", PropertyPath = "SeanceTraining.Plurality")]
        public int? Plurality_In_Minute { set; get; }


        public SeanceInfo_States SeanceInfo_State { set; get; }
        public SeancePlanning SeancePlanning { set; get; }
        public SeanceTraining SeanceTraining { set; get; }


        


    
        

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
