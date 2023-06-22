using InformationServer.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationServer.Data
{
	public class InformationDbContext : DbContext
	{
		public InformationDbContext(DbContextOptions options) : base(options) { }

		public DbSet<User> Users { get; set; }
		public DbSet<Channel> Channels { get; set; }
		public DbSet<Message> Messages { get; set; }
	}
}
