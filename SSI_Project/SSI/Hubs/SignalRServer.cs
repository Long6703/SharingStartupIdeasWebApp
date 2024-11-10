using Microsoft.AspNetCore.SignalR;

namespace SSI.Hubs
{
    public class SignalRServer : Hub
    {
        public void RefreshData()
        {
            Clients.All.SendAsync("ReloadData");
        }
    }
}
