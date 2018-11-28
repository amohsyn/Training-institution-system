using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities.Resources.MeetingResources;

namespace TrainingIS.Models.Meetings
{
    [FormView(typeof(Meeting))]
    public class Form_Meeting_Model : Default_Form_Meeting_Model
    {
        [Display(Name = "Mission_Working_Group", GroupName = "Object", Order = 3, AutoGenerateFilter = true, ResourceType = typeof(msg_Meeting))]
        public Mission_Working_Group Mission_Working_Group { set; get; }

        #region UI configuration
        [Display(AutoGenerateField = false)]
        public string President_Name { set; get; }

        [Display(AutoGenerateField = false)]
        public string VicePresident_Name { set; get; }

        [Display(AutoGenerateField = false)]
        public string Protractor_Name { set; get; }

        [Display(AutoGenerateField = false)]
        public WorkGroup WorkGroup { set; get; }
        #endregion
    }
}
