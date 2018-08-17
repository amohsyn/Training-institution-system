using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class MetierDAO : BaseDAO<Metier>{
        
		public MetierDAO(DbContext context) : base(context)
		{

        }

   }
}
