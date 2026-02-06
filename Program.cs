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

         Console.ReadKey();
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
         Console.WriteLine("\n2. Сериализация с форматированием (читаемый JSON):");
         Console.WriteLine(jsonserializeformatting);

         // 3. Десериализация объекта
         Console.WriteLine("\n3. Десериализация объекта");
         User jsondeserialized = JsonConvert.DeserializeObject<User>(jsonserializeformatting);
         Console.WriteLine("Имя пользователя: {0}", jsondeserialized.UserName);
         Console.WriteLine("Email: {0}", jsondeserialized.Email);
         Console.WriteLine("Активен: {0}", jsondeserialized.IsActive);
         Console.WriteLine("Роли: {0}", string.Join(", ", jsondeserialized.Roles));
      }

      // Сериализация/десериализация точного времени в Unix‑timestamp в миллисекундах (13‑значное число)
      static void CaseThree()
      {
         Event log = new Event
         {
            Date = DateTimeOffset.UtcNow,
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
         };

         // Настройка формата даты с помощью JsonSerializerSettings
         Console.WriteLine("1. Cериализация. Настройка формата даты с помощью JsonSerializerSettings:");
         JsonSerializerSettings customformat = new JsonSerializerSettings
         {
            DateFormatString = "dd.MM.yyyy HH:mm:ss.fff"
         };

         string jsoncustom = JsonConvert.SerializeObject(log, customformat);
         Console.WriteLine(jsoncustom);

         Event deserializedevent = JsonConvert.DeserializeObject<Event>(jsoncustom, customformat);
         Console.WriteLine("2. Десериализованная дата: {0}", deserializedevent.Date);
         Console.WriteLine("3. Время (в формате строки): {0}", deserializedevent.Date.ToString("dd.MM.yyyy HH:mm:ss.fff"));
         Console.WriteLine("4. Unix timestamp (ms): {0}", deserializedevent.Timestamp);

         // Настройка формата даты с помощью IsoDateTimeConverter
         Console.WriteLine("1. Cериализация. Настройка формата даты с помощью IsoDateTimeConverter:");
         JsonSerializerSettings customsettings = new JsonSerializerSettings
         {
            Converters = { new IsoDateTimeConverter { DateTimeFormat = "dd.MM.yyyy HH:mm:ss.fff" } }
         };

         string jsonsettings = JsonConvert.SerializeObject(log, customsettings);
         Console.WriteLine(jsonsettings);
         Event deserializedeven = JsonConvert.DeserializeObject<Event>(jsonsettings, customsettings);
         Console.WriteLine("2. Десериализованная дата: {0}", deserializedeven.Date);
         Console.WriteLine("3. Время (в формате строки): {0}", deserializedeven.Date.ToString("dd.MM.yyyy HH:mm:ss.fff"));
         Console.WriteLine("4. Unix timestamp (ms): {0}", deserializedeven.Timestamp);
      }
   }

   // Класс - Событие
   class Event
   {
      public DateTimeOffset Date { get; set; }
      public long Timestamp { get; set; }
   }

   // Класс - Пользователь
   class User
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