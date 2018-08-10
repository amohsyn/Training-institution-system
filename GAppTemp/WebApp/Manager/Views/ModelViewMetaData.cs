using GApp.WebApp.Manager.Views.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GApp.WebApp.Manager.Views
{
    /// <summary>
    /// Get Model View meta informations
    /// </summary>
    public class Entity_ModelsViewsConfiguration
    {
        public Type TypeOfEntity { set; get; }

        #region Attributes Instance  
        public IndexViewAttribute IndexViewAttribute { set; get; }
        public EditViewAttribute EditViewAttribute { set; get; }
        public CreateViewAttribute CreateViewAttribute { set; get; }
        public DetailsViewAttribute DetailsViewAttribute { set; get; }
        #endregion

        public Entity_ModelsViewsConfiguration(Type type_of_entity)
        {
           this.TypeOfEntity = type_of_entity;
           this.ReadAttributes();
        }

       
        private void ReadAttributes()
        {

            // IndexViewAttribute
            var ls_IndexViewAttribute = this.TypeOfEntity.GetCustomAttributes(typeof(IndexViewAttribute), false);
            if (ls_IndexViewAttribute != null && ls_IndexViewAttribute.Count() > 0) this.IndexViewAttribute = (IndexViewAttribute)ls_IndexViewAttribute[0];

            // CreateViewAttribute
            var ls_CreateViewAttribute = this.TypeOfEntity.GetCustomAttributes(typeof(CreateViewAttribute), false);
            if (ls_CreateViewAttribute != null && ls_CreateViewAttribute.Count() > 0) this.CreateViewAttribute = (CreateViewAttribute)ls_CreateViewAttribute[0];

            // EditViewAttribute
            var ls_EditViewAttribute = this.TypeOfEntity.GetCustomAttributes(typeof(EditViewAttribute), false);
            if (ls_EditViewAttribute != null && ls_EditViewAttribute.Count() > 0) this.EditViewAttribute = (EditViewAttribute) ls_EditViewAttribute[0];


            // DetailsViewAttribute
            var ls_DetailsViewAttribute = this.TypeOfEntity.GetCustomAttributes(typeof(DetailsViewAttribute), false);
            if (ls_DetailsViewAttribute != null && ls_DetailsViewAttribute.Count() > 0) this.DetailsViewAttribute = (DetailsViewAttribute)ls_DetailsViewAttribute[0];
        }
    }
}
