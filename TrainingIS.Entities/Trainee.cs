using GApp.Core.MetaDatas.Attributes;
using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using TrainingIS.Entities.Base;
using TrainingIS.Entities.Resources.GroupResources;
using TrainingIS.Entities.Resources.NationalityResources;
using TrainingIS.Entities.Resources.PersonResources;
using TrainingIS.Entities.Resources.SchoollevelResources;
using TrainingIS.Entities.Resources.TraineeResources;
 

namespace TrainingIS.Entities
{
    [LocalizationEnum(typeof(msg_Trainee))]
    public enum IsActifEnum
    {
        Yes,
        No
    }


    [EntityMetataData(isMale = true)]
    public class Trainee : Person
    {

        public override string ToString()
        {
            return this.FirstName + " " + this.LastName;
        }

        public override string CalculateReference()
        {
            string reference = "";
            if (!string.IsNullOrEmpty(this.CNE))
                reference = string.Format("{0}", this.CNE);
            return reference;
        }

        // 
        // RegistrationForm 
        //
        [Required]
        [Unique]
        [Display(Name = "CEF", GroupName = "RegistrationForm", Order = 30, ResourceType = typeof(msg_Trainee))]
        [StringLength(65)]
        [Index("IX_Trainee_CEF", IsUnique = true)]
        public string CNE { set; get; }
       
        [Display(Name = "DateRegistration", GroupName = "RegistrationForm", Order = 31, ResourceType = typeof(msg_Trainee))]
        public DateTime? DateRegistration { set; get; }

        [Display(Name = "isActif", GroupName = "RegistrationForm", Order = 32, ResourceType = typeof(msg_Trainee))]
        public IsActifEnum isActif { set; get; }

        // Schoollevel
        [Display(Name = "SingularName", GroupName = "RegistrationForm", Order = 33, ResourceType = typeof(msg_Schoollevel))]
        public virtual Schoollevel Schoollevel { set; get; }
        [Display(Name = "SingularName", Order = 19, ResourceType = typeof(msg_Schoollevel))]
        public long? SchoollevelId { set; get; }



        //
        // Assignements
        //

        // Group
        [Display(Name = "SingularName", GroupName = "Assignements", Order = 40, ResourceType = typeof(msg_Group))]
        public virtual Group Group { set; get; }
        [Required]
        [Display(Name = "SingularName", Order = 20, ResourceType = typeof(msg_Group))]
        public long GroupId { set; get; }


        //
        // AutoGenerateField = false
        //
        // Absence
        [Display(AutoGenerateField = false, Order = 21)]
        public  virtual List<StateOfAbsece> StateOfAbseces { set; get; }
    }
}

