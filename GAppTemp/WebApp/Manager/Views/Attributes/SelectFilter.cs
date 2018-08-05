using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GApp.WebApp.Manager.Views.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class SelectFilterAttribute : Attribute
    {
        public Type FilteredBy { get; set; }
        public string Code { get; set; }
    }
}
