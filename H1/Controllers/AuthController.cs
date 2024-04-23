using H1.Models;
using H1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace H1.Controllers
{
    public class AuthController : Controller
    {
        private readonly MongoService _mongoService;
        public AuthController(MongoService mongoService)
        {
            _mongoService = mongoService;
        }
        public class LoginModel
        {
            public string EmailId { get; set; }
            public string Password { get; set; }
            public string PhoneNumber { get; set; }
        
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Age { get; set; }
            public string Gender { get; set; }
            public string Height { get; set; }
            public string Weight { get; set; }
            public string Goal { get; set; }
            public string Level { get; set; }
            public string Category { get; set; }
        }
        [Route("Signin")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            try
            {
                if (loginModel != null)
                {
                    var userData = _mongoService.GetAsync();
                    var existingUserData = userData.Result.Where(w => w.Username == loginModel.EmailId && w.Password == loginModel.Password).FirstOrDefault();
                    if (existingUserData == null)
                        return BadRequest("user does not exists");
                    else
                        return Ok(existingUserData);
                }
                else
                    return BadRequest("login model is not correct");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [Route("Signup")]
        [HttpPost]
        public async Task<IActionResult> Signup([FromBody] LoginModel model)
        {
            try
            {
                if (model != null)
                {
                    var user = new User
                    {
                        Username = model.EmailId,
                        Password = model.Password,
                        PhoneNumber = model.PhoneNumber
                    };

                    await _mongoService.CreateAsync(user);
                }
                else
                    return BadRequest("login model is not correct");
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [Route("SignupPage")]
        [HttpPost]
        public async Task<IActionResult> SignupPage([FromBody] LoginModel model)
        {
            try
            {
                if (model != null)
                {
                    var user = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        EmailId = model.EmailId,
                        PhoneNumber = model.PhoneNumber,
                        Age = model.Age,
                        Gender = model.Gender,
                        Height = model.Height,
                        Weight = model.Weight,
                        Goal = model.Goal,
                        Level = model.Level,
                        Category = model.Category
                    };
        
                    await _mongoService.CreateAsync(user);
                }
                else
                    return BadRequest("login model is not correct");
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
