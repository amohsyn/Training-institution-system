namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<TrainingIS.DAL.TrainingISModel>
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

        }
    }
}
