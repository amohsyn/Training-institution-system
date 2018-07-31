using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GApp.WebApp.Manager.Generator
{
    public partial class EntityGeneratorWork<T>
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


        private void InitRelationShip()
        {
            EntityService<T> entityService = new EntityService<T>();
            this.ForeignKeiesIds = entityService.GetForeignKeiesIds(this.EntityType);
            this.ForeignKeyNames = entityService.GetForeignKeyNames(this.EntityType);
        }

    }
}
