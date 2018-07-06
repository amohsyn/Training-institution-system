using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using TrainingIS.Entities.Resources.GroupResources;
using TrainingIS.Entities.Resources.TraineeResources;

namespace TrainingIS.Entities
{
    public class Trainee : BaseEntity
    {
        public override string ToString()
        {
            return this.FirstName + " " + this.LastName;
        }

        // 
        // civil status
        //
        [Required]
        [Display(Name = "FirstName", ResourceType = typeof(msg_Trainee))]
        public string FirstName { set; get; }

        [Required]
        [Display(Name = "LastName", ResourceType = typeof(msg_Trainee))]
        public string LastName { set; get; }

        [Required]
        [Display(Name = "Sex", ResourceType = typeof(msg_Trainee))]
        public bool Sex { set; get; }

        [Display(Name = "CIN", ResourceType = typeof(msg_Trainee))]
        public string CIN { set; get; }


        // Contact information
        [Display(Name = "Cellphone", ResourceType = typeof(msg_Trainee))]
        public string Cellphone { set; get; }

        [Display(Name = "Email", ResourceType = typeof(msg_Trainee))]
        public string Email { set; get; }

        [Display(Name = "Address", ResourceType = typeof(msg_Trainee))]
        public string Address { set; get; }

        [Display(Name = "FaceBook", ResourceType = typeof(msg_Trainee))]
        public string FaceBook { set; get; }

        [Display(Name = "WebSite", ResourceType = typeof(msg_Trainee))]
        public string WebSite { set; get; }

        // Trainee Information
        [Required]
        [Display(Name = "CNE", ResourceType = typeof(msg_Trainee))]
        public string CNE { set; get; }

        // Assignment
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_Group))]
        public virtual Group Group { set; get; }
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

    }
}

