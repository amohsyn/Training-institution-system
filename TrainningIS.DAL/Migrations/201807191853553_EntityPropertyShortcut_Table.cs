namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntityPropertyShortcut_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EntityPropertyShortcuts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EntityName = c.String(nullable: false),
                        PropertyName = c.String(nullable: false),
                        PropertyShortcutName = c.String(nullable: false),
                        Description = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EntityPropertyShortcuts");
        }
    }
}
