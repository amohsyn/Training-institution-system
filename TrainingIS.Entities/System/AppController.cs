using GApp.Core.MetaDatas.Attributes;
using GApp.Entities;
using GApp.WebApp.Manager.Views.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities.Resources.AppResources;
using TrainingIS.Entities.Resources.AppRoleResources;

namespace TrainingIS.Entities
{
 
    [EntityMetataData(isMale = false)]
    [CreateView(typeof(AppControllerFormView))]
    [EditView(typeof(AppControllerFormView))]
    [IndexView(typeof(AppControllerDetailsView))]
    [DetailsView(typeof(AppControllerDetailsView))]
    public partial class AppController : BaseEntity
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

        [Required]
        [Display(Name = "Code", ResourceType = typeof(msg_app))]
        public string Code { get; set; }

        [Display(Name = "Description", ResourceType = typeof(msg_app))]
        public string Description { set; get; }

        [Display(Name = "PluralName", ResourceType = typeof(msg_AppRole))]
        public virtual List<AppRole> AppRoles { set; get; }
   
    }
}
