using Planner.Db.Utils;

namespace Planner.Db.Models;

public class Event : DbBase
{
	public int Year { get; set; }
	
	public IEnumerable<AttendeeGroup> AttendeeGroups { get; set; }
}