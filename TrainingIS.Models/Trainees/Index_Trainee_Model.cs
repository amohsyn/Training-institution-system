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
using TrainingIS.Entities.Resources.TraineeResources;
using TrainingIS.Entities.Resources.SchoollevelResources;
using TrainingIS.Entities.Resources.GroupResources;
using GApp.Entities.Resources.PersonResources;
using TrainingIS.Entities.Resources.NationalityResources;
using TrainingIS.Entities;
using TrainingIS.Entities.Resources.SpecialtyResources;
using TrainingIS.Entities.Resources.YearStudyResources;

namespace TrainingIS.Models.Trainees
{
    [IndexView(typeof(Trainee))]
    public class Index_Trainee_Model : BaseModel
    {
        [Required]
        [Unique]
        [Display(Name = "CEF", ResourceType = typeof(msg_Trainee))]
        public String CNE { set; get; }

        [Display(Name = "SingularName", ResourceType = typeof(msg_Group))]
        public Group Group { set; get; }

        [Required]
        [Display(Name = "FirstName", ResourceType = typeof(msg_Person))]
        public String FirstName { set; get; }

        [Required]
        [Display(Name = "LastName", ResourceType = typeof(msg_Person))]
        public String LastName { set; get; }
    }
}
