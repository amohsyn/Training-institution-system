﻿using GApp.Core.MetaDatas.Attributes;
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
        // Contact information
        //
        [Display(Name = "Cellphone", ResourceType = typeof(msg_Person))]
        public string Cellphone { set; get; }

        [Display(Name = "TutorCellPhone", ResourceType = typeof(msg_Trainee))]
        public string TutorCellPhone { set; get; }

        [Display(Name = "Email", ResourceType = typeof(msg_Person))]
        public string Email { set; get; }

        [Display(Name = "Address", ResourceType = typeof(msg_Person))]
        public string Address { set; get; }

        [Display(Name = "FaceBook", ResourceType = typeof(msg_Person))]
        public string FaceBook { set; get; }

        [Display(Name = "WebSite", ResourceType = typeof(msg_Person))]
        public string WebSite { set; get; }

        // 
        // Training Information
        //
        [Required]
        [Unique]
        [Display(Name = "CEF", ResourceType = typeof(msg_Trainee))]
        // Generate Error in Migration , DF
        [StringLength(65)]
        [Index("IX_Trainee_CEF", IsUnique = true)]
        public string CNE { set; get; }
        

        // 
        // Dossier
        //
        [Display(Name = "isActif", ResourceType = typeof(msg_Trainee))]
        public IsActifEnum isActif { set; get; }

        [Display(Name = "DateRegistration", ResourceType = typeof(msg_Trainee))]
        public DateTime? DateRegistration { set; get; }



        // Nationality
        [Display(Name = "SingularName", ResourceType = typeof(msg_Nationality))]
        public virtual Nationality Nationality { set; get; }
        [Required]
        public long NationalityId { set; get; }

        // Schoollevel
        [Display(Name = "SingularName", ResourceType = typeof(msg_Schoollevel))]
        public virtual Schoollevel Schoollevel { set; get; }
        [Display(Name = "SingularName", ResourceType = typeof(msg_Schoollevel))]
        public long? SchoollevelId { set; get; }



        //
        // Assignements
        //

        // Group
        [Display(Name = "SingularName", ResourceType = typeof(msg_Group))]
        public virtual Group Group { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_Group))]
        public long GroupId { set; get; }


        // Absence
        [Display(AutoGenerateField = false)]
        public  virtual List<StateOfAbsece> StateOfAbseces { set; get; }
    }
}

