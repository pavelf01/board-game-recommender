using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class BoardGameCategoryValue
    {
        public int GameId { get; set; }
        public int CategoryId { get; set; }
        public double Value { get; set; }

        public BoardGameCategoryValue(int gameId, int categoryId, double value)
        {
            GameId = gameId;
            CategoryId = categoryId;
            Value = value;
        }
    }
}
