namespace ActorLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDialectToActor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dialects", "Actor_ActorId", c => c.Int());
            CreateIndex("dbo.Dialects", "Actor_ActorId");
            AddForeignKey("dbo.Dialects", "Actor_ActorId", "dbo.Actors", "ActorId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Dialects", "Actor_ActorId", "dbo.Actors");
            DropIndex("dbo.Dialects", new[] { "Actor_ActorId" });
            DropColumn("dbo.Dialects", "Actor_ActorId");
        }
    }
}
