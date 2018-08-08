




using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingIS.Entities.ModelsViews.Generated
{
    public partial class DefaultModelView_MetaData : Base_DefaultModelView_MetaData
    {
        protected List<Type> _List_Default_ModelsViewsTypes;
        public List<Type> List_Default_ModelsViewsTypes
        {
            get
            {
                if (_List_Default_ModelsViewsTypes == null)
                {
                    foreach (var key in this.ModelsViewsTypes.Keys)
                    {
                        _List_Default_ModelsViewsTypes.AddRange(this.ModelsViewsTypes[key]);
                    }
                }
                return _List_Default_ModelsViewsTypes;

            }
        }
        
        public DefaultModelView_MetaData() : base()
        {
          
            
 
        }

        
    }
}


