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
        public Type EntityType { set; get; }
        public string IncludeBind { set; get; }
        public List<string> ForeignKeiesIds { set; get; }
        public List<string> ForeignKeyNames { set; get; }

        public EntityGeneratorWork(Type EntityType)
        {
            this.EntityType = EntityType;
           
            EntityService<T> entityService = new EntityService<T>();
            this.ForeignKeiesIds = entityService.GetForeignKeiesIds(this.EntityType);
            this.ForeignKeyNames = entityService.GetForeignKeyNames(this.EntityType);
            this.InitInludeBind(this.EntityType);
        }

        public List<PropertyInfo> GetCreatedProperties()
        {
            var properties = this.EntityType.GetProperties()
                .Where(p => !this.ForeignKeyNames.Contains(p.Name))
                .Where(p => p.Name != "Id")
                .Where(p => p.Name != "Ordre")
                .Where(p => p.Name != "Reference")
                .Where(p => p.Name != "CreateDate")
                .Where(p => p.Name != "UpdateDate")
                .ToList();

            return properties;
        }

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

        #region Get IndexModelView Types
        public Type getIndexModelView_Type()
        {
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(this.EntityType);
            BaseViewAttribute indexViewAttribute =  modelViewMetaData.IndexViewAttribute;
            return  indexViewAttribute?.TypeOfView;
        }
        public Type getCreateModelView_Type()
        {
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(this.EntityType);
            BaseViewAttribute indexViewAttribute = modelViewMetaData.CreateViewAttribute;
            return indexViewAttribute?.TypeOfView;
        }
        public Type getEditModelView_Type()
        {
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(this.EntityType);
            BaseViewAttribute indexViewAttribute = modelViewMetaData.EditViewAttribute;
            return indexViewAttribute?.TypeOfView;
        }
        public Type getDetailsModelView_Type()
        {
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(this.EntityType);
            BaseViewAttribute indexViewAttribute = modelViewMetaData.DetailsViewAttribute;
            return indexViewAttribute?.TypeOfView;
        }
        #endregion


    }
}