using GApp.Core.Entities.ModelsViews;
using GApp.Exceptions;
using GApp.WebApp.Manager.Views;
using GApp.WebApp.Manager.Views.Attributes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;

namespace GApp.WebApp.Manager.Generator
{
    /// <summary>
    /// Generate works for a Entity type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityGeneratorWork<T> where T : DbContext, new()
    {
        #region Properties
        public Type EntityType { set; get; }
        public string IncludeBind { set; get; }
        public List<string> ForeignKeiesIds { set; get; }
        public List<string> ForeignKeyNames { set; get; }
        public ModelViewMetaData modelViewMetaData { set; get; }
        #endregion

        #region Pricate Value
        private string _ModelsViewsNameSapce = "TrainingIS.Entities.ModelsViews";
        #endregion

        public EntityGeneratorWork(Type EntityType)
        {
            this.EntityType = EntityType;

            EntityService<T> entityService = new EntityService<T>();
            this.ForeignKeiesIds = entityService.GetForeignKeiesIds(this.EntityType);
            this.ForeignKeyNames = entityService.GetForeignKeyNames(this.EntityType);
            this.InitInludeBind(this.EntityType);
            modelViewMetaData = new ModelViewMetaData(this.EntityType);
        }


        private List<PropertyInfo> DefaultProperties()
        {
            return this.EntityType.GetProperties()
                 .Where(p => !this.ForeignKeyNames.Contains(p.Name))
                 .Where(p => p.Name != "Id")
                 .Where(p => p.Name != "Ordre")
                 .Where(p => p.Name != "Reference")
                 .Where(p => p.Name != "CreateDate")
                 .Where(p => p.Name != "UpdateDate")
                 .ToList();
        }


        #region Controller
        public Type GetLineViewType()
        {
            Type IndexModelView_Type = this.getIndexModelView_Type();
            if (IndexModelView_Type == null) return null;

            PropertyInfo listPropertyInfo = IndexModelView_Type.GetProperties()
             .Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
             .FirstOrDefault();
            Type LineViewType = listPropertyInfo?.PropertyType.GetGenericArguments()[0];
            return LineViewType;
        }

       
        #endregion


        #region CreateView
        /// <summary>
        /// Get the modle namespace in the create view
        /// </summary>
        /// <returns></returns>
        public string GetUsingModel_In_CreateView()
        {
            string using_model_namespace = "";
            string format = "@model {0}";
            string model_full_name = "";

            if (this.getCreateModelView_Type() != null)
            {
                model_full_name = this.getCreateModelView_Type().FullName;
            }
            else
            {
                model_full_name = this.EntityType.FullName;
            }
            using_model_namespace = string.Format(format, model_full_name);
            return using_model_namespace;
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
                            .ToList();
            }
            else
            {
                properties = this.DefaultProperties();
            }


            return properties;
        }
        #endregion

        #region Edit View
        /// <summary>
        /// Get the modle namespace in the edit view
        /// </summary>
        /// <returns></returns>
        public string GetUsingModel_In_EditView()
        {
            string using_model_namespace = "";
            string format = "@model {0}";
            string model_full_name = "";


            if (this.getEditModelView_Type() != null)
            {
                model_full_name = this.getEditModelView_Type().FullName;
            }
            else
            {
                model_full_name = this.EntityType.FullName;
            }
            using_model_namespace = string.Format(format,model_full_name);
            return using_model_namespace;
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
                            .ToList();
            }
            else
            {
                properties = this.DefaultProperties();
            }


            return properties;
        }
        #endregion

        #region Index View
        /// <summary>
        /// Get the modle namespace in the edit view
        /// </summary>
        /// <returns></returns>
        public string GetUsingModel_In_IndexView()
        {
            string using_model_namespace = "";
            string format = "@model {0}";
            string model_namespace = "";
            string model_full_name = "";

            if (this.getIndexModelView_Type() != null)
            {
                model_full_name = this.getIndexModelView_Type().FullName;
            }
            else
            {
                model_namespace = "TrainingIS.Entities.ModelsViews";
                model_full_name = model_namespace + ".DefaultIndex" + this.EntityType.Name + "View";
            }
            using_model_namespace = string.Format(format, model_full_name);
            return using_model_namespace;
        }
        /// <summary>
        /// Get the properties in Create View
        /// </summary>
        /// <returns></returns>
        public List<PropertyInfo> GetIndexProperties()
        {
            List<PropertyInfo> properties = null;
            Type IndexModelView_Type = this.getIndexModelView_Type();
            if (IndexModelView_Type != null)
            {

                PropertyInfo listPropertyInfo = IndexModelView_Type.GetProperties()
                 .Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                 .FirstOrDefault();
                var LineViewType = listPropertyInfo?.PropertyType.GetGenericArguments()[0];
                if (LineViewType == null)
                {
                    string msg = string.Format("The {0} must have a member of type {1}", this.getIndexModelView_Type().Name, typeof(LineView).Name);
                    throw new GAppException(msg);
                }
                properties = LineViewType.GetProperties().ToList();
            }
            else
            {
                properties = this.DefaultProperties();
            }


            return properties;
        }


        #endregion

        #region Details View
        public string GetUsingModel_In_DetailsView()
        {
            string using_model_namespace = "";
            string format = "@model {0}";
            string model_full_name = "";

            if (this.getDetailsModelView_Type() != null)
            {
                model_full_name = this.getDetailsModelView_Type().FullName;
            }
            else
            {

                model_full_name = this.EntityType.FullName;
            }
            using_model_namespace = string.Format(format, model_full_name);
            return using_model_namespace;
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
                            .ToList();
            }
            else
            {
                properties = this.DefaultProperties();
            }


            return properties;
        }
        #endregion

        private void InitInludeBind(Type EntityType)
        {
            string include_bind = "";
            List<string> binded_properties = EntityType.GetProperties()
                .Where(p => p.Name != "Ordre")
                .Where(p => p.Name != "Reference")
                .Where(p => p.Name != "CreateDate")
                .Where(p => p.Name != "UpdateDate")
                .Select(p => p.Name)
                .ToList<string>();
            include_bind = string.Join(",", binded_properties);
            this.IncludeBind = include_bind;
        }

        #region ModelViews
        public bool isModelView()
        {
            throw new NotImplementedException();
        }
        public Type getIndexModelView_Type()
        {
            BaseViewAttribute indexViewAttribute = modelViewMetaData.IndexViewAttribute;
            return indexViewAttribute?.TypeOfView;
        }
        public Type getCreateModelView_Type()
        {
            BaseViewAttribute indexViewAttribute = modelViewMetaData.CreateViewAttribute;
            return indexViewAttribute?.TypeOfView;
        }
        public Type getEditModelView_Type()
        {
            BaseViewAttribute indexViewAttribute = modelViewMetaData.EditViewAttribute;
            return indexViewAttribute?.TypeOfView;
        }
        public Type getDetailsModelView_Type()
        {
            BaseViewAttribute indexViewAttribute = modelViewMetaData.DetailsViewAttribute;
            return indexViewAttribute?.TypeOfView;
        }
        #endregion


    }
}