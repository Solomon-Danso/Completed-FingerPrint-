using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnderlyingApi.Models
{
    public class SignalUser
    {
        public int Id {get;set;}
        public string? UserId {get;set;}
        public string? ConnectionId {get;set;}
    }
}