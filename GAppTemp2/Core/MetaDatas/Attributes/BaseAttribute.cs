using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GApp.Core.MetaDatas.Attributes
{
    public class BaseAttribute : Attribute
    {
        /// <summary>
        /// Determine whether the annotation is locatable
        /// Default is True
        /// </summary>
        public bool Localizable { set; get; }

        public BaseAttribute()
        {
            Localizable = true;
        }



    }
}
