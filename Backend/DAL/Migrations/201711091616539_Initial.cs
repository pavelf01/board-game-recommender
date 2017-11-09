namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BoardGameArtists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BoardGames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                        ThumbnailImageURL = c.String(unicode: false),
                        Description = c.String(unicode: false),
                        Published = c.DateTime(nullable: false, precision: 0),
                        MinimalPlayers = c.Int(nullable: false),
                        MaximalPlayers = c.Int(nullable: false),
                        MinimalPlayingTime = c.Int(nullable: false),
                        MaximalPlayingTime = c.Int(nullable: false),
                        MinimalPlayerAge = c.Int(nullable: false),
                        BGGId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BoardGameCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BoardGameDesigners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BoardGamePublishers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRatings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(unicode: false),
                        Rating = c.Single(nullable: false),
                        BoardGame_Id = c.Int(),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BoardGames", t => t.BoardGame_Id)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.BoardGame_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BoardGameBoardGameArtists",
                c => new
                    {
                        BoardGame_Id = c.Int(nullable: false),
                        BoardGameArtist_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BoardGame_Id, t.BoardGameArtist_Id })
                .ForeignKey("dbo.BoardGames", t => t.BoardGame_Id, cascadeDelete: true)
                .ForeignKey("dbo.BoardGameArtists", t => t.BoardGameArtist_Id, cascadeDelete: true)
                .Index(t => t.BoardGame_Id)
                .Index(t => t.BoardGameArtist_Id);
            
            CreateTable(
                "dbo.BoardGameBoardGameCategories",
                c => new
                    {
                        BoardGame_Id = c.Int(nullable: false),
                        BoardGameCategory_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BoardGame_Id, t.BoardGameCategory_Id })
                .ForeignKey("dbo.BoardGames", t => t.BoardGame_Id, cascadeDelete: true)
                .ForeignKey("dbo.BoardGameCategories", t => t.BoardGameCategory_Id, cascadeDelete: true)
                .Index(t => t.BoardGame_Id)
                .Index(t => t.BoardGameCategory_Id);
            
            CreateTable(
                "dbo.BoardGameBoardGameDesigners",
                c => new
                    {
                        BoardGame_Id = c.Int(nullable: false),
                        BoardGameDesigner_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BoardGame_Id, t.BoardGameDesigner_Id })
                .ForeignKey("dbo.BoardGames", t => t.BoardGame_Id, cascadeDelete: true)
                .ForeignKey("dbo.BoardGameDesigners", t => t.BoardGameDesigner_Id, cascadeDelete: true)
                .Index(t => t.BoardGame_Id)
                .Index(t => t.BoardGameDesigner_Id);
            
            CreateTable(
                "dbo.BoardGameBoardGamePublishers",
                c => new
                    {
                        BoardGame_Id = c.Int(nullable: false),
                        BoardGamePublisher_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BoardGame_Id, t.BoardGamePublisher_Id })
                .ForeignKey("dbo.BoardGames", t => t.BoardGame_Id, cascadeDelete: true)
                .ForeignKey("dbo.BoardGamePublishers", t => t.BoardGamePublisher_Id, cascadeDelete: true)
                .Index(t => t.BoardGame_Id)
                .Index(t => t.BoardGamePublisher_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRatings", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserRatings", "BoardGame_Id", "dbo.BoardGames");
            DropForeignKey("dbo.BoardGameBoardGamePublishers", "BoardGamePublisher_Id", "dbo.BoardGamePublishers");
            DropForeignKey("dbo.BoardGameBoardGamePublishers", "BoardGame_Id", "dbo.BoardGames");
            DropForeignKey("dbo.BoardGameBoardGameDesigners", "BoardGameDesigner_Id", "dbo.BoardGameDesigners");
            DropForeignKey("dbo.BoardGameBoardGameDesigners", "BoardGame_Id", "dbo.BoardGames");
            DropForeignKey("dbo.BoardGameBoardGameCategories", "BoardGameCategory_Id", "dbo.BoardGameCategories");
            DropForeignKey("dbo.BoardGameBoardGameCategories", "BoardGame_Id", "dbo.BoardGames");
            DropForeignKey("dbo.BoardGameBoardGameArtists", "BoardGameArtist_Id", "dbo.BoardGameArtists");
            DropForeignKey("dbo.BoardGameBoardGameArtists", "BoardGame_Id", "dbo.BoardGames");
            DropIndex("dbo.BoardGameBoardGamePublishers", new[] { "BoardGamePublisher_Id" });
            DropIndex("dbo.BoardGameBoardGamePublishers", new[] { "BoardGame_Id" });
            DropIndex("dbo.BoardGameBoardGameDesigners", new[] { "BoardGameDesigner_Id" });
            DropIndex("dbo.BoardGameBoardGameDesigners", new[] { "BoardGame_Id" });
            DropIndex("dbo.BoardGameBoardGameCategories", new[] { "BoardGameCategory_Id" });
            DropIndex("dbo.BoardGameBoardGameCategories", new[] { "BoardGame_Id" });
            DropIndex("dbo.BoardGameBoardGameArtists", new[] { "BoardGameArtist_Id" });
            DropIndex("dbo.BoardGameBoardGameArtists", new[] { "BoardGame_Id" });
            DropIndex("dbo.UserRatings", new[] { "User_Id" });
            DropIndex("dbo.UserRatings", new[] { "BoardGame_Id" });
            DropTable("dbo.BoardGameBoardGamePublishers");
            DropTable("dbo.BoardGameBoardGameDesigners");
            DropTable("dbo.BoardGameBoardGameCategories");
            DropTable("dbo.BoardGameBoardGameArtists");
            DropTable("dbo.Users");
            DropTable("dbo.UserRatings");
            DropTable("dbo.BoardGamePublishers");
            DropTable("dbo.BoardGameDesigners");
            DropTable("dbo.BoardGameCategories");
            DropTable("dbo.BoardGames");
            DropTable("dbo.BoardGameArtists");
        }
    }
}
