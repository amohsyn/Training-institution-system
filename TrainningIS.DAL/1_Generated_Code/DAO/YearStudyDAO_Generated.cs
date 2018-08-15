using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class YearStudyDAO : BaseDAO<YearStudy>{
        
		public YearStudyDAO(DbContext context) : base(context)
		{

        }

   }
}
