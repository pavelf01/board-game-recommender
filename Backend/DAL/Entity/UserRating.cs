using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entity
{
    public class UserRating : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Comment { get; set; }
        [Required]
        public float Rating { get; set; }
        [Required]
        public User User { get; set; }
        [InverseProperty("Ratings")]
        public BoardGame BoardGame { get; set; }
    }
}
