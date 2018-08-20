using GApp.Models.DataAnnotations;
using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.Entities.Resources.AppResources;
using TrainingIS.Entities.Resources.NationalityResources;
using GApp.Entities.Resources.PersonResources;

namespace TrainingIS.Entities.Base
{
 
    public class Person : BaseEntity
    {
        // 
        // CivilStatus
        //
        [Required]
        [Display(Name = "FirstName", GroupName = "CivilStatus", Order =1,    ResourceType = typeof(msg_Person))]
        public string FirstName { set; get; }

        [Required]
        [Display(Name = "LastName", GroupName = "CivilStatus", Order = 2, ResourceType = typeof(msg_Person))]
        public string LastName { set; get; }

        [Display(Name = "FirstNameArabe", GroupName = "CivilStatus", Order = 3, ResourceType = typeof(msg_Person))]
        public string FirstNameArabe { set; get; }

        [Display(Name = "LastNameArabe", GroupName = "CivilStatus", Order = 4, ResourceType = typeof(msg_Person))]
        public string LastNameArabe { set; get; }

        [Required]
        [Display(Name = "Sex", Order = 5, GroupName = "CivilStatus", ResourceType = typeof(msg_Person))]
        public SexEnum Sex { set; get; }

        [Display(Name = "Birthdate", GroupName = "CivilStatus", Order =6, ResourceType = typeof(msg_Person))]
        public DateTime Birthdate { set; get; }

        // Nationality
        [Display(Name = "SingularName", Order = 7, ResourceType = typeof(msg_Nationality))]
        public virtual Nationality Nationality { set; get; }
        [Display(Name = "SingularName", Order = 8, ResourceType = typeof(msg_Nationality))]
        public long NationalityId { set; get; }

        [Display(Name = "BirthPlace", GroupName = "CivilStatus", Order = 9, ResourceType = typeof(msg_Person))]
        public string BirthPlace { set; get; }

        [Display(Name = "CIN", GroupName = "CivilStatus", Order = 10, ResourceType = typeof(msg_Person))]
        [Unique]
        public string CIN { set; get; }


        //
        // ContactInformation
        //
        [Display(Name = "Cellphone", GroupName = "ContactInformation", Order =20, ResourceType = typeof(msg_Person))]
        public string Cellphone { set; get; }


        [Display(Name = "Email", GroupName = "ContactInformation", Order = 21, ResourceType = typeof(msg_Person))]
        [Unique]
        [EmailAddress]
        public string Email { set; get; }

        [Display(Name = "Address", GroupName = "ContactInformation", Order = 22, ResourceType = typeof(msg_Person))]
        public string Address { set; get; }

        [Display(Name = "FaceBook", GroupName = "ContactInformation", Order = 23, ResourceType = typeof(msg_Person))]
        public string FaceBook { set; get; }

        [Display(Name = "WebSite", GroupName = "ContactInformation", Order = 24, ResourceType = typeof(msg_Person))]
        public string WebSite { set; get; }
    }
}
