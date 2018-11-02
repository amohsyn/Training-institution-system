namespace TrainingIS.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change_Sanction_Absence_Table : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SanctionAbsences", "Sanction_Id", "dbo.Sanctions");
            DropForeignKey("dbo.SanctionAbsences", "Absence_Id", "dbo.Absences");
            DropIndex("dbo.SanctionAbsences", new[] { "Sanction_Id" });
            DropIndex("dbo.SanctionAbsences", new[] { "Absence_Id" });
            AddColumn("dbo.Absences", "Sanction_Id", c => c.Long());
            CreateIndex("dbo.Absences", "Sanction_Id");
            AddForeignKey("dbo.Absences", "Sanction_Id", "dbo.Sanctions", "Id");
            DropTable("dbo.SanctionAbsences");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SanctionAbsences",
                c => new
                    {
                        Sanction_Id = c.Long(nullable: false),
                        Absence_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Sanction_Id, t.Absence_Id });
            
            DropForeignKey("dbo.Absences", "Sanction_Id", "dbo.Sanctions");
            DropIndex("dbo.Absences", new[] { "Sanction_Id" });
            DropColumn("dbo.Absences", "Sanction_Id");
            CreateIndex("dbo.SanctionAbsences", "Absence_Id");
            CreateIndex("dbo.SanctionAbsences", "Sanction_Id");
            AddForeignKey("dbo.SanctionAbsences", "Absence_Id", "dbo.Absences", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SanctionAbsences", "Sanction_Id", "dbo.Sanctions", "Id", cascadeDelete: true);
        }
    }
}
