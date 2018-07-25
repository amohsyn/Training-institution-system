using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GApp.WebApp.Manager.Views.Attributes
{
    public class DetailsViewAttribute : BaseViewAttribute
    {
        public DetailsViewAttribute(Type TypeOfView) : base(TypeOfView)
        {
        }
    }
}
