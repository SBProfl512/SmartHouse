using System;
using System.Collections.Generic;

// =============================================================
// Буду пока вести локальный гит, как раз научусь с ним работать
// =============================================================

// ==========================================
// 1. ТОЧКА ВХОДА (КОНСОЛЬНЫЙ КЛИЕНТ)
// ==========================================
Console.WriteLine("Hello, World!");

// Создаем "мозг" системы — контроллер, который будет управлять устройствами
SmartHomeController home = new SmartHomeController();

// Создаем конкретные объекты железа (выделяется память в куче)
SmartLamp lamp = new SmartLamp();
SmartKettle kettle = new SmartKettle();

// Включаем устройства напрямую через их собственные методы
lamp.TurnOn();
kettle.TurnOn();

// ПОЛИМОРФИЗМ В ДЕЙСТВИИ: метод AddDevices принимает интерфейс ISmartDevice.
// Мы передаем туда конкретные классы SmartLamp и SmartKettle, и контроллер принимает их без проблем.
home.AddDivices(lamp);
home.AddDivices(kettle);

// Проверяем текущий статус всех подключенных устройств
home.ShowStatuses();

// Выключаем весь дом одной кнопкой
home.TurnOffEverytghing();

// Проверяем статус еще раз, чтобы убедиться, что всё выключилось
home.ShowStatuses();


// ==========================================
// 2. УПРАВЛЯЮЩАЯ ЛОГИКА (АРХИТЕКТУРА)
// ==========================================
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
        foreach (ISmartDevice device in devices)
        {
            device.PrintStatus();
        }
    }
}


// ==========================================
// 3. РЕАЛИЗАЦИЯ ЖЕЛЕЗА (КОНКРЕТНЫЕ КЛАССЫ)
// ==========================================

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
        IsOn = true; // Меняем внутреннее состояние
        System.Console.WriteLine($"{Name} плавно зажглась на 100%");
    }

    // Выполняем требование интерфейса по методу TurnOff
    public void TurnOff()
    {
        IsOn = false; // Меняем внутреннее состояние
        System.Console.WriteLine($"{Name} плавно потухла");
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
        IsOn = true;
        System.Console.WriteLine($"{Name}, вода начинает закипать...");
    }

    // Своя уникальная реакция на команду выключения
    public void TurnOff()
    {
        IsOn = false;
        System.Console.WriteLine($"{Name} выключился");
    }
}


// ==========================================
// 4. КОНТРАКТ СИСТЕМЫ (ИНТЕРФЕЙС)
// ==========================================
public interface ISmartDevice
{
    // Требование: у любого устройства должно быть имя (доступно для чтения)
    string Name { get; }
    
    // Требование: у любого устройства должен быть флаг включения (чтение и запись)
    bool IsOn { get; set; }

    // Требование: устройство обязано уметь включаться (тела метода нет, только заголовок)
    void TurnOn();

    // Требование: устройство обязано уметь выключаться (тела метода нет)
    void TurnOff();

    // МАГИЯ C# 8.0+: Реализация по умолчанию. 
    // Если класс сам не напишет метод PrintStatus, автоматически применится этот код.
    // Работает только при обращении к объекту через переменную интерфейса.
    public void PrintStatus() => System.Console.WriteLine($"Устройство [{Name}] сейчас {(IsOn ? "Включено" : "Выключено")}");
}
