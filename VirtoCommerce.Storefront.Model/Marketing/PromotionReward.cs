using System;
using VirtoCommerce.Storefront.Model.Common;

namespace VirtoCommerce.Storefront.Model.Marketing
{
    /// <summary>
    /// Represents promotion reward object
    /// </summary>
    public partial class PromotionReward
    {
        /// <summary>
        /// Gets or sets promotion reward amount
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets promotion reward amount type (absolute or relative)
        /// </summary>
        public AmountType AmountType { get; set; }

        /// <summary>
        /// Gets or sets category ID promotion reward
        /// </summary>
        public string CategoryId { get; set; }

        /// <summary>
        /// Gets or sets promotion reward coupon code
        /// </summary>
        public string Coupon { get; set; }

        /// <summary>
        /// Gets or sets promotion reward coupon amount
        /// </summary>
        public Money CouponAmount { get; set; }

        /// <summary>
        /// Gets or sets promotion reward minimum order amount for applying coupon
        /// </summary>
        public Money CouponMinOrderAmount { get; set; }

        /// <summary>
        /// Gets or sets the description of promotion reward
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the image URL of promotion reward
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the sign that promotion reward is valid (for dynamic discount) or not (for potential discount)
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// Gets or sets line item ID for which promotion reward was applied
        /// </summary>
        public string LineItemId { get; set; }

        /// <summary>
        /// Gets or sets the measurement unit for promotion reward
        /// </summary>
        public string MeasureUnit { get; set; }

        /// <summary>
        /// Gets or sets product ID for which promotion reward was applied
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// Gets or sets the promotion for reward
        /// </summary>
        public Promotion Promotion { get; set; }

        /// <summary>
        /// Gets or sets promotion ID for reward
        /// </summary>
        public string PromotionId { get; set; }

        /// <summary>
        /// Gets or set the quantity of items
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets promotion reward type
        /// </summary>
        public PromotionRewardType RewardType { get; set; }

        /// <summary>
        /// Gets or sets the shipping method code for the marketing reward
        /// </summary>
        public string ShippingMethodCode { get; set; }

        /// <summary>
        /// Gets or sets the payment method code for the marketing reward
        /// </summary>
        public string PaymentMethodCode { get; set; }

        /// <summary>
        /// Gets or sets the max limit for relative rewards
        /// </summary>
        public decimal MaxLimit { get; set; }


        public Discount ToDiscountModel(Money amount)
        {
            var absoluteAmount = GetAbsoluteDiscountAmount(amount.Amount);
            var discount = new Discount(amount.Currency)
            {
                Amount = new Money(absoluteAmount, amount.Currency),
                Description = Promotion.Description,
                Coupon = Coupon,
                PromotionId = Promotion.Id
            };

            return discount;
        }

        private decimal GetAbsoluteDiscountAmount(decimal originAmount)
        {
            var absoluteAmount = Amount;
            if (AmountType == AmountType.Relative)
            {
                absoluteAmount = Amount * originAmount / 100;
                if (MaxLimit > 0)
                {
                    absoluteAmount = Math.Min(MaxLimit, absoluteAmount);
                }
            }
            else
            {
                //Do not allow to create discount greater than origin amount
                absoluteAmount = Math.Min(originAmount, absoluteAmount);
            }

            return absoluteAmount;
        }
    }
}
