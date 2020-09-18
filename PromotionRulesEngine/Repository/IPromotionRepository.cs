using PromotionRulesEngine.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionRulesEngine
{
    public interface IPromotionRepository
    {
        List<Product> ApplyPromoToProductA(ref List<Product> cart);

    }
}
