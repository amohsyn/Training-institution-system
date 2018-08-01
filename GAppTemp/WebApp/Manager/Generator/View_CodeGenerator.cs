using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GApp.WebApp.Manager.Generator
{
    public class View_CodeGenerator<T> where T : DbContext, new()
    {
        ModelView_CodeGenerator<T> modelView_CodeGenerator;
        Type EntityType;
        public View_CodeGenerator(Type EntityType, Dictionary<Type, List<Type>> Default_ModelsViewsTypes)
        {
            modelView_CodeGenerator = new ModelView_CodeGenerator<T>(EntityType, Default_ModelsViewsTypes);
            this.EntityType = EntityType;
        }

        #region Using_Model
        /// <summary>
        /// Get the modle namespace in the create view
        /// </summary>
        /// <returns></returns>
        public string GetUsingModel_In_CreateView()
        {
            string using_model_namespace = "";
            string format = "@model {0}";
            string model_full_name = "";

            if (modelView_CodeGenerator.getCreateModelView_Type() != null)
            {
                model_full_name = modelView_CodeGenerator.getCreateModelView_Type().FullName;
            }
            else
            {
                model_full_name = this.EntityType.FullName;
            }
            using_model_namespace = string.Format(format, model_full_name);
            return using_model_namespace;
        }
        /// <summary>
        /// Get the modle namespace in the edit view
        /// </summary>
        /// <returns></returns>
        public string GetUsingModel_In_EditView()
        {
            string using_model_namespace = "";
            string format = "@model {0}";
            string model_full_name = "";


            if (modelView_CodeGenerator.getEditModelView_Type() != null)
            {
                model_full_name = modelView_CodeGenerator.getEditModelView_Type().FullName;
            }
            else
            {
                model_full_name = this.EntityType.FullName;
            }
            using_model_namespace = string.Format(format, model_full_name);
            return using_model_namespace;
        }
        /// <summary>
        /// Get the modle namespace in the edit view
        /// </summary>
        /// <returns></returns>
        public string GetUsingModel_In_IndexView()
        {
            string using_model_namespace = "";
            string format = "@model IEnumerable<{0}>";
            string model_full_name = "";

            if (modelView_CodeGenerator.getIndexModelView_Type() != null)
            {
                model_full_name = modelView_CodeGenerator.getIndexModelView_Type().FullName;
            }
            else
            {
                model_full_name = this.EntityType.FullName;
            }
            using_model_namespace = string.Format(format, model_full_name);
            return using_model_namespace;
        }
        public string GetUsingModel_In_DetailsView()
        {
            string using_model_namespace = "";
            string format = "@model {0}";
            string model_full_name = "";

            if (modelView_CodeGenerator.getDetailsModelView_Type() != null)
            {
                model_full_name = modelView_CodeGenerator.getDetailsModelView_Type().FullName;
            }
            else
            {

                model_full_name = this.EntityType.FullName;
            }
            using_model_namespace = string.Format(format, model_full_name);
            return using_model_namespace;
        }
        #endregion
    }
}
