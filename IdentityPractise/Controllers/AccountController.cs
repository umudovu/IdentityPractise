using IdentityPractise.Helpers;
using IdentityPractise.Models;
using IdentityPractise.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace IdentityPractise.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _rolemanager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> rolemanager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _rolemanager = rolemanager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = new AppUser
            {
                FullName = register.Fullname,
                UserName = register.Username,
                Email = register.Email
            };

             IdentityResult result= await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded){
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(String.Empty, item.Description);

                }
                return View(register);
            }

            await _userManager.AddToRoleAsync(user, UserRoles.Admin.ToString());

            return RedirectToAction("login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task< IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = await _userManager.FindByEmailAsync(login.Email);

            if (user==null)
            {
                ModelState.AddModelError("", "email or password is Invalid");
                return View(login);
            }

            SignInResult result = await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, true);

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "lockoutdur");
                return View(login);

            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "email or password is Invalid");
                return View(login);
            }

            await _signInManager.SignInAsync(user, login.RememberMe);

            return RedirectToAction("index","home");
        }

        public async Task<IActionResult>LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index","home");
        }
        public async Task CreateRole()
        {
            foreach (var item in Enum.GetValues(typeof(UserRoles)))
            {
                if (!await _rolemanager.RoleExistsAsync(item.ToString()))
                {
                    await _rolemanager.CreateAsync(new IdentityRole { Name = item.ToString() });
                }
            };
        }

    }
}
