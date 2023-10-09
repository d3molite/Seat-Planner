using Planner.Db.Utils;

namespace Planner.Db.Models;

public class Event : DbBase
{
	public int Year { get; set; }

	public List<Attendee> Attendees { get; set; } = new();

	public List<SeatingRow> SeatingRows { get; set; } = new();
}