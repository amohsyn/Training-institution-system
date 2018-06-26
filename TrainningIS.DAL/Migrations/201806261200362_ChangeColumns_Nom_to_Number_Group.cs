namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeColumns_Nom_to_Number_Group : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Groups", "Number", c => c.String());
            DropColumn("dbo.Groups", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Groups", "Name", c => c.String());
            DropColumn("dbo.Groups", "Number");
        }
    }
}
