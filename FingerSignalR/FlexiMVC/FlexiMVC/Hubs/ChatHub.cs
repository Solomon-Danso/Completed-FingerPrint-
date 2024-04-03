using Microsoft.AspNetCore.SignalR;
using System;
using System.Net.Http;


namespace FlexiMVC.Hubs
{
    public class ChatHub:Hub
    {
          private static List<string> TotalUsers = new List<string>(); 
          
        
          



        public async Task SendMessageMainFunction(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

         public async Task SendMessageToConnectionId(string connectionId, string user, string message)
        {
           // await Clients.Client(connectionId).SendAsync("ReceiveMessage", user, message,base64String);
            await Clients.Client(connectionId).SendAsync("ReceiveMessage", user, message);
        }


    public async Task SendClientIdToHost(string connectionId)
        {
            await Clients.All.SendAsync("ReceiveClientId", connectionId);
        }








        public async Task SendApiLink(string connectionId, string ApiLink){

            await Clients.Client(connectionId).SendAsync("ApiLink", ApiLink);
        }





        public override Task OnConnectedAsync()
        {
            
            string connectionId = Context.ConnectionId;
            TotalUsers.Add(connectionId); // Add connection ID to the list



            
            Clients.All.SendAsync("ConnectionList", TotalUsers);
            Clients.Client(connectionId).SendAsync("ConnectionId", connectionId); 
           
            return base.OnConnectedAsync();
        }

    
        
      



  public override async Task OnDisconnectedAsync(Exception exception)
{
    string connectionId = Context.ConnectionId;
    TotalUsers.Remove(connectionId); // Remove connection ID from the list
    await Clients.All.SendAsync("ConnectionList", TotalUsers);

    // Make an API call to delete the user based on the connection ID
    try
    {
        using (var client = new HttpClient())
        {
            var apiUrl = "http://localhost:5286/api/Chat/DeleteOneUser?ConnectionId=" + connectionId;
            var response = await client.DeleteAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("API DELETE SUCCESS");
            }
            else
            {
                Console.WriteLine("API DELETE FAILED");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error calling API: " + ex.Message);
    }

    // Return a completed Task (no need for an object expression)
    return;
}



    }
}
