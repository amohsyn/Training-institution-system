namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Projects_Tables : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Projects", "Code");
            DropColumn("dbo.TaskProjects", "Code");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TaskProjects", "Code", c => c.String(nullable: false));
            AddColumn("dbo.Projects", "Code", c => c.String(nullable: false));
        }
    }
}
