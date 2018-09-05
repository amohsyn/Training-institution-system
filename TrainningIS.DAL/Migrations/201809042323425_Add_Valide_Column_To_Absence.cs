namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Valide_Column_To_Absence : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Absences", "Valide", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Absences", "Valide");
        }
    }
}
