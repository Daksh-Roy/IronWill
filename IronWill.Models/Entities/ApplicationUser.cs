using IronWill.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace IronWill.Models.Entities
{
	public class ApplicationUser : IdentityUser
    {
        public ApplicationUser(){}

        //public Guid UserId { get; set; } = Guid.NewGuid();

        public DateTime JoinDate { get; set; } = DateTime.UtcNow;

        public UserStatus Status { get; set; } = UserStatus.Active;
    }
}

