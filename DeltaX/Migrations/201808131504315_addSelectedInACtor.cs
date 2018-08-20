namespace DeltaX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSelectedInACtor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Actors", "Selected", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Actors", "Selected");
        }
    }
}
