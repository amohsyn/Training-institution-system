using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.WebApp.Controllers;

namespace TrainingIS.WebApp.Manager.Controller
{
    public partial class Controllers_MetaData
    {
        public List<Type> ControllersTypes { set; get; }

        public Controllers_MetaData()
        {
            ControllersTypes = new List<Type>();
            this.Add_Default_Controllers();

            this.Add_Not_Default_Controllers();
        }

        private void Add_Not_Default_Controllers()
        {
            ControllersTypes.Add(typeof(CplusController));
            ControllersTypes.Add(typeof(BackupDataController));
            ControllersTypes.Add(typeof(ProfileManagerController));
            ControllersTypes.Add(typeof(ReportsController));
        }
    }
}
