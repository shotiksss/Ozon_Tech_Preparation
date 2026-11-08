namespace lesson3_loyalty_system.Models
{
    public class User // Создаем публичный класс (чертеж) для создания пользователей
    {
        public int Id { get; } // Уникальный ID. Конструкция { get; } делает свойство неизменяемым (только для чтения)
        public string Name { get; private set; } // Имя клиента. private set разрешает менять имя только изнутри этого класса
        public string Email { get; private set; } // Email. private set защищает адрес почты от случайной перезаписи извне
        public User(int id, string name, string email) // Конструктор: функция, собирающая конкретного пользователя при регистрации
        {
            Id = id;
            Name = name;
            Email = email;
        }
    }
}
