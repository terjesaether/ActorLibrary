namespace ActorLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCommentField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Actors", "Comment", c => c.String(maxLength: 1024));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Actors", "Comment");
        }
    }
}
