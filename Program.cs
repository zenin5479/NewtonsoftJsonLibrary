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
         Movie movie = new Movie(1, "Миссия невыполнима");
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
            new Movie(1, title: "Титаник"),
            new Movie(2, title: "Марсианин"),
            new Movie(3, title : "Черная пантера"),
            new Movie(4, title : "Дэдпул 2")
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
      public Movie()
      {
      }

      public Movie(int id, string title)
      {
         Id = id;
         Title = title;
      }

      public int Id { get; set; }
      public string Title { get; set; }
   }
}