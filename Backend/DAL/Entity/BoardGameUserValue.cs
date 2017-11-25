using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class BoardGameUserValue
    {
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public double Value { get; set; }

        public BoardGameUserValue(int userId, int categoryId, double value)
        {
            UserId = userId;
            CategoryId = categoryId;
            Value = value;
        }
    }
}
