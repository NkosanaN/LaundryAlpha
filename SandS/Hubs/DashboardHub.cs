using Microsoft.AspNetCore.SignalR;
using Service.Data;
using Service.Repository.IRepository;

namespace S_and_S.Hubs
{
    public class DashboardHub:Hub
    {
        private readonly IUnityOfWork _unityofwork;
        //public DashboardHub(IUnityOfWork unityofwork)
        //{
        //    _unityofwork = unityofwork;
        //}

        public async Task SendPendingLaundry() 
        {
            //var list14 = _db.OrderHeader.ToList
            var list = _unityofwork.OrderHeader.GetAll();
            await Clients.All.SendAsync("ReceivedPending", list);
        }
    }
}
