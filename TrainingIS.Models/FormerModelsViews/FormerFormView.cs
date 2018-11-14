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
using TrainingIS.Entities.Resources.FormerSpecialtyResources;
using GApp.Entities;
using TrainingIS.Entities;

namespace TrainingIS.Models.FormerModelsViews
{
    [FormView(typeof(Former))]
    public class FormerFormView : BaseModel
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

        [Display(Name = "Photo", ResourceType = typeof(msg_Person))]
        [GAppDataTable(AutoGenerateFilter = false, FilterBy = "Photo.Id", SearchBy = "Photo.Reference", OrderBy = "Photo.Reference", PropertyPath = "Photo")]
        public GPicture Photo { set; get; }

        [Display(AutoGenerateField = false)]
        public String Photo_Reference { set; get; }

        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_FormerSpecialty))]
        public Int64 FormerSpecialtyId { set; get; }

        [Display(Name = "WeeklyHourlyMass", ResourceType = typeof(msg_Former))]
        public Int32 WeeklyHourlyMass { set; get; }


        [Required]
        [Display(Name = "FirstNameArabe", ResourceType = typeof(msg_Person))]
        public String FirstNameArabe { set; get; }

        [Required]
        [Display(Name = "LastNameArabe", ResourceType = typeof(msg_Person))]
        public String LastNameArabe { set; get; }

        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_Nationality))]
        public Int64 NationalityId { set; get; }

        [Required]
        [Display(Name = "Sex", ResourceType = typeof(msg_Person))]
        public SexEnum Sex { set; get; }

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


        [Display(Name = "CreateUserAccount", ResourceType = typeof(msg_Former))]
        public bool CreateUserAccount { set; get; }

        [Display(Name = "Login", ResourceType = typeof(msg_Former))]
        [ReadFrom(PropertyName = nameof(FormerFormView.Email) , ReadOnly = true )]
        public string Login { set; get; }

        [Display(Name = "Password", ResourceType = typeof(msg_Former))]
        [ReadFrom(PropertyName = nameof(FormerFormView.RegistrationNumber), ReadOnly = true)]
        public string Password { set; get; }
    }
}

 