using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GApp.WebApp.Manager.Views.Attributes
{
    public class IndexViewAttribute : BaseViewAttribute
    {
        public IndexViewAttribute(Type TypeOfView) : base(TypeOfView)
        {
        }
    }
}
