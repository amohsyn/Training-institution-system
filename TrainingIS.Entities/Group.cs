using GApp.Core.MetaDatas.Attributes;
using GApp.Entities;
using GApp.WebApp.Manager.Views.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Base;
using TrainingIS.Entities.ModelsViews.GroupModelsViews;
using TrainingIS.Entities.Resources.AppResources;
using TrainingIS.Entities.Resources.GroupResources;
using TrainingIS.Entities.Resources.SpecialtyResources;
using TrainingIS.Entities.Resources.TrainingTypeResources;
using TrainingIS.Entities.Resources.TrainingYearResources;
using TrainingIS.Entities.Resources.YearStudyResources;

namespace TrainingIS.Entities
{ 
    [EntityMetataData(isMale = true)]
    [Import(NotCompleteReference = true)]
    [IndexView(typeof(IndexGroupView))]
    [CreateView(typeof(CreateGroupView))]
    [EditView(typeof(EditGroupView))]
    [DetailsView(typeof(DetailsGroupView))]
    public partial class Group : NotCompleteReferenceEntity
    {
        public override string ToString()
        {
            return this.Code;
        }

        /// <summary>
        /// This role wil not be used, there are a onter role in GroupBLO.Save
        /// </summary>
        /// <returns></returns>
        public override string CalculateReference()
        {
            string reference = string.Format("{0}-{1}", this.Code,this.TrainingYear.Reference);
            return reference;
        }


        // TrainingType
        [Display(Name = "SingularName", ResourceType = typeof(msg_TrainingType))]
        public virtual TrainingType TrainingType { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_TrainingType))]
        public long TrainingTypeId { set; get; }

        // TrainingYear
        [Display(Name = "SingularName", ResourceType = typeof(msg_TrainingYear))]
        public virtual TrainingYear TrainingYear { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_TrainingYear))]
        public long TrainingYearId { set; get; }


        // Specialty
        [Display(Name = "SingularName", ResourceType = typeof(msg_Specialty))]
        public virtual Specialty Specialty { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_Specialty))]
        public long SpecialtyId { set; get; }

        // YearStudy
        [Display(Name = "SingularName", ResourceType = typeof(msg_YearStudy))]
        public virtual YearStudy YearStudy { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_YearStudy))]
        public long YearStudyId { set; get; }

        

  

    }
}
