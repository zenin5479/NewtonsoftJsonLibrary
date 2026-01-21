using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NewtonsoftJsonLibrary
{
   internal class Program
   {
      static void Main()
      {
         var eventItem = new Event
         {
            Name = "Конференция",
            Date = new DateTime(2024, 6, 15, 10, 0, 0, DateTimeKind.Utc),
            OptionalDate = null,
            DateWithOffset = new DateTimeOffset(2024, 6, 15, 10, 0, 0, TimeSpan.FromHours(3)),
            Duration = TimeSpan.FromHours(8)
         };

         // Форматированный JSON
         string json = JsonConvert.SerializeObject(eventItem, Formatting.Indented);
         Console.WriteLine("Сериализация с форматированием (читаемый JSON):");
         Console.WriteLine(json);
         /*
         {
           "Name": "Конференция",
           "Date": "2024-06-15T10:00:00Z",
           "OptionalDate": null,
           "DateWithOffset": "2024-06-15T10:00:00+03:00",
           "Duration": "08:00:00"
         }
         */

         // Десериализация
         Event deserializedEvent = JsonConvert.DeserializeObject<Event>(json);
         Console.WriteLine($"\nДесериализованная дата: {deserializedEvent.Date}");
         Console.WriteLine($"Kind: {deserializedEvent.Date.Kind}"); // Unspecified (по умолчанию)

         CaseOne();
         Console.WriteLine();
         CaseTwo();
         Console.ReadKey();
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

   public class Event
   {
      public string Name { get; set; }
      public DateTime Date { get; set; }
      public DateTime? OptionalDate { get; set; } // Nullable
      public DateTimeOffset DateWithOffset { get; set; }
      public TimeSpan Duration { get; set; }
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