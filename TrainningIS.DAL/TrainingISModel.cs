namespace TrainingIS.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Core.Metadata.Edm;
    using System.Linq;
    using TrainingIS.Entities;

    public class TrainingISModel : DbContext
    {
     
        public TrainingISModel()
            : base(@"data source=(LocalDb)\MSSQLLocalDB;initial catalog=TrainingIS;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
        {
        }
        // ApplicationParams
        public virtual DbSet<ApplicationParam> ApplicationParams { get; set; }
        public virtual DbSet<EntityPropertyShortcut> EntityPropertyShortcuts { get; set; }

        // Etablishement Params
        public virtual DbSet<Classroom> Classrooms { get; set; }
        public virtual DbSet<ClassroomCategory> ClassroomCategories { get; set; }
        // Training Params
        public virtual DbSet<TrainingType> TrainingTypes { get; set; }
        public virtual DbSet<SeanceDay> SeanceDays { get; set; }
        public virtual DbSet<SeanceNumber> SeanceNumbers { get; set; }
        public virtual DbSet<YearStudy> YearStudies { get; set; }


        // Modules Management
        public virtual DbSet<Specialty> Specialtys { get; set; }
        public virtual DbSet<ModuleTraining> Modules { get; set; }

        // Formers
        public virtual DbSet<Former> Formers { get; set; }

        // Trainee
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Trainee> Trainees { get; set; }

        // Training 
        public virtual DbSet<TrainingYear> TrainingYears { get; set; }
        public virtual DbSet<Training> Training { get; set; }
        public virtual DbSet<SeanceTraining> SeanceTrainings { get; set; }
       
        // Planning
        public virtual DbSet<SeancePlanning> SeancePlanning { get; set; }

        // Absence
        public virtual DbSet<Absence> Absences { get; set; }
        public virtual DbSet<StateOfAbsece> StateOfAbseces { get; set; }


        public System.Data.Entity.DbSet<TrainingIS.Entities.Nationality> Nationalities { get; set; }
        public System.Data.Entity.DbSet<TrainingIS.Entities.Schoollevel> Schoollevels { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            // Etablishement Params
            //
            // Classroom
            modelBuilder.Entity<Classroom>()
                .HasRequired<ClassroomCategory>(c => c.ClassroomCategory)
                .WithMany()
                .WillCascadeOnDelete(false);


            // Training Params
            //
      
            

            // Modules Management
            //
            // Module
            modelBuilder.Entity<ModuleTraining>()
               .HasRequired<Specialty>(c => c.Specialty)
               .WithMany()
               .WillCascadeOnDelete(false);

            // Trainee Management
            //
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
            modelBuilder.Entity<Group>()
                .HasRequired<YearStudy>(c => c.YearStudy)
                .WithMany()
                .WillCascadeOnDelete(false);

            
            // Trainnee
            modelBuilder.Entity<Trainee>()
               .HasRequired<Group>(c => c.Group)
               .WithMany()
               .WillCascadeOnDelete(false);
            modelBuilder.Entity<Trainee>()
              .HasRequired<Nationality>(c => c.Nationality)
              .WithMany()
              .WillCascadeOnDelete(false);
           

            // Training Management
            //
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


            // Planning Management
            //
            // SeancePlanning
            modelBuilder.Entity<SeancePlanning>()
               .HasRequired<Training>(c => c.Training)
               .WithMany()
               .WillCascadeOnDelete(false);
            modelBuilder.Entity<SeancePlanning>()
               .HasRequired<SeanceDay>(c => c.SeanceDay)
               .WithMany()
               .WillCascadeOnDelete(false);
            modelBuilder.Entity<SeancePlanning>()
               .HasRequired<SeanceNumber>(c => c.SeanceNumber)
               .WithMany()
               .WillCascadeOnDelete(false);

            // SeanceTraining Management
            //
            // SeanceTraining
            modelBuilder.Entity<SeanceTraining>()
              .HasRequired<SeancePlanning>(c => c.SeancePlanning)
              .WithMany()
              .WillCascadeOnDelete(false);


            //
            // Absence Management
            //
            // Absence
            modelBuilder.Entity<Absence>()
             .HasRequired<SeanceTraining>(c => c.SeanceTraining)
             .WithMany()
             .WillCascadeOnDelete(false);
        }


      
 
    }
}