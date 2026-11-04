using System; // Подключаем базовую библиотеку
using System.Collections.Generic; // Подключаем библиотеку для работы со списками и коллекциями
namespace FirstLesson // Пространство имен
{
    // Корневой класс пользователя
    public class User //Объявляем публичный класс-чертеж User. Это фундамент нашей учетной записи человека
    {
        public Guid Id { get; set; } = Guid.NewGuid(); 
        //Guid — это формат данных (очень длинный уникальный набор букв и цифр). Каждому новому юзеру автоматически выдаем уникальный номер при помощи Guid.NewGuid(). { get; set; } означает, что этот ID можно читать (get) и изменять (set)
        public string Name { get; set; }
        public string Phone { get; set; }
        //public string Name { get; set; } и Phone — Текстовые свойства (string) для хранения имени и номера телефона
        public CustomerProfile Customer {  get; set; }
        public CurierProfile Curier { get; set; }
        //Внутри чертежа User заложены пустые слоты под профиль покупателя и курьера. По умолчанию они пусты (null), пока мы их не активируем
    }
    public class CustomerProfile
    //Отдельный чертеж для данных покупателя
    {
        public string DeliveryAddress { get; set; }
        public List<string> Cart {  get; set; } = new List<string>();
        //DeliveryAddress для хранения адреса доставки
        //Создаем корзину товаров. List<string> — это динамический список строк (текстов). С помощью = new List<string>(); мы сразу создаем пустую корзину в момент создания профиля, чтобы туда можно было что-то положить
        public void AddToCart (string Item) => Cart.Add (Item);
        //Метод берет нашу корзину Cart и добавляет (Add) туда присланный товар
    }
    public class CurierProfile
        //Чертеж профиля курьера
    {
        public string CarNumber { get; set; }
        public decimal Balance { get; set; } = 0;
        //CarNumber номер автомобиля
        //Кошелек курьера для зарплаты. Изначально равен нулю
        public void EarnMoney(decimal amount) => Balance += amount;
        //Метод курьера «Заработать денег»
    }
    class Program
        //Точка входа в программу
    {
       static void Main(string[] args)
        {
            Console.WriteLine("---Создаем пользователя---");
            //Вывод текста
            User ivan = new User
            //Мы берем чертеж User, пишем ключевое слово new (создать новый в памяти) и даем переменной имя ivan
            {
                Name = "Иван",
                Phone = "+7 (999) 111-22-33"
            };

            //// --- ИВАН ПОКУПАЕТ ТОВАР ---
            // Иван зашел в приложение Ozon как обычный покупатель

            ivan.Customer = new CustomerProfile { DeliveryAddress = "ул. Ленина, дом 10" };
            //Активируем Ивану роль покупателя. Мы берем пустой слот Customer внутри объекта Ивана и создаем (new) там полноценный профиль покупателя, сразу прописывая ему адрес

            ivan.Customer.AddToCart("Наушники Apple");
            ivan.Customer.AddToCart("Игрушка-антистресс");
            //Обращаемся к Ивану, заходим в его профиль покупателя и два раза вызываем метод добавления товара. В корзину улетают две строки
            Console.WriteLine($"Покупатель {ivan.Name} добавил в корзину {ivan.Customer.Cart.Count} товар(а).");
            //Выводим результат. Буква $ перед кавычками включает режим «интерполяции». Это позволяет вставлять переменные прямо внутрь текста с помощью фигурных скобок { }
            Console.WriteLine($"Адрес доставки: {ivan.Customer.DeliveryAddress}");
            //Выводим на экран адрес доставки, который вытащили из профиля покупателя Ивана

            // --- ИВАН ИДЕТ НА РАБОТУ ---
            // Иван решил подработать курьером. Мы НЕ создаем нового пользователя.

            ivan.Curier = new CurierProfile { CarNumber = "A123AA 777" };
            // Мы просто активируем ему профиль курьера
            ivan.Curier.EarnMoney(350);
            ivan.Curier.EarnMoney(150);
            //Вызываем у Ивана через курьерский профиль метод заработка денег. На его баланс падают деньги за доставки

            Console.WriteLine("\n Проверка совмещения ролей");

            Console.WriteLine($"Курьер {ivan.Name} на машине {ivan.Curier.CarNumber} заработал за смену: {ivan.Curier.Balance} руб.");
            //Выводим данные курьера: имя берется из корневого профиля Ивана, а машина и баланс — из его курьерского профиля
            Console.WriteLine($"Связаться с курьером по телефону: {ivan.Phone}");
        }
    }
}
