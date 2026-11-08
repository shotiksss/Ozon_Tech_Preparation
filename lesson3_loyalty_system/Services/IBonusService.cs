namespace lesson3_loyalty_system.Services
{
    public interface IBonusService
    {
        // 1. Получить текущий баланс пользователя
        decimal GetBalance(int userId);
        // 2. Рассчитать максимально возможную скидку для конкретного заказа
        decimal CalculateMaxPossibleDiscount(int userId, decimal orderPrice);
        // 3. Использовать (списать) бонусы при оформлении заказа
        void SpendBonuses(int userId, decimal amount);
        // 4. Начислить бонусы в виде кэшбека от оплаченной суммы
        void AwardBonuses(int userId, decimal paidAmount);
    }
}
