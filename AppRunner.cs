
namespace SmartHomeApp
{
    class AppRunner
    {
        public static void Run()
        {
            
            // ==========================================
            // 1. ТОЧКА ВХОДА (КОНСОЛЬНЫЙ КЛИЕНТ)
            // ==========================================
            Console.WriteLine("Hello, World!");

            // Создаем "мозг" системы — контроллер, который будет управлять устройствами
            SmartHomeController home = new SmartHomeController();

            // Создаем конкретные объекты железа (выделяется память в куче)
            SmartLamp lamp = new SmartLamp();
            SmartKettle kettle = new SmartKettle();
            ISmartDevice airCondice = new SmartAirConditioner();

            // Включаем устройства напрямую через их собственные методы
            //lamp.TurnOn();
            //kettle.TurnOn();

            // ПОЛИМОРФИЗМ В ДЕЙСТВИИ: метод AddDevices принимает интерфейс ISmartDevice.
            // Мы передаем туда конкретные классы SmartLamp и SmartKettle, и контроллер принимает их без проблем.
            home.AddDivices(lamp);
            home.AddDivices(kettle);
            home.AddDivices(airCondice);
            airCondice.TurnOn();


           if(airCondice is SmartAirConditioner airConditione) airConditione.SetTemp(5, (x,y) => x + y );
          

            // Выключаем весь дом одной кнопкой
            home.TurnOffEverytghing();

            home.ShowStatuses();

        
           
        }

    }
}
