using GApp.Core.MetaDatas.Attributes;
using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Resources.AppResources;
using TrainingIS.Entities.Resources.PersonResources;

namespace TrainingIS.Entities
{
    [LocalizationEnum(typeof(msg_Person))]
    public enum SexEnum
    {
        man,
        woman
    }

    public class Person : BaseEntity
    {
       

        // 
        // civil status
        //
        [Required]
        [Display(Name = "FirstName", ResourceType = typeof(msg_Person))]
        public string FirstName { set; get; }

        [Required]
        [Display(Name = "LastName", ResourceType = typeof(msg_Person))]
        public string LastName { set; get; }

        [Required]
        [Display(Name = "FirstNameArabe", ResourceType = typeof(msg_Person))]
        public string FirstNameArabe { set; get; }

        [Required]
        [Display(Name = "LastNameArabe", ResourceType = typeof(msg_Person))]
        public string LastNameArabe { set; get; }

        [Required]
        [Display(Name = "Birthdate", ResourceType = typeof(msg_Person))]
        public DateTime Birthdate { set; get; }

        [Required]
        [Display(Name = "BirthPlace", ResourceType = typeof(msg_Person))]
        public string BirthPlace { set; get; }

        [Required]
        [Display(Name = "Sex", ResourceType = typeof(msg_Person))]
        public SexEnum Sex { set; get; }

        [Required]
        [Display(Name = "CIN", ResourceType = typeof(msg_Person))]
        [Unique]
        public string CIN { set; get; }

    }
}
