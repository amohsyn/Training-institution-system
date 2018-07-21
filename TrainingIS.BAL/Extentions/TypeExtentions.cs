using GApp.Core.MetaDatas.ReadConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Extentions
{
    public static class TypeExtentions
    {
        public static string getLocalName(this Type type)
        {
            EntityMetaDataConfiguratrion entityMetaDataConfiguratrion = EntityMetaDataConfiguratrion.CreateConfigEntity(type);
            return entityMetaDataConfiguratrion.entityMetataData.SingularName;
        }
    }
}
