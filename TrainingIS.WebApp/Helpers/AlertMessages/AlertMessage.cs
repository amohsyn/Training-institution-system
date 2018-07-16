using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static TrainingIS.WebApp.Enums.Enums;

namespace TrainingIS.WebApp.Helpers.AlertMessages
{
    public class AlertMessage
    {
        public string message { set; get; }
        public NotificationType notificationType { set; get; }
    }
}