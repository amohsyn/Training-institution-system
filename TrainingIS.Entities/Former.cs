using GApp.Core.MetaDatas.Attributes;
using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Resources.FormerResources;
using TrainingIS.Entities.Resources.PersonResources;
using TrainingIS.Entities.Resources.TraineeResources;
 
 
 

namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = true)]
    public class Former : BaseEntity
    {
        public override string ToString()
        {
            return this.FirstName + " " + this.LastName;
        }

        public override string CalculateReference()
        {
            string reference = string.Format("{0}", this.RegistrationNumber);
            return base.CalculateReference();
        }

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
        [Display(Name = "Sex", ResourceType = typeof(msg_Person))]
        public bool Sex { set; get; }

        [Display(Name = "CIN", ResourceType = typeof(msg_Person))]
        public string CIN { set; get; }


        // Contact information
        [Display(Name = "Cellphone", ResourceType = typeof(msg_Person))]
        public string Cellphone { set; get; }

      
        [Required]
        [Display(Name = "Email", ResourceType = typeof(msg_Person))]
        [StringLength(65)]
        [Index( "IX_Former_Email",IsUnique = true)]
        public string Email { set; get; }

        [Display(Name = "Address", ResourceType = typeof(msg_Person))]
        public string Address { set; get; }

        [Display(Name = "FaceBook", ResourceType = typeof(msg_Person))]
        public string FaceBook { set; get; }

        [Display(Name = "WebSite", ResourceType = typeof(msg_Person))]
        public string WebSite { set; get; }

        // job information
        [Required]
        [Display(Name = "RegistrationNumber", ResourceType = typeof(msg_Former))]
        public string RegistrationNumber { set; get; }


}
}
