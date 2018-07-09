using GApp.Exceptions;
using System;
using System.Runtime.Serialization;

namespace TrainingIS.BLL
{
    [Serializable]
    public class ImportLineException : GApp.Exceptions.GAppException
    {
       
        public ImportLineException(string message) : base(message)
        {
        }

      
    }
}