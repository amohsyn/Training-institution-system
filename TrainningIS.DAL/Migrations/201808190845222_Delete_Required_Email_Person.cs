namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Delete_Required_Email_Person : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Formers", "IX_Former_Email");
            DropIndex("dbo.Trainees", "IX_Former_Email");
            AlterColumn("dbo.Formers", "Email", c => c.String());
            AlterColumn("dbo.Trainees", "Email", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trainees", "Email", c => c.String(nullable: false, maxLength: 65));
            AlterColumn("dbo.Formers", "Email", c => c.String(nullable: false, maxLength: 65));
            CreateIndex("dbo.Trainees", "Email", unique: true, name: "IX_Former_Email");
            CreateIndex("dbo.Formers", "Email", unique: true, name: "IX_Former_Email");
        }
    }
}
