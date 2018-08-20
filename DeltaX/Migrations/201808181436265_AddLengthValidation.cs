namespace DeltaX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLengthValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Actors", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Actors", "Bio", c => c.String(maxLength: 50));
            AlterColumn("dbo.Movies", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Movies", "Plot", c => c.String(nullable: false, maxLength: 300));
            AlterColumn("dbo.Producers", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Producers", "Bio", c => c.String(maxLength: 300));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Producers", "Bio", c => c.String());
            AlterColumn("dbo.Producers", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Movies", "Plot", c => c.String(nullable: false));
            AlterColumn("dbo.Movies", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Actors", "Bio", c => c.String());
            AlterColumn("dbo.Actors", "Name", c => c.String(nullable: false));
        }
    }
}
