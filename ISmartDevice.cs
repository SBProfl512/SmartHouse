// ==========================================
// 4. КОНТРАКТ СИСТЕМЫ (ИНТЕРФЕЙС)
// ==========================================

namespace SmartHomeApp
{
    public interface ISmartDevice
    {
        // Требование: у любого устройства должно быть имя (доступно для чтения)
        string Name { get; }
       
        // Требование: у любого устройства должен быть флаг включения (чтение и запись)
        bool IsOn { get; }

        event Action<string> Notify;
      

        // Требование: устройство обязано уметь включаться (тела метода нет, только заголовок)
        void TurnOn();
      

        // Требование: устройство обязано уметь выключаться (тела метода нет)
        void TurnOff();
        

        // МАГИЯ C# 8.0+: Реализация по умолчанию. 
        // Если класс сам не напишет метод PrintStatus, автоматически применится этот код.
        // Работает только при обращении к объекту через переменную интерфейса.
        public void PrintStatus()
        {
            System.Console.WriteLine(new string('-',100));
            System.Console.WriteLine($"Стутус устройства[{Name}]  ");
            System.Console.WriteLine($"Устройство сейчас {(IsOn ? "Включено" : "Выключено")}");
            System.Console.WriteLine(new string('-',100));
        } 

        public void DisplayNotify(string message);
        
    }

}
