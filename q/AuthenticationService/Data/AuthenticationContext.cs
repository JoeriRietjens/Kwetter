using System;
using AuthenticationService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Data
{
	public class AuthenticationContext : DbContext
	{
	#nullable disable
		public DbSet<User> Users { get; set; }

		public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options) { }

        public AuthenticationContext() { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			base.OnModelCreating(modelBuilder);
        }

	}
}

