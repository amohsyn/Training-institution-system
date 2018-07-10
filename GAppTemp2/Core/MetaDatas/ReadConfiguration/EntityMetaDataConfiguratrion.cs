using GApp.Core.MetaDatas.Attributes;
using GApp.Core.Utils;
using GApp.Entities;
using GApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GApp.Core.MetaDatas.ReadConfiguration
{
    /// <summary>
    /// Entity Meta Data Loader
    /// </summary>
    public class EntityMetaDataConfiguratrion
    {

        #region Attributes Instance  
        public EntityMetataDataAttribute entityMetataDataAttribute { set; get; }
        #endregion


        #region Presentation Variables
        /// <summary>
        /// Culture Info
        /// </summary>
        public CultureInfo CultureInfo { get; set; }

        /// <summary>
        /// Entity Ressource manager
        /// </summary>
        public Dictionary<string, ResourceManager> RessourcesManagers { get; set; }
        ResourceManager baseEntityResourceManager = null;
        #endregion

        #region Business Variables
        /// <summary>
        /// Type of Entity 
        /// </summary>
        public Type TypeOfEntity { set; get; }
        /// <summary>
        /// Indicate if the entity is localizable
        /// </summary>
        public bool Localizable { get; set; }

        #endregion

        #region Data Variables
        /// <summary>
        /// List of Configuration instances, its saves to minim time of traitement
        /// </summary>
        private static Dictionary<Type, EntityMetaDataConfiguratrion> ConfigurationOfEntities { get; set; }
        #endregion


        private EntityMetaDataConfiguratrion(Type type_of_entity)
        {
            this.TypeOfEntity = type_of_entity;
            this.CultureInfo = Thread.CurrentThread.CurrentCulture;

            // Create Attribute Instances
            this.ReadConfiguration();
        }

        /// <summary>
        /// Create Singeloton ConfigEntity per TypeOfEntity
        /// </summary>
        /// <param name="type_of_entity"></param>
        /// <returns></returns>
        public static EntityMetaDataConfiguratrion CreateConfigEntity(Type type_of_entity)
        {
            if (ConfigurationOfEntities == null)
                ConfigurationOfEntities = new Dictionary<Type, EntityMetaDataConfiguratrion>();

            if (!ConfigurationOfEntities.Keys.Contains(type_of_entity))
                ConfigurationOfEntities[type_of_entity] = new EntityMetaDataConfiguratrion(type_of_entity);

            return ConfigurationOfEntities[type_of_entity];
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadRessouce()
        {
            //Fill RessouceManager
            this.RessourcesManagers = new Dictionary<string, ResourceManager>();
            RessoucesManagerUtil.FillResourcesManager(this.TypeOfEntity, this.RessourcesManagers);

            // BaseEntity RessouceManager
            string BaseEntityRessouceFullName = typeof(BaseEntity).Namespace + ".Resources." + typeof(BaseEntity).Name + "." + typeof(BaseEntity).Name;
            baseEntityResourceManager = new ResourceManager(BaseEntityRessouceFullName, typeof(BaseEntity).Assembly);
        }

        /// <summary>
        /// Read configuration of entity
        /// </summary>
        private void ReadConfiguration()
        {

            this.LoadRessouce();


            #region Read DisplayEntityAttribute
            // 
            // Read Entity  Configuration
            //

            // Load and Check Existance of EntityMetataDataAttribute
            Object[] ls_attribut = this.TypeOfEntity.GetCustomAttributes(typeof(EntityMetataDataAttribute), false);

            if (ls_attribut != null && ls_attribut.Count() > 0)
            {
                this.entityMetataDataAttribute = (EntityMetataDataAttribute)ls_attribut[0];

               
            }
            else
            {
                this.entityMetataDataAttribute = new EntityMetataDataAttribute();
            }

            // Check DisplayMember existance
            if (this.entityMetataDataAttribute.Localizable)
            {
                // set all attribute Localizable
                this.Localizable = this.entityMetataDataAttribute.Localizable;

                // Titre
                this.entityMetataDataAttribute.PluralName = this.GetStringFromRessource("PluralName", true);
                this.entityMetataDataAttribute.SingularName = this.GetStringFromRessource("SingularName", true);

                // Load Title with Name of Entity if PluraleNameKay Not exist
                if (this.entityMetataDataAttribute.PluralName == null)
                    this.entityMetataDataAttribute.PluralName = this.GetStringFromRessource(this.TypeOfEntity + "_PluraleName", false);
                if (this.entityMetataDataAttribute.SingularName == null)
                    this.entityMetataDataAttribute.SingularName = this.GetStringFromRessource(this.TypeOfEntity + "_SingularName", false);

            }

            #endregion
        }



        /// <summary>
        /// Get translated string from resource file
        /// if the string not exist is retrun string source with prefix language 
        /// </summary>
        /// <param name="key">the string to be translated</param>
        /// <param name="return_null_if_nat_exist">determine the return value type</param>
        /// <returns>The translated string or Null if return_null_if_nat_exist is true </returns>
        private string GetStringFromRessource(string key, bool return_null_if_nat_exist = false)
        {
            string msg = null;

            foreach (var item in RessourcesManagers.Values)
            {
                string text;
                text = item.GetString(key, this.CultureInfo);
                if (text != null)
                {
                    msg = text;
                    break;
                }
            }

            if (msg == null && !return_null_if_nat_exist)
                msg = this.CultureInfo.Name + "_" + key;
            return msg;
        }

        /// <summary>
        /// Translate Msg 
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string Translate(string msg)
        {
            string translated_msg = this.GetStringFromRessource(msg);
            return translated_msg;
        }

        public bool Dispose()
        {
            return EntityMetaDataConfiguratrion.ConfigurationOfEntities.Remove(this.TypeOfEntity);
        }
        /// <summary>
        /// Delete all ConfigEntity object
        /// </summary>
        public static void Despose()
        {
            if (ConfigurationOfEntities != null)
                ConfigurationOfEntities.Clear();
        }
    }
}
