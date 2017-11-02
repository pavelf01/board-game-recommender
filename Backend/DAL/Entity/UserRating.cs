using System.ComponentModel.DataAnnotations;

namespace DAL.Entity
{
    public class UserRating : BaseEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public string Comment { get; set; }
        [Required]
        public float Rating { get; set; }
        [Required]
        public User User { get; set; }
    }
}
