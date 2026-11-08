using lesson3_loyalty_system.Models;

namespace lesson3_loyalty_system.Services
{
    public class BonusServiсe : IBonusService // Двоеточие здесь означает реализацию интерфейса (или контракт на выполнение).
    {
        // 1. Хранилище кошельков в памяти: ключ — ID пользователя, значение — его кошелек
        private readonly Dictionary<int, BonusWallet> _wallets = new();
        // Вспомогательный метод: находит кошелек или создает новый, если пользователь пришел впервые\
        private BonusWallet GetOrCreateWallet(int userId)
        {
            if (!_wallets.TryGetValue(userId, out var wallet)) // Если кошелек !-НЕ найден
            {
                wallet = new BonusWallet(userId);
                _wallets[userId] = wallet;
            }
            return wallet;
        }
        // 2. Получение баланса
        public decimal GetBalance(int userId)
        {
            return GetOrCreateWallet(userId).Balance;
        }
        // 3. Расчет максимальной скидки (не более 50% от заказа и не более баланса)
        public decimal CalculateMaxPossibleDiscount(int userId, decimal orderPrice)
        {
            if (orderPrice <= 0) return 0;
            var wallet = GetOrCreateWallet(userId);
            decimal halfPrice = orderPrice * 0.5m; // Бизнес-правило: максимум 50%
            // Выбираем наименьшее значение между половиной стоимости заказа и балансом кошелька
            return Math.Min(halfPrice, wallet.Balance);
        }
        // 4. Списание бонусов
        public void SpendBonuses(int userId, decimal amount)
        {
            var wallet = GetOrCreateWallet(userId);
            wallet.SpendBonuses(amount); // Логика проверок сработает внутри кошелька
        }
        // 5. Начисление кэшбека (10% от фактически оплаченной суммы)
        public void AwardBonuses(int userId, decimal paidAmount)
        {
            if (paidAmount <= 0) return;
            var wallet = GetOrCreateWallet(userId);
            decimal cashback = paidAmount * 0.10m;
            wallet.AddBonuses(cashback);
        }
    }
}
