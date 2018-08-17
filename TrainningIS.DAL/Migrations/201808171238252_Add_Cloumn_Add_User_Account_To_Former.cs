namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Cloumn_Add_User_Account_To_Former : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Formers", "CreateUserAccount", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Formers", "CreateUserAccount");
        }
    }
}
