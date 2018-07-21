using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingIS.BLL
{
    public partial class EntityPropertyShortcutBLO
    {
        public List<Entities.EntityPropertyShortcut> getPropertiesShortcuts(Type type)
        {
            var Query = from s in _UnitOfWork.context.EntityPropertyShortcuts
                        where s.EntityName == type.Name
                        select s;
            return Query.ToList<Entities.EntityPropertyShortcut>();
        }

    }
}
