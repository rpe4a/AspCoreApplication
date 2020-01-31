using EntitiesLib.Models;
using Microsoft.EntityFrameworkCore.Internal;

namespace EntitiesLib
{
    public class DbInitialize
    {
        public static void Initialize(AppDbContext context)
        {
            if (!context.Phones.Any())
            {
                context.Phones.AddRange(
                    new Phone
                    {
                        Name = "iPhone X",
                        Company = "Apple",
                        Price = 600
                    },
                    new Phone
                    {
                        Name = "Samsung Galaxy Edge",
                        Company = "Samsung",
                        Price = 550
                    },
                    new Phone
                    {
                        Name = "Pixel 3",
                        Company = "Google",
                        Price = 500
                    }
                );
            }

            if (!context.Users.Any())
            {
                context.Users.Add(new User { Name = "Tom", Age = 26 });
                context.Users.Add(new User { Name = "Alice", Age = 31 });
                context.Users.Add(new User { Name = "Vasya", Age = 32 });
                context.Users.Add(new User { Name = "Masha", Age = 11 });
            }

            context.SaveChanges();
        }
    }
}