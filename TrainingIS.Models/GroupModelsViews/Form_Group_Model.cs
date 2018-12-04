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
using System.ComponentModel.DataAnnotations;

using TrainingIS.Entities.Resources.TrainingTypeResources;
using TrainingIS.Entities.Resources.TrainingYearResources;
using TrainingIS.Entities.Resources.SpecialtyResources;
using TrainingIS.Entities.Resources.YearStudyResources;
using GApp.Entities.Resources.AppResources;
using GApp.Entities.Resources.BaseEntity;

namespace TrainingIS.Entities.ModelsViews
{
    [FormView(typeof(Group))]
    public class Form_Group_Model : BaseModel
    {
        // TrainingYear
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_TrainingYear))]
        public long TrainingYearId { set; get; }

        // Specialty
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_Specialty))]
        public long SpecialtyId { set; get; }

        // TrainingType
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_TrainingType))]
        public long TrainingTypeId { set; get; }

        // YearStudy
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_YearStudy))]
        public long YearStudyId { set; get; }

        [Required]
        [Display(Name = "Code", ResourceType = typeof(msg_app))]
        public string Code { get; set; }

    }
}
