namespace TrainingIS.DAL
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using TrainingIS.Entities;

    public class TrainingISModel : DbContext
    {
     
        public TrainingISModel()
            : base(@"data source=(LocalDb)\MSSQLLocalDB;initial catalog=TrainingIS;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
        {
        }

        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Specialty> Specialtys { get; set; }


        #region Singleton
        private static TrainingISModel _ContextInstance;
        public static TrainingISModel CreateContext()
        {
            if (_ContextInstance == null)
            {
                _ContextInstance = new TrainingISModel();
                return _ContextInstance;
            }
            else return _ContextInstance;
        }
        #endregion

    }
}