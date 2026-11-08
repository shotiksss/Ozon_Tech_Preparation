using System;
using System.Collections.Generic;
using System.Linq;
using lesson3_loyalty_system.Enums;  // Подключаем наши статусы заказов

namespace lesson3_loyalty_system.Models
{
    public class Order // Главный класс, связывающий пользователя, корзину и доставку
    {
        public int Id { get; }
        public User Customer { get; }
        public Delivery DeliveryInfo { get; }
        private readonly Dictionary<Product, int> _items = new Dictionary<Product, int>(); // Внутренний словарь: Ключ — Товар, Значение — Количество штук в заказе
        public IReadOnlyDictionary<Product, int> Items => _items; // Отдаем корзину наружу только для чтения, защищая от несанкционированного изменения
        public OrderStatus Status { get; private set; } // Статус заказа из нашего Enum
        public DateTime CreatedAt { get; } // Время оформления заказа
        public PromoCode? AppliedPromoCode { get; private set; } // Свойство для хранения примененного промокода. Знак ? означает, что изначально там может быть null (пустота)
        public Order(int id, User customer, Delivery deliveryInfo) // Конструктор: собирает новый пустой заказ для клиента
        {
            Id = id;
            Customer = customer;
            DeliveryInfo = deliveryInfo;
            Status = OrderStatus.New; // Присваиваем стартовый статус
            CreatedAt = DateTime.UtcNow; // Фиксируем время в международном формате UTC
        }
        public bool AddProduct (Product product, int quantity) // Метод добавления товара в корзину с автоматической проверкой остатка на складе
        {
            if (product.ReduceStock(quantity)) // Просим сам товар списать остаток. Если склад дал добро:
            {
                if (_items.ContainsKey(product)) // Если такой товар уже лежит в корзине — увеличиваем количество
                { 
                    _items[product] += quantity;
                }
                else // Если это новый товар в корзине — добавляем его
                {
                    _items.Add(product, quantity);
                }
                return true; // Успешно добавлено
            }
            return false; // На складе не хватило товара
        }
        public void ApplyPromoCode (PromoCode promoCode)  // Метод, который привязывает промокод к нашему заказу
        {
            AppliedPromoCode = promoCode;
            Console.WriteLine($"[Сиситема] к заказу №{Id} успешно применён промокод: {promoCode.Code}");
        }
         
        public decimal CalculateTotal() // Подсчет стоимости заказа с помощью инструмента LINQ (.Sum)
        {
            decimal baseTotal = _items.Sum( item => item.Key.Price * item.Value);
            if (AppliedPromoCode == null)  // 2. Защитная проверка: если промокода нет, сразу отдаем базовую стоимость
            {
                return baseTotal;
            }
            if (AppliedPromoCode.Type == PromoCodeType.Percentage) // 3. Если промокод есть, проверяем его тип через Enum
            {
                decimal Discount = baseTotal * (AppliedPromoCode.DiscountValue / 100m); // Считаем процентную скидку. Буква m указывает на тип decimal
                return baseTotal - Discount;
            }
            else if (AppliedPromoCode.Type == PromoCodeType.FixedAmount)
            {
                return Math.Max (0, baseTotal - AppliedPromoCode.DiscountValue);
            }
            return baseTotal;
        }
        public void Pay()
        {
            if (Status == OrderStatus.New)
            {
                Status = OrderStatus.Paid;
            }
        }
    }
}
