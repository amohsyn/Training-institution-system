using GApp.Core.MetaDatas.Attributes;
using GApp.WebApp.Manager.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GApp.WebApp.Manager.Generator
{
    public class EntityMentaData_CodeGenerator<T> where T : DbContext, new()
    {
        public ModelViewMetaData modelViewMetaData { set; get; }

        public Type EntityType { set; get; }

        public EntityMentaData_CodeGenerator(Type EntityType)
        {
            this.EntityType = EntityType;
            modelViewMetaData = new ModelViewMetaData(this.EntityType);
        }

       

        public List<PropertyInfo> getRequiredProperties()
        {
            return this.EntityType.GetProperties()
                .Where(p => (p.GetCustomAttribute(typeof(RequiredAttribute)) != null))
                .ToList();
        }
        public List<PropertyInfo> getUniqueProperties()
        {
            return this.EntityType.GetProperties()
                .Where(p => (p.GetCustomAttribute(typeof(UniqueAttribute)) != null))
                .ToList();
        }
    }
}
