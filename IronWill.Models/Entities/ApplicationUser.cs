using IronWill.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace IronWill.Models.Entities
{
	public class ApplicationUser : IdentityUser
    {
        public ApplicationUser(){}

        //public Guid UserId { get; set; } = Guid.NewGuid();

        public string FullName { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public string? Address { get; set; }

        public DateTime JoinDate { get; set; } = DateTime.UtcNow;

        public UserStatus Status { get; set; } = UserStatus.Active;
    }
}

