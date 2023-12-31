﻿using Planner.Db.Utils;

namespace Planner.Db.Models;

public class Attendee : DbBase
{
	public string FirstName { get; set; }
	
	public string LastName { get; set; }
	
	public string NickName { get; set; }

	public string MailAddress { get; set; }
	
	public bool Confirmed { get; set; }
	
	public int NumberOfSeats { get; set; }
	public string SeatIdentifier { get; set; } = "";

	public List<Event> Events { get; set; } = new();

}