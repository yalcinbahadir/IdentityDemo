using IdentityDemo.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using Blazored.LocalStorage;

namespace IdentityDemo.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class CookieController : ControllerBase
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private ISessionStorageService _sessionStorage;
        private ILocalStorageService _locakStorage;
        public CookieController(UserManager<IdentityUser> userManager, 
                                SignInManager<IdentityUser> signInManager, 
                                ISessionStorageService sessionStorage, ILocalStorageService locakStorage)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _sessionStorage = sessionStorage;
            _locakStorage = locakStorage;


        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm]LoginModel model)
        {

            string errorMessage = null;
            var existing = await _userManager.FindByNameAsync(model.Email);
            if (existing != null)
            {
                var result = await _signInManager.PasswordSignInAsync(existing, model.Password, false, false);
                if (result.Succeeded)
                {
                    return Redirect("/");
                } 
                else
                {
                    errorMessage += "Invalid login attempt.";
                }
               
            } 
            else 
            {
                errorMessage += "User does not exists.";
               
            }


            // await _sessionStorage.SetItemAsync("errorMessage", errorMessage);
            //await _locakStorage.SetItemAsync("errorMessage", errorMessage);
            Messanger.Message = errorMessage;
            return Redirect("/login");
        }
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
           await _signInManager.SignOutAsync();
            return Redirect("/");
        }


    }
}
