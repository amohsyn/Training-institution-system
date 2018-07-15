using GApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TrainingIS.BLL.Exceptions
{
    [Serializable]
    public class CreateUserException : GAppException
    {
        public CreateUserException(string message) : base(message)
        {
        }
    }
}