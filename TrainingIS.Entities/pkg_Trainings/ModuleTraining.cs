using GApp.Models.DataAnnotations;
using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.Entities.Resources.AppResources;
using TrainingIS.Entities.Resources.SpecialtyResources;
using System.ComponentModel.DataAnnotations.Schema;
using TrainingIS.Entities.Base;
using TrainingIS.Entities.Resources.MetierResources;
using TrainingIS.Entities.Resources.YearStudyResources;
using TrainingIS.Entities.Resources.ModuleTrainingResources;

namespace TrainingIS.Entities
{
  
    [EntityMetataDataAttribute(isMale =true)]
    public class ModuleTraining : BaseEntity
    {
        public override string ToString()
        {
            return string.Format("{0}-{1}-{3} : {2}",this.Specialty?.Code,this.Code,this.Name,this.YearStudy) ;
        }
        public override string CalculateReference()
        {
            string reference = string.Format("{0}-{1}-{2}", this.Specialty.Code, this.Code, this.YearStudy);
            return reference;
        }

        // Specialty
        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_Specialty))]
        public virtual Specialty Specialty { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_Specialty))]
        public long SpecialtyId { set; get; }

        // Metier
        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_Metier))]
        public virtual Metier Metier { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_Metier))]
        public long MetierId { set; get; }

        // YearStudy
        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_YearStudy))]
        public virtual YearStudy YearStudy { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_YearStudy))]
        public long YearStudyId { set; get; }

        [Required]
        [Display(Name = "Name", AutoGenerateFilter = true, ResourceType = typeof(msg_app))]
        public string Name { get; set; }

        [Display(Name = "Code", ResourceType = typeof(msg_app))]
        public string Code { get; set; }

        [Display(Name = "HourlyMass", ResourceType = typeof(msg_ModuleTraining))]
        public float HourlyMass { get; set; }

        

        [Display(Name = "Hourly_Mass_To_Teach", ResourceType = typeof(msg_ModuleTraining))]
        public float Hourly_Mass_To_Teach {

            set
            {
                this._Hourly_Mass_To_Teach = value;
            }
            get
            {
                if (this._Hourly_Mass_To_Teach == 0)
                    return this.HourlyMass;
                else
                    return this._Hourly_Mass_To_Teach;
            }
        }
        private float _Hourly_Mass_To_Teach;

        [Display(Name = "Description", ResourceType = typeof(msg_app))]
        public string Description { set; get; }



    }
}
