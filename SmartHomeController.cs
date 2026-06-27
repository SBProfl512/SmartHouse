// ==========================================
// 2. УПРАВЛЯЮЩАЯ ЛОГИКА (АРХИТЕКТУРА)
// ==========================================


namespace SmartHomeApp
{
    public class SmartHomeController
    {
        // Инкапсуляция: список скрыт внутри класса, чтобы никто извне не мог его испортить.
        // Хранит не конкретные классы, а ссылки на интерфейс ISmartDevice.
        private List<ISmartDevice> devices = new List<ISmartDevice>();

        // Метод принимает ЛЮБОЙ объект, который подписан на контракт ISmartDevice
        public void AddDivices(ISmartDevice device)
        {
            devices.Add(device); // Добавляем в общий список
            System.Console.WriteLine($"Устройство [{device.Name}] успешно подключено");
        }

        public void TurnOffEverytghing()
        {
            // Пробегаемся по нашей "коробке" с устройствами
            foreach (ISmartDevice device in devices)
            {
                // Вызываем контрактный метод. Каждое устройство само знает, как правильно выключаться!
                device.TurnOff();

                // Вызываем метод по умолчанию из интерфейса, чтобы сразу увидеть изменение флага IsOn
                device.PrintStatus();
            }
            System.Console.WriteLine("Все устройства успешно выключены");
        }

        public void ShowStatuses()
        {
            // Важно: тип переменной в цикле указан как интерфейс (ISmartDevice).
            // Только благодаря этому C# видит метод по умолчанию PrintStatus().
            System.Console.WriteLine(new string('=',100));
            System.Console.WriteLine($"Стутус устройств дома ");
            foreach (ISmartDevice device in devices)
            {
                device.PrintStatus();
            }
            System.Console.WriteLine(new string('=',100));
        }
    }
}

