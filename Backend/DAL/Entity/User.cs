using Riganti.Utils.Infrastructure.Core;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entity
{
    public class User : IEntity<int>
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}
