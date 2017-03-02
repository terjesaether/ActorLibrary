namespace ActorLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedDialectListIdString : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Dialects", "TheDialectName_DialectListId", "dbo.DialectNames");
            DropIndex("dbo.Dialects", new[] { "TheDialectName_DialectListId" });
            DropColumn("dbo.Dialects", "DialectListId");
            RenameColumn(table: "dbo.Dialects", name: "TheDialectName_DialectListId", newName: "DialectListId");
            AlterColumn("dbo.Dialects", "DialectListId", c => c.Int(nullable: false));
            AlterColumn("dbo.Dialects", "DialectListId", c => c.Int(nullable: false));
            CreateIndex("dbo.Dialects", "DialectListId");
            AddForeignKey("dbo.Dialects", "DialectListId", "dbo.DialectNames", "DialectListId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Dialects", "DialectListId", "dbo.DialectNames");
            DropIndex("dbo.Dialects", new[] { "DialectListId" });
            AlterColumn("dbo.Dialects", "DialectListId", c => c.Int());
            AlterColumn("dbo.Dialects", "DialectListId", c => c.String());
            RenameColumn(table: "dbo.Dialects", name: "DialectListId", newName: "TheDialectName_DialectListId");
            AddColumn("dbo.Dialects", "DialectListId", c => c.String());
            CreateIndex("dbo.Dialects", "TheDialectName_DialectListId");
            AddForeignKey("dbo.Dialects", "TheDialectName_DialectListId", "dbo.DialectNames", "DialectListId");
        }
    }
}
