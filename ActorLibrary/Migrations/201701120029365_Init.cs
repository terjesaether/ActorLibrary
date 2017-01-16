namespace ActorLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dialects",
                c => new
                    {
                        DialectId = c.Int(nullable: false, identity: true),
                        DialectListId = c.Int(nullable: false),
                        Actor_ActorId = c.Int(),
                    })
                .PrimaryKey(t => t.DialectId)
                .ForeignKey("dbo.Actors", t => t.Actor_ActorId)
                .Index(t => t.Actor_ActorId);
            
            CreateTable(
                "dbo.Actors",
                c => new
                    {
                        ActorId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        Mail = c.String(),
                        Phone = c.String(),
                        Address = c.String(),
                        ImgUrl = c.String(),
                        Comment = c.String(maxLength: 1024),
                        ActorGenderId = c.String(),
                        AddedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ActorId);
            
            CreateTable(
                "dbo.VoiceTests",
                c => new
                    {
                        VoiceTestId = c.Int(nullable: false, identity: true),
                        VoiceTestTitle = c.String(),
                        Comment = c.String(),
                        VoiceTestUrl = c.String(),
                        Actor_ActorId = c.Int(),
                    })
                .PrimaryKey(t => t.VoiceTestId)
                .ForeignKey("dbo.Actors", t => t.Actor_ActorId)
                .Index(t => t.Actor_ActorId);
            
            CreateTable(
                "dbo.DialectNames",
                c => new
                    {
                        DialectListId = c.Int(nullable: false, identity: true),
                        DialectListName = c.String(),
                    })
                .PrimaryKey(t => t.DialectListId);
            
            CreateTable(
                "dbo.Genders",
                c => new
                    {
                        GenderId = c.Int(nullable: false, identity: true),
                        GenderName = c.String(),
                    })
                .PrimaryKey(t => t.GenderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VoiceTests", "Actor_ActorId", "dbo.Actors");
            DropForeignKey("dbo.Dialects", "Actor_ActorId", "dbo.Actors");
            DropIndex("dbo.VoiceTests", new[] { "Actor_ActorId" });
            DropIndex("dbo.Dialects", new[] { "Actor_ActorId" });
            DropTable("dbo.Genders");
            DropTable("dbo.DialectNames");
            DropTable("dbo.VoiceTests");
            DropTable("dbo.Actors");
            DropTable("dbo.Dialects");
        }
    }
}
