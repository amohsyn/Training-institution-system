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

        // Etablishement
        public virtual DbSet<Classroom> Classrooms { get; set; }
        public virtual DbSet<ClassroomCategory> ClassroomCategories { get; set; }
        public virtual DbSet<Former> Formers { get; set; }

        // Training Management
        public virtual DbSet<Specialty> Specialtys { get; set; }
        public virtual DbSet<TrainingType> TrainingTypes { get; set; }
        public virtual DbSet<ModuleTraining> Modules { get; set; }
        public virtual DbSet<Training> Training { get; set; }


        // 
        public virtual DbSet<TrainingYear> TrainingYears { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Trainee> Trainees { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Classroom
            modelBuilder.Entity<Classroom>()
                .HasRequired<ClassroomCategory>(c => c.ClassroomCategory)
                .WithMany()
                .WillCascadeOnDelete(false);

            // Group
            modelBuilder.Entity<Group>()
                .HasRequired<Specialty>(c => c.Specialty)
                .WithMany()
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Group>()
                .HasRequired<TrainingType>(c => c.TrainingType)
                .WithMany()
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Group>()
                .HasRequired<TrainingYear>(c => c.TrainingYear)
                .WithMany()
                .WillCascadeOnDelete(false);

            // Trainnee
            modelBuilder.Entity<Trainee>()
               .HasRequired<Group>(c => c.Group)
               .WithMany()
               .WillCascadeOnDelete(false);

            // Module
            modelBuilder.Entity<ModuleTraining>()
               .HasRequired<Specialty>(c => c.Specialty)
               .WithMany()
               .WillCascadeOnDelete(false);

            // Training
            modelBuilder.Entity<Training>()
               .HasRequired<TrainingYear>(c => c.TrainingYear)
               .WithMany()
               .WillCascadeOnDelete(false);
            modelBuilder.Entity<Training>()
               .HasRequired<Former>(c => c.Former)
               .WithMany()
               .WillCascadeOnDelete(false);
            modelBuilder.Entity<Training>()
              .HasRequired<ModuleTraining>(c => c.ModuleTraining)
              .WithMany()
              .WillCascadeOnDelete(false);
            modelBuilder.Entity<Training>()
              .HasRequired<Group>(c => c.Group)
              .WithMany()
              .WillCascadeOnDelete(false);


        }



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