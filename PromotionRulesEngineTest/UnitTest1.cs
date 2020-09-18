using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromotionRulesEngine;
using PromotionRulesEngine.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PromotionRulesEngineTest
{
    [TestClass]
    public class UnitTest1
    {
        PromotionRepository promotionRepository = new PromotionRepository();
        Product product = new Product();
        //expected discount Amount
        int expectedResult;


        /// <summary>
        /// no discount/promo will be applicable in this scenario
        /// </summary>
        [TestMethod]
        public void Scenario1()
        {
            List<Product> Cart = new List<Product>();
            product = new Product() { ProductCode = "A", Quantity = 1, Amount = 50 };
            Cart.Add(product);
            product = new Product() { ProductCode = "b", Quantity = 1, Amount = 30 };
            Cart.Add(product);
            product = new Product() { ProductCode = "c", Quantity = 1, Amount = 20 };
            Cart.Add(product);

            Cart = promotionRepository.ApplyPromoToProductA(ref Cart);
            Cart = promotionRepository.ApplyPromoToProductB(ref Cart);
            Cart = promotionRepository.ApplyPromoToProductC(ref Cart);
            Cart = promotionRepository.ApplyPromoToProductD(ref Cart);

            expectedResult = 0;
            int result = 0;
            if (Cart.Any(x => x.Discount > 0))
            {
                result = Cart.Where(x => x.IsPromoApplied).FirstOrDefault().Discount;
            }

            Assert.AreEqual(expectedResult, result);
        }


        [TestMethod]
        public void Scenario2()
        {
            List<Product> Cart = new List<Product>();
            product = new Product() { ProductCode = "A", Quantity = 5, Amount = 250 };// promo will give 20 discount
            Cart.Add(product);
            product = new Product() { ProductCode = "b", Quantity = 5, Amount = 150 }; // promo will give 30 discount
            Cart.Add(product);
            product = new Product() { ProductCode = "c", Quantity = 1, Amount = 20 };
            Cart.Add(product);
            
            Cart = promotionRepository.ApplyPromoToProductA(ref Cart);
            Cart = promotionRepository.ApplyPromoToProductB(ref Cart);
            Cart = promotionRepository.ApplyPromoToProductC(ref Cart);
            Cart = promotionRepository.ApplyPromoToProductD(ref Cart);

            expectedResult = 30;
            int result = 0;
            if (Cart.Any(x => x.Discount > 0))
            {
                result = Cart.Where(x => x.IsPromoApplied).FirstOrDefault().Discount;
            }

            Assert.AreEqual(expectedResult, result);
        }


        [TestMethod]
        public void Scenario3() {
        
        }

        [TestMethod]
        public void Scenario4() { }
    }
}
