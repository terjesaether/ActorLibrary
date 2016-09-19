namespace ActorLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedGenderClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genders",
                c => new
                    {
                        GenderId = c.Int(nullable: false, identity: true),
                        GenderName = c.String(),
                    })
                .PrimaryKey(t => t.GenderId);
            
            AddColumn("dbo.Actors", "Gender_GenderId", c => c.Int());
            CreateIndex("dbo.Actors", "Gender_GenderId");
            AddForeignKey("dbo.Actors", "Gender_GenderId", "dbo.Genders", "GenderId");
            DropColumn("dbo.Actors", "Gender");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Actors", "Gender", c => c.Int(nullable: false));
            DropForeignKey("dbo.Actors", "Gender_GenderId", "dbo.Genders");
            DropIndex("dbo.Actors", new[] { "Gender_GenderId" });
            DropColumn("dbo.Actors", "Gender_GenderId");
            DropTable("dbo.Genders");
        }
    }
}
