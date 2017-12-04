using System.ComponentModel.DataAnnotations;

namespace DAL.Entity
{
    public class BoardGameCategoryValue : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int GameId { get; set; }
        public int CategoryId { get; set; }
        public double Value { get; set; }

        public BoardGameCategoryValue(int gameId, int categoryId, double value) : this()
        {
            GameId = gameId;
            CategoryId = categoryId;
            Value = value;
        }

        public BoardGameCategoryValue()
        {

        }
    }
}
