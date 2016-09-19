namespace ActorLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedGenderString : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Actors", "Gender_GenderId", "dbo.Genders");
            DropIndex("dbo.Actors", new[] { "Gender_GenderId" });
            AddColumn("dbo.Actors", "Gender", c => c.String());
            DropColumn("dbo.Actors", "Gender_GenderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Actors", "Gender_GenderId", c => c.Int());
            DropColumn("dbo.Actors", "Gender");
            CreateIndex("dbo.Actors", "Gender_GenderId");
            AddForeignKey("dbo.Actors", "Gender_GenderId", "dbo.Genders", "GenderId");
        }
    }
}
