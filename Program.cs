using Newtonsoft.Json;
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
         CaseFour();
        
         Console.ReadKey();
      }

      // Точное время в Unix‑timestamp в миллисекундах (13‑значное число)
      static void CaseFour()
      {
         Console.WriteLine("Определение точного времени в миллисекундах (13-значное число)");
         // Способ 1
         Console.WriteLine("========================================================");
         Console.WriteLine("Способ 1. DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()");
         Console.WriteLine("Текущее UTC время в миллисекундах: {0:yyyy-MM-dd HH:mm:ss.fff}", DateTime.UtcNow);
         long timestampone = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
         Console.WriteLine("Unix timestamp (ms): {0}", timestampone);

         // Способ 2
         Console.WriteLine("========================================================");
         Console.WriteLine("Способ 2. Ручной расчет через Ticks");
         Console.WriteLine("Текущее UTC время в миллисекундах: {0:yyyy-MM-dd HH:mm:ss.fff}", DateTime.UtcNow);
         DateTimeOffset datetimeoffset = DateTimeOffset.UtcNow;
         long timestamptwo = (datetimeoffset.Ticks - DateTimeOffset.UnixEpoch.Ticks) / TimeSpan.TicksPerMillisecond;
         Console.WriteLine("Unix timestamp (ms): {0}", timestamptwo);

         // Способ 3
         Console.WriteLine("========================================================");
         Console.WriteLine("Способ 3. new DateTimeOffset().ToUnixTimeMilliseconds()");
         Console.WriteLine("Текущее UTC время в миллисекундах: {0:yyyy-MM-dd HH:mm:ss.fff}", DateTime.UtcNow);
         DateTimeOffset specificdate = DateTimeOffset.UtcNow;
         long timestampthree = new DateTimeOffset(specificdate.UtcDateTime).ToUnixTimeMilliseconds();
         Console.WriteLine("Unix timestamp (ms): {0}", timestampthree);

         // Способ 4
         Console.WriteLine("========================================================");
         Console.WriteLine("Способ 4. DateTime.UtcNow и вычитание эпохи");
         Console.WriteLine("Текущее UTC время в миллисекундах: {0:yyyy-MM-dd HH:mm:ss.fff}", DateTime.UtcNow);
         DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
         DateTime thistime = DateTime.UtcNow;
         TimeSpan span = thistime - epoch;
         long timestampfour = (long)span.TotalMilliseconds;
         Console.WriteLine("Unix timestamp (ms): {0}", timestampfour);

         // Способ 5
         Console.WriteLine("========================================================");
         Console.WriteLine("Способ 5. DateTimeOffset с явным преобразованием");
         Console.WriteLine("Текущее UTC время в миллисекундах: {0:yyyy-MM-dd HH:mm:ss.fff}", DateTime.UtcNow);
         DateTimeOffset rightnow = DateTimeOffset.UtcNow;
         long timestampfive = rightnow.ToUnixTimeMilliseconds();
         Console.WriteLine("Unix timestamp (ms): {0}", timestampfive);

         // Проверка эквивалентности
         Console.WriteLine("========================================================");
         Console.WriteLine("Проверка эквивалентности:");
         Console.WriteLine("Способ 1 == Способ 2: {0}", timestampone == timestamptwo);
         Console.WriteLine("Способ 2 == Способ 3: {0}", timestamptwo == timestampthree);
         Console.WriteLine("Способ 3 == Способ 4: {0}", timestampthree == timestampfour);
         Console.WriteLine("Способ 4 == Способ 5: {0}", timestampfour == timestampfive);

         // Конвертация обратно для проверки
         Console.WriteLine("========================================================");
         Console.WriteLine("Конвертация обратно в DateTime:");
         DateTimeOffset datefromtimestamp = DateTimeOffset.FromUnixTimeMilliseconds(timestampfive);
         Console.WriteLine("Из timestamp: {0:yyyy-MM-dd HH:mm:ss.fff}", datefromtimestamp);
      }

      // Сериализация/десериализация точного времени в Unix‑timestamp в миллисекундах (13‑значное число)
      static void CaseThree()
      {
         Event eventItem = new Event
         {
            Name = "Структура DateTime",
            Date = DateTime.Now,
            DateUtc = DateTimeOffset.UtcNow,
            TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
         };

         // Форматированный JSON
         string json = JsonConvert.SerializeObject(eventItem, Formatting.Indented);
         Console.WriteLine("Сериализация с форматированием (читаемый JSON):");
         Console.WriteLine(json);

         // Десериализация
         Event deserializedEvent = JsonConvert.DeserializeObject<Event>(json);
         Console.WriteLine("\nДесериализованная дата: {0}", deserializedEvent.Date);

         if (deserializedEvent.Date.Kind == DateTimeKind.Local)
         {
            Console.WriteLine("Представленное время является местным");
         }

         if (deserializedEvent.Date.Kind == DateTimeKind.Unspecified)
         {
            Console.WriteLine("Представленное время не определено ни как местное, ни как время UTC");
         }

         if (deserializedEvent.Date.Kind == DateTimeKind.Utc)
         {
            Console.WriteLine("Представленное время является временем UTC");
         }
      }

      // Базовая сериализация/десериализация объекта
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

      // Сериализация с форматированием (читаемый JSON)
      static void CaseOne()
      {
         Console.WriteLine("1. Сериализация объекта");
         // 1. Сериализация
         Movie movie = new Movie { Id = 1, Title = "Миссия невыполнима" };
         // movie - объект, преобразуем его в строку с помощью JsonConvert.SerializeObject           
         string result = JsonConvert.SerializeObject(movie);
         // Преобразуется в строку в виде {"Id": 1,"Title": "Миссия невыполнима"}
         Console.WriteLine(result);

         Console.WriteLine("\n2. Десериализация объекта");
         // 2. Десериализация
         Movie newMovie = JsonConvert.DeserializeObject<Movie>(result);
         // Теперь строка преобразуется в объект           
         Console.WriteLine("Id: " + newMovie.Id);
         Console.WriteLine("Title: " + newMovie.Title);

         Console.WriteLine("\n3. Сериализация коллекции");
         // 3. Сериализация коллекции
         List<Movie> movies = new List<Movie>
         {
            new Movie{ Id=1, Title="Титаник" },
            new Movie{ Id=2, Title="Марсианин"},
            new Movie{ Id=3, Title="Черная пантера"},
            new Movie{ Id=4, Title="Дэдпул 2"}
         };

         string collectionResult = JsonConvert.SerializeObject(movies);
         Console.WriteLine(collectionResult);

         Console.WriteLine("\n4. Десериализация коллекции");
         // 4. Десериализация коллекции
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

   // Класс - Событие (время)
   public class Event
   {
      public string Name { get; set; }
      public DateTime Date { get; set; }
      public DateTimeOffset DateUtc { get; set; }
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