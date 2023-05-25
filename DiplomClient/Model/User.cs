using WPFClient.Model;
using System.Collections.Generic;
using System.Drawing;

namespace DiplomClient.Model
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Email { get; set; }
        public byte[]? UserImage { get; set; }
        public Bitmap? userImage { get; set; }
        public List<Channel>? Channels { get; set; }
    }
}
