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
		builder.Entity<Event>().Navigation(x => x.Attendees).AutoInclude();
		builder.Entity<Event>().Navigation(x => x.SeatingRows).AutoInclude();
		base.OnModelCreating(builder);
	}
	
	public DbSet<Event> Events { get; set; }
	
	public DbSet<Attendee> Attendees { get; set; }
	
	public DbSet<SeatingRow> Rows { get; set; }

	public static PlannerDbContext Create()
	{
		return new PlannerDbContext(
			new DbContextOptionsBuilder<PlannerDbContext>()
				.UseSqlite("DataSource=app.db;Cache=Shared")
				.Options
		);
	}
}