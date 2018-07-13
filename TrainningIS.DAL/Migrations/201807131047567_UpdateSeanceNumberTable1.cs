namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSeanceNumberTable1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SeanceNumbers", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SeanceNumbers", "Description", c => c.String(nullable: false));
        }
    }
}
