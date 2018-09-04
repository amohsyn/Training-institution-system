using GApp.Models.DataAnnotations;
using GApp.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Base;
using TrainingIS.Entities.Resources.FormerResources;
using TrainingIS.Entities.Resources.MetierResources;
using TrainingIS.Entities.Resources.FormerSpecialtyResources;

namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = true)]
    public class Former : Person
    {
        //
        // Références and Codes
        //
        public override string ToString()
        {
            return this.FirstName + " " + this.LastName;
        }

        public override string CalculateReference()
        {
            string reference = string.Format("{0}", this.RegistrationNumber);
            return reference;
        }
        


        //
        // JobInformation
        //
        [Required]
        [Unique]
        [Display(Name = "RegistrationNumber", GroupName = "JobInformation", Order = 30, ResourceType = typeof(msg_Former))]
        [StringLength(65)]
        [Index("IX_Former_RegistrationNumber", IsUnique = true)]
        public string RegistrationNumber { set; get; }

        // Metier
        [Display(Name = "SingularName", ResourceType = typeof(msg_FormerSpecialty))]
        public virtual FormerSpecialty FormerSpecialty { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_FormerSpecialty))]
        public long FormerSpecialtyId { set; get; }


        
        [Display(Name = "WeeklyHourlyMass", ResourceType = typeof(msg_Former))]
        public int WeeklyHourlyMass { set; get; }

        [Display(Name = "CreateUserAccount", ResourceType = typeof(msg_Former))]
        public bool CreateUserAccount { set; get; }


        [Required]
        [Display(Name = "Login", ResourceType = typeof(msg_Former))]
        public string Login { set; get; }

        [Required()]
        [StringLength(100, ErrorMessageResourceName = "PasswordMustBeBetweenMinAndMaxCharacters", ErrorMessageResourceType = typeof(msg_Former), MinimumLength = 4)]
        [Display(Name = "Password", ResourceType = typeof(msg_Former))]
        public string Password { set; get; }

    }
}
