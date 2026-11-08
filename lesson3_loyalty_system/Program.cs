using lesson3_loyalty_system.Enums; // Подключаем наши папки
using lesson3_loyalty_system.Models;
using lesson3_loyalty_system.Services;
using System;
class Program
{
    static void Main()  // static void Main —  отсюда компьютер начинает читать код
    {
        // 1. Создаем покупателя
        User client = new User(1, "Алиса", "alica@example.com"); // Мы еще не подключили настоящую базу данных, просто имитируем (моделируем) ее работу. Dручную передали 1, как будто этот ID нам только что прислала база данных.
        IBonusService bonusService = new BonusServiсe();

        // Начислим Алисе немного приветственных бонусов (например, 5000 баллов)
        // Чтобы начислить 5000 бонусов при кэшбеке 10%, сымитируем "предыдущую покупку" на 50000 руб.
        bonusService.AwardBonuses(client.Id, 50000m);
        Console.WriteLine("\n--- Система лояльности ---");
        Console.WriteLine($"Баланс баллов нового пользователя: {bonusService.GetBalance(client.Id)} баллов.");

        // 2. Наполняем виртуальный склад товарами
        Product laptop = new Product("LAP-ASus-034", "Ноутбук Asus", 85000.00m, stock: 3); // // Буква 'm' после цены — указывает, что это тип decimal
        Product mouse = new Product("MS-LOGI-0123", "Компьютерная мышь Logitech", 8000.00m, stock: 10);

        // 3. Клиент выбирает способ и адрес доставки
        Delivery delivery = new Delivery(101, "Москва, ул. Ленина, д. 10, кв 134", DeliveryType.PickupPoint);

        // 4. Система генерирует новый заказ для этого клиента
        Order order = new Order(777, client, delivery);

        // 5. Имитируем добавление товаров в корзину на сайте
        Console.WriteLine($"[Склад] Добавляем ноутбук. Результат: {order.AddProduct(laptop, 1)}");
        Console.WriteLine($"[Склад] Добавляем 2 мышки. Результат: {order.AddProduct(mouse, 2)}");

        // 6. Выводим информацию о заказе в консоль до оплаты
        Console.WriteLine($"\n--- Информация о заказе №{order.Id} ---");
        Console.WriteLine($"Текущий статус заказа: {order.Status}");
        Console.WriteLine($"Покупатель: {order.Customer.Name} {order.Customer.Email}");
        Console.WriteLine($"Адрес: {order.DeliveryInfo.Address}");
        Console.WriteLine($"Тип доставки: {order.DeliveryInfo.Type}");

        // 7.1 Создаем новый объект промокода со скидкой 10%
        PromoCode promo = new PromoCode("SKIDKA_123", 10m, PromoCodeType.Percentage);
        // 7.2 Передаем этот промокод внутрь нашего заказа через метод
        order.ApplyPromoCode(promo);

        // Получаем стоимость заказа ПОСЛЕ применения промокода
        decimal priceAfterPromo = order.CalculateTotal();
        Console.WriteLine($"Стоимость заказа после применения промокода: {priceAfterPromo} руб.");

        // 8. ПРИМЕНЯЕМ СИСТЕМУ ЛОЯЛЬНОСТИ
        // Считаем, сколько бонусов клиент может максимально списать (не более 50% от цены и не более ее баланса)
        decimal bonusDiscount = bonusService.CalculateMaxPossibleDiscount(client.Id, priceAfterPromo);
        decimal finalPriceToPay = priceAfterPromo - bonusDiscount;

        Console.WriteLine($"Доступно к списанию баллами: {bonusService.GetBalance(client.Id)} баллов.");
        Console.WriteLine($"Сумма к оплате после списания бонусов: {finalPriceToPay} руб.");

        // 9. Проводим транзакцию списания бонусов и оплату заказа
        if (bonusDiscount > 0)
        {
            bonusService.SpendBonuses(client.Id, bonusDiscount);
        }
        Console.WriteLine("\n[Система] Проводим транзакцию оплаты...");
        order.Pay();

        // 10.НАЧИСЛЯЕМ КЭШБЕК ЗА ПОКУПКУ
        // Кэшбек 10% начисляется только на ту сумму, которую клиент реально оплатил рублями
        bonusService.AwardBonuses(client.Id, finalPriceToPay);

        // 11. Проверяем, изменился ли статус и показываем баланс бонусов
        Console.WriteLine($"Новый статус заказа: {order.Status}");
        Console.WriteLine($"Остаток баллов на балансе: {bonusService.GetBalance(client.Id)} баллов");

        // 12. Проверяем, уменьшился ли остаток товаров на складе Ozon
        Console.WriteLine($"Остаток ноутбуков на складе: {laptop.Stock} шт.");
        Console.WriteLine($"Остаток мышек на складе: {mouse.Stock} шт.");

        // Задерживаем экран консоли, чтобы он не закрылся мгновенно
        Console.ReadLine();
    }
}