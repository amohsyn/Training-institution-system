using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class Category_JustificationAbsenceDAO : BaseDAO<Category_JustificationAbsence>{
        
		public Category_JustificationAbsenceDAO(DbContext context) : base(context)
		{

        }

   }
}
