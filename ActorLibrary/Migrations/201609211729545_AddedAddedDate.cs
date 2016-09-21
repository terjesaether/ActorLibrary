namespace ActorLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAddedDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Actors", "AddedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Actors", "AddedDate");
        }
    }
}
