using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Resources.AbsenceResources;

namespace TrainingIS.Entities.enums
{
    [LocalizationEnum(typeof(msg_Absence))]
    public enum AbsenceStates
    {
        InValid_Absence,
        Valid_Absence,
        Justified_Absence,
        Sanctioned_Absence
    }
}
