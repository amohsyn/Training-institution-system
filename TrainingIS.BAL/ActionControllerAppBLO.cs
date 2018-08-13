using GApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;

namespace TrainingIS.BLL
{
    public partial class ActionControllerAppBLO
    {
        public ActionControllerApp Find_by_ControllerId_And_ActionReference(long ControllerAppId, string ActionControllerAppReference)
        {
            Dictionary<String, object> Filter = new Dictionary<string, object>();
            Filter.Add(nameof(ActionControllerApp.ControllerApp), ControllerAppId);
            Filter.Add(nameof(ActionControllerApp.Reference), ActionControllerAppReference);
            ActionControllerApp searched = this.FindAll(Filter, null).FirstOrDefault();
            return searched;
        }
    }
}
