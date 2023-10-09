using System.ComponentModel.DataAnnotations.Schema;
using Planner.Db.Enum;
using Planner.Db.Interfaces;
using Planner.Db.Utils;

namespace Planner.Db.Models;

public class Seat : DbBase
{
	public string Id { get; set; }
	
	public Category Category { get; set; }
	
	public string? OwnerName { get; set; }
	
	public string? SeatPosition { get; set; }

	[NotMapped]
	public Operation Operation { get; set; }
}