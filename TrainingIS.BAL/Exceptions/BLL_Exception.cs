using GApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingIS.BLL.Exceptions
{
    public class BLL_Exception : GAppException
    {
        public BLL_Exception(string message) : base(message)
        {
        }
    }
}
