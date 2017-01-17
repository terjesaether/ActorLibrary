namespace ActorLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDialectNameString : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Dialects", "TheDialectName_DialectListId", "dbo.DialectNames");
            DropIndex("dbo.Dialects", new[] { "TheDialectName_DialectListId" });
            AddColumn("dbo.Dialects", "DialectName", c => c.String());
            DropColumn("dbo.Dialects", "TheDialectName_DialectListId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Dialects", "TheDialectName_DialectListId", c => c.Int());
            DropColumn("dbo.Dialects", "DialectName");
            CreateIndex("dbo.Dialects", "TheDialectName_DialectListId");
            AddForeignKey("dbo.Dialects", "TheDialectName_DialectListId", "dbo.DialectNames", "DialectListId");
        }
    }
}
