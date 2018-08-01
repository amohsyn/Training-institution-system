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
    public class Controller_CodeGenerator<T> where T : DbContext, new()
    {
        public Type EntityType { set; get; }
        private ModelView_CodeGenerator<T> _ModelView_CodeGenerator;

        public Controller_CodeGenerator(Type EntityType, Dictionary<Type, List<Type>> Default_ModelsViewsTypes)
        {
            this.EntityType = EntityType;
            _ModelView_CodeGenerator = new ModelView_CodeGenerator<T>(this.EntityType , Default_ModelsViewsTypes);
        }

        #region IncludeBind
        public string GetEditInludeBind()
        {
            PropertyInfo IdPropertyInfo = this.EntityType.GetProperty(nameof(BaseEntity.Id));
            var properties = _ModelView_CodeGenerator.GetEditProperties();
            properties.Add(IdPropertyInfo);
            List<string> binded_properties = properties
                .Select(p => p.Name)
                .ToList<string>();
            return string.Join(",", binded_properties);
        }
        public string GetCreateInludeBind()
        {
            List<string> binded_properties = _ModelView_CodeGenerator.GetCreatedProperties()
                .Select(p => p.Name)
                .ToList<string>();
            return string.Join(",", binded_properties);
        }
        #endregion
    }
}
