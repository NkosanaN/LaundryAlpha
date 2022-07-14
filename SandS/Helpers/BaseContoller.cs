using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model;
using Newtonsoft.Json;
using NToastNotify;
using Service.Repository.IRepository;
using System.Security.Claims;

namespace SandS.Helpers
{
    public class BaseContoller : Controller
    {
        private ApplicationUser _getsignuser { get; set; }
        private readonly UserManager<ApplicationUser> _signInManager;
        private readonly IUnityOfWork _unityofwork;
        public enum MessageTypes { mtInfo, mtWarning, mtError, mtAlert, mtSuccess }
        public enum NotificationType { error, success, warning , info }
  
        public SelectList ServicesGet()
        {
            var stypes = new List<string>
            {
                "Laundry",
                "HouseHolds",
                "Comforters"
            };
            return new SelectList(stypes);
        }

        public BaseContoller(IUnityOfWork unityofwork, UserManager<ApplicationUser> signInManager)
        {
            _unityofwork = unityofwork;
            _signInManager = signInManager;
        }
        public ApplicationUser GetSignUser
        {
            get
            {
                if (_getsignuser == null)
                {
                    ApplicationUser applicationUser = (ApplicationUser)_signInManager.GetUserAsync(User).Result;
                    _getsignuser = applicationUser;
                }
                return _getsignuser;
            }
        }
        public string FirstName => GetSignUser.FirstName;
        public string LastName => GetSignUser.LastName;
        public string Email => GetSignUser.Email;
        public string PhoneNumber => GetSignUser.PhoneNumber;
        public string StreetAddress => GetSignUser.StreetAddress;

        public void Notify(string msg, 
                           string title = "Sweet Alert Toastr Demo",
                           NotificationType type = NotificationType.success)
        {
            var ms = new
            {
                msg = msg,
                title = title,
                icon = type.ToString(),
                type = type.ToString(),
                provider = GetProvider()
            };
            TempData["Message"] = JsonConvert.SerializeObject(ms);
        }

        public string GetProvider()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            IConfiguration configuration = builder.Build();
            var value = configuration["NotificationProvider"];
            return value;
        }
    }
}
