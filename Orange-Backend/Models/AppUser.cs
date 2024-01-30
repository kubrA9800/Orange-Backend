using Microsoft.AspNetCore.Identity;

namespace Orange_Backend.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
