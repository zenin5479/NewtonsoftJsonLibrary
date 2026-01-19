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
         string json = JsonConvert.SerializeObject(user);
         Console.WriteLine("1. Простая сериализация:");
         Console.WriteLine(json);
         // {"UserName":"Мишель Трахтенберг","Email":"michelletrachtenberg@example.com","IsActive":true,"Roles":["Actress","Producer"]}

         // 2. Сериализация с форматированием (читаемый JSON)
         string jsonFormatting = JsonConvert.SerializeObject(user, Formatting.Indented);
         Console.WriteLine("\n2. Сериализация с форматированием:");
         Console.WriteLine(jsonFormatting);

         // Десериализация полного объекта
         User deserializedUser = JsonConvert.DeserializeObject<User>(jsonFormatting);
         Console.WriteLine($"Имя пользователя: {deserializedUser.UserName}");
         Console.WriteLine($"Email: {deserializedUser.Email}");
         Console.WriteLine($"Активен: {deserializedUser.IsActive}");
         Console.WriteLine($"Роли: {string.Join(", ", deserializedUser.Roles)}");
      }

      // Базовая сериализация/десериализация
      static void CaseOne()
      {
         Console.WriteLine("Сериализация объекта");
         // 1. Сериализация
         Movie movie = new Movie { Id = 1, Title = "Миссия невыполнима" };
         // movie - объект, преобразуем его в строку с помощью JsonConvert.SerializeObject           
         string result = JsonConvert.SerializeObject(movie);
         // Преобразуется в строку в виде {"Id": 1,"Title": "Миссия невыполнима"}
         Console.WriteLine(result);

         Console.WriteLine("\nДесериализация объекта");
         // 2. Десериализация
         Movie newMovie = JsonConvert.DeserializeObject<Movie>(result);
         // Теперь строка преобразуется в объект           
         Console.WriteLine("Id: " + newMovie.Id);
         Console.WriteLine("Title: " + newMovie.Title);

         Console.WriteLine("\nСериализация коллекции");
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

         Console.WriteLine("\nДесериализация коллекции");
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

   // Простой класс
   public class User
   {
      public string UserName { get; set; }
      public string Email { get; set; }
      public bool IsActive { get; set; }
      public List<string> Roles { get; set; } = new List<string>();
   }

   class Movie
   {
      public int Id { get; set; }
      public string Title { get; set; }
   }
}