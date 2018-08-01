using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GApp.WebApp.Manager.Generator
{
    public class RelationShip_CodeGenerator<T> where T : DbContext, new()
    {
        public List<string> ForeignKeiesIds { set; get; }
        public List<string> ForeignKeyNames { set; get; }

        private List<string> _ManyRelationsShipNames = null;
        public List<string> ManyRelationsShipNames
        {
            get
            {
                if (_ManyRelationsShipNames == null)
                {
                    T context = new T();
                    _ManyRelationsShipNames = context.Get_Many_ForeignKeyNames(this.EntityType).ToList<string>();
                    return _ManyRelationsShipNames;
                }
                else
                    return _ManyRelationsShipNames;
            }
        }

        public Type EntityType { set; get; }

        public RelationShip_CodeGenerator(Type EntityType)
        {
            this.EntityType = EntityType;
            this.ForeignKeiesIds = this.GetForeignKeiesIds(this.EntityType);
            this.ForeignKeyNames = this.GetForeignKeyNames(this.EntityType);
        }

      

        private List<string> GetForeignKeyNames(Type EntityType)
        {
            T context = new T();
            return context.GetForeignKeyNames(EntityType).ToList<string>();

        }
        private List<string> GetForeignKeiesIds(Type EntityType)
        {
            T context = new T();
            return context.GetForeignKeysIds(EntityType).ToList<string>();

        }
    }
}
