using GApp.Entities;
using GApp.Entities.Resources.AppResources;
using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Resources.AdministratorResources;
using TrainingIS.Entities.Resources.FormerResources;
using TrainingIS.Entities.Resources.Mission_Working_GroupResources;
using TrainingIS.Entities.Resources.TraineeResources;
using TrainingIS.Entities.Resources.WorkGroupResources;

namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = false)]
    public class WorkGroup : BaseEntity
    {
        #region ToString and References
        public override string ToString()
        {
            return this.Code;
        }
        public override string CalculateReference()
        {
            string reference = string.Format("{0}", this.Code);
            return reference;
        }
        #endregion

       

        [Required]
        [Display(Name = "Name", ResourceType = typeof(msg_app))]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Code", ResourceType = typeof(msg_app))]
        public string Code { get; set; }

        // 
        // The present can be Former, Administrator or Trainee
        //
        [Display(Name = "President", ResourceType = typeof(msg_WorkGroup))]
        public Former President_Former { set; get; }

        [Display(Name = "President", ResourceType = typeof(msg_WorkGroup))]
        public Trainee President_Trainee { set; get; }

        [Display(Name = "President", ResourceType = typeof(msg_WorkGroup))]
        public Administrator President_Administrator { set; get; }

        // 
        // The VicePresident present can be Former, Administrator or Trainee
        //
        [Display(Name = "VicePresident", ResourceType = typeof(msg_WorkGroup))]
        public Former VicePresident_Former { set; get; }
        [Display(Name = "VicePresident", ResourceType = typeof(msg_WorkGroup))]
        public Trainee VicePresident_Trainee { set; get; }
        [Display(Name = "VicePresident", ResourceType = typeof(msg_WorkGroup))]
        public Administrator VicePresident_Administrator { set; get; }

        // 
        // The VicePresident present can be Former, Administrator or Trainee
        //
        [Display(Name = "Protractor", ResourceType = typeof(msg_WorkGroup))]
        public Former Protractor_Former { set; get; }
        [Display(Name = "Protractor", ResourceType = typeof(msg_WorkGroup))]
        public Trainee Protractor_Trainee { set; get; }
        [Display(Name = "Protractor", ResourceType = typeof(msg_WorkGroup))]
        public Administrator Protractor_Administrator { set; get; }

        // 
        // Memeber
        //
        [Many(userInterfaces = UserInterfaces.MultiSelect, TypeOfEntity = typeof(Former))]
        [Display(Name = "MemebersFormers", ResourceType = typeof(msg_WorkGroup))]
        public virtual List<Former> MemebersFormers { get; set; }

        [Many(userInterfaces = UserInterfaces.MultiSelect, TypeOfEntity = typeof(Administrator))]
        [Display(Name = "MemebersAdministrators", ResourceType = typeof(msg_WorkGroup))]
        public virtual List<Administrator> MemebersAdministrators { get; set; }

        [Many(userInterfaces = UserInterfaces.MultiSelect, TypeOfEntity = typeof(Trainee))]
        [Display(Name = "MemebersTrainees", ResourceType = typeof(msg_WorkGroup))]
        public virtual List<Trainee> MemebersTrainees { get; set; }

        // existance of guests
        [Display(Name = "GuestFormers", ResourceType = typeof(msg_WorkGroup))]
        public bool GuestFormers { set; get; }
        [Display(Name = "GuestTrainees", ResourceType = typeof(msg_WorkGroup))]
        public bool GuestTrainees { set; get; }
        [Display(Name = "GuestAdministrator", ResourceType = typeof(msg_WorkGroup))]
        public bool GuestAdministrator { set; get; }

        [Many(userInterfaces = UserInterfaces.MultiSelect, TypeOfEntity = typeof(msg_Mission_Working_Group))]
        [Display(Name = "PluralName", ResourceType = typeof(msg_Mission_Working_Group))]
        public virtual List<Mission_Working_Group> Mission_Working_Groups { get; set; }
  
        [Display(Name = "Description", ResourceType = typeof(msg_app))]
        public string Description { set; get; }

    }
}
