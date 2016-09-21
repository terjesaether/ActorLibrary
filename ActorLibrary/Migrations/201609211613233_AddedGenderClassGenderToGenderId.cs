namespace ActorLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedGenderClassGenderToGenderId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Actors", "ActorGenderId", c => c.String());
            DropColumn("dbo.Actors", "ActorGenderName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Actors", "ActorGenderName", c => c.String());
            DropColumn("dbo.Actors", "ActorGenderId");
        }
    }
}
