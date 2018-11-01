using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Resources.SanctionResources;

namespace TrainingIS.Entities.enums
{
    [LocalizationEnum(typeof(msg_Sanction))]
    public enum SanctionStates
    {
        InValid,
        Valid
    }
}
