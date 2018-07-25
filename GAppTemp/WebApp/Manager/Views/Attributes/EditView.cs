using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GApp.WebApp.Manager.Views.Attributes
{
    public class EditViewAttribute : BaseViewAttribute
    {
        public EditViewAttribute(Type TypeOfView) : base(TypeOfView)
        {
        }
    }
}
