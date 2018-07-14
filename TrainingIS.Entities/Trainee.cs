using GApp.Core.MetaDatas.Attributes;
using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using TrainingIS.Entities.Resources.GroupResources;
using TrainingIS.Entities.Resources.PersonResources;
using TrainingIS.Entities.Resources.TraineeResources;

namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = true)]
    public class Trainee : BaseEntity
    {
        public override string ToString()
        {
            return this.FirstName + " " + this.LastName;
        }

        public override string CalculateReference()
        {
            string reference = "";
            if (!string.IsNullOrEmpty(this.CIN))
                reference = string.Format("{0}", this.CIN);
            return reference;
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

        [Display(Name = "Email", ResourceType = typeof(msg_Person))]
        public string Email { set; get; }

        [Display(Name = "Address", ResourceType = typeof(msg_Person))]
        public string Address { set; get; }

        [Display(Name = "FaceBook", ResourceType = typeof(msg_Person))]
        public string FaceBook { set; get; }

        [Display(Name = "WebSite", ResourceType = typeof(msg_Person))]
        public string WebSite { set; get; }

        // Trainee Information
        [Required]
        [Display(Name = "CNE", ResourceType = typeof(msg_Person))]
        public string CNE { set; get; }

        // Assignment

        [Display(Name = "SingularName", ResourceType = typeof(msg_Group))]
        public virtual Group Group { set; get; }

        [Required]
        public long GroupId { set; get; }


        // Business Data
        public bool EtudiantActif { set; get; }
        public bool Principale { set; get; }
        public bool EtudiantPayant { set; get; }
        public string LibelleLong { set; get; }
        public string CodeDiplome { set; get; }
        public DateTime? DateInscription { set; get; }
        public DateTime? DateDossierComplet { set; get; }

        public string LieuNaissance { set; get; }
        public string MotifAdmission { set; get; }
        public string NTel_du_Tuteur { set; get; }
        public string Nationalite { set; get; }

        public string Nom_Arabe { set; get; }
        public string Prenom_arabe { set; get; }
        public string NiveauScolaire { set; get; }
        public string AnneeEtude { set; get; }

        public  virtual List<StateOfAbsece> StateOfAbseces { set; get; }
    }
}

