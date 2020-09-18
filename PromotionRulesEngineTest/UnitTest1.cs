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
        Product expectedResult = new Product();

        [TestMethod]
        public void Scenario1()
        {
            product = new Product()
            {
                ProductCode = "A",
                Quantity = 5,
                Amount = 250
            };

            List<Product> Cart = new List<Product>();
            Cart.Add(product);

            expectedResult = new Product()
            {
                ProductCode = product.ProductCode,
                Quantity = product.Quantity,
                Amount = product.Amount,
                IsPromoApplied = true,
                Discount = 20
            };

            Cart = promotionRepository.ApplyPromoToProductA(ref Cart);

            Product result = Cart.Where(x => x.IsPromoApplied).FirstOrDefault();

            Assert.ReferenceEquals(result, expectedResult);

        }
    }
}
