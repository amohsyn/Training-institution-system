using System;

namespace GApp.WebApp.Manager.Views.Attributes
{
    /// <summary>
    /// Bind this propert another property in the current view
    /// </summary>
    public class ReadFromAttribute : Attribute
    {
        public object PropertyName { get; set; }
        public bool ReadOnly { get; set; }
    }
}