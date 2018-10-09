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
        [Display(Name = "Photo", AutoGenerateField = false, ResourceType = typeof(msg_Person))]
        [GAppDataTable(AutoGenerateFilter = false, FilterBy = "Photo.Id", SearchBy = "Photo.Reference", OrderBy = "Photo.Reference", PropertyPath = "Photo")]
        public GPicture Photo { set; get; }

        [Display(AutoGenerateField = false)]
        public string Photo_Reference { set; get; }

        [Display(Name = "CEF", ResourceType = typeof(msg_Trainee))]
        [GAppDataTable(AutoGenerateFilter = false, FilterBy = "CNE", SearchBy = "CNE", OrderBy = "CNE", PropertyPath = "CNE")]
        public String CNE { set; get; }

        [Display(Name = "SingularName", ResourceType = typeof(msg_Group))]
        [GAppDataTable(AutoGenerateFilter = true, FilterBy = "Group.Id", SearchBy = "Group.Reference", OrderBy = "Group.Reference", PropertyPath = "Group")]
        public Group Group { set; get; }

        [Display(Name = "FirstName", ResourceType = typeof(msg_Person))]
        [GAppDataTable(AutoGenerateFilter = false, FilterBy = "FirstName", SearchBy = "FirstName", OrderBy = "FirstName", PropertyPath = "FirstName")]
        public String FirstName { set; get; }

        [Display(Name = "LastName", ResourceType = typeof(msg_Person))]
        [GAppDataTable(AutoGenerateFilter = false, FilterBy = "LastName", SearchBy = "LastName", OrderBy = "LastName", PropertyPath = "LastName")]
        public String LastName { set; get; }

     


    }
}
