using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ProductQuantity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public double Quantity { get; set; }
    }
}
