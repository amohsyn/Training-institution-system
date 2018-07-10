namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeModuleTableName : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Modules", newName: "ModuleTrainings");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ModuleTrainings", newName: "Modules");
        }
    }
}
