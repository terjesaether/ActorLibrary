namespace ActorLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedGenderClassFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Actors", "ActorGenderName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Actors", "ActorGenderName");
        }
    }
}
