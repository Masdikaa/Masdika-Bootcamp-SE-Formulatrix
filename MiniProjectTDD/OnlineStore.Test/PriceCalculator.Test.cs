namespace OnlineStore.Test;

public class PriceCalculatorTest {
    [SetUp]
    public void Setup() {
    }

    [Test] //* 1. Correct Calculation
    public void CalculateFinalPrice_With20PercentDiscount_ReturnCorrectPrice() {
        var calc = new PriceCalculator();
        var result = calc.CalculateFinalPrice(100_000, 20);
        Assert.That(result, Is.EqualTo(80_000));
    }

    [Test] //* 2. Throw Exception when Discount greater than 100%
    public void CalculateFinalPrice_DiscountGreaterThan100Percent_ThrowException() {
        var calc = new PriceCalculator();
        Assert.Throws<ArgumentException>(() => calc.CalculateFinalPrice(100_000, 120));
    }

    [Test] //* 3. Discount value negative
    public void CalculateFinalPrice_DiscountNegative_ThrowsException() {
        var calc = new PriceCalculator();
        Assert.Throws<ArgumentException>(() => calc.CalculateFinalPrice(100_000, -15));
    }

    [Test] //* Discount brutal
    public void CalculateFinalPrice_ResultLessThan0_Return0() {
        var calc = new PriceCalculator();
        var result = calc.CalculateFinalPrice(50_000, 200);
        Assert.That(result, Is.EqualTo(0));
    }
}