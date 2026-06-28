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
        public bool IsOn { get; set; } = false;

        // Выполняем требование интерфейса по методу TurnOn
        public void TurnOn()
        {
            if (!IsOn)
            {
                IsOn = true; // Меняем внутреннее состояние
                System.Console.WriteLine($"{Name} плавно зажглась на 100%");
            }
        }

        // Выполняем требование интерфейса по методу TurnOff
        public void TurnOff()
        {
            if (IsOn)
            {
                IsOn = false; // Меняем внутреннее состояние
                System.Console.WriteLine($"{Name} плавно потухла");
            }

        }
    }

    // Класс "Умный чайник" тоже обязуется выполнить контракт ISmartDevice
    public class SmartKettle : ISmartDevice
    {
        // Реализация свойства Name со своим кухонным именем.
        public string Name { get; } = "Чайник на кухне";

        // Состояние чайника.
        public bool IsOn { get; set; } = false;

        // Своя уникальная реакция на команду включения
        public void TurnOn()
        {
            if (!IsOn)
            {
                IsOn = true;
                System.Console.WriteLine($"{Name}, вода начинает закипать...");
            }


        }

        // Своя уникальная реакция на команду выключения
        public void TurnOff()
        {
            if (IsOn)
            {
                IsOn = false;
                System.Console.WriteLine($"{Name} выключился");
            }

        }
    }

    public class SmartAirConditioner : ISmartDevice
    {
        public string Name { get; set; } = "Кондиционер на кухне";
        private int Temp
        {
            get;
            set
            {
                if (value >= 15 && value <= 45)
                {
                    field = value;
                }
                else System.Console.WriteLine("Неверные параметры кондиционера");
            }

        } = 20;
        public bool IsOn { get; set; } = false;

        // Своя уникальная реакция на команду включения
        public void TurnOn()
        {
            if (!IsOn)
            {
                IsOn = true;
                System.Console.WriteLine($"{Name}, подано питание. Температурный режим {Temp} градусов Цельсия");
            }


        }

        public void TurnOff()
        {
            if (IsOn)
            {
                IsOn = false;
                System.Console.WriteLine($"{Name} выключился");
            }

        }

        public void SetTemp(int temp, Func<int, int, int> setTemp)
        {
            if (IsOn)
            {
               this.Temp = setTemp(this.Temp, temp);
               System.Console.WriteLine($"Температурный режим [{Name}] изменен на {this.Temp} градусов Цельсия");
            }
            
        }
    }
}


