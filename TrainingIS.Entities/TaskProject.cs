using GApp.Entities;
using GApp.Entities.Resources.AppResources;
using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entitie_excludes;
using TrainingIS.Entities.Base;
using TrainingIS.Entities.enums;
using TrainingIS.Entities.Resources.ProjectResources;
using TrainingIS.Entities.Resources.TaskProjectResources;

namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = false)]
    public class TaskProject : BaseTaskProject
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

        // Project
        [Display(Name = "SingularName", AutoGenerateFilter =true, ResourceType = typeof(msg_Project))]
        public virtual Project Project { set; get; }
        [Display(Name = "SingularName", ResourceType = typeof(msg_Project))]
        public Int64 ProjectId { set; get; }


        [Display(Name = "TaskState", AutoGenerateFilter =true, ResourceType = typeof(msg_TaskProject))]
        public TaskStates TaskState { set; get; }


    }
}
