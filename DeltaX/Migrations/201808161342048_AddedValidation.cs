namespace DeltaX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Actors", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Movies", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Movies", "Plot", c => c.String(nullable: false));
            AlterColumn("dbo.Producers", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Producers", "Name", c => c.String());
            AlterColumn("dbo.Movies", "Plot", c => c.String());
            AlterColumn("dbo.Movies", "Name", c => c.String());
            AlterColumn("dbo.Actors", "Name", c => c.String());
        }
    }
}
