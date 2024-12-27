using System;
using Microsoft.EntityFrameworkCore;

namespace IronWill.WebApi
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options){}

        // Add DbSets for your entities
        //public DbSet<GymMember> GymMembers { get; set; }
    }
}

