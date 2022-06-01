using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using Service;
using Service.Data;
using Service.Repository.IRepository;

namespace S_and_S.Controllers
{
    public class UserAuthController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;
        public UserAuthController(ApplicationDbContext context,
                                  UserManager<IdentityUser> userManager,
                                  SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            loginModel.LoginInValid = "true";


            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginModel.Email,
                                         loginModel.Password,
                                         loginModel.RememberMe,
                                         lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    loginModel.LoginInValid = "false";
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt");
                }

            }
            ViewBag.LoginInValid = loginModel.LoginInValid;
            return PartialView("_UserLoginPartial", loginModel);

        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();

            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser(RegistrationModel registrationModel)
        {
            registrationModel.RegistrationInValid = "true";

            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = registrationModel.Email,
                    Email = registrationModel.Email,
                    FirstName = registrationModel.FirstName, 
                    LastName = registrationModel.LastName,
                    PhoneNumber = registrationModel.PhoneNumber,
                    Name = registrationModel.Name,
                    PostalCode = registrationModel.PostalCode,
                    StreetAddress = registrationModel.StreetAddress,
                    City = registrationModel.City
                };

                var result = await _userManager.CreateAsync(user, registrationModel.Password);

                if (result.Succeeded)
                {
                    registrationModel.RegistrationInValid = "";

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    if (string.IsNullOrEmpty(registrationModel.Role))
                    {
                        await _userManager.AddToRoleAsync(user, UtilityConstant.Role_User_Ind);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, registrationModel.Role);
                    }
                    return PartialView("_UserRegistrationPartial", registrationModel);
                }

                AddErrorsToModelState(result);

            }
            ViewBag.LoginInValid = registrationModel.RegistrationInValid;
            return PartialView("_UserRegistrationPartial", registrationModel);

        }
        [AllowAnonymous]
        public async Task<bool> UserNameExists(string userName)
        {
            bool userNameExists = await _context.Users.AnyAsync(u => u.UserName.ToLower() == userName.ToLower());

            if (userNameExists)
                return true;

            return false;

        }
        private void AddErrorsToModelState(IdentityResult result)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
        }
    }
}
