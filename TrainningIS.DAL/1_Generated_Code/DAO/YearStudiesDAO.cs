using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class YearStudiesDAO : BaseDAO<YearStudy>{
        
		public YearStudiesDAO(DbContext context) : base(context)
		{

        }

   }
}
