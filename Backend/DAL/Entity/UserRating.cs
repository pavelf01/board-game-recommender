using Riganti.Utils.Infrastructure.Core;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entity
{
    public class UserRating : IEntity<int>
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public User User { get; set; }
    }
}
