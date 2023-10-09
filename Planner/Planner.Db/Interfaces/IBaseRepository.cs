namespace Planner.Db.Interfaces;

public interface IBaseRepository<T> 
	where T : class, IHasOperation, IDbItem
{
	public T? Get(string id);

	public T? GetCustom(Func<T, bool> match);

	public T[] GetAll();

	public bool Crud(T item);
}