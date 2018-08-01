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