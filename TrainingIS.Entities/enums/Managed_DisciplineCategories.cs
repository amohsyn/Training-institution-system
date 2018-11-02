using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Resources.DisciplineCategoryResources;

namespace TrainingIS.Entities.enums
{
    /// <summary>
    /// There are a table names DisciplineCategories to manage the user 
    /// DisciplineCategories
    /// this enum is for System managed DisciplineCategories
    /// </summary>
    [LocalizationEnum(typeof(msg_DisciplineCategory))]
    public enum System_DisciplineCategories
    {
        None,
        Attendance,
        Behavior,
    }
}
