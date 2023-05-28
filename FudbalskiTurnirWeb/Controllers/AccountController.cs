using FudbalskiTurnirWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FudbalskiTurnirWeb.Controllers
{
    public class AccountController : Controller
    {

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public UserManager<IdentityUser> UserManager { get; }
        public SignInManager<IdentityUser> SignInManager { get; }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUser model)
        {
            if(ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.UserName };
                var result = await UserManager.CreateAsync(user, model.Password);

                if(result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "leaderboard");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUser model)
        {
            if (ModelState.IsValid)
            {
                var user1 = await UserManager.FindByNameAsync(model.UserName);
                var test = await SignInManager.UserManager.CheckPasswordAsync(user1, model.Password);

                if(test)
                {
                    await SignInManager.SignInAsync(user1, isPersistent: false);
                    return RedirectToAction("Index", "leaderboard");
                }
                //var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
               
                //if(result.Succeeded)
                //{
                //    return RedirectToAction("Index", "leaderboard");
                //}
               
                 ModelState.AddModelError(string.Empty, "User name or password incorrect");
               
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}
