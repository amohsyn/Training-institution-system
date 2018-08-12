using GApp.Core.Entities.ModelsViews;
using GApp.Entities.Resources.AppResources;
using GApp.Models;
using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
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
 
    [CreateView(typeof(Group))]
    public class CreateGroupView : BaseModel
    {
        // TrainingYear
        [Display(Name = "SingularName", ResourceType = typeof(msg_TrainingYear))]
        public virtual TrainingYear TrainingYear { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_TrainingYear))]
        public long TrainingYearId { set; get; }

        // Specialty
        [Display(Name = "SingularName", ResourceType = typeof(msg_Specialty))]
        public virtual Specialty Specialty { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_Specialty))]
        public long SpecialtyId { set; get; }

        // TrainingType
        [Display(Name = "SingularName", ResourceType = typeof(msg_TrainingType))]
        public virtual TrainingType TrainingType { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_TrainingType))]
        public long TrainingTypeId { set; get; }
 
        // YearStudy
        [Display(Name = "SingularName", ResourceType = typeof(msg_YearStudy))]
        public virtual YearStudy YearStudy { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_YearStudy))]
        public long YearStudyId { set; get; }

        [Required]
        [Display(Name = "Code", ResourceType = typeof(msg_app))]
        public string Code { get; set; }

    }
}
