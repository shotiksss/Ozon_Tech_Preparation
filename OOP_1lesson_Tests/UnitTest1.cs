using Xunit;
using FirstLesson;

namespace FirstLesson_Tests
{
    public class CourierTests
    {
        [Fact]
        public void EarnMoney_ShouldIncreaseCourierBalance()
        {
            var courier = new CourierProfile();
            decimal firstDelivery = 350;
            decimal secondDelivery = 150;

            courier.EarnMoney(firstDelivery);
            courier.EarnMoney(secondDelivery);

            Assert.Equal(500, courier.Balance);
        }

        [Fact]
        public void AddToCart_ShouldAddItemToCartList()
        {
            // 1. Arrange (Подготовка)
            User testUser = new User { Name = "Тестовый Покупатель" };
            testUser.Customer = new CustomerProfile { DeliveryAddress = "Тестовый адрес" };

            // 2. Act (Действие)
            testUser.Customer.AddToCart("Смартфон Ozon");

            // 3. Assert (Проверка)
            // Проверяем, что в корзине теперь ровно 1 элемент
            Assert.Single(testUser.Customer.Cart);

            // Проверяем, что этот элемент — именно тот самый смартфон
            Assert.Contains("Смартфон Ozon", testUser.Customer.Cart);
        }
    }
}