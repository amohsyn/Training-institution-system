using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TrainingIS.WebApp.Tests.TestUtilities
{
    public class ModelStateTestController : Controller
    {
        public bool TestTryValidateModel(object model)
        {
            return TryValidateModel(model);
        }
    }
}
