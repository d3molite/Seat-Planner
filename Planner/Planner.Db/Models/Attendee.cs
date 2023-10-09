using System.ComponentModel.DataAnnotations.Schema;
using Planner.Db.Interfaces;
using Planner.Db.Utils;

namespace Planner.Db.Models;

public class Attendee : DbBase
{
	public string FirstName { get; set; }
	
	public string LastName { get; set; }
	
	public string NickName { get; set; }

	public string MailAddress { get; set; }
	
	public bool Confirmed { get; set; }
	
	public IEnumerable<Seat> Seats { get; set; }
	
}