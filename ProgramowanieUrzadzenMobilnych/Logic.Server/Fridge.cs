using Common.Constants;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic.Server
{
    public class Fridge
    {
        IList<ProductQuantity> _productQuantities { get; set; }

        public Fridge()
        {
            _productQuantities = new List<ProductQuantity>();
        }

        public bool AddProduct(ProductQuantity productQuantity, int userId)
        {
            ProductQuantity selectedProduct = _productQuantities
                .FirstOrDefault(p => p.ProductId == productQuantity.ProductId);

            selectedProduct.Quantity += productQuantity.Quantity;

            UserProductAction userProductAction = new UserProductAction()
            {
                ProductId = productQuantity.ProductId,
                Quantity = productQuantity.Quantity,
                UserId = userId,
                ActionType = UserProductActionType.Add
            };

            return true;
        }

        public bool RemoveProduct(ProductQuantity productQuantity, int userId)
        {
            ProductQuantity selectedProduct = _productQuantities
                .FirstOrDefault(p => p.ProductId == productQuantity.ProductId);

            // Check if fridge has product
            if (selectedProduct.Quantity < productQuantity.Quantity)
                return false;

            selectedProduct.Quantity -= productQuantity.Quantity;

            UserProductAction userProductAction = new UserProductAction()
            {
                ProductId = productQuantity.ProductId,
                Quantity = productQuantity.Quantity,
                UserId = userId,
                ActionType = UserProductActionType.Remove
            };

            return true;
        }

        #region Period methods
        public void CreateDailyStatistics()
        {

        }
        #endregion
    }
}
