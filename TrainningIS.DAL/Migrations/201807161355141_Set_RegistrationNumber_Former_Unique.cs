namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Set_RegistrationNumber_Former_Unique : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Formers", "RegistrationNumber", c => c.String(nullable: false, maxLength: 65));
            CreateIndex("dbo.Formers", "RegistrationNumber", unique: true, name: "IX_Former_RegistrationNumber");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Formers", "IX_Former_RegistrationNumber");
            AlterColumn("dbo.Formers", "RegistrationNumber", c => c.String(nullable: false));
        }
    }
}
