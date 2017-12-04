using DAL.Entity;
using MySql.Data.Entity;
using System.Data.Entity;

namespace DAL
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class MainContext : DbContext
    {
        public MainContext() :
            base("name=SSHTunneledConnection")
        {
        }

        public DbSet<BoardGame> BoardGames { get; set; }
        public DbSet<BoardGameArtist> BoardGameArtists { get; set; }
        public DbSet<BoardGameCategory> BoardGameCategories { get; set; }
        public DbSet<BoardGameDesigner> BoardGameDesigners { get; set; }
        public DbSet<BoardGamePublisher> BoardGamePublishers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRating> UserRatings { get; set; }


        public DbSet<BoardGameCategoryValue> BoardGameCategoryValues { get; set; }
    }
}