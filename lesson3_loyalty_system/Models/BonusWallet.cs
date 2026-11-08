namespace lesson3_loyalty_system.Models
{
    public class BonusWallet
    {
        // 1. Уникальный идентификатор пользователя, которому принадлежит кошелек
        public int UserId { get; }
        // 2. Приватное поле для хранения баланса. 
        // Мы не даем менять его напрямую снаружи, чтобы никто случайно не "нарисовал" себе бонусы.
        private decimal _balance;
        // 3. Публичное свойство только для чтения, чтобы другие сервисы могли узнать баланс
        public decimal Balance => _balance;
        // 4. Конструктор: при создании кошелька мы обязательно привязываем его к ID пользователя
        public BonusWallet(int userId)
        {
            UserId = userId;
            _balance = 0; // Изначально у нового пользователя 0 бонусов
        }
        // 5. Метод для начисления бонусов
        public void AddBonuses(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Сумма должна быть больше нуля!");
            _balance += amount;
        }
        // 6. Метод для списания бонусов
        public void SpendBonuses(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Сумма списания должна быть больше нуля!");
            if (_balance < amount)
                throw new InvalidOperationException("Недостаточно баллов на балансе.");
            _balance -= amount;
        }
    }
}
