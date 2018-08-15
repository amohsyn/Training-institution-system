using GApp.BLL;
using GApp.Dev.Generator.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews.FormerModelsViews;

namespace TrainingIS.BLL.Services
{
    [Obsolete("Use the class in the library TrainingIS.Context ")]
    public class GAppDevContext : IGAppDevContext
    {

        #region Models Types
        public List<Assembly> Get_All_Solution_Assemblies()
        {
            List<Assembly> assemblies = new List<Assembly>();

            // Models assembly
            assemblies.Add(typeof(FormerDetailsView).Assembly);

            // BLO assembly
            assemblies.Add(typeof(GAppDevContext).Assembly);

            return assemblies;
        }
        #endregion


      
        private static List<Type> Controllers_Types = null;


      

       

        #region BLO Types
        private static List<Type> BLO_Types = null;
        public List<Assembly> Get_BLO_Assemblies()
        {
            List<Assembly> BLO_assemblies = new List<Assembly>();

            // BLO assembly
            BLO_assemblies.Add(typeof(GAppDevContext).Assembly);

            return BLO_assemblies;
        }
        public Type Get_BLO_Type(Type EntityType)
        {
            if (BLO_Types == null) this.Init_BLO_Types();
            Type BLO_Type = BLO_Types.Where(type => type.Name == string.Format("{0}BLO", EntityType.Name)).FirstOrDefault();
            return BLO_Type;
        }
        private void Init_BLO_Types()
        {
            BLO_Types = new List<Type>();
            foreach (var blo_assembly in this.Get_BLO_Assemblies())
            {
                var blo_types = blo_assembly.GetTypes().Where(type => type.Name.EndsWith("BLO"));
                BLO_Types.AddRange(blo_types);
            }
            
        }
        #endregion

    


    }
}
