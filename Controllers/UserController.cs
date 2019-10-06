using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using user_parking_api.Models;
using user_parking_api.Persistence;
using user_parking_api.Utils;

namespace user_parking_api.Controllers
{
        


    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
       
        private readonly Tools _tools;
        bool onlyOneMoockData = true;

        private readonly DataContext _dataContext;

       

        public UserController(DataContext context)
        {         
            _dataContext = context;

            //if (onlyOneMoockData) {
             moockInitData();
            //}
        }

        /// <summary>
        /// </summary>
        /// <returns>List<UserCar></returns>
        // GET api/user
        [HttpGet]
        public IActionResult GetUsers()
        {
            List<User> users = _dataContext.users.ToList();
            List<Car> cars = _dataContext.cars.ToList();

            //UserCar userWithCar = new UserCar { };
            List<UserCar> userWithCarsList = _dataContext.userscars.ToList();

            //users.ForEach(u => 
            //{
            //    userWithCar.IdUserCar = u.Id;
            //    userWithCar.user = u;
            //    userWithCarsList.Add(userWithCar);
            //});

            //cars.ForEach(c => 
            //    {
            //        if (c.IdCar == userWithCar.IdUserCar)
            //        {
            //            userWithCar.car = c;
            //        }
            //    });

            //userWithCarsList.Add(userWithCar);
          
            return Ok(userWithCarsList);
        }

        /// <summary>
        /// </summary>
        /// <param id="id"></param>
        /// <returns>UserCar</returns>
        // GET api/user/5
        [HttpGet("{id}")]
        public IActionResult getUser(int id)
        {
            var userParking = _dataContext.users.FirstOrDefault(u => u.Id == id);
            var carParking = _dataContext.cars.FirstOrDefault(c => c.IdCar == userParking.Id);
            var userWithCar = new UserCar {
                    IdUserCar = userParking.Id,
                    user = userParking,
                    car = carParking
                    };

            if (userWithCar == null)
            {
                return NotFound();
            }
            return Ok(userWithCar);
        }

        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        /// <returns>UserCar</returns>
        [HttpPost("name")]
        public IActionResult getUserName([FromBody]string name)
        {
            if (!String.IsNullOrEmpty(name))
            {
                var userParking = _dataContext.users.FirstOrDefault(u => u.Name == name);
                var carParking = _dataContext.cars.FirstOrDefault(c => c.IdCar == userParking.Id);
                var userWithCar = new UserCar
                {
                    IdUserCar = userParking.Id,
                    user = userParking,
                    car = carParking
                };

                if (userWithCar == null)
                {
                    return NotFound();
                }

                return Ok(userWithCar);
            }

            return NoContent();
        }

        /// <summary>
        /// </summary>
        /// <param name="email"></param>
        /// <returns>UserCar</returns>
        [HttpPost("email")]
        public  IActionResult getUserEmail([FromBody]string email)
        {
            if (!String.IsNullOrEmpty(email))
            {
               
                var userParking = _dataContext.users.FirstOrDefault(u => u.Email == email);
                if (userParking != null)
                {

                    var carParking = _dataContext.cars.FirstOrDefault(c => c.IdCar == userParking.Id);

                    var userWithCar = new UserCar
                    {
                        IdUserCar = userParking.Id,
                        user = userParking,
                        car = carParking
                    };

                    if (userWithCar == null)
                    {
                        return NotFound();
                    }

                    _dataContext.users.Add(userParking);
                    _dataContext.cars.Add(carParking);
                    _dataContext.userscars.Add(userWithCar);

                     _dataContext.SaveChangesAsync();

                    return Ok(userWithCar);
                }
                else {

                    var userParkingNew = new User {
                        Name= "",
                        Surname = "",
                        Email= email,
                        Id = 1,
                        Telephone = ""
                    };
                    var carParkingNew = new Car {
                        IdCar = 1,
                        LicencePlate = "",
                        Model = ""
                    };
                    var userWithCarNew = new UserCar {
                            IdUserCar = 1,
                            car = carParkingNew,
                            user = userParkingNew
                    };

                    _dataContext.users.Add(userParkingNew);
                    _dataContext.cars.Add(carParkingNew);
                    _dataContext.userscars.Add(userWithCarNew);

                     _dataContext.SaveChangesAsync();

                    return Ok(userWithCarNew);
                }
            }

            return NoContent();
        }

        /// <summary>
        /// </summary>
        /// <param UserCar></param>
        // POST: api/user
        [HttpPost]
        public async Task<ActionResult> PostUserWithCar([FromBody]UserCar userCar)
        {
            if (userCar != null)
            {
                 

                User user = new User
                {
                    Name = userCar.user.Name,
                    Surname = userCar.user.Surname,
                    Email = userCar.user.Email,
                    Telephone = userCar.user.Telephone
                };
                Car car = new Car
                {
                    Model = userCar.car.Model,
                    LicencePlate = userCar.car.LicencePlate
                };

                UserCar userCarData = new UserCar {
                    IdUserCar = user.Id,
                    car = car,
                    user = user
                };
                _dataContext.users.Add(user);
                _dataContext.cars.Add(car);
                _dataContext.userscars.Add(userCarData);

                await _dataContext.SaveChangesAsync();

                //return CreatedAtAction("user", new { id = user.Id }, user);
                return Ok();

            }
            else {
                return StatusCode(400);

            }


        }

        // PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //    Console.Write(" put ");
        //}

        /**/
        
        public void moockInitData() {
            Random random = new Random();
            var tel=  random.Next(1000000, 9999999);
            var matricula = random.Next(1000, 9999);
            var emailNum = random.Next(1, 99);
            var  user = new User
            {
                Name = "Pepe",
                Surname = "Bueno",
                Email = "pepe" + emailNum.ToString() + "@mail.com",
                Telephone = tel.ToString()
            };
            var car = new Car
            {
                Model = "Ford T",
                LicencePlate = matricula.ToString()
            };

            _dataContext.Add(user);
            _dataContext.Add(car);
             _dataContext.SaveChangesAsync();
            onlyOneMoockData = false;
        }

    }
}