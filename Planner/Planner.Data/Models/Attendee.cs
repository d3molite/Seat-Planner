namespace Planner.Data.Models;

public class Attendee
{
	public string? FirstName { get; set; }
	
	public string? LastName { get; set; }
	
	public required string NickName { get; set; }

	public string? MailAddress { get; set; }
	
	public bool Confirmed { get; set; }

	public int NumberOfSeats { get; set; } = 1;
	
	public string SeatIdentifier { get; set; } = "";
}