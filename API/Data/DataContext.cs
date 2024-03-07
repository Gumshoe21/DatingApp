using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext : DbContext
{
	public DataContext(DbContextOptions options) : base(options) // our constructor will be empty as DbContext is what needs the options so we pass it them
	{
	}

	public DbSet<AppUser> Users { get; set; } // EF Core is very convention based - if we call our property 'Users' it will represent the name of the table in the database when it is created
}
