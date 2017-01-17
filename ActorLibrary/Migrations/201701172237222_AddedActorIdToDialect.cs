namespace ActorLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedActorIdToDialect : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Dialects", "Actor_ActorId", "dbo.Actors");
            DropIndex("dbo.Dialects", new[] { "Actor_ActorId" });
            RenameColumn(table: "dbo.Dialects", name: "Actor_ActorId", newName: "ActorId");
            AlterColumn("dbo.Dialects", "ActorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Dialects", "ActorId");
            AddForeignKey("dbo.Dialects", "ActorId", "dbo.Actors", "ActorId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Dialects", "ActorId", "dbo.Actors");
            DropIndex("dbo.Dialects", new[] { "ActorId" });
            AlterColumn("dbo.Dialects", "ActorId", c => c.Int());
            RenameColumn(table: "dbo.Dialects", name: "ActorId", newName: "Actor_ActorId");
            CreateIndex("dbo.Dialects", "Actor_ActorId");
            AddForeignKey("dbo.Dialects", "Actor_ActorId", "dbo.Actors", "ActorId");
        }
    }
}
