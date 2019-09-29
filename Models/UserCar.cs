using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace user_parking_api.Models
{
    public class UserCar
    {
        public int IdUserCar { get; set; }
        public Car car { get; set; }
        public User user { get; set; }

        public UserCar()
        {
            List<UserCar> lista;
        }
    }
}
