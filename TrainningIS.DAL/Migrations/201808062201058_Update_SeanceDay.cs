namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_SeanceDay : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SeanceNumbers", "Code", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SeanceNumbers", "Code", c => c.String());
        }
    }
}
