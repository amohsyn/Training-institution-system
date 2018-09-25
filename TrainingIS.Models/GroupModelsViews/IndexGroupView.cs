using GApp.Core.Entities.ModelsViews;
using GApp.Entities.Resources.AppResources;
using GApp.Models;
using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Resources.SpecialtyResources;
using TrainingIS.Entities.Resources.TrainingTypeResources;
using TrainingIS.Entities.Resources.TrainingYearResources;
using TrainingIS.Entities.Resources.YearStudyResources;

namespace TrainingIS.Entities.ModelsViews.GroupModelsViews
{
    [IndexView(typeof(Group))]
    public partial class IndexGroupView : BaseModel
    {
        [Display(Name = "Code", ResourceType = typeof(msg_app))]
        [GAppDataTable(AutoGenerateFilter = false, PropertyPath = "Code")]
        public string Code { get; set; }

        [Display(Name = "SingularName", ResourceType = typeof(msg_YearStudy))]
        [GAppDataTable(AutoGenerateFilter = true, FilterBy = "YearStudy.Id", SearchBy = "YearStudy.Reference",OrderBy = "YearStudy.Reference")]
        public virtual YearStudy YearStudy { set; get; }

        [GAppDataTable(AutoGenerateFilter = true, FilterBy = "Specialty.Id", SearchBy = "Specialty.Reference", OrderBy = "Specialty.Reference")]
        [Display(Name = "SingularName", ResourceType = typeof(msg_Specialty))]
        public virtual Specialty Specialty { set; get; }

        [GAppDataTable(AutoGenerateFilter = true, FilterBy = "TrainingType.Id", SearchBy = "TrainingType.Reference", OrderBy = "TrainingType.Reference")]
        [Display(Name = "SingularName", ResourceType = typeof(msg_TrainingType))]
        public virtual TrainingType TrainingType { set; get; }


        [Display(Name = "Description", ResourceType = typeof(msg_app))]
        public string Desc
        {
            get
            {
                return this.Code + " " + this.YearStudy.Code;
            }
        }
        private string _Desc;
    }
}
