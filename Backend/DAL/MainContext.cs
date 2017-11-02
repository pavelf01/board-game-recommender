using DAL.Entity;
using System.Data.Entity;

namespace DAL
{
    public class MainContext : DbContext
    {
        public MainContext() :
            base("name=DefaultConnection")
        {
        }

        public DbSet<BoardGame> BoardGames { get; set; }
        public DbSet<BoardGameArtist> BoardGameArtists { get; set; }
        public DbSet<BoardGameCategory> BoardGameCategories { get; set; }
        public DbSet<BoardGameDesigner> BoardGameDesigners { get; set; }
        public DbSet<BoardGamePublisher> BoardGamePublishers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRating> UserRatings { get; set; }
    }
}