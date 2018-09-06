using IdentitySample.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL;
using TrainingIS.WebApp.Controllers;

namespace TrainingIS.Context.Services
{
    public class GApp_Dev_Controllers_Context
    {
        private static List<Type> Controllers_Types = null;
        public List<Assembly> Get_Controller_Assemblies()
        {
            List<Assembly> Controller_assemblies = new List<Assembly>();

            // WebApp assembly
            Controller_assemblies.Add(typeof(TrainingsController).Assembly);

            return Controller_assemblies;
        }
        public Type Get_Controller_Type(Type EntityType)
        {
            if (Controllers_Types == null) this.Init_Controller_Types();
            Type Controller_Type = Controllers_Types.Where(type => type.Name == string.Format("{0}Controller", EntityType.Name.Pluralize())).FirstOrDefault();
            return Controller_Type;
        }
        public List<Type> Get_Controllers_Types()
        {
            if (Controllers_Types == null) this.Init_Controller_Types();
            return Controllers_Types;
        }
        private void Init_Controller_Types()
        {
            Controllers_Types = new List<Type>();
            foreach (var controller_assembly in this.Get_Controller_Assemblies())
            {
                var controller_types = controller_assembly
                    .GetTypes()
                    .Where(type => type.Name.EndsWith("Controller"))
                    .Where(type => !type.Name.StartsWith("Base")) ;
                Controllers_Types.AddRange(controller_types);
            }
        }
    }
}
