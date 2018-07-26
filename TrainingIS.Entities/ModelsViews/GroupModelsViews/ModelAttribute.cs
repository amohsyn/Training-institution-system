using System;

namespace TrainingIS.Entities.ModelsViews.GroupModelsViews
{
    /// <summary>
    /// Indicate the Model Type of ModelView
    /// </summary>
    public class ModelAttribute : Attribute
    {
        public Type TypeOfModel { set; get; }
        public ModelAttribute(Type TypeOfModel)
        {
            this.TypeOfModel = TypeOfModel;
        }
    }
}