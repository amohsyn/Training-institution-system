using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;

namespace GApp.WebApp.Manager.Generator
{
    
    public class EntityService<T> where T: DbContext, new()
    {
        public List<Type> getAllEntities()
        {
            T context = new T();
            return context.GetAllTypesInContextOrder();
        }


        public Dictionary<Type, List<Type>> getAllViewsModels()
        {
            Dictionary<Type, List<Type>> ViewsModels = new Dictionary<Type, List<Type>>();
            var All_Entities = this.getAllEntities();
            foreach (Type entityType in All_Entities)
            {
                List<Type> entity_viewsModels = this.getEntityModelsViewsTypes(entityType);
                ViewsModels.Add(entityType, entity_viewsModels);
            }
            return ViewsModels;
        }

        public List<Type> getEntityModelsViewsTypes(Type entityType)
        {
            EntityGeneratorWork<T> EntityGeneratorWork = new EntityGeneratorWork<T>(entityType);
            return EntityGeneratorWork.ModelsViewsTypes; ;
        }




        #region Ovsolete
        [Obsolete("Use EntityGenerator class")]
        public List<string> GetForeignKeyNames(Type EntityType)
        {
            T context = new T();
            return context.GetForeignKeyNames(EntityType).ToList<string>();

        }

        [Obsolete("Use EntityGenerator class")]
        public List<string> GetForeignKeiesIds(Type EntityType)
        {
            T context = new T();
            return context.GetForeignKeysIds(EntityType).ToList<string>();

        }
        #endregion



    }
}