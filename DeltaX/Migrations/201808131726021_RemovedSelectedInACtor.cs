namespace DeltaX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedSelectedInACtor : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Actors", "Selected");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Actors", "Selected", c => c.Boolean(nullable: false));
        }
    }
}
