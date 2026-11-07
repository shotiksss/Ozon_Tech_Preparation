using lesson2_online_shop.Enums; // Подключаем папку Enums, так как нам внутри нужен созданный нами ранее PromoCodeType
namespace lesson2_online_shop.Models
{
    public class PromoCode // Публичный класс (модель) промокода
    {
        public string Code { get; } // Кодовое слово, которое клиент вводит на сайте (например, "SKIDKA2026")
        public decimal DiscountValue { get; }  // Размер скидки. Может быть как процентом (например, 10), так и рублями (например, 500)
        public PromoCodeType Type { get; }  // Тип промокода: берется строго из нашего Enum (Percentage или FixedAmount)
        public PromoCode (string code, decimal discountValue, PromoCodeType type)
        {
            Code = code;
            DiscountValue = discountValue;
            Type = type;
        }

    }
}
