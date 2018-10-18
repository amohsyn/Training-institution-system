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
        // surveillant générale
        GeneralSupervisor,

        // Administration
        Administration,

        // conseil disciplinaire  
        Disciplinary_Council_Trainee,


        Disciplinary_Council_Trainees,

        Disciplinary_Council_Absences_Of_Trainee,
    }
}
