namespace ActorLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDialectNameModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dialects", "TheDialectName_DialectListId", c => c.Int());
            CreateIndex("dbo.Dialects", "TheDialectName_DialectListId");
            AddForeignKey("dbo.Dialects", "TheDialectName_DialectListId", "dbo.DialectNames", "DialectListId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Dialects", "TheDialectName_DialectListId", "dbo.DialectNames");
            DropIndex("dbo.Dialects", new[] { "TheDialectName_DialectListId" });
            DropColumn("dbo.Dialects", "TheDialectName_DialectListId");
        }
    }
}
