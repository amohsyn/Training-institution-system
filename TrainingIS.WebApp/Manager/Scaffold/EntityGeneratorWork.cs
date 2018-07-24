﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace TrainingIS.WebApp.Manager.Scaffold
{
    public class EntityGeneratorWork
    {
        public Type EntityType { set; get; }
        public string IncludeBind { set; get; }
        public List<string> ForeignKeiesIds { set; get; }
        public List<string> ForeignKeyNames { set; get; }

        public EntityGeneratorWork(Type EntityType)
        {
            this.EntityType = EntityType;
           
            EntityService entityService = new EntityService();
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

        private string InitInludeBind(Type EntityType)
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
            return include_bind;
        }
    }
}