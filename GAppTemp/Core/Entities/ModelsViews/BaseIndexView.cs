using System.Collections.Generic;

namespace GApp.Core.Entities.ModelsViews
{
    public class BaseIndexView<T>
    {
        public List<T> Data { set; get; }
        public BaseIndexView()
        {
            Data = new List<T>();
        }
    }
}