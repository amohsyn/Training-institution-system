using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GApp.WebApp.Tests
{
    public class ManagerControllerTests
    {
        public static List<ValidationResult> ValidateViewModel(Controller controller, object viewModelToValidate)
        {
            var validationContext = new ValidationContext(viewModelToValidate, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(viewModelToValidate, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                controller.ModelState.AddModelError(validationResult.MemberNames.FirstOrDefault() ?? string.Empty, validationResult.ErrorMessage);
            }
            return validationResults;
        }

        public static void PreBindModel(Controller controller, object viewModel, string operationName)
        {

            MethodInfo[] methods = controller.GetType().GetMethods();
            foreach (MethodInfo currentMethod in methods)
            {
                if (currentMethod.Name.Equals(operationName))
                {
                    bool foundParamAttribute = false;
                    foreach (ParameterInfo paramToAction in currentMethod.GetParameters())
                    {
                        object[] attributes = paramToAction.GetCustomAttributes(true);
                        foreach (object currentAttribute in attributes)
                        {
                            BindAttribute bindAttribute = currentAttribute as BindAttribute;
                            if (bindAttribute == null)
                                continue;

                            PropertyInfo[] allProperties = viewModel.GetType().GetProperties();
                            IEnumerable<PropertyInfo> propertiesToReset =
                                allProperties.Where(x => bindAttribute.IsPropertyAllowed(x.Name) == false);

                            foreach (PropertyInfo propertyToReset in propertiesToReset)
                            {
                                propertyToReset.SetValue(viewModel, null);
                            }

                            foundParamAttribute = true;
                        }
                    }

                    if (foundParamAttribute)
                        return;
                }
            }


            //// Methode with [Post]

            //// Find MMethode info
            //MethodInfo methodInfo = controller.GetType().GetMethods()
            //   .Where(m => m.Name.Equals(operationName))
            //   .Where(m =>  (m.GetCustomAttribute(typeof(HttpPostAttribute)) != null))
            //   .FirstOrDefault();
            //if (methodInfo == null) return;

            //foreach (ParameterInfo paramToAction in methodInfo.GetParameters())
            //{
            //    object[] attributes = paramToAction.GetCustomAttributes(true);
            //    foreach (object currentAttribute in attributes)
            //    {
            //        BindAttribute bindAttribute = currentAttribute as BindAttribute;
            //        if (bindAttribute == null)
            //            continue;

            //        PropertyInfo[] allProperties = viewModel.GetType().GetProperties();
            //        IEnumerable<PropertyInfo> propertiesToReset =
            //            allProperties.Where(x => bindAttribute.IsPropertyAllowed(x.Name) == false);

            //        foreach (PropertyInfo propertyToReset in propertiesToReset)
            //        {
            //            propertyToReset.SetValue(viewModel, null);
            //        }
            //    }
            //}

        }

    }
}
