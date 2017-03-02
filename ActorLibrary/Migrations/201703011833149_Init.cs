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
                        DialectName = c.String(),
                        ActorId = c.Int(nullable: false),
                        DialectListId = c.String(),
                        TheDialectName_DialectListId = c.Int(),
                    })
                .PrimaryKey(t => t.DialectId)
                .ForeignKey("dbo.DialectNames", t => t.TheDialectName_DialectListId)
                .ForeignKey("dbo.Actors", t => t.ActorId, cascadeDelete: true)
                .Index(t => t.ActorId)
                .Index(t => t.TheDialectName_DialectListId);
            
            CreateTable(
                "dbo.DialectNames",
                c => new
                    {
                        DialectListId = c.Int(nullable: false, identity: true),
                        DialectListName = c.String(),
                    })
                .PrimaryKey(t => t.DialectListId);
            
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
                        ActorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VoiceTestId)
                .ForeignKey("dbo.Actors", t => t.ActorId, cascadeDelete: true)
                .Index(t => t.ActorId);
            
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
            DropForeignKey("dbo.VoiceTests", "ActorId", "dbo.Actors");
            DropForeignKey("dbo.Dialects", "ActorId", "dbo.Actors");
            DropForeignKey("dbo.Dialects", "TheDialectName_DialectListId", "dbo.DialectNames");
            DropIndex("dbo.VoiceTests", new[] { "ActorId" });
            DropIndex("dbo.Dialects", new[] { "TheDialectName_DialectListId" });
            DropIndex("dbo.Dialects", new[] { "ActorId" });
            DropTable("dbo.Genders");
            DropTable("dbo.VoiceTests");
            DropTable("dbo.Actors");
            DropTable("dbo.DialectNames");
            DropTable("dbo.Dialects");
        }
    }
}
