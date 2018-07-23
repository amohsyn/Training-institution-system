using System;

namespace GApp.Core.MetaDatas.Attributes
{
    public class ImportAttribute : Attribute
    {
        public ImportAttribute()
        {
            AddAutomatically = false;
        }
        /// <summary>
        /// Add automatically if it exsit as Reference in other Entity
        /// </summary>
        public bool AddAutomatically { set; get; }

        public bool NotCompleteReference  { set; get; }
        
    }
}