// ==========================================
// 3. РЕАЛИЗАЦИЯ ЖЕЛЕЗА (КОНКРЕТНЫЕ КЛАССЫ)
// ==========================================

namespace SmartHomeApp
{
    // Класс "Умная лампа" обязуется выполнить контракт ISmartDevice
    public class SmartLamp : ISmartDevice
    {
        // Реализация свойства Name. Задаем дефолтное имя для лампы.
        public string Name { get; } = "Лампа в гостинной";

        // Состояние устройства. Изначально выключено (false).
        public bool IsOn { get; private set; } = false;

        public event Action<string>? Notify;

        public SmartLamp()
        {
            Notify += DisplayNotify;
        }
        

        // Выполняем требование интерфейса по методу TurnOn
        public void TurnOn()
        {
            if (!IsOn)
            {
                IsOn = true; // Меняем внутреннее состояние
                Notify?.Invoke($"{Name} плавно зажглась на 100%");
                
            }
        }

        // Выполняем требование интерфейса по методу TurnOff
        public void TurnOff()
        {
            if (IsOn)
            {
                IsOn = false; // Меняем внутреннее состояние
                Notify?.Invoke($"{Name} плавно потухла");
            }

        }

        public void DisplayNotify(string message) => System.Console.WriteLine(message);
    }

    // Класс "Умный чайник" тоже обязуется выполнить контракт ISmartDevice
    public class SmartKettle : ISmartDevice
    {
        // Реализация свойства Name со своим кухонным именем.
        public string Name { get; } = "Чайник на кухне";

        // Состояние чайника.
        public bool IsOn { get; private set; } = false;

        public event Action<string>? Notify;

        

        public SmartKettle()
        {
            Notify += DisplayNotify;
        }

        // Своя уникальная реакция на команду включения
        public void TurnOn()
        {
            if (!IsOn)
            {
                IsOn = true;
                Notify?.Invoke($"{Name}, вода начинает закипать...");
                System.Console.WriteLine("Чайник закипел");
                TurnOff(); // автоматическое выключение чайника
            }


        }

        // Своя уникальная реакция на команду выключения
        public void TurnOff()
        {
            if (IsOn)
            {
                IsOn = false;
                Notify?.Invoke($"{Name} выключился");

            }

        }

        public void DisplayNotify(string message) => System.Console.WriteLine(message);
    }

    public class SmartAirConditioner : ISmartDevice
    {
        public string Name { get; set; } = "Кондиционер на кухне";
        public int Temp
        {
            get;
            private set
            {
                if (value >= 15 && value <= 45)
                {
                    field = value;
                }
                else System.Console.WriteLine("Неверные параметры кондиционера");
            }

        } = 20;
        public bool IsOn { get; private set; } = false;

        public event Action<string>? Notify;

        public SmartAirConditioner()
        {
            Notify += DisplayNotify;
        }

        // Своя уникальная реакция на команду включения
        public void TurnOn()
        {
            if (!IsOn)
            {
                IsOn = true;
                Notify?.Invoke($"{Name}, подано питание. Температурный режим {Temp} градусов Цельсия");
                
            }


        }

        public void TurnOff()
        {
            if (IsOn)
            {
                IsOn = false;
                Notify?.Invoke($"{Name} выключился");
            }

        }

        public void SetTemp(int temp, Func<int, int, int> setTemp)
        {
            if (IsOn)
            {
               this.Temp = setTemp(this.Temp, temp);
               Notify?.Invoke($"Температурный режим [{Name}] изменен на {this.Temp} градусов Цельсия");
            }
            
        }

        public void DisplayNotify(string message) => System.Console.WriteLine(message);
    }

    
}


