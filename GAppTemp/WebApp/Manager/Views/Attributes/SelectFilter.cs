using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GApp.WebApp.Manager.Views.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SelectFilterAttribute : Attribute
    {
 
        /// <summary>
        /// Html Id of the Filter ComboBox
        /// </summary>
        public string Filter_HTML_Id { get; set; }
    }
}
