namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Former_Add_Login : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Formers", "Login", c => c.String(nullable: false));
            AddColumn("dbo.Formers", "Password", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Formers", "Password");
            DropColumn("dbo.Formers", "Login");
        }
    }
}
