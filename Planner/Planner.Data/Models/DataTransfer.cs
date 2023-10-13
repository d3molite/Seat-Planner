namespace Planner.Data.Models;

public class DataTransfer
{
	public required List<Attendee> Attendees { get; set; }	
	
	public required List<SeatingRow> Rows { get; set; }
}