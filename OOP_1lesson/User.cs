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
        public CourierProfile Courier { get; set; }
        //Внутри чертежа User заложены пустые слоты под профиль покупателя и курьера. По умолчанию они пусты (null), пока мы их не активируем
    }
}
