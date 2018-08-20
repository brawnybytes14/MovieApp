namespace DeltaX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLengthValidationCorrection : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Actors", "Bio", c => c.String(maxLength: 300));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Actors", "Bio", c => c.String(maxLength: 50));
        }
    }
}
