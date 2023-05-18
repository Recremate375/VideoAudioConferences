using WPFClient.Model;
using System.Collections.Generic;

namespace DiplomClient.Model
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Email { get; set; }
        public byte[]? UserImage { get; set; }
        public List<Channel>? Channels { get; set; }
    }
}
