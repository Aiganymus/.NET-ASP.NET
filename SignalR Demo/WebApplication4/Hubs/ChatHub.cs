using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Configuration;
using System.Data.SqlClient;
using WebApplication4.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Concurrent;
using System.Security.Principal;
using System.Text.RegularExpressions;

namespace WebApplication4.Hubs
{
    public class ChatHub : Hub
    {
        public static Dictionary<String, String> users = new Dictionary<String, String>();
        ChatEntities _db;

        public ChatHub()
        {
            _db = new ChatEntities();   
        }

        public void Send(string fromUsername, string toUsername, string toId, string message)
        {
            string UserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            Regex reg = new Regex("[a-zA-Z]+");
            string userName = Context.User.Identity.Name;
            if (reg.IsMatch(toId))
            {                
                if (users.Keys.Contains(toUsername))
                {
                    var s = users[toUsername];
                    Clients.Client(users[toUsername]).addNewMessage(userName, message, toUsername);
                }
                Clients.Client(Context.ConnectionId).addNewMessage(userName, message, toUsername);
                _db.Messages.Add(new Messages
                {
                    Id = _db.Messages.Count() + 1,
                    From = UserId,
                    MessageText = message,
                    Date = DateTime.Now,
                    To = toId,
                    FromName = fromUsername
                });
                _db.SaveChanges();
            } else
            {
                Clients.Group(toUsername).addNewMessage(userName, message);
                _db.Messages.Add(new Messages
                {
                    Id = _db.Messages.Count() + 1,
                    From = UserId,
                    MessageText = message,
                    Date = DateTime.Now,
                    GroupID = Int32.Parse(toId),
                    FromName = fromUsername
                });
                _db.SaveChanges();
            }
        }

        public override Task OnConnected()
        {
            string userName = Context.User.Identity.Name;
            if (!users.Keys.Contains(userName))
                users.Add(userName, Context.ConnectionId);
            else
                users[userName] = Context.ConnectionId;
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            //удалить юезра из словаря
            
            List<String> itemsToRemove = new List<String>();

            foreach (var pair in users)
            {
                if (pair.Value.Equals(Context.ConnectionId))
                    itemsToRemove.Add(pair.Key);
            }

            foreach (String item in itemsToRemove)
            {
                users.Remove(item);
            }
            return base.OnDisconnected(stopCalled);
        }


        public override Task OnReconnected()
        {
            //users.Remove(Context.ConnectionId);
            //Groups.Remove(Context.ConnectionId, "Group1");

            return base.OnReconnected();
        }

        public void JoinRoom(string roomName)
        {
            Groups.Add(Context.ConnectionId, roomName);
        }

        public void LeaveRoom(string roomName)
        {
            Groups.Remove(Context.ConnectionId, roomName);
        }
    }
}