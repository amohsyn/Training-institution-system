using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class FormerDAO : BaseDAO<Former>{
        
		public FormerDAO(DbContext context) : base(context)
		{

        }

   }
}
