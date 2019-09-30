using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user_parking_api.Models;

namespace user_parking_api.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {


        }

        public DbSet<User> users { get; set; }
        public DbSet<Car> cars { get; set; }
        //public DbSet<UserCar> userscars { get; set; }

        /*
         var newBrand = new CatalogBrand() { Brand = "Acme" };
        _context.Add(newBrand);
        await _context.SaveChangesAsync();*/

    }
}
