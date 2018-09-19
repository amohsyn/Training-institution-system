namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ValidationFormer_To_SeanceTrainings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SeanceTrainings", "FormerValidation", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SeanceTrainings", "FormerValidation");
        }
    }
}
