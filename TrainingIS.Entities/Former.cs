using GApp.Core.MetaDatas.Attributes;
using GApp.Entities;
using GApp.WebApp.Manager.Views.Attributes;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Base;
using TrainingIS.Entities.ModelsViews.FormerModelsViews;
using TrainingIS.Entities.Resources.FormerResources;
using TrainingIS.Entities.Resources.PersonResources;
using TrainingIS.Entities.Resources.TraineeResources;




namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = true)]
    [CreateView(typeof(FormerFormView))]
    [EditView(typeof(FormerFormView))]
    [IndexView(typeof(FormerIndexView))]
    [DetailsView(typeof(FormerDetailsView))]
    public class Former : Person
    {
        public override string ToString()
        {
            return this.FirstName + " " + this.LastName;
        }

        public override string CalculateReference()
        {
            string reference = string.Format("{0}", this.RegistrationNumber);
            return base.CalculateReference();
        }

        //
        // JobInformation
        //
        [Required]
        [Unique]
        [Display(Name = "RegistrationNumber", GroupName = "JobInformation", Order = 30, ResourceType = typeof(msg_Former))]
        [StringLength(65)]
        [Index("IX_Former_RegistrationNumber", IsUnique = true)]
        public string RegistrationNumber { set; get; }

    }
}
