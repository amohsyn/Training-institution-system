using GApp.Entities.Resources.PersonResources;
using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace TrainingIS.Entities
{
    [LocalizationEnum(typeof(msg_Person))]
    public enum SexEnum
    {
        man,
        woman
    }
}
