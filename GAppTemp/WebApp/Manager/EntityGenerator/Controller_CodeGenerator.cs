using GApp.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GApp.WebApp.Manager.Generator
{
    public partial class EntityGeneratorWork<T>
    {
        #region IncludeBind
        public string GetEditInludeBind()
        {
            PropertyInfo IdPropertyInfo = this.EntityType.GetProperty(nameof(BaseEntity.Id));
            var properties = this.GetEditProperties();
            properties.Add(IdPropertyInfo);
            List<string> binded_properties = properties
                .Select(p => p.Name)
                .ToList<string>();
            return string.Join(",", binded_properties);
        }
        public string GetCreateInludeBind()
        {
            List<string> binded_properties = this.GetCreatedProperties()
                .Select(p => p.Name)
                .ToList<string>();
            return string.Join(",", binded_properties);
        }
        #endregion
    }
}
