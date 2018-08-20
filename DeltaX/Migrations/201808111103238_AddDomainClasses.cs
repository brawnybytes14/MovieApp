namespace DeltaX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDomainClasses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Year = c.Int(nullable: false),
                        Plot = c.String(),
                        Poster = c.String(),
                        ProducerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Producers", t => t.ProducerId, cascadeDelete: true)
                .Index(t => t.ProducerId);
            
            CreateTable(
                "dbo.Producers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Sex = c.String(),
                        DOB = c.DateTime(),
                        Bio = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MovieActors",
                c => new
                    {
                        Movie_Id = c.Int(nullable: false),
                        Actor_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Movie_Id, t.Actor_Id })
                .ForeignKey("dbo.Movies", t => t.Movie_Id, cascadeDelete: true)
                .ForeignKey("dbo.Actors", t => t.Actor_Id, cascadeDelete: true)
                .Index(t => t.Movie_Id)
                .Index(t => t.Actor_Id);
            
            AddColumn("dbo.Actors", "Sex", c => c.String());
            AddColumn("dbo.Actors", "DOB", c => c.DateTime());
            AddColumn("dbo.Actors", "Bio", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "ProducerId", "dbo.Producers");
            DropForeignKey("dbo.MovieActors", "Actor_Id", "dbo.Actors");
            DropForeignKey("dbo.MovieActors", "Movie_Id", "dbo.Movies");
            DropIndex("dbo.MovieActors", new[] { "Actor_Id" });
            DropIndex("dbo.MovieActors", new[] { "Movie_Id" });
            DropIndex("dbo.Movies", new[] { "ProducerId" });
            DropColumn("dbo.Actors", "Bio");
            DropColumn("dbo.Actors", "DOB");
            DropColumn("dbo.Actors", "Sex");
            DropTable("dbo.MovieActors");
            DropTable("dbo.Producers");
            DropTable("dbo.Movies");
        }
    }
}
