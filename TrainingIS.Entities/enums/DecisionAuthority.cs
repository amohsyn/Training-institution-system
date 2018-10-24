using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Resources.SanctionCategoryResources;

namespace TrainingIS.Entities.enums
{
    [LocalizationEnum(typeof(msg_SanctionCategory))]
    public enum DecisionsAuthorities
    {

        No_decision,
        Sanction_Attendance_Per_GeneralSupervisor,
        Sanction_Attendance_Per_Administration,
        Sanction_Attendance_Per_Disciplinary_Council,

        Sanction_Behavior_Per_GeneralSupervisor,
        Sanction_Behavior_Per_Administration,
        Sanction_Behavior_Per_Disciplinary_Council,
    }
}
