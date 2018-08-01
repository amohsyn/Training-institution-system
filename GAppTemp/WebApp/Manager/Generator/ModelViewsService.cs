using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GApp.WebApp.Manager.Generator
{
    public class ModelViewsService<T> where T : DbContext, new()
    {
        private EntityService<T> _EntityService;
        Dictionary<Type, List<Type>> DefaulsViewsModels;
        public ModelViewsService(Dictionary<Type, List<Type>> DefaulsViewsModels)
        {
            this.DefaulsViewsModels = DefaulsViewsModels;
            _EntityService = new EntityService<T>();
        }

        public Dictionary<Type, List<Type>> getAllViewsModels(Dictionary<Type, List<Type>> DefaulsViewsModels)
        {
            var all_ViewsModels = this.getAllViewsModels();
            foreach (var key in DefaulsViewsModels.Keys)
            {
                if (all_ViewsModels.ContainsKey(key))
                {
                    all_ViewsModels[key].AddRange(DefaulsViewsModels[key]);
                        
                }
                else
                {
                    all_ViewsModels.Add(key, DefaulsViewsModels[key]);
                }
                   
            }
            return all_ViewsModels;
        }

        public Dictionary<Type, List<Type>> getAllViewsModels()
        {
            Dictionary<Type, List<Type>> ViewsModels = new Dictionary<Type, List<Type>>();
            var All_Entities = _EntityService.getAllEntities();
            foreach (Type entityType in All_Entities)
            {
                List<Type> entity_viewsModels = this.getEntityModelsViewsTypes(entityType);
                ViewsModels.Add(entityType, entity_viewsModels);
            }
            return ViewsModels;
        }

        public List<Type> getEntityModelsViewsTypes(Type entityType)
        {
            ModelView_CodeGenerator<T> modelView_CodeGenerator = new ModelView_CodeGenerator<T>(entityType, this.DefaulsViewsModels);
            return modelView_CodeGenerator.ModelsViewsTypes; ;
        }
    }
}
