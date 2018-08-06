using GApp.WebApp.Manager.Views.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GApp.WebApp.Manager.Generator.MetaData
{
    /// <summary>
    /// Meta Data for a ModelView
    /// </summary>
    public class ModelViewMetaData
    {
        public Type TypeOfModelView { set; get; }
        public ModelViewMetaData(Type TypeOfModelView)
        {
            this.TypeOfModelView = TypeOfModelView;
        }
        #region Select Filter
        public List<PropertyInfo> Properties_With_SelectFilter()
        {
            var Properties = this.TypeOfModelView.GetProperties()
                           .Where(p => p.IsDefined(typeof(SelectFilterAttribute))).ToList();
            return Properties;
        }

        public Dictionary<PropertyInfo,SelectFilterAttribute> GetAllSelectFilter()
        {
            Dictionary<PropertyInfo, SelectFilterAttribute> AllFilterAttributes = new Dictionary<PropertyInfo, SelectFilterAttribute>();

            foreach (var property in this.TypeOfModelView.GetProperties())
            {
                if (property.IsDefined(typeof(SelectFilterAttribute)))
                {
                    SelectFilterAttribute selectFilterAttribute = property.GetCustomAttribute(typeof(SelectFilterAttribute)) as SelectFilterAttribute;
                    if(selectFilterAttribute != null)
                    {
                        AllFilterAttributes.Add(property, selectFilterAttribute);
                    }
                }
            }
            return AllFilterAttributes;
        }
        #endregion
    }
}
