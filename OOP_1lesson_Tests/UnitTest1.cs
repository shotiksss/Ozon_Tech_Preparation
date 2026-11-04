using Xunit;
using FirstLesson;

namespace FirstLesson_Tests
{
    public class CourierTests
    {
        [Fact]
        public void EarnMoney_ShouldIncreaseCourierBalance()
        {
            var courier = new CurierProfile();
            decimal firstDelivery = 350;
            decimal secondDelivery = 150;

            courier.EarnMoney(firstDelivery);
            courier.EarnMoney(secondDelivery);

            // Assert (Проверяем результат: 350 + 150 должно быть ровно 500)
            Assert.Equal(500, courier.Balance);
        }
    }
}