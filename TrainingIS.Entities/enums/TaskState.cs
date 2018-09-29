using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Resources.TaskProjectResources;

namespace TrainingIS.Entities.enums
{
    [LocalizationEnum(typeof(msg_TaskProject))]
    public enum TaskStates
    {
        Waiting,
        Started,
        Completed,
        OnBreak
    }
}
