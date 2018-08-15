
using GApp.Dev.Generator.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL;
using TrainingIS.Context.Services;
using TrainingIS.Entities.ModelsViews.FormerModelsViews;

namespace TrainingIS.Context
{
    public class GAppDevContext : IGAppDevContext
    {
        public ControllersContext Controlers { set; get; }
        public BLO_Context BLO_Context { set; get; }

        public GAppDevContext()
        {
            Controlers = new ControllersContext();
            BLO_Context = new BLO_Context();
        }

        #region Models Types
        public List<Assembly> Get_All_Solution_Assemblies()
        {
            List<Assembly> assemblies = new List<Assembly>();

            // Models assembly
            assemblies.Add(typeof(FormerDetailsView).Assembly);

            // BLO assembly
            assemblies.Add(typeof(GroupBLO).Assembly);

            return assemblies;
        }
        #endregion


  


    }
}
