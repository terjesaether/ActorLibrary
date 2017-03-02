namespace ActorLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Actors", "ActorGenderId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Actors", "ActorGenderId", c => c.String());
        }
    }
}
