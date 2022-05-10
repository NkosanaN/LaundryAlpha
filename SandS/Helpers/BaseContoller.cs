using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NToastNotify;
using Service.Repository.IRepository;

namespace SandS.Helpers
{
    public class BaseContoller : Controller
    {
        private readonly IUnityOfWork _unityofwork;
        private readonly IToastNotification toastNotification;
        public enum MessageTypes { mtInfo, mtWarning, mtError, mtAlert, mtSuccess }
        public enum NotificationType { error, success, warning }
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

        public BaseContoller(IUnityOfWork unityofwork, IToastNotification toaster)
        {
            _unityofwork = unityofwork;
            toastNotification = toaster;
        }
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
        public void AddToastMessage(string msg, MessageTypes type = 0)
        {
            var options = new ToastrOptions { TapToDismiss = true, CloseButton = true, TimeOut = 5000 };
            switch (type)
            {
                case MessageTypes.mtAlert:
                    toastNotification.AddAlertToastMessage(msg, options);
                    break;
                case MessageTypes.mtError:
                    options.TimeOut = 0;
                    toastNotification.AddErrorToastMessage(msg, options);
                    break;
                case MessageTypes.mtSuccess:
                    toastNotification.AddSuccessToastMessage(msg, options);
                    break;
                case MessageTypes.mtWarning:
                    options.TimeOut = 0;
                    toastNotification.AddWarningToastMessage(msg, options);
                    break;
                default:
                    toastNotification.AddInfoToastMessage(msg, options);
                    break;
            }
        }
    }
}
