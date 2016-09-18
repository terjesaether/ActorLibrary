namespace ActorLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actors",
                c => new
                    {
                        ActorId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Mail = c.String(),
                        Phone = c.String(),
                        Address = c.String(),
                        ImgUrl = c.String(),
                    })
                .PrimaryKey(t => t.ActorId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Actors");
        }
    }
}
