using GApp.Core.MetaDatas.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrainingIS.WebApp.Manager.Views.msgs;

namespace TrainingIS.WebApp.Enums
{
   
    public class Enums
    {
        [LocalizationEnum(typeof(msgManager))]
        public enum NotificationType
        {
            error,
            success,
            warning,
            info
        }
    }
}