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


        #region Designation
        [Required]
        [Display(Name = "Name",GroupName = "Designation",Order =1, ResourceType = typeof(msg_WorkGroup))]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Code", GroupName = "Designation", Order = 2, ResourceType = typeof(msg_WorkGroup))]
        public string Code { get; set; }

        [Display(Name = "Description", GroupName = "Designation", Order = 3,ResourceType = typeof(msg_WorkGroup))]
        public string Description { set; get; }
        #endregion

        #region President
        // 
        // The present can be Former, Administrator or Trainee
        //
        [Display(Name = "President_Former", GroupName = "President", Order = 1, ResourceType = typeof(msg_WorkGroup))]
        public Former President_Former { set; get; }

        [Display(Name = "President_Trainee", GroupName = "President", Order = 2, ResourceType = typeof(msg_WorkGroup))]
        public Trainee President_Trainee { set; get; }

        [Display(Name = "President_Administrator", GroupName = "President", Order = 3, ResourceType = typeof(msg_WorkGroup))]
        public Administrator President_Administrator { set; get; }
        #endregion

        #region VicePresident
        // 
        // The VicePresident present can be Former, Administrator or Trainee
        //
        [Display(Name = "VicePresident_Former", GroupName = "VicePresident", Order = 1, ResourceType = typeof(msg_WorkGroup))]
        public Former VicePresident_Former { set; get; }
        [Display(Name = "VicePresident_Trainee", GroupName = "VicePresident", Order = 2, ResourceType = typeof(msg_WorkGroup))]
        public Trainee VicePresident_Trainee { set; get; }
        [Display(Name = "VicePresident_Administrator", GroupName = "VicePresident", Order = 3, ResourceType = typeof(msg_WorkGroup))]
        public Administrator VicePresident_Administrator { set; get; }
        #endregion

        #region Protractor
        // 
        // The VicePresident present can be Former, Administrator or Trainee
        //
        [Display(Name = "Protractor_Former", GroupName = "Protractor", Order = 1, ResourceType = typeof(msg_WorkGroup))]
        public Former Protractor_Former { set; get; }

        [Display(Name = "Protractor_Administrator", GroupName = "Protractor", Order = 2, ResourceType = typeof(msg_WorkGroup))]
        public Administrator Protractor_Administrator { set; get; }

        [Display(Name = "Protractor_Trainee", GroupName = "Protractor", Order = 3, ResourceType = typeof(msg_WorkGroup))]
        public Trainee Protractor_Trainee { set; get; }
        #endregion
   

        #region Member
        //
        // Memeber
        //
        [Many(userInterfaces = UserInterfaces.MultiSelect, TypeOfEntity = typeof(Former))]
        [Display(Name = "MemebersFormers", GroupName = "Members", Order = 1, ResourceType = typeof(msg_WorkGroup))]
        public virtual List<Former> MemebersFormers { get; set; }

        [Many(userInterfaces = UserInterfaces.MultiSelect, TypeOfEntity = typeof(Administrator))]
        [Display(Name = "MemebersAdministrators", GroupName = "Members", Order = 2, ResourceType = typeof(msg_WorkGroup))]
        public virtual List<Administrator> MemebersAdministrators { get; set; }

        [Many(userInterfaces = UserInterfaces.MultiSelect, TypeOfEntity = typeof(Trainee))]
        [Display(Name = "MemebersTrainees", GroupName = "Members", Order = 3, ResourceType = typeof(msg_WorkGroup))]
        public virtual List<Trainee> MemebersTrainees { get; set; }

        #endregion

        #region Guest
        // existance of guests
        [Display(Name = "GuestFormers", GroupName = "Guests", Order = 1, ResourceType = typeof(msg_WorkGroup))]
        public bool GuestFormers { set; get; }
        [Display(Name = "GuestTrainees", GroupName = "Guests", Order = 2, ResourceType = typeof(msg_WorkGroup))]
        public bool GuestTrainees { set; get; }
        [Display(Name = "GuestAdministrator", GroupName = "Guests", Order = 3, ResourceType = typeof(msg_WorkGroup))]
        public bool GuestAdministrator { set; get; }
        #endregion


        [Many(userInterfaces = UserInterfaces.MultiSelect,  TypeOfEntity = typeof(msg_Mission_Working_Group))]
        [Display(Name = "Missions", GroupName = "Missions", Order = 1, ResourceType = typeof(msg_WorkGroup))]
        public virtual List<Mission_Working_Group> Mission_Working_Groups { get; set; }
  
     

    }
}
