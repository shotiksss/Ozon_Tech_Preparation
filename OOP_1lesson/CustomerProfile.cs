namespace FirstLesson // Пространство имен
{
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
}
