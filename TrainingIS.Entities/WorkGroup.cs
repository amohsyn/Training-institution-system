using GApp.Entities;
using GApp.Entities.Resources.AppResources;
using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Base;
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
        [GAppDataTable(isColumn =false)]
        [Display(Name = "President_Former", GroupName = "President", Order = 1, ResourceType = typeof(msg_WorkGroup))]
        public virtual Former President_Former { set; get; }

        [GAppDataTable(isColumn = false)]
        [Display(Name = "President_Trainee", GroupName = "President", Order = 2, ResourceType = typeof(msg_WorkGroup))]
        public virtual Trainee President_Trainee { set; get; }

        [GAppDataTable(isColumn = false)]
        [Display(Name = "President_Administrator", GroupName = "President", Order = 3, ResourceType = typeof(msg_WorkGroup))]
        public virtual Administrator President_Administrator { set; get; }
        #endregion

        #region VicePresident
        // 
        // The VicePresident present can be Former, Administrator or Trainee
        //
        [GAppDataTable(isColumn = false)]
        [Display(Name = "VicePresident_Former", GroupName = "VicePresident", Order = 1, ResourceType = typeof(msg_WorkGroup))]
        public virtual Former VicePresident_Former { set; get; }

        [GAppDataTable(isColumn = false)]
        [Display(Name = "VicePresident_Trainee", GroupName = "VicePresident", Order = 2, ResourceType = typeof(msg_WorkGroup))]
        public virtual Trainee VicePresident_Trainee { set; get; }

        [GAppDataTable(isColumn = false)]
        [Display(Name = "VicePresident_Administrator", GroupName = "VicePresident", Order = 3, ResourceType = typeof(msg_WorkGroup))]
        public virtual Administrator VicePresident_Administrator { set; get; }
        #endregion

        #region Protractor
        // 
        // The VicePresident present can be Former, Administrator or Trainee
        //
        [GAppDataTable(isColumn = false)]
        [Display(Name = "Protractor_Former", GroupName = "Protractor", Order = 1, ResourceType = typeof(msg_WorkGroup))]
        public virtual Former Protractor_Former { set; get; }

        [GAppDataTable(isColumn = false)]
        [Display(Name = "Protractor_Administrator", GroupName = "Protractor", Order = 2, ResourceType = typeof(msg_WorkGroup))]
        public virtual Administrator Protractor_Administrator { set; get; }

        [GAppDataTable(isColumn = false)]
        [Display(Name = "Protractor_Trainee", GroupName = "Protractor", Order = 3, ResourceType = typeof(msg_WorkGroup))]
        public virtual Trainee Protractor_Trainee { set; get; }
        #endregion


        #region Member
        //
        // Memeber
        //
        [GAppDataTable(isColumn = false)]
        [Many(userInterfaces = UserInterfaces.MultiSelect, TypeOfEntity = typeof(Former))]
        [Display(Name = "MemebersFormers", GroupName = "Members", Order = 1, ResourceType = typeof(msg_WorkGroup))]
        public virtual List<Former> MemebersFormers { get; set; }

        [GAppDataTable(isColumn = false)]
        [Many(userInterfaces = UserInterfaces.MultiSelect, TypeOfEntity = typeof(Administrator))]
        [Display(Name = "MemebersAdministrators", GroupName = "Members", Order = 2, ResourceType = typeof(msg_WorkGroup))]
        public virtual List<Administrator> MemebersAdministrators { get; set; }

        [GAppDataTable(isColumn = false)]
        [Many(userInterfaces = UserInterfaces.MultiSelect, TypeOfEntity = typeof(Trainee))]
        [Display(Name = "MemebersTrainees", GroupName = "Members", Order = 3, ResourceType = typeof(msg_WorkGroup))]
        public virtual List<Trainee> MemebersTrainees { get; set; }

        #endregion

        #region Guest
        // existance of guests
        [GAppDataTable(isColumn = false)]
        [Display(Name = "GuestFormers", GroupName = "Guests", Order = 1, ResourceType = typeof(msg_WorkGroup))]
        public bool GuestFormers { set; get; }

        [GAppDataTable(isColumn = false)]
        [Display(Name = "GuestTrainees", GroupName = "Guests", Order = 2, ResourceType = typeof(msg_WorkGroup))]
        public bool GuestTrainees { set; get; }

        [GAppDataTable(isColumn = false)]
        [Display(Name = "GuestAdministrator", GroupName = "Guests", Order = 3, ResourceType = typeof(msg_WorkGroup))]
        public bool GuestAdministrator { set; get; }
        #endregion

        [Many(userInterfaces = UserInterfaces.MultiSelect,  TypeOfEntity = typeof(msg_Mission_Working_Group))]
        [Display(Name = "Missions", GroupName = "Missions", Order = 1, ResourceType = typeof(msg_WorkGroup))]
        public virtual List<Mission_Working_Group> Mission_Working_Groups { get; set; }

        [NotMapped]
        public Person President {
            get
            {
                if(this.President_Administrator != null)
                {
                    return this.President_Administrator;
                }
                else
                {
                    if (this.President_Former != null)
                        return this.President_Former;
                    else
                    {
                        return this.President_Trainee;
                    }
                }
            }
        }

        public Person VicePresident
        {
            get
            {
                if (this.VicePresident_Administrator != null)
                {
                    return this.VicePresident_Administrator;
                }
                else
                {
                    if (this.VicePresident_Former != null)
                        return this.VicePresident_Former;
                    else
                    {
                        return this.VicePresident_Trainee;
                    }
                }
            }
        }

        public Person Protractor
        {
            get
            {
                if (this.Protractor_Administrator != null)
                {
                    return this.Protractor_Administrator;
                }
                else
                {
                    if (this.Protractor_Former != null)
                        return this.Protractor_Former;
                    else
                    {
                        return this.Protractor_Trainee;
                    }
                }
            }
        }
    }
}
