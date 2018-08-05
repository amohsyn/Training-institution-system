using GApp.WebApp.Manager.Views;
using GApp.WebApp.Manager.Views.Attributes;
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
    public class ModelView_CodeGenerator<T> where T : DbContext, new()
    {
        public string DetailsModelView_ClassName { set; get; }
        public string FormModelView_ClassName { set; get; }
        private ModelViewMetaData _ModelViewMetaData;
        private RelationShip_CodeGenerator<T> _RelationShip_CodeGenerator;
        public Dictionary<Type, List<Type>> Default_ModelsViewsTypes { set; get; }
        private EntityService<T> entityService;


        public Type EntityType { set; get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="EntityType"></param>
        /// <param name="ListModelsViewsTypes">List from Default_ModelsViewsTypes</param>
        public ModelView_CodeGenerator(Type EntityType, Dictionary<Type, List<Type>> Default_ModelsViewsTypes)
        {
            this.Default_ModelsViewsTypes = Default_ModelsViewsTypes;
            this.EntityType = EntityType;
            this.DetailsModelView_ClassName = string.Format("Default_{0}DetailsView", this.EntityType.Name);
            this.FormModelView_ClassName = string.Format("Default_{0}FormView", this.EntityType.Name);
            this.entityService = new EntityService<T>();
            this._ModelViewMetaData = new ModelViewMetaData(EntityType);
            _RelationShip_CodeGenerator = new RelationShip_CodeGenerator<T>(this.EntityType);
        }

        #region Default ModelView_Types
        private Type _Default_IndexModelView_Type;
        public Type Default_IndexModelView_Type
        {
            get
            {
                if (_Default_IndexModelView_Type == null) this.Load_Model_View_Types();
                return _Default_IndexModelView_Type;
            }
        }
        private Type _Default_CreateModelView_Type;
        public Type Default_CreateModelView_Type
        {
            get
            {
                if (_Default_CreateModelView_Type == null) this.Load_Model_View_Types();
                return _Default_CreateModelView_Type;
            }
        }
        private Type _Default_EditModelView_Type;
        public Type Default_EditModelView_Type
        {
            get
            {
                if (_Default_EditModelView_Type == null) this.Load_Model_View_Types();
                return _Default_EditModelView_Type;
            }
        }
        private Type _Default_DeleleModelView_Type;
        public Type Default_DeleleModelView_Type
        {
            get
            {
                if (_Default_DeleleModelView_Type == null) this.Load_Model_View_Types();
                return _Default_DeleleModelView_Type;
            }
        }
        private Type _Default_DetailsModelView_Type;
        public Type Default_DetailsModelView_Type
        {
            get
            {
                if (_Default_DetailsModelView_Type == null) this.Load_Model_View_Types();
                return _Default_DetailsModelView_Type;
            }
        }

        private void Load_Model_View_Types()
        {

            Type formModelView_type = Default_ModelsViewsTypes[this.EntityType].Where(type => type.Name == this.FormModelView_ClassName).FirstOrDefault();
            Type DetailsModelView_Type = Default_ModelsViewsTypes[this.EntityType].Where(type => type.Name == this.DetailsModelView_ClassName).FirstOrDefault();
            this._Default_IndexModelView_Type = DetailsModelView_Type;
            this._Default_CreateModelView_Type = formModelView_type;
            this._Default_EditModelView_Type = formModelView_type;
            this._Default_DeleleModelView_Type = DetailsModelView_Type;
            this._Default_DetailsModelView_Type = DetailsModelView_Type;


        }
        #endregion

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



        #region  ModelsViews types


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
            BaseViewAttribute indexViewAttribute = _ModelViewMetaData.IndexViewAttribute;
            if (indexViewAttribute == null) return this.Default_IndexModelView_Type;
            else return indexViewAttribute.TypeOfView;
        }
        public Type getCreateModelView_Type()
        {
            BaseViewAttribute CreateViewAttribute = _ModelViewMetaData.CreateViewAttribute;
            if (CreateViewAttribute == null) return this.Default_CreateModelView_Type;
            else return CreateViewAttribute.TypeOfView;

        }
        public Type getEditModelView_Type()
        {
            BaseViewAttribute EditViewAttribute = _ModelViewMetaData.EditViewAttribute;
            if (EditViewAttribute == null) return this.Default_EditModelView_Type;
            else return EditViewAttribute.TypeOfView;
        }
        public Type getDetailsModelView_Type()
        {
            BaseViewAttribute DetailsViewAttribute = _ModelViewMetaData.DetailsViewAttribute;
            if (DetailsViewAttribute == null) return this.Default_DetailsModelView_Type;
            else return DetailsViewAttribute.TypeOfView;

        }
        public Type getDeleteModelView_Type()
        {
            BaseViewAttribute DeleteAttribute = _ModelViewMetaData.DetailsViewAttribute;
            if (DeleteAttribute == null) return this.Default_DeleleModelView_Type;
            else return DeleteAttribute.TypeOfView;

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
                 .Where(p => !_RelationShip_CodeGenerator.ForeignKeiesIds.Contains(p.Name))
                 .Where(propertyInfo =>
                 {
                     bool result = true;
                     DisplayAttribute displayAttribute = propertyInfo
                                                     .GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
                     if (displayAttribute != null
                         && displayAttribute.GetAutoGenerateField() != null
                         && displayAttribute.GetAutoGenerateField() == false)
                         result = false;

                     return result;
                 })
                 .Where(p => p.Name != "Id")
                 .Where(p => p.Name != "Ordre")
                 .Where(p => p.Name != "Reference")
                 .Where(p => p.Name != "CreateDate")
                 .Where(p => p.Name != "UpdateDate")
                 .ToList();
        }

        /// <summary>
        /// Default Edit Properties
        /// </summary>
        /// <returns></returns>
        public List<PropertyInfo> DefaultEditProperties()
        {
            return this.EntityType.GetProperties()
                 .Where(p => !_RelationShip_CodeGenerator.ForeignKeyNames.Contains(p.Name))
                 .Where(propertyInfo =>
                 {
                     bool result = true;
                     DisplayAttribute displayAttribute = propertyInfo
                                                     .GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
                     if (displayAttribute != null
                         && displayAttribute.GetAutoGenerateField() != null
                         && displayAttribute.GetAutoGenerateField() == false)
                         result = false;

                     return result;
                 })
                 .Where(p => p.Name != "Id")
                 .Where(p => p.Name != "Ordre")
                 .Where(p => p.Name != "Reference")
                 .Where(p => p.Name != "CreateDate")
                 .Where(p => p.Name != "UpdateDate")
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
                    .Where(propertyInfo =>
                    {
                        bool result = true;
                        DisplayAttribute displayAttribute = propertyInfo
                                                        .GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
                        if (displayAttribute != null
                            && displayAttribute.GetAutoGenerateField() != null
                            && displayAttribute.GetAutoGenerateField() == false)
                            result = false;

                        return result;
                    })
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
                            .Where(p => !_RelationShip_CodeGenerator.ForeignKeyNames.Contains(p.Name))
                            .Where(propertyInfo =>
                            {
                                bool result = true;
                                DisplayAttribute displayAttribute = propertyInfo
                                                                .GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
                                if (displayAttribute != null
                                    && displayAttribute.GetAutoGenerateField() != null
                                    && displayAttribute.GetAutoGenerateField() == false)
                                    result = false;

                                return result;
                            })
                            .Where(p => p.Name != "Id").ToList()
                            .ToList();
            }
            else
            {
                properties = this.DefaultEditProperties();
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
                            .Where(p => !_RelationShip_CodeGenerator.ForeignKeyNames.Contains(p.Name))
                             .Where(propertyInfo =>
                             {
                                 bool result = true;
                                 DisplayAttribute displayAttribute = propertyInfo
                                                                 .GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
                                 if (displayAttribute != null
                                     && displayAttribute.GetAutoGenerateField() != null
                                     && displayAttribute.GetAutoGenerateField() == false)
                                     result = false;

                                 return result;
                             })
                            .Where(p => p.Name != "Id").ToList()
                            .ToList();
            }
            else
            {
                properties = this.DefaultEditProperties();
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
                                .Where(propertyInfo =>
                                {
                                    bool result = true;
                                    DisplayAttribute displayAttribute = propertyInfo
                                                                    .GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
                                    if (displayAttribute != null
                                        && displayAttribute.GetAutoGenerateField() != null
                                        && displayAttribute.GetAutoGenerateField() == false)
                                        result = false;

                                    return result;
                                })
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


        public Dictionary<Type, List<Type>> getAllViewsModels(Dictionary<Type, List<Type>> DefaulsViewsModels)
        {
            var all_ViewsModels = this.getAllViewsModels();
            foreach (var key in DefaulsViewsModels.Keys)
            {
                all_ViewsModels.Add(key, DefaulsViewsModels[key]);
            }
            return all_ViewsModels;
        }

        public Dictionary<Type, List<Type>> getAllViewsModels()
        {
            Dictionary<Type, List<Type>> ViewsModels = new Dictionary<Type, List<Type>>();
            var All_Entities = this.entityService.getAllEntities();
            foreach (Type entityType in All_Entities)
            {
                List<Type> entity_viewsModels = this.getEntityModelsViewsTypes(entityType);
                ViewsModels.Add(entityType, entity_viewsModels);
            }
            return ViewsModels;
        }

        public List<Type> getEntityModelsViewsTypes(Type entityType)
        {
            ModelView_CodeGenerator<T> modelView_CodeGenerator = new ModelView_CodeGenerator<T>(entityType, this.Default_ModelsViewsTypes);
            return modelView_CodeGenerator.ModelsViewsTypes; ;
        }



    }
}
