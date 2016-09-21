namespace ActorLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBirthDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Actors", "BirthDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Actors", "BirthDate");
        }
    }
}
