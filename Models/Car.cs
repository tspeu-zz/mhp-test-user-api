using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace user_parking_api.Models
{
    public class Car
    {
        [Key]
        public int IdCar { get; set; }
        public string Model { get; set; }
        public string LicencePlate { get; set; } 
    }
}

