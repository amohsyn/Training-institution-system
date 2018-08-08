using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GApp.WebApp.Manager.Views.Attributes
{
    public class ComboBoxAttribute : Attribute
    {
        public Type DataFrom { set; get; }
    }
}
