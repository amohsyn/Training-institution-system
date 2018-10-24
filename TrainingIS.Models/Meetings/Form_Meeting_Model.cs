using GApp.Core.Entities.ModelsViews;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.Models.DataAnnotations;
using GApp.Models;
using GApp.Entities;
using TrainingIS.Entities.enums;
using TrainingIS.Entities.Resources.MeetingResources;
using GApp.Entities.Resources.AppResources;

namespace TrainingIS.Entities.ModelsViews
{
    [EditView(typeof(Meeting))]
    [CreateView(typeof(Meeting))]
    public class Form_Meeting_Model : Default_Form_Meeting_Model
    {
        #region UI configuration
        [Display(AutoGenerateField =false)]
        public string President_Name { set; get; }

        [Display(AutoGenerateField = false)]
        public string VicePresident_Name { set; get; }

        [Display(AutoGenerateField = false)]
        public string Protractor_Name { set; get; }

        [Display(AutoGenerateField =false)]
        public WorkGroup WorkGroup { set; get; }
        #endregion



    }
}
