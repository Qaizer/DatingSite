using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CityRepository
    {
        public List<City> getAllCities()
        {
            List<City> cities = new List<City>();
            using (var context = new OnlineDatingDBEntities())
            {
                foreach(City c in context.City)
                {
                    cities.Add(c);
                }
            }
            return cities;

        }
    }
}
