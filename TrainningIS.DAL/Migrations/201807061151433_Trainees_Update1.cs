namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Trainees_Update1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Trainees", "DateInscription", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trainees", "DateInscription", c => c.DateTime(nullable: false));
        }
    }
}
