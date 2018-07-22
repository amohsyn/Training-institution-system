using GApp.Exceptions;
using System;
using System.Runtime.Serialization;

namespace TrainingIS.BLL
{
    [Serializable]
    public class ImportException : GApp.Exceptions.GAppException
    {
       
        public ImportException(string message) : base(message)
        {
        }

      
    }
}