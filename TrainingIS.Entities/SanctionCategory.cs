using GApp.Entities;
using GApp.Entities.Resources.AppResources;
using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.enums;
using TrainingIS.Entities.Resources.DisciplineCategoryResources;
using TrainingIS.Entities.Resources.SanctionCategoryResources;

namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = false)]
    public class SanctionCategory : BaseEntity
    {
        public override string ToString()
        {
            return this.Code;
        }
        public override string CalculateReference()
        {
            string reference = string.Format("{0}", this.Code);
            return reference;
        }


        // Discipline
        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_DisciplineCategory))]
        public virtual DisciplineCategory DisciplineCategory { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_DisciplineCategory))]
        public long DisciplineCategoryId { set; get; }

        [Required]
        [Display(Name = "Name", ResourceType = typeof(msg_app))]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Code", ResourceType = typeof(msg_app))]
        public string Code { get; set; }

        [Display(Name = "DecisionAuthority", ResourceType = typeof(msg_SanctionCategory))]
        public DecisionsAuthorities DecisionAuthority { get; set; }

        [Display(Name = "WorkflowOrder", ResourceType = typeof(msg_SanctionCategory))]
        public int WorkflowOrder { set; get; }

        [Display(Name = "Number_Of_Days_Of_Exclusion", ResourceType = typeof(msg_SanctionCategory))]
        public int Number_Of_Days_Of_Exclusion { set; get; }

        [Display(Name = "Plurality_Of_Absences", ResourceType = typeof(msg_SanctionCategory))]
        public int Plurality_Of_Absences { set; get; }

        [Display(Name = "Description", ResourceType = typeof(msg_app))]
        public string Description { set; get; }
    }
}
