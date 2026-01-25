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
         Console.WriteLine();
         CaseFive();

         Console.ReadKey();
      }

      static void CaseFive()
      {
         Console.WriteLine("Текущее UTC время: {0:yyyy-MM-dd HH:mm:ss.fff}", DateTime.UtcNow);
         Console.WriteLine("==========================================");

         // Способ 1
         long timestampone = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
         Console.WriteLine("Способ 1 (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()):");
         Console.WriteLine("Результат: {0}", timestampone);
         Console.WriteLine("Длина: {0} цифр", timestampone.ToString().Length);
         Console.WriteLine("Формат: {0:#,##0}\n", timestampone);

         // Способ 2
         DateTimeOffset timeoffset = DateTimeOffset.UtcNow;
         long timestamptwo = (timeoffset.Ticks - DateTimeOffset.UnixEpoch.Ticks) / TimeSpan.TicksPerMillisecond;
         Console.WriteLine("Способ 2 (Ручной расчет через Ticks):");
         Console.WriteLine("Результат: {0}", timestamptwo);
         Console.WriteLine("Длина: {0} цифр", timestamptwo.ToString().Length);
         Console.WriteLine("Формат: {0:#,##0}\n", timestamptwo);

         // Способ 3
         DateTimeOffset specificdate = DateTimeOffset.UtcNow;
         long timestampthree = new DateTimeOffset(specificdate.UtcDateTime).ToUnixTimeMilliseconds();
         Console.WriteLine("Способ 3 (new DateTimeOffset().ToUnixTimeMilliseconds()):");
         Console.WriteLine("Результат: {0}", timestampthree);
         Console.WriteLine("Длина: {0} цифр", timestampthree.ToString().Length);
         Console.WriteLine("Формат: {0:#,##0}\n", timestampthree);

         // Способ 4
         DateTime timeextensions = DateTime.UtcNow;
         long timestampfour = DateTimeExtensions.ToUnixTimestampMilliseconds(timeextensions);
         Console.WriteLine("Способ 4 (Extension-метод):");
         Console.WriteLine("Результат: {0}", timestampfour);
         Console.WriteLine("Длина: {0} цифр", timestampfour.ToString().Length);
         Console.WriteLine("Формат: {0:#,##0}\n", timestampfour);

         // Проверка эквивалентности
         Console.WriteLine("Проверка эквивалентности:");
         Console.WriteLine("Способ 1 == Способ 2: {0}", timestampone == timestamptwo);
         Console.WriteLine("Способ 2 == Способ 3: {0}", timestamptwo == timestampthree);
         Console.WriteLine("Способ 3 == Способ 4: {0}", timestampthree == timestampfour);

         // Конвертация обратно для проверки
         Console.WriteLine("\nКонвертация обратно в DateTime:");
         DateTimeOffset datefromtimestamp = DateTimeOffset.FromUnixTimeMilliseconds(timestampone);
         Console.WriteLine("Из timestamp1: {0:yyyy-MM-dd HH:mm:ss.fff}", datefromtimestamp);
      }

      // Точное время в Unix‑timestamp в миллисекундах (13‑значное число)
      static void CaseFour()
      {
         Console.WriteLine("Точное время в Unix timestamp в миллисекундах (13-значное число)");

         // 1. Через DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
         Console.WriteLine("1. Через DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()");
         long timestampoffset = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
         Console.WriteLine("Unix timestamp (ms): {0}", timestampoffset);

         // 2. Через DateTime.UtcNow и вычитание эпохи
         Console.WriteLine("2. Через DateTime.UtcNow и вычитание эпохи");
         DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
         DateTime thistime = DateTime.UtcNow;
         TimeSpan span = thistime - epoch;
         long timestamputcnow = (long)span.TotalMilliseconds;
         Console.WriteLine("Unix timestamp (ms): {0}", timestamputcnow);

         // 3. Через DateTimeOffset с явным преобразованием
         Console.WriteLine("3. Через DateTimeOffset с явным преобразованием");
         DateTimeOffset rightnow = DateTimeOffset.UtcNow;
         long timestampoffsetconvert = rightnow.ToUnixTimeMilliseconds();
         Console.WriteLine("Unix timestamp (ms): {0}", timestampoffsetconvert);
      }

      // Сериализация/десериализация точного времени в Unix‑timestamp в миллисекундах (13‑значное число)
      static void CaseThree()
      {
         Event eventItem = new Event
         {
            Name = "Структура DateTime",
            Date = DateTime.Now,
            DateUtc = DateTime.UtcNow,
            DateToday = DateTime.Today,
            TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
         };

         // Форматированный JSON
         string json = JsonConvert.SerializeObject(eventItem, Formatting.Indented);
         Console.WriteLine("Сериализация с форматированием (читаемый JSON):");
         Console.WriteLine(json);

         // Десериализация
         Event deserializedEvent = JsonConvert.DeserializeObject<Event>(json);
         Console.WriteLine("\nДесериализованная дата: {0}", deserializedEvent.Date);
         // Unspecified (по умолчанию)

         // Имя         Значение 	Описание
         // Local          2        Представленное время является местным
         // Unspecified    0 	      Представленное время не определено ни как местное, ни как время UTC
         // Utc            1        Представленное время является временем UTC

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
            Console.WriteLine("Представленное время является местным");
         }

         Console.WriteLine("Kind: {0}", deserializedEvent.Date.Kind);
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

   public static class DateTimeExtensions
   {
      public static long ToUnixTimestampMilliseconds(DateTime dateTime)
      {
         return new DateTimeOffset(dateTime).ToUnixTimeMilliseconds();
      }
   }

   // Класс - Событие (время)
   public class Event
   {
      public string Name { get; set; }
      public DateTime Date { get; set; }
      public DateTime DateUtc { get; set; }
      public DateTime DateToday { get; set; }
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