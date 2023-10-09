using Microsoft.EntityFrameworkCore;
using Planner.Db.Models;

namespace Planner.Db.Context;

public class PlannerDbContext : DbContext
{
	public PlannerDbContext(DbContextOptions<PlannerDbContext> options) : base(options)
	{
		
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		
		base.OnModelCreating(builder);
	}
	
	public DbSet<Attendee> Attendees { get; set; }
	
	public DbSet<AttendeeGroup> AttendeeGroups { get; set; }
	
	public DbSet<Seat> Seats { get; set; }

	public static PlannerDbContext Create()
	{
		return new PlannerDbContext(
			new DbContextOptionsBuilder<PlannerDbContext>()
				.UseSqlite("DataSource=app.db;Cache=Shared")
				.Options
		);
	}
}