using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace NewtonsoftJsonLibrary
{
   internal class Program
   {
      static void Main()
      {
         CaseOne();
         Console.WriteLine();
         CaseTwo();
         Console.WriteLine();
         CaseThree();
         Console.WriteLine();
         CaseFive();

         Console.ReadKey();
      }

      static void CaseFive()
      {
         EventLog log = new EventLog
         {
            EventName = "Точное время",
            Timestamp = DateTime.Now
         };

         // Настройка формата даты с помощью JsonSerializerSettings
         JsonSerializerSettings customformat = new JsonSerializerSettings
         {
            DateFormatString = "yyyy-MM-dd HH:mm:ss.fff"
         };

         string customJson = JsonConvert.SerializeObject(log, customformat);
         Console.WriteLine(customJson);

         EventLog deserializedEvent = JsonConvert.DeserializeObject<EventLog>(customJson, customformat);
         Console.WriteLine("Десериализованная дата: {0}", deserializedEvent.Timestamp);
         Console.WriteLine("Время (в формате строки): {0}", deserializedEvent.Timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff"));

         // Настройка формата даты через IsoDateTimeConverter
         JsonSerializerSettings customsettings = new JsonSerializerSettings
         {
            Converters = { new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff" } }
         };

         string json = JsonConvert.SerializeObject(log, customsettings);
         EventLog deserializedEven = JsonConvert.DeserializeObject<EventLog>(json, customsettings);
         Console.WriteLine(deserializedEven.Timestamp);
         Console.WriteLine("Десериализованная дата: {0}", deserializedEven.Timestamp);
         Console.WriteLine("Время (в формате строки): {0}", deserializedEven.Timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff"));
      }

      // Сериализация/десериализация точного времени в Unix‑timestamp в миллисекундах (13‑значное число)
      static void CaseThree()
      {
         Event eventItem = new Event
         {
            Name = "Структура DateTime",
            Date = DateTime.Now,
            DateOffset = DateTimeOffset.Now,
            TimeStamp = DateTimeOffset.Now.ToUnixTimeMilliseconds()
         };

         // Форматированный JSON
         string json = JsonConvert.SerializeObject(eventItem, Formatting.Indented);
         Console.WriteLine("Сериализация с форматированием (читаемый JSON):");
         Console.WriteLine(json);

         // Десериализация
         Event deserializedEvent = JsonConvert.DeserializeObject<Event>(json);
         Console.WriteLine("\nДесериализованная дата: {0}", deserializedEvent.Date);

      }

      // Сериализация с форматированием (читаемый JSON)
      static void CaseTwo()
      {
         // Создание объекта
         User user = new User
         {
            UserName = "Мишель Трахтенберг",
            Email = "michelletrachtenberg@example.com",
            IsActive = true,
            Roles = { "Actress", "Producer" }
         };

         // 1. Простая сериализация
         string jsonserialize = JsonConvert.SerializeObject(user);
         Console.WriteLine("1. Простая сериализация объекта:");
         Console.WriteLine(jsonserialize);

         // 2. Сериализация с форматированием (читаемый JSON)
         string jsonserializeformatting = JsonConvert.SerializeObject(user, Formatting.Indented);
         Console.WriteLine("\n2. Сериализация объекта с форматированием:");
         Console.WriteLine(jsonserializeformatting);

         // Десериализация объекта
         Console.WriteLine("\n3. Десериализация объекта");
         User jsondeserialized = JsonConvert.DeserializeObject<User>(jsonserializeformatting);
         Console.WriteLine("Имя пользователя: {0}", jsondeserialized.UserName);
         Console.WriteLine("Email: {0}", jsondeserialized.Email);
         Console.WriteLine("Активен: {0}", jsondeserialized.IsActive);
         Console.WriteLine("Роли: {0}", string.Join(", ", jsondeserialized.Roles));
      }

      // Базовая сериализация/десериализация коллекции
      static void CaseOne()
      {
         Console.WriteLine("1. Сериализация коллекции");
         // 1. Сериализация коллекции
         List<Movie> movies = new List<Movie>
         {
            new Movie{ Id=1, Title="Титаник" },
            new Movie{ Id=2, Title="Марсианин"},
            new Movie{ Id=3, Title="Черная пантера"},
            new Movie{ Id=4, Title="Дэдпул 2"}
         };

         string collectionResult = JsonConvert.SerializeObject(movies);
         Console.WriteLine(collectionResult);

         Console.WriteLine("\n2. Десериализация коллекции");
         // 2. Десериализация коллекции
         List<Movie> newMovies = JsonConvert.DeserializeObject<List<Movie>>(collectionResult);
         int i = 0;
         while (i < newMovies.Count)
         {
            Movie item = newMovies[i];
            Console.WriteLine("Id: " + item.Id + "; " + "Title: " + item.Title);
            i++;
         }
      }
   }

   public class EventLog
   {
      public string EventName { get; set; }
      public DateTime Timestamp { get; set; }
   }

   // Класс - Событие (время)
   public class Event
   {
      public string Name { get; set; }
      public DateTime Date { get; set; }
      public DateTimeOffset DateOffset { get; set; }
      public long TimeStamp { get; set; }
   }

   // Класс - Пользователь
   public class User
   {
      public string UserName { get; set; }
      public string Email { get; set; }
      public bool IsActive { get; set; }
      public List<string> Roles { get; set; } = new List<string>();
   }

   // Класс - Фильмы
   class Movie
   {
      public int Id { get; set; }
      public string Title { get; set; }
   }
}