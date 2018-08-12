using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class SeanceNumbersDAO : BaseDAO<SeanceNumber>{
        
		public SeanceNumbersDAO(DbContext context) : base(context)
		{

        }

   }
}
