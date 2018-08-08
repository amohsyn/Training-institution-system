using GApp.Core.Entities.ModelsViews;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TrainingIS.Entities.Resources.AppResources;
using GApp.Core.MetaDatas.Attributes;
using TrainingIS.Entities.Resources.FormerResources;
using TrainingIS.Entities.Resources.PersonResources;
using TrainingIS.Entities.Resources.NationalityResources;

namespace TrainingIS.Entities.ModelsViews.FormerModelsViews
{

    public class FormerIndexView : BaseModelView
    {
        [Required]
        [Unique]
        [Display(Name = "RegistrationNumber", ResourceType = typeof(msg_Former))]
        public String RegistrationNumber { set; get; }

        [Required]
        [Display(Name = "FirstName", ResourceType = typeof(msg_Person))]
        public String FirstName { set; get; }

        [Required]
        [Display(Name = "LastName", ResourceType = typeof(msg_Person))]
        public String LastName { set; get; }


        [Display(Name = "Cellphone", ResourceType = typeof(msg_Person))]
        public String Cellphone { set; get; }

        [Required]
        [Display(Name = "Email", ResourceType = typeof(msg_Person))]
        [DataType(DataType.EmailAddress)]
        public String Email { set; get; }

    }
}
