using GApp.Models.DataAnnotations;
using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using TrainingIS.Entities.Base;
using TrainingIS.Entities.Resources.GroupResources;
using TrainingIS.Entities.Resources.SchoollevelResources;
using TrainingIS.Entities.Resources.TraineeResources;
using TrainingIS.Entities.Resources.YearStudyResources;
using TrainingIS.Entities.Resources.SpecialtyResources;

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

      


        #region RegistrationForm
        [Required]
        [Unique]
        [Display(Name = "CEF", GroupName = "RegistrationForm", Order = 30, ResourceType = typeof(msg_Trainee))]
        [StringLength(65)]
        [Index("IX_Trainee_CEF", IsUnique = true)]
        public string CNE { set; get; }
       
        [Display(Name = "DateRegistration", GroupName = "RegistrationForm", Order = 31, ResourceType = typeof(msg_Trainee))]
        public DateTime? DateRegistration { set; get; }

        [Display(Name = "isActif", AutoGenerateFilter  = true, GroupName = "RegistrationForm", Order = 32, ResourceType = typeof(msg_Trainee))]
        public IsActifEnum isActif { set; get; }
        #endregion


        #region Trainings
        // Schoollevel
        [Display(Name = "SingularName", AutoGenerateFilter = true, GroupName = "RegistrationForm", Order = 33, ResourceType = typeof(msg_Schoollevel))]
        public virtual Schoollevel Schoollevel { set; get; }
        [Display(Name = "SingularName", Order = 19, ResourceType = typeof(msg_Schoollevel))]
        public long? SchoollevelId { set; get; }


        //
        // Assignements
        //

        // Specialty
        [Display(Name = "SingularName", AutoGenerateFilter = true, GroupName = "Assignements", ResourceType = typeof(msg_Specialty))]
        public virtual Specialty Specialty { set; get; }
        [Display(Name = "SingularName", ResourceType = typeof(msg_Specialty))]
        public long SpecialtyId { set; get; }

        // YearStudy
        [Display(Name = "SingularName", AutoGenerateFilter = true, GroupName = "Assignements", ResourceType = typeof(msg_YearStudy))]
        public virtual YearStudy YearStudy { set; get; }
        [Display(Name = "SingularName", ResourceType = typeof(msg_YearStudy))]
        public long YearStudyId { set; get; }

        // Group
        [Display(Name = "SingularName", AutoGenerateFilter = true, GroupName = "Assignements", Order = 40, ResourceType = typeof(msg_Group))]
        public virtual Group Group { set; get; }
        [Required]
        [Display(Name = "SingularName", Order = 20, ResourceType = typeof(msg_Group))]
        public long GroupId { set; get; }
        #endregion 


 
        //
        // AutoGenerateField = false
        //
        // Absence
        [Display(AutoGenerateField = false, Order = 21)]
        public  virtual List<StateOfAbsece> StateOfAbseces { set; get; }


       
        public string GetFullName()
        {
            return string.Format("{0} {1}",this.FirstName,this.LastName);
        }

        [Display(AutoGenerateField =false)]
        public States States { set; get; }

        [Display(AutoGenerateField = false)]
        public virtual List<WorkGroup> Member_To_WorkGroups { get; set; }




    }
}

