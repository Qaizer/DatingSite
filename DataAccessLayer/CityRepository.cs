using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CityRepository
    {
        //Returnerar lista över alla tillgängliga stadsval i City-tabellen
        public IEnumerable<City> GetAll()
        {
            using (var context = new OnlineDatingDBEntities())
            {
                return context.City.ToList();
            }
        }
    }
}
