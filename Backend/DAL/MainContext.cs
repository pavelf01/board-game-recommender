using DAL.Entity;
using System.Data.Entity;

namespace DAL
{
    class MainContext : DbContext
    {
        public MainContext()
            : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<BoardGame> BoardGames { get; set; }
        public virtual DbSet<BoardGameArtist> BoardGameArtists { get; set; }
        public virtual DbSet<BoardGameGategory> BoardGameCategories { get; set; }
        public virtual DbSet<BoardGameDeisgner> BoardGameDesigners { get; set; }
        public virtual DbSet<BoardGamePublisher> BoardGamePublishers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRating> UserRatings { get; set; }
    }
}