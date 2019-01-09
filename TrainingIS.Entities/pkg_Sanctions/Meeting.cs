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
using TrainingIS.Entities.Resources.MeetingResources;
using TrainingIS.Entities.Resources.Mission_Working_GroupResources;
using TrainingIS.Entities.Resources.TraineeResources;
using TrainingIS.Entities.Resources.WorkGroupResources;

namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = false)]
    public class Meeting : BaseEntity
    {
        public override string ToString()
        {
            return string.Format("{0} : {1}-{2}",this.WorkGroup?.Code, this.Mission_Working_Group?.Name,this.MeetingDate.ToShortDateString());
        }

        #region Object

        [Display(Name = "MeetingDate", GroupName ="Object", Order =1, ResourceType = typeof(msg_Meeting))]
        public DateTime MeetingDate { set; get; }

        // WorkGroup
        [Display(Name = "WorkGroup", GroupName = "Object", Order = 2, AutoGenerateFilter = true, ResourceType = typeof(msg_Meeting))]
        public virtual WorkGroup WorkGroup { set; get; }
        [Required]
        [Display(Name = "WorkGroup", GroupName = "Object", Order = 2, ResourceType = typeof(msg_Meeting))]
        public long WorkGroupId { set; get; }

        // Mission_Working_Group
        [Display(Name = "Mission_Working_Group", GroupName = "Object", Order = 3, AutoGenerateFilter = true, ResourceType = typeof(msg_Meeting))]
        public virtual Mission_Working_Group Mission_Working_Group { set; get; }
        [Required]
        [Display(Name = "Mission_Working_Group", GroupName = "Object", Order = 3, ResourceType = typeof(msg_Meeting))]
        public long Mission_Working_GroupId { set; get; }

        [RichText]
        [Display(Name = "Description", GroupName = "Object", Order = 4 , ResourceType = typeof(msg_Meeting))]
        public string Description { set; get; }
        #endregion

        #region Members
        [Display(Name = "Presence_Of_President", GroupName = "Members", Order = 1, ResourceType = typeof(msg_Meeting))]
        public bool Presence_Of_President { set; get; }

        [Display(Name = "Presence_Of_VicePresident", GroupName = "Members", Order = 2, ResourceType = typeof(msg_Meeting))]
        public bool Presence_Of_VicePresident { set; get; }

        [Display(Name = "Presence_Of_Protractor", GroupName = "Members", Order = 3, ResourceType = typeof(msg_Meeting))]
        public bool Presence_Of_Protractor { set; get; }


        // 
        // Presence of Memebers
        //
        [Many(userInterfaces = UserInterfaces.MultiSelect, TypeOfEntity = typeof(Former))]
        [Display(Name = "Presences_Of_Formers", GroupName = "Members", Order = 4, ResourceType = typeof(msg_Meeting))]
        public virtual List<Former> Presences_Of_Formers { get; set; }

        [Many(userInterfaces = UserInterfaces.MultiSelect, TypeOfEntity = typeof(Administrator))]
        [Display(Name = "Presences_Of_Administrators", GroupName = "Members", Order = 5, ResourceType = typeof(msg_Meeting))]
        public virtual List<Administrator> Presences_Of_Administrators { get; set; }

        [Many(userInterfaces = UserInterfaces.MultiSelect, TypeOfEntity = typeof(Trainee))]
        [Display(Name = "Presences_Of_Trainees", GroupName = "Members", Order = 6, ResourceType = typeof(msg_Meeting))]
        public virtual List<Trainee> Presences_Of_Trainees { get; set; }
        #endregion


        #region Presences_Guest

        // 
        // Presences of Guest
        //
        [Many(userInterfaces = UserInterfaces.MultiSelect, TypeOfEntity = typeof(Former))]
        [Display(Name = "Presences_Of_Guests_Formers", GroupName = "Presences_Guest", Order = 6, ResourceType = typeof(msg_Meeting))]
        public virtual List<Former> Presences_Of_Guests_Formers { get; set; }

        [Many(userInterfaces = UserInterfaces.MultiSelect, TypeOfEntity = typeof(Administrator))]
        [Display(Name = "Presences_Of_Guests_Administrators", GroupName = "Presences_Guest", Order = 6, ResourceType = typeof(msg_Meeting))]
        public virtual List<Administrator> Presences_Of_Guests_Administrators { get; set; }

        [Many(userInterfaces = UserInterfaces.MultiSelect, TypeOfEntity = typeof(Trainee))]
        [Display(Name = "Presences_Of_Guests_Trainees", GroupName = "Presences_Guest", Order = 6, ResourceType = typeof(msg_Meeting))]
        public virtual List<Trainee> Presences_Of_Guests_Trainees { get; set; }
        #endregion

        [Display(AutoGenerateField =false)]
        public virtual List<Sanction> Sanctions { set; get; }
    }
}
