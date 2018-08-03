using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GApp.WebApp.Manager.Generator
{
    public class ConvertoTo_CodeGenerator<T> where T : DbContext, new()
    {
        public Type EntityType { set; get; }
        private RelationShip_CodeGenerator<T> _RelationShip_CodeGenerator;

        public ConvertoTo_CodeGenerator(Type EntityType)
        {
            this.EntityType = EntityType;
            _RelationShip_CodeGenerator = new RelationShip_CodeGenerator<T>(EntityType);

        }

        /// <summary>
        /// Search a property in Object
        /// </summary>
        /// <param name="TypeObject"></param>
        /// <param name="Searched_Property"></param>
        /// <returns>
        ///  Code of value of Finded_Property 
        ///  Exemple : Trainee.Code
        ///  Empty : if the property not exist
        /// </returns>
        public string Search_Property_In_Object(Type TypeObject, PropertyInfo Searched_Property)
        {
            string code = "";
            string code_format = "{0}.{1}";
          

            foreach (PropertyInfo Finded_Property in TypeObject.GetProperties())
            {
                if(Finded_Property.Name == Searched_Property.Name)
                {
                    if (Finded_Property.PropertyType == Searched_Property.PropertyType)
                    {
                       
                        code = string.Format(code_format, Searched_Property.ReflectedType.Name, Finded_Property.Name);
                        return code;
                    }
                    else
                    {
                        Type underlyingType_Finded_Property = Nullable.GetUnderlyingType(Finded_Property.PropertyType);
                        Type underlyingType_Searched_Property = Nullable.GetUnderlyingType(Searched_Property.PropertyType);

                        // if Finded_Property is nullable and Searched_Property not nullable
                        if (underlyingType_Finded_Property == null && underlyingType_Searched_Property!= null)
                        {
                            string code_format_2 = "ConversionUtil.DefaultValue_if_Null<{0}>({1}.{2})";
                            code = string.Format(code_format_2, underlyingType_Searched_Property.Name, Searched_Property.ReflectedType.Name, Searched_Property.Name);
                            return code;
                        }

                        // if Searched_Property is nullable and Finded_Property  not nullable
                        if (underlyingType_Finded_Property != null && underlyingType_Searched_Property == null)
                        {
                            code = string.Format(code_format, Searched_Property.ReflectedType.Name, Searched_Property.Name);
                            return code;

                           
                        }

                    }
                }
               
            }
            return code;
        } 

        public PropertyInfo Fin_ManyProperty_In_ModelView(Type viewModelType, PropertyInfo enityProperty)
        {
           if(_RelationShip_CodeGenerator.ManyRelationsShipNames.Contains(enityProperty.Name))
            {
                string Name_ManyProperty_In_ModelView = "Selected_" + enityProperty.Name;
                var  ManyProperty_In_ModelView = viewModelType.GetProperty(Name_ManyProperty_In_ModelView);
                return ManyProperty_In_ModelView;
            }
            return null;
        }

    }
}
