using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;

namespace TrainingIS.BLL.Services.Import
{
    public class BaseImportExportService
    {
        protected Type EntityType { set; get; }
        protected TrainingISModel Context;

        protected List<string> ForeignKeiesIds { get; }
        protected List<string> ForeignKeiesNames { get; }
        protected List<string> ManyKeiesNames { get; }
        protected List<string> ManyToManyKeiesNames { get; }
        protected List<string> One_To_Many_ForeignKeiesNames { get; }

        public BaseImportExportService(Type EntityType)
        {
            this.EntityType = EntityType;
            this.Context = new TrainingISModel();

            this.ForeignKeiesIds = this.Context.GetForeignKeysIds(this.EntityType);
            this.ForeignKeiesNames = this.Context.GetForeignKeyNames(this.EntityType).ToList();
            this.ManyKeiesNames = this.Context.Get_Many_ForeignKeyNames(this.EntityType).ToList();
            this.ManyToManyKeiesNames = this.Context.Get_Many_To_Many_ForeignKeyNames(this.EntityType).ToList();
            this.One_To_Many_ForeignKeiesNames = this.Context.Get_One_To_Many_ForeignKeyNames(this.EntityType).ToList();
        }

        public List<PropertyInfo> GetExportedProperties()
        {
            return this.EntityType.GetProperties()
                        .Where(property => !property.IsDefined(typeof(NotMappedAttribute)))
                        .Where(property => !this.ForeignKeiesIds.Contains(property.Name))
                        .Where(property => !this.One_To_Many_ForeignKeiesNames.Contains(property.Name))
                        .Where(property => property.Name != "Id")
                        .ToList();
        }
    }
}
