using System.ComponentModel.DataAnnotations.Schema;
using Planner.Db.Utils;

namespace Planner.Db.Interfaces;

public interface IHasOperation
{
	[NotMapped]
	public Operation Operation { get; set; }
}