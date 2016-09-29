namespace ActorLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedDialects : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Dialects", name: "DialectName_DialectListId", newName: "TheDialectName_DialectListId");
            RenameIndex(table: "dbo.Dialects", name: "IX_DialectName_DialectListId", newName: "IX_TheDialectName_DialectListId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Dialects", name: "IX_TheDialectName_DialectListId", newName: "IX_DialectName_DialectListId");
            RenameColumn(table: "dbo.Dialects", name: "TheDialectName_DialectListId", newName: "DialectName_DialectListId");
        }
    }
}
