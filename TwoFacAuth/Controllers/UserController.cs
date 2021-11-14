using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TwoFacAuth.Areas.Identity.Data;
using TwoFacAuth.Model;

namespace TwoFacAuth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]    
    public class UserController : ControllerBase
    {
        private readonly UserManager<TwoFacAuthUser> _userManager;
        private readonly SignInManager<TwoFacAuthUser> _signInManager;
        public UserController(UserManager<TwoFacAuthUser> userManager, SignInManager<TwoFacAuthUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserDto model)
        {
            Response<UserDto> serviceResponse = new Response<UserDto>();
            serviceResponse.Message = "Failed to create user!";
            if (!string.IsNullOrEmpty(model.Email))
            {                
                if (!string.IsNullOrEmpty(model.Password) && model.Password == model.ConfirmPassword)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (user == null)
                    {
                        var user1 = new TwoFacAuthUser { UserName = model.Email, Email = model.Email, EmailConfirmed = true };
                        var result = await _userManager.CreateAsync(user1, model.Password);
                        if (result.Succeeded)
                        {
                            serviceResponse.Message = "User Created Succefully!";
                        }
                    }
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Invalid Request!";
                }
            }

            return Ok(serviceResponse);
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto model)
        {
            Response<UserDto> serviceResponse = new Response<UserDto>();
            serviceResponse.Message = "Failed to Login!";

            if (!string.IsNullOrEmpty(model.Email) && !string.IsNullOrEmpty(model.Password))
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        serviceResponse.Message = "Login Successfully!";
                    }
                    if (result.RequiresTwoFactor)
                    {
                        serviceResponse.Message = "ENABLE_TWO_FACTOR";
                    }
                    if (!result.Succeeded)
                    {
                        serviceResponse.Message = "User or Password Mismatch Found!";
                    }
                }
                else
                {                    
                    serviceResponse.Message = "User Not Found!";
                }
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Invalid Request!";
            }            

            return Ok(serviceResponse);
        }
    }
}
