namespace TrainingIS.DAL
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;
    using System.Data.Entity.Core.Metadata.Edm;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;
    using TrainingIS.Entitie_excludes;
    using TrainingIS.Entities;
    using System.Configuration;
    using System.Reflection;
    using System.Runtime;
    using TrainingIS.DAL.Properties;
    using GApp.Entities;

    public class TrainingISModel : IdentityDbContext<ApplicationUser>
    {
        public static string Current_Data_Base_Name = "";
        static TrainingISModel()
        {
            var type = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
            if (type == null)
                throw new Exception("Do not remove, ensures static reference to System.Data.Entity.SqlServer");
        }

        //public TrainingISModel(string ConnectionString) : base(ConnectionString, throwIfV1Schema: false)
        //{
        //    // @"data source=(LocalDb)\MSSQLLocalDB;initial catalog=TrainingIS;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"
        //}

        public TrainingISModel() : base(GetConnectionString(), throwIfV1Schema: false)
        {
            // @"data source=(LocalDb)\MSSQLLocalDB;initial catalog=TrainingIS;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"
        }

        public static string GetConnectionString()
        {

            // Default ConnectionString
            string ConnectionString = "";
            var CompileConfiguration = Settings.Default.CompileConfiguration;
            string DataBaseName = "Cplus_" + CompileConfiguration;
            ConnectionString = string.Format(@"data source=(LocalDb)\MSSQLLocalDB;initial catalog={0};integrated security=True;MultipleActiveResultSets=True;App=EntityFramework", DataBaseName);
            Current_Data_Base_Name = "(LocalDb)/" + DataBaseName;


            // Debug - Default
            ConnectionString = @"server = .\SQLEXPRESS; database = Cplus_Release; User=sa;Password=admintp4;";
            Current_Data_Base_Name = @".\SQLEXPRESS/Cplus_Release";


            if (CompileConfiguration == "Data")
            {
                DataBaseName = "Cplus_Release";
                ConnectionString = string.Format(@"data source=(LocalDb)\MSSQLLocalDB;initial catalog={0};integrated security=True;MultipleActiveResultSets=True;App=EntityFramework", DataBaseName);
                Current_Data_Base_Name = "(LocalDb)/" + DataBaseName;
            }
            if (CompileConfiguration == "Release")
            {
                ConnectionString = @"server = .\SQLEXPRESS; database = Cplus_Release; User=sa;Password=admintp4;";
                Current_Data_Base_Name = @".\SQLEXPRESS/" + DataBaseName;
            }

            
            return ConnectionString;
        }
        // ! important : The DbSet is in order of Import

        // Order 1
        //
        // ApplicationParams
        public virtual DbSet<ApplicationParam> ApplicationParams { get; set; }
        public virtual DbSet<LogWork> LogWorks { get; set; }

        public virtual DbSet<RoleApp> RoleApps { get; set; }
        public virtual DbSet<ControllerApp> ControllerApps { get; set; }
        public virtual DbSet<ActionControllerApp> ActionControllerApps { get; set; }
        public virtual DbSet<AuthrorizationApp> AuthrorizationApps { get; set; }


        public virtual DbSet<EntityPropertyShortcut> EntityPropertyShortcuts { get; set; }
        public virtual DbSet<ClassroomCategory> ClassroomCategories { get; set; }
        public DbSet<Nationality> Nationalities { get; set; }


        



        // Training Params
        public virtual DbSet<TrainingYear> TrainingYears { get; set; }
        public virtual DbSet<TrainingType> TrainingTypes { get; set; }
        public virtual DbSet<SeanceDay> SeanceDays { get; set; }
        public virtual DbSet<SeanceNumber> SeanceNumbers { get; set; }
        public virtual DbSet<YearStudy> YearStudies { get; set; }
        public virtual DbSet<Specialty> Specialties { get; set; }
        public DbSet<Schoollevel> Schoollevels { get; set; }
        public DbSet<FormerSpecialty> FormerSpecialties { get; set; }
        public DbSet<TrainingLevel> TrainingLevels { get; set; }
        public DbSet<Metier> Metiers { get; set; }
        public DbSet<Sector> Sectors { get; set; }


        // Order 2
        //
        // Etablishement
        public virtual DbSet<Classroom> Classrooms { get; set; }
        // Ressources
        public virtual DbSet<ModuleTraining> ModuleTrainings { get; set; }
        public virtual DbSet<Former> Formers { get; set; }
        // Trainee
        public virtual DbSet<Group> Groups { get; set; }
        // Trainings
        public virtual DbSet<Schedule> Schedules { get; set; }


        // Order 3
        //
        public virtual DbSet<Trainee> Trainees { get; set; }
        // Training 
        public virtual DbSet<Training> Trainings { get; set; }

        // Order 5
        //
        // Planning
        public virtual DbSet<SeancePlanning> SeancePlannings { get; set; }


        // Order 6
        //
        public virtual DbSet<SeanceTraining> SeanceTrainings { get; set; }


        // Order 7
        // Absence
        public virtual DbSet<Absence> Absences { get; set; }

        // Order 8
        //
        public virtual DbSet<StateOfAbsece> StateOfAbseces { get; set; }






        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);

            //// Authorization

            //modelBuilder.Entity<AuthrorizationApp>()
            //    .HasRequired<ControllerApp>(c => c.ControllerApp)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);



            //// Etablishement Params
            ////
            //// Classroom
            //modelBuilder.Entity<Classroom>()
            //    .HasRequired<ClassroomCategory>(c => c.ClassroomCategory)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);


            //// Training Params
            ////



            //// Modules Management
            ////
            //// Module
            //modelBuilder.Entity<ModuleTraining>()
            //   .HasRequired<Specialty>(c => c.Specialty)
            //   .WithMany()
            //   .WillCascadeOnDelete(false);

            //// Trainee Management
            ////
            //// Group
            //modelBuilder.Entity<Group>()
            //    .HasRequired<Specialty>(c => c.Specialty)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);
            //modelBuilder.Entity<Group>()
            //    .HasRequired<TrainingType>(c => c.TrainingType)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);
            //modelBuilder.Entity<Group>()
            //    .HasRequired<TrainingYear>(c => c.TrainingYear)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);
            //modelBuilder.Entity<Group>()
            //    .HasRequired<YearStudy>(c => c.YearStudy)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);


            //// Trainnee
            //modelBuilder.Entity<Trainee>()
            //   .HasRequired<Group>(c => c.Group)
            //   .WithMany()
            //   .WillCascadeOnDelete(false);
            //modelBuilder.Entity<Trainee>()
            //  .HasRequired<Nationality>(c => c.Nationality)
            //  .WithMany()
            //  .WillCascadeOnDelete(false);


            //// Training Management
            ////
            //// Training
            //modelBuilder.Entity<Training>()
            //   .HasRequired<TrainingYear>(c => c.TrainingYear)
            //   .WithMany()
            //   .WillCascadeOnDelete(false);
            //modelBuilder.Entity<Training>()
            //   .HasRequired<Former>(c => c.Former)
            //   .WithMany()
            //   .WillCascadeOnDelete(false);
            //modelBuilder.Entity<Training>()
            //  .HasRequired<ModuleTraining>(c => c.ModuleTraining)
            //  .WithMany()
            //  .WillCascadeOnDelete(false);
            //modelBuilder.Entity<Training>()
            //  .HasRequired<Group>(c => c.Group)
            //  .WithMany()
            //  .WillCascadeOnDelete(false);


            //// Planning Management
            ////
            //// SeancePlanning
            //modelBuilder.Entity<SeancePlanning>()
            //   .HasRequired<Training>(c => c.Training)
            //   .WithMany()
            //   .WillCascadeOnDelete(false);
            //modelBuilder.Entity<SeancePlanning>()
            //   .HasRequired<SeanceDay>(c => c.SeanceDay)
            //   .WithMany()
            //   .WillCascadeOnDelete(false);
            //modelBuilder.Entity<SeancePlanning>()
            //   .HasRequired<SeanceNumber>(c => c.SeanceNumber)
            //   .WithMany()
            //   .WillCascadeOnDelete(false);

            //// SeanceTraining Management
            ////
            //// SeanceTraining
            //modelBuilder.Entity<SeanceTraining>()
            //  .HasRequired<SeancePlanning>(c => c.SeancePlanning)
            //  .WithMany()
            //  .WillCascadeOnDelete(false);


            ////
            //// Absence Management
            ////
            //// Absence
            //modelBuilder.Entity<Absence>()
            // .HasRequired<SeanceTraining>(c => c.SeanceTraining)
            // .WithMany()
            // .WillCascadeOnDelete(false);
        }

        public static TrainingISModel Create()
        {
            return new TrainingISModel();
        }


    }
}