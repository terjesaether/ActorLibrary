namespace ActorLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDialect : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dialects",
                c => new
                    {
                        DialectId = c.Int(nullable: false, identity: true),
                        DialectName = c.String(),
                    })
                .PrimaryKey(t => t.DialectId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Dialects");
        }
    }
}
