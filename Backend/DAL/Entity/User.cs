using System.ComponentModel.DataAnnotations;

namespace DAL.Entity
{
    public class User : BaseEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}
