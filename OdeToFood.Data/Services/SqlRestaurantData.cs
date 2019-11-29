using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OdeToFood.Data.Models;

namespace OdeToFood.Data.Services
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext dbContext;

        public SqlRestaurantData(OdeToFoodDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        public void Add(Restaurant restaurant)
        {
            dbContext.Restaurants.Add(restaurant);
            dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var restaurant = dbContext.Restaurants.Find(id);
            if (restaurant != null)
            {
                dbContext.Restaurants.Remove(restaurant);
                dbContext.SaveChanges();
            }
        }

        public Restaurant Get(int id)
        {
            return dbContext.Restaurants.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return from r in dbContext.Restaurants
                   orderby r.Name
                   select r;
        }

        public void Update(Restaurant restaurant)
        {
            // Saves the last user update
            //var r = Get(restaurant.Id);
            //r.Name = restaurant.Name;
            //r.Cuisine = restaurant.Cuisine;
            //dbContext.SaveChanges();

            // Optimistic cuncurrency 
            var entry = dbContext.Entry(restaurant);
            entry.State = EntityState.Modified;
            dbContext.SaveChanges();
        }
    }
}
