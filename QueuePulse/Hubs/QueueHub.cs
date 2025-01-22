using Microsoft.AspNetCore.SignalR;

namespace Queuing_System.Hubs
{
	public class QueueHub: Hub
	{
		
		public async Task SendQueueUpdate(bool playSound,string queueNo,string currentWorkStation)
		{
			await Clients.All.SendAsync("ReceiveQueueUpdate",playSound, queueNo, currentWorkStation);
        }

	}
}
