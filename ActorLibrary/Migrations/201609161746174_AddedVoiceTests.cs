namespace ActorLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedVoiceTests : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VoiceTests",
                c => new
                    {
                        VoiceTestId = c.Int(nullable: false, identity: true),
                        VoiceTestTitle = c.String(),
                        VoiceTestUrl = c.String(),
                        Actor_ActorId = c.Int(),
                    })
                .PrimaryKey(t => t.VoiceTestId)
                .ForeignKey("dbo.Actors", t => t.Actor_ActorId)
                .Index(t => t.Actor_ActorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VoiceTests", "Actor_ActorId", "dbo.Actors");
            DropIndex("dbo.VoiceTests", new[] { "Actor_ActorId" });
            DropTable("dbo.VoiceTests");
        }
    }
}
