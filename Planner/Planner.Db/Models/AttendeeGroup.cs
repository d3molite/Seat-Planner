using System.ComponentModel.DataAnnotations.Schema;
using Planner.Db.Interfaces;
using Planner.Db.Utils;

namespace Planner.Db.Models;

public class AttendeeGroup : DbBase
{
	public string Id { get; set; }
	
	public string Name { get; set; }

	public IEnumerable<Attendee> Attendees { get; set; }
}