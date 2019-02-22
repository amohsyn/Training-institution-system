using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class TraineeDAO : BaseDAO<Trainee>{
        
		public TraineeDAO(DbContext context) : base(context)
		{

        }

        public override int Insert(Trainee item)
        {

            Check_if_reference_is_not_null(item);
            return base.Insert(item);
        }
        public override int Update(Trainee item)
        {
            Check_if_reference_is_not_null(item);
            return base.Update(item);
        }

        protected void Check_if_reference_is_not_null(Trainee item)
        {
            if (string.IsNullOrEmpty(item.Reference))
            {
                string msg = string.Format("The reference of the entity must not be empty or null", item);
                throw new GApp.DAL.Exceptions.GAppDbException(msg, new System.Exception());
            }
        }

    }
}
