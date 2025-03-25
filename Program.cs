using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDomashka
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1. Добавить пользователя");
                Console.WriteLine("2. Просмотреть всех пользователей");
                Console.WriteLine("3. Удалить пользователя");
                Console.WriteLine("4. Поиск пользователей по имени");
                Console.WriteLine("5. Выход");
                Console.Write("Выберите действие: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddUser();
                        break;
                    case "2":
                        ViewUsers();
                        break;
                    case "3":
                        DeleteUser();
                        break;
                    case "4":
                        SearchUsers();
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неверный ввод. Попробуйте снова.");
                        break;
                }
            }
        }

        static void AddUser()
        {
            Console.Write("Введите имя: ");
            var name = Console.ReadLine();

            Console.Write("Введите возраст: ");
            int age;
            while (!int.TryParse(Console.ReadLine(), out age))
            {
                Console.Write("Некорректный возраст. Введите снова: ");
            }

            Console.Write("Введите телефон: ");
            var phone = Console.ReadLine();

            Console.Write("Введите город: ");
            var city = Console.ReadLine();

            using (var context = new AppContextWPF())
            {
                var user = new User { Name = name, Age = age, Phone = phone, City = city };
                context.Users.Add(user);
                context.SaveChanges();
                Console.WriteLine("Пользователь добавлен успешно!");
            }
        }

        static void ViewUsers()
        {
            using (var context = new AppContextWPF())
            {
                var users = context.Users.ToList();

                if (users.Count == 0)
                {
                    Console.WriteLine("Нет пользователей в базе данных.");
                    return;
                }

                Console.WriteLine("\nСписок пользователей:");
                foreach (var user in users)
                {
                    Console.WriteLine($"ID: {user.Id}, Имя: {user.Name}, Возраст: {user.Age}, Телефон: {user.Phone}, Город: {user.City}");
                }
            }
        }

        static void DeleteUser()
        {
            ViewUsers();
            Console.Write("Введите ID пользователя для удаления: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                using (var context = new AppContextWPF())
                {
                    var user = context.Users.Find(id);
                    if (user != null)
                    {
                        context.Users.Remove(user);
                        context.SaveChanges();
                        Console.WriteLine("Пользователь удален успешно!");
                    }
                    else
                    {
                        Console.WriteLine("Пользователь с таким ID не найден.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Некорректный ID.");
            }
        }

        static void SearchUsers()
        {
            Console.Write("Введите имя для поиска: ");
            var searchName = Console.ReadLine();

            using (var context = new AppContextWPF())
            {
                var users = context.Users
                    .Where(u => u.Name.Contains(searchName))
                    .ToList();

                if (users.Count == 0)
                {
                    Console.WriteLine("Пользователи с таким именем не найдены.");
                    return;
                }

                Console.WriteLine("\nРезультаты поиска:");
                foreach (var user in users)
                {
                    Console.WriteLine($"ID: {user.Id}, Имя: {user.Name}, Возраст: {user.Age}, Телефон: {user.Phone}, Город: {user.City}");
                }
            }
        }
    }
}

