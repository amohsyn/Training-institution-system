using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GApp.Core.MetaDatas.Attributes
{
    public enum UserInterfaces
    {
        MultiSelect,
        Checkbox
    }
    public class ManyAttribute : Attribute
    {
        public UserInterfaces userInterfaces { set; get; }
        public Type TypeOfEntity { set; get; }
    }
}
