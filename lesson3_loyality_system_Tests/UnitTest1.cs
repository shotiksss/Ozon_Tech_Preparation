using Xunit; // Подключаем библиотеку xUnit для работы с тестами
using lesson3_loyalty_system.Enums;
using lesson3_loyalty_system.Models;
using lesson3_loyalty_system.Services;

namespace lesson3_loyalty_system_Tests
{
    public class UnitTest1
    {
        // ТЕСТЫ ИЗ УРОКА 2 (СОХРАНЯЕМ И АДАПТИРУЕМ)
        [Fact]  // Атрибут [Fact] говорит системе xUnit, что этот метод является тест-кейсом
        public void CalculateTotal_WithPercentagePromoCode_ShouldApplyDiscount()
        {
            // 1. ARRANGE (Подготовка данных)
            User user = new User(1, "Тест Пользователь", "test@test.com");
            Delivery delivery = new Delivery(1, "ул. Пушкина, дом Колотушкина", DeliveryType.Courier);
            Order order = new Order(1, user, delivery);

            // Создаем тестовый товар за 30 000 рублей
            Product phone = new Product("Iphone-Test", "IphoneX", 30000.00m, stock: 5);
            order.AddProduct(phone, 1); // Добавляем 1 штуку в заказ. Сумма заказа = 30 000 руб.

            // Создаем промокод на 10% скидки
            PromoCode promo = new PromoCode("Test_1", 10m, PromoCodeType.Percentage);

            // 2. ACT (Выполнение действия)
            order.ApplyPromoCode(promo);  // Применяем промокод к заказу
            decimal finalTotal = order.CalculateTotal();

            // 3. ASSERT (Проверка результата)
            // Мы знаем: 30 000 - 10% = 27 000 рублей.
            // Метод Assert.Equal сравнивает ожидаемое значение (27000) и реальное (finalTotal).
            Assert.Equal(27000.00m, finalTotal);
        }
        [Fact]
        public void CalculateTotal_WithFixedAmountPromoCode_ShouldApplyDiscount()
        {
            // 1. ARRANGE (Подготовка данных)
            User user = new User(1, "Тест Пользователь", "test@test.com");
            Delivery delivery = new Delivery(1, "ПВЗ123", DeliveryType.PickupPoint);
            Order order = new Order(1, user, delivery);

            // Создаем тестовый товар за 10 000 рублей
            Product curler = new Product("Curler-Test", "Плойка", 10000.00m, stock: 5);
            order.AddProduct(curler, 2); // Добавляем 2 штуки в заказ. Сумма заказа = 20 000 руб.

            // Создаем промокод на 3000 р.
            PromoCode promo = new PromoCode("Test_1", 3000.00m, PromoCodeType.FixedAmount);

            // 2. ACT (Выполнение действия)
            order.ApplyPromoCode(promo);  // Применяем промокод к заказу
            decimal finalTotal = order.CalculateTotal();

            // 3. ASSERT (Проверка результата)
            // Мы знаем: 20 000 - 3000 = 17 000 рублей.
            // Метод Assert.Equal сравнивает ожидаемое значение (27000) и реальное (finalTotal).
            Assert.Equal(17000.00m, finalTotal);
        }

        // НОВЫЕ ТЕСТЫ ДЛЯ УРОКА 3 (ПРОГРАММА ЛОЯЛЬНОСТИ)
        [Fact] // Проверяем лимит списания в 50%
        public void CalculateMaxPossibleDiscount_ShouldNotExceedWithBigBalance()
        {
            //Arrange
            var bonusService = new BonusServiсe();
            int testUserId = 99;
            decimal orderPrice = 2000m;

            // Сымитируем прошлую покупку на 100 000 руб, чтобы у пользователя стало 10 000 баллов
            bonusService.AwardBonuses(testUserId, 100000m);

            //Act
            // Пытаемся рассчитать скидку для заказа в 2000 рублей
            decimal discount = bonusService.CalculateMaxPossibleDiscount(testUserId, orderPrice);

            // Assert
            // 50% от 2000 рублей — это 1000 рублей = 1000 баллами. 
            // Система должна снять ровно 1000, проигнорировав огромный баланс в 10 000 баллов.
            Assert.Equal(1000m, discount);
        }

        [Fact] // Проверяем правильность начисления 10% кэшбека
        public void AwardBonuses_ShouldAddExactlyTenPercentOfPaidAmount()
        {
            //Arrange
            var bonusService = new BonusServiсe();
            int testUserId = 49;
            decimal paidAmount = 20500m;

            //Act
            bonusService.AwardBonuses(testUserId, paidAmount);

            // Assert
            // 10% от 20500 рублей — это ровно 2050 бонусов. Проверяем баланс кошелька.
            decimal expectedBalance = 2050m;
            Assert.Equal(expectedBalance, bonusService.GetBalance(testUserId));
        }
    }
}