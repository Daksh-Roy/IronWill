using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IronWill.Models.Models;
using IronWill.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IronWill.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        // api/auth/register
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(registerModel);
                if (result.isSuccess)
                {
                    return Ok(result); // status code : 200
                }

                return BadRequest(result);
            }
            return BadRequest("Some properties are not valid"); // status code : 400
        }

        // api/auth/login
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUserAsync(loginModel);
                if (result.isSuccess)
                {
                    return Ok(result); // status code : 200
                }

                return Unauthorized(result); // status code : 401
            }

            return BadRequest("Some properties are not valid"); // status code : 400
        }

        // api/auth/forgot-password
        [HttpPost("Forgot-Password")]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordModel forgotPasswordModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ForgotPasswordAsync(forgotPasswordModel);
                if (result.isSuccess)
                {
                    return Ok(result); // status code : 200
                }

                return BadRequest(result); // status code : 400
            }

            return BadRequest("Some properties are not valid"); // status code : 400
        }

        // api/auth/reset-password
        [HttpPost("Reset-Password")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordModel resetPasswordModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ResetPasswordAsync(resetPasswordModel);
                if (result.isSuccess)
                {
                    return Ok(result); // status code : 200
                }
                return BadRequest(result);
            }
            return BadRequest("Some properties are not valid");
        }


        //// GET: api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}

