using System.ComponentModel.DataAnnotations.Schema;
using Planner.Db.Interfaces;

namespace Planner.Db.Utils;

public class DbBase : IDbItem, IHasOperation
{
	public string Id { get; set; }
	
	[NotMapped]
	public Operation Operation { get; set; }
	
}