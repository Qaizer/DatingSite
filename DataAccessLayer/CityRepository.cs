using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CityRepository
    {
        public List<City> GetAllCities()
        {
            var cities = new List<City>();
            using (var context = new OnlineDatingDBEntities())
            {
                cities.AddRange(context.City);
            }
            return cities;

        }
    }
}
