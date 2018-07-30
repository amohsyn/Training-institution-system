using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace System.Web.Mvc
{
    public static class ControllerExtentions
    {
        public static BindAttribute GetBindAttribute(this Controller controller, string MethodeName)
        {

            MethodInfo[] methods = controller.GetType().GetMethods();
            foreach (MethodInfo currentMethod in methods)
            {
                if (currentMethod.Name.Equals(MethodeName))
                {
                    foreach (ParameterInfo paramToAction in currentMethod.GetParameters())
                    {
                        object[] attributes = paramToAction.GetCustomAttributes(true);
                        foreach (object currentAttribute in attributes)
                        {
                            BindAttribute bindAttribute = currentAttribute as BindAttribute;
                            if (bindAttribute == null)
                                continue;
                            else
                                return bindAttribute;
                        }
                    }

                    
                }
            }

            return null;
        }
    }
}
