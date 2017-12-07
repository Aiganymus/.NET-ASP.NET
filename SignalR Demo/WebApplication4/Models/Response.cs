using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class Response
    {
        public string myId { get; set; }
        public User user;
        public List<Messages> messages;

        public Response(User user, List<Messages> messages)
        {
            this.user = user;
            this.messages = messages;
        }
    }
}