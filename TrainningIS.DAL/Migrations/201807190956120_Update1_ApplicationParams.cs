namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1_ApplicationParams : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationParams", "Value", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationParams", "Value");
        }
    }
}
