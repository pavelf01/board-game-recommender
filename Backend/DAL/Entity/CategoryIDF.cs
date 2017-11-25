using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class CategoryIDF
    {
        public int CategoryId { get; set; }
        public double Value { get; set; }

        public CategoryIDF(int categoryId, double value)
        {
            CategoryId = categoryId;
            Value = value;
        }
    }
}
