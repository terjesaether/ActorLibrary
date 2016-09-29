namespace ActorLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAgeRangesAgein : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AgeRanges",
                c => new
                    {
                        AgeRangeId = c.Int(nullable: false, identity: true),
                        AgeRange = c.String(),
                    })
                .PrimaryKey(t => t.AgeRangeId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AgeRanges");
        }
    }
}
