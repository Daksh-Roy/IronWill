using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IronWill.WebApi
{
	public class ApplicationDbContext : IdentityDbContext
    {
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options){}

        // Add DbSets for your entities
        //public DbSet<GymMember> GymMembers { get; set; }
    }
}

