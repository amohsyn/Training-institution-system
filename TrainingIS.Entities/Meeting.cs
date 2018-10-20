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

        [Display(Name = "MeetingDate", ResourceType = typeof(msg_Meeting))]
        public DateTime MeetingDate { set; get; }

        // WorkGroup
        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_WorkGroup))]
        public virtual WorkGroup WorkGroup { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_WorkGroup))]
        public long WorkGroupId { set; get; }

        // Mission_Working_Group
        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_Mission_Working_Group))]
        public virtual Mission_Working_Group Mission_Working_Group { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_Mission_Working_Group))]
        public long Mission_Working_GroupId { set; get; }

    
        [Display(Name = "Presence_Of_President", ResourceType = typeof(msg_Meeting))]
        public bool Presence_Of_President { set; get; }

        [Display(Name = "Presence_Of_VicePresident", ResourceType = typeof(msg_Meeting))]
        public bool Presence_Of_VicePresident { set; get; }

        [Display(Name = "Presence_Of_Protractor", ResourceType = typeof(msg_Meeting))]
        public bool Presence_Of_Protractor { set; get; }

 
        // 
        // Presence of Memebers
        //
        [Many(userInterfaces = UserInterfaces.MultiSelect, TypeOfEntity = typeof(Former))]
        [Display(Name = "Presences_Of_Formers", ResourceType = typeof(msg_Meeting))]
        public virtual List<Former> Presences_Of_Formers { get; set; }

        [Many(userInterfaces = UserInterfaces.MultiSelect, TypeOfEntity = typeof(Administrator))]
        [Display(Name = "Presences_Of_Administrators", ResourceType = typeof(msg_Meeting))]
        public virtual List<Administrator> Presences_Of_Administrators { get; set; }

        [Many(userInterfaces = UserInterfaces.MultiSelect, TypeOfEntity = typeof(Trainee))]
        [Display(Name = "Presences_Of_Trainees", ResourceType = typeof(msg_Meeting))]
        public virtual List<Trainee> Presences_Of_Trainees { get; set; }


        // 
        // Presences of Guest
        //
        [Many(userInterfaces = UserInterfaces.MultiSelect, TypeOfEntity = typeof(Former))]
        [Display(Name = "Presences_Of_Guests_Formers", ResourceType = typeof(msg_Meeting))]
        public virtual List<Former> Presences_Of_Guests_Formers { get; set; }

        [Many(userInterfaces = UserInterfaces.MultiSelect, TypeOfEntity = typeof(Administrator))]
        [Display(Name = "Presences_Of_Guests_Administrators", ResourceType = typeof(msg_Meeting))]
        public virtual List<Administrator> Presences_Of_Guests_Administrators { get; set; }

        [Many(userInterfaces = UserInterfaces.MultiSelect, TypeOfEntity = typeof(Trainee))]
        [Display(Name = "Presences_Of_Guests_Trainees", ResourceType = typeof(msg_Meeting))]
        public virtual List<Trainee> Presences_Of_Guests_Trainees { get; set; }

        [Display(Name = "Description", ResourceType = typeof(msg_app))]
        public string Description { set; get; }
    }
}
