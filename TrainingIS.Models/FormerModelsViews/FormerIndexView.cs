using GApp.Core.Entities.ModelsViews;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Resources.FormerResources;
using GApp.Models.DataAnnotations;
using GApp.Entities.Resources.PersonResources;
using GApp.Models;
using TrainingIS.Entities.Resources.FormerSpecialtyResources;

namespace TrainingIS.Entities.ModelsViews.FormerModelsViews
{
    [IndexView(typeof(Former))]
    public class FormerIndexView : BaseModel
    {
        [Display(Name = "RegistrationNumber", ResourceType = typeof(msg_Former))]
        [GAppDataTable(AutoGenerateFilter = false, FilterBy = "RegistrationNumber", SearchBy = "RegistrationNumber", OrderBy = "RegistrationNumber", PropertyPath = "RegistrationNumber")]
        public String RegistrationNumber { set; get; }

        [Display(Name = "FirstName", ResourceType = typeof(msg_Person))]
        [GAppDataTable(AutoGenerateFilter = false, FilterBy = "FirstName", SearchBy = "FirstName", OrderBy = "FirstName", PropertyPath = "FirstName")]
        public String FirstName { set; get; }

        [Display(Name = "LastName", ResourceType = typeof(msg_Person))]
        [GAppDataTable(AutoGenerateFilter = false, FilterBy = "LastName", SearchBy = "LastName", OrderBy = "LastName", PropertyPath = "LastName")]
        public String LastName { set; get; }


        [Display(Name = "SingularName", ResourceType = typeof(msg_FormerSpecialty))]
        [GAppDataTable(AutoGenerateFilter = true, FilterBy = "FormerSpecialty.Id", SearchBy = "FormerSpecialty.Reference", OrderBy = "FormerSpecialty.Reference", PropertyPath = "FormerSpecialty")]
        public FormerSpecialty FormerSpecialty { set; get; }


        [Display(Name = "Email", ResourceType = typeof(msg_Person))]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "")]
        [GAppDataTable(AutoGenerateFilter = false, FilterBy = "Email", SearchBy = "Email", OrderBy = "Email", PropertyPath = "Email")]
        [DataType(DataType.EmailAddress)]
        public String Email { set; get; }

    }
}
