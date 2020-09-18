using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionRulesEngine.Model
{
    public sealed class Product
    {
        public Guid ProductId { get; set; }

        public string ProductCode { get; set; }

        public int Quantity { get; set; }

        public int Amount { get; set; }

        public int Discount { get; set; }

        public bool IsPromoApplied { get; set; }
    }
}
