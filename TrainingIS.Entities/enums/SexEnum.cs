using GApp.Core.MetaDatas.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Resources.PersonResources;

namespace TrainingIS.Entities
{
    [LocalizationEnum(typeof(msg_Person))]
    public enum SexEnum
    {
        man,
        woman
    }
}
