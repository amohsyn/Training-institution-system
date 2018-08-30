using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace System.Web.Mvc
{
    public static class HtmlHelper_Extentions
    {
        public static bool IsDebug(this HtmlHelper htmlHelper)
        {
           
#if DEBUG
      return true;
#else
            return false;
#endif
        }
    }
}