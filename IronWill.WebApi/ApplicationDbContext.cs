using System;
using IronWill.Models.Entities;
using IronWill.Models.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IronWill.WebApi
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options){}

        // Add DbSets for your entities
        //public DbSet<GymMember> GymMembers { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(u => u.Status).HasDefaultValue(UserStatus.Active);
            });
        }
    }
}

