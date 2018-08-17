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
        [Required]
        [Unique]
        [Display(Name = "RegistrationNumber", ResourceType = typeof(msg_Former))]
        public String RegistrationNumber { set; get; }

        [Required]
        [Display(Name = "FirstName", ResourceType = typeof(msg_Person))]
        public String FirstName { set; get; }

        [Required]
        [Display(Name = "LastName", ResourceType = typeof(msg_Person))]
        public String LastName { set; get; }


        [Display(Name = "SingularName", ResourceType = typeof(msg_FormerSpecialty))]
        public FormerSpecialty FormerSpecialty { set; get; }

        [Required]
        [Display(Name = "Email", ResourceType = typeof(msg_Person))]
        [DataType(DataType.EmailAddress)]
        public String Email { set; get; }

    }
}
