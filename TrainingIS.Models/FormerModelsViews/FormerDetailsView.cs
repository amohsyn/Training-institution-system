using GApp.Core.Entities.ModelsViews;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Resources.FormerResources;
using TrainingIS.Entities.Resources.NationalityResources;
using GApp.Models.DataAnnotations;
using GApp.Models;
using GApp.Entities.Resources.PersonResources;

namespace TrainingIS.Entities.ModelsViews.FormerModelsViews
{
    [DetailsView(typeof(Former))]
    public class FormerDetailsView : BaseModel
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

        [Required]
        [Display(Name = "FirstNameArabe", ResourceType = typeof(msg_Person))]
        public String FirstNameArabe { set; get; }

        [Required]
        [Display(Name = "LastNameArabe", ResourceType = typeof(msg_Person))]
        public String LastNameArabe { set; get; }

        [Required]
        [Display(Name = "Sex", ResourceType = typeof(msg_Person))]
        public SexEnum Sex { set; get; }

        [Display(Name = "SingularName", ResourceType = typeof(msg_Nationality))]
        public Nationality Nationality { set; get; }

        [Required]
        [Display(Name = "Birthdate", ResourceType = typeof(msg_Person))]
        public DateTime Birthdate { set; get; }
 
        [Required]
        [Display(Name = "BirthPlace", ResourceType = typeof(msg_Person))]
        public String BirthPlace { set; get; }

        [Required]
        [Unique]
        [Display(Name = "CIN", ResourceType = typeof(msg_Person))]
        public String CIN { set; get; }

        [Display(Name = "Cellphone", ResourceType = typeof(msg_Person))]
        public String Cellphone { set; get; }

        [Required]
        [Display(Name = "Email", ResourceType = typeof(msg_Person))]
        [DataType(DataType.EmailAddress)]
        public String Email { set; get; }

        [Display(Name = "Address", ResourceType = typeof(msg_Person))]
        public String Address { set; get; }

    }
}
