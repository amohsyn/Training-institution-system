using GApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace GApp.Core.Utils
{
    /// <summary>
    /// Loading ressouce manager helper
    /// </summary>
    public class RessoucesManagerUtil
    {

        /// <summary>
        /// Get the ressouce manager by Type of Entity
        /// </summary>
        /// <param name="singlulareEntityName">Type of  Entity </param>
        /// <returns>Ressouce Manager</returns>
        public static ResourceManager FindEntityRessouceManager(Type EntityType)
        {

            string[] resoucesnames = EntityType
                .Assembly
                .GetManifestResourceNames();

            List<string> ls_resources_names = resoucesnames
                .Where(n => n.Contains(".msg_" + EntityType.Name + ".resources")).ToList<string>();


           


            // Check uniqueness of Ressouce name
            if (ls_resources_names.Count > 1)
                throw new GAppException(string.Format("There are many ressouce with the name {0} in Asseembly {1}", EntityType.Name, EntityType.Assembly.FullName));

            if (ls_resources_names.Count == 1)
            {
                string RessouceFullName = ls_resources_names.First();
                return new ResourceManager(RessouceFullName.Replace(".resources", ""), EntityType.Assembly);
            }


            return null;
        }


        /// <summary>
        /// Set Ressouce Manager of the entity and All its BaseEntity
        /// </summary>
        public static void FillResourcesManager(Type type_entity, Dictionary<string, ResourceManager> RessoucesManagers)
        {

            //System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(type_entity);



            ResourceManager EntityResouceManager = FindEntityRessouceManager(type_entity);

            if (EntityResouceManager != null)
                RessoucesManagers.Add(type_entity.Name, EntityResouceManager);
            else
            {
                // [Log]
                // MessageToUser.AddMessage(MessageToUser.Category.BusinessRule, "The resource file does not exist : " + RessouceFullName);
                return;
            }

            if (type_entity.BaseType != typeof(object))
                FillResourcesManager(type_entity.BaseType, RessoucesManagers);

        }

    }
}
