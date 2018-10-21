using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Base;
using TrainingIS.Entities.Resources.FormerResources;

namespace TrainingIS.Entities
{
    public abstract class Employee : Person
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
        [GAppDataTable(isColumn = false)]
        public string RegistrationNumber { set; get; }
 
        [Display(Name = "CreateUserAccount", ResourceType = typeof(msg_Former))]
        [GAppDataTable(isColumn = false)]
        public bool CreateUserAccount { set; get; }


        [Required]
        [Display(Name = "Login", ResourceType = typeof(msg_Former))]
        [GAppDataTable(isColumn = false)]
        public string Login { set; get; }

        [Required()]
        [StringLength(100, ErrorMessageResourceName = "PasswordMustBeBetweenMinAndMaxCharacters", ErrorMessageResourceType = typeof(msg_Former), MinimumLength = 4)]
        [Display(Name = "Password", ResourceType = typeof(msg_Former))]
        [GAppDataTable(isColumn = false)]
        public string Password { set; get; }
    }
}
