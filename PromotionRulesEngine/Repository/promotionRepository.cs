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


        /// <summary>
        /// This method calculates the discount for Product B and apply the discount. 
        /// Discount will bw applied, If the discount amount is grater than ProductA discount amount.
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public List<Product> ApplyPromoToProductB(ref List<Product> cart)
        {
            if (cart.Any(x => x.ProductCode.Equals(Constants.ProductCode_B, StringComparison.InvariantCultureIgnoreCase)))
            {
                Product product = cart.Where(x => x.ProductCode.Equals(Constants.ProductCode_B, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                int temp = product.Quantity / 2;
                int tempDiscount;
                if (temp != 0)
                {
                    tempDiscount = temp * 15;
                    return ApplyPromo(ref cart, tempDiscount, Constants.ProductCode_B);
                }
            }

            return cart;
        }

        /// <summary>
        /// No need to apply any logic here because the discount will be applicable if user selects C & D products togather.
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public List<Product> ApplyPromoToProductC(ref List<Product> cart)
        {
            return cart;
        }

        /// <summary>
        /// It calculates combine discount on ProductC and Product D and  applies to the cart.
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public List<Product> ApplyPromoToProductD(ref List<Product> cart)
        {
            if (cart.Any(x => x.ProductCode.Equals(Constants.ProductCode_C, StringComparison.InvariantCultureIgnoreCase))
                && cart.Any(x => x.ProductCode.Equals(Constants.ProductCode_D, StringComparison.InvariantCultureIgnoreCase)))
            {
                Product productC = cart.Where(x => x.ProductCode.Equals(Constants.ProductCode_C, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                Product productD = cart.Where(x => x.ProductCode.Equals(Constants.ProductCode_D, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                if (productC.Quantity > productD.Quantity)
                {
                    return ApplyPromo(ref cart, (productD.Quantity * 5), Constants.ProductCode_D);
                }
                else
                {
                    return ApplyPromo(ref cart, (productC.Quantity * 5), Constants.ProductCode_D);
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
