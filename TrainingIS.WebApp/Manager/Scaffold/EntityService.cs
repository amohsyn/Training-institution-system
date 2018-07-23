using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.Linq;
using System.Web;
using TrainingIS.DAL;

namespace TrainingIS.WebApp.Manager.Scaffold
{
    public class EntityService
    {
        public List<Type> getAllEntities()
        {
            TrainingISModel context = new TrainingISModel();
            return context.GetAllTypesInContextOrder();
        }
        public string Pluralize(string EntityName)
        {
            CultureInfo ci = new CultureInfo("en-us");
            PluralizationService ps =
              PluralizationService.CreateService(ci);
           return ps.Pluralize(EntityName);
        }

        public List<string> GetForeignKeyNames(Type EntityType)
        {
            TrainingISModel context = new TrainingISModel();
            return context.GetForeignKeyNames(EntityType).ToList<string>();
            
        }
        public List<string> GetForeignKeiesIds(Type EntityType)
        {
            TrainingISModel context = new TrainingISModel();
            return context.GetForeignKeysIds(EntityType).ToList<string>();

        }
        public string InludeBind(Type EntityType)
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