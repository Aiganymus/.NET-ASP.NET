using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class GroupResponse
    {
        public string myId { get; set; }
        public Group group;
        public List<Messages> messages;

        public GroupResponse(Group group, List<Messages> messages)
        {
            this.group = group;
            this.messages = messages;
        }
    }
}