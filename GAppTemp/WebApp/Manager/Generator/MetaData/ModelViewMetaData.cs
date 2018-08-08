﻿using GApp.WebApp.Manager.Views.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GApp.WebApp.Manager.Generator.MetaData
{
    /// <summary>
    /// Read Meta Data for a ModelView
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

        public Dictionary<PropertyInfo, ReadFromAttribute> GetAllReadFrom()
        {
            Dictionary<PropertyInfo, ReadFromAttribute> AllAllReadFromAttributes = new Dictionary<PropertyInfo, ReadFromAttribute>();

            foreach (var property in this.TypeOfModelView.GetProperties())
            {
                if (property.IsDefined(typeof(ReadFromAttribute)))
                {
                    ReadFromAttribute ReadFromAttribute = property.GetCustomAttribute(typeof(ReadFromAttribute)) as ReadFromAttribute;
                    if (ReadFromAttribute != null)
                    {
                        AllAllReadFromAttributes.Add(property, ReadFromAttribute);
                    }
                }
            }
            return AllAllReadFromAttributes;
        }

        public Dictionary<PropertyInfo, ComboBoxAttribute> GetAllCombBox()
        {
            Dictionary<PropertyInfo, ComboBoxAttribute> AllCombBoxAttributes = new Dictionary<PropertyInfo, ComboBoxAttribute>();

            foreach (var property in this.TypeOfModelView.GetProperties())
            {
                if (property.IsDefined(typeof(ComboBoxAttribute)))
                {
                    ComboBoxAttribute ComboBoxAttribute = property.GetCustomAttribute(typeof(ComboBoxAttribute)) as ComboBoxAttribute;
                    if (ComboBoxAttribute != null)
                    {
                        AllCombBoxAttributes.Add(property, ComboBoxAttribute);
                    }
                }
            }
            return AllCombBoxAttributes;
        }
        #endregion
    }
}
