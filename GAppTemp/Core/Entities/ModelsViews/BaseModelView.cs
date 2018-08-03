using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GApp.Core.Entities.ModelsViews
{
    public class BaseModelView 
    {
        public Int64 Id { set; get; }

        [Display(AutoGenerateField = false)]
        public string toStringValue { set; get; }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(this.toStringValue))
                return this.toStringValue;
            return base.ToString();
        }

    }
}
