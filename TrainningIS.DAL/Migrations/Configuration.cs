namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TrainingIS.DAL.TrainingISModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "TrainingIS.DAL.TrainingISModel";
        }

        protected override void Seed(TrainingIS.DAL.TrainingISModel context)
        {
            // To debug Seed method
            //if (System.Diagnostics.Debugger.IsAttached == false)
            //{
            //    System.Diagnostics.Debugger.Launch();
            //}

            // TrainingTypes
            context.TrainingTypes.AddOrUpdate(
               p => p.Code,
               new Entities.TrainingType { Reference = "cours-jour", Code = "cours-jour", Ordre = 1, Name = "Cours de jour" },
               new Entities.TrainingType { Reference = "cours-soir", Code = "cours-soir", Ordre = 2, Name = "Cours de soire" },
               new Entities.TrainingType { Reference = "formation-qualifiante", Code = "formation-qualifiante", Ordre = 3, Name = "Formation qualifiante" }
             );
            context.SaveChanges();
            var TrainingTypesCoursJour = context.TrainingTypes.Where(t => t.Code == "cours-jour").FirstOrDefault();

            // Spéciality
            context.Specialtys.AddOrUpdate(
               p=>p.Reference,
               new Entities.Specialty { Reference="TDI",Code="TDI" ,Ordre=1,Name= "Techniques de Développement Informatique" },
               new Entities.Specialty { Reference = "TRI", Code = "TRI", Ordre = 2, Name = "Techniques des Réseaux Informatiques" },
               new Entities.Specialty { Reference = "TDM", Code = "TDM", Ordre = 3, Name = "Techniques de Développement Multimédia" }
             );
            context.SaveChanges();
            var SpecialityTDI = context.Specialtys.Where(s => s.Code == "TDI").FirstOrDefault();


            // 
            // Jeux de Test
            //

            // TraineeYear
            context.TrainingYears.AddOrUpdate(
              p => p.Code,
              new Entities.TrainingYear { Code = "2017-2018",Reference= "2017-2018", Ordre=1, StartDate= new DateTime(2017,9,5),EndtDate= new DateTime(2018, 7, 30) }
            );
            context.SaveChanges();
            var TraininYear2018 = context.TrainingYears.Where(t => t.Code == "2017-2018").FirstOrDefault();
           
            // Groups
            context.Groups.AddOrUpdate(
              p => p.Reference,
              new Entities.Group { Reference = "TDI101", TrainingType = TrainingTypesCoursJour, Code = "TDI101", Ordre = 1, Number = "1", Specialty = SpecialityTDI, TrainingYear = TraininYear2018,Year = 1 },
              new Entities.Group { Reference = "TDI202", TrainingType = TrainingTypesCoursJour, Code = "TDI102", Ordre = 2, Number = "2", Specialty = SpecialityTDI, TrainingYear = TraininYear2018, Year = 2 }

            );
            context.SaveChanges();
        }
    }
}
