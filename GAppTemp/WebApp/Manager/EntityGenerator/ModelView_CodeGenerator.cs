using GApp.Core.Entities.ModelsViews;
using GApp.WebApp.Manager.Views;
using GApp.WebApp.Manager.Views.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GApp.WebApp.Manager.Generator
{
    public partial class EntityGeneratorWork<T>
    {
        public string DetailsModelView_ClassName { set; get; }
        public string FormModelView_ClassName { set; get; }
       

        public void InitModelView()
        {
             this.DetailsModelView_ClassName = string.Format("Default_{0}DetailsView", this.EntityType.Name);
             this.FormModelView_ClassName = string.Format("Default_{0}FormView", this.EntityType.Name);
        }

       
         

        // private string _ModelsViewsNameSapce = "TrainingIS.Entities.ModelsViews";

        /// <summary>
        /// get Models Views Names Spaces
        /// </summary>
        public List<string> ModelsViewsNamesSpaces
        {
            get
            {
                List<String> names_spaces = new List<string>();
                foreach (var model_type in this.ModelsViewsTypes)
                {
                    if (!names_spaces.Contains(model_type.Namespace))
                        names_spaces.Add(model_type.Namespace);
                }
                return names_spaces;
            }
        }

       

        #region Get modelsViews types


        private List<Type> _ModelsViewsTypes = null;
        /// <summary>
        /// Get a list of models views types
        /// </summary>
        public List<Type> ModelsViewsTypes
        {
            get
            {
                if (_ModelsViewsTypes == null) _ModelsViewsTypes = new List<Type>();

                Type index_type = getIndexModelView_Type();
                if (index_type != null)
                    if (!_ModelsViewsTypes.Contains(index_type))
                        _ModelsViewsTypes.Add(index_type);

                Type Create_type = getCreateModelView_Type();
                if (Create_type != null)
                    if (!_ModelsViewsTypes.Contains(Create_type))
                        _ModelsViewsTypes.Add(Create_type);

                Type Edit_type = getEditModelView_Type();
                if (Edit_type != null)
                    if (!_ModelsViewsTypes.Contains(Edit_type))
                        _ModelsViewsTypes.Add(Edit_type);

                Type Details_type = getDetailsModelView_Type();
                if (Details_type != null)
                    if (!_ModelsViewsTypes.Contains(Details_type))
                        _ModelsViewsTypes.Add(Details_type);

                return _ModelsViewsTypes;
            }
        }

      

       

        public Type getIndexModelView_Type()
        {
            throw new NotImplementedException("Use ModelView Generator");
        }
        public Type getCreateModelView_Type()
        {
            throw new NotImplementedException("Use ModelView Generator");

        }
        public Type getEditModelView_Type()
        {
            throw new NotImplementedException("Use ModelView Generator");
        }
        public Type getDetailsModelView_Type()
        {
            throw new NotImplementedException("Use ModelView Generator");

        }
        public Type getDeleteModelView_Type()
        {
            throw new NotImplementedException("Use ModelView Generator");

        }

        #endregion




        #region Properties used in Views
        /// <summary>
        /// Default Index Properties
        /// </summary>
        /// <returns></returns>
        public List<PropertyInfo> DefaultIndexProperties()
        {
            return this.EntityType.GetProperties()
                 .Where(p => !this.ForeignKeyNames.Contains(p.Name))
                 .Where(p => p.Name != "Id")
                 .Where(p => p.Name != "Ordre")
                 .Where(p => p.Name != "Reference")
                 .Where(p => p.Name != "CreateDate")
                 .Where(p => p.Name != "UpdateDate")
                 .Where(p => !ManyRelationsShipNames.Contains(p.Name))
                 .ToList();
        }
        /// <summary>
        /// Get the properties in Create View
        /// </summary>
        /// <returns></returns>
        public List<PropertyInfo> GetIndexProperties()
        {
            List<PropertyInfo> properties = null;
            Type Default_IndexModelView_Type = this.getIndexModelView_Type();
            if (Default_IndexModelView_Type != null)
            {
                properties = Default_IndexModelView_Type
                    .GetProperties()
                    .Where(p => p.Name != "Id").ToList();
            }
            else
            {
                properties = this.DefaultIndexProperties();
            }


            return properties;
        }
        /// <summary>
        /// Get the properties in Create View
        /// </summary>
        /// <returns></returns>
        public List<PropertyInfo> GetCreatedProperties()
        {
            List<PropertyInfo> properties = null;
            if (this.getCreateModelView_Type() != null)
            {
                properties = this.getCreateModelView_Type().GetProperties()
                            .Where(p => !this.ForeignKeyNames.Contains(p.Name))
                            .Where(p => p.Name != "Id").ToList()
                            .ToList();
            }
            else
            {
                properties = this.DefaultIndexProperties();
            }


            return properties;
        }
        /// <summary>
        /// Get the properties in Edit View
        /// </summary>
        /// <returns></returns>
        public List<PropertyInfo> GetEditProperties()
        {
            List<PropertyInfo> properties = null;
            if (this.getEditModelView_Type() != null)
            {
                properties = this.getEditModelView_Type().GetProperties()
                            .Where(p => !this.ForeignKeyNames.Contains(p.Name))
                            .Where(p => p.Name != "Id").ToList()
                            .ToList();
            }
            else
            {
                properties = this.DefaultIndexProperties();
            }


            return properties;
        }
        /// <summary>
        /// Get the properties in Create View
        /// </summary>
        /// <returns></returns>
        public List<PropertyInfo> GetDetailsProperties()
        {
            List<PropertyInfo> properties = null;
            if (this.getDetailsModelView_Type() != null)
            {
                properties = this.getDetailsModelView_Type().GetProperties()
                                .Where(p => p.Name != "Id").ToList()
                            .ToList();
            }
            else
            {
                properties = this.DefaultIndexProperties();
            }


            return properties;
        }


        #endregion

    }
}
