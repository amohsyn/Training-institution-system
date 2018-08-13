using GApp.BLL.Services;
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
            EntityMetaDataService EntityMetaDataService = EntityMetaDataService.CreateConfigEntity(type);
            return EntityMetaDataService.entityMetataData.SingularName;
        }
        public static string getLocalPluralName(this Type type)
        {
            EntityMetaDataService EntityMetaDataService = EntityMetaDataService.CreateConfigEntity(type);
            return EntityMetaDataService.entityMetataData.PluralName;
        }
    }
}
