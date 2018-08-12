using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class FormersDAO : BaseDAO<Former>{
        
		public FormersDAO(DbContext context) : base(context)
		{

        }

   }
}
