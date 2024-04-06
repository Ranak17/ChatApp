using ChatApp.WebApi.Models;
using System.Collections.Concurrent;

namespace ChatApp.WebApi.DataService
{
    public class SharedDB
    {
        public ConcurrentDictionary<string, UserConnection> Connections { get; } = new ConcurrentDictionary<string, UserConnection>();

        
    }
}
