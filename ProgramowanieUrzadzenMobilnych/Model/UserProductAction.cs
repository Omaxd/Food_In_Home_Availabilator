using Common.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class UserProductAction
    {
        public int UserProductActionId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public double Quantity { get; set; }
        public UserProductActionType ActionType { get; set; }

    }
}
