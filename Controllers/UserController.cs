using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using user_parking_api.Models;
using user_parking_api.Persistence;

namespace user_parking_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public UserController(DataContext context)
        {         
            _dataContext = context;      
        }

        // GET api/user
        [HttpGet]
        public IActionResult GetUsers()
        {
            List<User> users = _dataContext.users.ToList();
            List<Car> cars = _dataContext.cars.ToList();

            UserCar userWithCar = new UserCar { };
            List<UserCar> userWithCarsList = new List<UserCar>();

            users.ForEach(u => 
            {
                userWithCar.IdUserCar = u.Id;
                userWithCar.user = u;

                cars.ForEach(c => 
                {
                    if (c.IdCar == u.Id)
                    {
                        userWithCar.car = c;
                    }
                });

                userWithCarsList.Add(userWithCar);
            });
          
            return Ok(userWithCar);
        }

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

        // POST: api/user
        [HttpPost]
        public async Task<ActionResult> PostUserWithCar([FromBody]UserCar userCar)
        {
            if (userCar == null)
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

                _dataContext.users.Add(user);
                _dataContext.cars.Add(car);

                await _dataContext.SaveChangesAsync();

                //return CreatedAtAction("user", new { id = user.Id }, user);
                return Ok();

            }
            else {
                return StatusCode(400);

            }


        }
/*
        [HttpPost("promotionalcode")]
        public async Task<ActionResult<ResponseOk<bool>>> SendCouponNotifications(
                    string portalCountryCode, string userCountryCode, 
                    [FromBody]ContactNotificationDto contactNotificationDto)
        {
            ResponseOk<bool> response = new ResponseOk<bool>
            {
                Data = await _notificationService.SendCouponNotifications(portalCountryCode, userCountryCode, contactNotificationDto)
            };

            return Ok(response);
        }
*/

    }
}