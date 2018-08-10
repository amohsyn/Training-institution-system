using GApp.WebApp.Manager.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using GApp.Core.MetaDatas.Attributes;

namespace GApp.WebApp.Manager.Generator
{
    public partial class EntityGeneratorWork<T>
    {
        public Entity_ModelsViewsConfiguration modelViewMetaData { set; get; }

        private void InitEntityMetaData()
        {
            modelViewMetaData = new Entity_ModelsViewsConfiguration(this.EntityType);
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
