using System;

namespace GApp.Core.MetaDatas.Attributes
{
    public class ImportAttribute : Attribute
    {
        /// <summary>
        /// Add automatically if it exsit as Reference in other Entity
        /// </summary>
        public bool AddAutomatically { set; get; }
    }
}