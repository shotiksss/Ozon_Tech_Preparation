using System;
using lesson_4_VAR_DYNAMIC_GUID_INT;
// 1. Создаем два чека с помощью var (компилятор сам поймет, что тип — Receipt)
var receipt1 = new Receipt(101, 1500.00m);
var receipt2 = new Receipt(102, 499.99m);
// 2. Выводим публичный номер (int) и скрытый системный ключ (Guid)
Console.WriteLine($"Чек № {receipt1.PublicNumber} | Сумма: {receipt1.Amount} руб. | Системный ID: {receipt1.SystemId}");
Console.WriteLine($"Чек № {receipt2.PublicNumber} | Сумма: {receipt2.Amount} руб. | Системный ID: {receipt2.SystemId}");

Console.WriteLine("---");
// 3. Работаем с dynamic (имитируем приход неизвестных данных с кассы)
dynamic externalData = receipt1;
Console.WriteLine($"Сумма первого чека из dynamic: {externalData.Amount} руб.");
// 4. Меняем тип внутри dynamic на строку (компилятор это разрешит)
externalData = "Сбой связи с кассой";
Console.WriteLine($"Статус в dynamic: {externalData}");