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
    using Effort;
    using System.Data.Common;

    public class TrainingISModel : IdentityDbContext<ApplicationUser>
    {
        public static string Current_Data_Base_Name = "";
        public static bool IsTest = false;
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
            ConnectionString = string.Format(@"Data source=(LocalDb)\MSSQLLocalDB;initial catalog={0};integrated security=True;MultipleActiveResultSets=True;App=EntityFramework", DataBaseName);
            Current_Data_Base_Name = "(LocalDb)/" + DataBaseName;


            // 
            // Debug - Default
            //
            ConnectionString = @"server = .\SQLEXPRESS; database = Cplus_Developpement; User=sa;Password=admintp4;";
            Current_Data_Base_Name = @".\SQLEXPRESS/Cplus_Developpement";

            if (CompileConfiguration == "Data")
            {
                ConnectionString = @"server = .\SQLEXPRESS; database = Cplus_Data; User=sa;Password=admintp4;";
                Current_Data_Base_Name = @".\SQLEXPRESS/Cplus_Data";
            }
            if (CompileConfiguration == "Release")
            {
                ConnectionString = @"server = .\SQLEXPRESS; database = Cplus_Release; User=sa;Password=admintp4;";
                Current_Data_Base_Name = @".\SQLEXPRESS/Cplus_Release";
            }
            if (CompileConfiguration == "Test")
            {
                ConnectionString = @"server = .\SQLEXPRESS; database = Cplus_Test; User=sa;Password=admintp4;";
                Current_Data_Base_Name = @".\SQLEXPRESS/Cplus_Test";
                IsTest = true;
            }
            if (CompileConfiguration == "TestData")
            {
                ConnectionString = @"server = .\SQLEXPRESS; database = Cplus_Test; User=sa;Password=admintp4;";
                Current_Data_Base_Name = @".\SQLEXPRESS/Cplus_Test";
            }









            return ConnectionString;
        }
        // ! important : The DbSet is in order of Import

        // Order 1
        //

        // 

        public virtual DbSet<CalendarDay> CalendarDaies { get; set; }

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

        // Picture
        public DbSet<GPicture> GPictures { get; set; }

        //Project Manager
        public DbSet<Project> Projects { get; set; }

        // Absences
        public DbSet<Category_JustificationAbsence> Category_JustificationAbsences { get; set; }
        public DbSet<Category_WarningTrainee> Category_WarningTrainees { get; set; }


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

        // RH
        public DbSet<Function> Functions { get; set; }

        // WorkGroup
        public DbSet<Mission_Working_Group> Mission_Working_Groups { get; set; }

        // Sanctions
       

        public DbSet<DisciplineCategory> DisciplineCategories { get; set; }

        // Order 2
        //
        // Etablishement
        public virtual DbSet<Classroom> Classrooms { get; set; }
        // Ressources
        public virtual DbSet<ModuleTraining> ModuleTrainings { get; set; }
        public virtual DbSet<Former> Formers { get; set; }
        public virtual DbSet<Administrator> Administrators { get; set; }


        // Trainee
        public virtual DbSet<Group> Groups { get; set; }
        // Trainings
        public virtual DbSet<Schedule> Schedules { get; set; }
        //Project Manager
        public DbSet<TaskProject> TaskProject { get; set; }
        // Absences
        public DbSet<JustificationAbsence> JustificationAbsences { get; set; }
        public DbSet<WarningTrainee> WarningTrainees { get; set; }
        // Sanction
        public DbSet<SanctionCategory> SanctionCategories { get; set; }

        // Order 3
        //
        public virtual DbSet<Trainee> Trainees { get; set; }
        // Training 
        public virtual DbSet<Training> Trainings { get; set; }


        // Order 4
        //
        // WorkGroup
        public virtual DbSet<WorkGroup> WorkGroups { get; set; }

        // Order 5
        //
        // Planning
        public virtual DbSet<SeancePlanning> SeancePlannings { get; set; }
        // WorkGroup
        public virtual DbSet<Meeting> Meetings { get; set; }


        // Order 6
        //
        public virtual DbSet<SeanceTraining> SeanceTrainings { get; set; }




        // Order 7
        // Absence
        public virtual DbSet<Absence> Absences { get; set; }

        // Order 8
        //
        public virtual DbSet<StateOfAbsece> StateOfAbseces { get; set; }
        //Sanctions
        public virtual DbSet<Sanction> Sanctions { get; set; }





        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //modelBuilder.Entity<Account>()
            //.HasMany(a => a.Products)
            //.WithMany()
            //.Map(x =>
            //{
            //    x.MapLeftKey("Account_Id");
            //    x.MapRightKey("Product_Id");
            //    x.ToTable("AccountProducts");
            //});

            // 
            // WorkGroup Many-to-Many relationship
            // Members
            modelBuilder.Entity<WorkGroup>()
                .HasMany<Former>(w => w.MemebersFormers)
                .WithMany()
                .Map(x =>
                {
                    x.ToTable("WorkGroups_Memebers_Formers");
                });

            modelBuilder.Entity<WorkGroup>()
                .HasMany<Administrator>(w => w.MemebersAdministrators)
                .WithMany()
                .Map(x =>
                {
                    x.ToTable("WorkGroups_Memebers_Administrators");
                });
            modelBuilder.Entity<WorkGroup>()
              .HasMany<Trainee>(w => w.MemebersTrainees)
                .WithMany()
                .Map(x =>
                {
                    x.ToTable("WorkGroups_Memebers_Trainees");
                });

            // 
            // Meeting Many-to-Many relationship
            // Presence of Memebers
            modelBuilder.Entity<Meeting>()
                .HasMany<Former>(w => w.Presences_Of_Formers)
                .WithMany()
                .Map(x =>
                {
                    x.ToTable("Meetings_Presences_Of_Formers");
                });
            modelBuilder.Entity<Meeting>()
               .HasMany<Administrator>(w => w.Presences_Of_Administrators)
                .WithMany()
                .Map(x =>
                {
                    x.ToTable("Meetings_Presences_Of_Administrators");
                });
            modelBuilder.Entity<Meeting>()
              .HasMany<Trainee>(w => w.Presences_Of_Trainees)
                .WithMany()
                .Map(x =>
                {
                    x.ToTable("Meetings_Presences_Of_Trainees");
                });
            // Presences of Guest
            modelBuilder.Entity<Meeting>()
               .HasMany<Former>(w => w.Presences_Of_Guests_Formers)
                .WithMany()
                .Map(x =>
                {
                    x.ToTable("Meetings_Presences_Of_Guests_Formers");
                });
            modelBuilder.Entity<Meeting>()
               .HasMany<Administrator>(w => w.Presences_Of_Guests_Administrators)
                .WithMany()
                .Map(x =>
                {
                    x.ToTable("Meetings_Presences_Of_Guests_Administrators");
                });
            modelBuilder.Entity<Meeting>()
              .HasMany<Trainee>(w => w.Presences_Of_Guests_Trainees)
                .WithMany()
                .Map(x =>
                {
                    x.ToTable("Meetings_Presences_Of_Guests_Trainees");
                });


            //modelBuilder.Entity<Trainee>().Map(m =>
            //{
            //    m.MapInheritedProperties();
            //    m.ToTable("Trainees ");
            //});

            //modelBuilder.Entity<Former>().Map(m =>
            //{
            //    m.MapInheritedProperties();
            //    m.ToTable("Formers ");
            //});
            //modelBuilder.Entity<Administrator>().Map(m =>
            //{
            //    m.MapInheritedProperties();
            //    m.ToTable("Administrators ");
            //});

            base.OnModelCreating(modelBuilder);
        }

        public static TrainingISModel Create()
        {
            //DbConnection effortConnection = Effort.DbConnectionFactory.CreatePersistent("MyInstanceName");
            //return new TrainingISModel(effortConnection);

            return new TrainingISModel();
        }

        //public TrainingISModel(DbConnection DbConnection) : base(DbConnection,true)
        //{
        //    // @"data source=(LocalDb)\MSSQLLocalDB;initial catalog=TrainingIS;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"
        //}

    }
}