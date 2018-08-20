namespace DeltaX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MovieActors", "Actor_Id", "dbo.Actors");
            DropForeignKey("dbo.MovieActors", "Movie_Id", "dbo.Movies");
            DropIndex("dbo.MovieActors", new[] { "Actor_Id" });
            DropIndex("dbo.MovieActors", new[] { "Movie_Id" });
            DropPrimaryKey("dbo.MovieActors");
            AlterColumn("dbo.MovieActors", "Actor_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.MovieActors", "Movie_Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.MovieActors", new[] { "Movie_Id", "Actor_Id" });
            CreateIndex("dbo.MovieActors", "Movie_Id");
            CreateIndex("dbo.MovieActors", "Actor_Id");
            AddForeignKey("dbo.MovieActors", "Actor_Id", "dbo.Actors", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MovieActors", "Movie_Id", "dbo.Movies", "Id", cascadeDelete: true);
            DropColumn("dbo.MovieActors", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MovieActors", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.MovieActors", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.MovieActors", "Actor_Id", "dbo.Actors");
            DropIndex("dbo.MovieActors", new[] { "Actor_Id" });
            DropIndex("dbo.MovieActors", new[] { "Movie_Id" });
            DropPrimaryKey("dbo.MovieActors");
            AlterColumn("dbo.MovieActors", "Movie_Id", c => c.Int());
            AlterColumn("dbo.MovieActors", "Actor_Id", c => c.Int());
            AddPrimaryKey("dbo.MovieActors", "Id");
            CreateIndex("dbo.MovieActors", "Movie_Id");
            CreateIndex("dbo.MovieActors", "Actor_Id");
            AddForeignKey("dbo.MovieActors", "Movie_Id", "dbo.Movies", "Id");
            AddForeignKey("dbo.MovieActors", "Actor_Id", "dbo.Actors", "Id");
        }
    }
}
