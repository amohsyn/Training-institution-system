using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class JustificationAbsenceDAO : BaseDAO<JustificationAbsence>{
        
		public JustificationAbsenceDAO(DbContext context) : base(context)
		{

        }

   }
}
