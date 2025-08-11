public class PriceCalculator {
    public decimal CalculateFinalPrice(decimal price, int discount) {
        if (discount < 0)
            throw new ArgumentException("Discount percent cannot be negative.");

        var discountAmount = (price * discount) / 100;
        var finalPrice = price - discountAmount;

        return finalPrice < 0 ? 0 : finalPrice;
    }
}