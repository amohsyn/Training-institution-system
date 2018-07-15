namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Former_Unique_Email : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Formers", "Email", c => c.String(nullable: false, maxLength: 65));
            CreateIndex("dbo.Formers", "Email", unique: true, name: "IX_Former_Email");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Formers", "IX_Former_Email");
            AlterColumn("dbo.Formers", "Email", c => c.String());
        }
    }
}
