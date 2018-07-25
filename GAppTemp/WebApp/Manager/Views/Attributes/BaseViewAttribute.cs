using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GApp.WebApp.Manager.Views.Attributes
{
    public class BaseViewAttribute : Attribute
    {
        public Type TypeOfView { set; get; }
        public BaseViewAttribute(Type TypeOfView)
        {
            this.TypeOfView = TypeOfView;
        }

        public BaseViewAttribute()
        {
        }
    }
}
