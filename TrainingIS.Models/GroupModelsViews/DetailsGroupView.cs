using GApp.Core.Entities.ModelsViews;
using GApp.Entities.Resources.AppResources;
using GApp.Models;
using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Resources.SpecialtyResources;
using TrainingIS.Entities.Resources.TrainingTypeResources;
using TrainingIS.Entities.Resources.YearStudyResources;

namespace TrainingIS.Entities.ModelsViews.GroupModelsViews
{
    [DetailsView(typeof(Group))]
    public class DetailsGroupView : BaseModel
    {
        public override string ToString()
        {
            return this.Code;
        }

        [Display(Name = "Code", ResourceType = typeof(msg_app))]
        public string Code { get; set; }

        [Display(Name = "SingularName", ResourceType = typeof(msg_YearStudy))]
        public virtual YearStudy YearStudy { set; get; }

        [Display(Name = "SingularName", ResourceType = typeof(msg_Specialty))]
        public virtual Specialty Specialty { set; get; }

        [Display(Name = "SingularName", ResourceType = typeof(msg_TrainingType))]
        public virtual TrainingType TrainingType { set; get; }


        [Display(Name = "Description", ResourceType = typeof(msg_app))]
        public string Desc
        {
            get
            {
                return this.Code + " " + this.YearStudy.Code;
            }
        }
        private string _Desc;
    }
}
