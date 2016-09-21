namespace ActorLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAddedDateAddedConstr : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Actors", "AddedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Actors", "AddedDate", c => c.DateTime(nullable: false));
        }
    }
}
