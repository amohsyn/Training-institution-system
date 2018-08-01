using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GApp.WebApp.Manager.Generator
{
    public partial class EntityGeneratorWork<T>
    {
        /// <summary>
        /// Get the Code of the default value of a Type
        /// </summary>
        /// <param name="type"></param>
        /// <returns>C-sharp code as string, of a default value </returns>
        public string Code_Of_DefaultValue(Type type)
        {

            var default_value = GetDefault(type);
            if (default_value == null || type == typeof(DateTime?))
            {
                return "null";
            }
            else
            {
                if (type == typeof(String))
                    return "\"" + default_value + "\"";
                if (type == typeof(DateTime))
                    return "DateTime.Now";
                if (type == typeof(bool))
                    return default_value.ToString().ToLower();
                if (type.IsEnum)
                    return type.Name + "." + default_value.ToString();
                else
                    return default_value.ToString();
            }
        }
        public object GetDefault(Type t)
        {
            return this.GetType().GetMethod("GetDefaultGeneric").MakeGenericMethod(t).Invoke(this, null);
        }

        public T_default GetDefaultGeneric<T_default>()
        {
            return default(T_default);
        }

    }
}
