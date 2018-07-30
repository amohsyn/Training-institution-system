using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace System.Web.Mvc
{
    public static class ModelStatesExtentions
    {
        public static void AddModelError(this ModelStateDictionary ModelStates, IList<ValidationResult> validationResults)
        {
            if(validationResults != null)
            {
                foreach (var item in validationResults)
                {
                    ModelStates.AddModelError(item.ErrorMessage, item.MemberNames?.First());
                }
            }
        }
    }
}
