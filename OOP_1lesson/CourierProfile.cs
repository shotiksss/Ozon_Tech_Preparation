namespace FirstLesson // Пространство имен
{
    public class CourierProfile
        //Чертеж профиля курьера
    {
        public string CarNumber { get; set; }
        public decimal Balance { get; private set; } = 0;
        //CarNumber номер автомобиля
        //Кошелек курьера для зарплаты. Изначально равен нулю
        public void EarnMoney(decimal amount) => Balance += amount;
        //Метод курьера «Заработать денег»
    }
}
