using PromotionRulesEngine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using PromotionRulesEngine;

namespace PromotionRulesEngine
{

    /// <summary>
    /// This class implements the IPromotionRepository interface 
    /// It will have one method for each promo code discount caalculation.
    /// System will apply only one promo code based on the discount ammount (higher discount amount will be considered).
    /// </summary>
    public class PromotionRepository : IPromotionRepository
    {

        /// <summary>
        /// This method calculates the descount for 'Product A' based on the selected Quantity.
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public List<Product> ApplyPromoToProductA(ref List<Product> cart)
        {
            if (cart.Any(x => x.ProductCode.Equals(Constants.ProductCode_A, StringComparison.InvariantCultureIgnoreCase)))
            {
                Product product = cart.Where(x => x.ProductCode.Equals(Constants.ProductCode_A, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

                int temp = product.Quantity / 3;
                int tempDiscount;
                if (temp != 0)
                {
                    tempDiscount = temp * 20;
                    return ApplyPromo(ref cart, tempDiscount, Constants.ProductCode_A);
                }
            }

            return cart;
        }


        private List<Product> ApplyPromo(ref List<Product> Cart, int DiscountAmount, string ProductCode)
        {
            if (Cart.All(x => x.Discount <= 0) || !Cart.Any(x => x.Discount >= DiscountAmount))
            {
                foreach (var item in Cart.Where(x => x.Discount > 0 || x.ProductCode.Equals(ProductCode, StringComparison.InvariantCultureIgnoreCase)).ToList())
                {
                    if (item.Discount > 0)
                    {
                        item.Discount = 0;
                        item.IsPromoApplied = false;
                    }
                    else
                    {
                        item.Discount = DiscountAmount;
                        item.IsPromoApplied = true;
                    }

                }
            }
            return Cart;

        }

    }
}
