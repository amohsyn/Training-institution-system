namespace App.DAL.Database
{
    using App.GroupManagement.Entities;

    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class ModelContext : DbContext
    {
        public ModelContext()
            : base(@"data source=(LocalDb)\MSSQLLocalDB;initial catalog=TrainingIS;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
        {
        }
        private static ModelContext _ContextInstance;
        public static ModelContext CreateContext()
        {
            if (_ContextInstance == null)
            {
                _ContextInstance = new ModelContext();
                return _ContextInstance;
            }
            else return _ContextInstance;
        }

        // Group Management
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Specialty> Specialtys { get; set; }

        //// Project Management
        //public virtual DbSet<TaskCategory> TaskCategorys { get; set; }
        //public virtual DbSet<ProjectCategory> ProjectCategorys { get; set; }
        //public virtual DbSet<ProjectTask> ProjectTasks { get; set; }
        //public virtual DbSet<Project> Projects { get; set; }

    }


}