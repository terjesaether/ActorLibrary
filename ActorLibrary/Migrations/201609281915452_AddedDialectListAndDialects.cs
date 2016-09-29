namespace ActorLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDialectListAndDialects : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DialectNames",
                c => new
                    {
                        DialectListId = c.Int(nullable: false, identity: true),
                        DialectListName = c.String(),
                    })
                .PrimaryKey(t => t.DialectListId);
            
            AddColumn("dbo.Dialects", "DialectName_DialectListId", c => c.Int());
            CreateIndex("dbo.Dialects", "DialectName_DialectListId");
            AddForeignKey("dbo.Dialects", "DialectName_DialectListId", "dbo.DialectNames", "DialectListId");
            DropColumn("dbo.Dialects", "DialectName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Dialects", "DialectName", c => c.String());
            DropForeignKey("dbo.Dialects", "DialectName_DialectListId", "dbo.DialectNames");
            DropIndex("dbo.Dialects", new[] { "DialectName_DialectListId" });
            DropColumn("dbo.Dialects", "DialectName_DialectListId");
            DropTable("dbo.DialectNames");
        }
    }
}
