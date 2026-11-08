namespace lesson3_loyalty_system.Models
{
    public class Product
    {
        public string Sku { get; } // SKU — уникальный складской код товара (артикул)
        public string Title { get; private set; } // Название товара
        public decimal Price { get; private set; } // Цена товара. decimal — стандарт для денежных расчетов в C#
        public int Stock { get; private set; } // Количество единиц товара, доступных для покупки на складе
        public Product (string sku, string title, decimal price, int stock) // Конструктор: создает заполненную карточку товара
        {
            Sku = sku;
            Title = title;
            Price = price;
            Stock = stock;
        }
        public bool ReduceStock(int quantity)  // Бизнес-логика: этот метод уменьшает остаток на складе, если товара хватает
        {
            if (Stock >= quantity) // Защитная проверка: если на складе товара больше или столько же, сколько запросил клиент
            { 
                Stock -= quantity; // Уменьшаем складской остаток на купленное количество
                return true; // Успешно забронировано
            }
            return false; // Если товара на складе физически не хватает для этой покупкиS
        }
    }
}
