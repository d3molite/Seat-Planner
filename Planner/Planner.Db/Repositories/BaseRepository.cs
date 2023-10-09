using Microsoft.EntityFrameworkCore;
using Planner.Db.Context;
using Planner.Db.Interfaces;
using Planner.Db.Utils;
using Serilog;

namespace Planner.Db.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T>
	where T : class, IHasOperation, IDbItem
{
	private PlannerDbContext GetContext()
	{
		return PlannerDbContext.Create();
	}

	public static bool TrySave(PlannerDbContext db)
	{
		try
		{
			db.SaveChanges();

			return true;
		}
		catch (Exception ex)
		{
			Log.Error(ex, "Error while saving Changes to Database");

			return false;
		}
	}

	public T? Get(string id)
	{
		using var db = GetContext();

		return db.Set<T>().FirstOrDefault(x => x.Id == id);
	}

	public T? GetCustom(Func<T, bool> match)
	{
		using var db = GetContext();

		return db.Set<T>().AsEnumerable().FirstOrDefault(x => match.Invoke(x));
	}

	public T[] GetAll()
	{
		using var db = GetContext();
		return db.Set<T>().ToArray();
	}

	public bool Crud(T item)
	{
		return item.Operation switch
		{
			Operation.Created => Create(item),
			Operation.Deleted => Delete(item),
			Operation.Updated => Update(item),
			Operation.None => true,
			var _ => false
		};
	}

	public bool Create(T item)
	{
		using var db = GetContext();
		db.Set<T>().Add(item);

		return TrySave(db);
	}

	public bool Update(T item)
	{
		using var db = GetContext();
		
		db.Set<T>().Update(item);

		return TrySave(db);
	}

	public bool Delete(T item)
	{
		using var db = GetContext();
		db.Set<T>().Remove(item);

		return TrySave(db);
	}
}