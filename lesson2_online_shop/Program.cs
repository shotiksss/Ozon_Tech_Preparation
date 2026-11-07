using System;
using lesson2_online_shop.Enums; // Подключаем наши папки
using lesson2_online_shop.Models;
class Program
{
    static void Main()  // static void Main —  отсюда компьютер начинает читать код
    {
        // 1. Создаем покупателя
        User client = new User(1,"Алиса", "alica@example.com" ); // Мы еще не подключили настоящую базу данных, просто имитируем (моделируем) ее работу. Dручную передали 1, как будто этот ID нам только что прислала база данных.

        // 2. Наполняем виртуальный склад товарами
        Product laptop = new Product("LAP-ASus-034", "Ноутбук Asus", 85000.00m, stock: 3); // // Буква 'm' после цены — указывает, что это тип decimal
        Product mouse = new Product("MS-LOGI-0123", "Компьютерная мышь Logitech", 8000.00m, stock: 10);

        // 3. Клиент выбирает способ и адрес доставки
        Delivery delivery = new Delivery(101, "Москва, ул. Ленина, д. 10, кв 134", DeliveryType.PickupPoint);

        // 4. Система генерирует новый заказ для этого клиента
        Order order = new Order(777, client, delivery);

        // 4.1 Создаем новый объект промокода со скидкой 10%
        PromoCode promo = new PromoCode("SKIDKA_123", 10m, PromoCodeType.Percentage);
        // 4.2 Передаем этот промокод внутрь нашего заказа через метод
        order.ApplyPromoCode(promo);

        // 5. Имитируем добавление товаров в корзину на сайте
        Console.WriteLine($"[Склад] Добавляем ноутбук. Результат: {order.AddProduct(laptop, 1)}");
        Console.WriteLine($"[Склад] Добавляем 2 мышки. Результат: {order.AddProduct(mouse, 2)}");

        // 6. Выводим информацию о заказе в консоль до оплаты
        Console.WriteLine($"\n--- Информация о заказе №{order.Id} ---");
        Console.WriteLine($"Покупатель: {order.Customer.Name} {order.Customer.Email}");
        Console.WriteLine($"Адрес: {order.DeliveryInfo.Address}");
        Console.WriteLine($"Тип доставки: {order.DeliveryInfo.Type}");
        Console.WriteLine($"Текущий статус заказа: {order.Status}");
        Console.WriteLine($"Итоговая сумма к оплате: {order.CalculateTotal()} руб.");

        // 7. Проводим оплату
        Console.WriteLine("\n[Система] Проводим транзакцию оплаты...");
        order.Pay();

        // 8. Проверяем, изменился ли статус
        Console.WriteLine($"Новый статус заказа: {order.Status}");

        // 9. Проверяем, уменьшился ли остаток товаров на складе Ozon
        Console.WriteLine($"Остаток ноутбуков на складе: {laptop.Stock} шт.");
        Console.WriteLine($"Остаток мышек на складе: {mouse.Stock} шт.");

        // Задерживаем экран консоли, чтобы он не закрылся мгновенно
        Console.ReadLine();
    }
}