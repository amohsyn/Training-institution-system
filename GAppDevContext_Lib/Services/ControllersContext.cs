using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL;

namespace TrainingIS.Context.Services
{
    public class ControllersContext
    {
        private static List<Type> Controllers_Types = null;
        public List<Assembly> Get_Controller_Assemblies()
        {
            List<Assembly> Controller_assemblies = new List<Assembly>();

            // BLO assembly
            Controller_assemblies.Add(typeof(GroupBLO).Assembly);

            return Controller_assemblies;
        }
        public Type Get_Controller_Type(Type EntityType)
        {
            if (Controllers_Types == null) this.Init_Controller_Types();
            Type Controller_Type = Controllers_Types.Where(type => type.Name == string.Format("{0}Controller", EntityType.Name.Pluralize())).FirstOrDefault();
            return Controller_Type;
        }
        private void Init_Controller_Types()
        {
            Controllers_Types = new List<Type>();
            foreach (var controller_assembly in this.Get_Controller_Assemblies())
            {
                var controller_types = controller_assembly.GetTypes().Where(type => type.Name.EndsWith("Controller"));
                Controllers_Types.AddRange(controller_types);
            }

        }


        private void Add_Not_Default_Controllers()
        {
            ////ControllersTypes.Add(typeof(CplusController));
            ////ControllersTypes.Add(typeof(BackupDataController));
            ////ControllersTypes.Add(typeof(ProfileManagerController));
            ////ControllersTypes.Add(typeof(ReportsController));
            ////ControllersTypes.Add(typeof(ManageController));
            ////ControllersTypes.Add(typeof(UsersAdminController));
        }
    }
}
