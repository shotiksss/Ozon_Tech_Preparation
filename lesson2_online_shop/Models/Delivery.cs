using lesson2_online_shop.Enums;// Подключаем папку с перечислениями, чтобы этот класс "увидел" тип DeliveryType

namespace lesson2_online_shop.Models
{
    public class Delivery // Создаем публичный класс для управления доставкой заказа
    {
        public int Id { get; } // Номер транспортной накладной
        public string Address { get; private set; } // Адрес доставки (дом клиента или адрес ПВЗ)
        public DeliveryType Type { get; private set; } // Тип доставки, который мы берем строго из созданного нами ранее Enum
        public string TrackingStatus { get; private set; }  // Текущий статус посылки в пути
        public Delivery (int id, string address, DeliveryType type) // Конструктор для сборки информации о доставке
        {
            Id = id;
            Address = address;
            Type = type;
            TrackingStatus = "Накладная создана"; // Бизнес-правило: в момент создания доставки статус всегда стартовый
        }
        public void UpdateStatus(string newStatus)
        {
            TrackingStatus = newStatus;
        }
    }
}
